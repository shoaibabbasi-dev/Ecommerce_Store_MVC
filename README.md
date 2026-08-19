<img width="1167" height="524" alt="image" src="https://github.com/user-attachments/assets/1c794ed1-de1a-4282-9e0c-81451c98a553" /># 🛒 E-Commerce MVC Store

A full-stack **E-Commerce Web Application** built with **C# and ASP.NET Core MVC**, featuring product management, shopping cart functionality, user authentication, order processing, and a dedicated admin dashboard.

The application follows the **Model-View-Controller (MVC)** architectural pattern and uses **Entity Framework Core with SQL Server** for data persistence.

---

## 🚀 Features

### 👤 Customer Features

* User registration and authentication
* Secure login/logout using ASP.NET Core Identity
* Browse available products
* View product details
* Add products to shopping cart
* Update product quantities
* Remove products from cart
* View cart totals
* Place orders
* View order-related information

### 🔐 Authentication & Authorization

* ASP.NET Core Identity integration
* User authentication and authorization
* Role-based access control
* Protected administrative functionality
* Dedicated administrator access

### 🛠️ Admin Dashboard

Authorized administrators have access to a dedicated dashboard for managing the store.

#### 📦 Product Management

Administrators can:

* View all products
* Add new products
* Edit existing products
* Manage product information
* Set products as **Active** or **Inactive**
* Control product availability

Products can be marked as inactive without permanently deleting them from the database.

#### 📋 Order Management

Administrators can:

* View customer orders
* View order details
* Update order status
* Mark orders as **Shipped**
* Mark orders as **Delivered**
* **Cancel** orders
* Manage other supported order states

### 🗄️ Database

* Microsoft SQL Server
* Entity Framework Core
* Database migrations
* Relational data management
* Persistent application data

---

## 🛠️ Technologies Used

| Technology                | Purpose                        |
| ------------------------- | ------------------------------ |
| **C#**                    | Application development        |
| **ASP.NET Core MVC**      | Web application framework      |
| **Entity Framework Core** | ORM and database access        |
| **SQL Server**            | Relational database            |
| **ASP.NET Core Identity** | Authentication & authorization |
| **Razor Views**           | Dynamic web UI                 |
| **HTML5**                 | Page structure                 |
| **CSS3**                  | Styling                        |
| **JavaScript**            | Client-side functionality      |
| **Visual Studio**         | Development environment        |

---

## 🏗️ Architecture

The application follows the **MVC architecture** with additional service and ViewModel layers.

```text
                         ┌────────────────────┐
                         │      Browser       │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │    Controllers     │
                         └─────────┬──────────┘
                                   │
                    ┌──────────────┼──────────────┐
                    │              │              │
                    ▼              ▼              ▼
             ┌────────────┐ ┌────────────┐ ┌────────────┐
             │  Services  │ │ ViewModels │ │   Models   │
             └─────┬──────┘ └────────────┘ └──────┬─────┘
                   │                              │
                   └──────────────┬───────────────┘
                                  ▼
                         ┌────────────────────┐
                         │ Entity Framework   │
                         │       Core         │
                         └─────────┬──────────┘
                                   │
                                   ▼
                         ┌────────────────────┐
                         │     SQL Server     │
                         └────────────────────┘
```

---

## 📁 Project Structure

```text
EcommerceMvcStore/
│
├── Areas/
│   └── Identity/
│       └── Pages/
│
├── Controllers/
├── Data/
├── Filters/
├── Migrations/
├── Models/
├── Properties/
├── Services/
├── ViewModels/
├── Views/
├── wwwroot/
│
├── .gitignore
├── EcommerceMvcStore.csproj
├── EcommerceMvcStore.sln
├── Program.cs
├── appsettings.json
└── README.md
```

### Directory Overview

