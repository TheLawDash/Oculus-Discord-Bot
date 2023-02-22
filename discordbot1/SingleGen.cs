using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using static discordbot1.gemail;
using static discordbot1.Availability;
using static discordbot1.Delete;
using static discordbot1.CreateAccount;
using static discordbot1.CheckInbox;
using static discordbot1.ConfirmLink;
using static discordbot1.code;
using static discordbot1.fbcode;
using System.Threading;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static discordbot1.metacode;
using static discordbot1.metaavail;
using System.Windows.Forms;

namespace discordbot1
{
    public class SingleGen : BaseCommandModule
    {
        //callCommand structure to know who created accounts with account list allowing for multi-gen
        public struct callCommand
        {
            public DiscordMember memeber;
            public string DiscordUsername;
            public string usr;
            public List<string> firstname;
            public List<string> lastname;
            public List<string> email;
            public List<string> password;
            public List<string> cofnum;
            public List<string> cmgUsr;
            public int amount;
        }
        //Declaring public variables
        public static List<callCommand> commandList = new List<callCommand>();
        public DiscordClient Client { get; private set; }
        public static int amount;
        public static int createcount;
        public static int checkcount;
        public static int verify;

        //Discord role IDs
        public static long Council = 1000587839777542235;
        public static long Level3Access = 1000586908000333924;
        public static long LevelOmega = 1000586957962870795;
        public static long Scientist = 1000586848793542716;
        public static long Janitor = 1000586779344240690;

