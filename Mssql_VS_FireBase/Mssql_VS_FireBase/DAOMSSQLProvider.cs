using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    class DAOMSSQLProvider : IDAOProvider
    {
        public bool AddCustomer(Customer customer)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Customer.Any(cus => cus.Id == customer.Id);
                if (has == false)
                {
                    entities.Customer.Add(new Customer { Id = customer.Id, Name = customer.Name, Country = customer.Country, Age = customer.Age });
                    entities.SaveChanges();
                    return true;
          
                }
                else
                {
                    return false;
                }

            }
        }

        public bool AddOrder(Order order)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Order.Any(ord => ord.Id == order.Id);

                if (has == false)
                {
                    entities.Order.Add(new Order { Id = order.Id, Customer_Id  = order.Customer_Id , Price = order.Price, Date = order.Date });
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                   return false;
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {           
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                entities.Customer.ToList().ForEach(c => Console.WriteLine(c.ToString()));
                return entities.Customer.ToList();
            }
        }

        public List<OrderCustomer> GetAllOrderCustomer()
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                var list = entities.Order.Join(entities.Customer,
                                order => order.Customer_Id,
                                Customer => Customer.Id,
                                (order, Customer) => new OrderCustomer
                                {
                                    Id = Customer.Id,
                                    Name = Customer.Name,
                                    Price = order.Price,
                                    Date = order.Date
                                });

                return list.ToList();

                //entities.Order.Join(entities.Customer,
                //                order => order.Customer_Id,
                //                Customer => Customer.Id,
                //                (order, Customer) => new OrderCustomer
                //                {
                //                    Id = Customer.Id,
                //                    Name = Customer.Name,
                //                    Price = order.Price,
                //                    Date = order.Date
                //                }).ToList().ForEach(r => Console.WriteLine(r.ToString()));

                 
            }
        }

        public List<Order> GetAllOrders()
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                entities.Order.ToList().ForEach(o => Console.WriteLine(o.ToString()));
                return entities.Order.ToList();
            }
        }

        public List<Order> GetAllOrdersByCustomerId(int CustomerId)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                entities.Order.Where(p => p.Customer_Id == CustomerId).ToList().ForEach(p => Console.WriteLine($"get all oders cust by ID" + p.ToString()));
                return entities.Order.Where(p => p.Customer_Id == CustomerId).ToList();
            }
        }

        public Customer GetCustomerByID(int customerId)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                Customer c = new Customer();
                c = entities.Customer.SingleOrDefault(p => p.Id == customerId);
                return c;

            }
        }

        public Order GetOrderById(int OrderId)
        {
            Order o = new Order();

            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
              o =   entities.Order.SingleOrDefault(p => p.Id == OrderId);
                return o;

            }
        }

        public bool RemoveCustomer(int customerId)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Customer.Any(cus => cus.Id == customerId);
                if (has == true)
                {
                    entities.Customer.Remove(entities.Customer.Where (c => c.Id == customerId).FirstOrDefault());
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveOrder(int orderId)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Order.Any(o => o.Id == orderId);
                if (has == true)
                {
                    entities.Order.Remove(entities.Order.Where(o => o.Id == orderId).FirstOrDefault());
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Customer.Any(c => c.Id == customer.Id);
                if (has == true)
                {
                    entities.Customer.Take(1).FirstOrDefault().Name = $"{customer.Name}";
                    entities.Customer.Take(1).FirstOrDefault().Country = $"{customer.Country}";
                    entities.Customer.Take(1).FirstOrDefault().Age = customer.Age;
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool UpdateOrder(Order order)
        {
            using (FireBase_VS_MSSQLEntities3 entities = new FireBase_VS_MSSQLEntities3())
            {
                bool has = entities.Order.Any(o => o.Id == order.Id);
                if (has == true)
                {
                    entities.Order.Take(1).FirstOrDefault().Customer_Id = order.Customer_Id;
                    entities.Order.Take(1).FirstOrDefault().Date = order.Date;
                    entities.Order.Take(1).FirstOrDefault().Price = order.Price;
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