| Directory         | Description                                      |
| ----------------- | ------------------------------------------------ |
| `Areas/Identity/` | Authentication and identity-related pages        |
| `Controllers/`    | Handles HTTP requests and application flow       |
| `Data/`           | Database context and data configuration          |
| `Filters/`        | Custom MVC filters                               |
| `Migrations/`     | Entity Framework Core database migrations        |
| `Models/`         | Domain and database models                       |
| `Services/`       | Application and business logic                   |
| `ViewModels/`     | UI-specific data models                          |
| `Views/`          | Razor views and user interface                   |
| `wwwroot/`        | Static files such as CSS, JavaScript, and images |

---

# 🔐 Authentication

The application uses **ASP.NET Core Identity** for authentication and authorization.

Identity-related functionality is located under:

```text
Areas/
└── Identity/
    └── Pages/
```

Authentication provides functionality such as:

* User registration
* User login
* User logout
* Authentication management
* Authorization
* Administrator access control

Administrative functionality is protected so that authorized administrators can access the Admin Dashboard.

---

# 🛠️ Admin Dashboard

The application includes a dedicated **Admin Dashboard** for managing products and customer orders.

## 📦 Product Management

Administrators can:

```text
Product Management
│
├── View Products
├── Add Product
├── Edit Product
├── Update Product Information
├── Activate Product
└── Deactivate Product
```

Products can be set to:

```text
Active
Inactive
```

This allows administrators to control product availability without permanently deleting products.

---

## 📋 Order Management

Administrators can manage customer orders throughout their lifecycle.

### Available Operations

* View all orders
* View order details
* Update order status
* Mark orders as **Shipped**
* Mark orders as **Delivered**
* Mark orders as **Cancelled**
* Manage other supported order states

### Order Lifecycle

A typical order flow is:

```text
Pending
   │
   ▼
Processing
   │
   ▼
Shipped
   │
   ▼
Delivered
```

Orders can also be moved to an appropriate state such as:

```text
Cancelled
```

---

# 👨‍💼 Admin Demo Account

The application includes a **hard-coded administrator account** for demonstration and testing purposes.

```text
Email:    admin@store.com
Password: Admin@12345
```

After logging in with these credentials, the **Admin Dashboard** becomes available.

### Admin Capabilities

* 📦 Manage products
* ➕ Add products
* ✏️ Edit products
* 🔄 Activate/deactivate products
* 📋 View customer orders
* 🔍 View order details
* 🔄 Update order status
* 🚚 Mark orders as shipped
* ✅ Mark orders as delivered
* ❌ Cancel orders

> ⚠️ **Security Notice:** The credentials above are hard-coded and are intended strictly for demonstration and local testing. They should **not be used in a production environment**. Production applications should use secure credential management and avoid committing administrator passwords to source control.

---

# 🛍️ Customer Shopping Flow

The general customer workflow is:

```text
User
 │
 ▼
Browse Products
 │
 ▼
View Product Details
 │
 ▼
Add Product to Cart
 │
 ▼
Review Shopping Cart
 │
 ▼
Place Order
 │
 ▼
Order Processing
 │
 ▼
Order Stored in SQL Server
```

---

# 🗃️ Database

The application uses **Microsoft SQL Server** as its relational database.

**Entity Framework Core** is used as the ORM for communication between the application and database.

### Entity Framework Core provides:

* Object-relational mapping
* CRUD operations
* Database relationships
* Change tracking
* Database migrations
* Database schema management

Database migrations are stored in:

```text
Migrations/
```

---

# ⚙️ Prerequisites

Before running the project, make sure you have:

* **.NET SDK** compatible with the project's target framework
* **SQL Server**
* **Visual Studio 2022** or another compatible IDE
* **Entity Framework Core tools** if using EF Core commands from the command line

---

# 🔧 Installation & Setup

## 1. Clone the Repository

```bash
git clone https://github.com/your-username/EcommerceMvcStore.git
cd EcommerceMvcStore
```

## 2. Restore Dependencies

