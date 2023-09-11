# Employee Directory API

## Sprint 1

### GET /employees
[Authorize("everyone")]
Just want to be able to GET a list of employees, and optionally filter them by department.

```json
{
    "employees" [
        { "id": "1", "firstName": "Bob", "lastName": "Smith", "department": "DEV", "emailAddress": "bob@aol.com" }
    ]
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


