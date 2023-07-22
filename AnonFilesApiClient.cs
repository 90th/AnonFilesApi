using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class AnonfilesApiClient
{
    private const string BaseUrl = "https://api.anonfiles.com";

    private readonly HttpClient httpClient;

    public AnonfilesApiClient()
    {
        httpClient = new HttpClient();
    }

    public async Task<UploadResponse> UploadFileAsync(string filePath)
    {
        using (var formData = new MultipartFormDataContent())
        {
            formData.Add(new StreamContent(new FileStream(filePath, FileMode.Open)), "file", Path.GetFileName(filePath));
            var response = await httpClient.PostAsync($"{BaseUrl}/upload", formData);
            var responseBody = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UploadResponse>(responseBody);
        }
    }

    public async Task<FileInfoResponse> GetFileInfoAsync(string fileId)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/v2/file/{fileId}/info");
        var responseBody = await response.Content.ReadAsStringAsync();
        return Newtonsoft.Json.JsonConvert.DeserializeObject<FileInfoResponse>(responseBody);
    }
}

public class UploadResponse
{
    public bool Status { get; set; }
    public UploadResponseData Data { get; set; }
    public Error Error { get; set; } 
}

public class UploadResponseData
{
    public UploadedFile File { get; set; }
}

public class UploadedFile
{
    public FileUrl Url { get; set; }
    public FileMetadata Metadata { get; set; }
}

public class FileUrl
{
    public string Full { get; set; }
    public string Short { get; set; }
}

public class FileMetadata
{
    public string Id { get; set; }
    public string Name { get; set; }
    public FileSize Size { get; set; }
}

public class FileSize
{
    public int Bytes { get; set; }
    public string Readable { get; set; }
}

public class FileInfoResponse
{
    public bool Status { get; set; }
    public FileInfoResponseData Data { get; set; }
    public Error Error { get; set; } 
}

public class FileInfoResponseData
{
    public UploadedFile File { get; set; }
}

public class Error
{
    public string Message { get; set; }
    public string Type { get; set; }
    public int Code { get; set; }
}

