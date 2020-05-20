using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class UserService : IUserService
    {
        private CrowdFundingDbContext context;
        public UserService(CrowdFundingDbContext dbcontext)
        {
            context = dbcontext;
        }

        public User CreateUser(CreateUserOptions options)
        {
            if (options == null 
                || string.IsNullOrWhiteSpace(options.FirstName) 
                || string.IsNullOrWhiteSpace(options.LastName) 
                || string.IsNullOrWhiteSpace(options.Email))
            {
                return null;
            }

            var user = new User()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Country = options.Country,
            };

            context.Add(user);

            return context.SaveChanges() > 0 ? user : null;
        }

        public bool DisableUser(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var user = GetById(id)
                        .Include(c => c.CreatedProjectsList)
                        .SingleOrDefault();

            if (user == null)
            {
                return false;
            }

            user.IsActive = false;

            if (user.CreatedProjectsList != null)
            {
                user.CreatedProjectsList.Clear();
            }

            return context.SaveChanges() > 0;
        }

        public IQueryable<User> GetById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return context
                        .Set<User>()
                        .Where(x => x.UserId == id);
        }

        public IQueryable<User> SearchUsers(SearchUserOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context
                        .Set<User>()
                        .AsQueryable();

            if (options.AmountFrom != null)
            {
                query = query.Where(x => x.Amount >= options.AmountFrom);
            }

            if (options.AmountTo != null)
            {
                query = query.Where(x => x.Amount <= options.AmountTo);
            }

            if (string.IsNullOrWhiteSpace(options.Country))
            {
                query = query.Where(x => x.Country == options.Country);
            }

            if (options.CreateOnFrom != null)
            {
                query = query.Where(x => x.CreatedOn >= options.CreateOnFrom);
            }

            if (options.CreateOnTo != null)
            {
                query = query.Where(x => x.CreatedOn >= options.CreateOnTo);
            }

            if (string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(x => x.Email == options.Email);
            }

            if (string.IsNullOrWhiteSpace(options.FirstName))
            {
                query = query.Where(x => x.FirstName == options.FirstName);
            }

            if (string.IsNullOrWhiteSpace(options.LastName))
            {
                query = query.Where(x => x.LastName == options.LastName);
            }

            if (options.UserId != null)
            {
                query = query.Where(x => x.UserId == options.UserId);
            }

            return query;
        }

        public User UpdateUser(UpdateUserOptions options)
        {
            if (options == null || options.UserId == null)
            {
                return null;
            }

            var user = GetById(options.UserId)
                        .Include(x => x.CreatedProjectsList)
                        .Include(c => c.BackedProjectsList)
                        .SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.FirstName))
            {
                user.FirstName = options.FirstName;
            }

            if (string.IsNullOrWhiteSpace(options.Country))
            {
                user.Country = options.Country;
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                user.Description = options.Description;
            }

            if (string.IsNullOrWhiteSpace(options.Email))
            {
                user.Email = options.Email;
            }

            if (string.IsNullOrWhiteSpace(options.LastName))
            {
                user.LastName = options.LastName;
            }

            context.SaveChanges();
            return user;
        }
    }
}
