using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.IO;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ControllerBase
    {
        /// <summary>
        /// Generate a barcode (QR code) for a given patient ID or NHI.
        /// </summary>
        /// <param name="value">Patient ID or NHI to encode</param>
        /// <returns>SVG image of the barcode</returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetBarcode([FromQuery] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Value is required");

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            var svgQrCode = new SvgQRCode(qrCodeData);
            string svg = svgQrCode.GetGraphic(4);
            return Content(svg, "image/svg+xml");
        }
    }
}
