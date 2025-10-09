using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Drive.v3.Data;
using PosBox.BLL.Config;
using IOFile = System.IO.File;

namespace PosBox.BLL.Services
{
    /// <summary>
    /// A simplified Google Drive upload service using the official Google.Apis.Drive.v3 library
    /// Now using environment variables from Secrets.env via SecretManager
    /// </summary>
    public class DriveUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly DriveService _driveService;
        
        /// <summary>
        /// Creates a new instance of DriveUploadServiceSimple
        /// </summary>
        /// <param name="configuration">Application configuration (retained for backward compatibility)</param>
        public DriveUploadService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
            // Initialize the Drive service using OAuth 2.0 credentials
            var driveService = CreateDriveService();
            
            if (driveService == null)
            {
                throw new InvalidOperationException("Failed to initialize Google Drive service. Check your credentials.");
            }
            
            _driveService = driveService;
        }
        
        /// <summary>
        /// Creates the Drive API service using the refresh token flow
        /// </summary>
        private DriveService? CreateDriveService()
        {
            try
            {
                // Get Google API credentials from environment variables via SecretManager
                var clientId = SecretManager.GoogleApiClientId;
                var clientSecret = SecretManager.GoogleApiClientSecret;
                var refreshToken = SecretManager.GoogleApiRefreshToken;
                
                if (string.IsNullOrEmpty(clientId) || 
                    string.IsNullOrEmpty(clientSecret) || 
                    string.IsNullOrEmpty(refreshToken))
                {
                    Console.WriteLine("Missing Google API credentials in environment variables.");
                    return null;
                }
                
                // Create OAuth 2.0 credential with refresh token
                var secrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };
                
                // Create a custom authorization code flow
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = secrets
                });
                
                // Create the credential
                var credential = new UserCredential(flow, "user", new Google.Apis.Auth.OAuth2.Responses.TokenResponse
                {
                    RefreshToken = refreshToken
                });
                
                // Create Drive API service
                return new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "PosBox Drive Upload Service"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Drive service: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return null;
            }
        }
        
        /// <summary>
        /// Result containing file ID and public view link
        /// </summary>
        public class FileUploadResult
        {
            public string FileId { get; set; } = string.Empty;
            public string? ViewableLink { get; set; }
            
            public FileUploadResult(string fileId, string? viewableLink)
            {
                FileId = fileId;
                ViewableLink = viewableLink;
            }
        }
        
        /// <summary>
        /// Gets or creates a folder in Google Drive
        /// </summary>
        /// <param name="folderName">Name of the folder</param>
        /// <param name="parentFolderId">Optional parent folder ID</param>
        /// <returns>Folder ID if successful, null otherwise</returns>
        public async Task<string?> GetOrCreateFolder(string folderName, string? parentFolderId = null)
        {
            try
            {
                // First, try to find if the folder already exists
                string? folderId = await FindFolderByName(folderName, parentFolderId);
                if (!string.IsNullOrEmpty(folderId))
                {
                    Console.WriteLine($"Folder '{folderName}' already exists with ID: {folderId}");
                    return folderId;
                }
                
                Console.WriteLine($"Creating folder '{folderName}'...");
                
                // Create metadata for a new folder
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                
                // Set parent folder if specified
                if (!string.IsNullOrEmpty(parentFolderId))
                {
                    fileMetadata.Parents = new[] { parentFolderId };
                }
                
                // Create the folder
                var request = _driveService.Files.Create(fileMetadata);
                request.Fields = "id";
                var folder = await request.ExecuteAsync();
                
                Console.WriteLine($"Folder '{folderName}' created with ID: {folder.Id}");
                
                return folder.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating folder '{folderName}': {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Finds a folder by name in Google Drive
        /// </summary>
        /// <param name="folderName">Name of the folder</param>
        /// <param name="parentFolderId">Optional parent folder ID</param>
        /// <returns>Folder ID if found, null otherwise</returns>
        private async Task<string?> FindFolderByName(string folderName, string? parentFolderId = null)
        {
            try
            {
                // Build query
                string query = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
                
                // Add parent folder condition if specified
                if (!string.IsNullOrEmpty(parentFolderId))
                {
                    query += $" and '{parentFolderId}' in parents";
                }
                
                // Create search request
                var request = _driveService.Files.List();
                request.Q = query;
                request.Fields = "files(id, name)";
                
                // Execute request
                var result = await request.ExecuteAsync();
                
                if (result.Files != null && result.Files.Count > 0)
                {
                    return result.Files[0].Id;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding folder '{folderName}': {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Upload an image file to Google Drive
        /// </summary>
        /// <param name="filePath">Path to the file to upload</param>
        /// <param name="fileName">Name to give the file in Google Drive</param>
        /// <param name="description">Optional description</param>
        /// <param name="folderName">Optional folder name to upload to</param>
        /// <param name="folderId">Optional folder ID to upload to (overrides folderName)</param>
        /// <returns>FileUploadResult containing FileId and ViewableLink if successful, null otherwise</returns>
        public async Task<FileUploadResult?> UploadImg(string filePath, string fileName, string? description = null, string? folderName = null, string? folderId = null)
        {
            try
            {
                Console.WriteLine($"Starting upload: {filePath} as {fileName}");
                
                // Validate file exists
                if (!IOFile.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    throw new FileNotFoundException($"File not found: {filePath}");
                }
                
                // Validate file is an image
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" };
                
                if (!Array.Exists(allowedExtensions, ext => ext == extension))
                {
                    throw new ArgumentException($"Invalid image format: {extension}. Allowed formats: {string.Join(", ", allowedExtensions)}");
                }
                
                // Determine MIME type from file extension
                string mimeType = GetMimeTypeFromExtension(extension);
                
                // If folder name is provided but folder ID is not, get or create the folder
                if (!string.IsNullOrEmpty(folderName) && string.IsNullOrEmpty(folderId))
                {
                    folderId = await GetOrCreateFolder(folderName);
                    if (string.IsNullOrEmpty(folderId))
                    {
                        throw new InvalidOperationException($"Failed to create or find folder: {folderName}");
                    }
                }
                
                // Create file metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    Description = description
                };
                
                // Set parent folder if specified
                if (!string.IsNullOrEmpty(folderId))
                {
                    fileMetadata.Parents = new[] { folderId };
                }
                
                // Create the file upload request
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var request = _driveService.Files.Create(fileMetadata, stream, mimeType);
                    request.Fields = "id";
                    
                    // Upload the file
                    var upload = await request.UploadAsync();
                    
                    if (upload.Status == UploadStatus.Failed)
                    {
                        Console.WriteLine($"Upload failed: {upload.Exception?.Message}");
                        return null;
                    }
                    
                    // Get the created file
                    var file = request.ResponseBody;
                    Console.WriteLine($"File uploaded successfully with ID: {file.Id}");
                    
                    // Make the file publicly accessible
                    await MakeFilePublic(file.Id);
                    
                    // Get the file with webViewLink field included
                    var fileWithLink = await GetFileInfo(file.Id);
                    
                    return new FileUploadResult(file.Id, fileWithLink?.WebViewLink);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return null;
            }
        }
        
        /// <summary>
        /// Makes a file publicly accessible via link
        /// </summary>
        /// <param name="fileId">Google Drive file ID</param>
        private async Task MakeFilePublic(string fileId)
        {
            try
            {
                // Create a permission for anyone to view the file
                var permission = new Permission
                {
                    Type = "anyone",
                    Role = "reader"
                };
                
                // Apply the permission to the file
                var request = _driveService.Permissions.Create(permission, fileId);
                await request.ExecuteAsync();
                
                Console.WriteLine($"File {fileId} is now publicly accessible");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error making file public: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Gets file information including the web view link
        /// </summary>
        /// <param name="fileId">Google Drive file ID</param>
        /// <returns>File information or null if not found</returns>
        public async Task<Google.Apis.Drive.v3.Data.File?> GetFileInfo(string fileId)
        {
            try
            {
                var request = _driveService.Files.Get(fileId);
                request.Fields = "id,name,mimeType,webViewLink";
                
                return await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting file info: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Deletes a file from Google Drive
        /// </summary>
        /// <param name="fileId">Google Drive file ID to delete</param>
        /// <returns>True if deletion succeeds, false otherwise</returns>
        public async Task<bool> DeleteFile(string fileId)
        {
            try
            {
                var request = _driveService.Files.Delete(fileId);
                await request.ExecuteAsync();
                
                Console.WriteLine($"File {fileId} deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Gets the MIME type from a file extension
        /// </summary>
        /// <param name="extension">File extension including the dot (.jpg)</param>
        /// <returns>MIME type string</returns>
        private string GetMimeTypeFromExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".webp":
                    return "image/webp";
                case ".svg":
                    return "image/svg+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}