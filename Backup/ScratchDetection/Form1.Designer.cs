namespace ScratchDetection
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
            this.OriginalPicture = new System.Windows.Forms.PictureBox();
            this.ThresholdedPicture = new System.Windows.Forms.PictureBox();
            this.HoughPicture = new System.Windows.Forms.PictureBox();
            this.DetectedPicture = new System.Windows.Forms.PictureBox();
            this.ScratchThresholdTracker = new System.Windows.Forms.TrackBar();
            this.ScratchThresholdLabel = new System.Windows.Forms.Label();
            this.ComputeHoughTransform = new System.Windows.Forms.Button();
            this.PixelSizeXTextTextBox = new System.Windows.Forms.TextBox();
            this.PixelSizeYTextTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoughPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScratchThresholdTracker)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OriginalPicture
            // 
            this.OriginalPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OriginalPicture.Location = new System.Drawing.Point(15, 15);
            this.OriginalPicture.Name = "OriginalPicture";
            this.OriginalPicture.Size = new System.Drawing.Size(256, 256);
            this.OriginalPicture.TabIndex = 0;
            this.OriginalPicture.TabStop = false;
            this.OriginalPicture.Click += new System.EventHandler(this.OriginalPicture_Click);
            // 
            // ThresholdedPicture
            // 
            this.ThresholdedPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ThresholdedPicture.Location = new System.Drawing.Point(277, 15);
            this.ThresholdedPicture.Name = "ThresholdedPicture";
            this.ThresholdedPicture.Size = new System.Drawing.Size(256, 256);
            this.ThresholdedPicture.TabIndex = 1;
            this.ThresholdedPicture.TabStop = false;
            // 
            // HoughPicture
            // 
            this.HoughPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HoughPicture.Location = new System.Drawing.Point(539, 15);
            this.HoughPicture.Name = "HoughPicture";
            this.HoughPicture.Size = new System.Drawing.Size(256, 256);
            this.HoughPicture.TabIndex = 2;
            this.HoughPicture.TabStop = false;
            // 
            // DetectedPicture
            // 
            this.DetectedPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DetectedPicture.Location = new System.Drawing.Point(801, 15);
            this.DetectedPicture.Name = "DetectedPicture";
            this.DetectedPicture.Size = new System.Drawing.Size(256, 256);
            this.DetectedPicture.TabIndex = 4;
            this.DetectedPicture.TabStop = false;
            // 
            // ScratchThresholdTracker
            // 
            this.ScratchThresholdTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ScratchThresholdTracker.Location = new System.Drawing.Point(6, 43);
            this.ScratchThresholdTracker.Maximum = 100;
            this.ScratchThresholdTracker.Name = "ScratchThresholdTracker";
            this.ScratchThresholdTracker.Size = new System.Drawing.Size(438, 42);
            this.ScratchThresholdTracker.TabIndex = 5;
            this.ScratchThresholdTracker.Scroll += new System.EventHandler(this.ScratchThresholdTracker_Scroll);
            // 
            // ScratchThresholdLabel
            // 
            this.ScratchThresholdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ScratchThresholdLabel.AutoSize = true;
            this.ScratchThresholdLabel.Location = new System.Drawing.Point(15, 27);
            this.ScratchThresholdLabel.Name = "ScratchThresholdLabel";
            this.ScratchThresholdLabel.Size = new System.Drawing.Size(117, 13);
            this.ScratchThresholdLabel.TabIndex = 6;
            this.ScratchThresholdLabel.Text = "Scratch Threshold 60%";
            // 
            // ComputeHoughTransform
            // 
            this.ComputeHoughTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ComputeHoughTransform.Location = new System.Drawing.Point(459, 43);
            this.ComputeHoughTransform.Name = "ComputeHoughTransform";
            this.ComputeHoughTransform.Size = new System.Drawing.Size(87, 27);
            this.ComputeHoughTransform.TabIndex = 7;
            this.ComputeHoughTransform.Text = "Find Scratches";
            this.ComputeHoughTransform.UseVisualStyleBackColor = true;
            this.ComputeHoughTransform.Click += new System.EventHandler(this.ComputeHoughTransform_Click);
            // 
            // PixelSizeXTextTextBox
            // 
            this.PixelSizeXTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PixelSizeXTextTextBox.Location = new System.Drawing.Point(691, 50);
            this.PixelSizeXTextTextBox.Name = "PixelSizeXTextTextBox";
            this.PixelSizeXTextTextBox.Size = new System.Drawing.Size(36, 20);
            this.PixelSizeXTextTextBox.TabIndex = 8;
            this.PixelSizeXTextTextBox.Text = "1";
            // 
            // PixelSizeYTextTextBox
            // 
            this.PixelSizeYTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PixelSizeYTextTextBox.Location = new System.Drawing.Point(733, 50);
            this.PixelSizeYTextTextBox.Name = "PixelSizeYTextTextBox";
            this.PixelSizeYTextTextBox.Size = new System.Drawing.Size(36, 20);
            this.PixelSizeYTextTextBox.TabIndex = 9;
            this.PixelSizeYTextTextBox.Text = "1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pixel Dimensions";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(775, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "microns";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ScratchThresholdLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ScratchThresholdTracker);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ComputeHoughTransform);
            this.panel1.Controls.Add(this.PixelSizeYTextTextBox);
            this.panel1.Controls.Add(this.PixelSizeXTextTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 87);
            this.panel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 364);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DetectedPicture);
            this.Controls.Add(this.HoughPicture);
            this.Controls.Add(this.ThresholdedPicture);
            this.Controls.Add(this.OriginalPicture);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Scratch Detection";
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoughPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetectedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScratchThresholdTracker)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox OriginalPicture;
        private System.Windows.Forms.PictureBox ThresholdedPicture;
        private System.Windows.Forms.PictureBox HoughPicture;
        private System.Windows.Forms.PictureBox DetectedPicture;
        private System.Windows.Forms.TrackBar ScratchThresholdTracker;
        private System.Windows.Forms.Label ScratchThresholdLabel;
        private System.Windows.Forms.Button ComputeHoughTransform;
        private System.Windows.Forms.TextBox PixelSizeXTextTextBox;
        private System.Windows.Forms.TextBox PixelSizeYTextTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}

