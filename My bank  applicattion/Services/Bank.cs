using System;
using System.Collections.Generic;
using System.Text;

using BankingApp2.Models;
namespace BankingApp2.Services;

public class Bank
{
    private readonly List<Customer> _customers = new();
    private readonly List<BankAccount> _accounts = new();

    public Customer RegisterCustomer(string fullName)
    {
        Customer customer = new(fullName);
        _customers.Add(customer);

        return customer;
    }

    public BankAccount CreateAccount(int customerId, string accountType)
    {
        Customer? customer = _customers.Find(
            c => c.CustomerId == customerId);

        if (customer == null)
            throw new InvalidOperationException("Customer not found.");

        BankAccount account;

        switch (accountType.Trim().ToLower())
        {
            case "savings":
                account = new SavingsAccount(customer);
                break;

            case "current":
                account = new CurrentAccount(customer);
                break;

            default:
                throw new ArgumentException(
                    "Invalid account type. Choose Savings or Current.");
        }

        _accounts.Add(account);
        return account;
    }

    public Customer? FindCustomer(int customerId)
    {
        return _customers.Find(c => c.CustomerId == customerId);
    }

    public BankAccount? FindAccount(int accountNumber)
    {
        return _accounts.Find(a => a.AccountNumber == accountNumber);
    }

    public void DisplayCustomers()
    {
        if (_customers.Count == 0)
        {
            Console.WriteLine("No customers registered.");
            return;
        }

        foreach (Customer customer in _customers)
        {
            Console.WriteLine(customer);
        }
    }

    public void DisplayAccounts()
    {
        if (_accounts.Count == 0)
        {
            Console.WriteLine("No accounts created.");
            return;
        }

        foreach (BankAccount account in _accounts)
        {
            account.DisplayDetails();
            Console.WriteLine("--------------------------");
        }
    }
}