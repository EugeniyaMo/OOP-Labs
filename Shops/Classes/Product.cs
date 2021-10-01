namespace Shops.Classes
{
    public class Product
    {
        private static int _number = 100000;
        public Product(string productName)
        {
            ProductID = _number;
            _number += 1;
            ProductName = productName;
        }

        public int ProductID { get; }
        public string ProductName { get; }
    }
}