using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public class BackerService : IBackerService
    {
        private CrowdFundingDbContext context_;

        public BackerService(CrowdFundingDbContext context)
        {
            context_ = context;
        }
        public Backer CreateBacker(CreateBackerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var backer = new Backer()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Country = options.Country
            };

            context_.Add(backer);

            if (context_.SaveChanges() > 0)
            {
                return backer;
            }

            return null;
        }

        public IQueryable<Backer> SearchBacker(SearchBackerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                 .Set<Backer>()
                 .AsQueryable();

            if (options.BackerId != null)
            {
                query = query.Where(b => b.BackerId == options.BackerId);
            }

            if (options.FirstName != null)
            {
                query = query.Where(b => b.FirstName == options.FirstName);
            }

            if (options.LastName != null)
            {
                query = query.Where(b => b.LastName == options.LastName);
            }

            if (options.Email != null)
            {
                query = query.Where(b => b.Email == options.Email);
            }

            if (options.Country != null)
            {
                query = query.Where(b => b.Country == options.Country);
            }

            if (options.CreatedFrom != null)
            {
                query = query.Where(b => b.CreatedOn >= options.CreatedFrom);
            }

            if (options.CreatedTo != null)
            {
                query = query.Where(b => b.CreatedOn <= options.CreatedTo);
            }

            if (options.IsActive != null)
            {
                query = query.Where(b => b.IsActive == options.IsActive);
            }

            query = query.Take(500);

            return query;
        }

        public Backer UpdateBacker(UpdateBackerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var backer = SearchBacker(new SearchBackerOptions()
            {
                BackerId = options.BackerId
            }).SingleOrDefault();

            if (backer == null)
            {
                return null;
            }

            if (options.FirstName != null)
            {
                backer.FirstName = options.FirstName;
            }
            if (options.LastName != null)
            {
                backer.LastName = options.LastName;
            }
            if (options.Email != null)
            {
                backer.Email = options.Email;
            }
            if (options.Country != null)
            {
                backer.Country = options.Country;
            }
            if (options.IsActive != null)
            {
                backer.IsActive = options.IsActive.Value;
            }

            context_.SaveChanges();

            return backer;
        }
    }
}
