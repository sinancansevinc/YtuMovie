using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Server;
using Server.Data;
using Server.Logging;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Logging
builder.Services.AddLogging(l =>
{
    l.ClearProviders()
    .SetMinimumLevel(LogLevel.Error)
    .AddProvider(new CustomLoggerProvider());

});


#endregion


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());
});



builder.Services.AddDbContext<MovieDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MovieDBContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddRouting(builder => builder.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CustomLogger logger = new CustomLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



app.Use(async (context, next) =>
{
    await next();


    if (context.Response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
    {
        logger.LogInformation("Unauthorized request");
    }
});


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllers();
});


app.Run();
