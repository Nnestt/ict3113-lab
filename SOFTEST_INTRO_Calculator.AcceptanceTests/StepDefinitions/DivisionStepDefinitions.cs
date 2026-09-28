using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class DivisionStepDefinitions(CalculatorContext context)
{
    [When("I divide {double} by {double}")]
    public void WhenIDivide(double numerator, double divisor)
    {
        context.ResetOutcome();

        try
        {
            context.Result = context.Calculator.Divide(numerator, divisor);
        }
        catch (ArgumentException error)
        {
            context.Error = error;
        }
    }
}
