/**
	 * Class Name:  CrudMethods.cs
	 * Purpose:		Houses my static crud methods for course and evaluation objects.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using System;
using GradeTracker.Classes;

namespace GradeTracker.HelperClasses
{
    public static class CrudMethods
    {
        /*
             --- Public Methods ---
        */
        public static Course AddCourse()
        {
            /*Method Name:  AddCourse()
            *Purpose:       Create a course object that is validated against the courseSchema.json
            *Accepts:       void
            *Returns:       Course object
            */
            string input;

            HelperMethods.PromptUser("Enter a course code: ");
            input = Console.ReadLine();
            input = input.Trim();

            Course c = new Course(input);
            if (HelperMethods.ValidateCourse(c, HelperMethods._courseSchema))
            {
                return c;
            }
            else
            {
                Error.Message($"Invalid course code. String {input} does not match regex pattern");
                PrintMethods.BlankLine();
                c = null;
                return c;
            }
        }
        public static bool AddEvaluation(int courseSelection)
        {
            /*Method Name:  AddEvaluation
            *Purpose:       Prompts input from user to make an Evaluation object to add to a Course, Course is then validated against courseSchema.json
            *Accepts:       int (course index)
            *Returns:       bool
            */
            string[] userInput = new string[4];
            bool[] numericsOkay = new bool[3];

            HelperMethods.PromptUser("Enter a description: ");
            userInput[0] = Console.ReadLine();
            userInput[0] = userInput[0].Trim();

            HelperMethods.PromptUser("Enter the 'out of' mark: ");
            userInput[1] = Console.ReadLine();
            userInput[1] = userInput[1].Trim();

            HelperMethods.PromptUser("Enter the % weight: ");
            userInput[2] = Console.ReadLine();
            userInput[2] = userInput[2].Trim();

            HelperMethods.PromptUser("Enter marks earned or Press ENTER to skip: ");
            userInput[3] = Console.ReadLine();
            userInput[3] = userInput[3].Trim();

            Evaluation e = VerifyEvaluationInputs(userInput, ref numericsOkay);
            e.CalculateMarksAndPercent();

            CourseList._list[courseSelection].Evaluations.Add(e);

            if (HelperMethods.ValidateCourse(CourseList._list[courseSelection], HelperMethods._courseSchema))
            {
                return true;
            }
            else
            {
                if(!numericsOkay[0])
                {
                    Error.Message($"value 'out of' ({userInput[1]}) not an integer >= 0 ...");
                    PrintMethods.BlankLine();
                }
                if (!numericsOkay[1])
                {
                    Error.Message($"value 'weight' ({userInput[2]}) not a number >= 0 and <= 100 ...");
                    PrintMethods.BlankLine();
                }
                if (!numericsOkay[2])
                {
                    Error.Message($"value 'earned marks' ({userInput[3]}) not a number >= 0 or == null ...");
                    PrintMethods.BlankLine();
                }
                Error.Message($"evaluation '{userInput[0]}' has NOT been added... ");
                PrintMethods.BlankLine();
                CourseList._list[courseSelection].Evaluations.Remove(e);
                return false;
            }
        }
        public static void DeleteEvaluation(int courseSelection, int evaluationSelection)
        {
            /*Method Name:  DeleteEvaluation
            *Purpose:       Deletes an evaluation object from a Courses evaluation list.
            *Accepts:       int (course index), int (evaluation index)
            *Returns:       void
            */
            string input;

            HelperMethods.PromptUser($"Delete {CourseList._list[courseSelection].Evaluations[evaluationSelection].Description}? (Y/N): ");
            input = HelperMethods.GetUserSelection();

            if (input == "Y" || input == "N")
            {
                if (input == "Y")
                {
                    Evaluation toBeRemoved = CourseList._list[courseSelection].Evaluations[evaluationSelection];
                    CourseList._list[courseSelection].Evaluations.Remove(toBeRemoved);
                    toBeRemoved = null;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Error.Message("Incorrect input, try again...");
                DeleteEvaluation(courseSelection, evaluationSelection);
            }
        }
        public static void DeleteCourse(int courseSelection)
        {
            /*Method Name:  DeleteCourse
            *Purpose:       Deletes a course object from the static CourseList
            *Accepts:       int (course index)
            *Returns:       void
            */
            string input;

            HelperMethods.PromptUser($"Delete {CourseList._list[courseSelection].Code}? (Y/N): ");
            input = HelperMethods.GetUserSelection();

            if (input == "Y" || input == "N")
            {
                if (input == "Y")
                {
                    Course toBeRemoved = CourseList._list[courseSelection];
                    CourseList._list.Remove(toBeRemoved);
                    toBeRemoved = null;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Error.Message("Incorrect input, try again...");
                Console.WriteLine();
                DeleteCourse(courseSelection);
            }
        }  
        public static void EditEvaluation(int courseSelection, int evaluationSelection)
        {
            /*Method Name:  EditEvaluation
            *Purpose:       Edit the EarnedMarks property of an evaluation object of a Course from the static CourseList
            *Accepts:       int (course index), int (evaluation index)
            *Returns:       void
            */
            Evaluation temp = CourseList._list[courseSelection].Evaluations[evaluationSelection];
            Evaluation original = temp;
            string input;

            HelperMethods.PromptUser($"Enter marks earned out of {temp.OutOf}, press ENTER to leave unassigned: ");
            input = Console.ReadLine();

            // check for enter key
            if(input == "")
            {
                temp.EarnedMarks = null;
                temp.CalculateMarksAndPercent();
                CourseList._list[courseSelection].Evaluations[evaluationSelection] = temp;
                return;
            }

            double updatedMark;
            try
            {
                updatedMark = double.Parse(input);
                // check too see if assigned mark is too high
                if(updatedMark > temp.OutOf)
                {
                    Error.Message($"Invalid 'marks earned' value. '{updatedMark}' is higher than the outOf value {temp.OutOf}.");
                    PrintMethods.BlankLine();
                    EditEvaluation(courseSelection, evaluationSelection);
                    return;
                }
                temp.EarnedMarks = updatedMark;
                temp.CalculateMarksAndPercent();
                CourseList._list[courseSelection].Evaluations[evaluationSelection] = temp;
                if (HelperMethods.ValidateCourse(CourseList._list[courseSelection], HelperMethods._courseSchema))
                {
                    return;
                }
                else
                {
                    Error.Message($"Invalid 'marks earned' value. {updatedMark} is less than the minimum value of 0.");
                    PrintMethods.BlankLine();
                    CourseList._list[courseSelection].Evaluations[evaluationSelection] = original;
                    EditEvaluation(courseSelection, evaluationSelection);
                }
            }
            catch
            {
                Error.Message($"Invalid 'marks earned' value. '{input}' was not a number");
                PrintMethods.BlankLine();
                EditEvaluation(courseSelection, evaluationSelection);
            }
        }

        /*
             --- Private Methods ---
        */
        private static Evaluation VerifyEvaluationInputs(string[] input, ref bool[] numericsOkay)
        {
            /*Method Name:  VerifyEvaluationInputs
            *Purpose:       Verifys the input from user upon creation of an evaluation object, flags incorrect fields and displays errors accordingly
            *Accepts:       int [] (users input), ref bool [] (bool array for flagging incorrect input)
            *Returns:       Evaluation object
            */
            Evaluation e = new Evaluation
            {
                Description = input[0]
            };

            try
            {
                e.OutOf = int.Parse(input[1]);
                numericsOkay[0] = true;
                if (e.OutOf < 0)
                {
                    numericsOkay[0] = false;
                }
            }
            catch
            {
                e.OutOf = -1;
                numericsOkay[0] = false;
            }

            try
            {
                e.Weight = double.Parse(input[2]);
                numericsOkay[1] = true;
                if (e.Weight < 0 || e.Weight > 100)
                {
                    numericsOkay[1] = false;
                }
            }
            catch
            {
                e.Weight = -1.0;
                numericsOkay[1] = false;
            }

            try
            {
                e.EarnedMarks = input[3] == "" ? null : double.Parse(input[3]);
                numericsOkay[2] = true;
                if (e.EarnedMarks != null && e.EarnedMarks < 0)
                {
                    numericsOkay[2] = false;
                }
            }
            catch
            {
                e.EarnedMarks = -1.0;
                numericsOkay[2] = false;
            }

            return e;
        }
    }
}
