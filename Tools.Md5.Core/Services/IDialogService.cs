using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Md5.Core.Models;

namespace Tools.Md5.Core.Services;



public interface IDialogService
{
    /// <summary>
    /// 选择文件夹
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool ShowOpenFolderDialog(out string path, DialogInfo info);

    /// <summary>
    /// 选择单个文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool ShowOpenFileDialog(out string path, DialogInfo info);

    /// <summary>
    /// 保存单个文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool ShowSaveFileDialog(out string path,DialogInfo info);

    /// <summary>
    /// 选择多个文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool ShowOpenFilesDialog(out List<string> path, DialogInfo info);

    
}