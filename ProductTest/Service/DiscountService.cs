using ProductTest.Models;
namespace ProductTest.Service
{
    public class DiscountService()
    {
        public decimal GetDiscountedPrice(Product p)
        {
            var lastDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-1);
            //скидка рассчитывается по принципу проверки не было ли заказов за прошлый календарный месяц - стоимость умножается на 0.75, тк скидка 25%
            return p.ProductOrders.Any(po =>
            po.IdOrderNavigation.DataOrder.Month == lastDate.Month && po.IdOrderNavigation.DataOrder.Year == lastDate.Year) 
                ? p.Price 
                : p.Price * 0.75m;
        }
    }
}