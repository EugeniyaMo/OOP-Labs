using System.Collections.Generic;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class Flow
    {
        public Flow(string flowName, int flowMaxStudents)
        {
            FlowName = flowName;
            FlowMaxStudents = flowMaxStudents;
            FlowGroups = new List<Group>();
        }

        public string FlowName { get; }
        public int FlowMaxStudents { get; }
        public List<Group> FlowGroups { get; }
    }
}