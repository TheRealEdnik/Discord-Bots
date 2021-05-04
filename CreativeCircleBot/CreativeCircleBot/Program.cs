using Discord.WebSocket;
using Discord;
using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Rest;

namespace CreativeCircleBot
{
    class Program
    {
        private static char PREFIX = '!';

        private static DiscordSocketClient _client;
        public static async Task Main()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.UserJoined += OnUserJoin;
            _client.Log += Log;

            var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task OnUserJoin(SocketGuildUser arg)
        {
            var channel = _client.GetChannel(839016058454278167);
            var invite = (channel as ITextChannel).CreateInviteAsync(maxAge: 86400, maxUses: 1, isTemporary: false, isUnique: true, options: RequestOptions.Default);
            (channel as ITextChannel).SendMessageAsync(invite.ToString());
            return Task.CompletedTask;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private static async Task CommandHandler(SocketMessage message)
        {
            string command = "";
            int lengthOfCommand = -1;

            if (!message.Content.StartsWith(PREFIX))
                return;

            if (message.Author.IsBot)
                return;

            if (message.Content.Contains(' '))
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            switch (command)
            {
                case "hello":
                    await message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
                    break;

                case "age":
                    // TODO --> !age @UserMention functonality

                    /*
                      
                    string[] parts = message.ToString().Split(' ');
                    if (!(parts[2] as SocketMessage is IGroupUser))
                    {
                       break;
                    }
                    
                     */

                    await message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
                    break;

                case "createinv":
                    var invite1 = await (_client.GetChannel(839016058454278167) as ITextChannel).CreateInviteAsync(maxAge: null, maxUses: 1, isTemporary: false, isUnique: true, options: RequestOptions.Default);

                    var invite2 = await (_client.GetChannel(839016058454278167) as ITextChannel).CreateInviteAsync(maxAge: null, maxUses: 1, isTemporary: false, isUnique: true, options: RequestOptions.Default);

                    await message.Channel.SendMessageAsync($"Here you have your 2 invites. Make sure to use them wisely!\n{invite1.Url}\n{invite2.Url}");
                    break;

                default:
                    break;
            }
        }
    }
}
