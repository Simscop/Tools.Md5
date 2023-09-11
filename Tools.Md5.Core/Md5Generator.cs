using System.Security.Cryptography;
using Tools.Md5.Core.Helper;

namespace Tools.Md5.Core;

public static class Md5Generator
{
    /// <summary>
    /// 从单个文件获取MD5值
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<string> FromFileAsync(string path)
    {
        var data = File.OpenRead(path);
        
        using var md5 = MD5.Create();

        var hash = await md5.ComputeHashAsync(data);

        return hash.ToHexString(true);
    }
}