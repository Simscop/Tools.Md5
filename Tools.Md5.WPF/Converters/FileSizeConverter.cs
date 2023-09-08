using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tools.Md5.WPF.Converters;

internal class FileSizeConverter : BaseValueConvert<FileSizeConverter>
{
    public static string FormatFileSizeWithOffset(long fileSizeInBytes)
    {
        string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        if (fileSizeInBytes == 0)
        {
            return "0 B";
        }

        var mag = (int)Math.Log(fileSizeInBytes, 1024);
        var adjustedSize = (decimal)fileSizeInBytes / (1L << (mag * 10));

        return $"{adjustedSize:n1} {sizeSuffixes[mag]}";
    }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
       => value is not long v ? Binding.DoNothing : FormatFileSizeWithOffset(v);
}