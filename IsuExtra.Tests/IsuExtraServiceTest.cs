using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Classes;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtraService;
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _isuService = new IsuService();
        }

        [Test]
        public void CreateNewOGNP_AddStudent_CheckEnroll()
        {
            Group mGroup = _isuService.AddGroup("M3205");
            Student mStudent = _isuService.AddStudent(mGroup, "Stepan Volkov");
            OGNP pOGNP = _isuExtraService.AddOGNP("Computer Technology", 'P');
            var pFlow = new Flow("CT-1", 100);
            Group pGroup = new Group("P3001");
            _isuExtraService.AddGroupFlow(pGroup, pFlow);
            _isuExtraService.AddFlowOGNP(pFlow, pOGNP);
            
            List<Lesson> mainTimetable = new List<Lesson>();
            Lesson lesson = new Lesson("Math", DateTime.Parse("2021-09-19 10:00:00"),
                mGroup.GroupName, "Maxim Maximov", 309);
            mainTimetable.Add(lesson);
            _isuExtraService.AddTimetable(mGroup, mainTimetable);
            
            List<Lesson> extraTimetable = new List<Lesson>();
            lesson = new Lesson("Programming", DateTime.Parse("2021-09-18 11:40:00"),
                pGroup.GroupName, "Pavel Pavlov", 314);
            extraTimetable.Add(lesson);
            
            _isuExtraService.AddTimetable(pGroup, extraTimetable);
            _isuExtraService.EnrollStudent(mStudent, pOGNP);
            Assert.Contains(mStudent, pGroup.Students);
        }

        [Test]
        public void CreateNewOGNP_AddStudent_CanselEnroll()
        {
            Group mGroup = _isuService.AddGroup("M3205");
            Student mStudent = _isuService.AddStudent(mGroup, "Stepan Volkov");
            OGNP pOGNP = _isuExtraService.AddOGNP("Computer Technology", 'P');
            var pFlow = new Flow("CT-1", 100);
            Group pGroup = new Group("P3001");
            _isuExtraService.AddGroupFlow(pGroup, pFlow);
            _isuExtraService.AddFlowOGNP(pFlow, pOGNP);
            
            List<Lesson> mainTimetable = new List<Lesson>();
            Lesson lesson = new Lesson("Math", DateTime.Parse("2021-09-19 10:00:00"),
                mGroup.GroupName, "Maxim Maximov", 309);
            mainTimetable.Add(lesson);
            _isuExtraService.AddTimetable(mGroup, mainTimetable);
            
            List<Lesson> extraTimetable = new List<Lesson>();
            lesson = new Lesson("Programming", DateTime.Parse("2021-09-18 11:40:00"),
                pGroup.GroupName, "Pavel Pavlov", 314);
            extraTimetable.Add(lesson);
            
            _isuExtraService.AddTimetable(pGroup, extraTimetable);
            _isuExtraService.EnrollStudent(mStudent, pOGNP);
            _isuExtraService.CanselStudentEnroll(mStudent, pOGNP);
            List<Student> students = _isuExtraService.ReturnStudentsWithoutOGNP();
            Assert.AreEqual(students.Count, 0);
        }

        [Test]
        public void CreateNewOGNP_ReturnFlows()
        {
            Group mGroup = _isuService.AddGroup("M3205");
            Student mStudent = _isuService.AddStudent(mGroup, "Stepan Volkov");
            OGNP pOGNP = _isuExtraService.AddOGNP("Computer Technology", 'P');
            var pFlow = new Flow("CT-1", 100);
            Group pGroup = new Group("P3001");
            _isuExtraService.AddGroupFlow(pGroup, pFlow);
            _isuExtraService.AddFlowOGNP(pFlow, pOGNP);
            Assert.AreEqual(_isuExtraService.ReturnFlows(pOGNP).Count, 1);
        }

        [Test]
        public void CreateNewOGNP_TryEnrollStudentOnOwnMegafaculty_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                Group pGroup = _isuService.AddGroup("P3267");
                Student pStudent = _isuService.AddStudent(pGroup, "Agata Polozkova");
                OGNP pOGNP = _isuExtraService.AddOGNP("Computer Technology", 'P');
                var pFlow = new Flow("CT-1", 100);
                Group pGroupOGNP = new Group("P3001");
                _isuExtraService.AddGroupFlow(pGroup, pFlow);
                _isuExtraService.AddFlowOGNP(pFlow, pOGNP);
                _isuExtraService.EnrollStudent(pStudent, pOGNP);
            });
        }
    }
}