using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Tools.Md5.Core.Helper;

namespace Tools.Md5.Core.Models;

public partial class FileInfo : ObservableObject
{
    /// <summary>
    /// 文件地址
    /// </summary>
    [ObservableProperty]
    private string _path = string.Empty;

    /// <summary>
    /// 是否是被忽略的文件
    /// </summary>
    [ObservableProperty]
    private bool _ignored = false;

    /// <summary>
    /// 计算的md5值
    /// </summary>
    [ObservableProperty]
    private string? _md5 = null;

    /// <summary>
    /// file size
    /// </summary>
    [ObservableProperty]
    private long _size = 0;

    public override string ToString()
        =>$"{Path},{Size},{Md5}";

    public string ToString(string root)
        =>$"{FileHelper.GetRelativePath(root, Path)},{Size},{Md5}";
}