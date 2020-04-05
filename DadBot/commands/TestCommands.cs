using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DadBot.commands
{
    public class TestCommands : BaseCommandModule {
        [Command("ping")]
        public async Task Ping(CommandContext ctx) {
            await 
                ctx.Channel.SendMessageAsync("pong")
                .ConfigureAwait(false);               // await *CODE*.ConfigureAwait(false);
        }

        [Command("add")]
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo) {
            await 
                ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }
    }


}
