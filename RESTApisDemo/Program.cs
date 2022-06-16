using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using RESTApisDemo.Authentication;
using RESTApisDemo.Authorization;
using RESTApisDemo.Business;
using RESTApisDemo.DataAccess;

using System.Text;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

// Dependency registration.
builder.Services.AddSingleton<IAuthenticationManager, AuthenticationManager> ();
builder.Services.AddSingleton<ICustomerDataAccess, CustomerDataAccess> ();
builder.Services.AddSingleton<ICustomerBizValidator, CustomerBizValidator> ();
builder.Services.AddSingleton<ICustomerBizFacade, CustomerBizFacade> ();

// Authentication (JSON Web Token based) related services
builder.Services
    .AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer (options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenParameter.JWT_ISSUER,
                ValidAudience = TokenParameter.JWT_AUDIENCE,
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (TokenParameter.JWT_ENCRYPTION_KEY)),
                ClockSkew = TimeSpan.Zero
            };
        }
    );

// Authorization (Role-based Access Control) related services
builder.Services.AddAuthorization
    (
        config =>
        {
            config.AddPolicy (PolicyHub.ADMIN_POLICY, PolicyHub.GetAdminPolicy ());
            config.AddPolicy (PolicyHub.REGULAR_POLICY, PolicyHub.GetRegularPolicy ());
        }
    );

// API controllers.
builder.Services.AddControllers ();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
    app.UseDeveloperExceptionPage ();
}

app.UseHttpsRedirection ();
app.UseAuthentication ();
app.UseAuthorization ();
app.MapControllers ();

app.Run ();