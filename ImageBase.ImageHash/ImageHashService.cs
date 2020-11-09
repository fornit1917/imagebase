using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageBase.ImageHash
{
    public class ImageHashService : IImageHashService
    {
        private const int HASHMATRIXSIZE = 8;
        private ImageUtils imageHandler;
        public ImageHashService()
        {
            imageHandler = new ImageUtils();
        }
        public long CalculateImageHash(Stream imageStream)
        {
            long pHash;

            int[,] hashMatrix = imageHandler.CalculateHashMatrix(imageStream);

            string hashString = "";
            for (int i = 0; i < HASHMATRIXSIZE; i++)
            {
                for (int j = 0; j < HASHMATRIXSIZE; j++)
                {
                    hashString += hashMatrix[i, j].ToString();
                }
            }
            pHash = Convert.ToInt64(hashString, 2);
            return pHash;
        }

        public void CalculateImageHash(Stream imageStream, BitArray pHash)
        {
            int[,] hashMatrix = imageHandler.CalculateHashMatrix(imageStream);
            for (int i = 0, k = 0; i < HASHMATRIXSIZE; i++)
            {
                for (int j = 0; j < HASHMATRIXSIZE; j++, k++)
                {
                    pHash[k] = Convert.ToBoolean(hashMatrix[i, j]);
                }
            }
        }
    }
}
