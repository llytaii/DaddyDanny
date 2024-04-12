using Discord;
using Discord.WebSocket;

public static class Program
{
    private static DiscordSocketClient DiscordClient;

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    private static async Task MessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot) { return; }

        string content = message.CleanContent;
        var channel = message.Channel;
        var id = channel.Id;

        Console.WriteLine($"{id}: {content}");

        SocketTextChannel chan = SocketTextChannel();

        await channel.SendMessageAsync("hallo mein Freund");
    }

    public static async Task Main(string[] args)
    {
        var configResult = Config.LoadConfig(args);
        if (configResult.IsFailed) return;

        var config = configResult.Value;
        var discordConfig = new DiscordSocketConfig
        {
            MessageCacheSize = 100,
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };

        DiscordClient = new(discordConfig);

        DiscordClient.Log += Log;
        DiscordClient.MessageReceived += MessageReceived;

        await DiscordClient.LoginAsync(TokenType.Bot, config.Token);
        await DiscordClient.StartAsync();

        await Task.Delay(-1);
    }

}


