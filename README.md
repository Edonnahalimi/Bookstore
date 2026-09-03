# Bookstore API

Backend API for a bookstore built with ASP.NET Core and .NET 10.

The project includes book CRUD operations, book search with pagination, OAuth2 authentication and authorization using Duende IdentityServer, Swagger/OpenAPI, validation, global exception handling, and Docker support.

## Technologies

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* In-Memory Database
* MediatR
* CQRS
* FluentValidation
* Duende IdentityServer
* OAuth2
* Swagger / OpenAPI
* Docker

## Project Structure

The solution is split into the following layers:

* **Domain** - entities and domain models
* **Application** - commands, queries, handlers, DTOs, validators, and repository interfaces
* **Infrastructure** - database context and repository implementations
* **Identity** - IdentityServer configuration and login
* **API** - controllers, middleware, Swagger, and application configuration

## Requirements

* .NET 10 SDK
* Visual Studio or VS Code
* Docker Desktop (optional)

## Running the Application

Open the solution in Visual Studio or VS Code and run the API project.

Swagger:

```text
https://localhost:7143/swagger
```

The port may be different depending on the local launch configuration.

No database setup is required. The application uses an Entity Framework Core In-Memory database.

## Running with Docker

Make sure Docker Desktop is running.

```bash
docker compose up --build
```

To stop the application:

```bash
docker compose down
```

## Authentication

The API uses OAuth2 with Duende IdentityServer.

Two clients are configured:

### Client Credentials

Used for Book CRUD operations.

```text
Client ID: bookstore-client
Client Secret: bookstore-secret
Scope: bookstore
Grant Type: Client Credentials
```

### Implicit Flow

Used for book search through Swagger.

```text
Client ID: bookstore-search
Scope: bookstore
Grant Type: Implicit
```

Swagger is configured to use the `bookstore-search` client.

### Test Login

```text
Username: testuser
Password: password
```

These credentials are for local development and testing only.

---

# API

## Books

### Get all books

```http
GET /v1/books
```

### Get a book

```http
GET /v1/books/{id}
```

Example:

```http
GET /v1/books/1
```

### Create a book

```http
POST /v1/books
```

Example:

```json
{
  "authorId": 1,
  "title": "Clean Code",
  "subTitle": "A Handbook of Agile Software Craftsmanship"
}
```

### Update a book

```http
PUT /v1/books/{id}
```

Example:

```json
{
  "authorId": 1,
  "title": "Clean Code - Updated",
  "subTitle": "Updated subtitle"
}
```

### Delete a book

```http
DELETE /v1/books/{id}
```

---

## Search

Search books by title and/or author with pagination.

```http
GET /v1/books/search
```

Supported parameters:

* `title`
* `author`
* `page`
* `pageSize`

Examples:

```http
GET /v1/books/search?title=Clean&page=1&pageSize=10
```

```http
GET /v1/books/search?author=Martin&page=1&pageSize=10
```

```http
GET /v1/books/search?title=Clean&author=Martin&page=1&pageSize=10
```

Example response:

```json
{
  "items": [],
  "page": 1,
  "pageSize": 10,
  "totalCount": 0,
  "totalPages": 0
}
```

Search uses the `bookstore-search` client and the OAuth2 Implicit Flow.

---

## Authors

### Create an author

```http
POST /v1/authors
```

Example:

```json
{
  "name": "Robert C. Martin"
}
```

The returned author ID can then be used when creating or updating a book.

---

# Validation

The API uses FluentValidation.

### Author

* Name is required
* Minimum length: 3
* Maximum length: 100

### Book

* Title is required
* Minimum length: 3
* Maximum length: 100
* `AuthorId` must be greater than 0
* The specified author must exist

Validation errors return `400 Bad Request`.

---

# Error Handling

A global exception middleware is used to handle unexpected errors.

* `400 Bad Request` - validation errors
* `401 Unauthorized` - missing or invalid authentication
* `403 Forbidden` - authenticated but not allowed to access the endpoint
* `404 Not Found` - resource does not exist
* `500 Internal Server Error` - unexpected error

Internal exception details are not exposed to the client.

---

# Testing

The API can be tested using Swagger and PowerShell.

## 1. Book Search - Swagger

Start the application and open:

```text
https://localhost:7143/swagger
```

Click **Authorize**.

Swagger uses the `bookstore-search` client and the OAuth2 Implicit Flow.

You will be redirected to the IdentityServer login page.

Use:

```text
Username: testuser
Password: password
```

After login, Swagger will be authorized.

You can then test:

```http
GET /v1/books/search?title=Clean&page=1&pageSize=10
```

---

## 2. Book CRUD - Client Credentials

Book CRUD uses the `bookstore-client` client.

The easiest way to test it locally is with PowerShell.

### Get an access token

```powershell
$body = @{
    client_id     = "bookstore-client"
    client_secret = "bookstore-secret"
    grant_type    = "client_credentials"
    scope         = "bookstore"
}

$response = Invoke-RestMethod `
    -Uri "https://localhost:7143/connect/token" `
    -Method Post `
    -ContentType "application/x-www-form-urlencoded" `
    -Body $body

$token = $response.access_token
```

### Create an author

```powershell
$author = @{
    name = "Robert Martin"
} | ConvertTo-Json

$createdAuthor = Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/authors" `
    -Method Post `
    -Headers @{
        Authorization = "Bearer $token"
    } `
    -ContentType "application/json" `
    -Body $author

$createdAuthor
```

Use the returned author ID when creating the book.

### Create a book

```powershell
$book = @{
    authorId = 1
    title = "Clean Code"
    subTitle = "A Handbook of Agile Software Craftsmanship"
} | ConvertTo-Json

$createdBook = Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/books" `
    -Method Post `
    -Headers @{
        Authorization = "Bearer $token"
    } `
    -ContentType "application/json" `
    -Body $book

$createdBook
```

### Get the book

```powershell
Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/books/1" `
    -Method Get `
    -Headers @{
        Authorization = "Bearer $token"
    }
```

### Update the book

```powershell
$updatedBook = @{
    authorId = 1
    title = "Clean Code - Updated"
    subTitle = "Updated subtitle"
} | ConvertTo-Json

Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/books/1" `
    -Method Put `
    -Headers @{
        Authorization = "Bearer $token"
    } `
    -ContentType "application/json" `
    -Body $updatedBook
```

### Delete the book

```powershell
Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/books/1" `
    -Method Delete `
    -Headers @{
        Authorization = "Bearer $token"
    }
```

A successful delete returns `204 No Content`.

To verify:

```powershell
Invoke-RestMethod `
    -Uri "https://localhost:7143/v1/books/1" `
    -Method Get `
    -Headers @{
        Authorization = "Bearer $token"
    }
```

The expected result is `404 Not Found`.

---

# Database

The application uses Entity Framework Core with an In-Memory database:

```text
BookstoreDb
```

No external database setup is required.

Data is reset when the application restarts.

# Docker

Docker support is included in the solution.

```bash
docker compose up --build
```

Stop the containers with:

```bash
docker compose down
```

# Notes

The assignment allows either an In-Memory database or SQL Server, so this project uses an In-Memory database to keep the setup simple.

The OAuth2 clients and test credentials included in the project are intended for local development and testing.
