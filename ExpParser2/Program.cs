using Expressive;
using Expressive.Expressions;
using Expressive.Functions;

// https://github.com/bijington/expressive


var exp = new Expression("1+2");
var result = exp.Evaluate();
Console.WriteLine($"1+2 = {result}");

exp = new Expression("1 * [variable]");
result = exp.Evaluate(new Dictionary<string, object> { ["variable"] = 2 });
Console.WriteLine($"1 * [variable](2) = {result}");

exp = new Expression("Sum(1,2,3,4)");
result = exp.Evaluate();
Console.WriteLine($"sum(1,2,3,4) = {result}");



//exp  = new Expression("Abs(1)");
//exp.RegisterFunction(new AbsFunction());
//result = exp.Evaluate();
//Console.WriteLine($"IFunction Abs(1) = {result}");


exp = new Expression("myfunc(1)");
exp.RegisterFunction("myfunc", (p, v) =>
{
    // p = parameters
    // v = variables
    return p[0].Evaluate(v);
});
result = exp.Evaluate();
Console.WriteLine($"Lambda myfunc(1) = {result}");



internal class AbsFunction : IFunction
{
    #region IFunction Members

    public IDictionary<string, object> Variables { get; set; }

    public string Name { get { return "Abs"; } }

    public object Evaluate(IExpression[] parameters, Context context)
    {
        return Math.Asin(Convert.ToDouble(parameters[0].Evaluate(Variables)));
    }


    #endregion
}