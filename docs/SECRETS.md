# Managing Secrets in PosBox

This document describes how to properly manage sensitive information like API keys, passwords, and other secrets in the PosBox application.

## 1. Environment File (Development)

For local development, the recommended approach is to use the `Secrets.env` file:

1. Copy the `Secrets.env.template` file from the root directory to `PosBox.MVC/Secrets.env`
2. Fill in your actual secrets in the `Secrets.env` file
3. The application will automatically load these values at startup

**IMPORTANT: Never commit your `Secrets.env` file to source control!** It is included in `.gitignore` to prevent accidental commits.

## 2. Environment Variables (Production)

### Setting Environment Variables

#### Windows (PowerShell)

```powershell
# Set environment variables for the current session
$env:POSBOX_Jwt__Key = "YOUR_JWT_KEY_HERE"
$env:POSBOX_SmtpSettings__Password = "YOUR_SMTP_PASSWORD_HERE"
$env:POSBOX_GoogleApiSettings__ClientId = "YOUR_GOOGLE_CLIENT_ID_HERE"
$env:POSBOX_GoogleApiSettings__ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET_HERE"
$env:POSBOX_GoogleApiSettings__RefreshToken = "YOUR_GOOGLE_REFRESH_TOKEN_HERE"

# To set permanently for the user (run as administrator)
[System.Environment]::SetEnvironmentVariable("POSBOX_Jwt__Key", "YOUR_JWT_KEY_HERE", "User")
[System.Environment]::SetEnvironmentVariable("POSBOX_SmtpSettings__Password", "YOUR_SMTP_PASSWORD_HERE", "User")
[System.Environment]::SetEnvironmentVariable("POSBOX_GoogleApiSettings__ClientId", "YOUR_GOOGLE_CLIENT_ID_HERE", "User")
[System.Environment]::SetEnvironmentVariable("POSBOX_GoogleApiSettings__ClientSecret", "YOUR_GOOGLE_CLIENT_SECRET_HERE", "User")
[System.Environment]::SetEnvironmentVariable("POSBOX_GoogleApiSettings__RefreshToken", "YOUR_GOOGLE_REFRESH_TOKEN_HERE", "User")
```

#### macOS/Linux (Bash)

```bash
# Set environment variables for the current session
export POSBOX_Jwt__Key="YOUR_JWT_KEY_HERE"
export POSBOX_SmtpSettings__Password="YOUR_SMTP_PASSWORD_HERE"
export POSBOX_GoogleApiSettings__ClientId="YOUR_GOOGLE_CLIENT_ID_HERE"
export POSBOX_GoogleApiSettings__ClientSecret="YOUR_GOOGLE_CLIENT_SECRET_HERE"
export POSBOX_GoogleApiSettings__RefreshToken="YOUR_GOOGLE_REFRESH_TOKEN_HERE"

# To set permanently, add to ~/.bashrc or ~/.zshrc
echo 'export POSBOX_Jwt__Key="YOUR_JWT_KEY_HERE"' >> ~/.bashrc
echo 'export POSBOX_SmtpSettings__Password="YOUR_SMTP_PASSWORD_HERE"' >> ~/.bashrc
# ... and so on for other secrets
```

### Environment Variable Naming Convention

- Replace `:` in configuration keys with `__` (double underscore)
- Add the `POSBOX_` prefix to all environment variables

Examples:
- `Jwt:Key` becomes `POSBOX_Jwt__Key`
- `SmtpSettings:Password` becomes `POSBOX_SmtpSettings__Password`

## 2. User Secrets (Development Only)

For development environments, you can use the .NET User Secrets feature:

```powershell
# Navigate to the project directory
cd path\to\PosBox.MVC

# Initialize user secrets
dotnet user-secrets init

# Set secrets
dotnet user-secrets set "Jwt:Key" "YOUR_JWT_KEY_HERE"
dotnet user-secrets set "SmtpSettings:Password" "YOUR_SMTP_PASSWORD_HERE"
dotnet user-secrets set "GoogleApiSettings:ClientId" "YOUR_GOOGLE_CLIENT_ID_HERE"
dotnet user-secrets set "GoogleApiSettings:ClientSecret" "YOUR_GOOGLE_CLIENT_SECRET_HERE"
dotnet user-secrets set "GoogleApiSettings:RefreshToken" "YOUR_GOOGLE_REFRESH_TOKEN_HERE"

# List all secrets
dotnet user-secrets list
```

User secrets are stored in your user profile, not in the project directory, so they won't be committed to source control.

## 3. Required Secrets

The following secrets must be configured for the application to work properly:

### JWT Authentication
- `Jwt:Key` - Secret key for JWT token generation
- `Jwt:Issuer` - Token issuer
- `Jwt:Audience` - Token audience

### SMTP Email Settings
- `SmtpSettings:Server` - SMTP server address
- `SmtpSettings:Port` - SMTP server port
- `SmtpSettings:SenderEmail` - Email address to send from
- `SmtpSettings:Password` - Password or app-specific password for the email account

### Google API Settings
- `GoogleApiSettings:ClientId` - Google OAuth client ID
- `GoogleApiSettings:ClientSecret` - Google OAuth client secret
- `GoogleApiSettings:RedirectUri` - OAuth redirect URI
- `GoogleApiSettings:RefreshToken` - OAuth refresh token

## Security Best Practices

1. Never commit secrets or API keys to source control
2. Use environment variables or user secrets for local development
3. Use secure secret management in production (Azure Key Vault, AWS Secrets Manager, etc.)
4. Rotate credentials regularly
5. Use the principle of least privilege when creating API keys and credentials