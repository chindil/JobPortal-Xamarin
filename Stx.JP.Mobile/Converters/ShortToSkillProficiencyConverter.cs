using Stx.JP.Mobile.Controls;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Stx.JP.Mobile.Converters
{
    /// <summary>
    /// This class have methods to convert the short values to skill proficiency desc.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ShortToSkillProficiencyConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the short values to skill proficiency desc.
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
                return "";
            }
            else if (value != null && ((value is short) || (value is int)))
            {
                if ((short)value > 5) value = 5;
                return Stx.DataServices.Hrm.StaticData.SkillProficiency.GetDesc((short)value);
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