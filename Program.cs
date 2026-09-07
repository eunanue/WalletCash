using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WalletCash.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<WalletCash.Services.DappAuthHttpMessageHandler>();

builder.Services.AddHttpClient("CashoutApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CashoutApi:BaseUrl"]
        ?? "https://wallets-sandbox.dapp.mx/");
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<WalletCash.Services.DappAuthHttpMessageHandler>();
builder.Services.AddSingleton<WalletCash.Services.ICashoutSignatureValidator, WalletCash.Services.CashoutSignatureValidator>();

var cashDbConnectionString = builder.Configuration.GetConnectionString("CashDb");
if (string.IsNullOrWhiteSpace(cashDbConnectionString))
{
    cashDbConnectionString = "Server=localhost;Database=WalletCashDb;Trusted_Connection=True;TrustServerCertificate=True;";
}

builder.Services.AddDbContext<CashoutDbContext>(options =>
    options.UseSqlServer(
        cashDbConnectionString,
        sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "cashout")));

builder.Services.AddScoped<WalletCash.Services.ICashoutReferenceRepository, WalletCash.Services.CashoutReferenceRepository>();
builder.Services.AddScoped<WalletCash.Services.ICashoutNotificationProcessor, WalletCash.Services.CashoutNotificationProcessor>();

builder.Services.AddScoped<WalletCash.Services.ICashoutReferencesService, WalletCash.Services.CashoutReferencesService>();
builder.Services.AddScoped<WalletCash.Services.ICashoutWebhookService, WalletCash.Services.CashoutWebhookService>();
builder.Services.AddScoped<WalletCash.Services.ICashoutWebhooksService, WalletCash.Services.CashoutWebhooksService>();
builder.Services.AddScoped<WalletCash.Services.ICashoutService, WalletCash.Services.CashoutService>();
builder.Services.AddScoped<WalletCash.Services.IStoresService, WalletCash.Services.StoresService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
