using System.Text.Json;

namespace Cms.Shared.Shared.Models;

public class JsonModel
{
    public string Name { get; set; }
    public JsonElement Value { get; set; }
}