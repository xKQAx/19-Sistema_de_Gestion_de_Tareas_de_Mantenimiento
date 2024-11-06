using DemoDeIdentity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoDeIdentity.Pages.Account;

[Authorize]
public class RolesModel : PageModel
{
    private readonly RoleManager<MyRol> _roleManager;
    private readonly UserManager<MyUser> _userManager;

    [BindProperty]
    public RolesDTO Rol { get; set; }
    public RolesModel(
        RoleManager<MyRol> roleManager,
        UserManager<MyUser> userManager
        )
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task<ActionResult> OnGet()
    {
        var MyRoles = await _roleManager.Roles.ToListAsync();
        ViewData["roles"] = MyRoles;
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var newRol = new MyRol
        {
            Name = Rol.Name,
            FechaAlta = DateTime.Now,
            Seccion = Rol.Seccion // Actualizamos a "Correo" en lugar de "Seccion"
        };

        // Crear el nuevo rol
        var roleResult = await _roleManager.CreateAsync(newRol);

        // Verificar si el rol se creó correctamente
        if (roleResult.Succeeded)
        {
            // Obtener el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roleAssignResult = await _userManager.AddToRoleAsync(user, Rol.Name);
                if (!roleAssignResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Error asignando rol al usuario.");
                    return Page();
                }
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Error al crear el rol.");
        }

        return RedirectToPage("/account/roles");
    }
}

