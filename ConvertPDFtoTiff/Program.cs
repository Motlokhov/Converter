using PdfiumViewer;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            var document = PdfDocument.Load(@"d:\table.pdf");
            var count = document.PageCount;
            Image[] images = new Image[count];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = document.Render(i, 300, 400, true);
            }


            //string path = "d:\\multipage.tif";

            ImageCodecInfo inf = GetEncoderInfo("image/tiff");
            Encoder saveFlag = Encoder.SaveFlag;
            var myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(saveFlag, (long)EncoderValue.MultiFrame);
            images[0].Save(@"d:\table.tiff", inf, myEncoderParameters);
            for (int i = 1; i < images.Length; i++)
            {
                myEncoderParameters.Param[0] = new EncoderParameter(saveFlag, (long)EncoderValue.FrameDimensionPage);
                Bitmap img = new Bitmap(images[i]);
                images[0].SaveAdd(img, myEncoderParameters);
            }

            myEncoderParameters.Param[0] = new EncoderParameter(saveFlag, (long)EncoderValue.Flush);

        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
