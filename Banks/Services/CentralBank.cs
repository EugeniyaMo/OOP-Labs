using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks = new List<Bank>();
        private List<Client> _clients = new List<Client>();

        public List<Bank> GetBanks()
        {
            return _banks;
        }

        public List<Client> GetClients()
        {
            return _clients;
        }

        public Bank RegisterNewBank(Bank bank)
        {
            _banks.Add(bank);
            return bank;
        }

        public Client AddNewClientToBank(Client client, Bank bank)
        {
            _clients.Add(client);
            bank.BankClients.Add(client);
            return client;
        }

        public IAccount CreateNewAccount(Client client, Bank bank, int numberAccount, double accountSize)
        {
            IAccount newAccount = null;
            switch (numberAccount)
            {
                // Debit Account
                case 1:
                    newAccount = new DebitAccount(bank, client, accountSize);
                    bank.BankAccounts.Add(newAccount);
                    break;

                // Deposit Account
                case 2:
                    newAccount = new DepositAccount(bank, client, accountSize);
                    bank.BankAccounts.Add(newAccount);
                    break;

                // Credit Account
                case 3:
                    newAccount = new CreditAccount(bank, client, accountSize);
                    bank.BankAccounts.Add(newAccount);
                    break;
            }

            return newAccount;
        }

        public void NotifyUpdateAccountSize(Bank bank)
        {
            foreach (var currentAccount in bank.BankAccounts)
            {
                currentAccount.UpdateAccountSize(true);
            }
        }
    }
}