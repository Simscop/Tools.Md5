
using BenchmarkDotNet.Parameters;
using BenchmarkDotNet.Attributes;
using Tools.Md5.Core;
using BenchmarkDotNet.Running;

namespace Tools.Md5.Test;

public class Md5Benchmark
{
    private const string Path10G = @"D:\SteamLibrary\steamapps\common\Assassins Creed Odyssey\DataPC_ACD_Greece.forge";

    private const string Path1G = @"D:\SteamLibrary\steamapps\common\Assassins Creed Odyssey\dlc_33\DataPC_33_dlc.forge";

    private const string Path500M = @"C:\Users\haeer\Downloads\pycharm-professional-2023.1.3.exe";

    private const string Path300M = @"C:\Program Files\Google\Chrome\Application\116.0.5845.179\Installer\chrome.7z";

    private const string Path100M =
        @"C:\Windows\System32\DriverStore\FileRepository\nv_dispsig.inf_amd64_6c392b661a08bfd2\nvrtum64.dll";

    private const string Path10M =
        @"C:\Program Files\Leica Microsystems CMS GmbH\LAS X\AddIns\DyeDatabase\Lms3DWPF.dll";

    private const string Path1M = @"F:\old\Code\2021-2022\Python\0-2021\数据处理类\CSV数据匹配\task1_7.csv";


    [Params(Path1M, Path10M, Path100M, Path300M, Path500M)]
    public string Path { get; set; } = "";

    [Benchmark]
    public void TestFromSingleFile()
        => Md5Generator.FromFileAsync(Path).Wait();

    static void Run()
        => BenchmarkRunner.Run<Md5Benchmark>();
}