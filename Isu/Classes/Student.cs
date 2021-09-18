namespace Isu.Classes
{
    public class Student
    {
        private static int _number = 100000;
        public Student(string studentName)
        {
            StudentID = _number;
            _number += 1;
            StudentName = studentName;
        }

        public string StudentName { get; }
        public int StudentID { get; }
        public string GroupName { get; set; }
    }
}