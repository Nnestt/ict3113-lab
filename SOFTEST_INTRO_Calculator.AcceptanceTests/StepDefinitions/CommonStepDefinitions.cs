using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class CommonStepDefinitions(CalculatorContext context)
{
    [Given("I have a calculator")]
    public void GivenIHaveACalculator()
    {
        context.Calculator = new Calculator();
        context.ResetOutcome();
    }

    [Then("the result should be {double}")]
    public void ThenTheResultShouldBe(double expected)
    {
        Assert.That(context.Error, Is.Null);
        Assert.That(context.Result, Is.Not.Null);
        Assert.That(context.Result!.Value, Is.EqualTo(expected).Within(1e-9));
    }

    [Then("the integer result should be {long}")]
    public void ThenTheIntegerResultShouldBe(long expected)
    {
        Assert.That(context.Error, Is.Null);
        Assert.That(context.IntegerResult, Is.EqualTo(expected));
    }

    [Then("an ArgumentException should be raised")]
    public void ThenAnArgumentExceptionShouldBeRaised()
    {
        Assert.That(context.Error, Is.TypeOf<ArgumentException>());
    }

    [Then("an ArgumentOutOfRangeException should be raised")]
    public void ThenAnArgumentOutOfRangeExceptionShouldBeRaised()
    {
        Assert.That(context.Error, Is.TypeOf<ArgumentOutOfRangeException>());
    }
}
