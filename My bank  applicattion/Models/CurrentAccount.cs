using BankingApp2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp2.Models;

public class CurrentAccount : BankAccount
{
    private const decimal InterestRate = 0.01m;
    private const decimal OverdraftLimit = 1000000m;

    public CurrentAccount(Customer owner) : base(owner)
    {
    }

    public override decimal CalculateInterest()
    {
        // No interest is earned on a negative balance.
        return Balance > 0 ? Balance * InterestRate : 0;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException(
                "Withdrawal amount must be greater than zero.");

        if (Balance - amount < -OverdraftLimit)
            throw new InvalidOperationException(
                "Withdrawal exceeds the overdraft daily limit (daily limit is $1million).");

        Balance -= amount;
        Console.WriteLine($"Withdrawn: {amount:C}");
    }
}