using LearningGoal.Helpers;
using LearningGoal.Models;
using OpenQA.Selenium;
using System.Reflection;

namespace LearningGoal.Pages
{
    public class TablePopupPage
    {
        public readonly IWebDriver driver;

        public TablePopupPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement FirstName => driver.FindElement(By.Id("firstName"));
        IWebElement LastName => driver.FindElement(By.Id("lastName"));
        IWebElement UserEmail => driver.FindElement(By.Id("userEmail"));
        IWebElement Age => driver.FindElement(By.Id("age"));
        IWebElement Salary => driver.FindElement(By.Id("salary"));
        IWebElement Department => driver.FindElement(By.Id("department"));
        IWebElement SubmitBtn => driver.FindElement(By.Id("submit"));

        public void FillInData(UserModel user)
        {
            FirstName.EnterText(user.user.firstName);
            LastName.EnterText(user.user.lastName);
            UserEmail.EnterText(user.user.email);
            Age.EnterText(user.user.age.ToString());
            Salary.EnterText(user.user.salary.ToString());
            Department.EnterText(user.user.department);
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

        }
        public void ClickSubmitBtn()
        {
            SubmitBtn.SubmitElement();
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

        }

        public void UpdateSalary(int updSalary)
        {          
            Salary.EnterText(updSalary.ToString());
            ScreenshotHelper.TakeScreenshot(driver, MethodBase.GetCurrentMethod().Name);

        }
    }
}
