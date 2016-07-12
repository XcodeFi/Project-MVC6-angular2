//using Graduation.Entities;
//using Graduation.Infrastructure.Repositories.Abstract;
//using System.Collections.Generic;

//namespace Graduation.Infrastructure.Repositories
//{
//    public class UserRepository:EntityBaseRepository<User>, IUserRepository
//    {
//        IRoleRepository _roleReposistory;
//        public UserRepository(GraduationContext context, IRoleRepository roleReposistory)
//            : base(context)
//        {
//            _roleReposistory = roleReposistory;
//        }

//        public User GetSingleByUsername(string username)
//        {
//            return this.GetSingle(x => x.Username == username);
//        }

//        public IEnumerable<Role> GetUserRoles(string usernameOrEmail)
//        {
//            List<Role> _roles = null;

//            User _user = this.GetSingle(u => u.Username == usernameOrEmail||u.Email==usernameOrEmail, u => u.UserRoles);
//            if (_user != null)
//            {
//                _roles = new List<Role>();
//                foreach (var _userRole in _user.UserRoles)
//                    _roles.Add(_roleReposistory.GetSingle(_userRole.RoleId));
//            }

//            return _roles;
//        }
//    }
//}
