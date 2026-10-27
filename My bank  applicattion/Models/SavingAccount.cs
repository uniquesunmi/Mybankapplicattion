using BankingApp2.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace BankingApp2.Models;

public class SavingsAccount : BankAccount
{
    private const decimal InterestRate = 0.05m;

    public SavingsAccount(Customer owner) : base(owner)
    {
    }

    public override decimal CalculateInterest()
    {
        return Balance * InterestRate;
    }
}