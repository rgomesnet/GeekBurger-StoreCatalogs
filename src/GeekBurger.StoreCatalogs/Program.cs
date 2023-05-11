using GeekBurger.StoreCatalogs.Application.GetProducts;
using GeekBurger.StoreCatalogs.Domain.Publishers;
using GeekBurger.StoreCatalogs.Domain.Repositories;
using GeekBurger.StoreCatalogs.Domain.Subscribers;
using GeekBurger.StoreCatalogs.Infra.ClientServices;
using GeekBurger.StoreCatalogs.Infra.Repositores;
using GeekBurger.StoreCatalogs.Infra.ServiceBusImpl;
using GeekBurger.StoreCatalogs.Middlewares;
using Microsoft.Azure.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("ProductsAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://geekburgerproducts20230508203352.azurewebsites.net/");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("IngredientssAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://b079-191-181-59-157.ngrok-free.app/");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StoreCatalogs",
        Version = "v1"
    });
});

builder.Services.AddHostedService<ServiceBusService<ProductChanged>>(ctx =>
{
    var connectionString = builder.Configuration.GetSection("serviceBus:connectionString").Value;
    var subscriptionClient = new SubscriptionClient(connectionString, "ProductChangedtopic", "product");
    return new ServiceBusService<ProductChanged>(ctx.GetService<IProductChangedHandler>()!, subscriptionClient);
});

builder.Services.AddHostedService<ServiceBusService<ProductionAreaChanged>>(ctx =>
{
    var connectionString = builder.Configuration.GetSection("serviceBus:connectionString").Value;
    var subscriptionClient = new SubscriptionClient(connectionString, "productionareachanged", "Production");
    return new ServiceBusService<ProductionAreaChanged>(ctx.GetService<IProductionAreChangedHandler>()!, subscriptionClient);
});

builder.Services.AddTransient<Initialization>();

builder.Services.AddSingleton<IStoreRepository, StoreRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductionRepository, ProductionRepository>();

builder.Services.AddScoped<IGetProductService, GetProductService>();
builder.Services.AddScoped<IGetProductClientService, GetProductClientService>();
builder.Services.AddScoped<IIngredientsClientService, IngredientsClientService>();

builder.Services.AddScoped<IProductReadyPublisher, ProductReadyPublisher>(ctx =>
{
    var connectionString = builder.Configuration.GetSection("serviceBus:connectionString").Value;
    var topicClient = new TopicClient(connectionString, "productReady");
    return new ProductReadyPublisher(topicClient);
});

builder.Services.AddScoped<IProductChangedHandler, ProductChangedHandler>();
builder.Services.AddScoped<IProductionAreChangedHandler, ProductionAreChangedHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var initialization = scope.ServiceProvider.GetService<Initialization>();
    await initialization!.RunAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();