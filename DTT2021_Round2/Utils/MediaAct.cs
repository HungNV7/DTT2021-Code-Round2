using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DTT2021_Round2
{
    public class MediaAct
    {
        private static MediaAct instance = new MediaAct();

        private MediaAct()
        {
        }

        public static MediaAct Instance { get { return instance; } }


        public void Upload(MediaElement media, string fileName)
        {
            try
            {
                String path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                //path = Path.GetDirectoryName(path);
                //path = Path.GetDirectoryName(path);
                path += @"\Resources\" + fileName;

                media.Source = new Uri(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public void Upload(ImageBrush image, string fileName)
        {
            try
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();

                String path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                //path = Path.GetDirectoryName(path);
                //path = Path.GetDirectoryName(path);
                path += @"\Resources\" + fileName;
                logo.UriSource = new Uri(path);
                logo.EndInit();

                image.ImageSource = logo;
        }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
}

        public void Upload(Image image, string fileName)
        {
            try
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();

                String path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                //path = Path.GetDirectoryName(path);
                //path = Path.GetDirectoryName(path);
                path += @"\Resources\" + fileName;
                logo.UriSource = new Uri(path);
                logo.EndInit();

                image.Source = logo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public void Play(MediaElement media)
        {
            media.Stop();
            media.Visibility = System.Windows.Visibility.Visible;
            media.Play();
        }

        public void Stop(MediaElement media)
        {
            media.Visibility = System.Windows.Visibility.Hidden;
            media.Stop();
        }

    }
}
