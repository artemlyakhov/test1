using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MConverter
{
    class BmpPngConverter
    {
        public static int Count()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "xa*.bmp");

            return files.Length;
        }


        public static void Convert(int x, int y)
        {
            string mask = "xa*.bmp";
            string path = Directory.GetCurrentDirectory();

            string[] files = Directory.GetFiles(path, mask);

            Bitmap tempBmp = new Bitmap(files[0]);
            frameBmp = new Bitmap(tempBmp.Width, tempBmp.Height);

            Bitmap finalBmp = new Bitmap(x * tempBmp.Width, y * tempBmp.Height);
            tempBmp.Dispose();

            Graphics g = Graphics.FromImage(finalBmp);

            int index = 0;
            for(int j=0; j<y; j++)
                for (int i = 0; i < x; i++)
                {
                    if (index < files.Length)
                    {
                        frameBmp = new Bitmap(frameBmp.Width, frameBmp.Height);

                        //string xfilename = Path.GetFileName(files[index]);
                        //string afile = path + "\\a" + xfilename.Substring(1);

                        string afile = files[index];
                        string xfile = path + "\\" + Path.GetFileName(afile).Replace("xa", "x");

                        Convert1(xfile, afile);

                        File.Delete(xfile);
                        File.Delete(afile);

                        g.DrawImage(frameBmp, new Rectangle(i * frameBmp.Width, j * frameBmp.Height, frameBmp.Width, frameBmp.Height),
                            new Rectangle(0, 0, frameBmp.Width, frameBmp.Height), GraphicsUnit.Pixel);

                        index++;
                    }
                }

            finalBmp.Save(path + "\\x.png", ImageFormat.Png);        
            
        }
        public static Bitmap frameBmp;

        public static void Convert1(string xfile, string afile)
        {
            Bitmap xbmp = new Bitmap(xfile);
            Bitmap abmp = new Bitmap(afile);

            for(int j=0; j<xbmp.Height; j++)
                for (int i = 0; i < xbmp.Width; i++)
                {
                    Color c = xbmp.GetPixel(i, j);
                    frameBmp.SetPixel(i, j, Color.FromArgb(255 - abmp.GetPixel(i, j).R, c.R, c.G, c.B));
                }

            xbmp.Dispose();
            abmp.Dispose();

        }



        
    }
}
