using Library.Infraestructure.Common.Filters.Swagger;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.Middlewares;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Library.Infraestructure.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

SentrySdk.Init(options =>
{
    options.Dsn = BaseHelper.GetEnvVariable("PROJECT_SENTRY_KEY");
    options.Debug = true;
    options.SendDefaultPii = true;
    options.TracesSampleRate = 1.0f;
    options.ProfilesSampleRate = 1.0f;
});

var secretKey = BaseHelper.GetEnvVariable("AUTH_SECRET_KEY");
var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = BaseHelper.GetEnvVariable("PROJECT_DOMAIN"),
            ValidAudience = BaseHelper.GetEnvVariable("PROJECT_DOMAIN"),
            IssuerSigningKey = issuerSigningKey
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Error de autenticacion.");
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var projectName = BaseHelper.GetEnvVariable("PROJECT_NAME");
    var version = "v1";
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    int localhostPort = Convert.ToInt32(BaseHelper.GetEnvVariable("PROJECT_SERVICE_CUSTOMERS_PORT"));
    var proxyPath = "customers";

    c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
    c.OperationFilter<TagByApiExplorerSettingsOperationFilter>();
    c.OperationFilter<EndpointDocumentationOperationFilter>();
    c.DocumentFilter<BasePathDocumentFilter>(localhostPort, proxyPath);
    c.SwaggerDoc(version, new OpenApiInfo
    {
        Title = $"{projectName}.Services.Api.${proxyPath} {version}",
        Version = version,
        Description = $"Services.Api.${proxyPath}",
    });
    c.IncludeXmlComments(xmlPath);

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token en el formato: Bearer {TOKEN}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(BaseHelper.GetConnectionString()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();