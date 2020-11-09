using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageBase.ImageHash
{
    interface IImageHashService
    {
        long CalculateImageHash(Stream imageStream);

        void CalculateImageHash(Stream imageStream, BitArray pHash);
    }
}
