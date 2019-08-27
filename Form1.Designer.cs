namespace AltayFaz1
{
    partial class ehopesForm
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
            this.txtStatement = new System.Windows.Forms.RichTextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.staPanel = new System.Windows.Forms.Panel();
            this.crossBox = new System.Windows.Forms.PictureBox();
            this.btDelete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.staPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtStatement
            // 
            this.txtStatement.BackColor = System.Drawing.SystemColors.Menu;
            this.txtStatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatement.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtStatement.Location = new System.Drawing.Point(28, 39);
            this.txtStatement.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(464, 174);
            this.txtStatement.TabIndex = 4;
            this.txtStatement.Text = "";
            // 
            // btSave
            // 
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Location = new System.Drawing.Point(28, 278);
            this.btSave.Margin = new System.Windows.Forms.Padding(4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(100, 28);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // staPanel
            // 
            this.staPanel.Controls.Add(this.crossBox);
            this.staPanel.Controls.Add(this.btDelete);
            this.staPanel.Controls.Add(this.txtStatement);
            this.staPanel.Controls.Add(this.button1);
            this.staPanel.Controls.Add(this.btSave);
            this.staPanel.Location = new System.Drawing.Point(533, 156);
            this.staPanel.Margin = new System.Windows.Forms.Padding(4);
            this.staPanel.Name = "staPanel";
            this.staPanel.Size = new System.Drawing.Size(531, 321);
            this.staPanel.TabIndex = 9;
            this.staPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.staPanel_MouseDown_1);
            this.staPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.staPanel_MouseMove_1);
            // 
            // crossBox
            // 
            this.crossBox.Location = new System.Drawing.Point(490, 6);
            this.crossBox.Name = "crossBox";
            this.crossBox.Size = new System.Drawing.Size(29, 26);
            this.crossBox.TabIndex = 10;
            this.crossBox.TabStop = false;
            // 
            // btDelete
            // 
            this.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDelete.Location = new System.Drawing.Point(392, 278);
            this.btDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(100, 28);
            this.btDelete.TabIndex = 9;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(210, 278);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ehopesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.staPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ehopesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EHOPES";
            this.Load += new System.EventHandler(this.ehopesForm_Load);
            this.staPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crossBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtStatement;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel staPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.PictureBox crossBox;
    }
}

