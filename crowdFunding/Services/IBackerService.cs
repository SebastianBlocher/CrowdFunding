using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public interface IBackerService
    {
        Backer CreateBacker(CreateBackerOptions options);
        IQueryable<Backer> SearchBacker(SearchBackerOptions options);
        Backer UpdateBacker(UpdateBackerOptions options);
    }
}
