using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageBase.ImageHash
{
    public class ImageHashService : IImageHashService
    {
        private const int HASHSIZE = 64;
        private ImageDCTHashCalculator _imageHandler;

        public ImageHashService(ImageDCTHashCalculator imageHandler)
        {
            _imageHandler = imageHandler;
        }

        public long CalculateImageHash(Stream imageStream)
        {
            long pHash;
            var hashArray = new BitArray(HASHSIZE);
            _imageHandler.FillBitArrayWithHash(imageStream, hashArray);

            StringBuilder stringHash = new StringBuilder(HASHSIZE);
            for (int i = 0; i < HASHSIZE; i++)
            {
                stringHash.Append(Convert.ToInt32(hashArray[i]));
            }
            pHash = Convert.ToInt64(stringHash.ToString(), 2);
            return pHash;
        }

        public void CalculateImageHash(Stream imageStream, BitArray pHash)
        {
            _imageHandler.FillBitArrayWithHash(imageStream, pHash);
        }
    }
}
