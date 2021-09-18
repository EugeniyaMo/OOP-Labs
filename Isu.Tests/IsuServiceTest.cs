using System;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3101");
            Student student = _isuService.AddStudent(group, "Ivan Petrov");
            Assert.Contains(student, group.Students);
        }
        
        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3102");
                for (int i = 0; i <= 21; i++)
                {
                    _isuService.AddStudent(group, "Petr Ivanov");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3607");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group currentGroup = _isuService.AddGroup("M3101");
            Group newGroup = _isuService.AddGroup("M3106");
            Student student = _isuService.AddStudent(currentGroup, "Semen Fedorov");
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.Contains(student, newGroup.Students);
            Assert.AreEqual(currentGroup.Students.Count, 0);
        }
    }
}