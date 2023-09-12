# Employee Directory API

## Sprint 1

### GET /employees

Just want to be able to GET a list of employees, and optionally filter them by department.

`GET /employees`
```json
{
    "employees" [
        { "id": "1", "firstName": "Bob", "lastName": "Smith", "department": "DEV", "emailAddress": "bob@aol.com" }
    ]
}

```
`GET /employees?dept=DEV`

```json
{
    "employees": [
        {
            "id": "1",
            "firstName": "Bob",
            "lastName": "Smith",
            "department": "DEV",
            "emailAddress": "bob@aol.com"
        },
        {
            "id": "3",
            "firstName": "Jill",
            "lastName": "Turner",
            "department": "DEV",
            "emailAddress": "jill@aol.com"
        }
    ],
    "showingDepartment": "DEV"
}
```

### GET /employees/{id}
Just get a single employee.

```json
{
    "id": "1",
    "firstName": "Bob",
    "lastName": "Smith",
    "department": "DEV",
    "emailAddress": "bob@aol.com",
    "phoneNumber": "555-1212xt132"
}

```


## Hiring an Employee

### Job Candidates

POST /candidates

- basic contact information
- what are interested in?
- What are their salary requirements

Request

```json
{
    "firstName": "Anakin",
    "lastName": "Skywalker",
    "requiredSalaryMin": 100000,
    "phoneNumber": "555-1212",
    "emailAddress": "karen_smith@compuserve.com",
    "notes": "This is a super great developer. She knows C#, and F#"
}
```

Response

201 Created

```json
{
    "id": "1",
    "firstName": "Anakin",
    "lastName": "Skywalker",
    "requiredSalaryMin": 100000,
    "phoneNumber": "555-1212",
    "emailAddress": "karen_smith@compuserve.com",
    "notes": "This is a super great developer. She knows C#, and F#",
    "status": "AwaitingManagerAssignment",
    "dateCreated": "some date"
}
```