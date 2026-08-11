using System;
using System.Collections.Generic;
using System.Linq;

// 1. DOMAIN MODELS & CUSTOM EXCEPTIONS
public class Expense
{
    public Guid Id { get; }
    public string Description { get; }
    public decimal Amount { get; }
    public DateTime Date { get; }
    public string Category { get; }

    public Expense(Guid id, string description, decimal amount, DateTime date, string category)
    {
        Id = id;
        Description = description;
        Amount = amount;
        Date = date;
        Category = category;
    }
}

public class ExpenseNotFoundException : Exception
{
    public ExpenseNotFoundException(Guid id) 
        : base($"Expense with ID '{id}' was not found.") { }
}

public class InvalidExpenseDataException : Exception
{
    public InvalidExpenseDataException(string message) 
        : base(message) { }
}

// 2. SERVICE INTERFACE & IMPLEMENTATION
public interface IExpenseTrackerService
{
    Expense CreateExpense(string description, decimal amount, string category);
    Expense GetExpenseById(Guid id);
    IEnumerable<Expense> GetAllExpenses();
    bool DeleteExpense(Guid id);
}

public class ExpenseTrackerService : IExpenseTrackerService
{
    private readonly Dictionary<Guid, Expense> _expenses = new Dictionary<Guid, Expense>();

    public Expense CreateExpense(string description, decimal amount, string category)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidExpenseDataException("Description cannot be empty or whitespace.");

        if (amount <= 0)
            throw new InvalidExpenseDataException("Expense amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(category))
            throw new InvalidExpenseDataException("Category cannot be empty.");

        var expense = new Expense(
            Guid.NewGuid(),
            description.Trim(),
            amount,
            DateTime.UtcNow,
            category.Trim()
        );

        _expenses[expense.Id] = expense;
        return expense;
    }

    public Expense GetExpenseById(Guid id)
    {
        Expense expense;
        if (!_expenses.TryGetValue(id, out expense))
            throw new ExpenseNotFoundException(id);

        return expense;
    }

    public IEnumerable<Expense> GetAllExpenses() => _expenses.Values.ToList();

    public bool DeleteExpense(Guid id)
    {
        if (!_expenses.ContainsKey(id))
            throw new ExpenseNotFoundException(id);

        return _expenses.Remove(id);
    }
}

// 3. MAIN ENTRY POINT
public class Program
{
    public static void Main()
    {
        IExpenseTrackerService tracker = new ExpenseTrackerService();

        Console.WriteLine("--- 1. SUCCESSFUL OPERATIONS ---");
        try
        {
            var e1 = tracker.CreateExpense("Coffee & Lunch", 18.50m, "Food");
            var e2 = tracker.CreateExpense("Internet Bill", 65.00m, "Utilities");

            Console.WriteLine($"[Success] Created: {e1.Description} - ${e1.Amount}");
            Console.WriteLine($"[Success] Created: {e2.Description} - ${e2.Amount}");
            Console.WriteLine($"Total Expenses Stored: {tracker.GetAllExpenses().Count()}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }

        Console.WriteLine("--- 2. EXCEPTION HANDLING: INVALID AMOUNT ---");
        try
        {
            tracker.CreateExpense("Software License", -50.00m, "Software");
        }
        catch (InvalidExpenseDataException ex)
        {
            Console.WriteLine($"[Caught Domain Exception]: {ex.Message}\n");
        }

        Console.WriteLine("--- 3. EXCEPTION HANDLING: MISSING DESCRIPTION ---");
        try
        {
            tracker.CreateExpense("   ", 25.00m, "Office Supplies");
        }
        catch (InvalidExpenseDataException ex)
        {
            Console.WriteLine($"[Caught Domain Exception]: {ex.Message}\n");
        }

        Console.WriteLine("--- 4. EXCEPTION HANDLING: NON-EXISTENT ID ---");
        try
        {
            Guid fakeId = Guid.NewGuid();
            Console.WriteLine($"Fetching non-existent ID: {fakeId}");
            tracker.GetExpenseById(fakeId);
        }
        catch (ExpenseNotFoundException ex)
        {
            Console.WriteLine($"[Caught Domain Exception]: {ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Caught Generic Fallback]: {ex.Message}\n");
        }
    }
}
