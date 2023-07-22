using System;
using System.IO;
using System.Threading.Tasks;

namespace AnonFilesAPI
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                AnonfilesApiClient anonfilesApiClient = new AnonfilesApiClient();

                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Upload a file");
                Console.WriteLine("2. Retrieve file information by file ID");
                Console.Write("Enter your choice (1 or 2): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter the file path to upload: ");
                    string filePath = Console.ReadLine();

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("File not found: " + filePath);
                        return;
                    }

                    UploadResponse uploadResponse = await anonfilesApiClient.UploadFileAsync(filePath);

                    if (uploadResponse.Status)
                    {
                        // File uploaded successfully
                        var uploadedFile = uploadResponse.Data.File;

                        Console.WriteLine("File URL: " + uploadedFile.Url.Full);
                        Console.WriteLine("File ID: " + uploadedFile.Metadata.Id);
                        Console.ReadKey();
                    }
                    else
                    {
                        // Handle the error response
                        Console.WriteLine("Error: " + uploadResponse.Error.Message);
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Enter the file ID to retrieve file information: ");
                    string fileId = Console.ReadLine();

                    FileInfoResponse fileInfoResponse = await anonfilesApiClient.GetFileInfoAsync(fileId);

                    if (fileInfoResponse.Status)
                    {
                        // File info retrieved successfully
                        var file = fileInfoResponse.Data.File;
                        Console.WriteLine("File URL: " + file.Url.Full);
                        Console.WriteLine("File Name: " + file.Metadata.Name);
                        Console.WriteLine("File Size: " + file.Metadata.Size.Readable);
                        Console.ReadKey();
                    }
                    else
                    {
                        // Handle the error response
                        Console.WriteLine("Error: " + fileInfoResponse.Error.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose option 1 or 2.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }



    }
}
