using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests19_TransformLocalScale
    {
        [Test]
        public void TestDefaultValues()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            //Default Scale
            Assert.That(t.LocalScale.x, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(t.LocalScale.y, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(t.LocalScale.z, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            
            //Default Matrix
            AssertMatrixAlmostEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestChangeScale()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            //Scale X
            t.LocalScale = new Vector3(2f, 1f, 1f);
            AssertMatrixAlmostEqual(new[,]
            {
                { 2f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            //Scale Y
            t.LocalScale = new Vector3(1f, 5f, 1f);
            AssertMatrixAlmostEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 5f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            //Scale Z
            t.LocalScale = new Vector3(1f, 1f, 23f);
            AssertMatrixAlmostEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 23f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        private static void AssertMatrixAlmostEqual(float[,] expected, float[,] actual)
        {
            Assert.That(actual.GetLength(0), Is.EqualTo(expected.GetLength(0)));
            Assert.That(actual.GetLength(1), Is.EqualTo(expected.GetLength(1)));

            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.That(actual[i, j], Is.EqualTo(expected[i, j]).Within(GlobalSettings.DefaultFloatingPointTolerance));
                }
            }
        }
    }
}