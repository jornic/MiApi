using MiAPI.Data.Interfaz;
using MiAPI.Data.Repository;
using MiAPI.Models;
using MiAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Dependencias
builder.Services.AddScoped<IInsert<MfirstRegister>,RFirstRegister>();
builder.Services.AddScoped<ICrud<MPerson>,RPerson>();
builder.Services.AddScoped<ILogInUsers, RLogin>();
builder.Services.AddScoped<IAuthotizationServices,AuthorizationServices>();
#endregion

builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(Doc =>
{
    Doc.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = builder.Configuration["APISettingsSwagger:Description"],
        Title = builder.Configuration["APISettingsSwagger:Title"],
        Version = builder.Configuration["APISettingsSwagger:Version"]
    });

    Doc.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MiAPI.xml"));

    Doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Ingrese el token JWT generado en Auth/Login"
    });

    Doc.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region Habilitar los Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
    builder => builder.WithOrigins("http://localhost:5500")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
});
#endregion

#region Json Web Token
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtKey"]);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true, 
                ClockSkew = TimeSpan.Zero
            };
        });
#endregion


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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
