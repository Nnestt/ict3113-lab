using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class BasicMusaStepDefinitions(
    BasicMusaContext musa,
    CalculatorContext calculator)
{
    [Given("the initial failure intensity is {double} failures per hour")]
    public void GivenTheInitialFailureIntensity(double value)
    {
        musa.InitialFailureIntensity = value;
    }

    [Given("the expected total failures over infinite execution time is {double}")]
    public void GivenTheExpectedTotalFailures(double value)
    {
        musa.ExpectedTotalFailures = value;
    }

    [Given("the accumulated execution time is {double} hours")]
    public void GivenTheAccumulatedExecutionTime(double value)
    {
        musa.ExecutionTimeHours = value;
    }

    [When("I calculate the current failure intensity")]
    public void WhenICalculateTheCurrentFailureIntensity()
    {
        calculator.ResetOutcome();

        try
        {
            calculator.Result = calculator.Calculator.BasicMusaFailureIntensity(
                musa.InitialFailureIntensity,
                musa.ExpectedTotalFailures,
                musa.ExecutionTimeHours);
        }
        catch (ArgumentOutOfRangeException error)
        {
            calculator.Error = error;
        }
    }

    [When("I calculate the expected cumulative failures")]
    public void WhenICalculateTheExpectedCumulativeFailures()
    {
        calculator.ResetOutcome();

        try
        {
            calculator.Result = calculator.Calculator.BasicMusaExpectedCumulativeFailures(
                musa.InitialFailureIntensity,
                musa.ExpectedTotalFailures,
                musa.ExecutionTimeHours);
        }
        catch (ArgumentOutOfRangeException error)
        {
            calculator.Error = error;
        }
    }
}
