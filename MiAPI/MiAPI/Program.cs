using MiAPI.Data.Interfaz;
using MiAPI.Data.Repository;
using MiAPI.Models;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IInsert<MfirstRegister>,RFirstRegister>();
builder.Services.AddScoped<ICrud<MPerson>,RPerson>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Doc =>
{
    Doc.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = builder.Configuration["APISettingsSwagger:Description"],
        Title = builder.Configuration["APISettingsSwagger:Title"],
        Version = builder.Configuration["APISettingsSwagger:Version"]
    });

    Doc.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MiAPI.xml"));

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
    builder => builder.WithOrigins("http://localhost:5500")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages();
app.UseCors("AllowLocalhost");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
