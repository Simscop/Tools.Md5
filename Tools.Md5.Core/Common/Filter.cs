using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Tools.Md5.Core.Helper;
using FileInfo = Tools.Md5.Core.Models.FileInfo;
namespace Tools.Md5.Core.Common;

public class Filter
{
    /// <summary>
    /// 过滤文件路径
    /// </summary>
    /// <param name="path"></param>
    public Filter(string path)
    {
        IgnoreFilePath = path;
    }

    public string IgnoreFilePath { get; set; }

    public List<Regex>? Patterns { get; set; }

    /// <summary>
    /// 读取忽略文件，忽略方式类似gitignore
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="Exception"></exception>
    private void ReadIgnoreFile(string path)
    {
        if (!File.Exists(path)) throw new Exception($"Cannot find file: {path}.");

        Patterns = new List<Regex>();

        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

            var pattern = line.Trim();

            Patterns.Add(new Regex("^" + Regex.Escape(pattern).Replace(@"\*", ".*") + "$"));
        }

    }

    /// <summary>
    /// 分开split路径和普通路径
    /// </summary>
    /// <returns></returns>
    public (List<string> focus, List<string> ignore) SplitFilePath(List<string> paths)
    {
        var focus = new List<string>();
        var ignore = new List<string>();

        ReadIgnoreFile(IgnoreFilePath);

        foreach (var path in paths)
        {
            foreach (var pattern in Patterns!)
            {
                if (pattern.Match(path).Success) ignore.Add(path);
                else focus.Add(path);
            }
        }

        return (focus, ignore);
    }


    public void SplitFilePath(ObservableCollection<FileInfo> files)
    {
        ReadIgnoreFile(IgnoreFilePath);

        foreach (var file in files)
        {
            foreach (var pattern in Patterns!)
            {
                file.Ignored = pattern.Match(file.Path).Success;
            }
        }
    }
}
