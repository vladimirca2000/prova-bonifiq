﻿using ProvaPub.Interfaces.Payment;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdemRepository _ordemRepository;

        public OrderService(IOrdemRepository ordemRepository)
        {
            _ordemRepository = ordemRepository;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            IPaymentStrategy paymentStrategy = PaymentFactory.GetPaymentStrategy(paymentMethod);

            bool paymentSuccessful = await paymentStrategy.ProcessPayment(paymentValue, customerId);
            if (!paymentSuccessful)
                throw new Exception("Erro ao processar pagamento");

            var order = new Order
            {
                Value = paymentValue,
                CustomerId = customerId
            };
            order.SetOrderDate(DateTime.UtcNow); // Salvar sempre em UTC

            return await _ordemRepository.InsertAsync(order);
        }
    }
}






//      TestDbContext _ctx;

//      public OrderService(TestDbContext ctx)
//      {
//          _ctx = ctx;
//      }

//      public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
//{
//	if (paymentMethod == "pix")
//	{
//		//Faz pagamento...
//	}
//	else if (paymentMethod == "creditcard")
//	{
//		//Faz pagamento...
//	}
//	else if (paymentMethod == "paypal")
//	{
//		//Faz pagamento...
//	}

//	return await InsertOrder(new Order() //Retorna o pedido para o controller
//          {
//              Value = paymentValue
//          });


//}

//public async Task<Order> InsertOrder(Order order)
//      {
//	//Insere pedido no banco de dados
//	return (await _ctx.Orders.AddAsync(order)).Entity;
//      }

//    }
//}
