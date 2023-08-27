
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        public async Task<IActionResult> SignUp(SignupModel signup)
        {
            try
            {
                // insert
                if (signup.image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + signup.image.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                    signup.image.CopyTo(new FileStream(path, FileMode.Create));
                    signup.ProfileImage = fileName;
                }

                var result = await accountService.CreateAccount(signup);

                ViewData["AlertMessage"] = result;
                if (result.Succeeded) { 
                ViewData["Status"] = "Success";
                }
                else
                {
                    ViewData["Status"] = "Error";
                }
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Cerating User";
                ViewData["Status"] = "Error";
            }


            return View("CreateAccount");
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> CheckPassword(SignInModel signin)
        {
            var result= await accountService.SignIn(signin);

            if (result.Succeeded)
            {
                UserDTO user= await accountService.FindByName(signin.Email);
                UserProfile.Id = user.Id;
                UserProfile.Name = user.Name;
                UserProfile.Username = user.Name;
                UserProfile.Phone = user.Phone;
                UserProfile.ProfileImage = user.ProfileImage;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["AlertMessage"] = "Invalid Username or Password";
                ViewData["Status"] = "Error";
                return View("SignIn");
            }
        }

        public async Task<IActionResult> RoleList()
        {
            List<RoleDTO> roles =await accountService.GetRoles();
            return View(roles);
        }

        public IActionResult NewRole()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            try { 
            var result = await accountService.CreateRole(role);

                ViewData["AlertMessage"] = result;
                if (result.Succeeded)
                {
                    ViewData["Status"] = "Success";
                }
                else
                {
                    ViewData["Status"] = "Error";
                }
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While adding Role ";
                ViewData["Status"] = "Error";
            }
            return View("NewRole");
        }

        public async Task<IActionResult> UserList()
        {
            List<UserDTO> users=await accountService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> UserRoles(string userId,string name)
        {
            TempData["name"] = name;
            List<RoleDTO> roles=await accountService.getRoles(userId);
            return View(roles);
        }

        public async Task<IActionResult> UpdateRole(List<RoleDTO> liRoles)
        {
            List<RoleDTO> roles = new List<RoleDTO>();
            try
            {
                await accountService.UpdateRoles(liRoles);
                roles = await accountService.getRoles(liRoles[0].UserId);
                ViewData["AlertMessage"] = "User Role Updated Successfully";
                ViewData["Status"] = "Success";
            }
            catch (Exception ex)
            {
                ViewData["AlertMessage"] = "Error While Updating User Role ";
                ViewData["Status"] = "Error";
            }
            return View("UserRoles", roles);
        }

        public async Task<IActionResult> DeleteUser(string Id)
        {
            var result = await accountService.DeleteUser(Id);
            return RedirectToAction("UserList");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await accountService.SignOut();
            return View("SignIn");
        }


    }
}
