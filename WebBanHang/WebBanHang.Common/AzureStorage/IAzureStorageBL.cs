using Gather.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Common.Entities.Model;

namespace WebBanHang.Common.AzureStorage
{
    public interface IAzureStorageBL
    {
        public Task<ServiceResult> UploadFileAsync(IFormFile file);

        Task<List<BlobStorage>> GetAllBlobFiles();
        Task<ServiceResult> UploadBlobFileAsync(IFormFile files);
        Task DeleteDocumentAsync(string blobName);
    }
}
