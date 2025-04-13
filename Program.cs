using Contactly.Data;
using Contactly.Extensions;
using Microsoft.EntityFrameworkCore;

using MySqlConnector;
using System.IO;

var certificatePath = Path.Combine(AppContext.BaseDirectory, "ssl", "DigiCertGlobalRootCA.crt.pem");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<ContactlyDbContext>();

//var _GetConnStringName = builder.Configuration.GetConnectionString("ConecctionMySQL");
//builder.Services.AddDbContextPool<ContactlyDbContext>(options => 
//options.UseMySql(_GetConnStringName, ServerVersion.AutoDetect(_GetConnStringName)));

// last var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


//builder.Services.AddDbContext<ContactlyDbContext>(options =>
//    options.UseMySql(
//        connectionString,
//        ServerVersion.AutoDetect(connectionString),
//        options => options.EnableRetryOnFailure()
//    )
//);
//last
//builder.Services.AddDbContext<ContactlyDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
//last

var connectionStringBuilder = new MySqlConnectionStringBuilder
{
    Server = "servidor-mysql.mysql.database.azure.com",
    Database = "bd",
    Port = 3306,
    UserID = "inicio0admin@servidor-mysql",
    Password = "TuPasswordFuerte",
    SslMode = MySqlSslMode.VerifyCA,
    CertificateFile = certificatePath
};

builder.Services.AddDbContext<ContactlyDbContext>(options =>
    options.UseMySql(connectionStringBuilder.ConnectionString, ServerVersion.AutoDetect(connectionStringBuilder.ConnectionString))
);


//builder.Services.AddDbContext<ContactlyDbContext>(options =>
//options.UseInMemoryDatabase("ContactsDb"));

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ContactlyDbContext>();
//    db.Database.Migrate();
//}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactlyDbContext>();
    db.Database.Migrate();
}


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//app.ApplyMigrations();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ContactlyDbContext>();

    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(3);
    var attempt = 0;

    while (attempt < maxRetries)
    {
        try
        {
            Console.WriteLine($"[Migration] Attempt {attempt + 1}...");
            context.Database.Migrate();
            Console.WriteLine("[Migration] Success!");
            break;
        }
        catch (Exception ex)
        {
            attempt++;
            Console.WriteLine($"[Migration] Failed: {ex.Message}");

            if (attempt >= maxRetries)
            {
                Console.WriteLine("[Migration] Giving up.");
                throw;
            }

            Thread.Sleep(delay);
        }
    }
}

//}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );

app.UseAuthorization();

app.MapControllers();

app.Run();
