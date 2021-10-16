using System.Collections.Generic;
using Isu.Classes;
using Isu.Services;
using IsuExtra.Classes;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        OGNP AddOGNP(string name, char faculty);
        void AddFlowOGNP(Flow flow, OGNP ognp);
        void AddGroupFlow(Group group, Flow flow);
        void AddTimetable(Group group, List<Lesson> timetable);
        bool CheckTimetablesOverlap(List<Lesson> mainTimetable, List<Lesson> extraTimetable);
        void EnrollStudent(Student student, OGNP ognp);
        Student CanselStudentEnroll(Student student, OGNP ognp);
        List<Flow> ReturnFlows(OGNP ognp);
        List<Student> ReturnStudents(string groupName);
        bool CheckStudentOGNP(Student student);
        List<Student> ReturnStudentsWithoutOGNP();
    }
}