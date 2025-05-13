using OpenQA.Selenium;

namespace LearningGoal.Helpers
{
    public static class HelperMethods
    {

        public static void ClickElement(this IWebElement locator)
        {
            locator.Click();
        }

        public static void SubmitElement(this IWebElement locator)
        {
            locator.Submit();
        }

        public static void EnterText(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
    }
}
