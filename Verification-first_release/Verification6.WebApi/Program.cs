using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Verification.Repository.EfCore;
using Verification.SqlServer.Scripts;
using Verification.Tracker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.TrackerService(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region "JWT Token"
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime=true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetSection("Key").Value))
    };
    opt.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            if (string.IsNullOrEmpty(context.Error))
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.Headers.Add("WWW-Authenticate", "Bearer error=\"invalid_token\"");
                return Task.CompletedTask;
            }
            if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
            {
                var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o") ?? "01/01/2000");
                context.ErrorDescription = $"The token expired on {authenticationException.Expires:o}";
            }
            return Task.CompletedTask;
        }
    };
});

#endregion

builder.Services.AddControllers(options =>
{
   options.Filters.Add(typeof(Verification6.WebApi.Filters.ActionFilterAttribute_ForPerfomaceCalculation));
   options.Filters.Add(typeof(Verification6.WebApi.Filters.ExceptionFilterAttribute));
});

#region "Swegger"
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Verification.WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});
#endregion

//Connection for EfCore Framework and Repository Registration for EFCore
builder.Services.DataBaseConnections(builder.Configuration);

#region "Database Default Schema"
string _contentRootPath = builder.Environment.ContentRootPath;
ScriptDocumentLoad preload = new ScriptDocumentLoad(builder.Configuration);
preload.LoadXmlDoc(@$"{_contentRootPath}\DataBaseScript.xml");
#endregion

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Verification6.WebApi v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
