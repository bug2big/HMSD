using Encryption_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Encryption_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncryptionServiceController : ControllerBase
    {
        private readonly EncryptionWrapper _encryptionWrapper;

        public EncryptionServiceController(EncryptionWrapper encryptionWrapper)
        {
            _encryptionWrapper = encryptionWrapper;
        }

        [HttpGet(Name = "Encrypt")]
        public string EncryptData(string  plainText)
        {
            return _encryptionWrapper.Encrypt(plainText) ?? string.Empty;
        }

        [HttpGet(Name = "Decrypt")]
        public string DecryptData(string encryptedText)
        {
            return _encryptionWrapper.Decrypt(encryptedText);
        }

        [HttpPost(Name = "Rotate")]
        public void RotateKey()
        {
            _encryptionWrapper.RotateKey();
        }
    }
}