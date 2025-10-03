using System;
using TestUnitaire;

namespace Maths_Matrices.Tests
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 Zero => new Vector3(0f, 0f, 0f);
        public static Vector3 One => new Vector3(1f, 1f, 1f);
    }

    public class Transform
    {
        private Vector3 localPosition;
        private Vector3 localRotation;
        private Vector3 localScale;

        public Vector3 LocalPosition
        {
            get => localPosition;
            set
            {
                localPosition = value;
                UpdateLocalTranslationMatrix();
                UpdateDerivedMatrices();
            }
        }

        public Vector3 LocalRotation
        {
            get => localRotation;
            set
            {
                localRotation = value;
                UpdateLocalRotationMatrices();
                UpdateDerivedMatrices();
            }
        }

        public Vector3 LocalScale
        {
            get => localScale;
            set
            {
                localScale = value;
                UpdateLocalScaleMatrix();
                UpdateDerivedMatrices();
            }
        }

        public MatrixFloat LocalTranslationMatrix { get; private set; }
        public MatrixFloat LocalRotationXMatrix { get; private set; }
        public MatrixFloat LocalRotationYMatrix { get; private set; }
        public MatrixFloat LocalRotationZMatrix { get; private set; }
        public MatrixFloat LocalRotationMatrix { get; private set; }
        public MatrixFloat LocalScaleMatrix { get; private set; }
        public MatrixFloat LocalToWorldMatrix { get; private set; }
        public MatrixFloat WorldToLocalMatrix { get; private set; }

        public Transform()
        {
            localPosition = Vector3.Zero;
            localRotation = Vector3.Zero;
            localScale = Vector3.One;

            UpdateLocalTranslationMatrix();
            UpdateLocalRotationMatrices();
            UpdateLocalScaleMatrix();
            UpdateDerivedMatrices();
        }

        private static float DegreesToRadians(float degrees) => degrees * (float)Math.PI / 180f;

        private void UpdateLocalTranslationMatrix()
        {
            LocalTranslationMatrix = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, localPosition.x },
                { 0f, 1f, 0f, localPosition.y },
                { 0f, 0f, 1f, localPosition.z },
                { 0f, 0f, 0f, 1f },
            });
        }

        private void UpdateLocalRotationMatrices()
        {
            float radX = DegreesToRadians(localRotation.x);
            float radY = DegreesToRadians(localRotation.y);
            float radZ = DegreesToRadians(localRotation.z);

            float cosX = MathF.Cos(radX);
            float sinX = MathF.Sin(radX);
            float cosY = MathF.Cos(radY);
            float sinY = MathF.Sin(radY);
            float cosZ = MathF.Cos(radZ);
            float sinZ = MathF.Sin(radZ);

            LocalRotationXMatrix = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, cosX, -sinX, 0f },
                { 0f, sinX, cosX, 0f },
                { 0f, 0f, 0f, 1f },
            });

            LocalRotationYMatrix = new MatrixFloat(new[,]
            {
                { cosY, 0f, sinY, 0f },
                { 0f, 1f, 0f, 0f },
                { -sinY, 0f, cosY, 0f },
                { 0f, 0f, 0f, 1f },
            });

            LocalRotationZMatrix = new MatrixFloat(new[,]
            {
                { cosZ, -sinZ, 0f, 0f },
                { sinZ, cosZ, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            LocalRotationMatrix = MatrixFloat.Multiply(LocalRotationYMatrix, MatrixFloat.Multiply(LocalRotationXMatrix, LocalRotationZMatrix));
        }

        private void UpdateLocalScaleMatrix()
        {
            LocalScaleMatrix = new MatrixFloat(new[,]
            {
                { localScale.x, 0f, 0f, 0f },
                { 0f, localScale.y, 0f, 0f },
                { 0f, 0f, localScale.z, 0f },
                { 0f, 0f, 0f, 1f },
            });
        }

        private void UpdateDerivedMatrices()
        {
            MatrixFloat trs = MatrixFloat.Multiply(LocalTranslationMatrix, MatrixFloat.Multiply(LocalRotationMatrix, LocalScaleMatrix));
            LocalToWorldMatrix = trs;
            WorldToLocalMatrix = trs.InvertByRowReduction();
        }
    }
}