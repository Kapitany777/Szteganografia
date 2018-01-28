using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Szakdolgozat.Kepgeneralas;
using Szakdolgozat.XMessageBox;

namespace Szteganografia
{
    /// <summary>
    /// Interaction logic for WindowKepgeneralas.xaml
    /// </summary>
    public partial class WindowKepgeneralas : Window
    {
        public WriteableBitmap Bitmap { get; private set; }

        public WindowKepgeneralas()
        {
            InitializeComponent();

            Bitmap = null;

            try
            {
                BeallitasokBetoltese();
            }
            catch (Exception ex)
            {
                ExtendedMessageBox.ShowError(ex.Message);
            }
            
            FillRectangle();
        }

        private void BeallitasokBetoltese()
        {
            BeallitasokKepgeneralas b = new BeallitasokKepgeneralas();
            b.Betoltes();

            ComboBoxKepTipus.SelectedIndex = b.ComboBoxKepTipus;
            TextBoxPixelWidth.Text = b.TextBoxPixelWidth.ToString();
            TextBoxPixelHeight.Text = b.TextBoxPixelHeight.ToString();
            SliderRed.Value = b.SliderRed;
            SliderGreen.Value = b.SliderGreen;
            SliderBlue.Value = b.SliderBlue;
        }

        private void BeallitasokMentese()
        {
            BeallitasokKepgeneralas b = new BeallitasokKepgeneralas();

            b.ComboBoxKepTipus = ComboBoxKepTipus.SelectedIndex;
            b.TextBoxPixelWidth = TextBoxPixelWidth.IntValue.ToString();
            b.TextBoxPixelHeight = TextBoxPixelHeight.IntValue.ToString();
            b.SliderRed = SliderRed.Value;
            b.SliderGreen = SliderGreen.Value;
            b.SliderBlue = SliderBlue.Value;

            b.Mentes();
        }

        private void FillRectangle()
        {
            Color color = Color.FromRgb((byte)(SliderRed.Value * 255), (byte)(SliderGreen.Value * 255), (byte)(SliderBlue.Value * 255));
            RectangleColor.Fill = new SolidColorBrush(color);
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPixelWidth.IntValue < 100)
            {
                ExtendedMessageBox.ShowError("A kép szélességének legalább 100 pixelnek kell lennie!");
                return;
            }

            if (TextBoxPixelHeight.IntValue < 100)
            {
                ExtendedMessageBox.ShowError("A kép magasságának legalább 100 pixelnek kell lennie!");
                return;
            }

            AbstractKepgeneralo img = FactoryKepgeneralo.GetKepgeneralo((KepgeneraloAlgoritmus)ComboBoxKepTipus.SelectedIndex, TextBoxPixelWidth.IntValue, TextBoxPixelHeight.IntValue);
            img.Red = SliderRed.Value;
            img.Green = SliderGreen.Value;
            img.Blue = SliderBlue.Value;
            img.Generalas();

            this.Bitmap = img.Bitmap;

            try
            {
                BeallitasokMentese();
            }
            catch (Exception ex)
            {
                ExtendedMessageBox.ShowError(ex.Message);
            }

            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FillRectangle();
        }
    }
}
