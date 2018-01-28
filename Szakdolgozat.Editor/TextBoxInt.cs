using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Szakdolgozat.Editor
{
    public class TextBoxInt : TextBox
    {
        public int IntValue
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Text))
                {
                    try
                    {
                        return int.Parse(this.Text);
                    }
                    catch (FormatException)
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        
        public TextBoxInt()
        {
            this.KeyDown += TextBoxInt_KeyDown;
        }

        void TextBoxInt_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftDown = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            
            if ((e.Key >= Key.D0 && e.Key <= Key.D9 && !shiftDown) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Escape)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
