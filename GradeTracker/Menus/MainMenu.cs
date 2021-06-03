/**
	 * Class Name:  MainMenu.cs
	 * Purpose:		Static class which holds main menu display method.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using GradeTracker.HelperClasses;

namespace GradeTracker.Menu
{
    public static class MainMenu
    {
        public static void Display()
        {
            /*Method Name:  Display
            *Purpose:       Displays the Main Menu , takes user input and sends it to be parsed.
            *Accepts:       void
            *Returns:       void
            */
            string input;

            // calculate course mark values for all courses
            HelperMethods.CalculateCourseMarks();

            // display the top, courses, and options
            PrintMethods.CreateMainBox(Program.MAIN_TITLE, Program.DASHES);
            PrintMethods.DisplayCourses();
            OptionMenu.DisplayMainOptions(Program.DASHES);

            HelperMethods.PromptUser("Enter a command: ");
            input = HelperMethods.GetUserSelection();

            ParseMethods.ParseMainInput(input);
        }
    }
}
