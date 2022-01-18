using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTemplate.Core.Converters
{
    using MobileTemplate.Core.Helpers;
    using MobileTemplate.Core.Models;
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    /// <summary>
    /// Returns the proper icon based on the stauts
    /// </summary>
    public class SynchronizationLoadingConverter : IValueConverter
    {
        /// <summary>
        /// Returns the inverse value of a bool value
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var defaultIcon = IconConstants.CheckboxBlankCircleOutline;
            try
            {
                switch (value)
                {
                    case SyncLoadingStatus.Default:
                        return defaultIcon;
                    case SyncLoadingStatus.Error:
                        return IconConstants.CloseCircleOutline;
                    case SyncLoadingStatus.Loading:
                        return IconConstants.ProgressDownload;
                    case SyncLoadingStatus.Loaded:
                        return IconConstants.CheckboxMarkedCircleOutline;
                }
            }
            catch
            {
                // Ignored
            }
            return defaultIcon;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
