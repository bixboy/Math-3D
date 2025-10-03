using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests07_TransposeMatrices
    {
        [Test]
        public void TestTransposeMatrixInstance()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            MatrixInt m1T = m1.Transpose();

            Assert.That(m1T.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 4 },
                { 2, 5 },
                { 3, 6 }
            }));
        }
        
        [Test]
        public void TestTransposeMatrixStatic()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            MatrixInt m1T = MatrixInt.Transpose(m1);

            Assert.That(m1T.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1, 4 },
                { 2, 5 },
                { 3, 6 }
            }));
        }
    }
}