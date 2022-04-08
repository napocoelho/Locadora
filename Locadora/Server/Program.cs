//using Dapper;
using Data.Contexts;
using Data.Repositories;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Data.Contexts;
using Locadora.Services.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);
var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();




void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddSwaggerGen();

    //------------------

    string connString = $"Server=localhost;Port=3306;Database=locadora;Uid=root;Pwd=jurubeba;";

    // Testando conexão:
    //MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
    //IEnumerable<Cliente> clientes = conn.Query<Cliente>($"select * from cliente");

    


    builder.Services.AddDbContext<ClienteContext>(context => context.UseMySQL(connString)); //, ServerVersion.AutoDetect(connString)));
    builder.Services.AddDbContext<FilmeContext>(context => context.UseMySQL(connString)); //, ServerVersion.AutoDetect(connString)));
    builder.Services.AddDbContext<LocacaoContext>(context => context.UseMySQL(connString)); //, ServerVersion.AutoDetect(connString)));
    builder.Services.AddDbContext<RelatorioContext>(context => context.UseMySQL(connString)); //, ServerVersion.AutoDetect(connString)));


    builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
    builder.Services.AddTransient<IFilmeRepository, FilmeRepository>();
    builder.Services.AddTransient<ILocacaoRepository, LocacaoRepository>();


    builder.Services.AddTransient<IClienteService, ClienteService>();
    builder.Services.AddTransient<IFilmeService, FilmeService>();
    builder.Services.AddTransient<ILocacaoService, LocacaoService>();


    builder.Services.AddTransient<IRelatoriosXlsService, RelatoriosXlsService>();





}