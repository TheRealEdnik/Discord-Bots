using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Commands")]
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Alias("s")]
        [Summary("Make the bot say something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder] string text)
            => ReplyAsync(text);

        [Command("invites"), Alias ("inv")]
        public async Task Invites()
        {
            var invitesRole = Context.Guild.Roles.FirstOrDefault(x => x.Id == 839474198057320448);

            if (!(Context.User as SocketGuildUser).Roles.Any(x => x.Id == 839474198057320448))
            {
                await Context.Channel.SendMessageAsync("You can't get anymore invites!");
                return;
            }

            var inv1 = await ((Context.Guild as SocketGuild).GetChannel(839016058454278167) as ITextChannel).CreateInviteAsync(null, 1, false, true) as IInvite;
            var inv2 = await ((Context.Guild as SocketGuild).GetChannel(839016058454278167) as ITextChannel).CreateInviteAsync(null, 1, false, true) as IInvite;

            await (Context.User as SocketGuildUser).SendMessageAsync($"Here you have your invite links: \n\n{inv1.Url}\n{inv2.Url}\nMake sure to use them wisely!");

            await (Context.User as SocketGuildUser).RemoveRoleAsync(invitesRole as IRole);
        }
    }
}
