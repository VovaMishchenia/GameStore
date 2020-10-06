using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GameStoreClient.Helpers
{
    public class CustomImageConvertor
    {
        public static Bitmap ConvertToBitmap(Bitmap source, int width, int height)
        {
            Bitmap picture = new Bitmap(width, height);
            using (Graphics context = Graphics.FromImage(picture))
            {
                context.DrawImage(source, 0, 0, width, height);
            }
                return new Bitmap(picture);
        }
        
    }
}