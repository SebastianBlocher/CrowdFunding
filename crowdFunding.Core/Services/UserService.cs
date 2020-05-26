using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Result<User> CreateUser(CreateUserOptions options)
        {

            if (options == null)
            {
                return Result<User>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }
            if (string.IsNullOrWhiteSpace(options.FirstName))
            {
                return Result<User>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty FirstName");
            }

            if (string.IsNullOrWhiteSpace(options.LastName))
            {
                return Result<User>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty LastName");
            }

            if (string.IsNullOrWhiteSpace(options.Email))
            {
                return Result<User>.ActionFailed(
                     StatusCode.BadRequest, "Null or empty Email");
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                return Result<User>.ActionFailed(
                       StatusCode.BadRequest, "Null or empty Description");
            }

            if (string.IsNullOrWhiteSpace(options.Country))
            {
                return Result<User>.ActionFailed(
                     StatusCode.BadRequest, "Null or empty Country");
            }

            var user = new User()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Country = options.Country,
                Description = options.Description,
            };

            context.Add(user);

            var rows = 0;

            try
            {
                rows = context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<User>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<User>.ActionFailed(
                    StatusCode.InternalServerError,
                    "User could not be created");
            }

            return Result<User>.ActionSuccessful(user);
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
                        .Set<User>().Where(x => x.UserId == id);
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

            if (!string.IsNullOrWhiteSpace(options.Country))
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

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(x => x.Email == options.Email);
            }

            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                query = query.Where(x => x.FirstName == options.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                query = query.Where(x => x.LastName == options.LastName);
            }

            if (options.UserId != null)
            {
                query = query.Where(x => x.UserId == options.UserId);
            }

            return query
                .Include(x => x.BackedProjectsList)
                .Include(y => y.CreatedProjectsList);
        }

        public Result<User> UpdateUser(int userId,
            UpdateUserOptions options)
        {
            var result = new Result<User>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var user = GetById(userId)
                        .Include(x => x.CreatedProjectsList)
                        .Include(c => c.BackedProjectsList)
                        .SingleOrDefault();

            if (user == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"User with id {userId} was not found";

                return result;
            }

            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                user.FirstName = options.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(options.Country))
            {
                user.Country = options.Country;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                user.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                user.Email = options.Email;
            }

            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                user.LastName = options.LastName;
            }

            var rows = 0;

            try
            {
                rows = context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<User>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<User>.ActionFailed(
                    StatusCode.InternalServerError,
                    "User could not be updated");
            }

            return Result<User>.ActionSuccessful(user);
        }
    }
}
