using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static discordbot1.SingleGen;
using Log = Serilog.Log;

namespace discordbot1
{
    internal class ConfirmLink
    { 
        //Confirm method has itteration, amount and callcommand struct being passed through
        public static async Task confirm(int i, int amount, callCommand c)
        {
            //Sets local strings
            string email = c.email[i];
            string confnum = c.cofnum[i];

            //Sets API request URL
            string url = "https://graph.oculus.com/confirm_email";

            //Defines data dictionary for POST request
            var data = new Dictionary<string, string>
            {
                { "email", email } ,
                {"confirmation_code", confnum },
                {"registered_from_app_id", "1592049031074901" },
                {"access_token", "OC|660728964057742|" },
                {"lsd", "AVo2MJ1JOpI" },
                {"jazoest", "2850" }
            };

            //Encodes the data dictionary
            var encodedData = new FormUrlEncodedContent(data);

            //Set's proxy for confirmation
            var client1 = new WebClient();

            //Proxy address
            client1.Proxy = new WebProxy("PROXY ADDRESS");

            //Proxy sign-in
            client1.Proxy.Credentials =
              new NetworkCredential("USERNAME", "PASSWORD");

            //Defines httpClient
            var client = new HttpClient();

            //Makes post request passing the API URL and the encodedData
            var response = await client.PostAsync(url, encodedData);

            using (HttpContent content = response.Content)
            {
                if (response.IsSuccessStatusCode)
                {
                    Log.Information("The account with the username " + c.usr + " with the email " + c.email + " has been created!");
                }
                else
                {
                    Log.Error("There was an error");
                    Log.Error(email);

                }
                string result = await content.ReadAsStringAsync();
                Log.Information(result);
            }
        }
    }
}
