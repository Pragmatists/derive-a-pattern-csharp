#noinspection CucumberUndefinedStep
Feature: Basic sales

  Scenario: Standard order includes shipping cost
    Given client wants to buy items:
      | price | type |
      | 100   | cd   |
      | 100   | book |
    When client views the order summary
    Then total of the order is 215

  Scenario: Order of books only has lower shipping cost
    Given client wants to buy items:
      | price | type |
      | 50    | book |
      | 50    | book |
    When client views the order summary
    Then total of the order is 105

  Scenario: Order of books only worth more than 200 is free
    Given client wants to buy items:
      | price | type |
      | 100   | book |
      | 105   | book |
    When client views the order summary
    Then total of the order is 205

  Scenario: International shipping of light items
    Given client wants to buy items:
      | price | weight |
      | 100   | 1      |
    And wants them delivered to 'Germany'
    When client views the order summary
    Then total of the order is 150

  Scenario: International shipping of heavy items
    Given client wants to buy items:
      | price | weight |
      | 100   | 15     |
    And wants them delivered to 'Germany'
    When client views the order summary
    Then total of the order is 170