# DLS_IT_Solutions.MiniE-Commerce-BE
Mini E-Commerce Product Management System - Backend
Overview
This is the backend implementation of a simplified e-commerce product management system built using ASP.NET Core Web API. It provides APIs for product and category management, user authentication, and cart functionality. The backend uses a layered architecture (Controllers, Services, Repositories), Entity Framework Core for database operations, and JWT-based authentication for secure endpoints.
Tech Stack

Framework: ASP.NET Core 8.0
Database: SQLite (configurable to SQL Server)
ORM: Entity Framework Core
Authentication: JWT
Mapping: AutoMapper
Validation: DataAnnotations
Dependency Injection: Built-in ASP.NET Core DI

Setup Instructions
Prerequisites

.NET SDK 8.0
SQLite or SQL Server (optional, if switching database)
Visual Studio 2022 or VS Code with C# extensions
Postman or similar for API testing

Installation

Clone the Repository:
git clone https://github.com/your-username/your-repo.git
cd backend


Restore Dependencies:
dotnet restore


Configure Database:

The default database is SQLite, and the database file (ecommerce.db) is created automatically.
To use SQL Server, update the connection string in appsettings.json:"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MiniE_CommerceDB;Integrated Security=true;TrustServerCertificate=True"
}


Run migrations to create the database:dotnet ef migrations add InitialCreate
dotnet ef database update




Run the Application:
dotnet run

The API will be available at [https://localhost:7053] (or the port specified in launchSettings.json).


API Endpoints
Authentication

Register: POST /api/auth/register
Body: { "username": "string", "email": "string", "password": "string" }
Response: { "token": "jwt_token" }


Login: POST /api/auth/login
Body: { "email": "string", "password": "string" }
Response: { "token": "jwt_token" }



Products (CRUD)

Get All Products: GET /api/products
Query Params: categoryId (optional, for filtering)
Response: List of products


Get Product by ID: GET /api/products/{id}
Response: Product details


Create Product: POST /api/products [Authorize: Admin]
Body: { "name": "string", "description": "string", "price": number, "categoryId": number }
Response: Created product


Update Product: PUT /api/products/{id} [Authorize: Admin]
Body: { "name": "string", "description": "string", "price": number, "categoryId": number }
Response: Updated product


Delete Product: DELETE /api/products/{id} [Authorize: Admin]
Response: 204 No Content



Categories (CRUD)

Get All Categories: GET /api/categories
Response: List of categories


Get Category by ID: GET /api/categories/{id}
Response: Category details


Create Category: POST /api/categories [Authorize: Admin]
Body: { "name": "string" }
Response: Created category


Update Category: PUT /api/categories/{id} [Authorize: Admin]
Body: { "name": "string" }
Response: Updated category


Delete Category: DELETE /api/categories/{id} [Authorize: Admin]
Response: 204 No Content



Cart

View Cart: GET /api/cart [Authorize]
Response: List of cart items for the authenticated user


Add to Cart: POST /api/cart [Authorize]
Body: { "productId": number, "quantity": number }
Response: Updated cart item


Remove from Cart: DELETE /api/cart/{productId} [Authorize]
Response: 204 No Content



Default Credentials

Admin User:
Email: Admin@Admin.com
Password: Admin@123
User:
Email: user@user.com
Password: User@123

Role: Admin (can perform CRUD on products and categories) ,user



Project Structure
backend/
├── Controllers/          # API controllers
├── DTOs/                # Data Transfer Objects
├── Entities/            # EF Core entities (Product, Category, CartItem, User)
├── Mappings/            # AutoMapper profiles
├── Repositories/        # Data access layer
├── Services/            # Business logic
├── appsettings.json     # Configuration (connection strings, JWT settings)
├── Program.cs           # Entry point
└── Startup.cs           # Middleware and DI configuration

Notes

JWT Authentication: Secure endpoints require a valid JWT token in the Authorization header (Bearer <token>).
Validation: Uses DataAnnotations for input validation (e.g., [Required], [MaxLength]).
Database: SQLite is used for simplicity. Switch to SQL Server by updating the connection string.
Error Handling: Global exception middleware returns structured error responses.
Bonus: The backend supports filtering products by category and uses AutoMapper for DTO mapping.

Testing

Use Postman or a similar tool to test APIs.
Start by registering a user or logging in as the admin.
Use the returned JWT token to access protected endpoints.

Troubleshooting

Database Issues: Ensure the connection string is correct and migrations are applied.
JWT Errors: Verify the token is included in the Authorization header and not expired.
CORS Issues: Update Startup.cs if the frontend is hosted on a different domain.

For further details, refer to the Frontend README.
