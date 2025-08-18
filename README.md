# 🛒 Mart Management System

A management system built with **C#** and **.NET 6 Core**, using **SQL Server 2019** as the database.  
This project helps manage products, customers, and business operations with tables, stored procedures, and functions.

---

## 🚀 Features

- Manage **Products** and **Customers**
- Predefined **Stored Procedures** and **Functions**
- Easy setup with **Docker Compose**
- Automatic database initialization (tables, procs, functions)

---

## 📦 Prerequisites

Before running the project, make sure you have installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- (Optional) [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) for database inspection

---

## ⚡ Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/SethyRung/Mart-Management-System.git
cd Mart-Management-System
```

### 2️⃣ Run SQL Server with Docker

We use **Docker Compose** to run SQL Server and initialize the database automatically.

```bash
docker-compose up -d --build
```

- SQL Server will run on `localhost:1433`
- User: `sa`
- Password: `Strong!Passw0rd`
- Database: `MartDB`

📂 Initialization scripts are in `docker/sql/init/`:

- `1_Tables.sql` → creates tables
- `2_Types.sql` → creates types
- `3_StoredProcedures.sql` → creates stored procedures
- `4_Functions.sql` → creates functions
- `5_SeedData.sql` → seeding default user

---

### 3️⃣ Run the Application in Visual Studio

1. Open **Visual Studio 2022** (or later).
2. Go to **File → Open → Project/Solution**.
3. Select `Mart-Management-System.sln`.
4. Make sure the startup project is set to **Mart-Management-System**.
5. Press **F5** (Run with Debugging) or **Ctrl + F5** (Run without Debugging).

The app will connect to SQL Server automatically using the connection string in **`MyOperation.cs`**:

```cs
string conStr = "Server=localhost,1433;Database=MartDB;User Id=sa;Password=Strong!Passw0rd;"
```

Note: 
  - User: **admin**
  - Password: **admin**

---

### 4️⃣ Verify Database Setup

Open **SSMS** and connect with:

- Server: `localhost,1433`
- User: `sa`
- Password: `Strong!Passw0rd`

You should see:

- ✅ Tables
- ✅ Stored Procedures
- ✅ Functions
- ✅ Types

---

## 🛠️ Development Workflow

- Add new tables/functions → put SQL in `docker/sql/init/`
- Restart container to apply changes:

  ```bash
  docker-compose down
  docker-compose up -d --build
  ```

- Update `.NET` code as needed and run with `dotnet run`

---

## 🤝 Contribution

1. Fork the repo
2. Create your feature branch: `git checkout -b feature/YourFeature`
3. Commit your changes: `git commit -m "feat: add new feature"`
4. Push to the branch: `git push origin feature/YourFeature`
5. Open a Pull Request

---

## 📜 License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.

---

## 📧 Contact

**Author**: Sethy Rung
GitHub: [@SethyRung](https://github.com/SethyRung)
