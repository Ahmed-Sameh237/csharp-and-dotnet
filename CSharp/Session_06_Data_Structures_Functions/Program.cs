namespace StudentPortalConsole
{
    internal class Program
    {

        struct Student
        {
            public string Name;
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
            Console.WriteLine($"Name : {student.Name}");
            Console.WriteLine($"Year : {ClassifyYear(student.Year)}");
            Console.WriteLine($"GPA  : {student.GPA:F2}");
            Console.WriteLine($"Status : {ClassifyHonorStatus(student.GPA)}");
        }



        // -------------------------- PART B ------------------------

        struct Course
        {
            public String Title;
            public int Credits;
            public bool isRequired;
        };

        static String ClassifyCourseLevel(int credits)
        {
            String level;
            if (credits >= 4)
                level = "Advanced";
            else if (credits >= 2)
                level = "Intermediate";
            else
                level = "Introductory";
            return level;
        }


        static void DoubleInPlace(ref int number)
        {
            number *= 2;
        }


        static void GetMinMax(int a, int b, out int min, out int max)
        {
            if (a < b)
            {
                min = a;
                max = b;
            }
            else
            {
                min = b;
                max = a;
            }
        }





        static void Main(string[] args)
        {
            //Course[] courses = {
            //    new Course { Title = "C#", Credits = 3, isRequired = true},
            //    new Course { Title = "C++", Credits = 5, isRequired = true},
            //    new Course { Title = "Java", Credits = 6, isRequired = false}
            //};

            //for (int i = 0; i < courses.Length; i++)
            //    Console.WriteLine($"Course {i + 1}: {courses[i].Title}, Credits: {courses[i].Credits}, Needed: {courses[i].isRequired} \n");



            //String raw = "  database fundamentals  ";
            //String raw_trim = raw.Trim();
            //String raw_comparison = raw_trim.ToUpper();
            //Console.WriteLine(raw_trim);
            //Console.WriteLine(raw_comparison);

            //String blank = "  ";
            //bool is_invalid = String.IsNullOrWhiteSpace(blank);
            //Console.WriteLine($"Blank input is invalid: {is_invalid}");



            //Console.WriteLine($"\n{courses[0].Title} has credits {courses[0].Credits} and its level is {ClassifyCourseLevel(courses[0].Credits)}");
            //Console.WriteLine($"{courses[1].Title} has credits {courses[1].Credits} and its level is {ClassifyCourseLevel(courses[1].Credits)}\n");


            //var number = 10;
            //DoubleInPlace(ref number);
            //Console.WriteLine($"Number after Double with ref:{number}");

            //GetMinMax(10, 5,out int min,out int max);
            //Console.WriteLine($"Min = {min}");
            //Console.WriteLine($"Max = {max}");





            Student[] students = new Student[3];

            int index = 0;

            do
            {
                Console.WriteLine($"Student #{index + 1}");

                Console.Write("Name: ");
                students[index].Name = Console.ReadLine();

                int year;

                do
                {
                    Console.Write("Year (1-4): ");
                    year = int.Parse(Console.ReadLine());

                } while (!ValidateYear(year));

                students[index].Year = year;

                double gpa;

                do
                {
                    Console.Write("GPA (0-4): ");
                    gpa = double.Parse(Console.ReadLine());

                } while (!ValidateGpa(gpa));

                students[index].GPA = gpa;

                index++;

            } while (index < students.Length);

            Console.WriteLine();
            Console.WriteLine("===== Registered Students =====");

            for (int i = 0; i < students.Length; i++)
            {
                PrintStudentSummary(students[i]);
            }
        }




    }
    
}
