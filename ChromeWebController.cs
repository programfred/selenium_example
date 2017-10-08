using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
namespace ADposter
{

    class ChromeWebController
    {
        IWebDriver driver;
        public void init(string _driver)
        {
            if(_driver=="firefox")
            {
                this.driver = new FirefoxDriver();
            }
            else if(_driver=="chrome")
            {
                this.driver = new ChromeDriver();
            }

        }
        private Boolean InputById(string idName, string inputStr)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.Id(idName));
                element.SendKeys(inputStr);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private Boolean InputByName(string idName, string inputStr)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.Name(idName));
                element.SendKeys(inputStr);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        private bool ClickById(string idName)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.Id(idName));
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        private bool ClickByText(string textName)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.LinkText(textName));
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private Point GetIdLocation(string idName)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.Id(idName));
                Point myPoint = element.Location;
                return myPoint;
            }
            catch (Exception e)
            {
                return Point.Empty;
            }

        }
        private bool ClickByCss(string cssName)
        {
            try
            {
                IWebElement element = this.driver.FindElement(By.CssSelector(cssName));
                element.Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool login(string email, string password)
        {
            this.driver.Navigate().GoToUrl("https://www.testsite.com.au/t-login-form.html");
            Thread.Sleep(2000);
            bool loginEmailRes = InputById("login-email", email);
            bool loginPassRes = InputById("login-password", password);
            if(loginEmailRes==true && loginPassRes==true)
            {
                bool clkLoginRes = ClickById("btn-submit-login");
            }
        
            return true;
        }
        private void moveMouseToMap(Point myPoint)
        {
            Actions action = new Actions(driver);
            action.MoveByOffset(myPoint.X, myPoint.Y).Perform();
            action.Click();
        }
        public bool sendContent(string title,string contentex ,string  address)
        {
          
            Thread.Sleep(1000);
            //goto postAD
            this.driver.Navigate().GoToUrl("https://www.testsite.com.au/p-post-ad.html");

            Thread.Sleep(1000);
            this.driver.Navigate().GoToUrl("https://www.testsite.com.au/p-post-ad2.html?categoryId=18643&adType=OFFER");
            //select free section
            Thread.Sleep(2000);
            ClickByCss(".c-dark-green-bg");
            Thread.Sleep(1000);
            bool titleRes = InputById("postad-title", title);
            string content = contentex;
            bool contentRex = InputById("pstad-descrptn", content);

            bool emailRex = InputById("pstad-email", "yourmail@hotmail.com"); 
            //sologan
            bool sologanRes = InputById("removals_storage.slogan_s", "We are your moving solution");


            char[] addressArray = address.ToCharArray();
            IWebElement element = this.driver.FindElement(By.Id("pstad-map-address"));
               element.Clear();
            Point mapPoint = GetIdLocation("pstad-map-address");
            moveMouseToMap(mapPoint);
            foreach (char ad in addressArray)
            {
                InputById("pstad-map-address", ad.ToString());
                Thread.Sleep(3000);
            }
            element.SendKeys(OpenQA.Selenium.Keys.Down);
            Thread.Sleep(3000);
            element.SendKeys(OpenQA.Selenium.Keys.Enter);


            moveMouseToMap(mapPoint);

            Thread.Sleep(2000);
            ClickById("pstad-frmsubmit");
            Thread.Sleep(30000);
            return true;
        }
    }
}
