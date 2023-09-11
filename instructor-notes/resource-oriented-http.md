# Resource Oriented Architecture

the architectural "style" of HTTP
(as opposed to Object-Oriented, or Service Oriented)

## Resource
- A meaningful thingy you want to expose through your API

- Example: Customers -
    - This would be identied through a "Uniform Resource Identifier" (URI)
    - https://api.company.com/customers"
        - https is the "Scheme", and either HTTP or HTTPS
        - api.company.com - "the Authority" (we call it the server)
            - the authority gives meaning to the resources.
        - /customers - is the resource.

## The Verbs are the HTTP Methods
- You don't get to make these up.
There are 4 *primary* verbs that you will use 99.99% of the time in HTTP
- GET
- POST
- PUT
- DELETE



"api.ticketmaster.com/tickets"
"api.cpd.gov/tickets"

# "REST" - passing state to the server from the client

- We pass "state" to the server using various techniques.
    - We will do these immediately after lunch
    - We need to know who you are
    - We need to know some "variables"
    - URLS, Route Parameters, Query String Arguments, Headers
    - Entities (Bodies)
    
- We pass "state" from the server to the client in the body/entity of the response
    - We've already done this, but we'll do more.
