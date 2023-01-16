using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAppp
{
    abstract class Delivery
    {
        protected Address Address;
        protected MobileNumber MobileNumber;

        public Delivery(Address address, MobileNumber mobileNumber)
        {
            Address = address;
            MobileNumber = mobileNumber;
        }

        public abstract void Deliver(List<Product> products);


        public string GetDeliveryAddress()
        {
            return Address.ToString();
        }
    }

    class HomeDelivery : Delivery
    {
        private Courier _courier;
        public HomeDelivery(Address address, MobileNumber mobileNumber) : base(address, mobileNumber)
        {
            _courier = new Courier(address, mobileNumber);
        }

        public override void Deliver(List<Product> products)
        {
            Console.WriteLine($"Deliver to home");
            _courier.Deliver(products);
        }
    }

    class PickPointDelivery : Delivery
    {
        public PickPointDelivery(Address address, MobileNumber mobileNumber) : base(address, mobileNumber)
        {
        }

        public override void Deliver(List<Product> products)
        {
            Console.WriteLine($"Pick point delivery, Address:{Address.ToString()}");
        }
    }

    class ShopDelivery : Delivery
    {
        public ShopDelivery(Address address, MobileNumber mobileNumber) : base(address, mobileNumber)
        {
        }

        public override void Deliver(List<Product> products)
        {
            Console.WriteLine($"Shop delivery,  Address:{Address.ToString()}");
        }
    }

    class Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public OrderStatus Status;

        
        private List<Product> Products;

        public string Description;

        public Order(TDelivery delivery, int number, OrderStatus status, List<Product> products, string description)
        {
            Delivery = delivery;
            Number = number;
            Status = status;
            Products = products;
            Description = description;
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.GetDeliveryAddress());
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void Deliver()
        {
            Delivery.Deliver(Products);
        }

    }

    // Already paied by user
    class PaiedOrder<TDelivery, TStruct> : Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public PaiedOrder(TDelivery delivery, int number, List<Product> products, string description)
            : base(delivery, number, OrderStatus.Paied, products, description)
        {

        }
    }

    // Canceled paied by user
    class CanceledOrder<TDelivery, TStruct> : Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public CanceledOrder(TDelivery delivery, int number, List<Product> products, string description)
            : base(delivery, number, OrderStatus.Canceled, products, description)
        {

        }
    }

    class Product
    {
        public string Name { get; }
        public double Price { get; }
        public string Currency { get; }
        public string Description { get; }

        public Product(string name, double price, string currency, string description)
        {
            Name = name;
            Price = price;
            Currency = currency;
            Description = description;
        }

        
        public override string ToString()
        {
            return $"{Name} {Currency} {Price} {Description}";
        }
    }

    enum OrderStatus
    {
        Canceled,
        Paied,
        Delivered,
        Delivering
    }


    
    class Address
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string Appartement { get; }
        public string State { get; }

        public Address(string country, string city, string street, string appartement, string state)
        {
            Country = country;
            City = city;
            Street = street;
            Appartement = appartement;
            State = state;
        }

        public override string ToString()
        {
            return $"{Country} {State} {City} {Street} {Appartement}";
        }
    }

    class MobileNumber
    {
        public string CountryCode { get; }
        public string Number { get; }
        public string Operator { get; }
        public PhoneType PhoneType { get; }
        public MobileNumber(string countryCode, string number, string @operator, PhoneType phoneType)
        {
            CountryCode = countryCode;
            Number = number;
            Operator = @operator;
            PhoneType = phoneType;
        }

        
        public override string ToString()
        {
            return $"+{CountryCode} ({Operator}) {Number} {PhoneType}";
        }
    }

    enum PhoneType
    {
        Personal,
        Office
    }

    class Courier
    {
        private Address _address;
        private MobileNumber _mobileNumber;
        private string _courierId;

        public Courier(Address address, MobileNumber mobileNumber)
        {
            _address = address;
            _mobileNumber = mobileNumber;
            _courierId = Guid.NewGuid().ToString();
        }

        public void Deliver(List<Product> products)
        {
            Console.WriteLine($"Start delivering by ciurier. Id:{_courierId}");
            Console.WriteLine($"Address:{_address.ToString()}");
            Console.WriteLine($"Contact number, {_mobileNumber.ToString()}");
            Console.WriteLine("Product list");
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine($"Finished delivering. Ciurier Id:{_courierId}");
        }
    }

}