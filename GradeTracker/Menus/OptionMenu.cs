/**
	 * Class Name:  OptionMenu.cs
	 * Purpose:		Static class which holds option menus and displays.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using System;
using GradeTracker.HelperClasses;


namespace GradeTracker.Menu
{
    public static class OptionMenu
    {
        /*
            --- String option arrays ---
        */
        private static string[] courseOptions = {
                "Press D to delete this course.",
                "Press A to add an evaluation.",
                "Press # from the above list to edit/delete a specific evaluation",
                "Press X to return to main menu"
            };
        private static string[] evaluationOptions = {
                "Press D to delete this evaluation.",
                "Press E to edit this evaluation.",
                "Press X to return to the previous menu"
            };
        private static string[] mainOptions = {
                "Press # from the above list to view/edit/delete a specific course.",
                "Press A to add a new course.",
                "Press X to quit"
            };

        /*
            --- Public Methods ---
        */
        public static void DisplayCourseOptions(int dashes)
        {
            /*Method Name:  DisplayCourseOptions
            *Purpose:       Displays the course options 
            *Accepts:       int (dashes amount)
            *Returns:       void
            */
            PrintMethods.BlankLine();
            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();

            foreach (string option in courseOptions)
            {
                Console.WriteLine(option);
            }

            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();
        }
        public static void DisplayEvaluationOptions(int dashes)
        {
            /*Method Name:  DisplayEvaluationOptions
            *Purpose:       Displays the Evaluation options 
            *Accepts:       int (dashes amount)
            *Returns:       void
            */
            PrintMethods.BlankLine();
            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();

            foreach (string option in evaluationOptions)
            {
                Console.WriteLine(option);
            }

            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();
        }
        public static void DisplayMainOptions(int dashes) 
        {
            /*Method Name:  DisplayMainOptions
            *Purpose:       Display the main menu options
            *Accepts:       int (dashes amount)
            *Returns:       void
            */
            PrintMethods.BlankLine();
            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();

            foreach (string option in mainOptions)
            {
                Console.WriteLine(option);
            }

            PrintMethods.PrintChar("-", dashes);
            PrintMethods.BlankLine();
        }
    }
}
