namespace Banks.Classes
{
    public class Client
    {
        public Client(
            string clientName,
            string clientSurname,
            string clientAddress = "Default address",
            string clientPassport = "Default passport")
        {
            Name = clientName;
            Surname = clientSurname;
            Address = clientAddress;
            Passport = clientPassport;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }
    }
}