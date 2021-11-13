using System;
using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Services
{
    public interface IConsoleInterface
    {
        Client Greeting();
        Client Registration();
        void WorkingWithAccount(Client client);
        void CreateNewAccount(Client client);
    }
}