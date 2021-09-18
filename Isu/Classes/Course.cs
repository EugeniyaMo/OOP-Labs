using System.Collections.Generic;

namespace Isu.Classes
{
    public class Course
    {
        public Course(int courseNumber)
        {
            CourseNumber = courseNumber;
            Groups = new List<Group>();
        }

        public int CourseNumber { get; }
        public List<Group> Groups { get; }
    }
}