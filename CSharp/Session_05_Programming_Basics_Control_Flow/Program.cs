using System.Collections.Concurrent;

namespace StudentPortalConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =====================================================================
            // StudentPortalConsole — TODO GUIDE ONLY (Style Guide Rule 20)
            // ITI Summer Training | Web Development Using .NET | Morning Group
            // Session 05 — Programming Basics & Control Flow
            //
            // This file holds VERBAL/TODO guidance only — NOT working code. Build
            // this yourself, from scratch, following the TODOs below in order.
            // Every pattern here was demonstrated live in today's lecture — the
            // Student Guide has the exact worked examples if you get stuck.
            //
            // For the full, correct, runnable version (do NOT peek until you've
            // tried it yourself, or you're using it to check your own work), see:
            // ../StudentPortalConsole_Complete/Program.cs
            // =====================================================================

            // TODO 1: Declare an int variable to track the total number of students
            //         registered this run. Start it at 0.

            //string fullName = "Yasmin Adly";
            //int yearsOfStudy = 2;
            //double gpa = 3.5;
            //char gradeLetter = 'A';
            //bool IsHonorRoll = true;
            //yearsOfStudy = 3.8;
            //Console.WriteLine(fullName);
            //Console.WriteLine(yearsOfStudy);
            //Console.WriteLine(gpa);


            //int a = 5, b = 3, c = 4;

            //int sum = a + b;
            //int remainder = a % c;
            //bool isPassing = gpa >= 2.0;
            //bool isTopStudent = gpa >= 3.5 && IsHonorRoll;
            //gpa = 3.8; gpa == 3.8;

            //bool isOdd = a % 2 != 0;
            //Console.Write(sum + " ");
            //Console.Write(remainder + " ");
            //Console.Write(isPassing + " ");
            //Console.Write(isTopStudent + " ");
            //Console.Write(isOdd + " ");

            //Console.Write("Enter the Student's Full Name: ");
            //string enteredName = Console.ReadLine();
            //Console.Write("\nEnter the Student's GPA:");
            //string inputGpa = Console.ReadLine();
            //double gpaEntered = double.Parse(inputGpa);

            //Console.WriteLine(enteredName + " Has a GPA of " + gpaEntered); // concatenation (Old way)
            //Console.WriteLine($"{enteredName} Has a GPA of {gpaEntered}"); // string interpolation 

            //if (gpaEntered >= 3.5)
            //{
            //    Console.WriteLine("Top Students On Class");
            //}
            //else if (gpaEntered >= 3.0)
            //{
            //    Console.WriteLine("Honor Roll");
            //}
            //else
            //{
            //    Console.WriteLine("Regular Student");
            //}

            //string yearLabel;
            //yearsOfStudy = 40;
            //switch (yearsOfStudy)
            //{
            //    case 1: yearLabel = "Freshman"; break;
            //    case 2: yearLabel = "Sophomore"; break;
            //    case 3: yearLabel = "Junior"; break;
            //    case 4: yearLabel = "Senior"; break;
            //    default:
            //        yearLabel = "Unknown";
            //        break;
            //}

            //Console.WriteLine(yearLabel);

            //for (int i = 1; i <= 5; i++)
            //{
            //    Console.WriteLine($"Session {i} of 10");
            //}

            //int validatedYear = 0;
            //bool isValidYear = false;
            //while (!isValidYear)
            //{
            //    Console.WriteLine("Enter year of study (1-4):");
            //    string yearInput = Console.ReadLine();
            //    isValidYear = int.TryParse(yearInput, out validatedYear) && validatedYear >= 1 && validatedYear <= 4;
            //    //if (!isValidYear || validatedYear < 1 || validatedYear > 4)
            //    //{
            //    //    Console.WriteLine("This is not a valid year of study . Please try again.");
            //    //    isValidYear = false;
            //    //}
            //    if (!isValidYear)
            //    {
            //        Console.WriteLine("This is not a valid year of study . Please try again.");
            //    }
            //}
            //Console.WriteLine($"Year of study is {validatedYear}");

            // TODO 2: Start a do-while loop — this is the OUTER loop that lets the
            //         user register as many students as they want, one after
            //         another. It must run at least once (Block 5's do-while
            //         pattern).

            //string registerAnother;
            //int totalRegistered = 0;

            //do
            //{
            //    Console.WriteLine("Registering a new student...");
            //    totalRegistered++;

            //    Console.WriteLine("Register Another ? (y/n)");
            //    registerAnother = Console.ReadLine();

            //} while (registerAnother == "y" || registerAnother == "Y");
            //Console.WriteLine($"Total Registered: {totalRegistered}");

            // i = i+1 , i++ ? 
            // i++ , ++i? -- Postfix , Prefix

            //int number = 5;
            //int number2 = ++number;
            //Console.WriteLine($"number = {number} , number2 = {number2}");

            //   TODO 2a: Print a prompt asking for the student's full name, then
            //            read it with Console.ReadLine() into a string variable.



            //   TODO 2b: Validate the year of study using a WHILE loop (Block 5's
            //            while + TryParse pattern):
            //            - Print a prompt: "Enter year of study (1-4):"
            //            - Read the input as a string
            //            - Use int.TryParse to attempt converting it to an int
            //            - The loop condition must check BOTH that TryParse
            //              succeeded AND that the resulting value is between 1
            //              and 4 inclusive
            //            - If invalid, print an error message and loop again

            //   TODO 2c: Validate the GPA the same way, using a WHILE loop +
            //            double.TryParse, requiring the value to be between 0.0
            //            and 4.0 inclusive.

            //   TODO 2d: Classify the year of study into a class name using a
            //            SWITCH statement (Block 4's switch pattern):
            //            1 -> "Freshman", 2 -> "Sophomore", 3 -> "Junior",
            //            4 -> "Senior", default -> "Unknown Year"
            //            Store the result in a string variable.

            //   TODO 2e: Classify the GPA into an honor status using IF / ELSE IF
            //            / ELSE (Block 4's pattern):
            //            >= 3.5 -> "Dean's List"
            //            >= 3.0 (but < 3.5) -> "Honor Roll"
            //            otherwise -> "Standard Standing"
            //            Store the result in a string variable.

            //   TODO 2f: Print a one-line summary for this student using string
            //            interpolation ($"...") — name, year label, GPA, and honor
            //            status, all on one readable line.

            //   TODO 2g: Increment your total-registered counter from TODO 1.

            //   TODO 2h: Print "Register another? (y/n)" and read the answer into
            //            the do-while loop's condition variable.

            // TODO 3: After the loop ends (user typed anything other than "y"),
            //         print the final total number of students registered this
            //         run, using string interpolation.

            int totalRegistered = 0;
            string registerAnother;
            do
            {
                // Name
                Console.WriteLine("Enter the student's full name: ");
                string fullName = Console.ReadLine() ?? "";

                // Year Of Study , validate
                int yearOfStudy = 0;
                bool isValidYear = false;
                while (!isValidYear)
                {
                    Console.WriteLine("Enter the student's year of study (1-4): ");
                    string yearInput = Console.ReadLine() ?? "";
                    isValidYear = int.TryParse(yearInput, out yearOfStudy) && yearOfStudy >= 1 && yearOfStudy <= 4;
                    if (!isValidYear)
                    {
                        Console.WriteLine("That is not a valid year . Please try again");
                    }
                }
                // GPA , validate

                double gpa = 0;
                bool isValidGpa = false;
                while (!isValidGpa)
                {
                    Console.WriteLine("Enter the student's GPA: (0.0-4.0)");
                    string gpaInput = Console.ReadLine() ?? "";
                    isValidGpa = double.TryParse(gpaInput, out gpa) && gpa >= 0.0 && gpa <= 4.0;
                    if (!isValidGpa)
                    {
                        Console.WriteLine("That is not a valid GPA . Please try again");
                    }
                }

                //  Classify Year

                string yearLabel;
                switch (yearOfStudy)
                {
                    case 1: yearLabel = "Freshman"; break;
                    case 2: yearLabel = "Sophomore"; break;
                    case 3: yearLabel = "Junior"; break;
                    case 4: yearLabel = "Senior"; break;
                    default:
                        yearLabel = "Unknown Year";
                        break;
                }

                // Honor status Dean's list , honor , regular

                string honorStatus;
                if (gpa >= 3.5)
                {
                    honorStatus = "Dean's List";
                }
                else if (gpa >= 3.0)
                {
                    honorStatus = "Honor Roll";
                }
                else
                {
                    honorStatus = "Standard Standing";
                }

                // Print summary 
                Console.WriteLine($"{fullName} - {yearLabel} , GPA {gpa} , Honor Status {honorStatus}");
                totalRegistered++;
                Console.WriteLine("Register another? (y/n)");
                registerAnother = Console.ReadLine() ?? "";
            } while (registerAnother == "y");

            Console.WriteLine($"Total registered: {totalRegistered}");
        }
    }
}

