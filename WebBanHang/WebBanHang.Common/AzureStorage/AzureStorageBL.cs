using Azure;
using Azure.Core.Pipeline;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;

namespace WebBanHang.Common.AzureStorage
{
    public class AzureStorageBL : IAzureStorageBL
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly string _storageUrl;
        public AzureStorageBL(IConfiguration configuration)
        {
            _storageConnectionString = configuration["BlobConnectionString"].ToString();
            _storageContainerName = configuration["BlobContainerName"].ToString();
            _storageUrl = configuration["AzureStorageURL"].ToString();
        }

        public async Task<List<BlobStorage>> GetAllBlobFiles()
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                // Create the blob client.
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(_storageContainerName);
                CloudBlobDirectory dirb = container.GetDirectoryReference(_storageContainerName);


                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(string.Empty,
                    true, BlobListingDetails.Metadata, 100, null, null, null);
                List<BlobStorage> fileList = new List<BlobStorage>();

                foreach (var blobItem in resultSegment.Results)
                {
                    // A flat listing operation returns only blobs, not virtual directories.
                    var blob = (CloudBlob)blobItem;
                    fileList.Add(new BlobStorage()
                    {
                        FileName = blob.Name,
                        FileSize = Math.Round((blob.Properties.Length / 1024f) / 1024f, 2).ToString(),
                        Modified = DateTime.Parse(blob.Properties.LastModified.ToString()).ToLocalTime().ToString()
                    });
                }
                return fileList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ServiceResult> UploadBlobFileAsync(IFormFile file)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                BlobContainerClient blobcontaninerClient = new BlobContainerClient(_storageConnectionString, _storageContainerName);
                string fileName = file.FileName;
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    await blobcontaninerClient.UploadBlobAsync(fileName, stream);
                }
                serviceResult.Data = _storageUrl + fileName;

            }
            catch (Exception ex)
            {
                serviceResult.setError(ex.ToString());
            }
            return serviceResult;
        }
        public async Task DeleteDocumentAsync(string blobName)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_storageContainerName);
                var blob = cloudBlobContainer.GetBlobReference(blobName);
                await blob.DeleteIfExistsAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Upload file to azure storage
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UploadFileAsync(IFormFile file)
        {
            ServiceResult serviceResult = new ServiceResult();


            return serviceResult;
        }

        
    }
}
