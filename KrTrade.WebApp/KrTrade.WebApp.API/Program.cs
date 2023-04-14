using KrTrade.WebApp.Relational.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// Add data base contexts
//builder.Services.AddDbContexts(builder.Configuration);

//builder.Services.AddDbContext<KrTradeDbContext>();
builder.Services.AddDbContext<KrTradeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkConnection"), options =>
    {
        options.MigrationsAssembly("KrTrade.WebApp.Relational");
    });
});

//// Add ApplicationDbContext to DI
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
//b.DataSource = @"DESKTOP-VFT7HDS\SQLEXPRESS";
//b.InitialCatalog = "KrTradeDB";
//b.IntegratedSecurity = true;
////b.TrustServerCertificate = true;
////b.UserID = "sa";
////b.Password = "KrumonTrade-20";

//var cs = b.ConnectionString;

//using (SqlConnection c = new SqlConnection(cs))
//{
//    try
//    {
//        c.Open();
//        string state = c.State.ToString();
//        var id = c.ClientConnectionId;
//        var dataBase = c.Database;
//        using (SqlCommand cmd = c.CreateCommand())
//        {
//            cmd.CommandText = 
//                "create table TablaEmpresa " + 
//                "(" +
//                    "IdEmpresa int identity(1,1) primary key," +
//                    "RucEmpresa varchar(13)," +
//                    "RazonSocialEmp varchar(90)," +
//                    "NombreComercialEmp varchar(90)," +
//                    "TelefonoEmp1 varchar(14)," +
//                    "TelefonoEmp2 varchar(14)," +
//                    "CorreoEmp varchar(50)," +
//                    "CiudadEmp varchar(25)," +
//                    "DireccionEmp varchar(150)," +
//                    "ActividadEconomica varchar(300)" + 
//                ")";
//            cmd.CommandType = CommandType.Text;
//            var rows = cmd.ExecuteNonQuery();
//        }

//    }
//    catch (Exception ex)
//    {

//        throw;
//    }
//    finally
//    {
//        c.Close();
////    }

//}

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetService<KrTradeDbContext>();
//    context?.Database.GetDbConnection().Open();


//    context?.Database.GetDbConnection().Close();
//}


app.Run();
