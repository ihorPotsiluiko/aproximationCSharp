using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproximationCSharp
{
    class LaGrng
    {
        //  input points values x, y
        private float[] _xValues;
        private float[] _yValues;

        //number of points entered
        private int _numPoints;

        // init for given points
        public LaGrng(float[] xValues, float[] yValues)
        {
            _numPoints = xValues.Length;
            _xValues = new float[_numPoints];
            _yValues = new float[_numPoints];
            xValues.CopyTo(_xValues, 0);
            yValues.CopyTo(_yValues, 0);
            //Console.WriteLine(string.Join(" ", _xValues) + "\n" + string.Join(" ", _yValues));
        }

        //  finds aproximated value for x using LaGrange formula
        public float LagrngAproxFunction(float x)
        {
            float aprox_value = 0;

            for (int i = 0; i < _numPoints; i++)
            {
                float basic_polinom = 1;
                for (int j = 0; j < _numPoints; j++)
                {
                    if (i != j)
                    {
                        basic_polinom *= (x - _xValues[j]) / (_xValues[i] - _xValues[j]);
                    }
                }
                aprox_value += basic_polinom * _yValues[i];
            }
            return aprox_value;
        }
    }
}
