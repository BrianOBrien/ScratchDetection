using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Enums;
namespace ImageProcess
{
	public unsafe class IP
	{
		#region public feilds
		public Bitmap _bitmap	= null;		// the bitmap
		int width;							// bitmap width
		BitmapData bitmapData	= null;		// bitmap data
		Byte* pBase				= null;		// byte-sized memory pointer
		public int HistImax		= -1;
		public int HistRmax		= -1;
		public int HistGmax		= -1;
		public int HistBmax		= -1;
		public int HistMaxMax	= -1;
		public int[] arrHistI	= new int[256];
		public int[] arrHistR	= new int[256];
		public int[] arrHistG	= new int[256];
		public int[] arrHistB	= new int[256];
		public GraphicsPath pathHistI = new GraphicsPath();
		public GraphicsPath pathHistR = new GraphicsPath();
		public GraphicsPath pathHistG = new GraphicsPath();
		public GraphicsPath pathHistB = new GraphicsPath();
		#endregion

		public				IP(Bitmap bitmap)
		{
			_bitmap = bitmap;
		}

		public void			Dispose()
		{
			if (_bitmap != null)
			{
				_bitmap.Dispose();
			}
		}

		public Bitmap		Bitmap
		{
			get
			{
				return(_bitmap);
			}
		}
		public Point		PixelSize
		{
			get
			{
				GraphicsUnit unit = GraphicsUnit.Pixel;
				RectangleF bounds = _bitmap.GetBounds(ref unit);
				return new Point((int) bounds.Width, (int) bounds.Height);
			}
		}
		
		public void			LockBitmap()
		{
			GraphicsUnit unit = GraphicsUnit.Pixel;
			RectangleF boundsF = _bitmap.GetBounds(ref unit);
			Rectangle bounds = new Rectangle((int) boundsF.X,
				(int) boundsF.Y,
				(int) boundsF.Width,
				(int) boundsF.Height);

			// Figure out the number of bytes in a row
			// This is rounded up to be a multiple of 4
			// bytes, since a scan line in an image must always be a multiple of 4 bytes
			// in length. 
			width = (int) boundsF.Width * sizeof(PixelData);
			if (width % 4 != 0)
			{
				width = 4 * (width / 4 + 1);
			}

			bitmapData = 
				_bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			pBase = (Byte*) bitmapData.Scan0.ToPointer();
		}

		public void			UnlockBitmap()
		{
			_bitmap.UnlockBits(bitmapData);
			bitmapData = null;
			pBase = null;
		}

		public PixelData*	PixelAt(int x, int y)
		{
			return (PixelData*) (pBase + y * width + x * sizeof(PixelData));
		}
		public void			Save(string filename)
		{
			_bitmap.Save(filename, ImageFormat.Jpeg);
		}

