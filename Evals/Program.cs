using Expressive;
using Expressive.Expressions;
using Expressive.Functions;

namespace Evals;

internal class Program
{
    // https://github.com/bijington/expressive
    // docs: https://github.com/bijington/expressive/wiki/Usage
    static void Main(string[] args)
    {
        var expression = new Expression("sum(1,2,3,4)", ExpressiveOptions.All);
        var result = expression.Evaluate();
        Console.WriteLine($"sum(1,2,3,4) = {result}");

        var expression1 = new Expression("Abs(1)", ExpressiveOptions.All);
        expression1.RegisterFunction(new AbsFunction());
        var result1 = expression.Evaluate();
        Console.WriteLine($"Koko(1) = {result1}");

    }
}

internal class AbsFunction : IFunction
{
    #region IFunction Members

    public IDictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();

    public string Name { get { return "Koko"; } }

    public object Evaluate(IExpression[] parameters, Context context)
    {
        return Math.Asin(Convert.ToDouble(parameters[0].Evaluate(Variables)));
    }

    #endregion
}
