using BenchmarkDotNet.Running;
using Tools.Md5.Core.Common;
using Tools.Md5.Core.Helper;
using Tools.Md5.Test;

var paths = FileHelper.Travel(@"C:\Users\haeer\Desktop\publish\images");
var filter = new Filter(@"C:\Users\haeer\Desktop\New folder\ignore");

var split = filter.SplitFilePath(paths);

Console.WriteLine(string.Join('\n', split.focus));
Console.WriteLine("-----------------------------------------------------");
Console.WriteLine(string.Join('\n', split.ignore));
