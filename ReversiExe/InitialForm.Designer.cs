namespace ReversiExe
{
    partial class InitialForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textColor = new System.Windows.Forms.ComboBox();
            this.textBias = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COLOR: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BIAS factor:";
            // 
            // textColor
            // 
            this.textColor.FormattingEnabled = true;
            this.textColor.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.textColor.Location = new System.Drawing.Point(107, 64);
            this.textColor.Name = "textColor";
            this.textColor.Size = new System.Drawing.Size(145, 21);
            this.textColor.TabIndex = 4;
            // 
            // textBias
            // 
            this.textBias.FormattingEnabled = true;
            this.textBias.Items.AddRange(new object[] {
            "0",
            ".1",
            ".2",
            ".3",
            ".4",
            ".5",
            ".6",
            ".7",
            ".8",
            ".9",
            "1"});
            this.textBias.Location = new System.Drawing.Point(107, 123);
            this.textBias.Name = "textBias";
            this.textBias.Size = new System.Drawing.Size(145, 21);
            this.textBias.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = "";
            this.btnOK.Location = new System.Drawing.Point(137, 185);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(115, 34);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(48, 178);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Heads";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(48, 202);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Tails";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 281);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBias);
            this.Controls.Add(this.textColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InitialForm";
            this.Text = "InitialForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox textColor;
        private System.Windows.Forms.ComboBox textBias;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}