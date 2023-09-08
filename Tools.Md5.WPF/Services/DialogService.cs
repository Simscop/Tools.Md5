using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Md5.Core.Models;
using Tools.Md5.Core.Services;

namespace Tools.Md5.WPF.Services;

public class DialogService : IDialogService
{
    public bool ShowOpenFolderDialog(out string path, DialogInfo info)
    {
        path = string.Empty;

        FolderBrowserDialog dialog = new()
        {
            UseDescriptionForTitle = true,
            Description = info.Title,
            ShowNewFolderButton = false,
        };

        if (!string.IsNullOrEmpty(info.Root))
            dialog.InitialDirectory = info.Root;

        if (dialog.ShowDialog() != DialogResult.OK) return false;

        path = dialog.SelectedPath;

        return true;
    }

    public bool ShowOpenFileDialog(out string path, DialogInfo info)
    {
        path = string.Empty;

        var dialog = new OpenFileDialog()
        {
            Title = info.Title,
            Filter = info.Filter,
            Multiselect = false,
        };

        if (!string.IsNullOrEmpty(info.Root))
            dialog.InitialDirectory = info.Root;

        if (dialog.ShowDialog() != DialogResult.OK) return false;

        path = dialog.FileName;
        return true;
    }

    public bool ShowSaveFileDialog(out string path, DialogInfo info)
    {
        path = string.Empty;

        var dialog = new SaveFileDialog()
        {
            Title = info.Title,
            Filter = info.Filter,
        };

        if (!string.IsNullOrEmpty(info.Root))
            dialog.InitialDirectory = info.Root;

        if (dialog.ShowDialog() != DialogResult.OK) return false;

        path = dialog.FileName;
        return true;
    }


    public bool ShowOpenFilesDialog(out List<string> path, DialogInfo info)
    {
        throw new NotImplementedException();
    }
}