using SimpleAOPSamples;
using SimpleAOPSamples.Shared;

Console.WriteLine("***\r\n Begin program - no logging\r\n");
//IRepository<Customer> customerRepository =
//  new Repository<Customer>();

//IRepository<Customer> customerRepository =
//  new LoggerRepository<Customer>(new Repository<Customer>());

IRepository<Customer> customerRepository =
 DynamicProxy<IRepository<Customer>>.Create(new Repository<Customer>());

var customer = new Customer
{
    Id = 1,
    Name = "Customer 1",
    Address = "Address 1"
};
customerRepository.Add(customer);
customerRepository.Update(customer);
customerRepository.Delete(customer);
Console.WriteLine("\r\nEnd program - no logging\r\n***");
Console.ReadLine();
