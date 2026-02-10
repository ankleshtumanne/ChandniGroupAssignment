# Chandni Group Assignment – B2B Management API

This project is a **B2B Management Backend API** built using **ASP.NET Core (.NET 8)**.  
The application is designed with **clean, scalable, and maintainable architecture** following industry best practices.

---

## 🚀 Key Highlights

- Built with **ASP.NET Core Web API (.NET 8)**
- Follows **Clean Architecture**
- Implements **3-Layer Architecture**
- Designed using **SOLID Principles**
- Uses **JWT Authentication**
- Centralized constants using **AppConstants**
- Swagger integrated with JWT Authorization support

---

## 🏗 Architecture Overview

### 🔹 Clean Architecture
The project follows Clean Architecture principles to ensure:
- Separation of concerns
- Loose coupling
- High testability
- Easy maintainability

---

### 🔹 3-Layer Architecture

The application is structured into the following layers:

#### 1️⃣ Presentation Layer
- Controllers
- Handles HTTP requests and responses
- Uses DTOs for input/output

#### 2️⃣ Business / Service Layer
- Contains business logic
- Implements service interfaces
- Ensures rules and validations are applied

#### 3️⃣ Data Access Layer
- Repositories
- Entity Framework Core
- Handles database operations

---

## 🧠 SOLID Principles Used

- **S – Single Responsibility Principle**  
  Each class has only one responsibility.

- **O – Open/Closed Principle**  
  Code is open for extension but closed for modification.

- **L – Liskov Substitution Principle**  
  Interfaces and implementations are properly substituted.

- **I – Interface Segregation Principle**  
  Small and specific interfaces are used.

- **D – Dependency Inversion Principle**  
  High-level modules depend on abstractions, not concrete implementations.

---

## 📁 AppConstants

A centralized **AppConstants** class is used to:
- Store reusable constant values
- Avoid magic strings
- Improve code readability and maintainability  
Examples:
- JWT scheme names
- Authorization headers
- Common response messages
- Route names

---

## 🔐 Authentication & Security

- JWT-based authentication
- Token validation with issuer, audience, and signing key
- Secured APIs using `[Authorize]`
- Public APIs marked with `[AllowAnonymous]`

---

## 🧪 API Documentation

- Swagger UI integrated
- JWT Bearer authentication enabled in Swagger
- Easy testing of secured and unsecured endpoints

---

## 🛠 Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (Swashbuckle)
- Dependency Injection

---

## 📌 Conclusion

This project demonstrates a clean, scalable, and professional backend architecture suitable for real-world B2B applications.  
It follows modern development standards and best practices used in production-grade systems.

---

✅ **Author**: Anklesh Tumanne  
