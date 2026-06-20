# Order Service

Простой учебный ASP.NET Core REST API для управления заказами.

## Возможности

- `GET /api/orders` - получить список заказов.
- `GET /api/orders/{id}` - получить заказ по идентификатору.
- `POST /api/orders` - создать заказ.
- `PUT /api/orders/{id}` - обновить заказ.
- `DELETE /api/orders/{id}` - удалить заказ.
- Entity Framework Core + PostgreSQL.
- Начальная миграция для таблицы `orders`.

## Запуск

1. Создайте PostgreSQL базу `orders_db`.
2. При необходимости измените строку подключения в `appsettings.json`.
3. Примените миграции:

```bash
dotnet ef database update
```

4. Запустите сервис:

```bash
dotnet run
```

## Пример создания заказа

```http
POST /api/orders
Content-Type: application/json

{
  "customerName": "Ivan Petrov",
  "productName": "Laptop",
  "quantity": 1,
  "price": 1500.50
}
```
