using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tools.Md5.Core.Common;
using Tools.Md5.Core.Helper;
using Tools.Md5.Core.Models;
using Tools.Md5.Core.Services;
using FileInfo = Tools.Md5.Core.Models.FileInfo;

namespace Tools.Md5.Core;

public partial class Md5ViewModel : ObservableObject
{
    private string? _root = null;

    public IDialogService DialogService { get; set; }

    public Md5ViewModel(IDialogService dialogService)
    {
        DialogService = dialogService;
    }

    [ObservableProperty]
    private ObservableCollection<FileInfo> _files = new();

    [ObservableProperty]
    private IList? _selectedFiles;

    [ObservableProperty]
    private string? _status;

    [RelayCommand]
    void SwitchSelected(string value)
    {
        if (SelectedFiles is null || !int.TryParse(value, out var v)) return;

        foreach (FileInfo file in SelectedFiles)
        {
            file.Ignored = v switch
            {
                0 => !false,
                1 => !true,
                2 => !file.Ignored,
                _ => throw new Exception(),
            };
        }
    }


    [RelayCommand]
    void SelecteFolder()
    {
        if (!DialogService.ShowOpenFolderDialog(out var path, new DialogInfo()
        {
            Title = "Select Folder",

        })) return;

        var files = FileHelper.Travel(path);
        _root = path;

        Files.Clear();

        files.ForEach(item => Files.Add(new FileInfo()
        {
            Path = item,
            Ignored = false,
            Size = FileHelper.GetSize(item),
            Md5 = "",
        }));
    }

    [RelayCommand]
    void SelecteIgnoreFile()
    {
        if (!DialogService.ShowOpenFileDialog(out var path, new DialogInfo()
        {
            Title = "Select Ignore File",
            Filter = "Ignore file (*.txt;*.ignore;ignore)|*.txt;*.ignore;ignore|All files (*.*)|*.*",
            Root = "",
        })) return;

        var fileter = new Filter(path);
        fileter.SplitFilePath(Files);
    }

    [RelayCommand]
    void Export(string value)
    {
        try
        {
            if (_root == null || !int.TryParse(value, out var v)) return;

            Status = "Exporting...";

            var header = $"Time,{DateTime.Now:yyyy-M-d HH:mm:ss}\n" +
                         (v == 0 ? "" : $"Root,{_root}") +
                         $"---\n" +
                         $"File,Size,MD5\n";
            var content = v == 0
                ? string.Join('\n', Files.Where(item => !item.Ignored))
                : string.Join('\n', Files.Where(item => !item.Ignored)
                    .Select(item => item.ToString(_root)));

            if (!DialogService.ShowSaveFileDialog(out var path, new DialogInfo()
            {
                Title = "Save File",
                Filter = "csv file (*.csv)|*.csv|All files (*.*)|*.*",
                Root = "",
            }))
            {
                Status = "Cancel export file";
                return;
            }

            File.WriteAllText(path, header + content);

            Status = $"The file is exported successfully, and the export path is {path}";
        }
        catch (Exception e)
        {
            Status = $"Export failed, error message: {e.Message}";
        }
    }

    [RelayCommand]
    async Task RunAsync()
    {
        await Task.Run(() =>
        {
            Files.Where(item => !item.Ignored).ToList().ForEach(item =>
            {
                item.Md5 = Md5Generator.FromFileAsync(item.Path).Result;
            });
        });
    }
}