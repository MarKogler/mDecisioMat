using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

/// <summary>
/// The SharedClassDLL contains all Classes that are needed for both Client and Server
/// </summary>
namespace SharedClassDLL
{
    /// <summary>
    /// The Class RuleSet is used to open and use the read rule sets within the program
    /// </summary>
    [DataContract]
    public class RuleSet
    {
        #region Membervariables
        [DataMember]
        private string name;
        [DataMember]
        private string[] attributeHeader;
        [DataMember]
        private string[] attributeTypeHeader;
        [DataMember]
        private List<string[]> attributes; //List of Answersets; Every Listelement is a Set of Answers to a specific Question
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor. This constructor creates a new instance of the class RuleSet.
        /// </summary>
        /// <param name="name">Name of the rule set</param>
        /// <param name="attributeHeader">header-line of the rule set</param>
        /// <param name="attributeTypeHeader">attribute type line of the rule set</param>
        /// <param name="attributes">attribute matrix</param>
        public RuleSet(string name, string[] attributeHeader, string[] attributeTypeHeader, List<string[]> attributes)
        {
            this.Name = name;
            this.AttributeHeader = attributeHeader;
            this.AttributeTypeHeader = attributeTypeHeader;
            this.Attributes = attributes;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property to read and write the private variable name
        /// </summary>
        /// <returns>value of private variable name</returns>
        public string Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Property to count the number of available Questions;
        /// The number is counted by the number of attributes;
        /// The user is in charge to keep data consistent (the length of "attributeHeader", "attributeTypeHeader" and "attributes" must be the same)
        /// </summary>
        /// <returns>value of private variable numberOfQuestions</returns>
        public int NumberOfQuestions
        {
            get
            {
                return (this.Attributes.Count - 2);
            }
        }

        /// <summary>
        /// Property to count numberOfAnswers;
        /// The number is counted at the first element of "Attributes"
        /// The user is in charge to keep data consistent (every Question must have the same number of Answers)
        /// </summary>
        /// <returns>value of private variable numberOfAnswers</returns>
        public int NumberOfAnswers
        {
            get
            {
                return this.Attributes[0].Length;
            }
        }

        /// <summary>
        /// Property to read and write the private variable attributeheader
        /// </summary>
        /// <returns>value of private variable attributeHeader</returns>
        public string[] AttributeHeader
        {
            set
            {
                this.attributeHeader = value;
            }
            get
            {
                return this.attributeHeader;
            }
        }

        /// <summary>
        /// Property to read and write the private variable attributeTypeHeader
        /// </summary>
        /// <returns>value of private variable attributeTypeHeader</returns>
        public string[] AttributeTypeHeader
        {
            set
            {
                this.attributeTypeHeader = value;
            }
            get
            {
                return this.attributeTypeHeader;
            }
        }

        /// <summary>
        /// Property to read and write the private variable attributes
        /// </summary>
        /// <returns>value of private variable attributes</returns>
        public List<string[]> Attributes
        {
            set
            {
                this.attributes = value;
            }
            get
            {
                return this.attributes;
            }
        }
        #endregion

        #region dynamic Methods
        /// <summary>
        /// Method to read a certain question
        /// </summary>
        /// <param name="questionNumber">number of the question; starting at 0</param>
        /// <returns>value of the searched question</returns>
        public string[] GetQuestion(int questionNumber)
        {
            string[] question = new string[NumberOfAnswers + 2];

            question[0] = AttributeHeader[questionNumber + 2];
            question[1] = AttributeTypeHeader[questionNumber + 2];

            string[] temp = this.Attributes[questionNumber + 2];
            for (int i = 0; i < NumberOfAnswers; i++)
            {
                question[i + 2] = temp[i];
            }

            return question;
        }

        /// <summary>
        /// Provide a Function to print the content to on String
        /// </summary>
        /// <returns>content string</returns>
        public override string ToString()
        {

            //Add name
            string contentstring = this.Name + Environment.NewLine;

            //Add attributeHeader
            for (int i = 0; i < this.AttributeHeader.Length; i++)
            {
                if (this.AttributeHeader[i].Length <= 30)
                {
                    contentstring = contentstring + String.Format("{0,-30}", this.AttributeHeader[i]);
                }
                else
                {
                    contentstring = contentstring + String.Format("{0,-27}...", this.AttributeHeader[i].Substring(0, 27));
                }
            }
            contentstring = contentstring + Environment.NewLine;

            //Add attributeTypeHeader
            for (int i = 0; i < this.AttributeTypeHeader.Length; i++)
            {
                if (this.AttributeTypeHeader[i].Length <= 30)
                {
                    contentstring = contentstring + String.Format("{0,-30}", this.AttributeTypeHeader[i]);
                }
                else
                {
                    contentstring = contentstring + String.Format("{0,-27}...", this.AttributeTypeHeader[i].Substring(0,27));
                }
            }
            contentstring = contentstring + Environment.NewLine;

            //Add attributes
            for (int i = 0; i < this.NumberOfAnswers; i++)
            {
                for (int j = 0; j < this.NumberOfQuestions + 2; j++)
                {
                    if (this.Attributes[j][i].Length <= 30)
                    {
                        contentstring = contentstring + String.Format("{0,-30}", this.Attributes[j][i]);
                    }
                    else
                    {
                        contentstring = contentstring + String.Format("{0,-27}...", this.Attributes[j][i].Substring(0,27));
                    }
                }
                contentstring = contentstring + Environment.NewLine;
            }

            return contentstring;
        }

        /// <summary>
        /// Add a new Line of Answers to the existing sets of Answers
        /// </summary>
        /// <param name="newAnswerLine">Line of Answers that should be added</param>
        public void AddNewAnswerLine(string[] newAnswerLine)
        {
            this.Attributes = CreateNewAttributesList(newAnswerLine, this.Attributes);
        }

        /// <summary>
        /// Add a new set of answers for a new quetsion to the existing list of sets of answers
        /// </summary>
        /// <param name="newSetOfAnswers"></param>
        public void AddNewQuestionWithSetOfAnswers(string question, string typeOfQuestion, string[] newSetOfAnswers)
        {
            //add to attributeHeader
            string[] newAttributeHeader = new string[this.AttributeHeader.Length + 1];
            for (int i = 0; i < this.AttributeHeader.Length; i++)
            {
                newAttributeHeader[i] = this.AttributeHeader[i];
            }
            newAttributeHeader[newAttributeHeader.Length - 1] = question;
            this.AttributeHeader = newAttributeHeader;

            //add to attributeTypeHeader
            string[] newAttributeTypeHeader = new string[this.AttributeTypeHeader.Length + 1];
            for (int i = 0; i < this.AttributeTypeHeader.Length; i++)
            {
                newAttributeTypeHeader[i] = this.AttributeTypeHeader[i];
            }
            newAttributeTypeHeader[newAttributeTypeHeader.Length - 1] = typeOfQuestion;
            this.AttributeTypeHeader = newAttributeTypeHeader;

            this.Attributes = AddNewSetOfAnswers(newSetOfAnswers, this.Attributes);
        }
        #endregion

        #region static Methods
        /// <summary>
        /// Create a new List of Answersets out of a single line of Answers
        /// </summary>
        /// <param name="firstAnswerLine">The first Line of Answers to be in the new list</param>
        /// <returns>A new List of Answers</returns>
        public static List<string[]> CreateNewAttributesList(string[] firstAnswerLine)
        {
            List<string[]> result = new List<string[]>();

            for (int i = 0; i < firstAnswerLine.Length; i++)
            {
                result.Add(new string[] { firstAnswerLine[i] });
            }

            return result;
        }

        /// <summary>
        /// Adding a new Line of Answers to an existing List of Answersets
        /// </summary>
        /// <param name="appendedAnswerLine">Line of answers to add</param>
        /// <param name="existingList">Existing list of Answersets</param>
        /// <returns></returns>
        public static List<string[]> CreateNewAttributesList(string[] appendedAnswerLine, List<string[]> existingList)
        {
            string[] newAnswerColumn;
            //look at every set of answers
            for (int i = 0; i < appendedAnswerLine.Length; i++)
            {
                newAnswerColumn = new string[existingList[i].Length + 1];
                //copy the old answers
                for (int j = 0; j < newAnswerColumn.Length - 1; j++)
                {
                    newAnswerColumn[j] = existingList[i][j];
                }
                //add new answer
                newAnswerColumn[newAnswerColumn.Length - 1] = appendedAnswerLine[i];
                //replace the old set of answers by the new one
                existingList[i] = newAnswerColumn;
            }

            return existingList;
        }

        /// <summary>
        /// Add a new set of answers for a new question to an existing list of sets of answers
        /// </summary>
        /// <param name="newSetOfAnswers">added set of answers</param>
        /// <param name="existingList">existing list sets of answers</param>
        /// <returns>new list sets of answers</returns>
        public static List<string[]> AddNewSetOfAnswers(string[] newSetOfAnswers, List<string[]> existingList)
        {
            existingList.Add(newSetOfAnswers);
            return existingList;
        }
        #endregion
    }
}

