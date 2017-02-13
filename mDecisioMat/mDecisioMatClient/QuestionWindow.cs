using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedClassDLL;

/// <summary>
/// Client for Decision Making
/// </summary>
namespace mDecisioMatClient
{
    /// <summary>
    /// Window for decision finding dialog
    /// </summary>
    public partial class QuestionWindow : Form
    {
        #region Membervariables
        private RuleSet currentRuleSet;
        private bool[] sweepLine;
        private int actualQuestionNumber;
        private string[] actualQuestion;
        private bool vonbisQuestion;
        private int[] actualVonbisAnswerSet;
        private string[] actualAnswerSet;
        private Client mainWindow;
        #endregion

        #region Contructor
        /// <summary>
        /// Standard Constructor
        /// </summary>
        /// <param name="currentRuleSet">RuleSet to be asked</param>
        /// <param name="mainWindow">Reference to main Window for response</param>
        public QuestionWindow(RuleSet currentRuleSet, Client mainWindow)
        {
            InitializeComponent();
            this.currentRuleSet = currentRuleSet;
            this.mainWindow = mainWindow;
            this.sweepLine = new bool[currentRuleSet.NumberOfAnswers];
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                this.sweepLine[i] = true;
            }

            this.actualQuestionNumber = 0;
            AskQuestion(this.actualQuestionNumber);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to Parse a Set of answers from string into int; all Answers must be parse able
        /// </summary>
        /// <param name="answerSet">set of answers of type string</param>
        /// <returns>resulting set of answers of type int</returns>
        private int[] ParseAnsweSetToInt(string[] answerSet)
        {
            int[] intAnswerSet = new int[answerSet.Length];
            for (int i = 0; i < answerSet.Length; i++)
            {
                intAnswerSet[i] = int.Parse(answerSet[i]);
            }
            return intAnswerSet;
        }

