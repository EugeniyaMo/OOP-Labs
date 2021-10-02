namespace Shops.Classes
{
    public class Supply
    {
        public Supply(Product product, Shop shop, int price, int count)
        {
            SupplyProduct = product;
            SupplyShop = shop;
            Price = price;
            Count = count;
        }

        public Product SupplyProduct { get; }
        public Shop SupplyShop { get; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}