```bash
dotnet restore
```

## 3. Configure SQL Server

Open:

```text
appsettings.json
```

Configure the SQL Server connection string according to your local environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=EcommerceMvcStore;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance.

For example:

```text
localhost
```

or:

```text
.\SQLEXPRESS
```

> The exact connection string depends on your SQL Server installation and authentication configuration.

## 4. Apply Database Migrations

Using the **Visual Studio Package Manager Console**:

```powershell
Update-Database
```

Or using the .NET CLI:

```bash
dotnet ef database update
```

This creates or updates the database using the existing Entity Framework Core migrations.

## 5. Build the Application

```bash
dotnet build
```

## 6. Run the Application

```bash
dotnet run
```

Alternatively, open:

```text
EcommerceMvcStore.sln
```

in Visual Studio and run the application using:

```text
F5
```

or:

```text
Debug → Start Without Debugging
```

---

# 📊 Core Application Components

### Controllers

Responsible for:

* Handling HTTP requests
* Processing user actions
* Coordinating application logic
* Returning appropriate views

### Models

Represent application entities and database data.

### ViewModels

Provide data specifically required by individual views and UI operations.

### Services

Encapsulate reusable application and business logic, keeping controllers cleaner and easier to maintain.

### Data Layer

Handles:

* Database context
* Entity Framework Core configuration
* Database access

### Views

Razor-based UI responsible for presenting application data to users.

---

# 🎯 Project Objectives

This project was developed to gain practical experience with:

* C# web application development
* ASP.NET Core MVC
* MVC architectural patterns
* Entity Framework Core
* SQL Server
* Database migrations
* Authentication and authorization
* Role-based access control
* CRUD operations
* Shopping cart functionality
* Product management
* Order management
* Service-layer architecture
* Razor Views
* Full-stack web application development

---

# 🔮 Future Improvements

Potential improvements include:

* 💳 Online payment gateway integration
* 🔎 Product search and filtering
* ⭐ Product reviews and ratings
* ❤️ Wishlist functionality
* 🏷️ Discount and coupon system
* 📊 Advanced admin analytics dashboard
* 📦 Advanced order tracking
* 📧 Order confirmation emails
* 🧪 Automated unit and integration testing
* 📱 Further responsive UI improvements
* 🔔 Order and inventory notifications

---

# 📸 Screenshots

## 🏠 Home Page

<img width="1365" height="605" alt="Home Page" src="https://github.com/user-attachments/assets/97889f1a-4f10-435b-b703-b0d82f16be52" />

## 📦 Product Details

<img width="1007" height="574" alt="Product Details" src="https://github.com/user-attachments/assets/410c3eac-cf16-49b9-9d54-71767764a01c" />

## 🛒 Shopping Cart

<img width="1030" height="213" alt="Shopping Cart" src="https://github.com/user-attachments/assets/522f2170-e3e0-4d5c-b8e1-04124d1d5089" />

## 📦 Product Management

<img width="972" height="588" alt="Product Management" src="https://github.com/user-attachments/assets/9c8ea10b-fd64-4732-bff6-c694251b24a3" />

## 📋 Order Management

<img width="1167" height="524" alt="image" src="https://github.com/user-attachments/assets/f28a4b3e-c457-4212-ae48-c2678be59659" />


---

# 🤝 Contributing

Contributions, suggestions, and improvements are welcome.

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Commit your changes
5. Push the branch
6. Open a Pull Request

Example:

```bash
git checkout -b feature/new-feature
git add .
git commit -m "Add new feature"
git push origin feature/new-feature
```

---

# 👨‍💻 Author

**Shoaib Ahmed Abbasi**

**BS Software Engineering Student**

### E-Commerce MVC Store

Built with:

**C# · ASP.NET Core MVC · Entity Framework Core · SQL Server · HTML · CSS · JavaScript**

---

⭐ **If you found this project useful, consider giving the repository a star!**
