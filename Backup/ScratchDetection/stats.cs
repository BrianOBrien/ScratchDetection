using System;
using System.Collections.Generic;
using System.Text;

namespace ScratchDetection
{
    class stats
    {
        private double Sigma;				// The sum of all the samples added.
        private double SigmaSquares;		// The sum of all the samples squared then added.
        private long M;						// The number of samples.

        public double Average()
        {
            return (Sigma / (double)M);
        }

        public double StandardDeviation()
        {
            return (Math.Sqrt(GlobalVariance()));
        }

        public double GlobalVariance()
        {
            return ((SigmaSquares / M) - Math.Pow(Sigma / M, 2));
        }

        public void Reset()
        {
            Sigma = 0.0;
            SigmaSquares = 0.0;
            M = 0;
        }
        //
        // default constructor.
        //
        public stats()
        {
            Reset();
        }
        //
        // Constructor.
        //
        public stats(double sample)
        {
            Sigma = sample;
            SigmaSquares = sample * sample;
            M = 1;
        }
        //
        // This is intended to work when adding a single
        // sample.
        //
        public void addSample(double sample)
        {
            Sigma += sample;
            SigmaSquares += sample * sample;
            M = M + 1;
        }
    }
}
