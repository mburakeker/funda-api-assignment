# Funda API Assignment

This project is a .NET application that interacts with the Funda API to fetch real estate offers and generate reports on top agents by offer count.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Design Decisions and Remarks](#design-decisions-and-remarks)
- [Results](#results)

## Getting Started

These instructions will help you set up and run the project on your local machine for development and testing purposes.

## Prerequisites

- .NET 9.0 SDK or later
- Visual Studio or JetBrains Rider
- Funda API Key

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/mburakeker/funda-api-assignment.git
    cd funda-api-assignment
    ```

2. Set up the Funda API key in the `appsettings.json` file:
    ```json
    {
      "FundaOfferApiSettings": {
        "ApiKey": "<your-api-key>",
        "Url": "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/"
      }
    }
    ```

3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

## Usage

1. Build the project:
    ```sh
    dotnet build
    ```

2. Run the application:
    ```sh
    dotnet run --project Funda.ApiAssignment.API
    ```

3. Open your browser and navigate to: `http://localhost:5155/swagger` to view and test the endpoints using Swagger UI.

## Running Tests

To run the unit tests, use the following command:
```sh
dotnet test
```

## Project Structure
- `Funda.ApiAssignment.API`: The main API project.
- `Funda.ApiAssignment.Domain`: Contains domain models, handlers, and interfaces of providers.
- `Funda.ApiAssignment.Domain.Tests`: Contains unit tests for the domain project
- `Funda.ApiAssignment.Infrastructure`: Contains infrastructure-related code such as API clients and providers.
- `Funda.ApiAssignment.Infrastructure.Tests`: Contains unit tests for the infrastructure project

## Design Decisions and Remarks

- The project structure follows the [Design a DDD-oriented microservice by Microsoft.](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)
   - Projects depend on each other based on the Layer Design in the document and are logically separated.
- Used System.Net.Http.Json for consuming REST APIs instead of REST client libraries to keep it simple.
- For resilience, Microsoft.Extensions.Http.Resilience is used which is based on Polly

## Results
1. Makelaar's in Amsterdam that have the most objects listed for sale

| Makelaar Id  | Offer Count |
| ----- | ----- |
| 24648  | 118  |
| 24705  | 80  |
| 24067  | 79  |
| 24605  | 73  |
| 24065  | 64  |
| 24592  | 59  |
| 24079  | 53  |
| 24848  | 50  |
| 24053  | 50  |
| 24131  | 49  |

2. Makelaar's in Amsterdam that have the most objects with tuin listed for sale

| Makelaar Id  | Offer Count |
| ----- | ----- |
| 24648  | 29  |
| 24067  | 19  |
| 24065  | 16  |
| 24594  | 15  |
| 24079  | 11  |
| 24592  | 10  |
| 60557  | 10  |
| 12285  | 10  |
| 24848  | 8  |
| 24605  | 8  |
