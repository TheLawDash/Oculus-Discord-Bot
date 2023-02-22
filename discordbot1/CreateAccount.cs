using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static discordbot1.SingleGen;

namespace discordbot1
{
    internal class CreateAccount
    {
        //Defines local booleans
        public static bool taken;
        public static bool baduser;
        public static bool invalid;
        public static bool longuser;
        public static bool shortuser;
        public static bool method;
        public static bool special;
        public static bool noChar;
        public static bool manySpecial;

        //Create method defined with username, itteration and callCommand struct being passed through
        public static async Task create(string username, int i, callCommand c)
        {
            //sets local variables for each aspect of the callCommand struct
            string email = c.email[i];
            string pswd = c.password[i];
            string firstname = c.firstname[i];
            string lastname = c.lastname[i];

            //sets public variables to false...
            taken = false;
            baduser = false;
            invalid = false;
            longuser = false;
            shortuser = false;
            method = false;
            special = false;
            noChar = false;
            manySpecial = false;

            //Defines API URL
            string url = "https://graph.oculus.com/register";

            //Defines data dictionary for POST request
            var data = new Dictionary<string, string>
            {
                { "alias", username},
                {"email", email},
                {"password", pswd},
                {"first_name", firstname},
                {"last_name", lastname},
                {"locale", "en_US"},
                {"accepted_tos_id", "2390881407618872"},
                {"login_redirect_uri", "null"},
                {"email_marketing_opt_in_status", "OPTED_OUT"},
                {"session_id", "MTY0ODc2NzUzMC7Ko9WyY8CWrqYBLt93EfQiPJmZWgk."},
                {"access_token", "OC|1592049031074901|"},
                {"lsd", "AVoCOsrrSoc"},
                {"jazoest", "21044"},
            };

            //Encodes data dictionary
            var encodedData = new FormUrlEncodedContent(data);

            //defines WebClient
            var client1 = new WebClient();

            //Sets proxy address
            client1.Proxy = new WebProxy("PROXY ADDRESS HERE");
            
            //Sets proxy username and email
            client1.Proxy.Credentials = new NetworkCredential("USERNAME", "PASSWORD");
            
            //Writes the IP address of POST request proxy
            Console.WriteLine(client1.DownloadString("IPADDRESS"));

            //Defines httpClientHandler and sets proxy
            var httpClientHandler = new HttpClientHandler()
            {
                    Proxy = client1.Proxy
            };

            //Defines client variable passing through the handler to ensure it uses the proxy to prevent API limitations
            var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            
            //Response is saved to local variable passing through the API URL and encodedData for the POST request
            var response = await client.PostAsync(url, encodedData);

            //If the itteration is == to the amount then it is no longer creating
            if (i == amount)
            {
                creating = false;
            }

            //Retrieves the API Response and writes it to console...
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }
    }
}

