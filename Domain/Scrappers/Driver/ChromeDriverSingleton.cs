using OpenQA.Selenium.Chrome;

namespace Domain.Scrappers.Driver
{
    public class ChromeDriverSingleton
    {
        private static ChromeDriverSingleton? _instance;
        private static object _lock = new object();
        private ChromeDriverSingleton()
        {            
        }
        public static ChromeDriverSingleton GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ChromeDriverSingleton();
                    }
                }
            }
            return _instance;
        }

        public ChromeDriver GetDriver()
        {
            ChromeOptions options = SetChromedriverOptions(new ChromeOptions());

            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
        }

        public void CloseDriver(ChromeDriver driver)
        {
            driver.Close();
            driver.Quit();
        }

        public ChromeOptions SetChromedriverOptions(ChromeOptions options)
        {
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-software-rasterizer");
            options.AddArgument("--disable-features=VizDisplayCompositor");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-features=VizDisplayCompositor");
            options.AddArgument("--disable-features=IsolateOrigins");
            options.AddArgument("--disable-features=SharedArrayBuffer");
            options.AddArgument("--disable-features=CrossSiteDocumentBlockingIfIsolating");
            options.AddArgument("--disable-features=CrossSiteDocumentBlockingAlways");

            return options;
        }
    }
}
