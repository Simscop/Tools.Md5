using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Md5.Core.Helper;

public static class BytesHelper
{
    /// <summary>
    /// 从字节数组转换为十六进制字符串（小写）
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isUpper"></param>
    /// <returns></returns>
    public static string ToHexString(this byte[] data, bool isUpper = false)
    {
        var format = isUpper ? "X2" : "x2";;

        var builder = new StringBuilder();

        foreach (var t in data)
            builder.Append(t.ToString(format));

        return builder.ToString();
    }

    /// <summary>
    /// 从十六进制字符串（小写）转换为字节数组
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static byte[] ToBytes(this string data)
    {
        var bytes = new List<byte>();

        for (var i = 0; i < data.Length; i += 2)
        {
            var hex = data.Substring(i, 2);
            bytes.Add(Convert.ToByte(hex, 16));
        }

        return bytes.ToArray();
    }
}