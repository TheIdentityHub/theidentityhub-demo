//-----------------------------------------------------------------------
// <copyright file="BooleanToVisibilityConverter.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.Win8DemoApp.Converters
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : VisibilityConverter
    {
        protected override bool GetIsVisible(object value, object parameter, string culture)
        {
            return value is bool && (bool)value;
        }
    }
}