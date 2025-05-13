using LearningGoal.Helpers;
using LearningGoal.Models;
using OpenQA.Selenium;
using Polly;
using Polly.Retry;
using System.Reflection;

namespace LearningGoal.Pages
{
    public class MainPage
    {
        public readonly IWebDriver driver;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement AddNewRecordBtn => driver.FindElement(By.Id("addNewRecordButton"));

        IList<IWebElement> parentTable => driver.FindElements(By.XPath("//div[@class = 'rt-tr-group']"));


        IWebElement EditBtn
        {
            get
            {
                // get correct index of Edit Button
                int N = getRowIndex();
                return driver.FindElement(By.Id($"edit-record-{N}"));
            }
        }

        IWebElement DeleteBtn
        {
            get
            {
                // get correct index of Edit Button
                int N = getRowIndex();
                return driver.FindElement(By.Id($"delete-record-{N}"));
            }
        }

        public IWebElement FirstName(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.firstName}')]"));
        }

        public IWebElement LastName(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.lastName}')]"));
        }

        public IWebElement UserEmail(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.email}')]"));
        }

        public IWebElement Age(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.age}')]"));
        }

        public IWebElement Salary(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.salary}')]"));
        }

        public IWebElement UpdSalary(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.email}')]/following-sibling::div[contains(text(),'{user.updatedSalary}')]"));
        }

        public IWebElement Department(UserModel user)
        {
            return RetryFindElement(By.XPath($"//div[@class='rt-td'and contains(text(),'{user.user.department}')]"));
        }

        public void ClickAddRecord()
        {
            AddNewRecordBtn.ClickElement();
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);
        }

        public void ClickEditBtn()
        {
            EditBtn.ClickElement();
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

        }

        public void ClickDeleteBtn()
        {
            DeleteBtn.ClickElement();
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

        }


        private int getRowIndex()
        {
            int index = 0;
            for (int i = 1; i < parentTable.Count; i++)
            {
                index = i - 1;
                try
                {
                    IWebElement EditBtn = driver.FindElement(By.Id($"edit-record-{i}"));
                }
                catch (NoSuchElementException)
                {
                    return index;
                }

            }
            return index;
        }

        public bool WaitForUpdatedRowDisplayed(UserModel user)
        {
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);
            return UpdSalary(user).Displayed;
        }

        public bool WaitForTableRowData(UserModel user)
        {
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

            return FirstName(user).Displayed &&
                LastName(user).Displayed &&
                UserEmail(user).Displayed &&
                Age(user).Displayed &&
                Salary(user).Displayed &&
                Department(user).Displayed;
        }

        public bool WaitForTableRowDeletion(UserModel user)
        {
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

            try
            {
               return FirstName(user).Displayed &&
               LastName(user).Displayed &&
               UserEmail(user).Displayed &&
               Age(user).Displayed &&
               Salary(user).Displayed &&
               Department(user).Displayed;
            }
            catch (NoSuchElementException)
            {
                return true;
            }            
        }

        private RetryPolicy RetrySearch()
        {
            // Repeat searching an element if it is not found
            // Retry count: 10
            // Sleep time between retries: 500 ms
            var retryPolicy = Policy
                    .Handle<NoSuchElementException>()
                    .Or<StaleElementReferenceException>()
                    .WaitAndRetry(retryCount: 10, sleepDurationProvider: retryProvider => TimeSpan.FromMilliseconds(500));
            return retryPolicy;
        }

        protected IWebElement RetryFindElement(By element)
        {
            var retrySeacrh = RetrySearch();
            return retrySeacrh.Execute(() => driver.FindElement(element));
        }

    }
}
