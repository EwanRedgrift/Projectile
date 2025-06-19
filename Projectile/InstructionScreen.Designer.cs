namespace Projectile
{
    partial class InstructionScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.leaveScreen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label1.Location = new System.Drawing.Point(278, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 74);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag projectile back to fire at your opponent";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // leaveScreen
            // 
            this.leaveScreen.Location = new System.Drawing.Point(251, 303);
            this.leaveScreen.Name = "leaveScreen";
            this.leaveScreen.Size = new System.Drawing.Size(201, 54);
            this.leaveScreen.TabIndex = 1;
            this.leaveScreen.Text = "Main Menu";
            this.leaveScreen.UseVisualStyleBackColor = true;
            this.leaveScreen.Click += new System.EventHandler(this.leaveScreen_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Location = new System.Drawing.Point(278, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 74);
            this.label2.TabIndex = 2;
            this.label2.Text = "Avoid the rock in the middle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InstructionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.leaveScreen);
            this.Controls.Add(this.label1);
            this.Name = "InstructionScreen";
            this.Size = new System.Drawing.Size(804, 463);
            this.Load += new System.EventHandler(this.InstructionScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button leaveScreen;
        private System.Windows.Forms.Label label2;
    }
}
