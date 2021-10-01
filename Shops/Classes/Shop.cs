using System.Collections.Generic;

namespace Shops.Classes
{
    public class Shop
    {
        private static int _number = 100000;
        public Shop(string shopName, string shopAddress)
        {
            ShopID = _number;
            _number += 1;
            ShopName = shopName;
            ShopAddress = shopAddress;
            Products = new List<Supply>();
            ShopMoney = 0;
        }

        public int ShopID { get; }
        public string ShopName { get; }
        public string ShopAddress { get; }
        public List<Supply> Products { get; }
        public int ShopMoney { get; set; }
    }
}