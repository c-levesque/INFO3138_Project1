/**
	 * Class Name:  CourseMenu.cs
	 * Purpose:		Static class which holds course menu display method.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using GradeTracker.Classes;
using GradeTracker.HelperClasses;

namespace GradeTracker.Menu
{
    public static class CourseMenu
    {
        public static void Display(int selection)
        {
            /*Method Name:  Display
            *Purpose:       Displays the Course Menu , takes user input and sends it to be parsed.
            *Accepts:       int (course index)
            *Returns:       
            */
            string input;

            PrintMethods.CreateMainBox(CourseList._list[selection].Code, Program.DASHES);
            PrintMethods.DisplayCourseEvaluations(selection);
            OptionMenu.DisplayCourseOptions(Program.DASHES);

            HelperMethods.PromptUser("Enter a command: ");
            input = HelperMethods.GetUserSelection();

            ParseMethods.ParseCourseInput(input, selection);
        }
    }
}
