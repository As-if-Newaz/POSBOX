using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PosBox.BLL.Config;
using PosBox.BLL.Services;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework;
using PosBox.DAL.Interfaces;
using System.Text;
using System.IO;

// Load environment variables from Secrets.env if it exists
// SECURITY NOTE: The Secrets.env file should NEVER be committed to source control
// See docs/SECRETS.md for proper secret management instructions
string secretsPath = Path.Combine(Directory.GetCurrentDirectory(), "Secrets.env");
if (File.Exists(secretsPath))
{
    SecretManager.LoadEnvironmentVariables(secretsPath);
}
else
{
    Console.WriteLine("WARNING: Secrets.env file not found. Using environment variables or defaults.");
    Console.WriteLine("See docs/SECRETS.md for instructions on setting up secrets.");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<IApplicationDBContext, ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = SecretManager.JwtIssuer,
            ValidAudience = SecretManager.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretManager.JwtKey))
        };
    });

// Configure SMTP settings from environment variables
builder.Services.Configure<SmtpSettings>(options => {
    options.Server = SecretManager.SmtpServer;
    options.Port = SecretManager.SmtpPort;
    options.SenderEmail = SecretManager.SmtpSenderEmail;
    options.Password = SecretManager.SmtpPassword;
});
builder.Services.AddScoped<DataAccess>();
builder.Services.AddScoped<AuditLogService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DailyReportService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<EntryInvoiceService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<QuickSellService>();
builder.Services.AddScoped<SellInvoiceService>();
builder.Services.AddScoped<SellService>();
builder.Services.AddScoped<StockDiscardApplicationService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<TransferService>();
builder.Services.AddScoped<TransferInvoiceService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DriveUploadService>();
builder.Services.AddHttpClient();

// Add HttpContextAccessor service for accessing HttpContext in views
builder.Services.AddHttpContextAccessor();

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

// Add the custom middleware to extract JWT token from cookie
app.UseMiddleware<PosBox.MVC.Middleware.JwtCookieAuthenticationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// API health check endpoint
app.MapGet("/api/health", () => "API is healthy");

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Explicit route for the login page
app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "Auth", action = "Login" });

app.Run();
