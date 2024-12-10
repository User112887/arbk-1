using Newtonsoft.Json;

namespace Arbk.Helpers;

internal class CopyHelper
{
    public static T Copy<T>(T currentObject)
    {
        var newObject = JsonConvert.SerializeObject(currentObject);

        return JsonConvert.DeserializeObject<T>(newObject);
    }
}
