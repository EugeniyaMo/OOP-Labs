using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class CreditAccount : IAccount
    {
        public CreditAccount(Bank bank, Client client, double accountSize)
        {
            Number = Guid.NewGuid();
            Bank = bank;
            Client = client;
            Size = accountSize;
            Commission = bank.BankCommission;
            Limit = bank.BankLimit;
            Status = false;
        }

        public Guid Number { get; }
        public Bank Bank { get; }
        public Client Client { get; }
        public double Size { get; set; }
        public double Commission { get; }
        public double Limit { get; }
        public bool Status { get; }

        public Guid GetAccountNumber()
        {
            return Number;
        }

        public Client GetAccountClient()
        {
            return Client;
        }

        public double GetAccountSize()
        {
            return Size;
        }

        public double WithdrawMoney(double money)
        {
            CheckAccountStatus(money);
            if (!(Size - money > (-1) * Limit))
                throw new Exception("The credit limit reached.");
            Size -= money;
            return Size;
        }

        public double AppendMoney(double money)
        {
            CheckAccountStatus(money);
            Size += money;
            return Size;
        }

        public void TransferMoney(IAccount newAccount, double money)
        {
            CheckAccountStatus(money);
            Size -= money;
            newAccount.AppendMoney(money);
        }

        public double UpdateAccountSize(bool check)
        {
            if (Size < 0)
                Size -= Commission;
            return Size;
        }

        private void CheckAccountStatus(double money)
        {
            if (!Status & money > Bank.BankMaximumAvailableAmount)
                throw new BanksException("The account is doubtful, the operation is impossible.");
        }
    }
}