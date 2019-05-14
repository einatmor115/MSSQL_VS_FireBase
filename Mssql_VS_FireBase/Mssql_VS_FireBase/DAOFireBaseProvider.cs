using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    class DAOFireBaseProvider : IDAOProvider
    {
 
        static IFirebaseClient firebaseClient;
        static IFirebaseConfig config;

        static DAOFireBaseProvider()
        {
            config = new FirebaseConfig
            {
                AuthSecret = ConfigurationManager.AppSettings["secret"],
                BasePath = ConfigurationManager.AppSettings["URL"]
            };

            firebaseClient = new FireSharp.FirebaseClient(config);
                if (firebaseClient != null)
                {
                    Console.WriteLine("Connection Succeeded!");
                }
        }

        public bool AddCustomer(Customer customer)
        {
            FirebaseResponse response = firebaseClient.Get($"Customer/{customer.Id}");
            
            if (response.Body != "null")
            {
                Console.WriteLine("cust id already exist in Data");
                return false;
            }
            else
            {
                SetResponse response1 = firebaseClient.Set($"Customer/{customer.Id}", customer);
                DataCust result = response1.ResultAs<DataCust>();
                Console.WriteLine("cust added yofi(:");
                return true;
            }
           
        }

        public bool AddOrder(Order order)
        {
            FirebaseResponse response = firebaseClient.Get($"Order/{order.Id}");
            FirebaseResponse response2 = firebaseClient.Get($"Customer/{order.Customer_Id}");

            if (response.Body != "null")
            {
                Console.WriteLine("order id already exist in Data");
                return false;
            }
            else
            {
                if (response2.Body == "null")
                {
                    Console.WriteLine("cust not exist");
                    return false;
                }
                else
                {
                    SetResponse response1 = firebaseClient.Set($"Order/{order.Id}", order);
                    DataOrder result = response1.ResultAs<DataOrder>();
                    Console.WriteLine("order added yofi(:");
                    return true;
                }
      
            }
        }

        public List<Customer> GetAllCustomers()
        {
            FirebaseResponse response = firebaseClient.Get("Customer");            
            List<Customer> result = response.ResultAs<List<Customer>>();
            result.RemoveAt(0);
            result.ForEach(d => Console.WriteLine(d));
            return result;
        }

        public List<OrderCustomer> GetAllOrderCustomer()
        {
            List<Customer> c = GetAllCustomers();
            List<Order> o = GetAllOrders();

           var result = (
                from Customer in c
                join Order in o 
                on Customer.Id equals Order.Customer_Id
                select new
                {
                    resultId = Order.Id,
                    resultCustName = Customer.Name,
                    resultCustomerId = Customer.Id,
                    resultPrice = Order.Price,
                    resulDate = Order.Date

                }).ToList();

            List<OrderCustomer> AllOrdersCustomers = new List<OrderCustomer>();

            foreach (var r in result)
            {
                AllOrdersCustomers.Add(new OrderCustomer()
                {
                    Id = r.resultId,
                    Name = r.resultCustName,
                    Price = r.resultPrice,
                    Date = r.resulDate
                });
            }
            return AllOrdersCustomers;
        }

        public List<Order> GetAllOrders()
        {
            FirebaseResponse response = firebaseClient.Get("Order");
            List<Order> result = response.ResultAs<List<Order>>();
            result.RemoveAt(0);
            result.ForEach(o => Console.WriteLine(o.ToString()));
            return result;
        }

        public List<Order> GetAllOrdersByCustomerId(int CustomerId)
        {
            FirebaseResponse response = firebaseClient.Get("Order");

            List<Order> result = GetAllOrders();
            List<Order> newResult = new List<Order>();

            foreach (Order o in result)
            {
                if (o.Customer_Id == CustomerId)
                {
                    newResult.Add(o);
                }
            }
            return newResult;
        }

        public Customer GetCustomerByID(int customerId)
        {
            Customer c = new Customer();
            List <Customer> AllCustomers = GetAllCustomers();
            foreach(Customer c1 in AllCustomers)
            {
                if (c1.Id == customerId)
                {
                    c = c1;
                }               
            }
            if (c.Id == 0)
            {
                Console.WriteLine("cust does not exist");
            }

            return c;
        }

        public Order GetOrderById(int OrderId)
        {
            Order o = new Order();
            List<Order> AllOrders = GetAllOrders();
            foreach (Order o1 in AllOrders)
            {
                if (o1.Id == OrderId)
                {
                    o = o1;
                }
            }
            if (o.Id == 0)
            {
                Console.WriteLine("order does not exist");
            }

            return o;
        }

        public bool RemoveCustomer(int customerId)
        {
            List <Order> OrderList = GetAllOrdersByCustomerId(customerId);

            if (OrderList.Count == 0 )
            {
                DeleteResponse response = firebaseClient.Delete($"Customer/{customerId}");
                return true;
            }
            else
            {
                Console.WriteLine("cust has orders");
                return false;
            }
        }

        public bool RemoveOrder(int orderId)
        {
            DeleteResponse response = firebaseClient.Delete($"Order/{orderId}");
            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            FirebaseResponse response = firebaseClient.Update($"Customer/{customer.Id}", customer);
            Customer result = response.ResultAs<Customer>();
            return true;
        }

        public bool UpdateOrder(Order order)
        {
            Customer c = GetCustomerByID(order.Customer_Id);
            if (c.Id != 0)
            {
                FirebaseResponse response = firebaseClient.Update($"Order/{order.Id}", order);
                return true;
            }
            else
            {
                Console.WriteLine("cust not exist");
                return false;
            }
        }
    }
}
