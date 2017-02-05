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
    public class RuleSyncProvider : SharedClassDLL.RuleSyncInterface
    {

        #region Membervariables

        // Beachte wo sich der Dateipfad nach einer Installation des Programmes an anderem Rechner befindet.
        // @"..\..\..\Personaldatei.csv"
        //@"Ordnername\Beispielruleset.csv"
        // Beacht hier Programm aus Bachelor Hausübung 10 aus 1. Semester
        // Suche im Ordner nach allen CSVDateien -> In Schleife alle öffnen und in Array in erstellen
        private string sFilePathRuleSet = @"C:\Users\Martin\Documents\FH_Wels\Master\3.Semester\SWA\Abschlussprojekt\M_Solution_Regelwerk_csv.csv";
        private bool statusCsvFile;

        private bool initialized;
        
        // Variable for a new ruleset
        // Mache Variable zum Array
        private RuleSet[] setsOfRules = new RuleSet[1];
        #endregion
        
        #region Constructor

        /// <summary>
        /// With the constructor the server is started and the data from the CSV-file (RuleSet) is read in.
        /// </summary>
        public RuleSyncProvider()
        {
            Console.WriteLine("Msolutions (C)");
            Console.WriteLine("Server started!" + Environment.NewLine + Environment.NewLine);


            // Read data from CSV-file
            Console.WriteLine("Ruleset (data) read in at " + DateTime.Now);

            statusCsvFile = GetDataFromCsvRuleSet();
            if (!statusCsvFile)
            {
                Console.WriteLine("An error occured during reading data from the CSV-File!");
            }

            initialized = false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Read the data from Csv-File. Each line is separated in it's parts via the method ".Split".
        /// </summary>
        /// <returns></returns>
        private bool GetDataFromCsvRuleSet()
        {
            string line;
            char[] saparators = new char[] { ';', ',' };
            string[] separatedLine;
            int lineCounter = 0;

            string name;
            string[] attributeHeader = new string[1]; ;
            string[] attributeTypeHeader = new string[1];
            int numberOfQuestions;
            int numberOfAnswers;            
            string[,] attributes = new string[1,1];
            string[,] tempAttributes = new string[1, 1];

            try
            {   
                StreamReader inputFile = new StreamReader(sFilePathRuleSet);

                // Read header from CSV-file
                line = inputFile.ReadLine();
                separatedLine = line.Split(saparators);
                name = separatedLine[1];

                lineCounter++;

                while ((line = inputFile.ReadLine()) != null)
                {
                    // Whith each line, which is read in the lineCounter is increased.
                    lineCounter++;
                    if (line != null)
                    {
                        separatedLine = line.Split(saparators);

                        if (lineCounter ==2)
                        {
                            attributeHeader = separatedLine;
                        }
                        if (lineCounter == 3)
                        {
                            attributeTypeHeader = separatedLine;
                        }
                        if (lineCounter == 4)
                        {
                            numberOfQuestions = (separatedLine.Length - 2);
                            attributes = new string[1, separatedLine.Length - 2];
                        }
                        if (lineCounter >= 4)
                        {
                            for (int matLine = attributes.GetLength(0) - 1; matLine < attributes.GetLength(0); matLine++)
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
                                    attributes[matLine, matColumn] = tempAttributes[matLine, matColumn];
                                }
                            }
                        }
                    }
                    else
                    {                        
                        break;
                    }
                }
                numberOfAnswers = lineCounter - 3;

                if (attributes[attributes.GetLength(0) - 1, 0] == null)
                {
                    attributes = new string[attributes.GetLength(0) - 1, attributes.GetLength(1)];

                    for (int matLine = 0; matLine < tempAttributes.GetLength(0); matLine++)
                    {
                        for (int matColumn = 0; matColumn < tempAttributes.GetLength(1); matColumn++)
                        {
                            attributes[matLine, matColumn] = tempAttributes[matLine, matColumn];
                        }
                    }

                }
                // Creat another ruleset
                setsOfRules[0] = new RuleSet(name, attributeHeader, attributeTypeHeader, attributes);
                inputFile.Close();
            }
            catch
            {
                // If an error occurs.
                return false;
            }
            return true;
        }
        #endregion

        #region Methods for the interface

        /// <summary>
        /// Transfer value of bool variable "initialized".
        /// </summary>
        /// <returns></returns>
        public bool IsInitialized()
        {
            return initialized;
        }

        /// <summary>
        /// Gives back the name of each availabel rulesets.
        /// </summary>
        /// <returns>Returns all available rulesets.</returns>
        public string[] GetAvailableRuleSets()
        {
            string[] availableRuleSets = new string[setsOfRules.Length];

            for (int i = 0; i < setsOfRules.Length; i++)
            {
                availableRuleSets[i] = setsOfRules[i].Name;
            }

            return (availableRuleSets);
        }

        /// <summary>
        /// The client requests for a ruleset (request with the name of the needed ruleset) and the server provides this ruleset.
        /// </summary>
        /// <param name="nameOfRuleSet"></param>
        /// <returns>Returns the requested ruleset.</returns>
        public RuleSet GetSpecificRule(string nameOfRuleSet)
        {
            // Default = Give back the first ruleset.
            RuleSet neededRuleSet = setsOfRules[0];

            for (int i = 0; i < setsOfRules.Length; i++)
            {
                if (setsOfRules[i].Name == nameOfRuleSet)
                {
                    neededRuleSet = setsOfRules[i];
                    break;
                }
            }
            //string temp = "e";
            return (neededRuleSet);
        }

        #endregion

    }
}
