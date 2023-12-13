using System.Drawing;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface IQrCodeGeneratorService
    {
        public Bitmap GenerateImage(string pagamentoPedido);
        public byte[] GenerateByteArray(string url);
    }
}