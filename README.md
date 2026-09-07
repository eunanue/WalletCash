# WalletCashOut

API de cash-out: gestiona referencias de retiro, notificaciones/webhooks de estado y consulta de comercios, integrando con Dapp Wallets.

## Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 (SQL Server)
- Scalar (documentación OpenAPI interactiva, solo en Development)

## Estructura

```
Controllers/   Endpoints HTTP
Services/      Lógica de negocio y clientes HTTP hacia Dapp Wallets
Data/          DbContext, entidades y configuraciones de EF Core
DTOs/          Contratos de entrada/salida de la API
Migrations/    Migraciones de EF Core (esquema "cashout")
```

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/v2/cashout` | Lista transacciones de cash-out |
| POST | `/v2/cashout/references` | Crea una referencia de cash-out |
| GET | `/v2/cashout/references/{reference}` | Detalle de una referencia |
| DELETE | `/v2/cashout/references/{reference}` | Elimina/cancela una referencia |
| POST | `/v2/cashout/notifications` | Recibe notificaciones de estado de Dapp Wallets |
| POST | `/v2/cashout/webhooks` | Registra un webhook |
| PUT | `/v2/cashout/webhooks/{id}` | Actualiza un webhook |
| GET | `/v2/cashout/webhooks` | Lista webhooks registrados |
| GET | `/v2/stores` | Lista comercios |
| GET | `/v2/stores/{storeId}` | Detalle de un comercio |

## Configuración

Ver [`appsettings.json`](./appsettings.json):

- `ConnectionStrings:CashDb` — cadena de conexión a SQL Server (vacía en producción; en Development apunta al contenedor Docker, ver más abajo).
- `CashoutApi:BaseUrl` / `CashoutApi:ApiKey` — credenciales del cliente HTTP hacia Dapp Wallets.
- `CashoutWebhook:PublicKeyPath` — ruta a la llave pública usada para validar firmas de notificaciones/webhooks entrantes.

## Base de datos

Este proyecto comparte la base `WalletCashDb` con `WalletCashIn`, pero usa su propio esquema de migraciones (`cashout`), por lo que no colisiona con el otro proyecto.

Instrucciones completas para levantar SQL Server local (Docker) y aplicar migraciones: ver [`../SQLSERVER_SETUP.md`](../SQLSERVER_SETUP.md).

Resumen rápido:

```bash
docker compose -f ../docker-compose.sqlserver.yml up -d
dotnet ef database update --project . --context CashoutDbContext
```

## Ejecutar en local

```bash
dotnet run --project WalletCashOut
```

Con el entorno en `Development`, la documentación interactiva (Scalar) queda disponible en `/scalar` sobre la URL que imprima `dotnet run`.
