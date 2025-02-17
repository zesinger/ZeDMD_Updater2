namespace ZeDMD_Updater2
{
    partial class WaitForm
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
            this.mainText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainText
            // 
            this.mainText.AutoEllipsis = true;
            this.mainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainText.Location = new System.Drawing.Point(12, 9);
            this.mainText.Name = "mainText";
            this.mainText.Size = new System.Drawing.Size(598, 212);
            this.mainText.TabIndex = 0;
            this.mainText.Text = "Please wait while populating the versions available...";
            this.mainText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 230);
            this.Controls.Add(this.mainText);
            this.MaximizeBox = false;
            this.Name = "WaitForm";
            this.Text = "Please wait...";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label mainText;
    }
}