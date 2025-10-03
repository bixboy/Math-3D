using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests13_Determinants
    {
        [Test]
        public void TestDeterminantMatrix2x2()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(determinant, Is.EqualTo(-2f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestDeterminantMatrix3x3()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(determinant, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestDeterminantMatrix4x4()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.707f, 2.449f, 4.243f, 1.000f },
                { 0.707f, 2.449f, -4.243f, 2.000f },
                { -1.732f, 2.000f, 0.000f, 3.000f },
                { 0.000f, 0.000f, 0.000f, 1.000f },
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.1d;
            Assert.That(determinant, Is.EqualTo(48f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestDeterminantIdentityMatrices()
        {
            //Identity 2
            MatrixFloat identity2 = MatrixFloat.Identity(2);
            float determinantIdentity2 = MatrixFloat.Determinant(identity2);
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(determinantIdentity2, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 3
            MatrixFloat identity3 = MatrixFloat.Identity(3);
            float determinantIdentity3 = MatrixFloat.Determinant(identity3);
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(determinantIdentity3, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 10
            MatrixFloat identity10 = MatrixFloat.Identity(10);
            float determinantIdentity10 = MatrixFloat.Determinant(identity10);
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(determinantIdentity10, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}