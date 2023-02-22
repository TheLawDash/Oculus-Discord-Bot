using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static discordbot1.SingleGen;
using static discordbot1.CreateAccount;
using Serilog;

namespace discordbot1
{
    internal class Availability
    {
        public static bool available;
        public static async Task avail(string username)
        {
            //Resets public bools
            taken = false;
            baduser = false;
            invalid = false;
            longuser = false;
            shortuser = false;
            method = false;
            special = false;
            available = false;
            manySpecial = false;
            //Defining content string and API url...
            string content;
            string url = "https://graph.oculus.com/user_checks_by_alias?alias=" + username + "&locale=en_US&access_token=OC%7C1592049031074901%7C";
            //Defines HttpClient
            var client = new HttpClient();

            //Makes GET HTTPS reuqest to Meta(Oculus) API
            var response = await client.GetAsync(url);

            //Sends results to string content
            content = await response.Content.ReadAsStringAsync();
            try
            {
                //Parsing through Json response to see if available...
                string[] parsedAPI = content.Split('"');
                //Checks for each possible itteration
                if (parsedAPI[19] == "Username Already Taken")
                {
                    taken = true;
                }
                if (parsedAPI[19] == "Username Is Unavailable")
                {
                    baduser = true;
                }
                if (parsedAPI[19] == "Username Is Too Short")
                {
                    shortuser = true;
                }
                if (parsedAPI[19] == "Username Is Too Long")
                {
                    longuser = true;
                }
                if (parsedAPI[19] == "Username Has Invalid Characters")
                {
                    invalid = true;
                }
                if (parsedAPI[19] == "Unavailable Username")
                {
                    method = true;
                }
                if (parsedAPI[19] == "Username Can't Start or End With a Special Character")
                {
                    special = true;
                }
                if (parsedAPI[19] == "Username Has No Letters")
                {
                    noChar = true;
                }
                if (parsedAPI[19] == "Username Can't Have Consecutive Special Characters")
                {
                    manySpecial = true;
                }
            }
            catch(Exception)
            {
                //If the parse fails or breaks it means the name is available...
                available = true;
            }
            //Logs response
            Log.Information(content.ToString());
            
        }


    }
}
