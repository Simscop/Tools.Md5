using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Md5.Core.Models;

public class DialogInfo
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// 初始目录
    /// </summary>
    public string Root { get; set; } = "";

    /// <summary>
    /// 删选后缀
    /// </summary>
    public string Filter { get; set; } = "";
}