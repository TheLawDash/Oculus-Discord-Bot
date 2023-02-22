using System;
using System.Collections.Generic;
using RandomNameGeneratorLibrary;
using static discordbot1.SingleGen;
using Serilog;
using DSharpPlus.Entities;


namespace discordbot1
{
    internal class gemail
    {
        //Genmail method having the amount of accounts, discord Member, and username being created...
        public static void genmail(int amount, DiscordMember member, string username)
        {
            //Defines local list variables
            List<string> emails = new List<string>();
            List<string> passwords = new List<string>();
            List<string> firstname = new List<string>();
            List<string> lastname = new List<string>();
            //Defines local random class
            Random rd = new Random();

            //Defines local personNameGenerator class to generate common names instead of using a text file...
            var personGenerator = new PersonNameGenerator();

            //Defines local string array with the length of amount
            string[] email = new string[amount];
            string[] pass = new string[amount];
            string[] firstn = new string[amount];
            string[] second = new string[amount];

            //For loop to create the correct amount of emails and passwords
            for (int i = 0; i < amount; i++)
            {
                //Uses personNameGenerator to generate a random first name and last name...
                var names = personGenerator.GenerateRandomMaleFirstAndLastName();

                //Splits the names string by space to get first and last name
                string[] firstLast = names.Split(' ');

                //Sets firstname and last name varibales...
                firstn[i] = (firstLast[0]);
                second[i] = (firstLast[1]);

                //String of special characters being used for generated passwords
                string[] special = { "?", "!", "#", "*", "$" };

                //Generates email by Concatenating the first name and last name with a random number between 1 and 500000 to ensure no same email is used
                //Adds a @projectmayhem.xyz domain at the end to re-direct emails...
                email[i] = (firstLast[0] + firstLast[1] + rd.Next(1, 500000) + "@projectmayhem.xyz");

                //Generates a password string by using the lastname with a random special character, then adding a number between 5000 and 969696
                pass[i] = (firstLast[1] + special[rd.Next(0, special.Length)] + rd.Next(5000, 969696));

                //Logs this information incase the bot breaks or loses power so I can retrieve the User's account
                Log.Information(email[i]);
                Log.Information(pass[i]);

                //Adds to the local list
                emails.Add(email[i]);
                passwords.Add(pass[i]);
                firstname.Add(firstn[i]);
                lastname.Add(second[i]);
            }
            //Creates a local instance of the callCommand structure 
            callCommand c = new callCommand();
            c.memeber = member;
            c.DiscordUsername = member.Username;
            c.usr = username;
            c.email = emails;
            c.password = passwords;
            c.firstname = firstname;
            c.lastname = lastname;
            c.amount = amount;

            //Adds the local instance to the public list -- commandList to ensure that multiple commands can be run at once without overwritting different public variables...
            commandList.Add(c);
        }
    }
}
