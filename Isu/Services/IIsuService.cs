using System.Collections.Generic;
using Isu.Classes;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(string groupName);
        List<Student> FindStudents(Course courseNumber);

        Group FindGroup(string groupName);
        List<Group> FindGroups(Course courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}