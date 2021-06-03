/**
	 * Class Name:  Program.cs
	 * Purpose:		Class that houses the main method and some program wide static constants.
	 *              Creates grades.json file if not already created, and displays the contents.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using GradeTracker.Classes;
using GradeTracker.Menu;
using System.IO;
using Newtonsoft.Json;              
using Newtonsoft.Json.Linq;
using GradeTracker.HelperClasses;
using System.Collections.Generic;

namespace GradeTracker
{
    class Program
    {
        /*
             --- Constants ---
        */
        public const string COURSE_DATA_FILE = "grades.json";
        public const string COURSE_SCHEMA_FILE = "courseSchema.json";
        public const int DASHES = 90;
        public const string MAIN_TITLE = "Grades Summary";
   
        /*
             --- Main ---
        */
        static void Main(string[] args)
        {
            InstantiateStaticList();
            HelperMethods.LoadCourseSchema(COURSE_SCHEMA_FILE);
            ConfigureJsonData();
            RunProgram();
        }

        /*
             --- Private Methods ---
        */
        private static void ConfigureJsonData()
        {
            /*Method Name:  ConfigureJsonData
            *Purpose:       Checks is there is json data in COURSE_DATA_FILE, if not creates the COURSE_DATA_FILE
            *Accepts:       void
            *Returns:       void
            */
            if (IsJsonData(COURSE_DATA_FILE))
            {
                LoadJsonData(COURSE_DATA_FILE);
            }
            else
            {
                PrintMethods.DisplayFileCreate();
            }
        }
        private static bool IsJsonData(string path)
        {
            /*Method Name:  IsJsonData
            *Purpose:       Checks if the json file exists
            *Accepts:       string (filepath)
            *Returns:       bool
            */
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void LoadJsonData(string path)
        {
            /*Method Name:  LoadJsonData
            *Purpose:       Loads the json data ( course objects ) into the static CourseList only if verified by the courseSchema
            *Accepts:       string (filepath)
            *Returns:       void
            */
            // if file is empty, return
            if (new FileInfo(path).Length == 0)
            {
                return;
            }
            
            string jsonData;
            if (HelperMethods.FileReadIsOk(path, out jsonData))
            {
                try
                {
                    // instantiate new Jarray with the jsonData string 
                    JArray ja = JArray.Parse(jsonData);

                    // add every course object to the static list
                    Course temp;
                    foreach (JObject jt in ja)
                    {
                        temp = JsonConvert.DeserializeObject<Course>(jt.ToString());

                        // only adds validated courses to the programs static CourseList
                        if(HelperMethods.ValidateCourse(temp, HelperMethods._courseSchema))
                        {
                            CourseList._list.Add(temp);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    Error.Message("Json Data is corrupted and did not load...");
                    PrintMethods.BlankLine();
                    PrintMethods.DisplayFileCreate();
                }
            }
        }
        private static void RunProgram()
        {
            /*Method Name:  RunProgram
            *Purpose:       Wrapper to begin the program with the MainMenu.Display function
            *Accepts:       void
            *Returns:       void
            */
            MainMenu.Display();
        }
        private static void InstantiateStaticList()
        {
            /*Method Name:  InstantiateStaticList
            *Purpose:       instantiates the static CourseList used to store all course information
            *Accepts:       void
            *Returns:       void
            */
            CourseList._list = new List<Course>();
        }
    }
}