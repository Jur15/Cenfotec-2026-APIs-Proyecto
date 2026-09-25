using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.DTOs
{
    public record ClientDTO(int Id, string Name, string Email);
    public record CreateClientDTO(string Name, string Email);
    public record UpdateClientDTO(string Name, string Email);
}
