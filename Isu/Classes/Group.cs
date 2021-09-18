using System;
using System.Collections.Generic;

namespace Isu.Classes
{
    public class Group
    {
        public Group(string groupName)
        {
            GroupName = groupName;
            Students = new List<Student>();
        }

        public string GroupName { get; }
        public List<Student> Students { get; }
        public int MaxStudents { get; } = 20;

        public static bool CheckGroupName(string groupName)
        {
            bool result = !(groupName.Length != 5 || groupName[0] != 'M' || groupName[1] != '3' ||
                            !(groupName[2] >= '1') || !(groupName[2] <= '4') || !char.IsDigit(groupName[3]) ||
                            !char.IsDigit(groupName[4]));
            return result;
        }
    }
}