		public void			testChannel(Ch chantype, int outWidth, int outHeight, out Bitmap ChBmpOut)
		{
			ChBmpOut = _bitmap;
			return;
			
		}
		public Bitmap		ChannelExp(Ch channel)
		{
			Bitmap bmpLocal = new Bitmap((Bitmap)_bitmap.Clone());
			Ch theseCh = Ch.Red | Ch.Green;
			Point size = PixelSize;
			LockBitmap();
			string currentmodes = theseCh.ToString();
			//			if ((0 <= channel) & (channel <= 6))
			//			{
			for (int y = 0; y < size.Y; y++)
			{
				PixelData* pPixel = PixelAt(0, y);
				for (int x = 0; x < size.X; x++)
				{
					byte value = (byte) (0);
					#region Channels
					//						if (channel.IndexOf("All") > -1)
					//						{
					//							value = (byte) ((pPixel->blue + pPixel->red + pPixel->green) / 3);
					//							pPixel->red =  value;
					//							pPixel->green = value;
					//							pPixel->blue = value;
					//						}
					if (currentmodes.IndexOf("Red") > -1)
					{
						value = (byte) (pPixel->red);
						pPixel->red =  value;
						//pPixel->green = 0;
						//pPixel->blue = 0;
					}
					if (currentmodes.IndexOf("Green") > -1)
					{
						value = (byte) (pPixel->green);
						//pPixel->red =  0;
						pPixel->green = value;
						//pPixel->blue = 0;
					}
					if (currentmodes.IndexOf("Blue") > -1)
					{
						value = (byte) (pPixel->blue);
						//pPixel->red =  0;
						//pPixel->green = 0;
						pPixel->blue = value;
					}
					//						else if (channel == 4)
					//						{
					//							value = (byte) (pPixel->red);
					//							pPixel->red =  value;
					//							pPixel->green = value;
					//							pPixel->blue = value;
					//						}
					//						else if (channel == 5)
					//						{
					//							value = (byte) (pPixel->green);
					//							pPixel->red =  value;
					//							pPixel->green = value;
					//							pPixel->blue = value;
					//						}
					//						else if (channel == 6)
					//						{
					//							value = (byte) (pPixel->blue);
					//							pPixel->red =  value;
					//							pPixel->green = value;
					//							pPixel->blue = value;
					//						}
					#endregion
					pPixel++;
				}
			}
			//			}
			UnlockBitmap();
			return _bitmap;
		}
		public void			HistogramBuild()
		{
			//			if (!pDib) return false;
			Point size = PixelSize;
			LockBitmap();
			arrHistR = new int[256];
			arrHistG = new int[256];
			arrHistB = new int[256];
			arrHistI = new int[256];
			float[] tempHistR = new float[256];
			float[] tempHistG = new float[256];
			float[] tempHistB = new float[256];
			float[] tempHistI = new float[256];
			HistImax = 0;
			HistRmax = 0;
			HistGmax = 0;
			HistBmax = 0;
			HistMaxMax = 0;
			uint value = (uint) (0);
			for (int y = 0; y < size.Y; y++)
			{
				PixelData* pPixel = PixelAt(0, y);
				for (int x = 0; x < size.X; x++)
				{
					// calculate separate color histograms - RGB
					value = (uint) (pPixel->red);
					arrHistR[value]++;
					value = (uint) (pPixel->green);
					arrHistG[value]++;
					value = (uint) (pPixel->blue);
					arrHistB[value]++;
					// calculate brightness (intensity) histogram - greyscale
					value = (uint) ((pPixel->red + pPixel->green + pPixel->blue) / 3);
					arrHistI[value]++;
					// calculate histogram maxiuma 
					if (value != 0)
					{
						if (arrHistR[value] > HistRmax)
							HistRmax = arrHistR[value];
						if (arrHistG[value] > HistGmax)
							HistGmax = arrHistG[value];
						if (arrHistB[value] > HistBmax)
							HistBmax = arrHistB[value];
						if (arrHistI[value] > HistImax)
							HistImax = arrHistI[value];
						if (HistImax > HistMaxMax)
							HistMaxMax = HistImax;
						if (HistRmax > HistMaxMax)
							HistMaxMax = HistRmax;
						if (HistGmax > HistMaxMax)
							HistMaxMax = HistGmax;
						if (HistBmax > HistMaxMax)
							HistMaxMax = HistBmax;

					}
					pPixel++;
				}
			}
			UnlockBitmap();
			float pixCount = size.X * size.Y;
			for (int y = 0; y < 256; y++)
			{
				// convert intensity counts to percent of pixels
				// = true histogram !!!
				tempHistI[y] = (float)arrHistI[y] / HistMaxMax;
				// each channel calculated as a third
				// (pixCount * 3f)
				tempHistR[y] = (float)arrHistR[y] / (HistMaxMax * 3f);
				tempHistG[y] = (float)arrHistG[y] / (HistMaxMax * 3f);
				tempHistB[y] = (float)arrHistB[y] / (HistMaxMax * 3f);
			}
		}

