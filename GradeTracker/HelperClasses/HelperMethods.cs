/**
	 * Class Name:  HelperMethods.cs
	 * Purpose:		Static class which holds all of my helper methods.
	 * Coder:		Chris Levesque
	 * Date:		2021-05-30	
*/

using GradeTracker.Classes;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Schema;      
using Newtonsoft.Json.Linq;        
using System.IO;
using GradeTracker.Menu;

namespace GradeTracker.HelperClasses
{
    static class HelperMethods
    {
        /*
             --- Schema String ---
        */
        public static string _courseSchema { get; set; }

        /*
             --- Public Methods ---
        */
        public static bool FileReadIsOk(string path, out string json)
        {
            /*Method Name:  FileReadIsOk
            *Purpose:       Determine whether file was read or not.
            *Accepts:       string (filePath), out string (jsonData)
            *Returns:       bool
            */
            try
            {
                // Read JSON file data 
                json = File.ReadAllText(path);
                return true;
            }
            catch
            {
                json = null;
                return false;
            }
        }
        public static bool ValidateCourse(Course c, string jsonSchema)
        {
            /*Method Name:  ValidateCourse  
            *Purpose:       Validates course object against the courseSchema.
            *Accepts:       Course (object to check), string (course Schema)
            *Returns:       bool
            */
            string courseData = JsonConvert.SerializeObject(c);
            JSchema courseSchema = JSchema.Parse(jsonSchema);
            JObject courseObject = JObject.Parse(courseData);
            return courseObject.IsValid(courseSchema);
        }
        public static string GetUserSelection()
        {
            /*Method Name:  GetUserSelection
            *Purpose:       Get user input and return (forced ToUpper() for easier parsing).
            *Accepts:       void
            *Returns:       string
            */
            string input;
            input = Console.ReadLine();
            TrimAndToUpper(ref input);
            return input;
        }
        public static void CalculateCourseMarks()
        {
            /*Method Name:  CalculateCourseMarks
            *Purpose:       Iterate through the static CourseList and calculate the OutOf and Percent
            *Accepts:       void
            *Returns:       void
            */
            foreach (Course c in CourseList._list)
            {
                c.CalculateMarks();
            }
        }
        public static void LoadCourseSchema(string schemaPath)
        {
            /*Method Name:  LoadCourseSchema
            *Purpose:       Load the courseSchema into a static string for easy reference
            *Accepts:       string (path of the schema)
            *Returns:       void
            */
            string schemaData;
            try
            {
                if (FileReadIsOk(schemaPath, out schemaData))
                {
                    // forcing it into a schema object to make sure it is a valid schema
                    JSchema courseSchema = JSchema.Parse(schemaData);
                    _courseSchema = courseSchema.ToString();
                }
                else
                {
                    // schema file failed to load
                    Error.Message($"Schema file '{Program.COURSE_SCHEMA_FILE}' doesnt exist, you MUST include the schema file for program to run. \nExiting...");
                    System.Environment.Exit(1);
                }
            }
            catch
            {
                // schema file was found but is invalid schema format
                Error.Message($"Json Schema '{Program.COURSE_SCHEMA_FILE}' is INVALID and CANNOT be used.\nPlease Provide a valid Schema \nExiting...");
                System.Environment.Exit(1);
            }

        }
        public static void PromptUser(string prompt)
        {
            /*Method Name:  PromptUser
            *Purpose:       Wrapper for Console.Write to prompt the user ( just for readability ).
            *Accepts:       string (prompt message)
            *Returns:       void
            */
            Console.Write(prompt);
        }
        public static void TrimAndToUpper(ref string input)
        {
            /*Method Name:  TrimAndToUpper
            *Purpose:       Takes in reference string and Trims whitespace and forces it ToUpper.
            *Accepts:       ref string (user input)
            *Returns:       void
            */
            input = input.Trim();
            input = input.ToUpper();
        }
        public static void WriteToFile()
        {
            /*Method Name:  WriteToFile
            *Purpose:       Wrapper for File.WriteAllText, writes all Courses in the list to the COURSE_DATA_FILE ( readability )
            *Accepts:       void
            *Returns:       void
            */

            try
            {
                string jsonData = JsonConvert.SerializeObject(CourseList._list, new JsonSerializerSettings 
                { 
                    Formatting = Formatting.Indented
                });

                File.WriteAllText(Program.COURSE_DATA_FILE, jsonData);
            }
            catch
            {
                // if SOMEHOW this was possibly triggered then this will fire
                Error.Message($"writing data to '{Program.COURSE_DATA_FILE}' has failed");
                PrintMethods.BlankLine();
                PromptUser("Would you like to exit and lose changes? (y/n): ");
                string exit = GetUserSelection();
                if(exit == "Y")
                {
                    System.Environment.Exit(1);
                }
                else if (exit == "N")
                {
                    MainMenu.Display();
                }
                else 
                {
                    Error.Message($"input '{exit}' wasnt 'Y' or 'N' ");
                    PrintMethods.BlankLine();
                    WriteToFile();
                }
            }
        }
    }
}
