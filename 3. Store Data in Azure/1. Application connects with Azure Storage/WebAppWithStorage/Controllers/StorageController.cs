using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace WebAppWithStorage.Controllers
{
    public class StorageController : Controller
    {
        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger)
        {
            _logger = logger;
        }

        public IActionResult AzureStorage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AzureStorage(IFormFile fimage)
        {
            var filename = fimage.FileName;
            var fileUrl = "";
            BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=<Storage Account name>;AccountKey=<Storage Account Key>",
            "<Storage Blob Container Name>");
            try
            {
                BlobClient blob = container.GetBlobClient(filename);
                using (Stream stream = fimage.OpenReadStream())
                {
                    blob.Upload(stream);
                }
                fileUrl = blob.Uri.AbsoluteUri;
            }
            catch (Exception ex) { }

            return View();
        }

    }
}