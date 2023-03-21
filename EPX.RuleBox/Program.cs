using System;
using System.Collections.Generic;
using System.Linq.Expressions;


// Create some sample customers
Customer alice = new Customer() { Name = "Alice", TotalSpent = 120.50, Referrals = 2 };
Customer bob = new Customer() { Name = "Bob", TotalSpent = 80.00, Referrals = 0 };
Customer charlie = new Customer() { Name = "Charlie", TotalSpent = 150.00, Referrals = 3 };

// Create some sample rules

// If a customer spends more than $100, they get 10% off their next purchase
Rule rule1 = new Rule()
{
    Condition = c => c.TotalSpent > 100,
    Outcome = new Reward()
    {
        Type = "Discount",
        Value = 0.1,
        Description = "10% off your next purchase"
    }
};

// If a customer refers more than one friend, they get a free coffee mug
Rule rule2 = new Rule()
{
    Condition = c => c.Referrals > 1,
    Outcome = new Reward()
    {
        Type = "Item",
        Value = 0,
        Description = "A free coffee mug"
    }
};

// Create a list of rules
List<Rule> rules = new List<Rule>() { rule1, rule2 };

// Create a rule engine with the rules
RuleEngine engine = new RuleEngine(rules);

// Evaluate each customer and print their rewards
foreach (Customer customer in new Customer[] { alice, bob, charlie })
{
    Console.WriteLine($"Evaluating {customer.Name}...");
    List<Reward> rewards = engine.Evaluate(customer);
    if (rewards.Count > 0)
    {
        Console.WriteLine($"{customer.Name} gets the following rewards:");
        foreach (Reward reward in rewards)
        {
            Console.WriteLine($"- {reward.Description}");
        }
    }
    else
    {
        Console.WriteLine($"{customer.Name} gets no rewards.");
    }
    Console.WriteLine();
}
    


// A class that represents a customer
public class Customer
{
    public string Name { get; set; }
    public double TotalSpent { get; set; }
    public int Referrals { get; set; }
}

// A class that represents a reward
public class Reward
{
    public string Type { get; set; }
    public double Value { get; set; }
    public string Description { get; set; }
}

// A class that represents a rule
public class Rule
{
    // An expression that represents the condition of the rule
    public Expression<Func<Customer, bool>> Condition { get; set; }
    // An object that represents the outcome of the rule
    public Reward Outcome { get; set; }
}

// A class that represents a rule engine
public class RuleEngine
{
    // A list of rules to evaluate
    private List<Rule> rules;

    // A constructor that takes a list of rules as input
    public RuleEngine(List<Rule> rules)
    {
        this.rules = rules;
    }

    // A method that evaluates a customer against all the rules and returns a list of rewards
    public List<Reward> Evaluate(Customer customer)
    {
        // A list of rewards to return
        List<Reward> rewards = new List<Reward>();

        // Loop through each rule in the list
        foreach (Rule rule in rules)
        {
            // Compile and execute the condition expression with the customer as input
            bool result = rule.Condition.Compile().Invoke(customer);

            // If the condition is true, add the outcome to the rewards list
            if (result)
            {
                rewards.Add(rule.Outcome);
            }
        }

        // Return the rewards list
        return rewards;
    }
}