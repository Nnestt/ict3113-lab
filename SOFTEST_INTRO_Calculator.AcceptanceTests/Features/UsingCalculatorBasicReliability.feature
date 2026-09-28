@BasicMusa
Feature: Basic Musa reliability growth calculations
  Execution time is measured in hours. The model assumes a finite expected total
  number of failures and a constant decrease in intensity after corrected failures.

  Scenario Outline: Calculate current failure intensity
    Given I have a calculator
    And the initial failure intensity is <lambda0> failures per hour
    And the expected total failures over infinite execution time is <nu0>
    And the accumulated execution time is <tau> hours
    When I calculate the current failure intensity
    Then the result should be <intensity>

    Examples:
      | lambda0 | nu0 | tau | intensity         |
      | 10      | 100 | 0   | 10                |
      | 10      | 100 | 5   | 6.065306597126334 |

  Scenario: Calculate expected cumulative failures
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total failures over infinite execution time is 100
    And the accumulated execution time is 5 hours
    When I calculate the expected cumulative failures
    Then the result should be 39.346934028736655

  Scenario: Reject a zero initial failure intensity
    Given I have a calculator
    And the initial failure intensity is 0 failures per hour
    And the expected total failures over infinite execution time is 100
    And the accumulated execution time is 5 hours
    When I calculate the current failure intensity
    Then an ArgumentOutOfRangeException should be raised
