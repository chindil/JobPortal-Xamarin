using Stx.JP.Mobile.Controls;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Stx.JP.Mobile.Converters
{
    /// <summary>
    /// This class have methods to convert the string values to boolean (string to visibility boolean).     
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NumberToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the number to boolean.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (parameter == null)
            {
                return false;
            }

            switch (parameter.ToString())
            {
                //case "0": //Short
                //{ }
                case "1" when ((int?)value) > 0: //Int
                    {
                        return true;
                    }
                //case "2": //Long
                    //{ }
                case "3" when ((decimal?)value) > 0: //Decimal
                    {
                        return true;
                    }
            }

            return false;
        }

        /// <summary>
        /// This method is used to convert the boolean to number.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }
}