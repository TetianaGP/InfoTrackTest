using LearningGoal.Models;
using LearningGoal.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;

namespace LearningGoal.Tests
{
    public class TableTests
    {
        private IWebDriver driver;
        private MainPage mainPage;
        private TablePopupPage tablePopupPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demoqa.com/webtables");
            driver.Manage().Window.Maximize();
            Assert.That(driver.Title, Is.EqualTo("DEMOQA"));

            mainPage = new MainPage(driver);
            tablePopupPage = new TablePopupPage(driver);
        }

        [Test]
        [TestCaseSource(nameof(UserJsonDataSource))]
        public void GivenNewData_WhenAddDataInTable_ThenNewRowDispayed(UserModel user)
        {

            mainPage.ClickAddRecord();
            tablePopupPage.FillInData(user);
            tablePopupPage.ClickSubmitBtn();
            Assert.That(mainPage.WaitForTableRowData(user)==true, "One or more value in the new row is incorrect");
        }

        [Test]
        [TestCaseSource(nameof(UserJsonDataSource))]
        public void GivenUpdatedData_WhenEditItemInTable_ThenUpdatedRowShouldBeDisplayed(UserModel user)
        {      
            mainPage.ClickAddRecord();
            tablePopupPage.FillInData(user);
            tablePopupPage.ClickSubmitBtn();
            mainPage.ClickEditBtn();
            tablePopupPage.UpdateSalary(user.updatedSalary);
            tablePopupPage.ClickSubmitBtn();
            Assert.That(mainPage.WaitForUpdatedRowDisplayed(user)==true, "Updated row is not displayed");
        }

        [Test]
        [TestCaseSource(nameof(UserJsonDataSource))]
        public void WhenDeleteDataInTable_WaitForDataDeleted(UserModel user)
        {
            mainPage.ClickAddRecord();
            tablePopupPage.FillInData(user);
            tablePopupPage.ClickSubmitBtn();
            mainPage.WaitForTableRowData(user);
            mainPage.ClickDeleteBtn();
            Assert.That(mainPage.WaitForTableRowDeletion(user) == true, "Deleted row is still displayed");
        }

        public static IEnumerable<UserModel> UserJsonDataSource()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"JSON\Users.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            var userModels = JsonSerializer.Deserialize<List<UserModel>>(jsonString);
            foreach (var userModel in userModels)
            {
                yield return userModel;

            }
        }

       
        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

    }
}