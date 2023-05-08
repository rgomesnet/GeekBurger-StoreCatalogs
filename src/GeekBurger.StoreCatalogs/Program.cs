using GeekBurger.StoreCatalogs.Application.GetProducts;
using GeekBurger.StoreCatalogs.Domain.Repositories;
using GeekBurger.StoreCatalogs.Infra.ClientServices;
using GeekBurger.StoreCatalogs.Infra.Repositores;
using GeekBurger.StoreCatalogs.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("ProductsAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://geekburgerproducts20230508203352.azurewebsites.net/");

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


builder.Services.AddTransient<Initialization>();

builder.Services.AddSingleton<IStoreRepository, StoreRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IGetProductService, GetProductService>();
builder.Services.AddScoped<IGetProductClientService, GetProductClientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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
