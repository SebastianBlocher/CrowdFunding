using crowdFunding.Services;
using System;
using System.Linq;

namespace crowdFunding
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrowdFundingDbContext())
            {
                IBackerService backerService = new BackerService(context);
                //ICustomerService customerService = new CustomerService(context);
                //IOrderService orderService = new OrderService(context, customerService, productService);


                //var backer = backerService.CreateBacker(new CreateBackerOptions()
                //{
                //    FirstName = "Odysseas",
                //    LastName = "Platsakis",
                //    Country = "Greece",
                //    Email = "odyplat@mail.com"
                //});

                //var backerList = backerService.SearchBacker(new SearchBackerOptions()
                //{
                //    LastName = "Platsakis"
                //}).ToList();

                //Console.WriteLine(backerList[0].FirstName);

                var backer = backerService.UpdateBacker(new UpdateBackerOptions()
                {
                    BackerId = 6,
                    FirstName = "Odysseas"
                });

            }
        }
    }
}
