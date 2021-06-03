/**
	 * Class Name:  ParseMethods.cs
	 * Purpose:		Static class which holds my input parsing methods.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using System;
using GradeTracker.Classes;
using GradeTracker.Menu;

namespace GradeTracker.HelperClasses
{
    public static class ParseMethods
    {
        /*
            --- Public Methods ---
        */
        public static void ParseCourseInput(string input, int selection)
        {
            /*Method Name:  ParseCourseInput
            *Purpose:       Parses the input at the Course menu and directs further action
            *Accepts:       string (user input), int (course selection)
            *Returns:       void
            */
            if (int.TryParse(input, out var parsedSelection))
            {
                if(parsedSelection <= 0 || parsedSelection > CourseList._list[selection].Evaluations.Count)
                {
                    Error.Message("Incorrect input, try again..");
                    PrintMethods.BlankLine();
                    HelperMethods.PromptUser("Enter a command: ");
                    input = HelperMethods.GetUserSelection();
                    ParseCourseInput(input, selection);
                }
                else
                {
                    Console.Clear();
                    EvaluationMenu.Display(selection, --parsedSelection);
                }
            }
            else
            {
                switch (input)
                {
                    case "A":
                        if(CrudMethods.AddEvaluation(selection))
                        {
                            Console.Clear();
                            CourseMenu.Display(selection);
                            break;
                        }
                        else
                        {
                            HelperMethods.PromptUser("Enter a command: ");
                            string courseInput = HelperMethods.GetUserSelection();
                            ParseCourseInput(courseInput, selection);
                            break;
                        }

                    case "D":
                        CrudMethods.DeleteCourse(selection);
                        Console.Clear();
                        MainMenu.Display();
                        break;

                    case "X":
                        Console.Clear();
                        MainMenu.Display();
                        break;

                    default:
                        Error.Message("Incorrect input, try again..");
                        Console.WriteLine();
                        HelperMethods.PromptUser("Enter a command: ");
                        string newInput = HelperMethods.GetUserSelection();
                        ParseCourseInput(newInput, selection); break;
                }
            }

        }
        public static void ParseEvaluationInput(string input, int courseSelection, int evaluationSelection)
        {
            /*Method Name:  ParseEvaluationInput
            *Purpose:       Parses user input at the Evaluation menu to direct further action
            *Accepts:       string (user input), int (course index), int (evaluation index)
            *Returns:       void
            */
            switch (input)
            {
                case "D":
                    CrudMethods.DeleteEvaluation(courseSelection, evaluationSelection);
                    Console.Clear();
                    CourseMenu.Display(courseSelection);
                    break;

                case "E":
                    CrudMethods.EditEvaluation(courseSelection, evaluationSelection);
                    Console.Clear();
                    CourseMenu.Display(courseSelection);
                    break;

                case "X":
                    Console.Clear();
                    CourseMenu.Display(courseSelection);
                    break;

                default:
                    Error.Message("Incorrect input, try again..");
                    PrintMethods.BlankLine();
                    HelperMethods.PromptUser("Enter a command: ");
                    string newInput = HelperMethods.GetUserSelection();
                    ParseEvaluationInput(newInput, courseSelection, evaluationSelection); break;
            }
        }
        public static void ParseMainInput(string input)
        {
            /*Method Name:  ParseMainInput
            *Purpose:       Parses user input at the MainMenu to direct further action
            *Accepts:       string (user input)
            *Returns:       void
            */
            if (int.TryParse(input, out var parsedSelection))
            {
                if (parsedSelection <= 0 || parsedSelection > CourseList._list.Count)
                {
                    Error.Message("Incorrect input, try again..");
                    Console.WriteLine();
                    HelperMethods.PromptUser("Enter a command: ");
                    input = HelperMethods.GetUserSelection();
                    ParseMainInput(input);
                }
                else
                {
                    parsedSelection--;
                    Console.Clear();
                    CourseMenu.Display(parsedSelection);
                }
            }
            else
            {
                switch (input)
                {
                    case "A":
                        Course newCourse = CrudMethods.AddCourse();
                        if (newCourse != null)
                        {
                            CourseList._list.Add(newCourse);
                            Console.Clear();
                            MainMenu.Display();
                        }
                        else
                        {
                            ParseMainInput(input);
                        }
                        break;

                    case "X":
                        HelperMethods.WriteToFile();
                        System.Environment.Exit(1);
                        break;

                    default:
                        Error.Message("Incorrect input, try again..");
                        Console.WriteLine();
                        HelperMethods.PromptUser("Enter a command: ");
                        string new_input = HelperMethods.GetUserSelection();
                        ParseMainInput(new_input);
                        break;
                }
            }
        }
    }
}
