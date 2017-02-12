namespace mDecisioMatClient
{
    partial class QuestionWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbQuestion = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSkipAttribute = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.nudUpperLimit = new System.Windows.Forms.NumericUpDown();
            this.nudLowerLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.clbAnswers = new System.Windows.Forms.CheckedListBox();
            this.gbQuestion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpperLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowerLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // gbQuestion
            // 
            this.gbQuestion.Controls.Add(this.btnCancel);
            this.gbQuestion.Controls.Add(this.btnSkipAttribute);
            this.gbQuestion.Controls.Add(this.btnOK);
            this.gbQuestion.Controls.Add(this.nudUpperLimit);
            this.gbQuestion.Controls.Add(this.nudLowerLimit);
            this.gbQuestion.Controls.Add(this.label2);
            this.gbQuestion.Controls.Add(this.label1);
            this.gbQuestion.Controls.Add(this.clbAnswers);
            this.gbQuestion.Location = new System.Drawing.Point(12, 12);
            this.gbQuestion.Name = "gbQuestion";
            this.gbQuestion.Size = new System.Drawing.Size(267, 214);
            this.gbQuestion.TabIndex = 0;
            this.gbQuestion.TabStop = false;
            this.gbQuestion.Text = "Question";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(181, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSkipAttribute
            // 
            this.btnSkipAttribute.Location = new System.Drawing.Point(95, 173);
            this.btnSkipAttribute.Name = "btnSkipAttribute";
            this.btnSkipAttribute.Size = new System.Drawing.Size(80, 35);
            this.btnSkipAttribute.TabIndex = 1;
            this.btnSkipAttribute.Text = "skip attribute";
            this.btnSkipAttribute.UseVisualStyleBackColor = true;
            this.btnSkipAttribute.Click += new System.EventHandler(this.btnSkipAttribute_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(6, 173);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 35);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // nudUpperLimit
            // 
            this.nudUpperLimit.Location = new System.Drawing.Point(141, 145);
            this.nudUpperLimit.Name = "nudUpperLimit";
            this.nudUpperLimit.Size = new System.Drawing.Size(120, 20);
            this.nudUpperLimit.TabIndex = 1;
            this.nudUpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudLowerLimit
            // 
            this.nudLowerLimit.Location = new System.Drawing.Point(141, 119);
            this.nudLowerLimit.Name = "nudLowerLimit";
            this.nudLowerLimit.Size = new System.Drawing.Size(120, 20);
            this.nudLowerLimit.TabIndex = 1;
            this.nudLowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLowerLimit.ValueChanged += new System.EventHandler(this.nudLowerLimit_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Upper Limit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lower Limit:";
            // 
            // clbAnswers
            // 
            this.clbAnswers.FormattingEnabled = true;
            this.clbAnswers.Location = new System.Drawing.Point(6, 19);
            this.clbAnswers.Name = "clbAnswers";
            this.clbAnswers.Size = new System.Drawing.Size(255, 94);
            this.clbAnswers.TabIndex = 1;
            this.clbAnswers.SelectedIndexChanged += new System.EventHandler(this.clbAnswers_SelectedIndexChanged);
            // 
            // QuestionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
            this.Controls.Add(this.gbQuestion);
            this.MaximumSize = new System.Drawing.Size(300, 270);
            this.MinimumSize = new System.Drawing.Size(300, 270);
            this.Name = "QuestionWindow";
            this.Text = "QuestionWindow";
            this.gbQuestion.ResumeLayout(false);
            this.gbQuestion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpperLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLowerLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbQuestion;
        private System.Windows.Forms.Button btnSkipAttribute;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown nudUpperLimit;
        private System.Windows.Forms.NumericUpDown nudLowerLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbAnswers;
        private System.Windows.Forms.Button btnCancel;
    }
}