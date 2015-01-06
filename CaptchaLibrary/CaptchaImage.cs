using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CaptchaLibrary
{
    public class CaptchaImage
    {
        private string text;
        private int width;
        private int height;
        public Bitmap Image { get; set; }

        public CaptchaImage(string s, int width, int height)
        {
            text = s;
            this.width = width;
            this.height = height;
            GenerateImage();
        }
        
        private void GenerateImage()
        {
            int backCount = 7;
            int textCount = 7;
            Brush[] forBackground = new Brush[]
            {
                Brushes.AliceBlue,
                Brushes.Blue,
                Brushes.Brown,
                Brushes.Chocolate,
                Brushes.Coral,
                Brushes.Cyan,
                Brushes.DarkBlue
            };
            Brush[] forText = new Brush[]
            {
                Brushes.DarkGoldenrod,
                Brushes.DarkGreen,
                Brushes.Linen,
                Brushes.Orange,
                Brushes.LimeGreen,
                Brushes.Magenta,
                Brushes.LightYellow
            };

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            
            Graphics g = Graphics.FromImage(bitmap);
            Random rand = new Random();
            Brush background = forBackground[rand.Next(backCount)];
            Brush textColor = forText[rand.Next(textCount)];

            g.FillRectangle(background, 0, 0, width, height);
            g.DrawString(text, new Font("Arial", height / 2, FontStyle.Italic),
                                textColor, new RectangleF(0, 0, width, height));
            for (int i = 0; i != 10; i++)
                g.DrawLine(new Pen(textColor), rand.Next(width), rand.Next(height), rand.Next(width), rand.Next(height));

            g.Dispose();

            Image = bitmap;
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Image.Dispose();
        }
    }
}
