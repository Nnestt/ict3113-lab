using Reqnroll.Assist;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class AvailabilityStepDefinitions(
    ReliabilityContext reliability,
    CalculatorContext calculator)
{
    [Given("the system operated for {double} hours and recorded {int} failures")]
    public void GivenTheSystemOperatingRecord(double operatingTimeHours, int failures)
    {
        reliability.OperatingTimeHours = operatingTimeHours;
        reliability.NumberOfFailures = failures;
    }

    [Given("MTBF is {double} hours and MTTR is {double} hours")]
    public void GivenMtbfAndMttr(double mtbf, double mttr)
    {
        reliability.Mtbf = mtbf;
        reliability.Mttr = mttr;
    }

    [Given("the reliability values are")]
    public void GivenTheReliabilityValuesAre(DataTable table)
    {
        ReliabilityValues values = table.Rows.Single().CreateInstance<ReliabilityValues>();
        reliability.Mtbf = values.Mtbf;
        reliability.Mttr = values.Mttr;
    }

    [When("I calculate MTBF")]
    public void WhenICalculateMtbf()
    {
        calculator.ResetOutcome();

        try
        {
            calculator.Result = calculator.Calculator.MeanTimeBetweenFailures(
                reliability.OperatingTimeHours,
                reliability.NumberOfFailures);
        }
        catch (ArgumentOutOfRangeException error)
        {
            calculator.Error = error;
        }
    }

    [When("I calculate Availability from these values")]
    public void WhenICalculateAvailability()
    {
        calculator.ResetOutcome();

        try
        {
            calculator.Result = calculator.Calculator.Availability(
                reliability.Mtbf,
                reliability.Mttr);
        }
        catch (ArgumentException error)
        {
            calculator.Error = error;
        }
    }

    private sealed class ReliabilityValues
    {
        public double Mtbf { get; set; }
        public double Mttr { get; set; }
    }
}
