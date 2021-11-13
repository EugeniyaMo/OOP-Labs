using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Banks.Classes;

namespace Banks.Services
{
    public class ConsoleInterface : IConsoleInterface
    {
        private ICentralBank _centralBank;
        private Bank _bank;
        private Dictionary<string, string> _dataBasePasswords = new Dictionary<string, string>();
        private Dictionary<string, Client> _dataBaseClients = new Dictionary<string, Client>();

        public ConsoleInterface(ICentralBank centralBank, Bank bank)
        {
            _centralBank = centralBank;
            _bank = bank;
        }

        public Client Greeting()
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Are you already registered in the system? Y/N");
            string answer = Console.ReadLine();
            if (answer == "Y")
            {
                Console.WriteLine("Enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                if (_dataBasePasswords[username] == password)
                    Console.WriteLine("OK");
                else
                    Console.WriteLine("Invalid password");
                return _dataBaseClients[username];
            }
            else
            {
                return Registration();
            }
        }

        public Client Registration()
        {
            Console.WriteLine("Okey, then you can register right now.");
            Console.WriteLine("Enter your new username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your new password:");
            string password = Console.ReadLine();
            _dataBasePasswords[username] = password;
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your surname:");
            string surname = Console.ReadLine();
            Console.WriteLine("Do you want to enter an address? Y/N");
            string answer = Console.ReadLine();
            string address = (answer == "Y") ? Console.ReadLine() : null;
            Console.WriteLine("Do you want to enter an passport? Y/N");
            answer = Console.ReadLine();
            string passport = (answer == "Y") ? Console.ReadLine() : null;
            var newClient = new Client(name, surname, address, passport);
            _centralBank.AddNewClientToBank(newClient, _bank);
            return newClient;
        }

        public void CreateNewAccount(Client client)
        {
            Console.WriteLine("What kind of account do you want to create?");
            Console.WriteLine("1 - Debit account\n2 - Deposit account\n3 - Credit account");
            Console.WriteLine("Write number:");
            int numberAccount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Write the amount of money you want to put into the account:");
            int money = Convert.ToInt32(Console.ReadLine());
            _centralBank.CreateNewAccount(client, _bank, numberAccount, money);
        }

        public void WorkingWithAccount(Client client)
        {
            Console.WriteLine("Do you already have an active account? Y/N");
            string answer = Console.ReadLine();
            while (answer != "Y")
            {
                CreateNewAccount(client);
                Console.WriteLine("Do you already have an active account? Y/N");
                answer = Console.ReadLine();
            }

            List<IAccount> accounts = new List<IAccount>();
            foreach (var currentAccount in _bank.BankAccounts)
            {
                if (currentAccount.GetAccountClient() == client)
                {
                    Console.WriteLine(currentAccount.GetAccountNumber());
                    accounts.Add(currentAccount);
                }
            }

            Console.WriteLine("Choose necessary account and enter number.");
            int numAnswer = Convert.ToInt32(Console.ReadLine()) - 1;
            Guid accountNumber = accounts[numAnswer].GetAccountNumber();
            int numOperation = 0;
            while (numOperation != 5)
            {
                Console.WriteLine("What kind of operation do you want to do?");
                Console.WriteLine("1 - Withdraw money\n2 - Append money\n3 - Transfer money\n4 - Show the balance");
                Console.WriteLine("5 - Change account");
                Console.WriteLine("Write number:");
                numOperation = Convert.ToInt32(Console.ReadLine());
                double money;
                switch (numOperation)
                {
                    case 1:
                        Console.WriteLine("Enter amount of money:");
                        money = Convert.ToDouble(Console.ReadLine());
                        accounts[numAnswer].WithdrawMoney(money);
                        break;
                    case 2:
                        Console.WriteLine("Enter amount of money:");
                        money = Convert.ToDouble(Console.ReadLine());
                        accounts[numAnswer].AppendMoney(money);
                        break;
                    case 3:
                        Console.WriteLine("Enter amount of money:");
                        money = Convert.ToDouble(Console.ReadLine());
                        foreach (var currentAccount in _bank.BankAccounts)
                            Console.WriteLine(currentAccount.GetAccountNumber());
                        Console.WriteLine("Choose necessary account and enter number.");
                        accounts[numAnswer].TransferMoney(accounts[Convert.ToInt32(Console.ReadLine())], money);
                        break;
                    case 4:
                        Console.WriteLine(accounts[numAnswer].GetAccountSize());
                        break;
                }
            }
        }
    }
}