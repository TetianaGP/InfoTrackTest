using OpenQA.Selenium;

namespace LearningGoal.Helpers
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string stepName)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{timestamp}_{stepName}.png";
                folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                string fullPath = Path.Combine(folderPath, fileName);

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(fullPath);
                TestContext.WriteLine($"Screenshot saved: {fullPath}");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Screenshot failed: {ex.Message}");
            }
        }
    }
}
