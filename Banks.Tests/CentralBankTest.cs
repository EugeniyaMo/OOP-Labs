using Banks.Classes;
using Banks.Services;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class CentralBankTest
    {
        public class Tests
        {
            private ICentralBank _centralBank;
            
            [SetUp]
            public void Setup()
            {
                _centralBank = new CentralBank();
            }

            [Test]
            public void CreateNewBank()
            {
                Bank newBank = new Bank("Sberbank", 13.1, 3, 15000,
                    5000, 100000);
                _centralBank.RegisterNewBank(newBank);
                Assert.Contains(newBank, _centralBank.GetBanks());
            }
            
            [Test]
            public void CreateNewClient_AddClientToBank()
            {
                Bank bank = new Bank("Sberbank", 13.1, 3, 15000,
                    5000, 100000);
                _centralBank.RegisterNewBank(bank);
                Client newClient = new Client("Sergey", "Potapov");
                _centralBank.AddNewClientToBank(newClient, bank);
                Assert.Contains(newClient, bank.BankClients);
            }

            [Test]
            public void CreateNewDebitAccount_DoTransactions()
            {
                Bank bank = new Bank("Sberbank", 13.1, 3, 15000,
                    5000, 100000);
                _centralBank.RegisterNewBank(bank);
                Client newClient = new Client("Sergey", "Potapov");
                _centralBank.AddNewClientToBank(newClient, bank);
                IAccount newAccount = _centralBank.CreateNewAccount(newClient, bank, 1, 40000);
                newAccount.AppendMoney(1000);
                newAccount.WithdrawMoney(200);
                Assert.AreEqual(newAccount.GetAccountSize(), 40000 + 1000 - 200);
            }
            
            [Test]
            public void CreateNewAccount_NotEnoughMoney_ThrowException()
            {
                Assert.Catch<BanksException>(() =>
                {
                    Bank bank = new Bank("Sberbank", 13.1, 3, 15000,
                        5000, 100000);
                    _centralBank.RegisterNewBank(bank);
                    Client newClient = new Client("Sergey", "Potapov");
                    _centralBank.AddNewClientToBank(newClient, bank);
                    IAccount newAccount = _centralBank.CreateNewAccount(newClient, bank, 1, 1000);
                    newAccount.WithdrawMoney(2000);
                });
            }


            [Test]
            public void CreateNewDoubtfulAccount_ThrowException()
            {
                Assert.Catch<BanksException>(() =>
                {
                    Bank bank = new Bank("Sberbank", 13.1, 3, 15000,
                        5000, 100000);
                    _centralBank.RegisterNewBank(bank);
                    Client newClient = new Client("Sergey", "Potapov");
                    _centralBank.AddNewClientToBank(newClient, bank);
                    IAccount newAccount = _centralBank.CreateNewAccount(newClient, bank, 1, 40000);
                    newAccount.WithdrawMoney(6000);
                });
            }
        }
        
    }
}