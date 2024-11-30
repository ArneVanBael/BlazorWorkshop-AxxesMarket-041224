using AxxesMarket.Api.Domain;
using AxxesMarket.Api.Middleware;
using AxxesMarket.Api.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               // base-address of your identityserver
               options.Authority = "https://demo.duendesoftware.com";

               // audience is optional, make sure you read the following paragraphs
               // to understand your options
               options.TokenValidationParameters.ValidateAudience = false;
               options.TokenValidationParameters.NameClaimType = "name";
               options.TokenValidationParameters.RoleClaimType = "role";


               // it's recommended to check the type header to avoid "JWT confusion" attacks
               options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
           });
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
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
