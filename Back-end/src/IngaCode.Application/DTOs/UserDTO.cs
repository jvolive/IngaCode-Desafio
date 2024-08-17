using System.Text.Json.Serialization;

namespace IngaCode.Application.DTOs;

public class UserDto
{
    [JsonPropertyName("userName")]
    public required string UserName { get; set; }
}
