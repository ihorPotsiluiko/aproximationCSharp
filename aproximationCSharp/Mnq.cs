using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproximationCSharp
{
    /// <summary>
    /// class finds polynomial constants using Gauss method, takes points x, y as an input
    /// </summary>
    class Mnq
    {
        //  input points values x, y
        private float[] _xValues;
        private float[] _yValues;

        //number of points entered
        private int _numPoints;

        // polynomial constants for output eq.: a1 + a2*x + a3*x^2 + ...
        private float[] _a;

        // polinomial power for MNQ method
        private int _polPower;

        // rigth side of the matrix for Gauss method
        private float[,] _xSums;

        // left side of the matrix for Gauss method
        private float[] _b;

        // init for given points and runs calculation
        public Mnq(float[] xValues, float[] yValues)
        {
            _numPoints = xValues.Length;
            _polPower = 3;
            _xValues = new float[_numPoints];
            _yValues = new float[_numPoints];
            xValues.CopyTo(_xValues, 0);
            yValues.CopyTo(_yValues, 0);
            _xSums = new float[_polPower + 1, _polPower + 1];
            _a = new float[_polPower + 1];
            _b = new float[_polPower + 1];

            for (int i = 0; i < _polPower + 1; i++)
            {
                _a[i] = 0;
                _b[i] = 0;
                for (int j = 0; j < _polPower + 1; j++)
                {
                    _xSums[i, j] = 0;
                }
            }

            RunMNQ();
        }

        // create Gauss matrix
        private void InitialiseGaussMatrix()
        {
            for (int i = 0; i < _polPower + 1; i++)
            {
                for (int j = 0; j < _polPower + 1; j++)
                {
                    for (int k = 0; k < _numPoints; k++)
                    {
                        _xSums[i, j] += (float)Math.Pow(_xValues[k], i + j);
                    }

                }
            }
            for (int i = 0; i < _polPower + 1; i++)
            {
                for (int k = 0; k < _numPoints; k++)
                {
                    _b[i] += (float)Math.Pow(_xValues[k], i) * _yValues[k];
                }
            }
        }

        // exchange rows if zero on diagonal
        private void MakeDiagonalNotzeros()
        {
            for (int i = 0; i < _polPower + 1; i++)
            {
                if (_xSums[i, i] == 0)
                {
                    for (int j = 0; j < _polPower + 1; j++)
                    {
                        if (i == j) continue;
                        if (_xSums[j, i] != 0 && _xSums[i, j] != 0)
                        {
                            // exchange rows in Gauss matrix
                            float[] temp = new float[_polPower + 1];
                            for (int k = 0; k < _polPower + 1; k++)
                            {
                                temp[k] = _xSums[j, k];
                                _xSums[j, k] = _xSums[i, k];
                                _xSums[i, k] = temp[k];
                            }

                            float tempb = _b[j];
                            _b[j] = _b[i];
                            _b[i] = tempb;
                            break;
                        }
                    }
                }
            }
        }

        // transform matrix to diagonal view
        private bool TransformMatrix()
        {
            for (int i = 0; i < _polPower + 1; i++)
            {
                for (int j = i + 1; j < _polPower + 1; j++)
                {
                    if (_xSums[i, i] == 0) return false;
                    float coef = _xSums[j, i] / _xSums[i, i];

                    for (int k = i; k < _polPower + 1; k++)
                    {
                        _xSums[j, k] -= coef * _xSums[i, k];
                    }
                    _b[j] -= coef * _b[i];
                }
            }
            return true;
        }

        // calculate output constants a1, a2, ... from diagonal matrix
        private void CalculateConstants()
        {
            for (int i = _polPower; i > -1; i--)
            {
                float s = 0;
                for (int j = i; j < _polPower + 1; j++)
                {
                    s += _xSums[i, j] * _a[j];
                }
                _a[i] = (_b[i] - s) / _xSums[i, i];
            }
        }

        // run MNQ method
        private void RunMNQ()
        {
            InitialiseGaussMatrix();
            MakeDiagonalNotzeros();
            if (TransformMatrix() == false) return; // polinom does not exist
            CalculateConstants();
        }

        // calculate aproximated value for x
        public float MnqAproxFunction(float x)
        {
            float y = 0;
            for (int i = 0; i < _polPower + 1; i++)
            {
                y += (float)(_a[i] * Math.Pow(x, i));
            }
            return y;
        }
    }
}
