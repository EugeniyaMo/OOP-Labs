namespace Shops.Classes
{
    public class Customer
    {
        public Customer(string customerName, int customerMoney)
        {
            CustomerName = customerName;
            CustomerMoney = customerMoney;
        }

        public string CustomerName { get; }
        public int CustomerMoney { get; set; }
    }
}