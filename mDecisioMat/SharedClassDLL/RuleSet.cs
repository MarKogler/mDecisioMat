using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The SharedClassDLL contains all Classes that are needed for both Client and Server
/// </summary>
namespace SharedClassDLL
{
    /// <summary>
    /// The Class RuleSet is used to open and use the read rule sets within the program
    /// </summary>
    public class RuleSet
    {
        #region Membervariables
        private string name;
        private int numberOfQuestions;
        private int numberOfAnswers;
        private string[] attributeHeader;
        private string[] attributeTypeHeader;
        private string[,] attributes; //[numberOfAnswers,numberOfQuestions]
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor. This constructor creates a new instance of the class RuleSet.
        /// </summary>
        /// <param name="name">Name of the rule set</param>
        /// <param name="attributeHeader">header-line of the rule set</param>
        /// <param name="attributeTypeHeader">attribute type line of the rule set</param>
        /// <param name="attributes">attribute matrix</param>
        public RuleSet(string name, string[] attributeHeader, string[] attributeTypeHeader, string[,] attributes)
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
        /// Property to read and write the private variable numberOfQuestion;
        /// The number of questions can be changed by the properties "AttributeHeader", "AttributeTypeHeader" and "Attributes";
        /// The user is in charge to keep data consistent
        /// </summary>
        /// <returns>value of private variable numberOfQuestions</returns>
        public int NumberOfQuestions
        {
            set
            {
                this.numberOfQuestions = value;
            }
            get
            {
                return this.numberOfQuestions;
            }
        }

        /// <summary>
        /// Property to read and write the private variable numberOfAnswers
        /// </summary>
        /// <returns>value of private variable numberOfAnswers</returns>
        public int NumberOfAnswers
        {
            set
            {
                this.numberOfAnswers = value;
            }
            get
            {
                return this.numberOfAnswers;
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
                this.NumberOfQuestions = value.Length - 2;
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
                this.NumberOfQuestions = value.Length - 2;
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
        public string[,] Attributes
        {
            set
            {
                this.attributes = value;
                this.NumberOfQuestions = value.GetLength(1) - 2;
                this.NumberOfAnswers = attributes.GetLength(0);
            }
            get
            {
                return this.attributes;
            }
        }
        #endregion

        #region Methods
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

            for (int i = 0; i < NumberOfAnswers; i++)
            {
                question[i + 2] = Attributes[i, questionNumber + 2];
            }

            return question;
        }

        /// <summary>
        /// Provide a Function to print the content to on String
        /// </summary>
        /// <returns>content string</returns>
        public override string ToString()
        {
            string contentstring = this.Name + "\n";

            for (int i = 0; i < this.AttributeHeader.Length - 1; i++)
            {
                contentstring = contentstring + this.AttributeHeader[i] + "\t";
            }
            contentstring = contentstring + this.AttributeHeader[this.AttributeHeader.Length - 1] + "\n";

            for (int i = 0; i < this.AttributeTypeHeader.Length - 1; i++)
            {
                contentstring = contentstring + this.AttributeTypeHeader[i] + "\t";
            }
            contentstring = contentstring + this.AttributeTypeHeader[this.AttributeTypeHeader.Length - 1] + "\n";

            for (int j = 0; j < this.Attributes.GetLength(0); j++)
            {
                for (int i = 0; i < this.Attributes.GetLength(1) - 1; i++)
                {
                    contentstring = contentstring + this.Attributes[j, i] + "\t";
                }
                contentstring = contentstring + this.Attributes[j, this.Attributes.GetLength(1) - 1] + "\n";
            }
            return contentstring;
        }
        #endregion
    }
}

