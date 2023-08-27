using DocumentFormat.OpenXml.Office2010.ExcelAc;
using InventorySystem.data;
using InventorySystem.IServices;
using InventorySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.services
{
    public class AccountService: IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager) {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> CreateAccount(SignupModel signup)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = signup.Email;
            user.Email = signup.Email;
            user.Name = signup.Name;
            user.PhoneNumber = signup.Phone;
            user.ProfileImage = signup.ProfileImage;
            //user.PasswordHash = signup.Password;

            var result=await userManager.CreateAsync(user,signup.Password);

            return result;


        }


        public async Task<SignInResult> SignIn(SignInModel signIn)
        {
            var result=await signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, signIn.RememberMe, false);
            return result;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateRole(RoleModel role)
        {
            IdentityRole newrole = new IdentityRole()
            {
                Name = role.Name
            };
            //IdentityRole newrole = new IdentityRole();

            //newrole.Name = role.Name;

            var result=await roleManager.CreateAsync(newrole);

            return result;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            List<ApplicationUser> allusers= await userManager.Users.ToListAsync();

            List<UserDTO> users = new List<UserDTO>();

            foreach (ApplicationUser item in allusers)
            {
                users.Add(new UserDTO()
                {
                    Id=item.Id,
                    Name=item.Name,
                    Phone=item.PhoneNumber,
                    ProfileImage=item.ProfileImage,
                    Username=item.UserName
                });
            }

            return users;


        }

        public async Task<List<RoleDTO>> getRoles(string userId)
        {
            List<IdentityRole> allroles = await roleManager.Roles.ToListAsync();

            List<RoleDTO> roles = new List<RoleDTO>();
            foreach (IdentityRole item in allroles)
            {
                roles.Add(new RoleDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsSelected=false,
                    UserId= userId
                });
            }

            var user = await userManager.FindByIdAsync(userId);
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (RoleDTO item in roles)
            {
                if (userRoles.Contains(item.Name)==true)
                {
                    item.IsSelected = true;
                }
            }

            return roles;


        }

        public async Task UpdateRoles(List<RoleDTO> liUserRoles)
        {
            // id user => user

            foreach (RoleDTO item in liUserRoles)
            {
                ApplicationUser user = await userManager.FindByIdAsync(item.UserId);
                if (item.IsSelected == true)
                {                    
                    if (await userManager.IsInRoleAsync(user,item.Name)==false) { 
                        await userManager.AddToRoleAsync(user, item.Name);
                    }
                }
                else
                {
                    if(await userManager.IsInRoleAsync(user, item.Name) == true)
                    {
                        await userManager.RemoveFromRoleAsync(user, item.Name);
                    }
                }
            }
        }

        public async Task<UserDTO> FindByName(string Username)
        {
            var user= await userManager.FindByNameAsync(Username);
            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                Name=user.Name,
                Username=user.UserName,
                ProfileImage=user.ProfileImage,
                Phone=user.ProfileImage

            };
            return userDTO;

        }

        public async Task<IdentityResult> DeleteUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var result = await userManager.DeleteAsync(user);
            return result;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var allroles= await roleManager.Roles.ToListAsync();

            List<RoleDTO> roles = new List<RoleDTO>();
            foreach (var item in allroles)
            {
                roles.Add(new RoleDTO()
                {
                    Id=item.Id,
                    Name=item.Name
                });

            }

            return roles;
        }


    }
}
