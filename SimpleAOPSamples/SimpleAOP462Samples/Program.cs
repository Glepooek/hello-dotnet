using SimpleAOPSamples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAOP462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***\r\n Begin program - no logging\r\n");
            //IRepository<Customer> customerRepository =
            //  new Repository<Customer>();

            //IRepository<Customer> customerRepository =
            //  new LoggerRepository<Customer>(new Repository<Customer>());

            //IRepository<Customer> customerRepository =
            //    RepositoryFactory.Create<Customer>();

            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity("Administrator"),
                new[] { "ADMIN" });
            IRepository<Customer> customerRepository =
                RepositoryFactory.Create<Customer>();

            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            customerRepository.GetAll();
            Console.WriteLine("\r\nEnd program - no logging\r\n***");
            Console.ReadLine();

        }
    }
}
