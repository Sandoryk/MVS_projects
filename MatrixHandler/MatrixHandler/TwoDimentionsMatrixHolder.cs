using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixHandler
{
    [Serializable]
    public class TwoDimentionsMatrixHolder
    {
        uint _rows;
        uint _columns;
        double[,] matrix;
        
        public TwoDimentionsMatrixHolder(uint rows,uint columns)
        {
            _rows = rows;
            _columns = columns;
            matrix = new double[rows, columns];
        }
        public TwoDimentionsMatrixHolder(double[,] inMatrix)
        {
            if (inMatrix!=null)
	        {
		        _rows = (uint)inMatrix.GetLongLength(0);
                _columns = (uint)inMatrix.GetLongLength(1);
                matrix = new double[_rows, _columns];
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        matrix[i, j] = inMatrix[i, j];
                    }
                }
	        } 
        }
        public uint GetRowsCount
        {
            get { return _rows; }
        }
        public uint GetColumnsCount
        {
            get { return _columns; }
        }

        /// <summary>
        /// <br>Exceptions:</br>
        /// <br>    NoSuchMemberException</br>
        /// </summary>
        public double GetMember(uint rowIndex,uint columnIndex)
        {

            if(rowIndex<=_rows && columnIndex<=_columns)
            {
                return matrix[rowIndex, columnIndex];
            }
            else
            {
                throw new NoSuchMemberException();
                return 0;
            } 
        }

        public double[,] GetWholeMatrix()
        {
            return matrix;
        }

        public void SetMember(uint rowIndex, uint columnIndex,double value)
        {
            if (rowIndex <= _rows && columnIndex <= _columns)
            {
                matrix[rowIndex, columnIndex] = value;
            }
        }

        public static TwoDimentionsMatrixHolder operator + (TwoDimentionsMatrixHolder matr1,TwoDimentionsMatrixHolder matr2)
        {
            TwoDimentionsMatrixHolder resultMatr = null;
            bool testf= true;

            if (matr1.GetRowsCount != matr2.GetRowsCount || matr1.GetColumnsCount != matr2.GetColumnsCount)
                testf = false;
            if (matr1 == null || matr2 == null)
                testf = false;
            if (testf)
            {
                resultMatr = new TwoDimentionsMatrixHolder(matr1.GetRowsCount, matr1.GetColumnsCount);
                for (uint i = 0; i < matr1.GetRowsCount; i++)
                {
                    for (uint j = 0; j < matr1.GetColumnsCount; j++)
                    {
                        resultMatr.SetMember(i, j, matr1.GetMember(i, j) + matr2.GetMember(i, j));
                    }
                }
            }

            return resultMatr;
        }
        public static TwoDimentionsMatrixHolder operator * (TwoDimentionsMatrixHolder matr1, TwoDimentionsMatrixHolder matr2)
        {
            TwoDimentionsMatrixHolder resultMatr = null;
            bool testf = true;

            if (matr1.GetColumnsCount != matr2.GetRowsCount)
                testf = false;
            if (matr1 == null || matr2 == null)
                testf = false;
            if (testf)
            {
                resultMatr = new TwoDimentionsMatrixHolder(matr1.GetRowsCount, matr1.GetColumnsCount);
                for (uint i = 0; i < matr1.GetRowsCount; i++)
                {
                    for (uint j = 0; j < matr1.GetColumnsCount; j++)
                    {
                        resultMatr.SetMember(i, j, matr1.GetMember(i, j) * matr2.GetMember(j, i));
                    }
                }
            }

            return resultMatr;
        }
        public static TwoDimentionsMatrixHolder operator - (TwoDimentionsMatrixHolder matr1, TwoDimentionsMatrixHolder matr2)
        {
            TwoDimentionsMatrixHolder resultMatr = null;
            bool testf;
            testf = true;
            if (matr1.GetRowsCount != matr2.GetRowsCount || matr1.GetColumnsCount != matr2.GetColumnsCount)
                testf = false;
            if (matr1 == null || matr2 == null)
                testf = false;
            if (testf)
            {
                resultMatr = new TwoDimentionsMatrixHolder(matr1.GetRowsCount, matr1.GetColumnsCount);
                for (uint i = 0; i < matr1.GetRowsCount; i++)
                {
                    for (uint j = 0; j < matr1.GetColumnsCount; j++)
                    {
                        resultMatr.SetMember(i, j, matr1.GetMember(i, j) - matr2.GetMember(i, j));
                    }
                }
            }

            return resultMatr;
        }
        public TwoDimentionsMatrixHolder CloneMarix()
        {
            double[,] mtrxArr = this.GetWholeMatrix();
            TwoDimentionsMatrixHolder resMtrx = new TwoDimentionsMatrixHolder(mtrxArr);

            return resMtrx;
        }

        public string TwoDimentionsMatrixToString()
        {
            string res = "";

            if (matrix != null)
            {
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        res += matrix[i, j] + " ";
                    }
                    res += "\n";
                }
            }
            return res;
        }
    }
}
