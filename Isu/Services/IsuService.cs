using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Course> _courses = new List<Course>();
        public Group AddGroup(string name)
        {
            int courseNumber = int.Parse(name[2] + " ");
            bool check = true;
            if (!Group.CheckGroupName(name))
                throw new IsuException("Incorrect Group name");
            Group newGroup = new Group(name);
            foreach (Course course in _courses)
            {
                if (course.CourseNumber == courseNumber)
                {
                    check = false;
                    course.Groups.Add(newGroup);
                }
            }

            if (check)
            {
                _courses.Add(new Course(courseNumber));
                _courses.Last().Groups.Add(newGroup);
            }

            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            Student newStudent = new Student(name);
            if (group.Students.Count >= group.MaxStudents)
                throw new IsuException("Maximum count of students in a Group");
            foreach (Course course in _courses)
            {
                foreach (Group element in course.Groups)
                {
                    if (group.Equals(element))
                    {
                        element.Students.Add(newStudent);
                        newStudent.GroupName = element.GroupName;
                        return newStudent;
                    }
                }
            }

            throw new IsuException("Wrong Group");
        }

        public Student GetStudent(int id)
        {
            foreach (Course course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.StudentID == id)
                            return student;
                    }
                }
            }

            throw new IsuException("Student not found");
        }

        public Student FindStudent(string name)
        {
            foreach (Course course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.StudentName == name)
                            return student;
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(Course courseNumber)
        {
            var entireCourse = new List<Student>();
            foreach (Course course in _courses)
            {
                if (course.CourseNumber == courseNumber.CourseNumber)
                {
                    foreach (Group group in course.Groups)
                    {
                        foreach (Student student in group.Students)
                        {
                            entireCourse.Add(student);
                        }
                    }
                }
            }

            return entireCourse;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Course course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    if (group.GroupName == groupName)
                        return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(Course courseNumber)
        {
            foreach (Course course in _courses)
            {
                if (course.CourseNumber == courseNumber.CourseNumber)
                    return course.Groups;
            }

            return null;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Course course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student element in group.Students.ToList())
                    {
                        if (element == student)
                        {
                            FindStudents(student.GroupName).Remove(student);
                            if (newGroup.Students.Count >= newGroup.MaxStudents)
                                throw new IsuException("Maximum count of students in a Group");
                            newGroup.Students.Add(student);
                        }
                    }
                }
            }
        }
    }
}