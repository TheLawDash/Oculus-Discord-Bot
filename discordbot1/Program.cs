using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using DSharpPlus.Entities;
using Serilog;

namespace discordbot1
{
    internal class Program
    {
        public DiscordChannel logs;

        public static void Main(string[] args)
        {
            //Starts RunAsync for discord bot...
            var prog = new Program();
            prog.RunAsync().GetAwaiter().GetResult();

        }
        //Declaring public variables
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            //Defining logs path for discord bot, naming convention of month-day-year etc...
            string path = @"logs/" + DateTime.Now.ToString(@"MM-dd-yyyy_h-mm-tt") + ".txt";
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(path)
            .CreateLogger();
        
            //Defining logging using Serilog
            var logFactory = new LoggerFactory().AddSerilog();

            //Defining config for discord bot
            var config = new DiscordConfiguration
            {
                Token = "ENTER TOKEN HERE",
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                LoggerFactory = logFactory,
                //UseInternalLogHandler = true,
            };

            //Setting the client config
            Client = new DiscordClient(config);
            
            //Setting interactivity
            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });
            
            //Defining command prefixes
            string[] prefixes = { ".", "!", "/", "?", "$" };
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = prefixes,
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true
            };

            //Sets the command configuration
            Commands = Client.UseCommandsNext(commandsConfig);

            //Sets command list
            Commands.RegisterCommands<SingleGen>();

            //Connects to bot
            Client.ConnectAsync();

            //Leaves program running and listening
            await Task.Delay(-1);


        }
    }
}
