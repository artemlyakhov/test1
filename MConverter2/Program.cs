using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace MConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Check2BmpFiles())
            {
                //Application.Run(new MainForm());

                int count = BmpPngConverter.Count();

                if (count > 0)
                {
                    int x;
                    int y;

                    int sqrt = (int)Math.Sqrt((double)count);
                    if ((double)sqrt == Math.Sqrt((double)count))
                    {
                        x = y = sqrt;
                    }
                    else if ((double)(count / 2) == ((double)count) / 2.0)
                    {
                        x = count / 2;
                        y = 2;
                    }
                    else
                    {
                        x = count;
                        y = 1;
                    }

                    BmpPngConverter.Convert(x, y);

                    
                }
                else
                {
                    MessageBox.Show("Nothing to convert");
                }


            }
        }

        static public bool Check2BmpFiles()
        {
            if (File.Exists("1.bmp") && File.Exists("1a.bmp"))
            {
                Bitmap temp = new Bitmap("1.bmp");
                BmpPngConverter.frameBmp = new Bitmap(temp.Width, temp.Height);
                temp.Dispose();

                BmpPngConverter.Convert1("1.bmp", "1a.bmp");
                BmpPngConverter.frameBmp.Save("1.png", System.Drawing.Imaging.ImageFormat.Png);

                File.Delete("1.bmp");
                File.Delete("1a.bmp");

                //MessageBox.Show("Done");

                return true;

            }
            else
                return false;
        }
    }
}