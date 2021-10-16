using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Classes;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<OGNP> _ognp = new List<OGNP>();
        private Dictionary<string, List<Lesson>> _timetables = new Dictionary<string, List<Lesson>>();

        private IsuService _isuService = new IsuService();
        public OGNP AddOGNP(string name, char faculty)
        {
            var newOGNP = new OGNP(name, faculty);
            _ognp.Add(newOGNP);
            return newOGNP;
        }

        public void AddFlowOGNP(Flow flow, OGNP ognp)
        {
            ognp.OGNPflows.Add(flow);
        }

        public void AddGroupFlow(Group group, Flow flow)
        {
            flow.FlowGroups.Add(group);
        }

        public void AddTimetable(Group group, List<Lesson> timetable)
        {
            _timetables.Add(group.GroupName, timetable);
        }

        public bool CheckTimetablesOverlap(List<Lesson> mainTimetable, List<Lesson> extraTimetable)
        {
            foreach (Lesson lesson in mainTimetable)
            {
                foreach (Lesson anotherLesson in extraTimetable)
                {
                    if (lesson.LessonBegin == anotherLesson.LessonBegin)
                        return false;
                }
            }

            return true;
        }

        public void EnrollStudent(Student student, OGNP ognp)
        {
            if (student.GroupName[0] == ognp.OGNPmegafaculty)
                throw new IsuExtraException("Student can't record on disciplines own megafaculty.");
            foreach (Flow currentFlow in ognp.OGNPflows)
            {
                List<Lesson> mainTimetable = _timetables[student.GroupName];
                foreach (var currentGroup in currentFlow.FlowGroups)
                {
                    List<Lesson> extraTimetable = _timetables[currentGroup.GroupName];
                    if (CheckTimetablesOverlap(mainTimetable, extraTimetable))
                    {
                        if (currentGroup.Students.Count < currentGroup.MaxStudents)
                        {
                            currentGroup.Students.Add(student);
                            return;
                        }
                    }
                }
            }

            throw new IsuExtraException("A student can't enroll in this OGNP course because" +
                                            "there are not enough places on flows or course timetable intersects" +
                                            "with the main timetable of student.");
        }

        public Student CanselStudentEnroll(Student student, OGNP ognp)
        {
            foreach (Flow currentFlow in ognp.OGNPflows)
            {
                foreach (Group currentGroup in currentFlow.FlowGroups)
                {
                    foreach (Student currentStudent in currentGroup.Students)
                    {
                        if (currentStudent == student)
                        {
                            currentGroup.Students.Remove(student);
                            return student;
                        }
                    }
                }
            }

            return null;
        }

        public List<Flow> ReturnFlows(OGNP ognp)
        {
            foreach (OGNP currentOGNP in _ognp)
            {
                if (currentOGNP == ognp)
                {
                    return currentOGNP.OGNPflows;
                }
            }

            return null;
        }

        public List<Student> ReturnStudents(string groupName)
        {
            foreach (OGNP currentOGNP in _ognp)
            {
                foreach (Flow currentFlow in currentOGNP.OGNPflows)
                {
                    foreach (Group currentGroup in currentFlow.FlowGroups)
                    {
                        if (currentGroup.GroupName == groupName)
                        {
                            return currentGroup.Students;
                        }
                    }
                }
            }

            return null;
        }

        public bool CheckStudentOGNP(Student student)
        {
            foreach (OGNP currentOGNP in _ognp)
            {
                foreach (Flow currentFlow in currentOGNP.OGNPflows)
                {
                    foreach (Group currentGroup in currentFlow.FlowGroups)
                    {
                        foreach (Student currentStudent in currentGroup.Students)
                        {
                            if (student == currentStudent)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public List<Student> ReturnStudentsWithoutOGNP()
        {
            var studentsWithoutOGNP = new List<Student>();
            List<Course> courses = _isuService.GetCourses();
            foreach (Course currentCourse in courses)
            {
                foreach (Group currentGroup in currentCourse.Groups)
                {
                    foreach (Student currentStudent in currentGroup.Students)
                    {
                        if (!CheckStudentOGNP(currentStudent))
                            studentsWithoutOGNP.Add(currentStudent);
                    }
                }
            }

            return studentsWithoutOGNP;
        }
    }
}