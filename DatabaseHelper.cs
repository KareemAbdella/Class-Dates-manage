using System;
using System.IO;

namespace SubjectProgram
{
    class DatabaseHelper
    {
        public static void AddNewSubjectToDatabase(string subjectName)
        {
            string path = @"E:\DatabaseFile\SubjectsNames.txt";

            using (StreamWriter file =
                new StreamWriter(path, true))
            {
                file.WriteLine(subjectName);
                Console.Clear();
                Console.WriteLine("====================== Subject added successfully ======================");
            }
        }
        public static void Save(String newLesson)
        {
            if (File.ReadAllText(@"E:\DatabaseFile\SubjectsTimes.txt").Contains(newLesson))
            {
                Console.Clear();
                Console.WriteLine("====================== Time is already exist ======================");
            }
            else
            {
                using (StreamWriter file =
                    new StreamWriter(@"E:\DatabaseFile\SubjectsTimes.txt", true))
                {
                    file.WriteLine(newLesson);
                }

                Console.Clear();
                Console.WriteLine(
                    "====================== Time added successfully ======================");
            }
        }

    }
}
