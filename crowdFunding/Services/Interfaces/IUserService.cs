using crowdFunding.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public interface IUserService
    {
        User CreateUser(CreateUserOptions options);

        IQueryable<User> SearchUsers(SearchUserOptions options);

        IQueryable<User> GetById(int? id);

        User UpdateUser(UpdateUserOptions options);

        bool DisableUser(int? id);
    }
}
