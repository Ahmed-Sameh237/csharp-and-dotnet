using System.Reflection.Metadata;

namespace StudentPortalConsole
{
    internal class Program
    {

        struct Student
        {
            public string FullName;
            public int Year;
            public double GPA;
        }
        static bool ValidateYear(int year)
        {
            return year >= 1 && year <= 4;
        }

        static bool ValidateGpa(double gpa)
        {
            return gpa >= 0 && gpa <= 4;
        }

        static string ClassifyYear(int year)
        {
            switch (year)
            {
                case 1:
                    return "Freshman";
                case 2:
                    return "Sophomore";
                case 3:
                    return "Junior";
                case 4:
                    return "Senior";
                default:
                    return "Unknown";
            }
        }

        static string ClassifyHonorStatus(double gpa)
        {
            if (gpa >= 3.7)
                return "High Honors";
            else if (gpa >= 3.0)
                return "Honors";
            else
                return "Regular";
        }

        static void PrintStudentSummary(Student student)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Name : {student.FullName}");
            Console.WriteLine($"Year : {ClassifyYear(student.Year)}");
            Console.WriteLine($"GPA  : {student.GPA:F2}");
            Console.WriteLine($"Status : {ClassifyHonorStatus(student.GPA)}");
        }

        

        struct Box { public int Value; }




        static void Main(string[] args)
        {

            //Box b1 = new Box { Value = 10 };
            //Box b2 = b1;
            //b2.Value = 99;
            //Console.WriteLine(b1.Value);  // value = 10 because here we pass by value ,so value wont change

            //int[] nums1 = { 10 };
            //int[] nums2 = nums1;
            //nums2[0] = 99;
            //Console.WriteLine(nums1[0]); // value = 99  because array works by refrence ,so it will change


            //static void Change(int x) { x = 999; }
            //int myVar = 5;
            //Change(myVar);
            //Console.WriteLine(myVar); // will not change it still out 5 because we passed the value of int not ref


            //static void ChangeArray(int[] arr) { arr[0] = 999; }
            //int[] data = { 5 };
            //ChangeArray(data);
            //Console.WriteLine(data[0]); // will change to 999 because we pass the whole array and array is passed by ref


            //int count = 3;
            //Student[] roster = new Student[3];
            //Student[] rosterCopy = roster;

            ////Stack                 //Heap
            ////count = 3
            ////roster = 0x0013       0x0013{default,default,default}
            ////rosterCopy = 0x0013   0x0013{,,}


            static void AdjustAllGpas(Student[] students, int count, double amount)
            {
                for (int i = 0; i < count; i++)
                {
                    students[i].GPA += amount;

                    if (students[i].GPA > 4.0)
                    {
                        students[i].GPA = 4.0;
                    }
                    else if (students[i].GPA < 0.0)
                    {
                        students[i].GPA = 0.0;
                    }
                }
            }

            //Student[] students = {
            //        new Student{ FullName = "Ahmed",Year = 1, GPA = 3.5},
            //        new Student{ FullName = "Hend", Year = 3 ,GPA = 3.9 },
            //        new Student{ FullName = "Hany", Year = 2 ,GPA = 0.2}
            //};
            //AdjustAllGpas(students, students.Length, 0.4);
            //for(int i = 0;i < students.Length; i++)
            //{
            //    Console.WriteLine($"{students[i].FullName} has GPA: {students[i].GPA}");
            //}




            static double FindHighestGpa(Student[] students, int count, out string topStudentName)
            {
                double maxGpa = students[0].GPA;
                topStudentName = students[0].FullName;
                for (int i = 0; i < count; i++)
                {
                    if (students[i].GPA > maxGpa)
                    {

                        maxGpa = students[i].GPA;
                        topStudentName = students[i].FullName;
                    }
                }

                return maxGpa;

            }
            //String topStudent;
            //double HighestGpa = FindHighestGpa(students, students.Length, out topStudent);

            //Console.WriteLine($"Highest Studnet GPA:{HighestGpa} and its name is {topStudent}");




            //AdjustAllGpas does not need ref because the parameter Student[] students is an array, and arrays in C# are a reference type. 
            //The reference to the array is passed by value, but it still points to the same array object on the heap. The reference variable itself is stored on the stack, 
            //while the array lives on the heap. When the function executes:







            Student[] students1 = new Student[3];
            int count = 0;

            while (true)
            {
                
                Console.WriteLine();
                Console.WriteLine("=== StudentPortal Menu ===");
                Console.WriteLine("1. Register a new student");
                Console.WriteLine("2. Adjust all GPAs by an amount");
                Console.WriteLine("3. Show the top student");
                Console.WriteLine("4. Print the full roster");
                Console.WriteLine("0. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        if (count >= students1.Length)
                        {
                            Console.WriteLine("Roster is full.");
                            break;
                        }

                        Console.Write("Name: ");
                        students1[count].FullName = Console.ReadLine();

                        int year;
                        do
                        {
                            Console.Write("Year (1-4): ");
                            year = int.Parse(Console.ReadLine());
                        }
                        while (!ValidateYear(year));

                        students1[count].Year = year;

                        double gpa;
                        do
                        {
                            Console.Write("GPA (0-4): ");
                            gpa = double.Parse(Console.ReadLine());
                        }
                        while (!ValidateGpa(gpa));

                        students1[count].GPA = gpa;

                        count++;
                        Console.WriteLine("Student registered.");
                        break;

                    case "2":

                        if (count == 0)
                        {
                            Console.WriteLine("No students registered.");
                            break;
                        }

                        Console.Write("Enter GPA adjustment amount: ");
                        double amount = double.Parse(Console.ReadLine());

                        AdjustAllGpas(students1, count, amount);

                        Console.WriteLine("All GPAs updated.");
                        break;

                    case "3":

                        if (count == 0)
                        {
                            Console.WriteLine("No students registered.");
                            break;
                        }

                        string topName;
                        double highest = FindHighestGpa(students1, count, out topName);

                        Console.WriteLine($"Top Student: {topName}");
                        Console.WriteLine($"Highest GPA: {highest:F2}");
                        break;

                    case "4":

                        if (count == 0)
                        {
                            Console.WriteLine("No students registered.");
                            break;
                        }

                        Console.WriteLine("===== Registered Students =====");

                        for (int i = 0; i < count; i++)
                        {
                            PrintStudentSummary(students1[i]);
                        }

                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }










        }
    }

}
