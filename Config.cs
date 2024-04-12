using FluentResults;
using System.Text.Json;

public class Config
{
    public string Token { get; set; } = "";
    public List<string> ChannelIds { get; set; } = new();

    public static Result<Config> LoadConfig(string[] args)
    {
        var executableName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

        if (args.Length < 1)
        {
            Console.WriteLine($"usage: {executableName} <path_to_config>");
            return Result.Fail("");
        }

        string configPath = args.First();

        if (!File.Exists(configPath))
        {
            Console.WriteLine($"invalid config path: {configPath}");
            Console.WriteLine($"usage: {executableName} <path_to_config>");
            return Result.Fail("");
        }

        var configStream = new FileStream(configPath, FileMode.Open);
        var config = JsonSerializer.Deserialize<Config>(configStream);

        if (config == null)
        {
            Console.WriteLine("failed to parse config");
            return Result.Fail("");
        }

        return Result.Ok(config);
    }
}
