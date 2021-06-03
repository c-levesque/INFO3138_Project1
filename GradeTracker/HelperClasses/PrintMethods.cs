/**
	 * Class Name:  PrintMethods.cs
	 * Purpose:		Static class which holds my console printing methods
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using System;
using System.IO;
using GradeTracker.Classes;
using GradeTracker.Menu;


namespace GradeTracker.HelperClasses
{
    public static class PrintMethods
    {
        /*
            --- Constants ---
        */
        private const int SHORT_SPACE = 5;
        private const int SPACE = 15;
        private const int COURSE_SPACE = 12;

        /*
            --- Public Methods ---
        */
        public static void BlankLine()
        {
            /*Method Name:  BlankLine
            *Purpose:       Wrapper for Console.WriteLine() ( for readability ).
            *Accepts:       void
            *Returns:       void
            */
            Console.WriteLine();
        }
        public static void CreateMainBox(string display, int dashes)
        {
            /*Method Name:  CreateMainBox
            *Purpose:       Writes all the data on screen in a nice format.
            *Accepts:       string (display title), int (number of dashes)
            *Returns:       
            */
            const string intro = " ~ GRADES TRACKING SYTEM ~";
            int spaces = (dashes - display.Length) / 2;

            DisplayTop(intro, spaces);
            DisplayBox(display, spaces, dashes);
            DisplayBottom(dashes);
        }
        public static void DisplayCourses()
        {
            /*Method Name:  DisplayCourses
            *Purpose:       Displays all courses from the static CourseList in a nice format.
            *Accepts:       void
            *Returns:       void
            */
            int courseCount = 1;
            
            // if no courses in the list
            if (CourseList._list.Count == 0)
            {
                Console.WriteLine("\n\nThere are currently no saved courses.");
            }
            else
            {
                // print course property titles
                BlankLine();
                Console.WriteLine($"{" #. ", SHORT_SPACE}{"Course", -SPACE}{"Marks Earned", SPACE}{"Out Of", SPACE}{"Percent", SPACE}");
                BlankLine();

                // print out each courses data formatted
                foreach (Course c in CourseList._list)
                {
                    c.CalculateMarks();
                    Console.WriteLine(
                        $"{$"{courseCount}. ", SHORT_SPACE}" +
                        $"{$"{c.Code}", -SPACE}" +
                        $"{$"{c.EarnedMarks:F1}", SPACE}" +
                        $"{$"{c.OutOf:F1}", SPACE}" +
                        $"{$"{c.Percent:F1}", SPACE}" +
                        $"");
                    courseCount++;
                }
            }
        }
        public static void DisplayCourseEvaluations(int selection)
        {
            /*Method Name:  DisplayCourseEvaluations
            *Purpose:       Displays a courses evaluations list from the static CourseList in a nice format.
            *Accepts:       void
            *Returns:       void
            */
            int evaluation_count;

            if (CourseList._list[selection].Evaluations.Count == 0)
            {
                Console.WriteLine($"\n\nThere are currently no evaluations for {CourseList._list[selection].Code}.");
            }
            else
            {
                evaluation_count = 1;

                BlankLine();
                Console.WriteLine(
                    $"{"  #.", -SHORT_SPACE}" +
                    $"{"Evaluation", -SPACE}" +
                    $"{"Marks Earned", SPACE}" +
                    $"{"Out Of", COURSE_SPACE}" +
                    $"{"Percent", COURSE_SPACE}" +
                    $"{"Course Marks", SPACE}" +
                    $"{"Weight/100", COURSE_SPACE}");
                BlankLine();

                foreach (Evaluation e in CourseList._list[selection].Evaluations)
                {
                    Console.WriteLine(
                        $"{evaluation_count + ". ",SHORT_SPACE}" +
                        $"{$"{e.Description}",-SPACE}" +
                        $"{$"{e.EarnedMarks:F1}",SPACE}" +
                        $"{$"{e.OutOf:F1}",COURSE_SPACE}" +
                        $"{$"{e.Percent:F1}",COURSE_SPACE}" +
                        $"{$"{e.CourseMarks:F1}",SPACE}" +
                        $"{$"{e.Weight:F1}",COURSE_SPACE}");
                    evaluation_count++;
                }
            }
        }
        public static void DisplayEvaluation(int courseSelection, int evaluationSelection)
        {
            /*Method Name:  DisplayEvaluation
            *Purpose:       Displays Evaluation details in a nice format.
            *Accepts:       int (course index), int (evaluation index)
            *Returns:       void
            */
            Evaluation temp = CourseList._list[courseSelection].Evaluations[evaluationSelection];

            BlankLine();
            Console.WriteLine(
                $"{"Marks Earned",SPACE}" +
                $"{"Out Of",COURSE_SPACE}" +
                $"{"Percent",COURSE_SPACE}" +
                $"{"Course Marks",SPACE}" +
                $"{"Weight/100",COURSE_SPACE}"
                );
            BlankLine();

            Console.WriteLine(
              $"{$"{temp.EarnedMarks:F1}",SPACE}" +
              $"{$"{temp.OutOf:F1}",COURSE_SPACE}" +
              $"{$"{temp.Percent:F1}",COURSE_SPACE}" +
              $"{$"{temp.CourseMarks:F1}",SPACE}" +
              $"{$"{temp.Weight:F1}",COURSE_SPACE}"
              );
            BlankLine();
        }
        public static void DisplayFileCreate()
        {
            /*Method Name:  DisplayFileCreate
            *Purpose:       Displays the fileCreate menu when grades.json isnt found.
            *Accepts:       void
            *Returns:       void
            */
            HelperMethods.PromptUser($"Grades data file {Program.COURSE_DATA_FILE} not found. Create new file? (y/n): ");
            string input = HelperMethods.GetUserSelection();
            
            if (input == "Y")
            {
                // create file and close so we can write to it later
                FileStream fs = File.Create(Program.COURSE_DATA_FILE);
                fs.Close();

                HelperMethods.PromptUser("\nNew data set created. Press enter to continue...");
                input = HelperMethods.GetUserSelection();
                Console.Clear();

                // display main menu
                MainMenu.Display();
            }
            else
            {
                System.Environment.Exit(1);
            }

        }
        public static void LoopChar(string s, int amount)
        {
            /*Method Name:  LoopChar    
            *Purpose:       Writes a char (or string) in a loop
            *Accepts:       string (char), int (amount of times to loop)
            *Returns:       void
            */
            int index;

            for (index = 0; index < amount; index++)
            {
                Console.Write(s);
            }
        }
        public static void PrintChar(string s, int amount)
        {
            /*Method Name:  PrintChar
            *Purpose:       LoopChar wrapper with formatted +'s at the start and end.
            *Accepts:       string (char), int ( amount of times to loop)
            *Returns:       void
            */
            Console.Write("+");
            LoopChar(s, amount);
            Console.Write("+");
        }

        /*
            --- Private Methods ---
        */
        private static void DisplayBottom(int dashes)
        {
            /*Method Name:  DisplayBottom
            *Purpose:       prints the +----+ container for the console output ( bottom )
            *Accepts:       int (amount of dashes)
            *Returns:       void
            */
            Console.Write("|\n");
            PrintChar("-", dashes);
            BlankLine();
        }
        private static void DisplayBox(string display, int spaces, int dashes)
        {
            /*Method Name:  DisplayBox
            *Purpose:       Displays the entire console box with name of course or evaluation etc
            *Accepts:       string (message in box), int (spaces amount), int (dashes amount)
            *Returns:       void
            */
            bool isEven = display.Length % 2 == 0 ? true : false;
            PrintChar("-", dashes);
            BlankLine();
            Console.Write("|");
            if (!isEven)
            {
                spaces++;
            }
            LoopChar(" ", spaces);
            Console.Write(display);
            if (!isEven)
            {
                spaces--;
            }
            LoopChar(" ", spaces);
        }
        private static void DisplayTop(string intro, int spaces)
        {
            /*Method Name:  DisplayTop
            *Purpose:       Displays program title.
            *Accepts:       string (title), int (spaces to center title)
            *Returns:       void
            */
            BlankLine();
            LoopChar(" ", (spaces / 2) + (intro.Length / 2));
            Console.Write(intro);
            BlankLine();
            BlankLine();
        }
    }
}
