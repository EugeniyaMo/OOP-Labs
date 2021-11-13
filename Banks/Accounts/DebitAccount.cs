using System;
using System.Security.Cryptography.X509Certificates;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class DebitAccount : IAccount
    {
        public DebitAccount(Bank bank, Client client, double accountSize)
        {
            Number = Guid.NewGuid();
            Bank = bank;
            Client = client;
            Size = accountSize;
            Percent = bank.BankPercent;
            Status = false;
            AccumulatedSum = 0;
        }

        public Guid Number { get; }
        public Client Client { get; }
        public Bank Bank { get; }
        public double Size { get; set; }
        public double Percent { get; }
        public bool Status { get; }
        private double AccumulatedSum { get; set; }

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
            if (Size < money)
                throw new BanksException("Insufficient money in the account.");
            Size -= money;
            return Size;
        }

        public double AppendMoney(double money)
        {
            Size += money;
            return Size;
        }

        public void TransferMoney(IAccount newAccount, double money)
        {
            CheckAccountStatus(money);
            if (Size < money)
                throw new BanksException("Not enough money in the account.");
            Size -= money;
            newAccount.AppendMoney(money);
        }

        public double UpdateAccountSize(bool check)
        {
            if (check)
            {
                Size += AccumulatedSum;
                AccumulatedSum = 0;
            }
            else
            {
                AccumulatedSum += Size * (Percent / 100 / 365);
            }

            return Size;
        }

        private void CheckAccountStatus(double money)
        {
            if (!Status & money > Bank.BankMaximumAvailableAmount)
                throw new BanksException("The account is doubtful, the operation is impossible.");
        }
    }
}