using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedClassDLL;

/// <summary>
/// Test drivers for the SharedClassDLL are located here
/// </summary>
namespace TestDriverSharedClassDLL
{
    /// <summary>
    /// Test the Class RuleSet
    /// </summary>
    class TestDriverRuelSet
    {
        static void Main(string[] args)
        {
            #region initialize variables
            string testName = "Test RuleSet";
            string[] testAttributeHeader = { "ID-Nummer", "Bezeichnung", "Marke", "Preis", "ABS" };
            string[] testAttributeTypeHeader = { "-", "-", "SD", "vonbis", "jn" };
            string[,] testAttributes = { { "1", "Peugeot 206", "Peugeot", "500", "ja" },
                                         { "2", "Fiat Stilo", "Fiat", "1690", "ja" },
                                         { "3", "Volkswagen T4", "Volkswagen", "2990","nein"} };
            #endregion

            #region initialize testRuleSet
            //try
            //{
            RuleSet testRuleSet = new RuleSet(testName,
                                                  testAttributeHeader,
                                                  testAttributeTypeHeader,
                                                  testAttributes);

            Console.WriteLine("testRuleSet initialized successfully");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("initialization of testRuleSet failed");
            //    Console.Write("Error: ");
            //    Console.WriteLine(e.ToString());
            //}
            #endregion

            #region test if data of testRuleSet to be correct
            Console.WriteLine();
            Console.WriteLine("Initialized Content of testRuleSet:");
            Console.WriteLine(testRuleSet.ToString());
            #endregion

            #region test properties
            Console.WriteLine("Number of Questions:");
            Console.WriteLine(testRuleSet.NumberOfQuestions.ToString());
            Console.WriteLine("Number of Answers:");
            Console.WriteLine(testRuleSet.NumberOfAnswers.ToString());
            Console.WriteLine();

            Console.WriteLine("Change Entries:");
            testRuleSet.Name = "Name2";
            Console.WriteLine("Name changed:");
            Console.WriteLine(testRuleSet.ToString());
            Console.WriteLine("Number of Questions:");
            Console.WriteLine(testRuleSet.NumberOfQuestions.ToString());
            Console.WriteLine("Number of Answers:");
            Console.WriteLine(testRuleSet.NumberOfAnswers.ToString());
            Console.WriteLine();
            testRuleSet.AttributeHeader = new string[] { "ID-Nummer", "Bezeichnung", "Marke", "Preis", "ABS", "Alarmanlage" };
            Console.WriteLine("AttributeHeader changed:");
            Console.WriteLine(testRuleSet.ToString());
            Console.WriteLine("Number of Questions:");
            Console.WriteLine(testRuleSet.NumberOfQuestions.ToString());
            Console.WriteLine("Number of Answers:");
            Console.WriteLine(testRuleSet.NumberOfAnswers.ToString());
            Console.WriteLine();
            testRuleSet.AttributeTypeHeader = new string[] { "-", "-", "SD", "vonbis", "jn", "jn" };
            Console.WriteLine("AttributeTypeHeader changed:");
            Console.WriteLine(testRuleSet.ToString());
            Console.WriteLine("Number of Questions:");
            Console.WriteLine(testRuleSet.NumberOfQuestions.ToString());
            Console.WriteLine("Number of Answers:");
            Console.WriteLine(testRuleSet.NumberOfAnswers.ToString());
            Console.WriteLine();

            testRuleSet.Attributes = new string[,]{ { "1", "Peugeot 206", "Peugeot", "500", "ja" ,"nein" },
                                                    { "2", "Fiat Stilo", "Fiat", "1690", "ja" , "nein" },
                                                    { "3", "Volkswagen T4", "Volkswagen", "2990","nein", "nein" },
                                                    { "4", "Audi A4", "Audi", "30790", "ja", "nein"} };
            Console.WriteLine("Attributes changed:");
            Console.WriteLine(testRuleSet.ToString());
            Console.WriteLine("Number of Questions:");
            Console.WriteLine(testRuleSet.NumberOfQuestions.ToString());
            Console.WriteLine("Number of Answers:");
            Console.WriteLine(testRuleSet.NumberOfAnswers.ToString());
            Console.WriteLine();
            #endregion

            #region test methods
            Console.WriteLine("Get the first Question");
            string[] firstquestion = testRuleSet.GetQuestion(0);
            for (int i = 0; i < firstquestion.Length; i++)
            {
                Console.WriteLine(firstquestion[i]);
            }
            #endregion

            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}

