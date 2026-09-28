@Availability
Feature: Calculating observed reliability and availability
  MTBF is observed operating time in hours divided by a positive failure count.
  Availability uses compatible hour units and the repairable-system approximation
  that MTTF is approximately MTBF; the result is a ratio, not a prediction.

  Scenario: Calculating observed MTBF in hours
    Given I have a calculator
    And the system operated for 600 hours and recorded 3 failures
    When I calculate MTBF
    Then the result should be 200

  Scenario: Calculating Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      | 90   | 10   |
    When I calculate Availability from these values
    Then the result should be 0.9

  Scenario: A system with zero MTBF has zero availability
    Given I have a calculator
    And MTBF is 0 hours and MTTR is 8 hours
    When I calculate Availability from these values
    Then the result should be 0

  Scenario: Reject an MTBF observation without failures
    Given I have a calculator
    And the system operated for 600 hours and recorded 0 failures
    When I calculate MTBF
    Then an ArgumentOutOfRangeException should be raised
