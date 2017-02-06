namespace mDecisioMatClient
{
    partial class Client
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRuleSetSelection = new System.Windows.Forms.GroupBox();
            this.lbRuleSets = new System.Windows.Forms.ListBox();
            this.btnSelectRuleSet = new System.Windows.Forms.Button();
            this.btnGetAvailableRuleSets = new System.Windows.Forms.Button();
            this.gbCurrentRuleSet = new System.Windows.Forms.GroupBox();
            this.gbDecisionMaker = new System.Windows.Forms.GroupBox();
            this.tbxAnswer = new System.Windows.Forms.TextBox();
            this.btnGetDecision = new System.Windows.Forms.Button();
            this.tbxCurrentRuleSet = new System.Windows.Forms.TextBox();
            this.gbRuleSetSelection.SuspendLayout();
            this.gbCurrentRuleSet.SuspendLayout();
            this.gbDecisionMaker.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRuleSetSelection
            // 
            this.gbRuleSetSelection.Controls.Add(this.lbRuleSets);
            this.gbRuleSetSelection.Controls.Add(this.btnSelectRuleSet);
            this.gbRuleSetSelection.Controls.Add(this.btnGetAvailableRuleSets);
            this.gbRuleSetSelection.Location = new System.Drawing.Point(12, 12);
            this.gbRuleSetSelection.Name = "gbRuleSetSelection";
            this.gbRuleSetSelection.Size = new System.Drawing.Size(260, 228);
            this.gbRuleSetSelection.TabIndex = 0;
            this.gbRuleSetSelection.TabStop = false;
            this.gbRuleSetSelection.Text = "Rule Set Selection";
            // 
            // lbRuleSets
            // 
            this.lbRuleSets.Enabled = false;
            this.lbRuleSets.FormattingEnabled = true;
            this.lbRuleSets.Location = new System.Drawing.Point(6, 74);
            this.lbRuleSets.Name = "lbRuleSets";
            this.lbRuleSets.ScrollAlwaysVisible = true;
            this.lbRuleSets.Size = new System.Drawing.Size(248, 147);
            this.lbRuleSets.TabIndex = 2;
            // 
            // btnSelectRuleSet
            // 
            this.btnSelectRuleSet.Enabled = false;
            this.btnSelectRuleSet.Location = new System.Drawing.Point(134, 19);
            this.btnSelectRuleSet.Name = "btnSelectRuleSet";
            this.btnSelectRuleSet.Size = new System.Drawing.Size(120, 49);
            this.btnSelectRuleSet.TabIndex = 1;
            this.btnSelectRuleSet.Text = "select rule set";
            this.btnSelectRuleSet.UseVisualStyleBackColor = true;
            this.btnSelectRuleSet.Click += new System.EventHandler(this.btnSelectRuleSet_Click);
            // 
            // btnGetAvailableRuleSets
            // 
            this.btnGetAvailableRuleSets.Location = new System.Drawing.Point(6, 19);
            this.btnGetAvailableRuleSets.Name = "btnGetAvailableRuleSets";
            this.btnGetAvailableRuleSets.Size = new System.Drawing.Size(120, 49);
            this.btnGetAvailableRuleSets.TabIndex = 1;
            this.btnGetAvailableRuleSets.Text = "get available rule sets";
            this.btnGetAvailableRuleSets.UseVisualStyleBackColor = true;
            this.btnGetAvailableRuleSets.Click += new System.EventHandler(this.btnGetAvailableRuleSets_Click);
            // 
            // gbCurrentRuleSet
            // 
            this.gbCurrentRuleSet.Controls.Add(this.tbxCurrentRuleSet);
            this.gbCurrentRuleSet.Location = new System.Drawing.Point(12, 246);
            this.gbCurrentRuleSet.Name = "gbCurrentRuleSet";
            this.gbCurrentRuleSet.Size = new System.Drawing.Size(539, 207);
            this.gbCurrentRuleSet.TabIndex = 1;
            this.gbCurrentRuleSet.TabStop = false;
            this.gbCurrentRuleSet.Text = "Current Rule Set";
            // 
            // gbDecisionMaker
            // 
            this.gbDecisionMaker.Controls.Add(this.tbxAnswer);
            this.gbDecisionMaker.Controls.Add(this.btnGetDecision);
            this.gbDecisionMaker.Location = new System.Drawing.Point(278, 12);
            this.gbDecisionMaker.Name = "gbDecisionMaker";
            this.gbDecisionMaker.Size = new System.Drawing.Size(273, 228);
            this.gbDecisionMaker.TabIndex = 2;
            this.gbDecisionMaker.TabStop = false;
            this.gbDecisionMaker.Text = "Decision Maker";
            // 
            // tbxAnswer
            // 
            this.tbxAnswer.Location = new System.Drawing.Point(6, 74);
            this.tbxAnswer.Multiline = true;
            this.tbxAnswer.Name = "tbxAnswer";
            this.tbxAnswer.ReadOnly = true;
            this.tbxAnswer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxAnswer.Size = new System.Drawing.Size(261, 147);
            this.tbxAnswer.TabIndex = 2;
            // 
            // btnGetDecision
            // 
            this.btnGetDecision.Enabled = false;
            this.btnGetDecision.Location = new System.Drawing.Point(6, 19);
            this.btnGetDecision.Name = "btnGetDecision";
            this.btnGetDecision.Size = new System.Drawing.Size(261, 49);
            this.btnGetDecision.TabIndex = 1;
            this.btnGetDecision.Text = "get decision";
            this.btnGetDecision.UseVisualStyleBackColor = true;
            this.btnGetDecision.Click += new System.EventHandler(this.btnGetDecision_Click);
            // 
            // tbxCurrentRuleSet
            // 
            this.tbxCurrentRuleSet.Location = new System.Drawing.Point(6, 19);
            this.tbxCurrentRuleSet.Multiline = true;
            this.tbxCurrentRuleSet.Name = "tbxCurrentRuleSet";
            this.tbxCurrentRuleSet.ReadOnly = true;
            this.tbxCurrentRuleSet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxCurrentRuleSet.Size = new System.Drawing.Size(527, 182);
            this.tbxCurrentRuleSet.TabIndex = 2;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 465);
            this.Controls.Add(this.gbDecisionMaker);
            this.Controls.Add(this.gbCurrentRuleSet);
            this.Controls.Add(this.gbRuleSetSelection);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.gbRuleSetSelection.ResumeLayout(false);
            this.gbCurrentRuleSet.ResumeLayout(false);
            this.gbCurrentRuleSet.PerformLayout();
            this.gbDecisionMaker.ResumeLayout(false);
            this.gbDecisionMaker.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRuleSetSelection;
        private System.Windows.Forms.ListBox lbRuleSets;
        private System.Windows.Forms.Button btnSelectRuleSet;
        private System.Windows.Forms.Button btnGetAvailableRuleSets;
        private System.Windows.Forms.GroupBox gbCurrentRuleSet;
        private System.Windows.Forms.GroupBox gbDecisionMaker;
        private System.Windows.Forms.Button btnGetDecision;
        private System.Windows.Forms.TextBox tbxAnswer;
        private System.Windows.Forms.TextBox tbxCurrentRuleSet;
    }
}