		public GraphicsPath	HistogramPlotI()
		{
			pathHistI.Reset();								// reset/prepare GraphicsPath
			PointF		ptfThis = new PointF(0f, 0f);		// initialize storage/write Points for subpaths
			PointF		ptfLast = new PointF(0f, 0f);
			PointF		ptfStart = new PointF(0f, 0f);
			ptfStart.X = ptfThis.X = -1f;					// set Point starting values
			ptfStart.Y = 257f;
			ptfThis.Y = 256f;
			pathHistI.AddLine(ptfStart, ptfThis);			// add first line (vertical, 1-pixel, left-edge)
			ptfStart = ptfLast = ptfThis;					// set Point starting values
			for (int x = 0; x < 256; x++)					// create intermediate subpaths
			{
				ptfThis.X = (float)x;
				ptfThis.Y = 256f - ((float)arrHistI[x] * 256f);
				if (false == (((ptfStart.Y == 0) & (ptfLast.Y == 0)) && (ptfThis.Y == 0)))
				{
					if (ptfStart.Y != ptfLast.Y)
					{
						pathHistI.AddLine(ptfStart, ptfLast);
						ptfStart = ptfThis;
					}
				}
				ptfLast = ptfThis;
			}
			ptfStart.Y		= 256f - ((float)arrHistI[254] * 256f);
			ptfStart.X		= 254f;
			ptfLast.Y		= 256f - ((float)arrHistI[255] * 256f);
			ptfLast.X		= 255f;
			pathHistI.AddLine(ptfStart, ptfLast);			// calc and add line 254-255
			ptfThis.X		= 256f;
			ptfThis.Y		= 256f;
			pathHistI.AddLine(ptfLast, ptfThis);			// calc and add line 256-"256"
			ptfStart.X		= 256f;
			ptfStart.Y		= 257f;
			pathHistI.AddLine(ptfThis, ptfStart);			// calc and add vertical line

			return pathHistI;
		}

		public GraphicsPath	HistogramPlotR()
		{
			pathHistR.Reset();								// reset/prepare GraphicsPath
			//			float maxhist = (float)HistRmax;
			PointF		ptfThis = new PointF(0f, 0f);		// initialize storage/write Points for subpaths
			PointF		ptfLast = new PointF(0f, 0f);
			PointF		ptfStart = new PointF(0f, 0f);
			ptfStart.X = ptfThis.X = -1f;					// set Point starting values
			ptfStart.Y = 257f;
			ptfThis.Y = 256f;
			pathHistR.AddLine(ptfStart, ptfThis);			// add first line (vertical, 1-pixel, left-edge)
			ptfStart = ptfLast = ptfThis;					// set Point starting values
			for (int x = 0; x < 256; x++)					// create intermediate subpaths
			{
				ptfThis.X = (float)x;
				ptfThis.Y = 256f - (((float)arrHistR[x] / 3) * 256f);
				if (false == (((ptfStart.Y == 0) & (ptfLast.Y == 0)) && (ptfThis.Y == 0)))
				{
					if (ptfStart.Y != ptfLast.Y)
					{
						pathHistR.AddLine(ptfStart, ptfLast);
						ptfStart = ptfThis;
					}
				}
				ptfLast = ptfThis;
			}
			ptfStart.Y		= 256f - (((float)arrHistR[254] / 3) * 256f);
			ptfStart.X		= 254f;
			ptfLast.Y		= 256f - (((float)arrHistR[255] / 3) * 256f);
			ptfLast.X		= 255f;
			pathHistR.AddLine(ptfStart, ptfLast);			// calc and add line 254-255
			ptfThis.X		= 256f;
			ptfThis.Y		= 256f;
			pathHistR.AddLine(ptfLast, ptfThis);			// calc and add line 256-"256"
			ptfStart.X		= 256f;
			ptfStart.Y		= 257f;
			pathHistR.AddLine(ptfThis, ptfStart);			// calc and add vertical line

			return pathHistR;

		}

		public GraphicsPath	HistogramPlotG()
		{
			pathHistG.Reset();								// reset/prepare GraphicsPath
			PointF		ptfThis = new PointF(0f, 0f);		// initialize storage/write Points for subpaths
			PointF		ptfLast = new PointF(0f, 0f);
			PointF		ptfStart = new PointF(0f, 0f);
			ptfStart.X = ptfThis.X = -1f;					// set Point starting values
			ptfStart.Y = 257f;
			ptfThis.Y = 256f;
			pathHistG.AddLine(ptfStart, ptfThis);			// add first line (vertical, 1-pixel, left-edge)
			ptfStart = ptfLast = ptfThis;					// set Point starting values
			for (int x = 0; x < 256; x++)					// create intermediate subpaths
			{
				ptfThis.X = (float)x;
				ptfThis.Y = 256f - ((float)arrHistG[x] * 256f);
				if (false == (((ptfStart.Y == 0) & (ptfLast.Y == 0)) && (ptfThis.Y == 0)))
				{
					if (ptfStart.Y != ptfLast.Y)
					{
						pathHistG.AddLine(ptfStart, ptfLast);
						ptfStart = ptfThis;
					}
				}
				ptfLast = ptfThis;
			}
			ptfStart.Y		= 256f - ((float)arrHistG[254] * 256f);
			ptfStart.X		= 254f;
			ptfLast.Y		= 256f - ((float)arrHistG[254] * 256f);
			ptfLast.X		= 255f;
			pathHistG.AddLine(ptfStart, ptfLast);			// calc and add line 254-255
			ptfThis.X		= 256f;
			ptfThis.Y		= 256f;
			pathHistG.AddLine(ptfLast, ptfThis);				// calc and add line 256-"256"
			ptfStart.X		= 256f;
			ptfStart.Y		= 257f;
			pathHistG.AddLine(ptfThis, ptfStart);			// calc and add vertical line

			return pathHistG;
		}

