using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;

namespace CuteDev
{
    /// <summary>
    /// Dosya yöneticisi (volkansendag - 2014.10.17)
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Fiziksel dizine dosyayı kaydeder (volkansendag - 2014.10.17)
        /// </summary>
        public static bool SaveFile(byte[] file, string fileName, string directory, bool thumbSave)
        {
            DateTime dt = DateTime.Now;
            string filePath = String.Format("{0}\\{1}", directory, fileName);

            if (!Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            try
            {
                FileSaveToPath(filePath, file);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Dosyayı kaydeder. (volkansendag - 2014.03.03)
        /// </summary>
        public static void FileSaveToPath(string path, byte[] file)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(file, 0, file.Length);
            fs.Close();
        }

        /// <summary>
        /// Objeyi resim'e dönüştürür dizine dosyayı kaydeder. (volkansendag - 2014.03.03)
        /// </summary>
        public static Image ObjToImg(byte[] file)
        {
            if (file == null)
                return null;
            else
            {
                Image returnImage;
                using (var ms = new MemoryStream(file, 0, file.Length))
                {
                    try
                    {
                        returnImage = Image.FromStream(ms);
                    }
                    catch (Exception)
                    {
                        returnImage = null;
                    }
                }
                return returnImage;
            }
        }

        /// <summary>
        /// Objeyi resim'e dönüştürür dizine dosyayı kaydeder. (volkansendag - 2014.03.03)
        /// </summary>
        public static bool ImageValidation(byte[] file)
        {
            if (file == null)
                return false;
            else
            {
                Image returnImage;
                using (var ms = new MemoryStream(file, 0, file.Length))
                {
                    try
                    {
                        returnImage = Image.FromStream(ms);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Resmin boyutlarını yeniden ayarlar. (volkansendag - 2014.03.03)
        /// </summary>
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        /// <summary>
        /// Resmin boyutlarını yeniden ayarlar. (volkansendag - 2014.03.03)
        /// </summary>
        public static Size GetSizeByWidth(Image img, int width)
        {
            if (img.Width <= width)
                return img.Size;

            int height = Convert.ToInt32((double)(img.Height / ((double)img.Width / width)));
            return new Size(width, height);
        }

        /// <summary>
        /// Resmi Byte[] olarak döndürür. (volkansendag - 2014.03.03)
        /// </summary>
        private static byte[] ImageToByteArraybyMemoryStream(Image image)
        {

            ImageConverter converter = new ImageConverter();
            byte[] imgArray = (byte[])converter.ConvertTo(image, typeof(byte[]));
            return imgArray;
        }


        public static byte[] GetFileFromHttp(string url)
        {
            byte[] file;
            using (WebClient Client = new WebClient())
            {
                try
                {
                    file = Client.DownloadData(url);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return file;
        }

        public static Image GetImageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                return ByteArrayToImage(webClient.DownloadData(url));
            }
        }

        public static Image ByteArrayToImage(byte[] fileBytes)
        {
            using (var stream = new MemoryStream(fileBytes))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
