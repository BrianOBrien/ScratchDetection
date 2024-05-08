using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace ScratchDetection
{
    unsafe class ImageProcessing
    {
        #region Private Properties
        private double[,] A;  // The Hough Transform Accumulator
        #endregion
        #region Private Methods
        private double hypot(int a, int b)
        {
            return(Math.Sqrt((double) a * (double) a + (double) b * (double) b));
        }
        private double saturation(Color c)
        {
            return (Math.Sqrt((double)c.R * (double)c.R + (double)c.G * (double)c.G + (double)c.B * (double)c.B));
        }
        private int round(double v)
        {
            if (v < 0.0)
                return (int)(v - 0.5);
            else
                return (int)(v + 0.5);
        }
        private double GetMin()
        {
            int x, y;
            double min = A[0, 0];
            for (y = 0; y < A.GetLength(0); ++y)
                for (x = 0; x < A.GetLength(1); ++x)
                {
                    if (A[y, x] < min)
                        min = A[y,x];
                }
            return min;
        }
        private double GetMax()
        {
            int x, y;
            double max = A[0, 0];
            for (y = 0; y < A.GetLength(0); ++y)
                for (x = 0; x < A.GetLength(1); ++x)
                {
                    if (A[y, x] > max)
                        max = A[y, x];
                }
            return max;
        }
        #endregion
        #region Public Methods
        public void GetMeanStdDev(ref Bitmap Img, ref double mean, ref double stddev)
        {
            stats s = new stats();
            Color sc, dc;
            int x, y, width, height;
            double grey;

            height = Img.Height;
            width = Img.Width;

            for (y = 0; y < height; ++y)
            {
                for (x = 0; x < width; ++x)
                {
                    sc = Img.GetPixel(x, y);

                    grey = saturation(sc);
                    s.addSample(grey);
                }
            }
            mean = s.Average();
            stddev = s.StandardDeviation();
        }

        public void ThresholdPicture(ref Bitmap sourceImg, ref Bitmap destImg)
        {
            Color sc, dc;
            int x, y, width, height;
            double grey;
            double mean=0;
            double stddev=0;
            const double nstddevs = 3.0;

            GetMeanStdDev(ref sourceImg, ref mean, ref stddev);

            height = sourceImg.Height;
            width = sourceImg.Width;

            for (y = 0; y < height; ++y)
            {
                for (x = 0; x < width; ++x)
                {
                    sc = sourceImg.GetPixel(x, y);

                    grey = saturation(sc);

                    if ((grey >= mean + (stddev * nstddevs)) || (grey <= mean - (stddev * nstddevs)))
                        dc = Color.FromArgb(255, 255, 255, 255);
                    else
                        dc = Color.FromArgb(255, 0, 0, 0);
                    destImg.SetPixel(x, y, dc);
                }
            }
        }
        public void ThresholdPicture(ref Bitmap sourceImg, ref Bitmap destImg, double threshold)
        {
            Color sc, dc;
            int x, y, width, height;
            double grey;

            height = sourceImg.Height;
            width = sourceImg.Width;

            for (y = 0; y < height; ++y)
            {
                for (x = 0; x < width; ++x)
                {
                    sc = sourceImg.GetPixel(x, y);

                    grey = saturation(sc);

                    if (grey >= threshold)
                        dc = Color.FromArgb(255, 255, 255, 255);
                    else
                        dc = Color.FromArgb(255, 0, 0, 0);
                    destImg.SetPixel(x, y, dc);
                }
            }
        } 
        public Bitmap ForwardHoughTransform(Bitmap Img)
        {
            double weight;   //unsigned char weight;
            int x, y, i, j;
            double p, t;
            Color c;

            int HoughWidth = Img.Width;
            int HoughHeight = Img.Height;

            double ThetaInc = Math.PI / (double)HoughHeight;

            double maxR = hypot(HoughWidth, HoughHeight);
            double minR = -maxR;
            double rInc = (maxR - minR) / HoughWidth;

            //TRACE("maxR = %f\n", maxR);
            //TRACE("minR = %f\n", minR);
            //TRACE("wi   = %f\n", maxR-minR);
            //TRACE("rInc = %f\n", rInc);

            /*double[,]*/
            A = new double[HoughHeight, HoughWidth];
            double[] C = new double[HoughHeight];
            double[] S = new double[HoughHeight];

            // Set the accumulator to zero.
            for (y = 0; y < HoughHeight; ++y)
            {
                for (x = 0; x < HoughWidth; ++x)
                {
                    A[y, x] = 0;
                }
            }

            // Precompute cos and sin valus.
            for (i = 0; i < HoughHeight; ++i)
            {
                t = (double)i * ThetaInc;
                C[i] = Math.Cos(t);
                S[i] = Math.Sin(t);
            }

            // Hough Transform
            for (y = 0; y < HoughHeight; ++y)
            {
                for (x = 0; x < HoughWidth; ++x)
                {
                    c = Img.GetPixel(x, y);
                    //weight = saturation(c);  //Math.Sqrt(c.R * c.R + c.G * c.G + c.B * c.B); // Img.GetPixel(x, y); 
                    if (c.R > 0)
                    //if (weight > 0.0)
                    {
                        for (i = 0; i < HoughHeight; ++i)
                        {
                            p = (double)x * C[i] + (double)y * S[i];

                            p += maxR;
                            p = p / rInc;

                            j = round(p);
                            A[i, j] += 1; // (double)weight;
                        }
                    }
                }
            }

            // Build the sinogram image.
            Bitmap newImage = new Bitmap(Img);
            double min, max, v;
            Color dc;
            min = GetMin();
            max = GetMax();
            for (y=0; y < HoughHeight; ++y)
            {
            	for (x=0; x < HoughWidth; ++x)
            	{
                    v = ((A[y,x]-min)/(max-min)) * 255.0;
                    dc = Color.FromArgb(255, (int) v, (int) v, (int) v);
                    newImage.SetPixel(x, y, dc);
            	}
            }
            return newImage;

            
        }
        public void InverseHoughTransform(List<line> lines)
        {
            int x, y;
            Color c;
            double t, r, fx, fy, m, b;
            int x1, y1, x2, y2;
            x1 = y1 = x2 = y2 = 0;

            lines.Clear();
            int ImageHeight = A.GetLength(0);
            int ImageWidth = A.GetLength(1);

            double tScale = Math.PI / (double)ImageHeight;

            double rMax = hypot(ImageWidth, ImageHeight);
            double rScale = (2.0 * rMax) / (double)ImageWidth;

            double threshold = GetMax();
 
            for (y = 0; y < ImageHeight; ++y)
            {
                for (x = 0; x < ImageWidth; ++x)
                {
                    if (A[y, x] >= threshold)
                    {
                        t = y * tScale;

                        r = x;
                        r = (r - ImageWidth / 2) * rScale;

                        fx = r * Math.Cos(t);
                        fy = r * Math.Sin(t);

                        if (fy != 0)
                        {
                            m = -(fx / fy);
                            b = fy - (m * fx);

                            //*****get (x1, y1)*****
                            x1 = 0;
                            y1 = round(m * (double)x1 + b);
                            if (y1 > ImageHeight - 1)
                            {
                                y1 = ImageHeight - 1;
                                x1 = round(((double)y1 - b) / m);
                            }
                            else if (y1 < 0)
                            {
                                y1 = 0;
                                x1 = round(((double)y1 - b) / m);
                            }

                            //******get (x2, y2)*****
                            x2 = ImageWidth - 1;
                            y2 = round((double)x2 * m + b);
                            if (y2 > ImageHeight - 1)
                            {
                                y2 = ImageHeight - 1;
                                x2 = round(((double)y2 - b) / m);
                            }
                            else if (y2 < 0)
                            {
                                y2 = 0;
                                x2 = round(((double)y2 - b) / m);
                            }
                        }
                        else if (fy == 0)
                        {
                            x1 = round(fx);
                            y1 = 0;

                            x2 = round(fx);
                            y2 = ImageHeight - 1;
                        }
                        line l = new line(x1, y1, x2, y2);
                        lines.Add(l);
                    }
                }
            }
        }
        #endregion
    }
}
 