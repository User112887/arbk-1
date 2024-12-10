# ARBK Scraper

This repository contains a .NET 8 project designed to scrape the ARBK (Kosovo Business Registration Agency) website. It uses Puppeteer for browser automation and request interception to efficiently collect data.

## Features

- **Powered by .NET 8**: Leverages the latest features and performance improvements in .NET 8.
- **Browser Automation**: Utilizes Puppeteer for seamless interaction with ARBK's web interface.
- **Request Interception**: Captures and modifies network requests to optimize data extraction.
- **Highly Configurable**: Easy to customize scraping parameters and targets.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (required for Puppeteer)
- A modern operating system (Windows, macOS, or Linux)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/arbk-scraper.git
   cd arbk-scraper
   ```

2. Restore .NET dependencies:

   ```bash
   dotnet restore
   ```

3. Install Puppeteer:

   ```bash
   npm install puppeteer
   ```

## Configuration

### Puppeteer Settings

- The Puppeteer browser is configured in `PuppeteerService.cs`. Modify the launch arguments, such as headless mode and executable path, as needed.

### Request Interception

- Request interception is enabled to capture and manipulate network traffic. Modify the interception logic in `RequestInterceptor.cs` to filter or alter specific requests.

### App Settings

- Configure application settings in `appsettings.json`:
  ```json
  {
    "Scraper": {
      "TargetUrl": "https://arbk.rks-gov.net/",
      "OutputPath": "./data/output.json"
    }
  }
  ```

## Usage

### Running the Scraper

To execute the scraper:

```bash
dotnet run
```

### Sample Output

The scraped data is saved as JSON in the specified `OutputPath` directory. Ensure you have write permissions for the directory.

## Development

### Adding Features

- To add custom scraping logic, update the `ScraperService.cs` file.
- Extend Puppeteer functionality using custom browser scripts.

### Testing

- Unit tests are located in the `Tests` folder.
- Run tests with:
  ```bash
  dotnet test
  ```

## License

This project is licensed under the Apache-2.0 License. See the [LICENSE](LICENSE) file for details.

