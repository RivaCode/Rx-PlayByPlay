using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Svg;

namespace RxExamples.Pages
{
    class SvgConverter : IValueConverter
    {
        private Dictionary<string, BitmapImage> _svgCache = new Dictionary<string, BitmapImage>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var svgFile = value as string;
            if (svgFile == null)
            {
                return null;
            }
            if (_svgCache.ContainsKey(svgFile))
            {
                return _svgCache[svgFile];
            }
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Assests", svgFile);
                var byteArray = File.ReadAllBytes(path);
                using (var stream = new MemoryStream(byteArray))
                {
                    var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                    var bitmap = svgDocument.Draw();

                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Bmp);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.DecodePixelWidth = 35;
                    image.DecodePixelHeight = 25;
                    ms.Seek(0, SeekOrigin.Begin);
                    image.StreamSource = ms;
                    image.EndInit();
                    _svgCache[svgFile] = image;
                    return image;
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class NoCapitalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var capital = value as string;
            return string.IsNullOrEmpty(capital) ? "N/A" : capital;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
