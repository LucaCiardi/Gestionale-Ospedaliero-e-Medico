using Gestionale_Ospedaliero_e_Medico.Data;
using Gestionale_Ospedaliero_e_Medico.Services.Implementations;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database context
builder.Services.AddDbContext<OspedaleDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IOspedaleService, OspedaleService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPazienteService, PazienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing configuration
app.UseRouting();  // UseRouting deve essere prima di UseEndpoints
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Aggiungi UseEndpoints per assicurare il corretto funzionamento del routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
