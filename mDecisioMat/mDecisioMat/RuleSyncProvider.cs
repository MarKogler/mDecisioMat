using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using SharedClassDLL;

namespace mDecisioMat
{
    /// <summary>
    /// Chose the mode sigleton.
    /// Class converter is for 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    class RuleSyncProvider :SharedClassDLL.RuleSyncInterface        
    {

        #region Membervariables

        // Beachte wo sich der Dateipfad nach einer Installation des Programmes an anderem Rechner befindet.
        // @"..\..\..\Personaldatei.csv"
        // Beacht hier Programm aus Bachelor Hausübung 10 aus 1. Semester
        private string sFilePathRuleSet = @"C:\Users\Martin\Documents\FH_Wels\Master\3.Semester\SWA\Abschlussprojekt\M_Solution_Regelwerk_csv.csv";
        private string nameOfRuleSet = "M_Solution_Regelwerk_csv";

        private bool initialized;
        // Variable for a new ruleset
        private RuleSet ruleSet;
        #endregion
        
        #region Constructor

        /// <summary>
        /// With the constructor the server is started and the data from the CSV-file (RuleSet) is read in.
        /// </summary>
        public RuleSyncProvider()
        {
            Console.WriteLine("Msolutions (C)");
            Console.WriteLine("Server started!" + Environment.NewLine);


            // Read data from CSV-file
            Console.WriteLine("Data read in at " + DateTime.Now);

            GetDataFromCsvRuleSet();
            if (!GetDataFromCsvRuleSet())
            {
                Console.WriteLine("An error occured during reading data from the CSV-File!");
            }

            initialized = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Read the data from Csv-File. One line is saparated in it's parts via the method ".Split". It is necessary that 
        /// defined rules are complied during the CSV-file is created. Otherwise errors will occur in the while-loop 
        /// during the ruleSet is read in.
        /// </summary>
        /// <returns></returns>
        private bool GetDataFromCsvRuleSet()
        {
            //private string name;
            //private int numberOfQuestions;
            //private int numberOfAnswers;
            //private string[] attributeHeader;
            //private string[] attributeTypeHeader;
            //private string[,] attributes; //[numberOfAnswers,numberOfQuestions]

            string line;
            char[] saparators = new char[] { ';', ',' };
            string[] separatedLine;
            int lineCounter = 0;

            string name;
            int numberOfQuestions;
            int numberOfAnswers;
            string[,] attributes = new string[1,1];
            string[,] tempAttributes;

            try
            {   
                try
                {
                    StreamReader inputFile = new StreamReader(sFilePathRuleSet);
                    //SaveNameOfRulseSet(nameOfRuleSet);
                }
                catch (ArgumentOutOfRangeException exep)
                {
                    Console.WriteLine(exep.Message + "Please close CSV-file");
                }

                // Read header from CSV-file
                line = inputFile.ReadLine();

                lineCounter++;

                while ((line = inputFile.ReadLine()) != null)
                {
                    if (line != null)
                    {
                        separatedLine = line.Split(saparators);

                        if (lineCounter == 4)
                        {
                            numberOfQuestions = CountNumberOfQuestions(separatedLine);
                        }

                        if (lineCounter > 2)
                        {
                            attributes = new string[1, separatedLine.Length - 2];

                            for (int matLine = 0; matLine < attributes.GetLength(0); matLine++)
                            {
                                for (int matColumn = 0; matColumn < attributes.GetLength(1); matColumn++)
                                {
                                    attributes[matLine, matColumn] = separatedLine[matColumn];
                                }
                            }
                            tempAttributes = attributes;
                            // Expand rows in attributes.
                            attributes = new string[tempAttributes.GetLength(0) + 1, tempAttributes.GetLength(1)];

                            for (int matLine = 0; matLine < tempAttributes.GetLength(0); matLine++)
                            {
                                for (int matColumn = 0; matColumn < tempAttributes.GetLength(1); matColumn++)
                                {
                                    attributes[matLine, matColumn] = tempAttributes[matLine,matColumn];
                                }
                            }

                            for (int matLine = tempAttributes.GetLength(0); matLine < attributes.GetLength(0); matLine++)
                            {
                                for (int matColumn = 0; matColumn < attributes.GetLength(1); matColumn++)
                                {
                                    attributes[matLine, matColumn] = separatedLine[matColumn];
                                }
                            }                            
                        }

                        // Whith each line, which is read in the lineCounter is increased.
                        lineCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
                CountNumberOfAnswers(lineCounter);
                SaveAttributesOfRuleSet(attributes);

                //ruleSet = new RuleSet();
                inputFile.Close();
            }
            catch
            {
                // If an error occurs.
                return false;
            }
            return true;
        }

        #region Methods to edit data from CSV
        
        //private void SaveAttributeHeader(string readLine)
        //{
        //    ruleSet.AttributeHeader = readLine;
        //}

        /// <summary>
        /// Save/define the name of the ruleset which was read in.
        /// </summary>
        /// <param name="nameOfRuleSet"></param>
        private void SaveNameOfRulseSet(string nameOfRuleSet)
        {
            ruleSet.Name = nameOfRuleSet;
        }

        /// <summary>
        /// Attributes from the CSV-file are read in -> Save them into the class.
        /// </summary>
        /// <param name="attributes"></param>
        private void SaveAttributesOfRuleSet(string[,] attributes)
        {
            ruleSet.Attributes = attributes;
        }
        
        /// <summary>
        /// Count the amount of questions in one ruleset. Subtract two from the result -> Because the first two columns are no questions.
        /// </summary>
        /// <param name="readLine">One line, which was read in</param>
        /// <returns></returns>
        private int CountNumberOfQuestions(string[] readLine)
        {
            return (readLine.Length - 2);
        }

        /// <summary>
        /// Defines the amount of answers in one ruleset. Subtract three from the counted lines -> Because the first three rows are no answers.
        /// </summary>
        /// <param name="countedLines"></param>
        private void CountNumberOfAnswers(int countedLines)
        {
            ruleSet.NumberOfAnswers = (countedLines - 3);
        }
        #endregion

        #endregion

        #region Methods for the interface

        /// <summary>
        /// If client sends order to read out current data from the CSV-File . Additionally, the data is displayed on the console window of the server.
        /// </summary>
        /// <returns></returns>
        public bool ReadDataFromCsvFile()
        {
            Console.WriteLine("Data read in at " + DateTime.Now);

            GetDataFromCsvRuleSet();
            if (!GetDataFromCsvRuleSet())
            {
                Console.WriteLine("An error occured during reading data from the CSV-File!");
            }

            initialized = true;

            return true;
        }

        /// <summary>
        /// Transfer value of bool variable "initialized".
        /// </summary>
        /// <returns></returns>
        public bool IsInitialized()
        {
            return initialized;
        }
        #endregion

    }
}
