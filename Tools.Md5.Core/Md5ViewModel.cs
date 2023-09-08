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
    private string _status;

    [RelayCommand]
    void ReverseSelected()
    {
        if (SelectedFiles is null) return;

        foreach (FileInfo file in SelectedFiles)
        {
            file.Ignored = !file.Ignored;
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
    void Export()
    {
        Status = "Exporting...";

        var header = $"Time,{DateTime.Now:yyyy-M-d HH:mm:ss}\n" +
                     $"---\n" +
                     $"File,Size,MD5\n";
        var content = string.Join('\n', Files.Where(item => !item.Ignored));

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

    [RelayCommand]
    void Load()
    {
        var root = @"C:\Users\haeer\Desktop\win12-main";

        var list = FileHelper.Travel(root);

        list.ForEach(item => Files.Add(new FileInfo()
        {
            Path = item,
            Ignored = false,
            Size = FileHelper.GetSize(item),
            Md5 = "",
        }));
    }

}