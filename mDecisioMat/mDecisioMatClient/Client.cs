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
using System.ServiceModel;
using System.Runtime.Serialization;


/// <summary>
/// Client for Decision Making
/// </summary>
namespace mDecisioMatClient
{
    /// <summary>
    /// Main Window
    /// </summary>
    public partial class Client : Form
    {
        #region Membervariables
        private RuleSyncInterface ruleInterface;
        private string[] availableRuleSets;
        private RuleSet currentRuleSet;
        private QuestionWindow questionWindow;
        private string answerString;
        private DialogResult dialogResult;
        #endregion

        #region Constructor
        /// <summary>
        /// Standard Constructor
        /// </summary>
        public Client()
        {
            InitializeComponent();

            ChannelFactory<RuleSyncInterface> cFactory;
            cFactory = new ChannelFactory<RuleSyncInterface>("WSHttpBinding_RuleSyncInterface");
            ruleInterface = cFactory.CreateChannel();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property to write to the private variable answerString
        /// </summary>
        public string AnswerString
        {
            set
            {
                this.answerString = value;
            }
        }
        #endregion

        /// <summary>
        /// Eventhandler for the closing event of the Main Windeow to close the communication as well
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            (ruleInterface as ICommunicationObject).Close();
        }

        /// <summary>
        /// Method to get a list of available rule sets from the server.
        /// By getting valid data this event also enables the ability to choose a specific rule set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetAvailableRuleSets_Click(object sender, EventArgs e)
        {
            //Get available RuleSets
            try
            {
                this.availableRuleSets = ruleInterface.GetAvailableRuleSets();
            }
            catch
            {
                this.availableRuleSets = null;
                this.rtbCurrentRuleSet.Text = "Server Connection Error";
            }
            //If there are RuleSets available List them and enable neccessary control elements
            if (this.availableRuleSets != null)
            {
                this.lbRuleSets.Enabled = true;

                this.lbRuleSets.Items.Clear();
                for (int i = 0; i < this.availableRuleSets.Length; i++)
                {
                    this.lbRuleSets.Items.Add(this.availableRuleSets[i]);
                }
                //Select Index so there is never no Index Selected
                this.lbRuleSets.SelectedIndex = 0;
                this.btnSelectRuleSet.Enabled = true;
            }
            else
            {
                //disable harmful controls if there are no RuleSets Available
                this.lbRuleSets.Enabled = false;
                this.btnSelectRuleSet.Enabled = false;
                this.btnGetDecision.Enabled = false;
            }
        }

        /// <summary>
        /// Method to get the chosen rule set from the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectRuleSet_Click(object sender, EventArgs e)
        {
            //Get chosen RuleSet
            try
            {
                this.currentRuleSet = this.ruleInterface.GetSpecificRuleSet(this.lbRuleSets.SelectedItem.ToString());
            }
            catch
            {
                this.currentRuleSet = null;
                this.rtbCurrentRuleSet.Text = "Server Connection Error";
            }

            //Make sure RuleSet is valid
            if (this.currentRuleSet != null)
            {
                this.btnGetDecision.Enabled = true;
                this.rtbCurrentRuleSet.Text = this.currentRuleSet.ToString();
            }
            else
            {
                this.btnGetDecision.Enabled = false;
            }
        }

        /// <summary>
        /// Method to start a decision finding dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetDecision_Click(object sender, EventArgs e)
        {
            //Ask Questions
            this.questionWindow = new QuestionWindow(this.currentRuleSet, this);
            this.dialogResult = this.questionWindow.ShowDialog();

            //Display result
            if (this.dialogResult == DialogResult.OK)
            {
                this.tbxAnswer.Text = this.answerString;
            }
            else
            {
                this.tbxAnswer.Text = "Decision process canceled";
            }
        }
    }
}
