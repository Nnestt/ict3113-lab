using SOFTEST_INTRO_Calculator;
using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    private Calculator _calculator = null!;

    // Creates a fresh calculator before each test so one test cannot affect another.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // Verifies the simplest addition path and acts as a readable smoke test for the calculator.
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        double result = _calculator.Add(10, 20);

        Assert.That(result, Is.EqualTo(31));
    }

    // Covers representative addition cases, including zero, negative values, and floating-point precision.
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(
        double a,
        double b,
        double expected)
    {
        double result = _calculator.Add(a, b);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Confirms subtraction works for normal values, zero, and negative operands.
    [TestCase(10, 4, 6)]
    [TestCase(5, 0, 5)]
    [TestCase(-3, -8, 5)]
    public void Subtract_RepresentativeInputs_ReturnsDifference(
        double a,
        double b,
        double expected)
    {
        double result = _calculator.Subtract(a, b);

        Assert.That(result, Is.EqualTo(expected));
    }

    // Confirms multiplication returns the expected product for positive, zero, and negative inputs.
    [TestCase(4, 5, 20)]
    [TestCase(7, 0, 0)]
    [TestCase(-3, 8, -24)]
    public void Multiply_RepresentativeInputs_ReturnsProduct(
        double a,
        double b,
        double expected)
    {
        double result = _calculator.Multiply(a, b);

        Assert.That(result, Is.EqualTo(expected));
    }

    // Checks valid division cases while allowing for small floating-point rounding differences.
    [TestCase(1, 2, 0.5)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_NonZeroDivisor_ReturnsQuotient(
        double a,
        double b,
        double expected)
    {
        double result = _calculator.Divide(a, b);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Ensures the calculator reports invalid division instead of returning an unsafe result.
    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
            Throws.TypeOf<ArgumentException>());
    }

    // Protects the operation dispatcher from silently accepting unsupported operation codes.
    [Test]
    public void DoOperation_UnknownOperation_ThrowsArgumentException()
    {
        Assert.That(() => _calculator.DoOperation(1, 2, "x"),
            Throws.TypeOf<ArgumentException>());
    }

    // Documents the mathematical base case used by factorial and the counting methods.
    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);

        Assert.That(result, Is.EqualTo(1L));
    }

    // Verifies factorial across the supported range, including the largest value that fits in a long.
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsProductFromOneToInput(
        int n,
        long expected)
    {
        long result = _calculator.Factorial(n);

        Assert.That(result, Is.EqualTo(expected));
    }

    // Confirms factorial rejects values the program cannot safely or meaningfully calculate.
    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_OutsideSupportedRange_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Checks triangle area calculations for ordinary and zero-sized dimensions.
    [TestCase(3, 4, 6)]
    [TestCase(0, 4, 0)]
    [TestCase(3, 0, 0)]
    public void TriangleArea_NonNegativeDimensions_ReturnsArea(
        double height,
        double width,
        double expected)
    {
        double result = _calculator.TriangleArea(height, width);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Ensures invalid triangle dimensions are rejected before an impossible area is returned.
    [TestCase(-1, 4)]
    [TestCase(3, -1)]
    public void TriangleArea_NegativeDimension_ThrowsArgumentOutOfRangeException(
        double height,
        double width)
    {
        Assert.That(() => _calculator.TriangleArea(height, width),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Verifies circle area for typical, zero, and larger radii using the program's Math.PI formula.
    [TestCase(1, Math.PI)]
    [TestCase(0, 0)]
    [TestCase(2, 4 * Math.PI)]
    public void CircleArea_NonNegativeRadius_ReturnsArea(
        double radius,
        double expected)
    {
        double result = _calculator.CircleArea(radius);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Ensures a negative radius is rejected because it is not a valid circle measurement.
    [Test]
    public void CircleArea_NegativeRadius_ThrowsArgumentOutOfRangeException()
    {
        Assert.That(() => _calculator.CircleArea(-1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Verifies UnknownFunctionA behaves as permutations, where order matters in the count.
    [TestCase(5, 5, 120L)]
    [TestCase(5, 4, 120L)]
    [TestCase(5, 3, 60L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 30L)]
    public void UnknownFunctionA_ValidInputs_ReturnsPermutationCount(
        int n,
        int r,
        long expected)
    {
        long result = _calculator.UnknownFunctionA(n, r);

        Assert.That(result, Is.EqualTo(expected));
    }

    // Verifies UnknownFunctionB behaves as combinations, where order does not matter in the count.
    [TestCase(5, 5, 1L)]
    [TestCase(5, 4, 5L)]
    [TestCase(5, 3, 10L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 15L)]
    public void UnknownFunctionB_ValidInputs_ReturnsCombinationCount(
        int n,
        int r,
        long expected)
    {
        long result = _calculator.UnknownFunctionB(n, r);

        Assert.That(result, Is.EqualTo(expected));
    }

    // Confirms permutation counting uses the shared input rules for n and r.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 1)]
    [TestCase(5, -1)]
    public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(
        int n,
        int r)
    {
        Assert.That(() => _calculator.UnknownFunctionA(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Confirms combination counting uses the shared input rules for n and r.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 1)]
    [TestCase(5, -1)]
    public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(
        int n,
        int r)
    {
        Assert.That(() => _calculator.UnknownFunctionB(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(1, 11, 7)]
    [TestCase(10, 11, 11)]
    [TestCase(11, 11, 15)]
    public void Add_BinaryDigitSpecialCases_ConcatenatesBits(
        double a,
        double b,
        double expected)
    {
        Assert.That(_calculator.Add(a, b), Is.EqualTo(expected));
    }

    [TestCase(1_000, 5, 200)]
    [TestCase(7.5, 3, 2.5)]
    public void MeanTimeBetweenFailures_ValidInputs_ReturnsObservedAverage(
        double operatingTimeHours,
        int failures,
        double expected)
    {
        Assert.That(
            _calculator.MeanTimeBetweenFailures(operatingTimeHours, failures),
            Is.EqualTo(expected).Within(1e-12));
    }

    [TestCase(0, 1)]
    [TestCase(-1, 1)]
    [TestCase(double.PositiveInfinity, 1)]
    [TestCase(1, 0)]
    [TestCase(1, -1)]
    public void MeanTimeBetweenFailures_InvalidInputs_ThrowsArgumentOutOfRangeException(
        double operatingTimeHours,
        int failures)
    {
        Assert.That(
            () => _calculator.MeanTimeBetweenFailures(operatingTimeHours, failures),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(90, 10, 0.9)]
    [TestCase(0, 10, 0)]
    [TestCase(10, 0, 1)]
    public void Availability_ValidInputs_ReturnsRatio(
        double mtbf,
        double mttr,
        double expected)
    {
        Assert.That(
            _calculator.Availability(mtbf, mttr),
            Is.EqualTo(expected).Within(1e-12));
    }

    [TestCase(-1, 1, typeof(ArgumentOutOfRangeException))]
    [TestCase(1, -1, typeof(ArgumentOutOfRangeException))]
    [TestCase(0, 0, typeof(ArgumentException))]
    public void Availability_InvalidInputs_ThrowsExpectedException(
        double mtbf,
        double mttr,
        Type expectedException)
    {
        Assert.That(
            () => _calculator.Availability(mtbf, mttr),
            Throws.TypeOf(expectedException));
    }

    [Test]
    public void BasicMusa_ZeroExecutionTime_ReturnsInitialState()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                _calculator.BasicMusaFailureIntensity(10, 100, 0),
                Is.EqualTo(10).Within(1e-12));
            Assert.That(
                _calculator.BasicMusaExpectedCumulativeFailures(10, 100, 0),
                Is.EqualTo(0).Within(1e-12));
        });
    }

    [Test]
    public void BasicMusa_PositiveExecutionTime_ReturnsModelValues()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                _calculator.BasicMusaFailureIntensity(10, 100, 5),
                Is.EqualTo(6.065306597126334).Within(1e-12));
            Assert.That(
                _calculator.BasicMusaExpectedCumulativeFailures(10, 100, 5),
                Is.EqualTo(39.346934028736655).Within(1e-12));
        });
    }

    [TestCase(0, 100, 1)]
    [TestCase(10, 0, 1)]
    [TestCase(10, 100, -1)]
    public void BasicMusa_InvalidBoundary_ThrowsArgumentOutOfRangeException(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTimeHours)
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => _calculator.BasicMusaFailureIntensity(
                    initialFailureIntensity,
                    expectedTotalFailures,
                    executionTimeHours),
                Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(
                () => _calculator.BasicMusaExpectedCumulativeFailures(
                    initialFailureIntensity,
                    expectedTotalFailures,
                    executionTimeHours),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        });
    }
}
