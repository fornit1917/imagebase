using System;
using System.Collections;
using System.IO;

namespace ImageBase.ImageHash
{
    public class ImageHashService : IImageHashService
    {
        private const int HASHMATRIXSIZE = 8;
        private ImageDCTHashCalculator _imageHandler;

        public ImageHashService()
        {
            _imageHandler = new ImageDCTHashCalculator();
        }

        public long CalculateImageHash(Stream imageStream)
        {
            long pHash;
            var hashArray = new BitArray(64);
            _imageHandler.FillBitArrayWithHash(imageStream, hashArray);

            string hashString = "";
            for (int i = 0; i < 64; i++)
            {
                hashString += Convert.ToInt32(hashArray[i]).ToString();
            }
            pHash = Convert.ToInt64(hashString, 2);
            return pHash;
        }

        public void CalculateImageHash(Stream imageStream, BitArray pHash)
        {
            _imageHandler.FillBitArrayWithHash(imageStream,pHash);
        }
    }
}
