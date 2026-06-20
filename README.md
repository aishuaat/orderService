# OrderService

Small ASP.NET Core Web API project for working with orders.

I made it for practice with:

- ASP.NET Core
- REST API
- Entity Framework Core
- PostgreSQL

## What it can do

The API has basic CRUD methods for orders:

- get all orders
- get one order by id
- create order
- update order
- delete order

## Order model

Order has these fields:

- Id
- CustomerName
- ProductName
- Quantity
- Price

## How to run

Create database in PostgreSQL:

```sql
CREATE DATABASE orders_db;
```

Check connection string in `appsettings.json`.

Then run migrations:

```bash
dotnet ef database update
```

Run project:

```bash
dotnet run
```

## Endpoints

```http
GET /api/orders
GET /api/orders/1
POST /api/orders
PUT /api/orders/1
DELETE /api/orders/1
```

Example body for POST:

```json
{
  "customerName": "Ivan Petrov",
  "productName": "Laptop",
  "quantity": 1,
  "price": 1500.50
}
```
