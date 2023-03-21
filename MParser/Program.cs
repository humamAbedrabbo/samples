
ExpressionParser ep = new();

Console.WriteLine($"3*(6-1) = {ep.Eval("3*(6-1)", new Dictionary<string, double>())}");



public class ExpressionParser
{
    public double Eval(string expression, Dictionary<string, double> vars)
    {
        int bracketCounter = 0;
        int operatorIndex = -1;

        for(int i =0; i<expression.Length; i++)
        {
            char c = expression[i];
            if (c == '(') bracketCounter++;
            else if(c == ')') bracketCounter--;
            else if((c == '+' || c == '-') && bracketCounter == 0)
            {
                operatorIndex = i;
                break;
            }
            else if((c == '*' || c == '/') && bracketCounter == 0 && operatorIndex < 0)
            {
                operatorIndex = i;
            }
        }

        if(operatorIndex < 0)
        {
            expression = expression.Trim();
            if (expression[0] == '(' && expression[expression.Length - 1] == ')') {
                return Eval(expression.Substring(1, expression.Length - 2), vars);
            }
            else if(vars.ContainsKey(expression))
            {
                return vars[expression];
            }
            else
                return double.Parse(expression);
        }
        else
        {
            switch(expression[operatorIndex])
            {
                case '+':
                    return Eval(expression.Substring(0, operatorIndex), vars) + Eval(expression.Substring(operatorIndex + 1), vars);
                case '-':
                    return Eval(expression.Substring(0, operatorIndex), vars) - Eval(expression.Substring(operatorIndex + 1), vars);
                case '*':
                    return Eval(expression.Substring(0, operatorIndex), vars) * Eval(expression.Substring(operatorIndex + 1), vars);
                case '/':
                    return Eval(expression.Substring(0, operatorIndex), vars) / Eval(expression.Substring(operatorIndex + 1), vars);
            }
        }
        return 0;
    }
}