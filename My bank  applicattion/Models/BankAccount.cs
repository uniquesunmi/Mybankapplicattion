using BankingApp2.Models;
using BankingApp2.Services;
using System;
using System.Collections.Generic;
using System.Text;


namespace BankingApp2.Models;

public abstract class BankAccount
{
    private static int _nextAccountNumber = 100001;

    public int AccountNumber { get; }
    public Customer Owner { get; }
    public decimal Balance { get; protected set; }

    protected BankAccount(Customer owner)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        AccountNumber = _nextAccountNumber++;
        Balance = 0;
    }

    // Method overloading: deposit with a description
    public void Deposit(decimal amount)
    {
        Deposit(amount, "General deposit");
    }

    // Method overloading: deposit with an amount and description
    public void Deposit(decimal amount, string description)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        Balance += amount;

        Console.WriteLine(
            $"Deposited: {amount:C} | Description: {description}");
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be greater than zero.");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance.");

        Balance -= amount;
        Console.WriteLine($"Withdrawn: {amount:C}");
    }

    // Overridden by each account type
    public abstract decimal CalculateInterest();

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Customer Name: {Owner.FullName}");
        Console.WriteLine($"Customer ID: {Owner.CustomerId}");
        Console.WriteLine($"Account Type: {GetType().Name}");
        Console.WriteLine($"Balance: {Balance:C}");
    }
}