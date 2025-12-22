using Application.Layer.Services.Interface;
using Domain.Layer.Dtos.Account;
using Domain.Layer.Dtos.Paging;
using Domain.Layer.Entities.Account;
using Domain.Layer.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Layer.Services.Implementation
{
    public class UserServices : IUserServices
    {
        #region Constructor

        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserServices(IGenericRepository<User> userRepository, IPasswordHasher passwordHasher, 
            IGenericRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        #endregion

        #region Account

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDto register)
        {
            try
            {
                    var user = new User
                    {
                        Mobile = register.Mobile,
                        Email = register.Email != null ? register.Email : "info@gmail.com",
                        Password = _passwordHasher.EncodePasswordMd5(register.Password),
                        ActivationCode = register.ActivationCode != null ? register.ActivationCode : null,
                        RoleId = 2,
                    };


                    await _userRepository.AddEntity(user);
                    await _userRepository.SaveChanges();

                    return RegisterUserResult.Success;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<LoginUserDto.LoginUserResult> GetUserForLogin(LoginUserDto login)
        {
            var user = await _userRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Mobile == login.Mobile);

            if (user == null)
            {
                return LoginUserDto.LoginUserResult.NotFound;
            }

            if (user.Password != _passwordHasher.EncodePasswordMd5(login.Password))
            {
                return LoginUserDto.LoginUserResult.NotFound;
            }

            return LoginUserDto.LoginUserResult.Success;
        }
        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository
                .GetQuery()
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.Id == userId);

        }
        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Mobile == mobile);
        }
        public async Task<string> GetUserMobileById(long userId)
        {
            var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user.Mobile;
        }
        public async Task<FilterUserDto> FilterUser(FilterUserDto filter)
        {
            var query = _userRepository
                .GetQuery()
                .Include(x => x.Role)
                .AsQueryable();

            if (filter.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == filter.RoleId);
            }
            if (!string.IsNullOrEmpty(filter.Mobile))
            {
                query = query.Where(x => EF.Functions.Like(x.Mobile, $"%{filter.Mobile}%"));
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(x => EF.Functions.Like(x.Email, $"%{filter.Email}%"));
            }

            #region Paging


            var roleCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, roleCount, filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).OrderByDescending(x => x.Id).ToListAsync();


            #endregion

            return filter.SetPaging(pager).SetUsers(allEntities);
        }
        public async Task<EditUserDto> GetUserForEdit(long userId)
        {
            var user = await _userRepository.GetQuery()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return new EditUserDto();
            }

            return new EditUserDto
            {
                Id = user.Id,
                RoleId = user.Role.Id,
                Email = user.Email,
                Mobile = user.Mobile,
                IsBlocked = user.IsBlocked,
            };
        }
        public async Task<EditUserResult> EditUser(EditUserDto edit)
        {
            var mainUser = await _userRepository
                .GetQuery()
                .AsQueryable()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == edit.Id);

            if (mainUser == null)
            {
                return EditUserResult.UserNotFound;
            }

            mainUser.Id = edit.Id;
            mainUser.RoleId = edit.RoleId;
            mainUser.Mobile = edit.Mobile;
            mainUser.Email = edit.Email;
            mainUser.IsBlocked = edit.IsBlocked;
            mainUser.ActivationCode = edit.ActivationCode;
            

            _userRepository.EditEntity(mainUser);
            await _userRepository.SaveChanges();

            return EditUserResult.Success;
        }

        #endregion

        #region Role

        public async Task<FilterRoleDto> FilterRole(FilterRoleDto filter)
        {
            var query = _roleRepository
                .GetQuery()
                .Include(x => x.Users)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.RoleName))
            {
                query = query.Where(x => EF.Functions.Like(x.RoleName, $"%{filter.RoleName}%"));
            }

            #region Paging

            var roleCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, roleCount, filter.TakeEntity,
                filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();


            #endregion

            return filter.SetPaging(pager).SetRoles(allEntities);
        }
        public async Task<CreateRoleResult> CreateRole(CreateRoleDto role)
        {
            var newRole = new Role
            {
                RoleName = role.RoleName,
            };

            await _roleRepository.AddEntity(newRole);
            await _roleRepository.SaveChanges();

            return CreateRoleResult.Success;
        }
        public async Task<EditRoleDto> GetRoleForEdit(long roleId)
        {
            var role = await _roleRepository.GetEntityById(roleId);
            if (role == null)
            {
                return null;
            }

            return new EditRoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
        public async Task<EditRoleResult> EditRole(EditRoleDto edit)
        {
            var mainRole = await _roleRepository
                .GetQuery()
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.Id == edit.Id);

            if (mainRole == null)
            {
                return EditRoleResult.Error;
            }

            mainRole.Id = edit.Id;
            mainRole.RoleName = edit.RoleName;

            _roleRepository.EditEntity(mainRole);
            await _roleRepository.SaveChanges();

            return EditRoleResult.Success;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await _roleRepository
                .GetQuery()
                .AsQueryable()
                .Select(x => new Role
                {
                    Id = x.Id,
                    RoleName = x.RoleName
                })
                .ToListAsync();

        }

        #endregion


        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_userRepository != null)
            {
                await _userRepository.DisposeAsync();
            }

            if (_roleRepository != null)
            {
                await _roleRepository.DisposeAsync();
            }
        }

        #endregion

        
    }
}
