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
            if (questionNumber < this.currentRuleSet.NumberOfQuestions)
            {
                this.actualQuestion = this.currentRuleSet.GetQuestion(questionNumber);
                this.vonbisQuestion = this.actualQuestion[1] == "vonbis";
                this.actualAnswerSet = new string[this.actualQuestion.Length - 2];
                this.gbQuestion.Text = this.actualQuestion[0];

                for (int i = 0; i < this.actualAnswerSet.Length; i++)
                {
                    this.actualAnswerSet[i] = this.actualQuestion[i + 2];
                }

                this.clbAnswers.Items.Clear();
                if (this.vonbisQuestion)
                {
                    this.actualVonbisAnswerSet = ParseAnsweSetToInt(this.actualAnswerSet);
                    for (int i = 0; i < this.actualVonbisAnswerSet.Length; i++)
                    {
                        if (this.sweepLine[i] && !this.clbAnswers.Items.Contains(this.actualVonbisAnswerSet[i]))
                        {
                            this.clbAnswers.Items.Add(this.actualVonbisAnswerSet[i]);
                        }
                    }
                    this.nudLowerLimit.Enabled = true;
                    this.nudUpperLimit.Enabled = true;
                    this.nudLowerLimit.Maximum = this.GetMaximumPossibleAnswer();
                    this.nudUpperLimit.Maximum = this.nudLowerLimit.Maximum;
                    this.nudLowerLimit.Minimum = this.GetMinimumPossibleAnswer();
                    this.nudUpperLimit.Minimum = this.nudLowerLimit.Minimum;
                    this.nudLowerLimit.Value = this.nudLowerLimit.Minimum;
                    this.nudUpperLimit.Value = this.nudUpperLimit.Maximum;
                    this.btnOK.Enabled = true;
                }
                else
                {
                    for (int i = 0; i < this.actualAnswerSet.Length; i++)
                    {
                        if (this.sweepLine[i] && !this.clbAnswers.Items.Contains(this.actualAnswerSet[i]))
                        {
                            this.clbAnswers.Items.Add(this.actualAnswerSet[i]);
                        }
                    }
                    this.nudLowerLimit.Enabled = false;
                    this.nudUpperLimit.Enabled = false;
                    this.btnOK.Enabled = false;
                }
            }
            else
            { 
                this.mainWindow.AnswerString = CreateAnswerString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private string CreateAnswerString()
        {
            string result = "Possible choices:" + Environment.NewLine;
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                if (this.sweepLine[i])
                {
                    result = result + this.currentRuleSet.Attributes[1][i] + Environment.NewLine;
                }
            }

            return result;
        }
        private int GetMinimumPossibleAnswer()
        {
            int result = this.actualVonbisAnswerSet.Max();
            for (int i = 0; i < this.sweepLine.Length; i++)
            {
                if (this.sweepLine[i] && this.actualVonbisAnswerSet[i] < result)
                {
                    result = this.actualVonbisAnswerSet[i];
                }
            }
            return result;
        }

        private int GetMaximumPossibleAnswer()
        {
            int result = this.actualVonbisAnswerSet.Min();
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

        private void btnSkipAttribute_Click(object sender, EventArgs e)
        {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.vonbisQuestion)
            {
                for (int i = 0; i < this.sweepLine.Length; i++)
                {
                    this.sweepLine[i] = this.sweepLine[i] 
                        && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                        && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
                }
            }
            else
            {
                for (int i = 0; i < this.sweepLine.Length; i++)
                {
                    this.sweepLine[i] = this.sweepLine[i] && this.clbAnswers.CheckedItems.Contains(this.actualAnswerSet[i]);
                }
            }

            this.actualQuestionNumber++;
            AskQuestion(this.actualQuestionNumber);
        }

        private void nudLowerLimit_ValueChanged(object sender, EventArgs e)
        {
            bool possibleAnswersRemaining = false;
            for (int i = 0; i < this.sweepLine.Length && !possibleAnswersRemaining; i++)
            {
                possibleAnswersRemaining = this.sweepLine[i]
                    && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                    && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
            }
            if (possibleAnswersRemaining)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
            this.nudUpperLimit.Minimum = this.nudLowerLimit.Value;
        }

        private void nudUpperLimit_ValueChanged(object sender, EventArgs e)
        {
            bool possibleAnswersRemaining = false;
            for (int i = 0; i < this.sweepLine.Length && !possibleAnswersRemaining; i++)
            {
                possibleAnswersRemaining = this.sweepLine[i]
                    && this.actualVonbisAnswerSet[i] <= this.nudUpperLimit.Value
                    && this.actualVonbisAnswerSet[i] >= this.nudLowerLimit.Value;
            }
            if (possibleAnswersRemaining)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
            this.nudLowerLimit.Maximum = this.nudUpperLimit.Value;
        }

        private void clbAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!vonbisQuestion)
            {
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
    }
}
