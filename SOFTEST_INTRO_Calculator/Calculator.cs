namespace SOFTEST_INTRO_Calculator;

public class Calculator
{
    public double GenMagicNum(int choice, string path, IFileReader fileReader)
    {
        ArgumentNullException.ThrowIfNull(fileReader);

        if (choice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }

        string[] magicStrings = fileReader.Read(path);
        if (choice >= magicStrings.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }

        double magicNumber = double.Parse(magicStrings[choice]);
        return 2 * Math.Abs(magicNumber);
    }

    /// Adds two numbers so callers can perform the calculator's basic addition operation.
    public double Add(double a, double b)
    {
        if (IsBinaryDigits(a) && IsBinaryDigits(b) && b >= 10)
        {
            string combinedBits = $"{a:0}{b:0}";
            return Convert.ToInt64(combinedBits, 2);
        }

        return a + b;
    }

    /// Subtracts the second number from the first to support basic difference calculations.
    
    public double Subtract(double a, double b) => a - b;

    /// Multiplies two numbers so the calculator can return products for arithmetic workflows.
    
    public double Multiply(double a, double b) => a * b;

    
    /// Divides the first number by the second and protects the program from invalid divide-by-zero input.
    
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new ArgumentException("Cannot divide by zero.");
        }

        return a / b;
    }

    
    /// Calculates n! for supported values and limits the range so the result fits safely in a long.
    ///  long data type. In C#, long has a maximum value, and factorial numbers grow very quickly.
    /// 20! still fits inside a long, but 21! is too large (long.maxValue).
    ///   9,223,372,036,854,775,807
    
    public long Factorial(int n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Factorial is defined for integers from 0 to 20.");
        }

        long result = 1;
        for (int factor = 2; factor <= n; factor++)
        {
            result *= factor;
        }

        return result;
    }

    
    /// Calculates the area of a triangle and rejects negative dimensions that cannot represent real measurements.
    
    public double TriangleArea(double height, double width)
    {
        if (height < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
        }

        if (width < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");
        }

        return height * width / 2;
    }

    
    /// Calculates the area of a circle and validates the radius before using it in the formula.
    
    public double CircleArea(double radius)
    {
        if (radius < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be negative.");
        }

        return Math.PI * radius * radius;
    }

    public double MeanTimeBetweenFailures(double operatingTimeHours, int numberOfFailures)
    {
        if (!double.IsFinite(operatingTimeHours) || operatingTimeHours <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(operatingTimeHours), "Operating time must be positive and finite.");
        }

        if (numberOfFailures <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfFailures), "The number of failures must be positive.");
        }

        return operatingTimeHours / numberOfFailures;
    }

    public double Availability(double mtbf, double mttr)
    {
        if (!double.IsFinite(mtbf) || mtbf < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF must be non-negative and finite.");
        }

        if (!double.IsFinite(mttr) || mttr < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mttr), "MTTR must be non-negative and finite.");
        }

        if (mtbf + mttr <= 0)
        {
            throw new ArgumentException("MTBF and MTTR cannot both be zero.");
        }

        return mtbf / (mtbf + mttr);
    }

    public double BasicMusaFailureIntensity(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTimeHours)
    {
        ValidateBasicMusaInputs(initialFailureIntensity, expectedTotalFailures, executionTimeHours);
        return initialFailureIntensity * Math.Exp(
            -(initialFailureIntensity * executionTimeHours) / expectedTotalFailures);
    }

    public double BasicMusaExpectedCumulativeFailures(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTimeHours)
    {
        ValidateBasicMusaInputs(initialFailureIntensity, expectedTotalFailures, executionTimeHours);
        return expectedTotalFailures * (1 - Math.Exp(
            -(initialFailureIntensity * executionTimeHours) / expectedTotalFailures));
    }

    
    /// Calculates the number of ordered selections, nPr, which supports permutation-style counting.(order does matter)
    
    public long UnknownFunctionA(int n, int r) //r is how many chosen
    {
        ValidateUnknownFunctionInputs(n, r);

        return Factorial(n) / Factorial(n - r);
    }

    
    /// Calculates the number of unordered selections, nCr, which supports combination-style counting. (order doesnt matter)
    
    public long UnknownFunctionB(int n, int r)
    {
        ValidateUnknownFunctionInputs(n, r);

        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    
    /// Routes a short operation code to the matching arithmetic method, giving the program one entry point for simple operations.
    
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }

    
    /// Keeps permutation and combination inputs inside the factorial-supported range.
    
    private static void ValidateUnknownFunctionInputs(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
        {
            throw new ArgumentOutOfRangeException(null, "Inputs must satisfy 0 <= r <= n <= 20.");
        }
    }

    private static bool IsBinaryDigits(double value)
    {
        if (!double.IsFinite(value) || value < 0 || value != Math.Truncate(value))
        {
            return false;
        }

        return value.ToString("0", System.Globalization.CultureInfo.InvariantCulture)
            .All(digit => digit is '0' or '1');
    }

    private static void ValidateBasicMusaInputs(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTimeHours)
    {
        if (!double.IsFinite(initialFailureIntensity) || initialFailureIntensity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialFailureIntensity));
        }

        if (!double.IsFinite(expectedTotalFailures) || expectedTotalFailures <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(expectedTotalFailures));
        }

        if (!double.IsFinite(executionTimeHours) || executionTimeHours < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(executionTimeHours));
        }
    }
}
