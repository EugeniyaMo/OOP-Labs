using Shops.Classes;

namespace Shops.Services
{
    public interface IShopManager
    {
        Product AddProduct(string name);
        Shop CreateShop(string name, string address);
        Supply AddProductToShop(Product product, Shop shop, int price, int count);

        void ChangePrice(string product, Shop shop, int newPrice);
        Shop SearchTheMostProfitableShop(Product product, int count);

        Customer AddCustomer(string name, int money);
        void Purchase(Customer customer, Shop shop, string productName, int count);
    }
}