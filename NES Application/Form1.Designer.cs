
namespace NES_Application
{
    partial class Form1
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
            this.ppuOutput = new System.Windows.Forms.PictureBox();
            this.memDump = new System.Windows.Forms.Button();
            this.printReg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ppuOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // ppuOutput
            // 
            this.ppuOutput.Location = new System.Drawing.Point(12, 12);
            this.ppuOutput.Name = "ppuOutput";
            this.ppuOutput.Size = new System.Drawing.Size(776, 397);
            this.ppuOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ppuOutput.TabIndex = 0;
            this.ppuOutput.TabStop = false;
            // 
            // memDump
            // 
            this.memDump.Location = new System.Drawing.Point(668, 415);
            this.memDump.Name = "memDump";
            this.memDump.Size = new System.Drawing.Size(120, 23);
            this.memDump.TabIndex = 1;
            this.memDump.Text = "Dump Memory";
            this.memDump.UseVisualStyleBackColor = true;
            this.memDump.Click += new System.EventHandler(this.memDump_Click);
            // 
            // printReg
            // 
            this.printReg.Location = new System.Drawing.Point(542, 415);
            this.printReg.Name = "printReg";
            this.printReg.Size = new System.Drawing.Size(120, 23);
            this.printReg.TabIndex = 2;
            this.printReg.Text = "Print Registers";
            this.printReg.UseVisualStyleBackColor = true;
            this.printReg.Click += new System.EventHandler(this.printReg_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.printReg);
            this.Controls.Add(this.memDump);
            this.Controls.Add(this.ppuOutput);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ppuOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox ppuOutput;
        private System.Windows.Forms.Button memDump;
        private System.Windows.Forms.Button printReg;
    }
}

