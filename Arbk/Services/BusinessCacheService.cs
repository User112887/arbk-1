using Arbk.Helpers;
using Arbk.Models;
using Newtonsoft.Json;

namespace Arbk.Services;

internal class BusinessCacheService
{
    readonly string _dumpPath;
    readonly List<Business> _businesses = new List<Business>();

    private int _currentDump = 0;

    public BusinessCacheService(string dumpPath = "")
    {
        _dumpPath = string.IsNullOrEmpty(dumpPath) ? Config.JSON_FILE_PATH : dumpPath;

        if (!File.Exists(_dumpPath))
        {
            File.Create(_dumpPath);
        }

        var bText = File.ReadAllText(_dumpPath);

        _businesses = JsonConvert.DeserializeObject<List<Business>>(bText);

        int startsWithHello = _businesses.Count(c => c.TeDhenatBiznesit.Email.Contains("helloworld.com"));
    }

    public void UpdateOrAdd(Business business)
    {
        var existingBusinessIndex = _businesses.FindIndex(0, b => b.TeDhenatBiznesit.nRegjistriID == business.TeDhenatBiznesit.nRegjistriID);

        if (existingBusinessIndex == -1)
        {
            _businesses.Add(business);
        }
        else
        {
            _businesses[existingBusinessIndex] = CopyHelper.Copy<Business>(business);
        }

        Dump();
    }

    private void Dump()
    {
        if (_currentDump < Config.DUMP_FREQUENCE)
        {
            _currentDump++;
            return;
        }

        _currentDump = 0;

        int startsWithHello = _businesses.Count(c => c.TeDhenatBiznesit.Email.Contains("helloworld.com"));


        File.WriteAllText(_dumpPath, JsonConvert.SerializeObject(_businesses));
    }
}
