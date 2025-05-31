# 🛒 Dotnet E-Commerce App

A full-stack e-commerce web application built using:
- ASP.NET Core Web API (Backend)
- Razor Pages (Frontend)
- Entity Framework Core (ORM)
- SQLite
- HttpClient for backend API calls from frontend



## ⚙️ Prerequisites

- [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)

## 🔧 Getting Started

### 1. Clone the Repository

```bash
git https://github.com/capt-farvez/dotnet-ecommerce-app.git

cd dotnet-ecommerce-app
```

### 2. Restore NuGet Packages
```bash
dotnet restore ./backend/Backend
dotnet restore ./frontend/Frontend
```
### 3. Apply Database Migrations
- Go to `Backend` Folder
```bash
dotnet ef database update
```

## 🚀 Running the App
### 1. Run Backend API
- Go to `Backend` Folder
```bash
dotnet watch run
```

### 2. Run Frontend (Razor Pages)
- Go to `Frontend` Folder
```bash
dotnet watch run
```