        //Defining availability command
        [Command("a")]
        public async Task check(CommandContext ctx, string username)
        {
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1000585263837351957 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                //Sends a message in the same chat telling the user it is checking the username...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Checking username...`").ConfigureAwait(false);

                //calls the avail method passing through the username string
                await avail(username);

                //Messages the user if any of the booleans were set to true...
                if (baduser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is NOT available...`").ConfigureAwait(false);
                }
                else if (taken)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
                }
                else if (invalid)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                }
                else if (special)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                }
                else if (shortuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                }
                else if (longuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                }
                else if (method)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);
                }
                else if (noChar)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);
                }
                else if (Availability.available)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is available...`").ConfigureAwait(false);
                }
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);

            }
        }

        ////Experimental feature for meta-avail [Not Functioning ATM]
        //[Command("ma")]
        //public async Task mcheck(CommandContext ctx, string username)
        //{
        //    //Ensuring the user is typing in the correct channel
        //    if (ctx.Message.Channel.Id == 1012568648055013416 | ctx.Message.Channel.Id == 1000296387080114216)
        //    {
        //        //Sends a message in the same chat telling the user it is checking the username...
        //        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Checking username...`").ConfigureAwait(false);
        //
        //        //calls the mavail method passing through the username string
        //        await mavail(username);
        //
        //        //Messages the user if any of the booleans were set to true...
        //        if (baduser)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is NOT available...`").ConfigureAwait(false);
        //        }
        //        else if (taken)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
        //        }
        //        else if (invalid)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
        //        }
        //        else if (special)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
        //        }
        //        else if (shortuser)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
        //        }
        //        else if (longuser)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too long to be created, please input 20 or less characters...`").ConfigureAwait(false);
        //        }
        //        else if (method)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);
        //        }
        //        else if (noChar)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);
        //        }
        //        else if (manySpecial)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
        //        }
        //        else if (metaavail.available)
        //        {
        //            await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is available...`").ConfigureAwait(false);
        //        }
        //    }
        //    else
        //    {                
        //        //If user calls command in wrong channel it deletes and tells them to use the propper channel...
        //        await ctx.Message.DeleteAsync();
        //        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);
        //
        //    }
        //}
        public static bool janitor;

        //Command for generating an account -- SingleGen
        [Command("g")]
        //Makes sure the user has one of the 4 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access", "Scientist Level")]
        public async Task sg(CommandContext ctx, string username)
        {
            janitor = false;
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1000295628909318164 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                //Defines the amount of accounts being generated [1]
                amount = 1;

                //Sends a message in the same chat telling the user it is creating the account...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Creating account, one moment...`").ConfigureAwait(false);

                //calls the mavail method passing through the username string
                await avail(username);

                //Messages the user if any of the booleans were set to true...
                if (baduser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is NOT available...`").ConfigureAwait(false);
                }
                else if (taken)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
                }
                else if (invalid)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                }
                else if (special)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                }
                else if (shortuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                }
                else if (longuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                }
                else if (noChar)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);
                }
                else if (manySpecial)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
                }
                else if (method)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);
                }
                else
                {
                    //If available it will go through the mail and delete anyhting extra from mailbox...
                    delmail();

                    //Generates email and password
                    genmail(amount, ctx.Member, username);

                    //cCreates local variable of index
                    int index = 0;

                    //navigates through each itteration of commandList
                    for (int j = 0; j < commandList.Count; j++)
                    {
                        //Checks through commandList to see what index matches with the member that executed the command
                        callCommand d = commandList[j];
                        if (d.memeber == ctx.Member)
                        {
                            //saves to index
                            index = j;
                        }
                    }
                    
                    //Sets local callCommand struct z to the commandList index that matches user input...
                    callCommand z = commandList[index];

                    //Calls create method while passing through the username, itteration and callCommand struct
                    create(username, 0, z);

                    //Checks the inbox for the confirmation email...
                    checkinbox(1, z);

                    //Gathers new index of callCommand list since the old one was removed and new one was added
                    for (int j = 0; j < commandList.Count; j++)
                    {
                        callCommand d = commandList[j];
                        if (d.memeber == ctx.Member)
                        {
                            index = j;
                        }
                    }

                    //Sets local callCommand struct z to the commandList index that matches user input...
                    callCommand c = commandList[index];

                    //Confirms the account creation
                    await confirm(0, 1, c);

                    //Creates ADDED string for URI handler for the oculus account tools
                    string ADDED = "AccountSwapper://" + username + "/" + c.email[0] + "/" + c.password[0];

                    //Creates a discord embed for discord DM to present the data in a nice manner...
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                    {

                        Color = DiscordColor.Purple,
                        Title = "Account Details",
                        Description = "Add Account To Oculus Account Generator: <" + ADDED + ">",
                        ImageUrl = "https://cdn.discordapp.com/attachments/710094676190822440/1033852651743485973/lmao_oculus.png",
                        Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                        {
                            Url = "https://1000logos.net/wp-content/uploads/2021/07/Oculus_Symbol.png"
                        },
                        Author = new DiscordEmbedBuilder.EmbedAuthor
                        {
                            Name = "Oculus account created!"
                        }



                    };
                    //Adds different fields to the embed
                    embed.AddField("Username:", username, false);
                    embed.AddField("Email:", c.email[0], false);
                    embed.AddField("Password:", c.password[0], false);
                    embed.AddField("Full Name:", c.firstname[0] + " " + c.lastname[0], false);
                    try
                    {
                        var discordMember = ctx.Member;

                        //Attempts to send discord embed
                        await ctx.Member.SendMessageAsync(embed);
                    }
                    catch
                    {
                        //If the User have discord DMs turned off it will send an error message...
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `You have your DMs blocked so like, I guess message law for info.`");
                    }
                    //Defines discord config for DMing the user their data
                    var config = new DiscordConfiguration
                    {
                        Token = "TOKEN HERE",
                        TokenType = TokenType.Bot,
                        AutoReconnect = true,
                        MinimumLogLevel = LogLevel.Debug,
                        //UseInternalLogHandler = true,
                    };
                    Client = new DiscordClient(config);

                    //Defines log channel
                    var work = await Client.GetChannelAsync(1000280066346979388);

                    //Sends account info to the log channel incase there are any issues...
                    await work.SendMessageAsync(ctx.Message.Author.Mention + "has generated the following account: \n" +
                        "***Username***: " + username + "\n***Email***: ||" + c.email[0] + "||\n***Password***: ||" + c.password[0] + "||\n***Full Name***: ||" + c.firstname[0] + " " + c.lastname[0] + "||");

                    //Sends a message in the same channel telling the user the account has been created and has been sent to your DMs
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your account info has been sent to your DMs!`");
                }
                
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);

            }
        }

        //Command for generating an Meta account -- SingleGen
        [Command("mag")]
        //Makes sure the user has one of the 4 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access", "Scientist Level")]
        public async Task mag(CommandContext ctx, string username)
        {
            janitor = false;
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1012568447089131524 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                //Defines the amount of accounts being generated [1]
                amount = 1;

                //Sends a message in the same chat telling the user it is creating the account...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Creating account, one moment...`").ConfigureAwait(false);

                //calls the mavail method passing through the username string
                await avail(username);

                //Messages the user if any of the booleans were set to true...
                if (baduser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is NOT available...`").ConfigureAwait(false);
                }
                else if (taken)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
                }
                else if (invalid)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                }
                else if (special)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                }
                else if (shortuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                }
                else if (longuser)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                }
                else if (noChar)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);

                }
                else if (manySpecial)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
                }
                else if (method)
                {
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);
                }
                else
                {
                    //If available it will go through the mail and delete anyhting extra from mailbox...
                    delmail();

                    //Generates email and password
                    genmail(amount, ctx.Member, username);

                    //cCreates local variable of index
                    int index = 0;

                    //navigates through each itteration of commandList
                    for (int j = 0; j < commandList.Count; j++)
                    {
                        //Checks through commandList to see what index matches with the member that executed the command
                        callCommand d = commandList[j];
                        if (d.memeber == ctx.Member)
                        {
                            //saves to index
                            index = j;
                        }
                    }

                    //Sets local callCommand struct z to the commandList index that matches user input...
                    callCommand z = commandList[index];

                    //Calls create method while passing through the username, itteration and callCommand struct
                    create(username, 0, z);

                    //Checks the inbox for the confirmation email...
                    checkinbox(1, z);

                    //Gathers new index of callCommand list since the old one was removed and new one was added
                    for (int j = 0; j < commandList.Count; j++)
                    {
                        callCommand d = commandList[j];
                        if (d.memeber == ctx.Member)
                        {
                            index = j;
                        }
                    }

                    //Sets local callCommand struct z to the commandList index that matches user input...
                    callCommand c = commandList[index];

                    //Confirms the account creation
                    await confirm(0, 1, c);

                    //Creates ADDED string for URI handler for the oculus account tools
                    string ADDED = "MetaSwapper://" + username + "/" + c.email[0] + "/" + c.password[0];

                    //Creates a discord embed for discord DM to present the data in a nice manner...
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                    {

                        Color = DiscordColor.Purple,
                        Title = "Account Details",
                        Description = "Add Account To Oculus Account Generator: <" + ADDED + ">",
                        ImageUrl = "https://cdn.discordapp.com/attachments/710094676190822440/1033852651743485973/lmao_oculus.png",
                        Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                        {
                            Url = "https://1000logos.net/wp-content/uploads/2021/10/logo-Meta.png"
                        },
                        Author = new DiscordEmbedBuilder.EmbedAuthor
                        {
                            Name = "Meta account created!"
                        }



                    };
                    //Adds different fields to the embed
                    embed.AddField("Username:", username, false);
                    embed.AddField("Email:", c.email[0], false);
                    embed.AddField("Password:", c.password[0], false);
                    embed.AddField("Full Name:", c.firstname[0] + " " + c.lastname[0], false);
                    try
                    {
                        await MetaGen(c.email[0], c.password[0]);

                        //Attempts to send discord embed
                        await ctx.Member.SendMessageAsync(embed);
                    }
                    catch
                    {
                        //If the User have discord DMs turned off it will send an error message...
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `You have your DMs blocked so like, I guess message law for info.`");
                    }
                    //Defines discord config for DMing the user their data
                    var config = new DiscordConfiguration
                    {
                        Token = "TOKEN HERE",
                        TokenType = TokenType.Bot,
                        AutoReconnect = true,
                        MinimumLogLevel = LogLevel.Debug,
                        //UseInternalLogHandler = true,
                    };
                    Client = new DiscordClient(config);

                    //Defines log channel
                    var work = await Client.GetChannelAsync(1000280066346979388);

                    //Sends account info to the log channel incase there are any issues...
                    await work.SendMessageAsync(ctx.Message.Author.Mention + "has generated the following meta account: \n" +
                        "***Username***: " + username + "\n***Email***: ||" + c.email[0] + "||\n***Password***: ||" + c.password[0] + "||\n***Full Name***: ||" + c.firstname[0] + " " + c.lastname[0] + "||");

                    //Sends a message in the same channel telling the user the account has been created and has been sent to your DMs
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your account info has been sent to your DMs!`");
                    
                }
                
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);

            }
        }

        //Command for finding Facebook code
        [Command("fbc")]
        //Makes sure the user has one of the 4 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access", "Scientist Level")]
        public async Task verify1(CommandContext ctx, string email)
        {
            janitor = false;
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1011341867822108672 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                //This deletes the user message
                await ctx.Message.DeleteAsync();
                
                //Sends message saying the email address is being checked...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Checking the email address...`");
                
                //Calls the fbCode function passing through the email
                fbCode(email);

                //Check's if the email is valid
                if (fbcode.stupid)
                {
                    //Sends a message if there is no email
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `looks like your email address may be incorrect, please verify your send the correct one...`");
                }
                else
                {
                    //Sends message saying a message has been sent to DMs
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `your code has been sent to your dms!`");

                    //Creates a discord embed for discord DM to present the data in a nice manner...
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                    {

                        Color = DiscordColor.Purple,
                        Title = "Your code",
                        Description = "Here's your code!",
                        ImageUrl = "https://cdn.discordapp.com/attachments/710094676190822440/1033852651743485973/lmao_oculus.png",
                        Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                        {
                            Url = "https://1000logos.net/wp-content/uploads/2021/07/Oculus_Symbol.png"
                        },
                        Author = new DiscordEmbedBuilder.EmbedAuthor
                        {
                            Name = "Facebook Verification Code!"
                        }
                    };
                    //Adds different fields to the embed
                    embed.AddField("Your Code:", fbcode.codey, false);
                    embed.AddField("Your Link:", fbcode.linky, false);
                    try
                    {
                        //Attempts to send discord embed
                        await ctx.Member.SendMessageAsync(embed);
                    }
                    catch
                    {
                        //If the User have discord DMs turned off it will send an error message...
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `You're an idiot and have your DMs blocked so like, I guess message law for info.`");
                    }
                }
                
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Please use the proper channel for this command...`").ConfigureAwait(false);

            }
        }

        //Command for finding Oculus/Meta code
        [Command("v")]
        //Makes sure the user has one of the 4 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access", "Scientist Level")]
        public async Task verify11(CommandContext ctx, string email)
        {
            janitor = false;
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1011333447014428713 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                //This deletes the user message
                await ctx.Message.DeleteAsync();

                //Sends message saying the email address is being checked...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Checking the email address...`");

                //Calls the fbCode function passing through the email
                OculusCode(email);

                //Check's if the email is valid
                if (code.stupid)
                {
                    //Sends a message if there is no email
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `looks like you're stupid lmao, maybe try having oculus send a link again or just be better...`");
                }
                else
                {
                    //Sends message saying a message has been sent to DMs
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `your code has been sent to your dms!`");

                    //Creates a discord embed for discord DM to present the data in a nice manner...
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                    {

                        Color = DiscordColor.Purple,
                        Title = "Your code",
                        Description = "Here's your code!",
                        ImageUrl = "https://cdn.discordapp.com/attachments/710094676190822440/1033852651743485973/lmao_oculus.png",
                        Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                        {
                            Url = "https://1000logos.net/wp-content/uploads/2021/07/Oculus_Symbol.png"
                        },
                        Author = new DiscordEmbedBuilder.EmbedAuthor
                        {
                            Name = "Oculus Verification Code!"
                        }
                    };

                    //Adds different fields to the embed
                    embed.AddField("Your Code:", code.codey, false);
                    try
                    {
                        //Attempts to send discord embed
                        await ctx.Member.SendMessageAsync(embed);
                    }
                    catch
                    {
                        //If the User have discord DMs turned off it will send an error message...
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `You have your DMs blocked so like, I guess message law for info.`");
                    }
                }
                
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Please use the proper channel for this command...`").ConfigureAwait(false);
            }
        }

        //Command for private multi gen
        [Command("pmg")]
        //Makes sure the user has one of the 2 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega")]
        public async Task pmg(CommandContext ctx, string username, int amount)
        {
            //Ensuring the user is typing in the correct channel
            if (ctx.Channel.Id == 1000588229650677810 | ctx.Message.Channel.Id == 1000296387080114216)
            {   
                if (amount > 15)
                {
                    //If the user tries to generate more than 14 accounts it will say this
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Please contact Law for generating 15 or more accounts`").ConfigureAwait(false);
                }
                else
                {
                    //This deletes the user message
                    await ctx.Message.DeleteAsync();

                    //This sends message telling the user an account is being created...
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Creating accounts, one moment...`").ConfigureAwait(false);

                    //calls the mavail method passing through the username string
                    await avail(username);

                    //Messages the user if any of the booleans were set to true...
                    if (baduser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Your username is NOT available...`").ConfigureAwait(false);
                    }
                    else if (taken)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your username has been taken...`").ConfigureAwait(false);
                    }
                    else if (invalid)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your username contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                    }
                    else if (special)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Your username begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                    }
                    else if (shortuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Your username is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                    }
                    else if (longuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your username is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                    }
                    else if (noChar)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);
                    }
                    else if (manySpecial)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
                    }
                    else if (method)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your username is unavailable and requires a special method...`").ConfigureAwait(false);
                    }
                    else
                    {
                        //If available it will go through the mail and delete anyhting extra from mailbox...
                        delmail();

                        //Generates email and password
                        genmail(amount, ctx.Member, username);

                        //Creates local variable of index
                        int index = 0;

                        //navigates through each itteration of commandList
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            //Checks through commandList to see what index matches with the member that executed the command
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                //saves to index
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand z = commandList[index];

                        //Calls create method while passing through the username, itteration and callCommand struct
                        for (int i = 0; i < amount; i++)
                        {
                            create(username, i, z);
                        }
                        creating = true;

                        //Checks the inbox for the confirmation email...
                        checkinbox(amount, z);

                        //Gathers new index of callCommand list since the old one was removed and new one was added
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand c = commandList[index];
                        int veriy;

                        //Confirms the account creation
                        for (veriy = 0; veriy <= amount; veriy++)
                        {
                            confirm(veriy, amount, c);
                        }
                        //Defines discord config for DMing the user their data
                        var config = new DiscordConfiguration
                        {
                            Token = "TOKEN HERE",
                            TokenType = TokenType.Bot,
                            AutoReconnect = true,
                            MinimumLogLevel = LogLevel.Debug,
                            //UseInternalLogHandler = true,
                        };

                        Client = new DiscordClient(config);

                        //Defines log channel
                        var work = await Client.GetChannelAsync(1002011080857485433);

                        //Sends account info to the log channel incase there are any issues...
                        await work.SendMessageAsync(ctx.Message.Author.Mention + "has generated the following accounts:");

                        //Sends a message in the same channel telling the user the account has been created and has been sent to your DMs
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your account info has been sent to your DMs!`");
                        
                        //Loops through created accounts and DMs them individually to the user
                        for (int em = 0; em < amount; em++)
                        {
                            //Creates ADDED string for URI handler for the oculus account tools
                            string ADDED = "AccountSwapper://" + username + "/" + c.email[em] + "/" + c.password[em];
                            
                            //Creates a discord embed for discord DM to present the data in a nice manner...
                            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                            {

                                Color = DiscordColor.Purple,
                                Title = "Account Details",
                                Description = "Add Account To Oculus Account Generator: <" + ADDED + ">",
                                ImageUrl = "https://cdn.discordapp.com/attachments/710094676190822440/1033852651743485973/lmao_oculus.png",
                                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                                {
                                    Url = "https://1000logos.net/wp-content/uploads/2021/07/Oculus_Symbol.png"
                                },
                                Author = new DiscordEmbedBuilder.EmbedAuthor
                                {
                                    Name = "Private Multi Generated Accounts!"
                                }


                            };
                            //Adds different fields to the embed
                            embed.AddField("Username:", username, false);
                            embed.AddField("Email:", c.email[em], false);
                            embed.AddField("Password:", c.password[em], false);
                            embed.AddField("Full Name:", c.firstname[em] + " " + c.lastname[em], false);
                           
                            //Sends the Embed to the user
                            await ctx.Member.SendMessageAsync(embed: embed);

                            //Sends account info to the log channel incase there are any issues...
                            await work.SendMessageAsync("***Username***: " + username + "\n***Email***: ||" + c.email[em] + "||\n***Password***: ||" + c.password[em] + "||\n***Full Name***: ||" + c.firstname[em] + " " + c.lastname[em] + "||");
                        }
                    }
                }
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);
            }
        }
        public static bool creating;

        //Command for multi gen
        [Command("mg")]
        //Makes sure the user has one of the 3 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access")]
        public async Task mg(CommandContext ctx, string username, int amount)
        {
            janitor = false;
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1000576422928195697 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                if (amount > 15)
                {
                    //If the user tries to generate more than 14 accounts it will say this
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Please contact Law for generating 15 or more accounts`").ConfigureAwait(false);
                }
                else
                {                    
                    //This sends message telling the user an account is being created...
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Creating accounts, one moment...`").ConfigureAwait(false);

                    //calls the mavail method passing through the username string
                    await avail(username);

                    //Messages the user if any of the booleans were set to true...
                    if (baduser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is NOT available...`").ConfigureAwait(false);
                    }
                    else if (taken)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
                    }
                    else if (invalid)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                    }
                    else if (special)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                    }
                    else if (shortuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                    }
                    else if (longuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                    }
                    else if (noChar)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);
                    }
                    else if (manySpecial)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + username + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
                    }
                    else if (method)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);

                    }
                    else
                    {
                        //If available it will go through the mail and delete anyhting extra from mailbox...
                        delmail();

                        //Generates email and password
                        genmail(amount, ctx.Member, username);

                        //Creates local variable of index
                        int index = 0;

                        //navigates through each itteration of commandList
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            //Checks through commandList to see what index matches with the member that executed the command
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                //saves to index
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand z = commandList[index];

                        //Calls create method while passing through the username, itteration and callCommand struct
                        for (int i = 0; i < amount; i++)
                        {
                            create(username, i, z);
                        }
                        creating = true;

                        //Checks the inbox for the confirmation email...
                        checkinbox(amount, z);

                        //Gathers new index of callCommand list since the old one was removed and new one was added
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand c = commandList[index];
                        int veriy;

                        //Confirms the account creation
                        for (veriy = 0; veriy <= amount; veriy++)
                        {
                            confirm(veriy, amount, c);
                        }

                        //Defines discord config for DMing the user their data
                        var config = new DiscordConfiguration
                        {
                            Token = "TOKEN HERE",
                            TokenType = TokenType.Bot,
                            AutoReconnect = true,
                            MinimumLogLevel = LogLevel.Debug,
                            //UseInternalLogHandler = true,
                        };
                        Client = new DiscordClient(config);

                        //Defines public multi gen channel
                        var work = await Client.GetChannelAsync(1000576446462447756);

                        //Sends account info to the log channel incase there are any issues...
                        await work.SendMessageAsync(ctx.Message.Author.Mention + "has generated the following accounts:");

                        //Sends a message in the same channel telling the user the account has been created and has been sent to your DMs
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your accounts have been created and sent to the multigen logs channel.`").ConfigureAwait(false);
                        
                        //Loops through created accounts and DMs them individually to the user
                        for (int em = 0; em < amount; em++)
                        {
                            //Creates ADDED string for URI handler for the oculus account tools
                            string ADDED = "AccountSwapper://" + username + "/" + c.email[em] + "/" + c.password[em];

                            //Sends account info to the public multi gen channel incase there are any issues...
                            await work.SendMessageAsync("***Username***: " + username + "\n***Email***: ||" + c.email[em] + "||\n***Password***: ||" + c.password[em] + "||\n***Full Name***: ||" + c.firstname[em] + " " + c.lastname[em] + "||\n**Add Account**: ||" + $"<{ADDED}>||");
                        }
                    }
                    
                }
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);
            }
        }

        //Command for the remote reset
        [Command("rs")]
        [RequireRoles(RoleCheckMode.Any, "05 Council")]
        public async Task rs(CommandContext ctx)
        {
            //Verifies it's only in the admin channel...
            if(ctx.Message.Channel.Id == 1000296387080114216)
            {
                //Sends a message and starts another instance of the bot whilst exiting this one...
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Undestood, restarting bot now... Should be online in 10 seconds.`");
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
        }
        //Command for captial multi gen
        [Command("cmg")]
        //Makes sure the user has one of the 3 different roles listed below...
        [RequireRoles(RoleCheckMode.Any, "05 Council", "Level Omega", "Level 3 Access")]
        public async Task cmg(CommandContext ctx, int amount, string names)
        {
            //Ensuring the user is typing in the correct channel
            if (ctx.Message.Channel.Id == 1008411523791716472 | ctx.Message.Channel.Id == 1000296387080114216)
            {
                if (amount > 15)
                {
                    //If the user tries to generate more than 14 accounts it will say this
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Please contact Law for generating 15 or more accounts`").ConfigureAwait(false);
                }
                else
                {
                    //Splits the capitalized usernames by comma
                    string[] usernames = names.Split(',');

                    //This sends message telling the user an account is being created...
                    await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Creating accounts, one moment...`").ConfigureAwait(false);

                    //calls the mavail method passing through the username string
                    await avail(usernames[0]);

                    //Messages the user if any of the booleans were set to true...
                    if (baduser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `is NOT available...`").ConfigureAwait(false);
                    }
                    else if (taken)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username has been taken...`").ConfigureAwait(false);
                    }
                    else if (invalid)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + " ** `contains a character that is considered invalid, either a dash or some other invalid character...`").ConfigureAwait(false);
                    }
                    else if (special)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `begins or ends with some sort of special character, a user cannot begin nor end in this...`").ConfigureAwait(false);
                    }
                    else if (noChar)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `has no alhpabetical characters, please ensure it contains atleast one...`").ConfigureAwait(false);

                    }
                    else if (manySpecial)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `has consecutive special characters, please ensure it doesn't contain more than 1 special character in succession.`").ConfigureAwait(false);
                    }
                    else if (shortuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `is too short to be created, please input 3 or more characters...`").ConfigureAwait(false);
                    }
                    else if (longuser)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " **" + usernames[0] + "** `is too long to be created, please input 30 or less characters...`").ConfigureAwait(false);
                    }
                    else if (method)
                    {
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `This username is unavailable and requires a special method...`").ConfigureAwait(false);
                    }
                    else
                    {
                        //If available it will go through the mail and delete anyhting extra from mailbox...
                        delmail();

                        //Generates email and password
                        genmail(amount, ctx.Member, usernames[0]);

                        //Creates local variable of index
                        int index = 0;

                        //navigates through each itteration of commandList
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            //Checks through commandList to see what index matches with the member that executed the command
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                //saves to index
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand z = commandList[index];
                        z.cmgUsr = usernames.ToList();

                        //Calls create method while passing through the username, itteration and callCommand struct
                        for (int i = 0; i < amount; i++)
                        {
                            create(usernames[i], i, z);
                            creating = true;
                        }

                        //Checks the inbox for the confirmation email...
                        checkinbox(amount, z);

                        //Gathers new index of callCommand list since the old one was removed and new one was added
                        for (int j = 0; j < commandList.Count; j++)
                        {
                            callCommand d = commandList[j];
                            if (d.memeber == ctx.Member)
                            {
                                index = j;
                            }
                        }

                        //Sets local callCommand struct z to the commandList index that matches user input...
                        callCommand c = commandList[index];
                        int veriy;

                        //Confirms the account creation
                        for (veriy = 0; veriy <= amount; veriy++)
                        {
                            confirm(veriy, amount, c);
                        }

                        //Defines discord config for DMing the user their data
                        var config = new DiscordConfiguration
                        {
                            Token = "TOKEN HERE",
                            TokenType = TokenType.Bot,
                            AutoReconnect = true,
                            MinimumLogLevel = LogLevel.Debug,
                            //UseInternalLogHandler = true,
                        };
                        Client = new DiscordClient(config);

                        //Defines public captial multi gen channel
                        var work = await Client.GetChannelAsync(1008412805365514380);

                        //Sends account info to the public captial multi gen channel incase there are any issues...
                        await work.SendMessageAsync(ctx.Message.Author.Mention + "has generated the following accounts:");

                        //Sends a message in the same channel telling the user the account has been created and has been sent to your DMs
                        await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + " `Your accounts have been created and sent to the multigen logs channel.`").ConfigureAwait(false);

                        //Loops through created accounts and DMs them individually to the user
                        for (int em = 0; em < amount; em++)
                        {
                            //Creates ADDED string for URI handler for the oculus account tools
                            string ADDED = "AccountSwapper://" + usernames[em] + "/" + c.email[em] + "/" + c.password[em];

                            //Sends account info to the public multi gen channel incase there are any issues...
                            await work.SendMessageAsync("***Username***: " + c.cmgUsr[em] + "\n***Email***: ||" + c.email[em] + "||\n***Password***: ||" + c.password[em] + "||\n***Full Name***: ||" + c.firstname[em] + " " + c.lastname[em] + "||\n**Add Account**: ||" + $"<{ADDED}>||");
                        }
                    }
                    
                }
            }
            else
            {
                //If user calls command in wrong channel it deletes and tells them to use the propper channel...
                await ctx.Message.DeleteAsync();
                await ctx.Channel.SendMessageAsync(ctx.Message.Author.Mention + "`Please use the proper channel for this command...`").ConfigureAwait(false);
            }
        }
    }
}