		public GraphicsPath	HistogramPlotB()
		{
			pathHistB.Reset();								// reset/prepare GraphicsPath
			PointF		ptfThis = new PointF(0f, 0f);		// initialize storage/write Points for subpaths
			PointF		ptfLast = new PointF(0f, 0f);
			PointF		ptfStart = new PointF(0f, 0f);
			ptfStart.X = ptfThis.X = -1f;					// set Point starting values
			ptfStart.Y = 257f;
			ptfThis.Y = 256f;
			pathHistB.AddLine(ptfStart, ptfThis);			// add first line (vertical, 1-pixel, left-edge)
			ptfStart = ptfLast = ptfThis;					// set Point starting values
			for (int x = 0; x < 256; x++)					// create intermediate subpaths
			{
				ptfThis.X = (float)x;
				ptfThis.Y = 256f - ((float)arrHistB[x] * 256f);
				if (false == (((ptfStart.Y == 0) & (ptfLast.Y == 0)) && (ptfThis.Y == 0)))
				{
					if (ptfStart.Y != ptfLast.Y)
					{
						pathHistB.AddLine(ptfStart, ptfLast);
						ptfStart = ptfThis;
					}
				}
				ptfLast = ptfThis;
			}
			ptfStart.Y		= 256f - ((float)arrHistB[254] * 256f);
			ptfStart.X		= 254f;
			ptfLast.Y		= 256f - ((float)arrHistB[255] * 256f);
			ptfLast.X		= 255f;
			pathHistB.AddLine(ptfStart, ptfLast);			// calc and add line 254-255
			ptfThis.X		= 256f;
			ptfThis.Y		= 256f;
			pathHistB.AddLine(ptfLast, ptfThis);			// calc and add line 256-"256"
			ptfStart.X		= 256f;
			ptfStart.Y		= 257f;
			pathHistB.AddLine(ptfThis, ptfStart);			// calc and add vertical line

			return pathHistB;
		}

		public void			GreyscaleMean(bool UseR, bool UseG, bool UseB)
		{
			Point size = PixelSize;
			LockBitmap();
			for (int y = 0; y < size.Y; y++)
			{
				PixelData* pPixel = PixelAt(0, y);
				for (int x = 0; x < size.X; x++)
				{
					byte value = (byte) (0);
					if (UseB == true)
						if (UseG == true)
							if (UseR == true)
								value = (byte) ((pPixel->blue + pPixel->red + pPixel->green) / 3);
							else
								value = (byte) ((pPixel->blue + pPixel->green) / 2);
						else
							if (UseR == true)
							value = (byte) ((pPixel->blue + pPixel->red) / 2);
						else
							value = (byte) ((pPixel->blue) / 2);
					else
						if (UseG == true)
						if (UseR == true)
							value = (byte) ((pPixel->red + pPixel->green) / 2);
						else
							value = (byte) ((pPixel->green) / 2);
					else
						if (UseR == true)
						value = (byte) ((pPixel->red) / 2);
					else
						value = (byte) (0);


					//					byte value = (byte) ((pPixel->red + pPixel->green + pPixel->blue) / 3);

					pPixel->red =  value;
					pPixel->green = value;
					pPixel->blue = value;
					pPixel++;
				}
			}
			UnlockBitmap();
		}

		public void			ThresholdFaster(int Max)
		{
			Point size = PixelSize;
			LockBitmap();
			for (int y = 0; y < size.Y; y++)
			{
				PixelData* pPixel = PixelAt(0, y);
				for (int x = 0; x < size.X; x++)
				{
				{
					if (pPixel->red > Max)
					{
						pPixel->red =  255;
						pPixel->green = 255;
						pPixel->blue = 255;
					}
					else
					{
						pPixel->red =  0;
						pPixel->green = 0;
						pPixel->blue = 0;
					}
				}
					pPixel++;
				}
			}
			UnlockBitmap();
		}
	
