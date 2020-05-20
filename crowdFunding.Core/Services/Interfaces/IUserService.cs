using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Core.Services.Interfaces
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
