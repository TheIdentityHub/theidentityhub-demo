//-----------------------------------------------------------------------
// <copyright file="VisibilityConverter.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
        
namespace U2UConsult.Win8DemoApp.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public abstract class VisibilityConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isVisible = this.GetIsVisible(value, parameter, language);

            if (this.Reverse)
            {
                isVisible = !isVisible;
            }

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }

        protected abstract bool GetIsVisible(object value, object parameter, string culture);
    }
}