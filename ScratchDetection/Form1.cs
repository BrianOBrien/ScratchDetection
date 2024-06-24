using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScratchDetection
{
    public partial class Form1 : Form
    {
        private Bitmap OriginalImage;
        private Bitmap ThresholdImage;
        private Bitmap HoughImage;
        private Bitmap DetectedImage;
        private ImageProcessing ip;

        int _margin = 10;

        public Form1()
        {
            InitializeComponent();

            ip = new ImageProcessing();

            ScratchThresholdTracker.Value = 60;

            OriginalPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            ThresholdedPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            HoughPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            DetectedPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SuspendLayout();
            int w = this.ClientRectangle.Width;
            int h = this.ClientRectangle.Height;
            int pboxW = (w - (_margin * 5)) / 4;
            int pboxH = h - panel1.Height - (_margin * 2);
            OriginalPicture.Location = new Point(_margin, _margin);
            OriginalPicture.Size = new Size(pboxW, pboxH);
            ThresholdedPicture.Location = new Point(OriginalPicture.Right + _margin, _margin);
            ThresholdedPicture.Size = new Size(pboxW, pboxH);
            HoughPicture.Location = new Point(ThresholdedPicture.Right + _margin, _margin);
            HoughPicture.Size = new Size(pboxW, pboxH);
            DetectedPicture.Location = new Point(HoughPicture.Right + _margin, _margin);
            DetectedPicture.Size = new Size(w - HoughPicture.Right - (2*_margin), pboxH);
            this.ResumeLayout();
        }
        //I believe if you set the InitialDirectory, the app will always start here,
        //but you may wish the OS to remember this location for the user, i think it is best
        //left blank.
        private void OriginalPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
  
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg|anything else (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OriginalImage = (Bitmap) Image.FromFile(openFileDialog1.FileName);
                OriginalPicture.Image = OriginalImage;
                ThresholdImage = (Bitmap) OriginalImage.Clone();
                ip.ThresholdPicture(ref OriginalImage, ref ThresholdImage);
                ThresholdedPicture.Image = ThresholdImage;
            }
        }

        private double getScratchThreshold()
        {            
            double threshold;
            threshold = (float)ScratchThresholdTracker.Value;
            threshold *= 441.67295593006 / 100.0; //No idea now where this constant came from.
            return threshold; // Might it be something to do with the dimensions of the slider itself on the UI?
        }

        private void ScratchThresholdTracker_Scroll(object sender, EventArgs e)
        {
            ip.ThresholdPicture(ref OriginalImage, ref ThresholdImage, getScratchThreshold());
            ThresholdedPicture.Refresh();
            ScratchThresholdLabel.Text = "Scratch Threshold " + ScratchThresholdTracker.Value + "%";
        }

        private void ComputeHoughTransform_Click(object sender, EventArgs e)
        {
            List<line> lines = new List<line>();
            Color c = Color.FromArgb(255, 255, 255, 0);

            HoughImage = ip.ForwardHoughTransform((Bitmap)ThresholdImage);
            HoughPicture.Image = HoughImage;

            DetectedImage = (Bitmap)OriginalImage.Clone();
            ip.InverseHoughTransform(lines);
            MessageBox.Show("Found " + lines.Count.ToString() + " scratches.");
            foreach (line l in lines)
                l.DrawLine(DetectedImage, c, getScratchThreshold() / 2);
            DetectedPicture.Image = DetectedImage;
        }
    }
}
