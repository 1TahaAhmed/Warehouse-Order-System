# Warehouse & Order Management System

A robust, enterprise-inspired backend console application built using **C#**, **.NET**, and **Entity Framework Core (SQL Server)**. This project demonstrates clean domain modeling, database relationships, caching strategies, and business-logic validation.

## 🚀 Key Features & Architecture

### 1. Advanced EF Core Mapping
* **Table-Per-Hierarchy (TPH) Inheritance:** Polymorphic modeling of products (`PhysicalProduct` and `DigitalProduct`) mapped elegantly to a single database table using discriminators.
* **Complex Schema Design:** Includes a `One-to-One` relationship (`Customer` ↔ `CustomerProfile`), and `Many-to-Many` relational structures with customized composite keys.

### 2. High-Performance In-Memory Caching
* Built a custom `ProductCacheService` utilizing optimized dictionary lookup strategies.
* Accelerates read operations down to **O(1) time complexity**, significantly reducing redundant database trips during high-traffic checkout scenarios.

### 3. Queue-Based Shipping Dispatcher
* Implemented an `OrderDispatcher` relying on a thread-safe sequential `Queue<Order>` data structure.
* Decouples order placement from fulfillment, ensuring orderly FIFO (First-In, First-Out) processing and address resolution.

---

## 🛠️ Tech Stack
* **Language:** C# 12
* **Framework:** .NET 8 / EF Core (Relational SQL Server Provider)
* **Database:** Microsoft SQL Server
* **Design Patterns:** Repository-like caching, Decoupled Dispatcher, Polymorphism.

---

## 🚦 Getting Started

### Prerequisites
* .NET SDK 8.x
* MS SQL Server LocalDB or Express instance

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/1TahaAhmed/Warehouse-Order-System.git](https://github.com/1TahaAhmed/Warehouse-Order-System.git)
