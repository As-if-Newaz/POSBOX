# PosBox

An ASP .NET Core N-tier POS (Point Of Sale) web application.

## Getting Started

1. Clone the repository
2. Set up your environment variables for secrets (see [Managing Secrets](docs/SECRETS.md))
3. Restore NuGet packages: `dotnet restore`
4. Set up the database: `dotnet ef database update`
5. Run the application: `dotnet run --project PosBox.MVC`

## Project Structure

- **PosBox.MVC**: Web front-end and controllers
- **PosBox.BLL**: Business Logic Layer - services and DTOs
- **PosBox.DAL**: Data Access Layer - repositories and entity models

## Security

This application uses environment variables to store sensitive information like API keys and passwords. 
See [Managing Secrets](docs/SECRETS.md) for instructions on how to properly configure these secrets.
