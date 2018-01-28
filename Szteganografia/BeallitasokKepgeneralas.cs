using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Szteganografia
{
    public class BeallitasokKepgeneralas
    {
        public int ComboBoxKepTipus { get; set; }
        
        public string TextBoxPixelWidth { get; set; }
        public string TextBoxPixelHeight { get; set; }

        private double sliderRed;

        public double SliderRed
        {
            get { return sliderRed; }
            set
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    sliderRed = value;
                }
                else
                {
                    sliderRed = 0.0;
                }
            }
        }

        private double sliderGreen;

        public double SliderGreen
        {
            get { return sliderGreen; }
            set
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    sliderGreen = value;
                }
                else
                {
                    sliderGreen = 0.0;
                }
            }
        }

        private double sliderBlue;

        public double SliderBlue
        {
            get { return sliderBlue; }
            set
            {
                if (value >= 0.0 && value <= 1.0)
                {
                    sliderBlue = value;
                }
                else
                {
                    sliderBlue = 0.0;
                }
            }
        }
        
        public BeallitasokKepgeneralas()
        {
            TextBoxPixelWidth = "500";
            TextBoxPixelHeight = "500";
            SliderRed = 1.0;
            SliderGreen = 1.0;
            SliderBlue = 1.0;
        }

        private string FileNev()
        {
            return String.Format("{0}\\{1}", Konyvtarak.AdatKonyvtar, "WindowKepgeneralas.xml");
        }
        
        public void Betoltes()
        {
            string fileNev = FileNev();

            if (!File.Exists(fileNev))
            {
                return;
            }

            XElement root = XElement.Load(fileNev);
           
            ComboBoxKepTipus = int.Parse(root.Element("ComboBoxKepTipus").Value.ToString());
            TextBoxPixelWidth = root.Element("TextBoxPixelWidth").Value.ToString();
            TextBoxPixelHeight = root.Element("TextBoxPixelHeight").Value.ToString();

            NumberFormatInfo nfi = new CultureInfo("en-US").NumberFormat;
            SliderRed = double.Parse(root.Element("SliderRed").Value.ToString(), nfi);
            SliderGreen = double.Parse(root.Element("SliderGreen").Value.ToString(), nfi);
            SliderBlue = double.Parse(root.Element("SliderBlue").Value.ToString(), nfi);
        }

        public void Mentes()
        {
            Konyvtarak.AdatKonyvtarLetrehozasa();
            
            XDocument doc = new XDocument();
            XElement root = new XElement("Beallitasok");
            doc.Add(root);

            root.Add(new XElement("ComboBoxKepTipus", ComboBoxKepTipus));
            root.Add(new XElement("TextBoxPixelWidth", TextBoxPixelWidth));
            root.Add(new XElement("TextBoxPixelHeight", TextBoxPixelHeight));
            root.Add(new XElement("SliderRed", SliderRed));
            root.Add(new XElement("SliderGreen", SliderGreen));
            root.Add(new XElement("SliderBlue", SliderBlue));

            doc.Save(FileNev());
        }
    }
}
