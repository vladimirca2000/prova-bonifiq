﻿using ProvaPub.Models;
using System.Threading.Tasks;

namespace ProvaPub.Interfaces;

public interface IOrderService 
{
    Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
    //Task<Order> InsertOrder(Order order);
}