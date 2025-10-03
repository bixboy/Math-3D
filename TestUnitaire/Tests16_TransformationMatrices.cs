using System;
using System.Numerics;
using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests16_TransformationMatrices
    {
        [Test]
        public void TestTranslatePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 0f, 0f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.That(vTransformed.X, Is.EqualTo(6f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Y, Is.EqualTo(3f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Z, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestTranslateDirection()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 0f, 0f, 0f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });
            
            Vector4 vTransformed = m * v;
            Assert.That(vTransformed.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Y, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Z, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(0f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestScalePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(2f, 1f, 3f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.5f, 0f, 0f, 0f },
                { 0.0f, 2f, 0f, 0f },
                { 0.0f, 0f, 3f, 0f },
                { 0.0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.That(vTransformed.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Y, Is.EqualTo(2f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Z, Is.EqualTo(9f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(2f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(3f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(2f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(3f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestRotatePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 4f, 7f, 1f);
            double a = Math.PI / 2d;
            float cosA = (float)Math.Cos(a);
            float sinA = (float)Math.Sin(a);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { cosA, -sinA, 0f, 0f },
                { sinA, cosA, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.That(vTransformed.X, Is.EqualTo(-4f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Y, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformed.Z, Is.EqualTo(7f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(4f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(7f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.That(vTransformedInverted.X, Is.EqualTo(1f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Y, Is.EqualTo(4f).Within(GlobalSettings.DefaultFloatingPointTolerance));
            Assert.That(vTransformedInverted.Z, Is.EqualTo(7f).Within(GlobalSettings.DefaultFloatingPointTolerance));

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}