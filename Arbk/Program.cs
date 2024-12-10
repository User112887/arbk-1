using Arbk.Models;
using Arbk.Services;
using Newtonsoft.Json;
using PuppeteerSharp;

List<int> scrapedIds = new List<int>();

int maxFetch = 300000, i = 1;

var javascriptEmulator = new JavascriptEmulator();

SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

var browser = await javascriptEmulator.GetBrowserAsync();

var firstPage = await javascriptEmulator.GetFirstPageWithDefaults(browser);

bool fetchedMax = false;

var bussinessCacheService = new BusinessCacheService();

firstPage.Request += async (sender, e) =>
{
    await semaphore.WaitAsync();

    if (i > maxFetch)
    {
        i = 1;
        scrapedIds = new List<int>();
    }

    var key = e.Request.Headers.FirstOrDefault(c => c.Key == "key");

    if (string.IsNullOrEmpty(key.Value))
    {
        await ContinueWithNextRequest(e);
        semaphore.Release();
        return;
    }

    try
    {
        if (!fetchedMax)
        {
            maxFetch = await ArbkService.GetNumberOfBusinesses(e.Request.Headers);
            fetchedMax = true;
            return;
        }

        var idScraped = scrapedIds.Any(c => c == i);

        if (idScraped)
        {
            i++;
            return;
        }

        try
        {
            var business = await ArbkService.GetBusiness(i, e.Request.Headers);

            if (business == null) { return; }

            bussinessCacheService.UpdateOrAdd(business);

            scrapedIds.Add(i);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error forwarding request: {ex.Message}");
            await e.Request.AbortAsync();
        }
        
        i++;
    }
    finally
    {
        semaphore.Release();
    }

    await firstPage.ReloadAsync(int.MaxValue);
};

Console.ReadLine();
await browser.CloseAsync();

async Task ContinueWithNextRequest(RequestEventArgs e)
{
    try
    {
        await e.Request.ContinueAsync();
        return;
    }
    catch (Exception ex)
    {
        // DO NOTHING: 
    }
}