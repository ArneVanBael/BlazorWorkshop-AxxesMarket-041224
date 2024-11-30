using AxxesMarket.Api.Domain;
using AxxesMarket.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AxxesMarketContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// ADD AUTHENTICATION HERE

var app = builder.Build();

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetService<AxxesMarketContext>();
    context.Database.EnsureCreated();

    var products = await context.Products.CountAsync();
    if(products == 0)
    {
        // add test product
        var product = Product.Create(new Guid(), "test product", "test beschrijving", "test detail beschrijving", new DateTime(2020,1,1), true, 50, null, "https://picsum.photos/1600/900", "Bob Smith");
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADD AUTHENTICATION HERE
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
