using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace ImageBase.ImageHash
{
    public class ImageDCTHashCalculator
    {
        private const int IMAGEMATRIXSIZE = 32;
        private const int RESULTMATRIXSIZE = 8;
        private double[,] cosMatrix;
        private double[,] transposeCosMatrix;

        public ImageDCTHashCalculator()
        {
            CalculateCosMatrices();
        }
        
        public void FillBitArrayWithHash(Stream imageStream,BitArray pHash)
        {
            var originalImage = new Bitmap(imageStream);
            var reducedImage = new Bitmap(originalImage, IMAGEMATRIXSIZE, IMAGEMATRIXSIZE);
            Bitmap greyImage = ConvertImageToGrey(reducedImage);
            double[,] imageMatrix = GetImageMatrix(greyImage);
            double[,] dctResultMatrix = CalculateDCT(imageMatrix);
            double averageValue = GetAverageValueSquareMatrix(dctResultMatrix);
            FillArrayWithMatrixHash(dctResultMatrix, averageValue,pHash);
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

        private double[,] CalculateDCT(double[,] imageMatrix)
        {
            double[,] cosMutlImageMatrix = SquareMatrixMultiply(cosMatrix, imageMatrix);
            double[,] transformResult = SquareMatrixMultiply(cosMutlImageMatrix, transposeCosMatrix);
            return transformResult;
        }      

        private double GetAverageValueSquareMatrix(double[,] matrix)
        {
            double sum = 0;
            for (int i = 0; i < RESULTMATRIXSIZE; i++)
            {
                for (int j = 0; j < RESULTMATRIXSIZE; j++)
                {
                    sum += matrix[i+1, j+1];
                }
            }
            double averageValue = sum / (RESULTMATRIXSIZE * RESULTMATRIXSIZE);
            return averageValue;
        }

        private void FillArrayWithMatrixHash(double[,] matrix, double averageValue,BitArray pHash)
        {
            for (int i = 0,k = 0; i < RESULTMATRIXSIZE; i++)
            {
                for (int j = 0; j < RESULTMATRIXSIZE; j++,k++)
                {
                    pHash[k] = (matrix[i+1, j+1] < averageValue);
                }
            }
        }

        private double[,] GetImageMatrix(Bitmap image)
        {
            double[,] imageMatrix = new double[IMAGEMATRIXSIZE, IMAGEMATRIXSIZE];
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
