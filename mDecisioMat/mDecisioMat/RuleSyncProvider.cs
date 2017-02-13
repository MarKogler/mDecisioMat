using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using SharedClassDLL;
using System.Runtime.Serialization;


namespace mDecisioMat
{
    /// <summary>
    /// Chose the mode sigleton.
    /// In the RuleSyncProvider.cs all CSV-files from the folder @"RuleSets" are read in. The data from the files are stored in
    /// class variable setsOfRules. 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RuleSyncProvider : SharedClassDLL.RuleSyncInterface
    {

        #region Membervariables

        private string sFilePathRuleSet;
        System.IO.DirectoryInfo parentDirectory = new System.IO.DirectoryInfo(@"RuleSets");
        private string[] availableRuleSetsName;
        private bool statusCsvFile;
        private int numberOfQuestions;

        private bool initialized;
        private int notAvailableRuleSets;
        
        // Variable for a new ruleset
        private RuleSet[] setsOfRules;
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
            Console.WriteLine(Environment.NewLine);

            // Count available rulesets.
            notAvailableRuleSets = 0;
            for (int i = 0; i < setsOfRules.Length; i++)
            {
                if (setsOfRules[i] == null)
                {
                    notAvailableRuleSets++;
                }
            }

            if (statusCsvFile == true && notAvailableRuleSets != setsOfRules.Length)
            {
                Console.WriteLine("All other files are read in successfully.");
            }
            else if (notAvailableRuleSets == setsOfRules.Length)
            {
                Console.WriteLine("No available rulesets.");
            }
            else
            {
                Console.WriteLine("All files are read in successfully.");
            }
            initialized = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Read the data from Csv-File. Each line is separated in it's parts via the method ".Split".
        /// </summary>
        /// <returns></returns>
        private bool GetDataFromCsvRuleSet()
        {
            bool errorOccured;
            string line;
            char[] saparators = new char[] {';'};
            string[] separatedLine;
            int lineCounter = 0;
            int indexCounter = 0;

            string name;
            string[] attributeHeader = null;
            string[] attributeTypeHeader = null;
            List<string[]> listAttributes = null;

            // Count how many CSV-files have to be read. 
            foreach (System.IO.FileInfo f in parentDirectory.GetFiles())
            {
                indexCounter++;
            }

            // Store the names of the available CSV-files.
            availableRuleSetsName = new string[indexCounter];
            // For each CSV-file one enty in the variable setsOfRules is greated.
            setsOfRules = new RuleSet[indexCounter];

            // Reset the indexCounter
            indexCounter = 0;

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("The following rulesets were found: ");
            foreach (System.IO.FileInfo f in parentDirectory.GetFiles())
            {
                availableRuleSetsName[indexCounter] = f.Name;
                Console.WriteLine("Ruleset {0}: " + f.Name, (indexCounter + 1));
                indexCounter++;
            }

            errorOccured = false;
            for (int i = 0; i < indexCounter; i++)
            {
                try
                {
                    sFilePathRuleSet = @"RuleSets\" + availableRuleSetsName[i].ToString();
                    StreamReader inputFile = new StreamReader(sFilePathRuleSet);

                    // Read header from CSV-file
                    line = inputFile.ReadLine();
                    separatedLine = line.Split(saparators);
                    name = separatedLine[1];
                    listAttributes = null;

                    lineCounter++;

                    while ((line = inputFile.ReadLine()) != null)
                    {
                        // Whith each line, which is read in the lineCounter is increased.
                        lineCounter++;

                        separatedLine = line.Split(saparators);

                        // Store the attributeHeader from line 2.
                        if (lineCounter ==2)
                        {
                            attributeHeader = new string[separatedLine.Length];
                            attributeHeader = separatedLine;
                        }
                        // Store the attributeTypeHeader from line 2.
                        if (lineCounter == 3)
                        {
                            attributeTypeHeader = new string[separatedLine.Length];
                            attributeTypeHeader = separatedLine;
                        }
                        // Store all attributes.
                        if (lineCounter == 4)
                        {
                            numberOfQuestions = (separatedLine.Length - 2);
                        }
                        if (lineCounter >= 4)
                        {
                            if (listAttributes == null)
                            {
                                listAttributes = RuleSet.CreateNewAttributesList(separatedLine);
                            }
                            else
                            {
                                listAttributes = RuleSet.CreateNewAttributesList(separatedLine, listAttributes);
                            }
                        }                       
                    }

                    // Creat another ruleset
                    setsOfRules[i] = new RuleSet(name, attributeHeader, attributeTypeHeader, listAttributes);

                    lineCounter = 0;
                    inputFile.Close();
                }
                catch 
                {
                    // If one file is not read in successfully an error occurs. All other files are read in.
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Following file was not read in successfully: {0}", availableRuleSetsName[i].ToString());
                    errorOccured = true;
                }

            }
            return (errorOccured);
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
        /// Gives back the name of each availabel ruleset.
        /// </summary>
        /// <returns>Returns all available rulesets.</returns>
        public string[] GetAvailableRuleSets()
        {
            // Counter for available and not available rulesets.
            int counter = 0;
            string[] availableRuleSets;

            // Count the available rulesets.
            for (int i = 0; i < setsOfRules.Length; i++)
            {
                if (setsOfRules[i] == null)
                {
                    counter++;
                }
            }
            availableRuleSets = new string[setsOfRules.Length - notAvailableRuleSets];

            // Reset the counter.
            counter = 0;
            for (int i = 0; i < setsOfRules.Length; i++)
            {
                if (setsOfRules[i] != null)
                {
                    availableRuleSets[counter] = setsOfRules[i].Name;
                    counter++;
                }
            }
            return (availableRuleSets);
        }

        /// <summary>
        /// The client requests for a ruleset (request with the name of the needed ruleset) and the server provides this ruleset.
        /// </summary>
        /// <param name="nameOfRuleSet"></param>
        /// <returns>Returns the requested ruleset.</returns>
        public RuleSet GetSpecificRuleSet(string nameOfRuleSet)
        {
            // Default = Give back null.
            RuleSet neededRuleSet = null;

            for (int i = 0; i < setsOfRules.Length; i++)
            {
                // Check if setsOfRules[i] is not null, to avoid errors.
                if (setsOfRules[i] != null && setsOfRules[i].Name == nameOfRuleSet)
                {
                    neededRuleSet = setsOfRules[i];
                }
            }
            return (neededRuleSet);
        }

        #endregion

    }
}
