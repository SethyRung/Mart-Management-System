# Mart Management System

A desktop application for managing mart operations such as products, customers, suppliers, sales, and stock.  
Built with **C#** and **Microsoft SQL Server** for small to medium-sized retail businesses.

---

## 📌 Features

- **Product Management** – Add, update, delete, and search products.
- **Supplier Management** – Keep track of suppliers and their products.
- **Customer Management** – Manage customer records.
- **Inventory Control** – Monitor stock levels and prevent shortages.
- **Sales Processing** – Create and manage sales transactions.
- **Reporting** – Generate business and sales reports.
- **Database Stored Procedures** – Improve performance with pre-built SQL logic.

---

## 🛠 Tech Stack

- **Language**: C#
- **Framework**: .NET Framework / WinForms (adjust if WPF or other)
- **Database**: Microsoft SQL Server
- **Database Script**: `script.sql` (contains tables, relationships, stored procedures)

---

## ⚙️ Installation & Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/SethyRung/Mart-Management-System.git
   ```

2. **Set Up the Database**

   - Open **SQL Server Management Studio (SSMS)**.
   - Create a new database:

     ```sql
     CREATE DATABASE MartDB;
     ```

   - Open the `script.sql` file from this repo in SSMS.
   - Execute the script to create all tables, relationships, and stored procedures.

3. **Configure the Application**

   - Open the solution in **Visual Studio**.
   - Update the database connection string in the application configuration file (e.g., `App.config` or `Settings.settings`) to match your SQL Server instance.

4. **Run the Application**
   - Build and run the project in Visual Studio.
   - Log in with the default admin credentials (if applicable).

---

## 🤝 Contributing

1. Fork the repository.
2. Create a new branch:

   ```bash
   git checkout -b feature-name
   ```

3. Make your changes and commit:

   ```bash
   git commit -m "Add new feature"
   ```

4. Push the branch:

   ```bash
   git push origin feature-name
   ```

5. Open a Pull Request.

---

## 📜 License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.

---

## 📧 Contact

**Author**: Sethy Rung
GitHub: [@SethyRung](https://github.com/SethyRung)
