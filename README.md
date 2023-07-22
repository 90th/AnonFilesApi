# AnonfilesApiClient Class Documentation

The AnonfilesApiClient class provides a C# wrapper for interacting with the Anonfiles API, which allows users to upload files and retrieve file information using the API.
## Constructor

```csharp

public AnonfilesApiClient()
```
Creates an instance of the AnonfilesApiClient class.
Methods
UploadFileAsync

```csharp

public async Task<UploadResponse> UploadFileAsync(string filePath)
```
Uploads a file to the Anonfiles API.

Parameters:

  -  filePath (string): The local file path of the file to be uploaded.

Returns:

  -  A Task<UploadResponse> representing the asynchronous operation.
  -  UploadResponse: Contains the response data from the API, including the file URL, file ID, and metadata if successful. Otherwise, it contains the error information.

Example Usage:

```csharp

AnonfilesApiClient anonfilesApiClient = new AnonfilesApiClient();
string filePath = "file.txt";
UploadResponse uploadResponse = await anonfilesApiClient.UploadFileAsync(filePath);

if (uploadResponse.Status)
{
    // File uploaded successfully
    var uploadedFile = uploadResponse.Data.File;

    Console.WriteLine("File URL: " + uploadedFile.Url.Full);
    Console.WriteLine("File ID: " + uploadedFile.Metadata.Id);
}
else
{
    // Handle the error response
    Console.WriteLine("Error: " + uploadResponse.Error.Message);
}
```
GetFileInfoAsync

```csharp

public async Task<FileInfoResponse> GetFileInfoAsync(string fileId)
```
Retrieves file information from the Anonfiles API based on the file ID.

Parameters:

   - fileId (string): The unique ID of the file to retrieve information for.

Returns:

  -  A Task<FileInfoResponse> representing the asynchronous operation.
  -  FileInfoResponse: Contains the response data from the API, including the file URL, file name, file size, and metadata if successful. Otherwise, it contains the error information.

Example Usage:

```csharp

AnonfilesApiClient anonfilesApiClient = new AnonfilesApiClient();
string fileId = "u1C0ebc4b0";
FileInfoResponse fileInfoResponse = await anonfilesApiClient.GetFileInfoAsync(fileId);

if (fileInfoResponse.Status)
{
    // File info retrieved successfully
    var file = fileInfoResponse.Data.File;
    Console.WriteLine("File URL: " + file.Url.Full);
    Console.WriteLine("File Name: " + file.Metadata.Name);
    Console.WriteLine("File Size: " + file.Metadata.Size.Readable);
}
else
{
    // Handle the error response
    Console.WriteLine("Error: " + fileInfoResponse.Error.Message);
}
```
## Response Classes
 UploadResponse

Contains the response data from the API after a file upload request.

Properties:

  *  Status (bool): Indicates if the request was successful or not.
  *  Data (UploadResponseData): Contains the data for the uploaded file.
  *  Error (Error): Contains error information if the request was not successful.

UploadResponseData

Contains the data for the uploaded file.

Properties:

  *  File (UploadedFile): Contains the file URL and metadata.

## UploadedFile

Contains the file URL and metadata.

Properties:

  *  Url (FileUrl): Contains the URLs for the uploaded file (full and short URLs).
  *  Metadata (FileMetadata): Contains metadata for the uploaded file, such as the file ID, name, and size.

## FileUrl

Contains the URLs for the uploaded file.

Properties:

   * Full (string): The full URL of the uploaded file.
   * Short (string): The short URL of the uploaded file.

## FileMetadata

Contains metadata for the uploaded file.

Properties:

  *  Id (string): The unique ID of the uploaded file.
  *  Name (string): The name of the uploaded file.
  * Size (FileSize): Contains the file size information.

## FileSize

Contains information about the file size.

Properties:

  *  Bytes (int): The file size in bytes.
  *  Readable (string): The human-readable representation of the file size (e.g., "6.7 KB").

## FileInfoResponse

Contains the response data from the API after a file information request.

Properties:

 *   Status (bool): Indicates if the request was successful or not.
 *   Data (FileInfoResponseData): Contains the data for the requested file.
 *   Error (Error): Contains error information if the request was not successful.

## FileInfoResponseData

Contains the data for the requested file.

Properties:

    File (UploadedFile): Contains the file URL and metadata for the requested file.

Error Handling

The response classes (UploadResponse, FileInfoResponse, and Error) provide error information in case the API request is not successful. Always check the Status property of the response objects to determine if the API call was successful or not. If the status is false, you can access the Error property to get the error message, type, and code.

```csharp

if (!uploadResponse.Status)
{
    // Handle the error response
    Console.WriteLine("Error: " + uploadResponse.Error.Message);
}
```
Disclaimer

``Please note that the Anonfiles API is an external service not maintained or endorsed by this project. Always follow the API usage guidelines and respect the terms of service for any third-party APIs you use.``
