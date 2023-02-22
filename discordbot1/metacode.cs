using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Keys = OpenQA.Selenium.Keys;
using Serilog;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Forms;

namespace discordbot1
{
    internal class metacode
    {
        IWebDriver wb = null;
        public static bool CODEEEEEEStop;
        public static async Task MetaGen(string email, string password)
        {
            //Creates DeviceDriver for meta-gen
            var DeviceDriver = ChromeDriverService.CreateDefaultService();
            IWebDriver wb = null;
            
            //Uses selenium and hides the command prompt
            DeviceDriver.HideCommandPromptWindow = true;

            //Creates chromeOptions
            ChromeOptions options = new ChromeOptions();

            //Runs as headless meaning no chrome GUI -- Runs in background
            options.AddArguments("--headless");
            options.AddArguments("--disable-infobars");

            //Sets the options to the driver
            wb = new ChromeDriver(DeviceDriver, options);

            //Maximizes window
            wb.Manage().Window.Maximize();

            //Navigates to Meta website
            wb.Navigate().GoToUrl("https://auth.meta.com");
            wb.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Login With oculus
            var links = wb.FindElements(By.TagName("img"));
            var test = links[0].GetAttribute("src");
            var spans = wb.FindElements(By.TagName("span"));
            var hehe = spans[1].Text;
            if (hehe != "Welcome")
            {
                var ugh = wb.FindElements(By.TagName("span"));
                var theone = ugh[7];
                theone.Click();
                Console.WriteLine("Clicked already have!");
                //Already Have
                var ugh1 = wb.FindElements(By.TagName("span"));
                var thetwo = ugh1[3];
                thetwo.Click();
                //Not Facebook
                var ugh2 = wb.FindElements(By.TagName("span"));
                var thethree = ugh2[4];
                thethree.Click();
                Thread.Sleep(1250);
                var ugh22 = wb.FindElements(By.TagName("span"));
                var thethreee = ugh22[6];
                thethreee.Click();
                //Oculus sign in
                var ugh3 = wb.FindElements(By.TagName("input"));
                //for(int i = 0; i < ugh3.Count; i++)
                //{
                //    try
                //    {
                //        ugh3[i].Click();
                //        Console.WriteLine(i);
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                var thefour = ugh3[0];
                thefour.Click();
                thefour.SendKeys(email);
                var thefive = ugh3[1];
                thefive.Click();
                string decrypt = password;
                thefive.SendKeys(decrypt);
                var ugh4 = wb.FindElements(By.TagName("button"));
                ugh4[0].Click();
                Thread.Sleep(6000);
                var ugh5 = wb.FindElements(By.TagName("button"));
                var fff = ugh5[3].Text;
                ugh5[2].Click();
                Thread.Sleep(4000);
                var ugh6 = wb.FindElements(By.TagName("div"));
                //This code was heavenly to debug lmao
                //for (int i = 0; i < ugh6.Count; i++)
                //{
                //    try
                //    {
                //        ugh6[i].Click();
                //        Console.WriteLine(i);
                //        
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                ugh6[126].Click();
                Thread.Sleep(5000);
                var ugh7 = wb.FindElements(By.TagName("div"));
                //for (int i = 85; i < ugh7.Count; i++)
                //{
                //    try
                //    {
                //        ugh7[i].Click();
                //        Console.WriteLine(i);
                //        
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                ugh7[90].Click();
                ugh7[90].SendKeys("1");
                ugh7[90].SendKeys(Keys.Enter);
                ugh7[85].Click();
                ugh7[85].SendKeys("j");
                ugh7[85].SendKeys(Keys.Enter);
                ugh7[95].Click();
                ugh7[95].SendKeys("1");
                ugh7[95].SendKeys(Keys.Enter);
                //for (int i = 100; i < ugh7.Count; i++)
                //{
                //    try
                //    {
                //        ugh7[i].Click();
                //        Console.WriteLine(i);
                //        
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                ugh7[128].Click();
                Thread.Sleep(2500);
                var ugh8 = wb.FindElements(By.TagName("div"));
                ugh8[81].Click();
                var ugh9 = wb.FindElements(By.TagName("div"));
                ugh9[180].Click();
                Thread.Sleep(4000);
                wb.Quit();

            }
            else
            {
                var mailbox = wb.FindElements(By.TagName("input"));
                mailbox[0].Click();
                mailbox[0].SendKeys(email);
                var span1 = wb.FindElements(By.TagName("span"));
                for (int i = 0; i < span1.Count; i++)
                {
                    try
                    {
                        if (span1[i].Text == "Continue")
                        {
                            span1[i].Click();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("SadBruh");
                    }
                }
                Thread.Sleep(2000);
                //Already Have
                var ugh1 = wb.FindElements(By.TagName("span"));
                var thetwo = ugh1[3];
                thetwo.Click();
                //Not Facebook
                Thread.Sleep(1250);
                var spanb = wb.FindElements(By.TagName("span"));
                for (int i = 0; i < spanb.Count; i++)
                {
                    try
                    {
                        if (spanb[i].Text == "Yes")
                        {
                            spanb[i].Click();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("SadBruh");
                    }
                }
                var ugh2 = wb.FindElements(By.TagName("span"));
                var thethree = ugh2[6];
                thethree.Click();
                //Oculus sign in
                var ugh3 = wb.FindElements(By.TagName("input"));
                //for(int i = 0; i < ugh3.Count; i++)
                //{
                //    try
                //    {
                //        ugh3[i].Click();
                //        Console.WriteLine(i);
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                var thefour = ugh3[0];
                thefour.Click();
                thefour.SendKeys(email);
                var thefive = ugh3[1];
                thefive.Click();
                string decrypt = password;
                thefive.SendKeys(decrypt);
                var ugh4 = wb.FindElements(By.TagName("button"));
                ugh4[0].Click();
                Thread.Sleep(6000);
                var ugh5 = wb.FindElements(By.TagName("button"));
                var fff = ugh5[3].Text;
                ugh5[2].Click();
                Thread.Sleep(4000);
                var ugh6 = wb.FindElements(By.TagName("div"));
                //for (int i = 0; i < ugh6.Count; i++)
                //{
                //    try
                //    {
                //        ugh6[i].Click();
                //        Console.WriteLine(i);
                //        
                //    }
                //    catch
                //    {
                //        Console.WriteLine("L");
                //    }
                //}
                //Console.WriteLine("Wellllllll");
                ugh6[73].Click();
                Thread.Sleep(5000);
                var ugh7 = wb.FindElements(By.TagName("div"));
                ugh7[60].Click();
                ugh7[60].SendKeys("1");
                ugh7[60].SendKeys(Keys.Enter);
                ugh7[55].Click();
                ugh7[55].SendKeys("j");
                ugh7[55].SendKeys(Keys.Enter);
                ugh7[65].Click();
                ugh7[65].SendKeys("1");
                ugh7[65].SendKeys(Keys.Enter);
                ugh7[74].Click();
                Thread.Sleep(2500);
                var ugh8 = wb.FindElements(By.TagName("div"));
                ugh8[81].Click();
                Thread.Sleep(4000);
                wb.Quit();
            }
        }
    }
}
