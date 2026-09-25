using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.DTOs
{
    public record OrderDTO(int Id, int ClientId, DateTime Date, List<OrderLineDTO> Lines);
    public record CreateOrderDTO(int ClientId, List<CreateOrderLineDTO> Lines);
}