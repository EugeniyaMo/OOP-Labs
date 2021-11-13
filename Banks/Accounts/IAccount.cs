using System;
using Banks.Classes;

namespace Banks.Services
{
    public interface IAccount
    {
        Guid GetAccountNumber();
        Client GetAccountClient();
        double GetAccountSize();
        double WithdrawMoney(double money);
        double AppendMoney(double money);
        void TransferMoney(IAccount newAccount, double money);
        double UpdateAccountSize(bool check);
    }
}