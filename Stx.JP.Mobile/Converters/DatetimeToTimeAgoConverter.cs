using Stx.JP.Mobile.Controls;
using Stx.Shared;
using Stx.Shared.Extensions.Common;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Stx.JP.Mobile.Converters
{
    /// <summary>
    /// This class have methods to convert the string values to boolean.     
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DatetimeToTimeAgoConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the string to boolean.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime dt)
            {
                return dt.TimeAgo();
            }
            else if (value != null && value is DateTime? && (value as DateTime?).HasValue)
            {
                return (value as DateTime?).Value.TimeAgo();
            }
            return "";
        }

        /// <summary>
        /// This method is used to convert the boolean to string.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>The result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

    }
}