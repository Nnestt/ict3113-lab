using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class AdditionStepDefinitions(CalculatorContext context)
{
    [When("I have entered {double} and {double} into the calculator and press add")]
    public void WhenIEnterValuesAndPressAdd(double first, double second)
    {
        context.ResetOutcome();
        context.Result = context.Calculator.Add(first, second);
    }
}
