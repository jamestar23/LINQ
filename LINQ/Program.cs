using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Customer[] customersArray = new Customer[3]
            {
                new Customer(1, "Smith"),
                new Customer(2, "Johnson"),
                new Customer(3, "Rabon"),
            };


            Customers customersList = new Customers(customersArray);
                foreach (Customer c in customersList)
                    Console.WriteLine(c.CustomerNo + " " + c.CustomerName);


            var listOfOrders = new List<Orders>();
            listOfOrders.Add(new Orders { CustomerNo = 1, NumberOfItems = 3, OrderNo = 11 });
            listOfOrders.Add(new Orders { CustomerNo = 1, NumberOfItems = 2, OrderNo = 12 });
            listOfOrders.Add(new Orders { CustomerNo = 2, NumberOfItems = 2, OrderNo = 13 });
            listOfOrders.Add(new Orders { CustomerNo = 3, NumberOfItems = 4, OrderNo = 14 });

            foreach (var item in listOfOrders.OrderBy(c => c.CustomerNo))
            {
                Console.WriteLine("Order No: " + item.OrderNo
                    + " -- Customer No: " + item.CustomerNo + " -- Number Of Items: " +
                    item.NumberOfItems);
            }

            Console.WriteLine();
            // Print a list of Orders grouped by Customer # and in ascending Order #sequence

            var OrderedListOfOrders = listOfOrders
                .GroupBy(c => c.CustomerNo)
                .Select(y => new { customerNo = y.Key, Orders = y.Count() })
                .OrderBy(c => c.Orders)
                .ToList();
            foreach (var item in OrderedListOfOrders)
            {
                Console.WriteLine($"Customer #: {item.customerNo} - Orders {item.Orders}");
            }
        }
    }


    public class Customer
    {
        public Customer(int custNo, string custName)
        {
            this.CustomerNo = custNo;
            this.CustomerName = custName;
        }
        public int CustomerNo;
        public string CustomerName;
    }
    public class Customers : IEnumerable
    {
        private Customer[] _customers;
        public Customers(Customer[] cArray)
        {
            _customers = new Customer[cArray.Length];

            for (int i = 0; i < cArray.Length; i++)
            {
                _customers[i] = cArray[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public CustomersEnum GetEnumerator()
        {
            return new CustomersEnum(_customers);
        }
    }

    public class CustomersEnum : IEnumerator
    {
        public Customer[] _customers;

        int position = -1;

        public CustomersEnum(Customer[] list)
        {
            _customers = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _customers.Length);
        }

        public void Reset()
        {
            position = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Customer Current
        {
            get
            {
                try
                {
                    return _customers[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    public class Orders
    {
        public int OrderNo { get; set; }
        public int CustomerNo { get; set; }
        public int NumberOfItems { get; set; }
    }
}

