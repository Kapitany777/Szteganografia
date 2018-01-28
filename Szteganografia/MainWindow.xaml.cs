using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Szakdolgozat.Kepgeneralas;
using Szakdolgozat.Szteganografia;
using Szakdolgozat.Titkositas;
using Szakdolgozat.XMessageBox;

namespace Szteganografia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                KezdoKepGeneralasa();
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Hiba történt a kezdőkép generálása során!{0}{1}", Environment.NewLine, ex.Message);
                ExtendedMessageBox.ShowError(errorMessage);
            }
        }

        private void BitmapMegjelenitese(WriteableBitmap bmp)
        {
            if (bmp.PixelWidth < BorderImage.Width)
            {
                ImageStego.Width = bmp.PixelWidth;
                ImageStego.Stretch = Stretch.Fill;
            }
            else
            {
                ImageStego.Width = BorderImage.Width;
                ImageStego.Stretch = Stretch.Fill;
            }

            ImageStego.Source = bmp;
            BorderImagaData.DataContext = bmp;
        }

        private void KepBetoltese(string fileNev)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmp.UriSource = new Uri(fileNev);
            bmp.EndInit();

            BitmapMegjelenitese(new WriteableBitmap(bmp));
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (JPG, PNG, BMP, TIF)|*.jpg;*.png;*.bmp;*.tif";
            dlg.Title = "Kép megnyitása";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    KepBetoltese(dlg.FileName);
                    TextBlockFilenev.Text = dlg.FileName;
                }
                catch (Exception ex)
                {
                    string errorMessage = String.Format("Hiba történt a kép betöltése során!{0}{1}", Environment.NewLine, ex.Message);
                    ExtendedMessageBox.ShowError(errorMessage);
                }
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bitmap files (*.bmp)|*.bmp";
            dlg.Title = "Kép mentése";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    ImageStego.Source = null;

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bmp.UriSource = new Uri(TextBlockFilenev.Text);
                    bmp.EndInit();
                    
                    string ujFileNev = dlg.FileName;
                    BitmapMentese(new WriteableBitmap(bmp), ujFileNev);

                    KepBetoltese(ujFileNev);
                    TextBlockFilenev.Text = ujFileNev;
                }
                catch (Exception ex)
                {
                    string errorMessage = String.Format("Hiba történt a kép mentése során!{0}{1}", Environment.NewLine, ex.Message);
                    ExtendedMessageBox.ShowError(errorMessage);
                }
            }
        }

        private void MenuItemOpenText_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.Title = "Szövegfájl megnyitása";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    TextBoxPlainText.Text = File.ReadAllText(dlg.FileName, Encoding.GetEncoding("iso-8859-2"));
                }
                catch (Exception ex)
                {
                    string errorMessage = String.Format("Hiba történt a szövegfájl betöltése során!{0}{1}", Environment.NewLine, ex.Message);
                    ExtendedMessageBox.ShowError(errorMessage);
                }
            }
        }

        private void BitmapMentese(WriteableBitmap bmp, string fileNev)
        {
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (FileStream f = new FileStream(fileNev, FileMode.Create))
            {
                encoder.Save(f);
            }
        }

        private void MenuItemKepGeneralas_Click(object sender, RoutedEventArgs e)
        {
            WindowKepgeneralas DialogKepgeneralas = new WindowKepgeneralas();

            if (DialogKepgeneralas.ShowDialog() == true)
            {
                if (DialogKepgeneralas.Bitmap != null)
                {
                    BitmapMegjelenitese(DialogKepgeneralas.Bitmap);

                    string fileNev = Konyvtarak.AdatKonyvtar + "\\generalt.bmp";
                    BitmapMentese(DialogKepgeneralas.Bitmap, fileNev);
                    TextBlockFilenev.Text = fileNev;
                }
            }
        }

        private void KezdoKepGeneralasa()
        {
            AbstractKepgeneralo img = FactoryKepgeneralo.GetKepgeneralo(KepgeneraloAlgoritmus.Mandelbrot, 400, 400);
            img.Red = 1.0;
            img.Green = 1.0;
            img.Blue = 1.0;
            img.Generalas();

            BitmapMegjelenitese(img.Bitmap);

            string fileNev = Konyvtarak.AdatKonyvtar + "\\generalt.bmp";
            BitmapMentese(img.Bitmap, fileNev);
            TextBlockFilenev.Text = fileNev;
        }

        private void AdatRejtes(string fileNev, string szoveg)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmp.UriSource = new Uri(fileNev);
            bmp.EndInit();
            
            WriteableBitmap wbmp = new WriteableBitmap(bmp);

            int arraySize = wbmp.BackBufferStride * wbmp.PixelHeight;
            byte[] originalPixels = new byte[arraySize];
            wbmp.CopyPixels(originalPixels, wbmp.BackBufferStride, 0);

            SztegoLSB lsb = new SztegoLSB((TitkositoAlgoritmus)ComboBoxTitkositoTipus.SelectedIndex);
            lsb.Elrejtes(szoveg, originalPixels);
            wbmp.WritePixels(new Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight), originalPixels, wbmp.BackBufferStride, 0);
            ImageStego.Source = wbmp;

            string stegoFileNev = Konyvtarak.AdatKonyvtar + "\\stego.bmp";
            BitmapMentese(wbmp, stegoFileNev);

            KepBetoltese(stegoFileNev);
            TextBlockFilenev.Text = stegoFileNev;
        }

        private void MenuItemAdatRejtes_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBlockFilenev.Text))
            {
                try
                {
                    AdatRejtes(TextBlockFilenev.Text, TextBoxPlainText.Text);
                    ExtendedMessageBox.ShowInfo("Az adatrejtés sikeresen megtörtént!");
                }
                catch (Exception ex)
                {
                    string errorMessage = String.Format("Hiba történt az adatrejtés során!{0}{1}", Environment.NewLine, ex.Message);
                    ExtendedMessageBox.ShowError(errorMessage);
                }
            }
            else
            {
                ExtendedMessageBox.ShowError("Nincs megadva kép az adatrejtéshez!");
            }
        }

        private void Visszafejtes(string fileNev)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmp.UriSource = new Uri(fileNev);
            bmp.EndInit();
            
            WriteableBitmap wbmp = new WriteableBitmap(bmp);

            int arraySize = wbmp.BackBufferStride * bmp.PixelHeight;
            byte[] originalPixels = new byte[arraySize];
            bmp.CopyPixels(originalPixels, wbmp.BackBufferStride, 0);

            SztegoLSB lsb = new SztegoLSB();
            TextBoxVisszafejtett.Text = lsb.Visszafejtes(originalPixels);
        }

        private void MenuItemVisszafejtes_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBlockFilenev.Text))
            {
                string extension = System.IO.Path.GetExtension(TextBlockFilenev.Text).ToUpper();

                if (extension != ".BMP")
                {
                    ExtendedMessageBox.ShowError("Csak .bmp kiterjesztésű fájlokban lehet elrejtett szöveget keresni!");
                    return;
                }

                try
                {
                    Visszafejtes(TextBlockFilenev.Text);
                    ExtendedMessageBox.ShowInfo("A szöveg visszafejtése sikeresen megtörtént!");
                }
                catch (Exception ex)
                {
                    string errorMessage = String.Format("Hiba történt a visszafejtés során!{0}{1}", Environment.NewLine, ex.Message);
                    ExtendedMessageBox.ShowError(errorMessage);
                }
            }
            else
            {
                ExtendedMessageBox.ShowError("Nincs megadva kép a visszafejtéshez!");
            }
        }

        private void MenuItemProgramrol_Click(object sender, RoutedEventArgs e)
        {
            WindowProgramrol DialogProgramrol = new WindowProgramrol();

            DialogProgramrol.ShowDialog();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
