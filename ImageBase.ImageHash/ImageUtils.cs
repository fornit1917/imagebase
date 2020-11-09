using System;
using System.Drawing;
using System.IO;

namespace ImageBase.ImageHash
{
    internal class ImageUtils
    {
        private const int IMAGEMATRIXSIZE = 32;
        private const int RESULTMATRIXSIZE = 8;
        private double[,] cosMatrix;
        private double[,] transposeCosMatrix;

        public ImageUtils()
        {
            CalculateCosMatrices();
        }

        public int[,] CalculateHashMatrix(Stream imageStream)
        {
            var originalImage = new Bitmap(imageStream);
            Bitmap imageForDCT = ConvertImageToGrey(originalImage);
            double[,] DCTResultMatrix = DCT(imageForDCT);
            double[,] reducedMatrix = ReduceSquareMatrix(DCTResultMatrix);
            double averageValue = GetAverageValueSquareMatrix(reducedMatrix);
            int[,] hashMatrix = GetHashSquareMatrix(reducedMatrix, averageValue);
            return hashMatrix;
        }

        private void CalculateCosMatrices()
        {
            cosMatrix = new double[IMAGEMATRIXSIZE, IMAGEMATRIXSIZE];
            for (int k = 0; k < IMAGEMATRIXSIZE; k++)
            {
                for (int n = 0; n < IMAGEMATRIXSIZE; n++)
                {
                    double cosValue = Math.Cos(((2 * n + 1) * k * Math.PI) / (2 * IMAGEMATRIXSIZE));
                    cosMatrix[k, n] = cosValue;
                }
            }
            transposeCosMatrix = SquareMatrixTranspose(cosMatrix);
        }

        private Bitmap ConvertImageToGrey(Bitmap originalImage)
        {
            originalImage = new Bitmap(originalImage, IMAGEMATRIXSIZE, IMAGEMATRIXSIZE);
            var newImage = new Bitmap(IMAGEMATRIXSIZE, IMAGEMATRIXSIZE);
            for (int i = 0; i < IMAGEMATRIXSIZE; i++)
            {
                for (int j = 0; j < IMAGEMATRIXSIZE; j++)
                {
                    var origColor = originalImage.GetPixel(i, j);
                    var newColorValue = ((int)origColor.R + (int)origColor.G + (int)origColor.B) / 3;
                    var newColor = Color.FromArgb(newColorValue, newColorValue, newColorValue);
                    newImage.SetPixel(i, j, newColor);
                }
            }
            return newImage;
        }

        private double[,] DCT(Bitmap image)
        {
            double[,] imageMatrix = GetImageMatrix(image);
            double[,] cosMutlImageMatrix = SquareMatrixMultiply(cosMatrix, imageMatrix);
            double[,] transformResult = SquareMatrixMultiply(cosMutlImageMatrix, transposeCosMatrix);
            return transformResult;
        }

        private double[,] ReduceSquareMatrix(double[,] initialMatrix)
        {
            double[,] reducedMatrix = new double[RESULTMATRIXSIZE, RESULTMATRIXSIZE];
            for (int i = 0; i < RESULTMATRIXSIZE; i++)
            {
                for (int j = 0; j < RESULTMATRIXSIZE; j++)
                {
                    reducedMatrix[i, j] = initialMatrix[i + 1, j + 1];
                }
            }
            return reducedMatrix;
        }

        private double GetAverageValueSquareMatrix(double[,] matrix)
        {
            double sum = 0;
            for (int i = 0; i < RESULTMATRIXSIZE; i++)
            {
                for (int j = 0; j < RESULTMATRIXSIZE; j++)
                {
                    sum += matrix[i, j];
                }
            }
            double averageValue = sum / (RESULTMATRIXSIZE * RESULTMATRIXSIZE);
            return averageValue;
        }

        private int[,] GetHashSquareMatrix(double[,] matrix, double averageValue)
        {
            int[,] hashMatrix = new int[RESULTMATRIXSIZE, RESULTMATRIXSIZE];
            for (int i = 0; i < RESULTMATRIXSIZE; i++)
            {
                for (int j = 0; j < RESULTMATRIXSIZE; j++)
                {
                    hashMatrix[i, j] = Convert.ToInt32(matrix[i, j] < averageValue);
                }
            }
            return hashMatrix;
        }

        private double[,] GetImageMatrix(Bitmap image)
        {
            double[,] imageMatrix = new double[image.Height, image.Width];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    imageMatrix[i, j] = image.GetPixel(j, i).R;
                }
            }
            return imageMatrix;
        }

        private double[,] SquareMatrixMultiply(double[,] firstMatrix, double[,] secondMatrix)
        {
            double[,] resultMatrix = new double[IMAGEMATRIXSIZE, IMAGEMATRIXSIZE];
            for (int i = 0; i < IMAGEMATRIXSIZE; i++)
            {
                for (int j = 0; j < IMAGEMATRIXSIZE; j++)
                {
                    for (int k = 0; k < IMAGEMATRIXSIZE; k++)
                    {
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }
            return resultMatrix;
        }

        private double[,] SquareMatrixTranspose(double[,] initialMatrix)
        {
            var resultMatrix = new double[IMAGEMATRIXSIZE, IMAGEMATRIXSIZE];
            for (int i = 0; i < IMAGEMATRIXSIZE; i++)
            {
                for (int j = 0; j < IMAGEMATRIXSIZE; j++)
                {
                    resultMatrix[j, i] = initialMatrix[i, j];
                }
            }
            return resultMatrix;
        }
    }
}
