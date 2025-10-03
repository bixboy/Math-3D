using System;
using TestUnitaire;

namespace TestUnitaire
{
    public class MatrixInt
    {
        private int[,] data;

        public int NbLines { get; }
        public int NbColumns { get; }

        public MatrixInt(int nbLines, int nbColumns)
        {
            NbLines = nbLines;
            NbColumns = nbColumns;
            data = new int[nbLines, nbColumns];
        }

        public MatrixInt(int[,] array)
        {
            NbLines = array.GetLength(0);
            NbColumns = array.GetLength(1);
            
            data = new int[NbLines, NbColumns];
            
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    data[i, j] = array[i, j];
        }

        public MatrixInt(MatrixInt other)
        {
            NbLines = other.NbLines;
            NbColumns = other.NbColumns;
            
            data = new int[NbLines, NbColumns];
            
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    data[i, j] = other[i, j];
        }

        public int this[int line, int column]
        {
            get { return data[line, column]; }
            set { data[line, column] = value; }
        }

        public int[,] ToArray2D()
        {
            int[,] copy = new int[NbLines, NbColumns];

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                    copy[i, j] = data[i, j];   
            }
            
            return copy;
        }

        public static MatrixInt Identity(int size)
        {
            MatrixInt result = new MatrixInt(size, size);
            
            for (int i = 0; i < size; i++)
                result[i, i] = 1;
            
            return result;
        }

        public bool IsIdentity()
        {
            if (NbLines != NbColumns)
                return false;

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i == j && data[i, j] != 1) 
                        return false;
                    
                    if (i != j && data[i, j] != 0) 
                        return false;
                }
            }
            
            return true;
        }
        
        
        public void Multiply(int scalar)
        {
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                    data[i, j] *= scalar;   
            }
        }
        
        public static MatrixInt Multiply(MatrixInt m, int scalar)
        {
            MatrixInt result = new MatrixInt(m);
            result.Multiply(scalar);
            
            return result;
        }
        
        public static MatrixInt operator *(MatrixInt m, int scalar) => Multiply(m, scalar);
        public static MatrixInt operator *(int scalar, MatrixInt m) => Multiply(m, scalar);
        
        public static MatrixInt operator -(MatrixInt m)
        {
            MatrixInt result = new MatrixInt(m);
            
            for (int i = 0; i < result.NbLines; i++)
            {
                for (int j = 0; j < result.NbColumns; j++)
                    result[i, j] = -result[i, j];

            }
            
            return result;
        }
        
        
        public void Add(MatrixInt other)
        {
            if (NbLines != other.NbLines || NbColumns != other.NbColumns)
                throw new MatrixSumException("Matrices must have the same dimensions to be added.");

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                    data[i, j] += other[i, j];   
            }
        }
        
        public static MatrixInt Add(MatrixInt a, MatrixInt b)
        {
            if (a.NbLines != b.NbLines || a.NbColumns != b.NbColumns)
                throw new MatrixSumException("Matrices must have the same dimensions to be added.");

            MatrixInt result = new MatrixInt(a);
            result.Add(b);
            
            return result;
        }
        
        public static MatrixInt operator +(MatrixInt a, MatrixInt b) => Add(a, b);

        public static MatrixInt operator -(MatrixInt a, MatrixInt b)
        {
            if (a.NbLines != b.NbLines || a.NbColumns != b.NbColumns)
                throw new MatrixSumException("Matrices must have the same dimensions to be subtracted.");

            MatrixInt result = new MatrixInt(a);
            for (int i = 0; i < result.NbLines; i++)
            {
                for (int j = 0; j < result.NbColumns; j++)
                    result[i, j] -= b[i, j];
   
            }
            
            return result;
        }
        
        
        public MatrixInt Multiply(MatrixInt other)
        {
            return Multiply(this, other);
        }

        public static MatrixInt Multiply(MatrixInt a, MatrixInt b)
        {
            if (a.NbColumns != b.NbLines)
                throw new MatrixMultiplyException("Number of columns of the first matrix must equal the number of lines of the second matrix.");

            MatrixInt result = new MatrixInt(a.NbLines, b.NbColumns);

            for (int i = 0; i < result.NbLines; i++)
            {
                for (int j = 0; j < result.NbColumns; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < a.NbColumns; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static MatrixInt operator *(MatrixInt a, MatrixInt b) => Multiply(a, b);
        
        
        public MatrixInt Transpose()
        {
            return Transpose(this);
        }

        public static MatrixInt Transpose(MatrixInt m)
        {
            MatrixInt result = new MatrixInt(m.NbColumns, m.NbLines);
            for (int i = 0; i < m.NbLines; i++)
            {
                for (int j = 0; j < m.NbColumns; j++)
                {
                    result[j, i] = m[i, j];
                }
            }
            
            return result;
        }
        
        
        public static MatrixInt GenerateAugmentedMatrix(MatrixInt a, MatrixInt b)
        {
            if (a.NbLines != b.NbLines)
                throw new ArgumentException("Matrices must have the same number of rows.");

            MatrixInt result = new MatrixInt(a.NbLines, a.NbColumns + b.NbColumns);

            for (int i = 0; i < a.NbLines; i++)
            {
                for (int j = 0; j < a.NbColumns; j++)
                    result[i, j] = a[i, j];

                for (int j = 0; j < b.NbColumns; j++)
                    result[i, a.NbColumns + j] = b[i, j];
            }

            return result;
        }

        public (MatrixInt, MatrixInt) Split(int nbColumnsForM1)
        {
            int rows = NbLines;
            int cols = NbColumns;

            MatrixInt m1 = new MatrixInt(rows, nbColumnsForM1);
            MatrixInt m2 = new MatrixInt(rows, cols - nbColumnsForM1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < nbColumnsForM1; j++)
                    m1[i, j] = this[i, j];

                for (int j = nbColumnsForM1; j < cols; j++)
                    m2[i, j - nbColumnsForM1] = this[i, j];
            }

            return (m1, m2);
        }
    }
}