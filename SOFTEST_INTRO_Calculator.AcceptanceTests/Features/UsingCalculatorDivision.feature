@Divisions
Feature: Using calculator division
  Division returns a quotient for non-zero divisors and rejects zero divisors.

  Scenario Outline: Divide by a non-zero divisor
    Given I have a calculator
    When I divide <numerator> by <divisor>
    Then the result should be <quotient>

    Examples:
      | numerator | divisor | quotient |
      | 1         | 2       | 0.5      |
      | 0         | 15      | 0        |
      | 15        | -3      | -5       |

  Scenario Outline: Reject division by zero
    Given I have a calculator
    When I divide <numerator> by 0
    Then an ArgumentException should be raised

    Examples:
      | numerator |
      | 15        |
      | 0         |
