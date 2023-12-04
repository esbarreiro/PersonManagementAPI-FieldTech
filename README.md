# Person Management API
This project implements a RESTful API for managing person data using C# and .NET Core.

# Table of Contents
Person Model
API Endpoints
Data Storage
Validation
Testing
Documentation
Person Model

The Person class has the following properties:
Id (integer, unique identifier)
FirstName (string)
LastName (string)
Age (integer)

# API Endpoints
1. GET /api/persons
Retrieve a list of all persons.

2. GET /api/persons/{id}
Retrieve a specific person by their unique identifier.

3. POST /api/persons
Create a new person.

4. PUT /api/persons/{id}
Update an existing person.

5. DELETE /api/persons/{id}
Delete a person by their unique identifier.

# Data Storage
For simplicity, the project uses in-memory data storage. It maintains a list of persons stored in memory.

# Validation
Basic input validation is implemented to ensure that the required fields (FirstName, LastName, and Age) are provided when creating a person. Additionally, age is validated to be a positive integer.

# Testing
Unit tests have been written to ensure the correctness of the API endpoints. The tests cover scenarios such as retrieving persons, creating, updating, and deleting persons.

To run the tests, use a testing framework such as NUnit.

# Documentation
To run the project and test the API endpoints:

# Clone the repository.
Open the project in your preferred IDE.
Build and run the project.
Use a tool like Postman or curl to interact with the API.
Libraries and Tools
C#
.NET Core
NUnit (for testing)
