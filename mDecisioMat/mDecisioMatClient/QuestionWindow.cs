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
        private string answerString;
        private string[] actualQuestion;
        #endregion
        
        #region Contructor
        public QuestionWindow(RuleSet currentRuleSet, ref string resultString)
        {
            InitializeComponent();
            this.currentRuleSet = currentRuleSet;
            this.sweepLine = new bool[currentRuleSet.NumberOfAnswers];

            this.actualQuestion = this.currentRuleSet.GetQuestion(0);
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

        private void AskQuestion(int questionNumber)
        {
            this.actualQuestion = this.currentRuleSet.GetQuestion(questionNumber);

        }
        #endregion

        private void btnSkipAttribute_Click(object sender, EventArgs e)
        {

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
    }
}
