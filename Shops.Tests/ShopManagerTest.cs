using NUnit.Framework;
using Shops.Classes;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddNewProductsToShop()
        {
            Product newProduct = _shopManager.AddProduct("Bread");
            Shop shop = _shopManager.CreateShop("Bakery", "Moskovsky prospect, 23");
            Supply supply = _shopManager.AddProductToShop(newProduct, shop, 70, 20);
            Assert.Contains(supply, shop.Products);
        }

        [Test]
        public void ChangePriceOfProductInShop()
        {
            Product newProduct = _shopManager.AddProduct("Cookies");
            Shop shop = _shopManager.CreateShop("Bakery", "Moskovsky prospect, 23");
            int price = 120;
            Supply newSupply = _shopManager.AddProductToShop(newProduct, shop, price, 45);
            _shopManager.ChangePrice(newProduct.ProductName, shop, 140);
            Assert.AreNotEqual(price, newSupply.Price);
        }

        [Test]
        public void SearchTheMostProfitableShopForProduct()
        {
            Product product = _shopManager.AddProduct("Cinnamon bun");
            Shop firstShop = _shopManager.CreateShop("Bakery", "Moskovsky prospect, 23");
            _shopManager.AddProductToShop(product, firstShop, 35, 12);
            Shop secondShop = _shopManager.CreateShop("Coffee Shop", "Lenin street, 12-1");
            _shopManager.AddProductToShop(product, secondShop, 47, 20);
            Shop answer = _shopManager.SearchTheMostProfitableShop(product, 17);
            Assert.AreEqual(answer, secondShop);
        }

        [Test]
        public void BoughtProductByPerson()
        {
            int moneyBefore = 350;
            Customer person = _shopManager.AddCustomer("Agnessa", moneyBefore);
            Shop shop = _shopManager.CreateShop("Bakery", "Moskovsky prospect, 23");
            Product product = _shopManager.AddProduct("Cookies");
            Supply supply = _shopManager.AddProductToShop(product, shop, 119, 21);
            int count = 2;
            _shopManager.Purchase(person, shop, product.ProductName, count);
            Assert.AreEqual(shop.ShopMoney, supply.Price * count);
            Assert.AreEqual(person.CustomerMoney, moneyBefore - supply.Price * count);
        }

        [Test]
        public void BoughtProductByPerson_NotHaveEnoughMoney_ThrowException()
        {
            Assert.Catch<ShopsException>(() =>
            {
                int moneyBefore = 350;
                Customer person = _shopManager.AddCustomer("Agnessa", moneyBefore);
                Shop shop = _shopManager.CreateShop("Bakery", "Moskovsky prospect, 23");
                Product product = _shopManager.AddProduct("Cookies");
                _shopManager.AddProductToShop(product, shop, 119, 21);
                int count = 3;
                _shopManager.Purchase(person, shop, product.ProductName, count);
            });
        }
    }
}