        /// <summary>
        /// Method to prepare the actual question to be asked
        /// </summary>
        /// <param name="questionNumber">number of actual question</param>
        private void AskQuestion(int questionNumber)
        {
            //The sequence ends automatically if there is no further question to be asked
            if (questionNumber < this.currentRuleSet.NumberOfQuestions)
            {
                //Get the actual question
                this.actualQuestion = this.currentRuleSet.GetQuestion(questionNumber);
                //Get if the question needs numeric input
                this.vonbisQuestion = this.actualQuestion[1] == "vonbis";
                //separate answers
                this.actualAnswerSet = new string[this.actualQuestion.Length - 2];
                for (int i = 0; i < this.actualAnswerSet.Length; i++)
                {
                    this.actualAnswerSet[i] = this.actualQuestion[i + 2];
                }

                //Display attribute to the user
                this.gbQuestion.Text = this.actualQuestion[0];

                //Show possible choices
                //Delete old content
                this.clbAnswers.Items.Clear();
                //"vonbis" questions need numeric input
                if (this.vonbisQuestion)
                {
                    //"vonbis" question
                    //Parse Answerset
                    this.actualVonbisAnswerSet = ParseAnsweSetToInt(this.actualAnswerSet);
                    //add chooseable choices to the checked list box
                    for (int i = 0; i < this.actualVonbisAnswerSet.Length; i++)
                    {
                        if (this.sweepLine[i] && !this.clbAnswers.Items.Contains(this.actualVonbisAnswerSet[i]))
                        {
                            this.clbAnswers.Items.Add(this.actualVonbisAnswerSet[i]);
                        }
                    }
                    //Enable and initialize numeric input control elements
                    this.nudLowerLimit.Enabled = true;
                    this.nudUpperLimit.Enabled = true;
                    this.nudLowerLimit.Maximum = this.GetMaximumPossibleAnswer();
                    this.nudUpperLimit.Maximum = this.nudLowerLimit.Maximum;
                    this.nudLowerLimit.Minimum = this.GetMinimumPossibleAnswer();
                    this.nudUpperLimit.Minimum = this.nudLowerLimit.Minimum;
                    this.nudLowerLimit.Value = this.nudLowerLimit.Minimum;
                    this.nudUpperLimit.Value = this.nudUpperLimit.Maximum;
                    this.btnOK.Enabled = true; //in this case the range is valid
                }
                else
                {
                    //no "vonbis" question
                    //add possible choices to checked list box
                    for (int i = 0; i < this.actualAnswerSet.Length; i++)
                    {
                        if (this.sweepLine[i] && !this.clbAnswers.Items.Contains(this.actualAnswerSet[i]))
                        {
                            this.clbAnswers.Items.Add(this.actualAnswerSet[i]);
                        }
                    }
                    //disable numeric elements
                    this.nudLowerLimit.Enabled = false;
                    this.nudUpperLimit.Enabled = false;
                    this.btnOK.Enabled = false; //no valid choice has been made at this moment
                }
            }
            else
            { 
                //Automatic end of the sequence
                this.mainWindow.AnswerString = CreateAnswerString();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Method to put the desciptive strings of each of the remaining answers into one common formattet answer string
        /// </summary>
        /// <returns>formatted answer string</returns>
        private string CreateAnswerString()
        {
            //Header
            string result = "Possible choices:" + Environment.NewLine;
            //remaining answers
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                if (this.sweepLine[i])
                {
                    result = result + this.currentRuleSet.Attributes[1][i] + Environment.NewLine;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the minimum of the remaining answers in a "vonbis" answerset;
        /// method will deliver the max value of all answers if there is no possible answer left
        /// </summary>
        /// <returns>minimum remaining answer</returns>
        private int GetMinimumPossibleAnswer()
        {
            //start at the biggest value of the whole answerset
            int result = this.actualVonbisAnswerSet.Max();
            //find the minimum possible value
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                if (this.sweepLine[i] && this.actualVonbisAnswerSet[i] < result)
                {
                    result = this.actualVonbisAnswerSet[i];
                }
            }
            return result;
        }

        /// <summary>
        /// Find the maximum of the remaining answers in a "vonbis" answerset;
        /// method will deliver the min value of all answers if there is no possible answer left
        /// </summary>
        /// <returns></returns>
        private int GetMaximumPossibleAnswer()
        {
            //start at the minimum value of the whole answerset
            int result = this.actualVonbisAnswerSet.Min();
            //find the maximum possible value
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                if (this.sweepLine[i] && this.actualVonbisAnswerSet[i] > result)
                {
                    result = this.actualVonbisAnswerSet[i];
                }
            }
            return result;
        }
        #endregion

        #region enventhandler
        /// <summary>
        /// Method to skip a question in case the skip button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkipAttribute_Click(object sender, EventArgs e)
        {
            //increase question number and ask next question; ignore chosen answers
            this.actualQuestionNumber++;
            AskQuestion(this.actualQuestionNumber);
        }

        /// <summary>
        /// Method to cancel the decision finding dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Method to process user choices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //different algorithm for "vonbis" questions
            if (this.vonbisQuestion)
            {
                //"vonbis" question
                //get all remaining answers and update sweepline
                for (int i = 0; i < this.sweepLine.Length; i++)
                {
                    //sweepline stays true if it has been true and the value of this answer is between the user set borders
                    this.sweepLine[i] = this.sweepLine[i] 
                        && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                        && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
                }
            }
            else
            {
                //no "vonbis" question
                //get all remaining answers and update sweepline
                for (int i = 0; i < this.sweepLine.Length; i++)
                {
                    //sweepline stays true if it has been true and the attribute of this answer has been chosen by the user
                    this.sweepLine[i] = this.sweepLine[i] && this.clbAnswers.CheckedItems.Contains(this.actualAnswerSet[i]);
                }
            }

            //increase question number and ask next question
            this.actualQuestionNumber++;
            AskQuestion(this.actualQuestionNumber);
        }

        /// <summary>
        /// Method to update the minimum value for the upper limit and to ensure that there are possible answers remaining
        /// disable ok button if there are no possible answers remaining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudLowerLimit_ValueChanged(object sender, EventArgs e)
        {
            //look for remaining possible answers
            bool possibleAnswersRemaining = false;
            for (int i = 0; i < this.sweepLine.Length && !possibleAnswersRemaining; i++)
            {
                possibleAnswersRemaining = this.sweepLine[i]
                    && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                    && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
            }
            //enable and disable ok button depending on remaining possible answers
            if (possibleAnswersRemaining)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
            //upadte minimum for upper limit
            this.nudUpperLimit.Minimum = this.nudLowerLimit.Value;
        }

        /// <summary>
        /// Method to update the maximum value for the lower limit and to ensure that there are possible answers remaining
        /// disable ok button if there are no possible answers remaining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudUpperLimit_ValueChanged(object sender, EventArgs e)
        {
            //look for remaining possible answers
            bool possibleAnswersRemaining = false;
            for (int i = 0; i < this.sweepLine.Length && !possibleAnswersRemaining; i++)
            {
                possibleAnswersRemaining = this.sweepLine[i]
                    && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                    && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
            }
            //enable and disable ok button depending on remaining possible answers
            if (possibleAnswersRemaining)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
            //upadte maximum for lower limit
            this.nudLowerLimit.Maximum = this.nudUpperLimit.Value;
        }

        /// <summary>
        /// Method to ensure that the ok button is only enabled if there are possible answers selected
        /// this method only is needed if the question is no "vonbis" question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clbAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //enable if no "vonbis" question
            if (!vonbisQuestion)
            {
                //enable and disable ok button depending on the number of chosen answers
                if (this.clbAnswers.CheckedIndices.Count > 0)
                {
                    this.btnOK.Enabled = true;
                }
                else
                {
                    this.btnOK.Enabled = false;
                }
            }

        }
        #endregion
    }
}
