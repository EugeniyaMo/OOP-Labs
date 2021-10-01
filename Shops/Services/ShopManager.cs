using System.Collections.Generic;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Product> _registeredProducts = new List<Product>();
        private List<Shop> _shops = new List<Shop>();
        private List<Customer> _customers = new List<Customer>();

        public Product AddProduct(string name)
        {
            var newProduct = new Product(name);
            _registeredProducts.Add(newProduct);
            return newProduct;
        }

        public Shop CreateShop(string name, string address)
        {
            var newShop = new Shop(name, address);
            _shops.Add(newShop);
            return newShop;
        }

        public Supply AddProductToShop(Product product, Shop shop, int price, int count)
        {
            var newSupply = new Supply(product, shop, price, count);
            foreach (Supply currentSupply in shop.Products)
            {
                if (currentSupply.SupplyProduct == product)
                {
                    currentSupply.Price = price;
                    currentSupply.Count += count;
                    return newSupply;
                }
            }

            shop.Products.Add(newSupply);
            return newSupply;
        }

        public void ChangePrice(string product, Shop shop, int newPrice)
        {
            foreach (Supply currentSupply in shop.Products)
            {
                if (currentSupply.SupplyProduct.ProductName == product)
                {
                    currentSupply.Price = newPrice;
                }
            }
        }

        public Shop SearchTheMostProfitableShop(Product product, int count)
        {
            Shop theMostProfitableShop = _shops[0];
            int minPrice = 0;
            bool productFound = false;
            foreach (Shop shop in _shops)
            {
                foreach (Supply currentSupply in shop.Products)
                {
                    if (currentSupply.SupplyProduct == product)
                    {
                        productFound = true;
                        if (currentSupply.Count >= count)
                        {
                            minPrice = currentSupply.Price;
                            theMostProfitableShop = shop;
                            break;
                        }
                    }
                }
            }

            if (!productFound)
                throw new ShopsException("This product is not available in shops.");
            if (minPrice == 0)
                throw new ShopsException("There is no such quantity of products in any shop.");

            foreach (Shop shop in _shops)
            {
                foreach (Supply currentSupply in shop.Products)
                {
                    if (currentSupply.SupplyProduct == product)
                    {
                        if (currentSupply.Price < minPrice && currentSupply.Count >= count)
                        {
                            minPrice = currentSupply.Price;
                            theMostProfitableShop = shop;
                            break;
                        }
                    }
                }
            }

            return theMostProfitableShop;
        }

        public Customer AddCustomer(string name, int money)
        {
            var newCustomer = new Customer(name, money);
            _customers.Add(newCustomer);
            return newCustomer;
        }

        public void Purchase(Customer customer, Shop shop, string productName, int count)
        {
            foreach (Supply currentSupply in shop.Products)
            {
                if (currentSupply.SupplyProduct.ProductName == productName)
                {
                    if (currentSupply.Count < count)
                        throw new ShopsException("There is no such quantity of products in this shop.");
                    int cost = currentSupply.Price * count;
                    if (cost > customer.CustomerMoney)
                        throw new ShopsException("The customer doesn't have enough money.");
                    customer.CustomerMoney -= cost;
                    shop.ShopMoney += cost;
                    currentSupply.Count -= count;
                    break;
                }
            }
        }
    }
}