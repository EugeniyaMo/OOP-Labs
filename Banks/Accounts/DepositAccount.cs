using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class DepositAccount : IAccount
    {
        public DepositAccount(Bank bank, Client client, double accountSize)
        {
            Number = Guid.NewGuid();
            Bank = bank;
            Client = client;
            Size = accountSize;
            Percent = GetPercent(Size, bank.BankInitialPercent);
            Status = false;
            AccumulatedSum = 0;
        }

        public Guid Number { get; }
        public Bank Bank { get; }
        public Client Client { get; }
        public double Size { get; set; }
        public double Percent { get; }
        public DateTime Term { get; }
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
            if (DateTime.Now < Term)
                throw new BanksException("The account term is not over yet.");
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
            if (DateTime.Now < Term)
                throw new BanksException("The account term is not over yet.");
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

        private double GetPercent(double debitAccountSize, double initialPercent)
        {
            int fixAmount = 50000;
            return ((int)debitAccountSize / fixAmount * 0.5) + initialPercent;
        }

        private void CheckAccountStatus(double money)
        {
            if (!Status & money > Bank.BankMaximumAvailableAmount)
                throw new BanksException("The account is doubtful, the operation is impossible.");
        }
    }
}