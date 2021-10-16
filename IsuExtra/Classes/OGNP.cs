using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class OGNP
    {
        public OGNP(string name, char faculty)
        {
            OGNPname = name;
            OGNPmegafaculty = faculty;
            OGNPflows = new List<Flow>();
        }

        public string OGNPname { get; }
        public char OGNPmegafaculty { get; }
        public List<Flow> OGNPflows { get; }
    }
}