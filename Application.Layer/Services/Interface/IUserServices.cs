using Domain.Layer.Dtos.Account;
using Domain.Layer.Entities.Account;

namespace Application.Layer.Services.Interface
{
    public interface IUserServices : IAsyncDisposable
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDto register);
        Task<LoginUserDto.LoginUserResult> GetUserForLogin(LoginUserDto login);
        Task<User> GetUserById(long userId);
        Task<FilterUserDto> FilterUser(FilterUserDto filter);
        Task<EditUserDto> GetUserForEdit(long userId);
        Task<EditUserResult> EditUser(EditUserDto edit, string username);


        #region Role

        Task<FilterRoleDto> FilterRole(FilterRoleDto filter);
        Task<CreateRoleResult> CreateRole(CreateRoleDto role);
        Task<EditRoleDto> GetRoleForEdit(long roleId);
        Task<EditRoleResult> EditRole(EditRoleDto edit, string username);
        Task<List<Role>> GetRoles();


        #endregion
    }
}
