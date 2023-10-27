using System.Text.Json.Serialization;

namespace TechLanches.Application.DTOs
{
    public class ErrorResponseDTO
    {
        public int StatusCode { get; set; }
        public string MensagemErro { get; set; }
    }
}