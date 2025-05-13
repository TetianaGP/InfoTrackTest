namespace LearningGoal.Models
{
    public class UserModel
    {
        public User user { get; set; }
        public int updatedSalary { get; set; }
    }

    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string department { get; set; }
    }
}
