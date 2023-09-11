using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Md5.Core.Helper;

public static class FileHelper
{
    /// <summary>
    /// 要求文件必须存在且小于10G
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsValid(string path)
        => File.Exists(path) &&
              ((new FileInfo(path).Length) >> 20) < 10240;

    /// <summary>
    /// 获取根目录下所有的文件
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<string> Travel(string root)
        => Directory.Exists(root)
            ? Directory.GetFiles(root, "*", SearchOption.AllDirectories).ToList()
            : throw new Exception("The folder path does not exist.");

    public static long GetSize(string path) => new FileInfo(path).Length;

    public static string GetRelativePath(string root, string path)
    {
        var absoluteUri = new Uri(path);
        var baseUri = new Uri(root);

        var relativeUri = baseUri.MakeRelativeUri(absoluteUri);

        return Uri.UnescapeDataString(relativeUri.ToString());
    }

}