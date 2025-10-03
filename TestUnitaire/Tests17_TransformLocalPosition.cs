using System.Numerics;
using NUnit.Framework;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests17_TransformLocalPosition
    {
        [Test]
        public void TestDefaultValues()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //Default Position
            Assert.That(t.LocalPosition.x, Is.EqualTo(0f));
            Assert.That(t.LocalPosition.y, Is.EqualTo(0f));
            Assert.That(t.LocalPosition.z, Is.EqualTo(0f));

            //Default Translation Matrix
            Assert.That(t.LocalTranslationMatrix.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }));
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestTransformChangePosition()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //Translation
            t.LocalPosition = new Vector3(5f, 2f, 1f);
            Assert.That(t.LocalTranslationMatrix.ToArray2D(), Is.EqualTo(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 2f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            }));

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
    }
}