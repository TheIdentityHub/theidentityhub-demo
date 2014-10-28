//-----------------------------------------------------------------------
// <copyright file="ReferenceVisibilityConverter.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.Win8DemoApp.Converters
{
    public class ReferenceVisibilityConverter : VisibilityConverter
    {
        protected override bool GetIsVisible(object value, object parameter, string culture)
        {
            string s = value as string;
            return value != null && (s == null || s.Length > 0);
        }
    }
}