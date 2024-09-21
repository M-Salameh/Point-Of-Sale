using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using ApplicationLayer.AutoMapper; 
using System.Collections.Generic;
using ApplicationLayer.ServicesProvider;
using PersistenceLayer.UnitOfWork.ConcreteClasses;
using System.Linq;

namespace TestingLayer
{
	internal class Program
	{
		public static UnitOfWork UnitOfWork { get; set; }
		public static void Main(string[] args)
		{
			UnitOfWork = new UnitOfWork();
			Order order1 = new Order
            {
				CustomerId = 1,
				CreatedAt = new DateTime(2023, 6, 1),
            };
			Order order2 = new Order
			{
				CustomerId = 1,
				CreatedAt = new DateTime(2023, 6, 1),
			};

			Order order3 = new Order
			{
				CustomerId = 1,
				CreatedAt = new DateTime(2023, 6, 6),
			};
			Order order4 = new Order
			{
				CustomerId = 1,
				CreatedAt = new DateTime(2023, 6, 6),
			};

			UnitOfWork.OrderRepository.Add(order1);
			UnitOfWork.OrderRepository.Add(order2);
			UnitOfWork.OrderRepository.Add(order3);
			UnitOfWork.OrderRepository.Add(order4);

			UnitOfWork.SaveChanges();

			Console.WriteLine("Done");
		}
	}

}