		public bool			HistogramEqualize()
		// HistogramEqualize function by <dave> : dave(at)posortho(dot)com
		{
			//if (!pDib) return false;
			Point size = PixelSize;
			LockBitmap();
			int[] map = new int[256];
			int[] equalize_map = new int[256];
			//int x, y, i, j;
			//RGBQUAD color;
			//RGBQUAD	yuvClr;
			//uint YVal, high, low;
			//memset( &histogram, 0, sizeof(int) * 256 );
			//memset( &map, 0, sizeof(int) * 256 );
			//memset( &equalize_map, 0, sizeof(int) * 256 );
			/*			
				// form histogram
				for(y=0; y < head.biHeight; y++)
				{
					info.nProgress = (long)(50*y/head.biHeight);
					if (info.nEscape) break;
					for(x=0; x < head.biWidth; x++)
					{
						color = GetPixelColor( x, y );
						YVal = (unsigned int)RGB2GRAY(color.rgbRed, color.rgbGreen, color.rgbBlue);
						histogram[YVal]++;
					}
				}
			*/
			/*
				// integrate the histogram to get the equalization map.
				j = 0;
				for(i=0; i <= 255; i++)
				{
					j += histogram[i];
					map[i] = j; 
				}

				// equalize
				low = map[0];
				high = map[255];
				if (low == high) return false;
				for( i = 0; i <= 255; i++ )
				{
					equalize_map[i] = (unsigned int)((((double)( map[i] - low ) ) * 255) / ( high - low ) );
				}

				// stretch the histogram
				if(head.biClrUsed == 0)
				{ // No Palette
					for( y = 0; y < head.biHeight; y++ )
					{
						info.nProgress = (long)(50+50*y/head.biHeight);
						if (info.nEscape) break;
						for( x = 0; x < head.biWidth; x++ )
						{

							color = GetPixelColor( x, y );
							yuvClr = RGBtoYUV(color);

							yuvClr.rgbRed = (BYTE)equalize_map[yuvClr.rgbRed];

							color = YUVtoRGB(yuvClr);
							SetPixelColor( x, y, color );
						}
					}
				} 
				else 
				{ // Palette
					for( i = 0; i < (int)head.biClrUsed; i++ )
					{

						color = GetPaletteColor((BYTE)i);
						yuvClr = RGBtoYUV(color);

						yuvClr.rgbRed = (BYTE)equalize_map[yuvClr.rgbRed];

						color = YUVtoRGB(yuvClr);
						SetPaletteColor( (BYTE)i, color );
					}
				}
				*/
			UnlockBitmap();
			return true;
		}

	}
	#region Unused
	//			public void			MakeGreyUnsafeFaster(bool UseR, bool UseG, bool UseB)
	//			{
	//				Point size = PixelSize;
	//				LockBitmap();
	//				for (int y = 0; y < size.Y; y++)
	//				{
	//					PixelData* pPixel = PixelAt(0, y);
	//					for (int x = 0; x < size.X; x++)
	//					{
	//						byte value = (byte) (0);
	//						if (UseB == true)
	//							if (UseG == true)
	//								if (UseR == true)
	//									value = (byte) ((pPixel->blue + pPixel->red + pPixel->green) / 3);
	//								else
	//									value = (byte) ((pPixel->blue + pPixel->green) / 2);
	//							else
	//								if (UseR == true)
	//								value = (byte) ((pPixel->blue + pPixel->red) / 2);
	//							else
	//								value = (byte) ((pPixel->blue) / 2);
	//						else
	//							if (UseG == true)
	//							if (UseR == true)
	//								value = (byte) ((pPixel->red + pPixel->green) / 2);
	//							else
	//								value = (byte) ((pPixel->green) / 2);
	//						else
	//							if (UseR == true)
	//							value = (byte) ((pPixel->red) / 2);
	//						else
	//							value = (byte) (0);
	//
	//
	//						//					byte value = (byte) ((pPixel->red + pPixel->green + pPixel->blue) / 3);
	//
	//						pPixel->red =  value;
	//						pPixel->green = value;
	//						pPixel->blue = value;
	//						pPixel++;
	//					}
	//				}
	//				UnlockBitmap();
	//			}
	//
	#endregion
}