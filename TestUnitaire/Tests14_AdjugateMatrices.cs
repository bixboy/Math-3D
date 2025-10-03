using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests14_AdjugateMatrices
    {
        [Test]
        public void TestCalculateAdjugateMatrixInstance()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            MatrixFloat adjM = m.Adjugate();
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { 4f, -2f },
                { -3f, 1f },
            }));
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestCalculateAdjugateMatrixStatic()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 5f },
                { 2f, 1f, 6f },
                { 3f, 4f, 0f },
            });

            MatrixFloat adjM = MatrixFloat.Adjugate(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { -24f, 20f, -5f },
                { 18f, -15f, 4f },
                { 5f, -4f, 1f },
            }));
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestCalculateAdjugateMatrixIdentity4x4()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            MatrixFloat adjM = m.Adjugate();
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Assert.That(adjM.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }));
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}