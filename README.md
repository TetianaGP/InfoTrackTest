# DEMOQA Web Tables – Automated UI Testing Project

This project demonstrates automated UI tests for the **Web Tables** module on (https://demoqa.com/webtables) using the following technologies:

- Selenium WebDriver – for browser automation  
- NUnit – for organizing and executing test cases  
- Polly – for resilient retry logic when locating dynamic elements  
- JSON – used as a structured input source for test data  
- Screenshot logging – captured at each test step for better traceability  

## Project Structure

- Tests/ # NUnit test classes
- Pages/ # Page Object Models (POM)
- Models/ # C# classes for test data
- JSON/Users.json # Input test data
- bin/Debug/Screenshots/ # Step-by-step screenshots from test runs

## Automated Test Scenarios

The test suite includes the following scenarios:

- Add New Table Record – Fills and submits the form to add a new user
- Update Existing Record – Edits a user’s salary and validates the update
- Delete Record – Deletes a user row and verifies it no longer appears

## Retry and Wait Logic

To improve test stability, this project implements custom retry logic using Polly's `WaitAndRetry` policy. This retry mechanism:

- Retries finding elements up to a defined number of times (e.g., 10)
- Waits 500 milliseconds between attempts
- Handles transient issues like `NoSuchElementException` and `StaleElementReferenceException`

Special wait methods like `WaitForUpdatedRowDisplayed()` and `waitForTableRowDeletion()` ensure that:

- New or updated values (FirstName, LastName, Email, etc.) are visible
- Deleted values are confirmed removed, even if the page updates with a delay

## Screenshot Support

To assist in debugging and failure analysis:

- Screenshots are taken after each test step
- Stored in the `Screenshots/` folder within `bin/Debug`
- Filenames include the test name, step name, and timestamp
- Implemented via a centralized utility method in `ScreenshotHelper`

### Prerequisites

- Visual Studio with .NET SDK
- Chrome browser

## Technologies Used

- C#
- Selenium WebDriver
- NUnit
- Polly
- System.Text.Json
