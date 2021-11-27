using Discord.Interactions;
using Discord.Rest;

namespace DiscordRestClient.Modules
{
    public class MainModule : RestInteractionModuleBase<RestInteractionContext>
    {
        [SlashCommand("ping", "get pong")]
        public async Task Ping() =>
            await RespondAsync("pong");
    }
}
