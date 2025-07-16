# CafeteriaOrderingSystem


An internal cafeteria ordering system built with ASP.NET Core, Entity Framework Core, and SQL Server. This web application allows employees to deposit funds, browse available restaurants, view menu items with images, and place orders.

## Features

- User registration and login (using ASP.NET Identity)
- Employee deposit functionality
- Browse restaurants with images and descriptions
- View and order from restaurant menus
- View order history ("My Orders")
- Admin management of employees, menu items, and restaurants

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Visual Studio 2022

## How to Run

1. Clone the repository:

2. 
2. Open the solution in **Visual Studio 2022**.

3. Ensure **SQL Server** is running and update `appsettings.json` with your connection string.

4. Apply migrations (if needed):

5. Run the project using IIS Express or `dotnet run`.

## Screenshots

Include screenshots of:
- Login/registration page
- Restaurant list with images
- Order placement
- Deposit screen

---

**Note:** This is a demo/internal app, not intended for production use without further security and validation improvements.

## Database

A backup of the SQL Server database (`CafeteriaDatabase.bak`) is included in the `/Database` folder. To restore it:

1. Open SQL Server Management Studio
2. Right-click "Databases" > Restore Database...
3. Choose "Device", browse to select the .bak file
4. Click OK to restore
