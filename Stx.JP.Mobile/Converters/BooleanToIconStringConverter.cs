using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Stx.JP.Mobile.Converters
{
    /// <summary>
    /// This class have methods to convert the Boolean values to string.     
    /// </summary>
    [Preserve(AllMembers = true)]
    public class BooleanToIconStringConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the bool to string.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Type of font Icons. 0=PlusMinus Letters, 1=Heart Icons, 2= Bookmark Icons, 3=Filter Icons.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                if (parameter.ToString() == "0") //PlusMinus Letters
                {
                    if ((bool)value)
                    {
                        return "+";
                    }
                    else
                    {
                        return "-";
                    }
                }
                else if (parameter.ToString() == "1") //Heart Icon
                {
                    if ((bool)value)
                    {
                        return "\ue7e5"; //Heart Filled
                        //return "\ue732"; //UiFont
                    }
                    else
                    {
                        return "\ue7e6"; //Heart Outlined
                        //return "\ue701"; //UiFont
                    }
                }
                else if (parameter.ToString() == "2") // Bookmark Icon
                {
                    if ((bool)value)
                    {
                        return "\ue72f"; //Bookmark Filled
                    }
                    else
                    {
                        return "\ue734"; //Bookmark Outlined
                    }
                }
                else if (parameter.ToString() == "3") //Filter Icon
                {
                    if ((bool)value)
                    {
                        return "\ue8fb"; //Filter Filled with Tick-Mark
                    }
                    else
                    {
                        return "\ue8f1"; //Filter Outlined
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// This method is used to convert the string to bool.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the string.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
