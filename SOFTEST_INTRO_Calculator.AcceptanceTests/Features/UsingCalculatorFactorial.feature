@Factorial
Feature: Using calculator factorial
  Factorial is supported for integers from zero through twenty.

  Scenario: Calculate a normal factorial
    Given I have a calculator
    When I calculate the factorial of 5
    Then the integer result should be 120

  Scenario: Calculate the factorial identity
    Given I have a calculator
    When I calculate the factorial of 0
    Then the integer result should be 1

  Scenario: Reject an unsupported factorial input
    Given I have a calculator
    When I calculate the factorial of 21
    Then an ArgumentOutOfRangeException should be raised
