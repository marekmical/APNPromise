using System.Text;
using APNPromise.Configuration;
using APNPromise.Models;
using APNPromise.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            IgnoreTrailingSlashWhenValidatingAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BearerTokenOptions.SigningKey)),
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            RequireAudience = true,
            RequireSignedTokens = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidAudience = BearerTokenOptions.Audience,
            ValidIssuer = BearerTokenOptions.Issuer
        };
        o.Authority = "http://localhost:7205";
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
    });

builder.Services.AddDbContext<BooksContext>(opt =>
    opt.UseInMemoryDatabase("BooksList"));
builder.Services.AddDbContext<OrdersContext>(opt =>
    opt.UseInMemoryDatabase("OrdersList"));

builder.Services.AddScoped<BearerTokenService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
