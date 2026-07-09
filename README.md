# 🎬 Movie Catalog API

A RESTful Web API built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server** for managing movie categories and movies.

This project demonstrates:
- CRUD Operations
- Entity Framework Core Code First
- DTO Pattern
- Data Validation using Data Annotations
- REST API Best Practices
- Swagger Documentation
- Postman Testing

---

## 🚀 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Swagger / OpenAPI
- Postman
- C#

---

## 📂 Project Structure

```text
MovieCatalog.API
│
├── Controllers
│   ├── CategoriesController.cs
│   └── MoviesController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   ├── Category.cs
│   └── Movie.cs
│
├── DTOs
│   ├── CategoryCreateDto.cs
│   ├── CategoryReadDto.cs
│   ├── MovieCreateDto.cs
│   ├── MovieReadDto.cs
│   └── MovieUpdateDto.cs
│
├── Migrations
│
├── appsettings.json
├── Program.cs
└── README.md
```

---

## 🗄 Database Design

### Category

| Property | Type |
|-----------|--------|
| Id | int |
| Name | string |
| Movies | ICollection<Movie> |

### Movie

| Property | Type |
|-----------|--------|
| Id | int |
| Title | string |
| ReleaseYear | int |
| Rating | double |
| Director | string |
| CategoryId | int |
| Category | Category |

---

## 🔗 Relationship

### One-to-Many

A Category can contain multiple Movies.

```text
Category
   |
   | 1
   |
   |------< Movies
              *
```

---

## 📋 Validation Rules

### Category

| Property | Validation |
|-----------|------------|
| Name | Required, MaxLength(100) |

### Movie

| Property | Validation |
|-----------|------------|
| Title | Required, MaxLength(150) |
| ReleaseYear | Range(1950,2035) |
| Rating | Range(1,10) |
| Director | Required |

---

## 📦 DTOs

### CategoryReadDto

```csharp
public class CategoryReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

### CategoryCreateDto

```csharp
public class CategoryCreateDto
{
    public string Name { get; set; }
}
```

### MovieReadDto

```csharp
public class MovieReadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public string CategoryName { get; set; }
}
```

### MovieCreateDto

```csharp
public class MovieCreateDto
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public string Director { get; set; }
    public int CategoryId { get; set; }
}
```

### MovieUpdateDto

```csharp
public class MovieUpdateDto : MovieCreateDto
{
}
```

---

## 🌐 API Endpoints

### Categories

| Method | Endpoint | Description |
|----------|----------|-------------|
| GET | /api/categories | Get All Categories |
| GET | /api/categories/{id} | Get Category By Id |
| POST | /api/categories | Create Category |
| PUT | /api/categories/{id} | Update Category |
| DELETE | /api/categories/{id} | Delete Category |

---

### Movies

| Method | Endpoint | Description |
|----------|----------|-------------|
| GET | /api/movies | Get All Movies |
| GET | /api/movies/{id} | Get Movie By Id |
| POST | /api/movies | Create Movie |
| PUT | /api/movies/{id} | Update Movie |
| DELETE | /api/movies/{id} | Delete Movie |

---

## ✅ HTTP Status Codes

| Operation | Status Code |
|------------|------------|
| GET | 200 OK |
| POST | 201 Created |
| PUT | 204 No Content |
| DELETE | 204 No Content |
| Validation Error | 400 Bad Request |
| Resource Not Found | 404 Not Found |

---

## 🔄 Entity to DTO Mapping

Entity objects are never returned directly.

Example:

```csharp
var movies = _context.Movies
    .Include(m => m.Category)
    .Select(m => new MovieReadDto
    {
        Id = m.Id,
        Title = m.Title,
        Rating = m.Rating,
        CategoryName = m.Category.Name
    })
    .ToList();
```

---

## 🧰 Entity Framework Core

### Create Migration

```powershell
Add-Migration InitialCreate
```

### Update Database

```powershell
Update-Database
```

---

## ⚙ Connection String

```json
"ConnectionStrings": {
  "DefaultConnection":
  "Server=.;Database=MovieCatalogDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

## 📖 Swagger

Swagger is enabled for API documentation and endpoint testing.

Run the application and navigate to:

```text
https://localhost:{port}/swagger
```

---

## 🧪 Postman Testing

All endpoints were tested using:

- Swagger UI
- Postman Collection

The Postman collection is included in the repository.

---

## 🎯 Features

- RESTful API Design
- Entity Framework Core Code First
- SQL Server Integration
- DTO Pattern
- Data Validation
- LINQ Queries
- Swagger Documentation
- Postman Testing
- Proper HTTP Status Codes
- Clean Architecture Structure

---

## ⭐ Bonus Implemented

✔ Return CategoryName inside MovieReadDto

✔ Use DTOs for all endpoints

✔ Include() for loading Category information

✔ Entity-to-DTO mapping using Select()

---

## 🚫 Not Included

This assignment intentionally stops before:

- Authentication
- Authorization
- ASP.NET Core Identity
- JWT Tokens

---

## 👨‍💻 Author
Shrouq Ramadan
