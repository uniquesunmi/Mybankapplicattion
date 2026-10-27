using System;
using System.Collections.Generic;
using System.Text;


namespace BankingApp2.Models;

public class Customer
{
    private static int _nextCustomerId = 1001;

    public int CustomerId { get; }
    public string FullName { get; }

    public Customer(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        CustomerId = _nextCustomerId++;
        FullName = fullName;
    }

    public override string ToString()
    {
        return $"Customer ID: {CustomerId} | Name: {FullName}";
    }
}