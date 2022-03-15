using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MoneyTrack.Core.AppServices;
using MoneyTrack.Core.AppServices.Automapper;
using MoneyTrack.Core.DomainServices;
using MoneyTrack.Data.MsSqlServer;
using MoneyTrack.Web.Api.Automapper;
using MoneyTrack.Web.Infrastructure;
using MoneyTrack.Web.Infrastructure.Settings;
using System.Net;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

var settings = builder.Configuration.Get<AppSettings>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Authorization.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Exception = context.Exception.ToString(),
                    StatusCode = HttpStatusCode.Unauthorized
                }));

                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                if (!context.Response.HasStarted)
                {
                    // Override the response status code.
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    // Emit the WWW-Authenticate header.
                    context.Response.Headers.Append(HeaderNames.WWWAuthenticate, context.Options.Challenge);

                    if (!string.IsNullOrEmpty(context.Error))
                    {
                        context.Response.WriteAsync(context.Error);
                    }

                    if (!string.IsNullOrEmpty(context.ErrorDescription))
                    {
                        context.Response.WriteAsync(context.ErrorDescription);
                    }
                }
                context.HandleResponse();
                return Task.FromResult(0);
            }
        };
    });

// DI Setup
builder.Services.AddInfrastructure(settings);
builder.Services.AddAppServices();
builder.Services.AddDomainServices();
builder.Services.AddMsSqlDb(builder.Configuration, settings);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new DomainModelsDtoMapperProfile());
    config.AddProfile(new DbToDomainProfile());
    config.AddProfile(new DtoToModelProfile());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.TryCreateDb();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
