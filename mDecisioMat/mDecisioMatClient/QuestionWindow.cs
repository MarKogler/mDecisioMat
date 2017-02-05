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
        #endregion
        
        #region Contructor
        public QuestionWindow(RuleSet currentRuleSet, ref string resultString)
        {
            InitializeComponent();
            this.currentRuleSet = currentRuleSet;
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
