/**
	 * Class Name:  EvaluationMenu.cs
	 * Purpose:		Static class which holds evaluation menu display method.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using GradeTracker.HelperClasses;
using GradeTracker.Classes;

namespace GradeTracker.Menu
{
    public static class EvaluationMenu
    {
        public static void Display(int courseSelection, int evaulationSelection)
        {
            /*Method Name:  Display
            *Purpose:       Displays the Evaluation Menu , takes user input and sends it to be parsed.
            *Accepts:       int (course index), int (evaluation index)
            *Returns:       void
            */
            string input;
            string title = $"{CourseList._list[courseSelection].Code} {CourseList._list[courseSelection].Evaluations[evaulationSelection].Description}"; 

            PrintMethods.CreateMainBox(title, Program.DASHES);

            PrintMethods.DisplayEvaluation(courseSelection, evaulationSelection);

            OptionMenu.DisplayEvaluationOptions(Program.DASHES);

            HelperMethods.PromptUser("Enter a command: ");
            input = HelperMethods.GetUserSelection();

            ParseMethods.ParseEvaluationInput(input, courseSelection, evaulationSelection);
        }
    }
}
