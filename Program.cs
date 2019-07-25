using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SubjectProgram
{
    class Program
    {
        static readonly List<Subject> Subjects = new List<Subject>();

        static void Main(string[] args)
        {
            LoadInitialState();
            ShowWelcomeMessage();
            StringBuilder optionList = new StringBuilder();
            optionList.Insert(0, "1 - Add new subject").AppendLine().Append("2 - Show subject time").AppendLine()
                .Append("3- Add time to specific subject").AppendLine().Append("4 - Reset").AppendLine()
                .Append("5- Exit");
            while (true)
            {
                LoadOptionList(optionList);
            }
        }

        private static void LoadInitialState()
        {
            if (File.Exists(@"E:\DatabaseFile\SubjectsNames.txt"))
            {
                IEnumerable<string> subjectNames = File.ReadLines(@"E:\DatabaseFile\SubjectsNames.txt");
                foreach (String line in subjectNames)
                {
                    Subjects.Add(new Subject($"{line}"));
                }
            }
        }

        public static void ShowWelcomeMessage()
        {
            StringBuilder welcomeMSG = new StringBuilder();
            welcomeMSG.Insert(0, "================================================================================")
                .AppendLine()
                .Append("\t \t \t Welcome To Our Program").AppendLine()
                .Append("================================================================================")
                .AppendLine();
            Console.WriteLine(welcomeMSG);
        }

        public static void LoadOptionList(StringBuilder optionList)
        {
            Console.WriteLine(optionList + "\n");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    AddNewSubject();
                    break;
                case 2:
                    ShowSubjectsTimes();
                    break;
                case 3:
                    Console.Clear();
                    AddTimeToSpecificSubject();
                    break;
                case 4:
                    ResetDatabase();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("------------------------Enter a valid number-------------------- \n");
                    LoadOptionList(optionList);
                    break;
            }
        }

        private static void ResetDatabase()
        {
            FileStream namesFile = File.Open(@"E:\DatabaseFile\SubjectsNames.txt", FileMode.Open);
            namesFile.SetLength(0);
            namesFile.Close();
            FileStream timesFile = File.Open(@"E:\DatabaseFile\SubjectsTimes.txt", FileMode.Open);
            timesFile.SetLength(0);
            timesFile.Close();
            Subjects.Clear();
            Console.WriteLine("====================== Reset done successfully ======================");
        }


        public static void AddNewSubject()
        {
            Console.Clear();
            Console.WriteLine("Please Write Subject name : ");
            String subjectName = Console.ReadLine();
            if (!String.IsNullOrEmpty(subjectName))
            {
                DatabaseHelper.AddNewSubjectToDatabase(subjectName);
                Subjects.Add(new Subject($"{subjectName}"));
            }
            else
            {
                AddNewSubject();
            }
        }


        public static void ShowSubjectsTimes()
        {
            Console.Clear();
            if (!ShowSubjectsNames())
            {
                int numberOfTheSubject = Convert.ToInt32(Console.ReadLine()) - 1;
                if (numberOfTheSubject >= 0 && numberOfTheSubject < Subjects.Count)
                {
                    ShowSpecificSubjectTimes(numberOfTheSubject);
                }
                else
                {
                    Console.WriteLine("Enter A Valid Number");
                    ShowSubjectsTimes();
                }
            }
        }

        private static void ShowSpecificSubjectTimes(int numberOfTheSubject)
        {
            IEnumerable<string> dpFile = File.ReadLines(@"E:\DatabaseFile\SubjectsTimes.txt");
            bool foundAnyTime = false;
            foreach (String line in dpFile)
            {
                if (line.StartsWith(Subjects[numberOfTheSubject].GetName()))
                {
                    DoPrintDateInThisLine(line, Subjects[numberOfTheSubject].GetName().Length);
                    Console.WriteLine();
                    foundAnyTime = true;
                }
            }

            if (!foundAnyTime)
                Console.WriteLine(
                    "====================== There is No times saved in this subject yet ======================");

            Console.ReadLine();
            Console.Clear();
        }

        private static void DoPrintDateInThisLine(string line, int lengthOfTheSubject)
        {
            for (int i = lengthOfTheSubject + 3; i < line.Length; i++)
            {
                Console.Write(line[i]);
            }
        }

        public static bool ShowSubjectsNames()
        {
            Console.WriteLine("Subjects is :");
            if (Subjects.Count > 0)
            {
                for (int i = 0; i < Subjects.Count; i++)
                {
                    Console.WriteLine($"{i + 1}- {Subjects[i].GetName()}");
                }

                return false;
            }
            else
            {
                Console.WriteLine("There are No Subjects added to database. Please add some");
                return true;
            }
        }


        [SuppressMessage("ReSharper", "ReplaceWithSingleAssignment.False")]
        public static void AddTimeToSpecificSubject()
        {
            if (!ShowSubjectsNames())
            {
                int numberOfTheSubject = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Clear();
                Console.WriteLine(" Add Time in This Form *Only* : 04/06/2011 13:10:04 \n Wrtie 00 to get back ");
                String newTime = Console.ReadLine();
                bool timeValid = false;

                if (newTime == "00")
                {
                    timeValid = true;
                }

                while (!timeValid)
                {
                    try
                    {
                        Subjects[numberOfTheSubject].AddTime(newTime);
                        DatabaseHelper.Save(
                            $"{Subjects[numberOfTheSubject].GetName()} : {Subjects[numberOfTheSubject].getTimes()[Subjects[numberOfTheSubject].getTimes().Count - 1]}");
                        timeValid = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Enter a valid time format....");
                        newTime = Console.ReadLine();
                        if (newTime == "00")
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
