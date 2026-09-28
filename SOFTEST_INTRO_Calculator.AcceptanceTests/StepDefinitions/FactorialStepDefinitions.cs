using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class FactorialStepDefinitions(CalculatorContext context)
{
    [When("I calculate the factorial of {int}")]
    public void WhenICalculateTheFactorial(int value)
    {
        context.ResetOutcome();

        try
        {
            context.IntegerResult = context.Calculator.Factorial(value);
        }
        catch (ArgumentOutOfRangeException error)
        {
            context.Error = error;
        }
    }
}
