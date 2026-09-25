using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.DTOs
{
    public record OrderLineDTO(int AlbumId, int Quantity);
    public record CreateOrderLineDTO(int AlbumId, int Quantity);
}
