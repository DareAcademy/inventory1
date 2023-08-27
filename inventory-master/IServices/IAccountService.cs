using InventorySystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateAccount(SignupModel signup);

        Task<SignInResult> SignIn(SignInModel signIn);

        Task<IdentityResult> CreateRole(RoleModel role);

        Task<List<UserDTO>> GetUsers();

        Task<List<RoleDTO>> getRoles(string userId);

        Task UpdateRoles(List<RoleDTO> liUserRoles);

        Task SignOut();

        Task<UserDTO> FindByName(string Username);

        Task<List<RoleDTO>> GetRoles();

        Task<IdentityResult> DeleteUser(string Id);
    }

}
