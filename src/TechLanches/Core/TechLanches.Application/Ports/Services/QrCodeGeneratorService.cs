using QRCoder;
using System.Drawing;
using TechLanches.Application.Ports.Services.Interfaces;

namespace TechLanches.Application.Ports.Services
{
    public class QrCodeGeneratorService : IQrCodeGeneratorService
    {
        public Bitmap GenerateImage(string pagamentoPedido)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(pagamentoPedido, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;
        }

        public byte[] GenerateByteArray(string url)
        {
            var image = GenerateImage(url);
            return ImageToByte(image);
        }

        private byte[] ImageToByte(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
