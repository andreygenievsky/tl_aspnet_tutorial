
using System.Collections.Generic;

namespace TodoRestApi.Utils;

/*
    Base class for DTOs used in PATCH requests
*/
public abstract class PatchDtoBase
{
    private HashSet<string> PropertiesInHttpRequest { get; set; } = new HashSet<string>();

    public bool IsFieldPresent(string propertyName)
    {
        return PropertiesInHttpRequest.Contains(propertyName.ToLowerInvariant());
    }

    public void SetHasProperty(string propertyName)
    {
        PropertiesInHttpRequest.Add(propertyName.ToLowerInvariant());
    }
}
