/**
	 * Class Name:  Evaluation.cs
	 * Purpose:		Houses the evaluation object properties and methods.	
	 * Coder:		Chris Levesque
	 * Date:		2021-05-28	
*/

using Newtonsoft.Json;

namespace GradeTracker.Classes
{
    public class Evaluation
    {
        /*
            --- Class Props ---
        */
        [JsonIgnore]
        public double CourseMarks { get; set; }
        [JsonIgnore]
        public double Percent { get; set; }
        public double Weight { get; set; }
        public double? EarnedMarks { get; set; }
        public int OutOf { get; set; }
        public string Description { get; set; }
 
        /*
            --- Class Ctors ---
        */
        public Evaluation()
        {

        }

        /*
            --- Class Methods ---
        */
        public void CalculateMarksAndPercent()
        {
            /*Method Name:  CalculateMarksAndPercent
            *Purpose:       Calculate this objects percent and coursemark properties based off EarnedMarks, OutOf and Weight
            *Accepts:       void
            *Returns:       void
            */
            Percent = EarnedMarks == null ? 0 : 100 * (double)EarnedMarks / OutOf;
            CourseMarks = Percent * Weight / 100;
        }
    }    
}
