



var builder = WebApplication.CreateBuilder(args);

// Make connection string available for DI
builder.Services.AddSingleton(sp => builder.Configuration.GetConnectionString("DefaultConnection"));


// Add services to the container.
builder.Services.AddScoped<Backend.Services.PatientService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
});



// Rate limiting temporarily disabled due to .NET 9 incompatibility
// builder.Services.AddMemoryCache();
// builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
// builder.Services.AddSingleton<AspNetCoreRateLimit.IRateLimitConfiguration, AspNetCoreRateLimit.RateLimitConfiguration>();
// builder.Services.AddInMemoryRateLimiting();




// JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false // For demo only; set to true and provide key in production
        };
    });

// Role-based Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
});

var app = builder.Build();

// Enable Swagger UI and OpenAPI endpoints for all environments
app.UseSwagger();
app.UseSwaggerUI();



// app.UseIpRateLimiting(); // Disabled for .NET 9 compatibility



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
