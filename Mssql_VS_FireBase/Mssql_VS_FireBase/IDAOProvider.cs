using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    interface IDAOProvider
    {
         List<Customer> GetAllCustomers();
         List<Order> GetAllOrders();
         List<Order> GetAllOrdersByCustomerId(int CustomerId);
         Order GetOrderById(int OrderId);
         Customer GetCustomerByID(int customerId);
         bool AddCustomer(Customer customer);
         bool RemoveCustomer(int customerId);
         bool UpdateCustomer(Customer customer);
         bool AddOrder(Order order);
         bool RemoveOrder(int orderId);
         bool UpdateOrder(Order order);
         List<OrderCustomer> GetAllOrderCustomer();








    }
}
