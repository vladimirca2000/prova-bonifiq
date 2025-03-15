using System;

namespace ProvaPub.Models;

public class Order : BaseEntity
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; private set; }
    public Customer Customer { get; set; }
    public void SetOrderDate(DateTime date)
    {
        OrderDate = date;
    }
}

