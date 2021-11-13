using System;
using Banks.Classes;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            ICentralBank centralBank = new CentralBank();

            // Create main bank
            Bank bank = new Bank(
                "Tinkoff",
                9.7,
                4,
                7000,
                1500,
                200000);
            centralBank.RegisterNewBank(bank);
            DateTime startTime = DateTime.Now;
            IConsoleInterface consoleInterface = new ConsoleInterface(centralBank, bank);
            Client client = consoleInterface.Greeting();
            while (true)
            {
                consoleInterface.WorkingWithAccount(client);
            }
        }
    }
}
