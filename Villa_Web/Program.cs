using Microsoft.AspNetCore.Authentication.Cookies;
using Villa_Web.Mapper;
using Villa_Web.Services;
using Villa_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IVillaService, VillaService>();
builder.Services.AddScoped<IVillaService, VillaService>();

builder.Services.AddHttpClient<INumeroVillaService, NumeroVillaService>();
builder.Services.AddScoped<INumeroVillaService, NumeroVillaService>();

builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//Para gestionar sesiones 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Acceder al token desde la interfaz web



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie( options =>
                                                            {
                                                                options.Cookie.HttpOnly = true;
                                                                options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
                                                                options.LoginPath = "/Usuario/Login";
                                                                options.AccessDeniedPath = "/Usuario/AccesoDenegado";
                                                                options.SlidingExpiration = true;
                                                            });  



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // para que el proyecto maneje sesiones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
