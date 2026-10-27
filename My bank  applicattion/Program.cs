using BankingApp2.Models;
using BankingApp2.Services;

Bank bank = new();

while (true)
{
    Console.WriteLine("\n===== SUNMICREEDHUB BANKING APPLICATION =====");
    Console.WriteLine("1. Register Customer");
    Console.WriteLine("2. Create Account");
    Console.WriteLine("3. View Customers");
    Console.WriteLine("4. View Accounts");
    Console.WriteLine("5. Check Balance");
    Console.WriteLine("6. Deposit");
    Console.WriteLine("7. Withdraw");
    Console.WriteLine("8. Calculate Interest");
    Console.WriteLine("9. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Enter full name: ");
                string? name = Console.ReadLine();

                Customer customer = bank.RegisterCustomer(name ?? "");

                Console.WriteLine("Customer registered successfully.");
                Console.WriteLine(customer);
                break;

            case "2":
                Console.Write("Enter Customer ID: ");
                int customerId = ReadInt();

                Console.Write("Enter account type (Savings/Current): ");
                string accountType = Console.ReadLine() ?? "";

                BankAccount account = bank.CreateAccount(
                    customerId, accountType);

                Console.WriteLine("Account created successfully.");
                account.DisplayDetails();
                break;

            case "3":
                bank.DisplayCustomers();
                break;

            case "4":
                bank.DisplayAccounts();
                break;

            case "5":
                BankAccount balanceAccount = GetAccount(bank);
                Console.WriteLine(
                    $"Current Balance: {balanceAccount.Balance:C}");
                break;

            case "6":
                BankAccount depositAccount = GetAccount(bank);

                Console.Write("Enter deposit amount: ");
                decimal depositAmount = ReadDecimal();

                Console.Write("Enter description: ");
                string description = Console.ReadLine() ?? "";

                depositAccount.Deposit(depositAmount, description);
                Console.WriteLine(
                    $"New Balance: {depositAccount.Balance:C}");
                break;

            case "7":
                BankAccount withdrawalAccount = GetAccount(bank);

                Console.Write("Enter withdrawal amount: ");
                decimal withdrawalAmount = ReadDecimal();

                withdrawalAccount.Withdraw(withdrawalAmount);
                Console.WriteLine(
                    $"New Balance: {withdrawalAccount.Balance:C}");
                break;

            case "8":
                BankAccount interestAccount = GetAccount(bank);

                decimal interest = interestAccount.CalculateInterest();

                Console.WriteLine($"Calculated Interest: {interest:C}");
                Console.WriteLine(
                    "Note: Interest is calculated, not added to the balance.");
                break;

            case "9":
                Console.WriteLine("Thank you for using Simple Bank System.");
                return;

            default:
                Console.WriteLine(
                    "Invalid menu option. Please choose from 1 to 9.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Input error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Operation error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

static int ReadInt()
{
    if (!int.TryParse(Console.ReadLine(), out int value))
        throw new FormatException("Please enter a valid whole number.");

    return value;
}

static decimal ReadDecimal()
{
    if (!decimal.TryParse(Console.ReadLine(), out decimal value))
        throw new FormatException("Please enter a valid monetary amount.");

    return value;
}

static BankAccount GetAccount(Bank bank)
{
    Console.Write("Enter Account Number: ");
    int accountNumber = ReadInt();

    return bank.FindAccount(accountNumber)
        ?? throw new InvalidOperationException("Account not found.");
}