# TestTaskApiFB
##  1. Обзор проекта
- Проект реализует REST‑API для управления заказами и их элементами с использованием ASP.NET Core Web API, Entity Framework Core и AutoMapper. Архитектура разделена на несколько слоёв, что соответствует принципам чистой архитектуры и инверсии зависимостей:

- TestApi (Web API) – контроллеры и настройка DI, Swagger для документации.

- TestApi.Bll (Бизнес‑логика) – сервисы для работы с заказами и элементами заказа, включая проверки предметной области (например, уникальность номера заказа для поставщика).

- TestApi.Domain (Сущности и интерфейсы) – модели (Entities), DTO и интерфейсы для сервисов и репозиториев.

- TestApi.Infrastructure (Доступ к данным) – реализация репозиториев на базе Entity Framework Core, настройка DbContext, конфигурации сущностей и миграции.

## 2. Подключение к базе данных
Подключение к PostgreSQL настраивается через строку подключения, которая задаётся в файле appsettings.json (обычно этот файл добавляется в .gitignore):


```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TestApiDb;Username=YOURNAME;Password=YOURPASSWORD"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
В проекте TestApi.Infrastructure строка подключения используется в классе TestApiDbContext.

## 3. Основные зависимости и технологии
 - ASP.NET Core Web API – построение REST‑сервисов.

 - Entity Framework Core – ORM для работы с PostgreSQL (используется пакет Npgsql.EntityFrameworkCore.PostgreSQL).

 - AutoMapper – для маппинга между DTO и сущностями.

 - Swashbuckle.AspNetCore – для генерации Swagger‑документации.

 - LinqKit.Microsoft.EntityFrameworkCore – для расширенной поддержки динамических запросов (при необходимости).

## 4. Особенности и потенциальные нюансы
 - Миграции и база данных:
Миграции находятся в проекте TestApi.Infrastructure. Перед запуском приложения необходимо выполнить команду миграции, чтобы создать структуру БД.
Замечание: строка подключения хранится в appsettings.json и, как правило, исключается из системы контроля версий для защиты конфиденциальных данных.

 - Инверсия зависимостей (DI):
Сервисы бизнес‑логики (из TestApi.Bll) и репозитории (из TestApi.Infrastructure) регистрируются в DI контейнере ASP.NET Core. Это позволяет легко подменять реализации и тестировать отдельные компоненты.

 - DTO и AutoMapper:
AutoMapper используется для преобразования между сущностями и DTO, что облегчает разделение слоёв и поддержку чистоты архитектуры.

 - Swagger‑документация:
Swagger (Swashbuckle) позволяет просматривать и тестировать API через веб-интерфейс, что удобно для проверки эндпоинтов.

 - Проверки предметной области:
На уровне бизнес‑логики реализованы проверки (например, уникальность номера заказа для конкретного поставщика и запрет на совпадение OrderItem.Name с Order.Number).

- ## 5. Сборка и запуск
- Сборка проекта:
Все проекты используют .NET 8.0 и настроены через соответствующий SDK (например, Microsoft.NET.Sdk.Web для веб‑проекта и Microsoft.NET.Sdk для остальных).

- Запуск API:
Запустите проект TestApi. API будет доступно по адресу, указанному в настройках (например, https://localhost:7204). Swagger‑документация доступна по пути /swagger/index.html.

