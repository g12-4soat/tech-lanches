using System.Drawing;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface IQrCodeGeneratorService
    {
        public Bitmap GenerateImage(string pagamentoPedido);
        public byte[] GenerateByteArray(string url);
    }
}