﻿
namespace AtoIndicator.View
{
    partial class RegisterBlockByCode
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
            this.registerTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // registerTxtBox
            // 
            this.registerTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registerTxtBox.Location = new System.Drawing.Point(0, 0);
            this.registerTxtBox.Multiline = true;
            this.registerTxtBox.Name = "registerTxtBox";
            this.registerTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.registerTxtBox.Size = new System.Drawing.Size(800, 450);
            this.registerTxtBox.TabIndex = 0;
            // 
            // RegisterBlockByCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.registerTxtBox);
            this.Name = "RegisterBlockByCode";
            this.Text = "RegisterBlockByCode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox registerTxtBox;
    }
}