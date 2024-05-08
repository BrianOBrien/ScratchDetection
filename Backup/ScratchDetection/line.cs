using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScratchDetection
{
    class line
    {
        #region private properties
        private int m_x1;
        private int m_y1;
        private int m_x2;
        private int m_y2;
        private double m_m;
        private double m_b;
        private int m_dx;
        private int m_dy;
        #endregion

        #region private methods
        private double saturation(Color c)
        {
            return (Math.Sqrt((double)c.R * (double)c.R + (double)c.G * (double)c.G + (double)c.B * (double)c.B));
        }
        private int round(double v)
        {
            if (v < 0.0)
                return (int)(v - 0.5);
            return (int)(v + 0.5);
        }
        private int Y(int x)
        {
            if (m_dx != 0)
		        return(round(m_m * x + m_b));
            return(0);

        }
        private int X(int y)
        {
        	if (m_dx != 0)
		        return(round(((double)y - m_b) / m_m));
	        return m_x1;
        }
        private void PutPixel(Bitmap b, Color c, int x, int y, double saturatioThreshold)
        {
            Color pc = b.GetPixel(x, y);
            double sat = saturation(pc);
            if (sat >= saturatioThreshold)
            {
                b.SetPixel(x,y,c);
            }
        }
        #endregion

        #region public methods
        public line(int x1, int y1, int x2, int y2)
        {
            m_x1 = x1;
            m_y1 = y1;
            m_x2 = x2;
            m_y2 = y2;
            
            m_dx = x2 - x1;
            m_dy = y2 - y1;

            if (m_dx != 0)
            {
                m_m = (double)m_dy / (double)m_dx;
                m_b = (double)m_y1 - m_m * (double)m_x1;
            }
        }
        public void DrawLine(Bitmap b, Color c, double saturationThreshold)
        {
	        int x, y;

	        int x1 = Math.Min(m_x1, m_x2);
	        int x2 = Math.Max(m_x1, m_x2);
	        int y1 = Math.Min(m_y1, m_y2);
	        int y2 = Math.Max(m_y1, m_y2);

            int m_dx = x2 - x1;
	        int m_dy = y2 - y1;

	        // if not horizontal or vertical
	        if ((m_dx != 0) && (m_dy != 0))
	        {
		        for (x=x1; x < x2; ++x)
		        {
			        y=Y(x);
                    //b.SetPixel(x, y, c);
                    PutPixel(b, c, x, y, saturationThreshold);
		        }
		        for (y=y1; y < y2; ++y)
		        {
			        x=X(y);
                    //b.SetPixel(x, y, c);
                    PutPixel(b, c, x, y, saturationThreshold);
		        }
	        }
	        else if (m_dx == 0) // vertical;
	        {
		        for (y=y1; y < y2; ++y)
		        {
                    //b.SetPixel(m_x1, y, c);
                    PutPixel(b, c, m_x1, y, saturationThreshold);
		        }
	        }
	        else if (m_dy == 0) // horizontal.
	        {
		        for (x=x1; x < x2; ++x)
		        {
                    //b.SetPixel(x, m_y1, c);
                    PutPixel(b, c, x, m_y1, saturationThreshold);
		        }
	        }
        }
        #endregion
   }
 }
