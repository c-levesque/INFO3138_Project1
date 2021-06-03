/**
	 * Class Name:  Course.cs
	 * Purpose:		Houses the course object properties and methods.	
	 * Coder:		Chris Levesque
	 * Date:		2021-05-28	
*/

using Newtonsoft.Json;
using System.Collections.Generic;

namespace GradeTracker.Classes
{
    public class Course
    {
        /*
            --- Class Props ---
        */
        public string Code { get; set; }
        [JsonIgnore]
        public double EarnedMarks { get; set; }
        [JsonIgnore]
        public int OutOf { get; set; }
        [JsonIgnore]
        public double Percent { get; set; }
        public List<Evaluation> Evaluations = new List<Evaluation>();

        /*
           --- Class Ctor ---
        */
        public Course(string code)
        {
            this.Code = code;
        }

        /*
           --- Class Methods ---
       */
        public void CalculateMarks()
        {
            /*Method Name:  CalculateMarks
            *Purpose:       Iterate through evaluation array and calculate the marks
            *Accepts:       void
            *Returns:       void
            */
            double marksEarned = 0.0, percent;
            int outOf = 0;
            
            foreach(Evaluation e in Evaluations)
            {
                e.CalculateMarksAndPercent();
                marksEarned += e.CourseMarks;
                if(e.EarnedMarks != null)
                {
                    outOf += (int)e.Weight;
                }
            }

            percent = outOf == 0 ? 0 : 100 * marksEarned / outOf;
            
            EarnedMarks = marksEarned;
            OutOf = outOf;
            Percent = percent;
        }  
    }
}
