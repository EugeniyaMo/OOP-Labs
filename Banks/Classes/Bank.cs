using System.Collections.Generic;
using Banks.Services;

namespace Banks.Classes
{
    public class Bank
    {
        public Bank(
            string bankName,
            double bankPercent,
            double bankInitialPercent,
            double bankCommission,
            double bankMaximumAvailableAmount,
            double bankLimit)
        {
            BankName = bankName;
            BankPercent = bankPercent;
            BankInitialPercent = bankInitialPercent;
            BankCommission = bankCommission;
            BankMaximumAvailableAmount = bankMaximumAvailableAmount;
            BankLimit = BankLimit;
            BankAccounts = new List<IAccount>();
            BankClients = new List<Client>();
        }

        public string BankName { get; }
        public double BankPercent { get; set; }
        public double BankInitialPercent { get; }
        public double BankCommission { get; }
        public double BankMaximumAvailableAmount { get; set; }
        public double BankLimit { get; }
        public List<IAccount> BankAccounts { get; }
        public List<Client> BankClients { get; }

        public void ChangeBankPercent(double newPercent)
        {
            BankPercent = newPercent;
        }

        public void ChangeMaximumAvailableAmount(double newAmount)
        {
            BankMaximumAvailableAmount = newAmount;
        }
    }
}