using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.DTOs
{
    public class PedidoDTO
    {
        public int ClienteId { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } 
    }
}
