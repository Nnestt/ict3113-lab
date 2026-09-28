using System.Globalization;
using SOFTEST_INTRO_Calculator;

var calculator = new Calculator();

Console.WriteLine("Calculator operations:");
Console.WriteLine("a=add, s=subtract, m=multiply, d=divide, f=factorial");

Console.Write("Operation: ");
string op = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

if (op == "f")
{
    Console.Write("Number: ");
    string input = Console.ReadLine() ?? "";

    bool numberOk = int.TryParse(
        input,
        NumberStyles.Integer,
        CultureInfo.InvariantCulture,
        out int n);

    if (!numberOk)
    {
        Console.WriteLine("Enter an integer from 0 to 20.");
        return;
    }

    try
    {
        long result = calculator.Factorial(n);
        Console.WriteLine("Result: " + result.ToString(CultureInfo.InvariantCulture));
    }
    catch (ArgumentException error)
    {
        Console.WriteLine(error.Message);
    }

    return;
}

Console.Write("First number: ");
string first = Console.ReadLine() ?? "";

Console.Write("Second number: ");
string second = Console.ReadLine() ?? "";

bool firstOk = double.TryParse(
    first,
    NumberStyles.Float,
    CultureInfo.InvariantCulture,
    out double a);

bool secondOk = double.TryParse(
    second,
    NumberStyles.Float,
    CultureInfo.InvariantCulture,
    out double b);

if (!firstOk || !secondOk ||
    !double.IsFinite(a) || !double.IsFinite(b))
{
    Console.WriteLine("Enter finite numbers; use . for decimals.");
    return;
}

try
{
    double result = calculator.DoOperation(a, b, op);
    string text = result.ToString(CultureInfo.InvariantCulture);
    Console.WriteLine("Result: " + text);
}
catch (ArgumentException error)
{
    Console.WriteLine(error.Message);
}
