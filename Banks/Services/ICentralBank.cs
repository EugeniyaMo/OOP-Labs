using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Services
{
    public interface ICentralBank
    {
        List<Bank> GetBanks();
        List<Client> GetClients();
        Bank RegisterNewBank(Bank bank);
        Client AddNewClientToBank(Client client, Bank bank);
        IAccount CreateNewAccount(Client client, Bank bank, int numberAccount, double accountSize);
        void NotifyUpdateAccountSize(Bank bank);

        // void CanselLastTransaction(Bank bank);
    }
}