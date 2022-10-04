using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Drawing;
using System.Reflection;




Console.WriteLine("FÖRARPROV 1.0");
RunScript();


static void RunScript()
{

    //GET USER INPUTS FROM APP.CONFIG
    string userSSN = ConfigurationManager.AppSettings["userSSN"];
    string userLicense = ConfigurationManager.AppSettings["userLicense"];
    string userTestType = ConfigurationManager.AppSettings["userTestType"];
    string userCity = ConfigurationManager.AppSettings["userCity"];
    string userCar = ConfigurationManager.AppSettings["userCar"];
    string userDates = ConfigurationManager.AppSettings["userDates"];
    string refreshTime = ConfigurationManager.AppSettings["refreshTime"];

    int refreshRate = Convert.ToInt32(refreshTime) * 1000;
    
    //Sets Minimum every 2 minutes 
    if (refreshRate < 120000)
    {
        refreshRate = 120000;
    } 




    //SET DRIVER
    ChromeOptions options = new ChromeOptions();
    var chromeDriverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
    chromeDriverService.HideCommandPromptWindow = true;
    chromeDriverService.SuppressInitialDiagnosticInformation = true;
    options.AddArgument("--silent");
    options.AddArgument("log-level=3");


    IWebDriver driver = new ChromeDriver(chromeDriverService, options);


    //GO TO WEBSITE

    driver.Url = "https://fp.trafikverket.se/Boka/#/licence";
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


    //Change window size since site looks diffrent depending on resloution / Sidan fungerar olika beroende på fönstrets storlek
    driver.Manage().Window.Maximize();


    //INPUT SSN / LÄGG IN PERSONNUMMER
    var ssnField = driver.FindElement(By.XPath("//input[@id='social-security-number-input']"));
    ssnField.SendKeys(userSSN);


    //CHOOSE LICENSE / VÄLJ KÖRKORTS TYP
    var chooseLicense = driver.FindElement(By.XPath("//*[text()='" + (userLicense) + "']"));
    chooseLicense.Click();

    //CHANGE WINDOW SIZE / ÄNDRA FÖNSTERSTORLEK
    driver.Manage().Window.Size = new Size(500, 1000);


    //OPENS THE TEST MENU / ÖPPNAR TAB MED VILKA PROV SOM FINNS
    var chooseProv = driver.FindElement(By.XPath("//body/div[2]/section[3]/form[1]/div[3]/div[1]/button[1]"));
    chooseProv.Click();
    //CLICKING ON THE USERS CHOISE / KLICKAR PÅ ANV ALTERNATIV
    var confirmProv = driver.FindElement(By.XPath("//span[contains(text(),'" + userTestType + "')]"));
    confirmProv.Click();



    //--''--
    var chooseCity = driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/section[3]/form[1]/div[4]/div[1]/button[1]"));
    chooseCity.Click();
    //--''--
    var confirmCity = driver.FindElement(By.XPath("//span[contains(text(),'" + userCity + "')]"));
    confirmCity.Click();



    //OPEN MENU / ÖPPNA MENYN

    var chooseCar = driver.FindElement(By.XPath("//body/div[2]/section[3]/form[1]/div[7]/div[1]/button[1]"));
    chooseCar.Click();
    //CHOOSING MANUAL OR AUTOMATIC / VÄLJER MANUELL BIL ELLER AUTOMAT
    var confirmCar = driver.FindElement(By.XPath("//span[contains(text(),'" + userCar + "')]"));
    confirmCar.Click();


    //GETTING THE EARLIEST TIME AVAILABLE
    var Date = driver.FindElement(By.XPath("//body[1]/div[2]/section[5]/div[2]/div[14]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/strong[1]")).Text;
    string earliestDate = Date.ToString();

    getLatest();
    //CHECK IF EARLIEST DATE ON WEBISTE MATCHES WITH userDates
    void getLatest()
    {
        List<string> checkDates = userDates.Split(',').ToList<string>();
        foreach (string dates in checkDates)
        {
            if (checkDates.Contains(earliestDate.Split()[0]))
            {
                Console.WriteLine("TIME FOUND AT " + Date.ToString());
                driver.Quit();
                break;
            }
            else
            {
                Console.WriteLine("EARLIEST TIME FOUND " + Date.ToString());
                Console.WriteLine("Checking for new time...");
                refreshDate();
            }
        }
        //REFRESH THE SITE EVERY MIUNTE
        void refreshDate()
        {
            driver.Navigate().Refresh();
            System.Threading.Thread.Sleep(refreshRate);

            //OPENS THE TEST MENU / ÖPPNAR TAB MED VILKA PROV SOM FINNS
            var chooseProv = driver.FindElement(By.XPath("//body/div[2]/section[3]/form[1]/div[3]/div[1]/button[1]"));
            chooseProv.Click();
            //CLICKING ON THE USERS CHOISE / KLICKAR PÅ ANV ALTERNATIV
            var confirmProv = driver.FindElement(By.XPath("//span[contains(text(),'" + userTestType + "')]"));
            confirmProv.Click();

            //--''--
            var chooseCity = driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/section[3]/form[1]/div[4]/div[1]/button[1]"));
            chooseCity.Click();
            //--''--
            var confirmCity = driver.FindElement(By.XPath("//span[contains(text(),'" + userCity + "')]"));
            confirmCity.Click();

            var chooseCar = driver.FindElement(By.XPath("//body/div[2]/section[3]/form[1]/div[7]/div[1]/button[1]"));
            chooseCar.Click();
            //CHOOSING MANUAL OR AUTOMATIC / VÄLJER MANUELL BIL ELLER AUTOMAT
            var confirmCar = driver.FindElement(By.XPath("//span[contains(text(),'" + userCar + "')]"));
            confirmCar.Click();

            //Run to see if any of the new dates matches
            getLatest();
        }
    }
}
    




