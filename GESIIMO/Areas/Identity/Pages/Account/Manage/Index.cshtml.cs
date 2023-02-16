using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GESIIMO.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<GESIIMO.Data.ApplicationUser> _userManager;
        private readonly SignInManager<GESIIMO.Data.ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<GESIIMO.Data.ApplicationUser> userManager,
            SignInManager<GESIIMO.Data.ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            [Display(Name = "Nombre Completo")]
            public string NombrePersona { get; set; }
        }

        private async Task LoadAsync(GESIIMO.Data.ApplicationUser user)
        {
            var dbuser = await _userManager.GetUserAsync(User);
            var userName = dbuser.UserName;
            var phoneNumber = dbuser.PhoneNumber;
            var NombreP = dbuser.NombrePersona;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                NombrePersona = NombreP
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.NombrePersona != user.NombrePersona)
            {
                user.NombrePersona = Input.NombrePersona;
            }

            if (Input.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }

            var success = await _userManager.UpdateAsync(user);
            if (!success.Succeeded)
            {
                StatusMessage = "Error inesperado al intentar actualizar el usuario.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Tu perfil ha sido actualizado";
            return RedirectToPage();
        }
    }
}
