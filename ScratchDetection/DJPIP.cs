using System;
using System.Collections.Generic;
using System.Text;

namespace ScratchDetection
{
    class DJPIP
    {
        //public Bitmap _bitmap = null;		// the bitmap
        //BitmapData bitmapData = null;		// bitmap data
        //int width;							// bitmap width
        //Byte* pBase = null;		// byte-sized memory pointer

        //public Bitmap Bitmap
        //{
        //    get
        //    {
        //        return (_bitmap);
        //    }
        //    set { _bitmap = value; }
        //}

        //public struct PixelData
        //{
        //    public byte blue;
        //    public byte green;
        //    public byte red;
        //}
        //public Point PixelSize
        //{
        //    get
        //    {
        //        GraphicsUnit unit = GraphicsUnit.Pixel;
        //        RectangleF bounds = _bitmap.GetBounds(ref unit);
        //        return new Point((int)bounds.Width, (int)bounds.Height);
        //    }
        //}

        //public void ThresholdFaster(int threshold)
        //{
        //    Point size = PixelSize;
        //    LockBitmap();
        //    for (int y = 0; y < size.Y; y++)
        //    {
        //        PixelData* pPixel = PixelAt(0, y);
        //        for (int x = 0; x < size.X; x++)
        //        {
        //            {
        //                if (pPixel->red > threshold)
        //                {
        //                    pPixel->red = 255;
        //                    pPixel->green = 255;
        //                    pPixel->blue = 255;
        //                }
        //                else
        //                {
        //                    pPixel->red = 0;
        //                    pPixel->green = 0;
        //                    pPixel->blue = 0;
        //                }
        //            }
        //            pPixel++;
        //        }
        //    }
        //    UnlockBitmap();
        //}
        //public void LockBitmap()
        //{
        //    GraphicsUnit unit = GraphicsUnit.Pixel;
        //    RectangleF boundsF = _bitmap.GetBounds(ref unit);
        //    Rectangle bounds = new Rectangle((int)boundsF.X,
        //        (int)boundsF.Y,
        //        (int)boundsF.Width,
        //        (int)boundsF.Height);

        //    // Figure out the number of bytes in a row
        //    // This is rounded up to be a multiple of 4
        //    // bytes, since a scan line in an image must always be a multiple of 4 bytes
        //    // in length. 
        //    width = (int)boundsF.Width * sizeof(PixelData);
        //    if (width % 4 != 0)
        //    {
        //        width = 4 * (width / 4 + 1);
        //    }

        //    bitmapData =
        //        _bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    pBase = (Byte*)bitmapData.Scan0.ToPointer();
        //}

        //public void UnlockBitmap()
        //{
        //    _bitmap.UnlockBits(bitmapData);
        //    bitmapData = null;
        //    pBase = null;
        //}
        //public PixelData* PixelAt(int x, int y)
        //{
        //    return (PixelData*)(pBase + y * width + x * sizeof(PixelData));
        //}
    }
    //private void DisplaySetTargetScaledProp()
    //{
    //    float dimProp = (float)_imageDimsInMM.X / (float)_imageDimsInMM.Y;
    //    float thisProp = (float)this.Width / (float)this.Height;
    //    if (dimProp == thisProp)
    //    {
    //        _rectTarget.Width = this.Width;
    //        _rectTarget.Height = this.Height;
    //    }
    //    else if (dimProp > thisProp)
    //    {
    //        _rectTarget.Width = this.Width;
    //        _rectTarget.Height = (int)((float)this.Height * thisProp / dimProp);
    //    }
    //    else //  if (dimProp < thisProp)
    //    {
    //        _rectTarget.Width = (int)((float)this.Width * dimProp / thisProp);
    //        _rectTarget.Height = this.Height;
    //    }
    //    _rectTarget.Location = new Point((this.Width - _rectTarget.Width) / 2, (this.Height - _rectTarget.Height) / 2);
    //    // Set flag for repainting of background, since displayed
    //    //	image will not fill drawing area
    //    _doPaintBkgnd = true;
    //    // Set flag for SizingType
    //    _lastImageSizingType = "fit";
    //}

}
