using PuppeteerSharp;

namespace Arbk.Services;

internal class JavascriptEmulator
{
    private readonly string _bPath;
    public JavascriptEmulator(string bPath = "")
    {
        _bPath = string.IsNullOrEmpty(bPath) ? Config.BROWSER_PATH : bPath;
    }

    public async Task<IBrowser> GetBrowserAsync()
    {
        return await GetBrwoser();
    }


    public async Task<IPage> GetFirstPageWithDefaults(IBrowser browser)
    {
        var pages = await browser.PagesAsync();

        var firstPage = pages[0];

        await firstPage.SetUserAgentAsync("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");

        await firstPage.GoToAsync("https://arbk.rks-gov.net/", new NavigationOptions
        {
            Timeout = int.MaxValue
        });

        await firstPage.SetRequestInterceptionAsync(true);

        return firstPage;
    }

    public async Task<IBrowser> GetBrwoser()
    {
        var launchOptions = new LaunchOptions
        {
            Headless = false,
            ExecutablePath = _bPath,
        };

        return await Puppeteer.LaunchAsync(launchOptions);
    }
}
