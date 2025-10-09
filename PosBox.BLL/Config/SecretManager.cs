using System;

namespace PosBox.BLL.Config
{
    public static class SecretManager
    {
        // Initialize environment variables - this should be called at application startup
        public static void LoadEnvironmentVariables(string? envFilePath = null)
        {
            try
            {
                // Load environment variables from .env file if path is provided
                if (!string.IsNullOrEmpty(envFilePath))
                {
                    DotNetEnv.Env.Load(envFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading environment variables: {ex.Message}");
            }
        }

        public static string JwtIssuer => Environment.GetEnvironmentVariable("JWTIssuer") ?? "Iforms";
        public static string JwtAudience => Environment.GetEnvironmentVariable("JWTAudience") ?? "Iforms";
        public static int JwtLifetime => int.TryParse(Environment.GetEnvironmentVariable("JWTLifetime"), out int lifetime) ? lifetime : 30;
        public static string JwtKey => Environment.GetEnvironmentVariable("JWTKey") ?? throw new InvalidOperationException("JWT Key is missing in environment variables");

        // SMTP Settings
        public static string SmtpServer => Environment.GetEnvironmentVariable("SMTPServer") ?? "smtp.gmail.com";
        public static int SmtpPort => int.TryParse(Environment.GetEnvironmentVariable("SMTPPort"), out int port) ? port : 587;
        public static string SmtpSenderEmail => Environment.GetEnvironmentVariable("SMTPSenderEmail") ?? throw new InvalidOperationException("SMTP Sender Email is missing in environment variables");
        public static string SmtpPassword => Environment.GetEnvironmentVariable("SMTPPassword") ?? throw new InvalidOperationException("SMTP Password is missing in environment variables");

        // Google API Settings
        public static string GoogleApiClientId => Environment.GetEnvironmentVariable("GOOGLE_API_ClientId") ?? throw new InvalidOperationException("Google API Client ID is missing in environment variables");
        public static string GoogleApiClientSecret => Environment.GetEnvironmentVariable("GOOGLE_API_ClientSecret") ?? throw new InvalidOperationException("Google API Client Secret is missing in environment variables");
        public static string GoogleApiRedirectUri => Environment.GetEnvironmentVariable("GOOGLE_API_RedirectUri") ?? throw new InvalidOperationException("Google API Redirect URI is missing in environment variables");
        public static string GoogleApiRefreshToken => Environment.GetEnvironmentVariable("GOOGLE_API_RefreshToken") ?? throw new InvalidOperationException("Google API Refresh Token is missing in environment variables");
    }
}