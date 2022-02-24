using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobStorage
{
    class Program
    {
        private static string connectionString
        {
            get
            {
                return "DefaultEndpointsProtocol=https;AccountName=azureblobstorageprac;AccountKey=ilYB6miKkEBe4NWxlH9YWOk7RMWraLRu6PRi4P5cqjYdGXY5UbNRdYtEkWSePPOQN/7OW5USxPOo+AStYV/1Xg==;EndpointSuffix=core.windows.net";
            }
        }

        private static string containerName
        {
            get
            {
                return "blobpracticecontainer";
            }
        }

        //Add Your System Path for file to Download
        private static string FilePath
        {
            get
            {
                return @"c:\Users\sadeed.ullahkhan\Desktop\BlobStorage";
            }
        }

        public static BlobContainerClient _container { get; set; }

        static Program()
        {
            _container = new BlobContainerClient(connectionString, containerName);
            _container.CreateIfNotExists();
        }

        static void Main(string[] args)
        {
            UploadBlob();
        }

        public static void DownloadBlob()
        {
            try
            {
                var blobs = _container.GetBlobs();
                foreach (var blob in blobs)
                {
                    using (var fileStream = File.OpenWrite(Path.Combine(FilePath, blob.Name)))
                    {
                         new BlobClient(connectionString, containerName, blob.Name).DownloadTo(fileStream);
                    }
                    Console.WriteLine(blob.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UploadBlob()
        {
            try
            {
                string fileName = "Instructions" + Guid.NewGuid().ToString() + ".txt"; //File Name With Extention
                BlobClient blobClient = _container.GetBlobClient(fileName);
                using (var fileStream = File.OpenRead(FilePath))
                {
                    blobClient.Upload(fileStream,true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
