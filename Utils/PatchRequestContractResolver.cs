using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TodoRestApi.Utils;
 /*
    Class for adding all property names which really present in PATCH requests during json deserialization
*/
public class PatchRequestContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        prop.SetIsSpecified += (o, o1) =>
        {
            if (o is PatchDtoBase patchDtoBase)
            {
                patchDtoBase.SetHasProperty(prop.PropertyName);
            }
        };

        return prop;
    }
}
