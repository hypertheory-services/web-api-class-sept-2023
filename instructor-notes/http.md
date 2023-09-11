# Http

## Passing data to the API

### Vectors

- Resource
- Route Params
- Query String
- Headers

- WE can also send data in the "body" of the request IF
the request is either a POST or a PUT request 
(GET, and DELETE should not have a body)

## Types of Resources

### Collection

Almost always a plural name. Represents an associated group of documents, usually.

For example:

`GET /employees` -> all employees
`GET /employees?dept=DEV` -> Just the employees in that department.

### Document

A singular name, a leaf-level of a collection.

For example:

`GET /employees/bob-smith`

(Where `bob-smith` is the identifier for an employee)

May contain *subordinate resources*:

`GET /employees/bob-smith/manager` (A document)

`GET /employees/bob-smith/addresses` (A collection)

`GET /employees/bob-smith/addresses/home` A document

#### Special Cases

##### Store

Used to mirror client state in the server. Always associated with a specific session or user of your API.

Example might be a shopping cart. Or a quote.

Stores often become documents or collections at some point. (a shopping cart becomes an order, for example, a quote becomes a policy)

##### "Controller" or "RPC-Like"

Not everything can be modelled using resources, or it is awkward to do so.

Use these sparingly.

Example:

`POST /employees/bob-smith/vacation-requests/resubmit`

## Methods

The KEY Http Methods for API Developers are GET, POST, PUT, DELETE.

The Http "unsafe" methods (POST, PUT, DELETE) are a *declarative* language for a client to express their desired state.

| Method | Resource Type | Example                       | Meaning                                     | Cacheable | Safe  | Idempotent |
| ------ | ------------- | ----------------------------- | ------------------------------------------- | --------- | ----- | ---------- |
| GET    | Collection    | `GET /employees`              | Get me information about this collection    | True      | True  | True       |
| GET    | Document      | `GET /employee/bob-smith`     | Get me this document.                       | True      | True  | True       |
| POST   | Collection    | `POST /employees`             | Consider making this a subordinate resource | False*    | False | False      |
| POST   | Document      | `POST /employees/bob-smith`   | Submit this entity for processing           | False     | False | false      |
| DELETE | Collection    | `DELETE /employees`           | Make this no longer exist                   | False     | False | True       |
| DELETE | Document      | `DELETE /employees/bob-smith` | Make this no longer exist                   | False     | False | True       |
| PUT    | Collection    | `PUT /employees`              | Replace this collection                     | False     | False | False      |
| PUT    | Document      | `PUT /employees/bob-smith`    | Replace this document                       | False     | False | True       |

- Cacheable means that the response can have a cache control header that says how long this is "valid"