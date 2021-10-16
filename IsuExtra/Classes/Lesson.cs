using System;

namespace IsuExtra.Classes
{
    public class Lesson
    {
        public Lesson(
            string lessonName,
            DateTime lessonBegin,
            string lessonGroup,
            string lessonTeacher,
            int lessonAuditorium)
        {
            LessonName = lessonName;
            LessonBegin = lessonBegin;
            TimeSpan interval = new TimeSpan(1, 30, 0);
            LessonEnd = lessonBegin;
            LessonEnd.Add(interval);
            LessonGroup = lessonGroup;
            LessonTeacher = lessonTeacher;
            LessonAuditorium = lessonAuditorium;
        }

        public string LessonName { get; }
        public DateTime LessonBegin { get; }
        public DateTime LessonEnd { get; }
        public string LessonGroup { get; }
        public string LessonTeacher { get; }
        public int LessonAuditorium { get; }
    }
}