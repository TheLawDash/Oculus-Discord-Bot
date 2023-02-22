using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static discordbot1.CreateAccount;
using System.IO.Compression;
using System.IO;
using Serilog;

namespace discordbot1
{
    internal class metaavail
    {
        public static bool available;
        public static async Task mavail(string username)
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
            if (username.Length <= 20)
            {
                //Variables string for API being passed
                string variables = "{" + '"' + "action_source" + '"' + ":" + '"' + "BYPASS_OCULUS_COOLDOWN_ENTRYPOINT_IKNOWWHATIMDOING" + '"' + "," + '"' + "identity_ids" + '"' + ":[]," + '"' + username + '"' + ":" + '"' + "ughadfsaf" + '"' + "," + '"' + "included_app_validations" + '"' + ":[" + '"' + "OCULUS" + '"' + "]}";
                //API URL
                string url = "https://quest.meta.com/api/graphql?";
                //Defining dictionary for the POST request (Some variables cause the response to break as they are not static... current avail method works fine so this is currently abandoned...)
                var data = new Dictionary<string, string>
                {
                    {"av", "108923888614023"},
                    {"__user", "0"},
                    {"__a", "1"},
                    {"__dyn", "7xeUmwlE7ibwKBWo2vwAxu13wqovzEdEc8co2qwJw5ux60Vo1upE4W0OE2WxO0FE2awt81sbzo5-0Bo7O2l0Fwqo31wnEfo5m1mxe6E7e58jwGzE2swwwNwKwHw8Xwn82Lx-0iS2S3qazo11E2ZwiU8U9E4a1pg"},
                    {"__csr", "h2eaqFOUWqUTyucxKiaKuLHzEcopwzDwQghx28yV8y9xC5ry449-4-uA-5UtwDxeQ3GUG78-6Utg-2Sm4ErwIwGzvxrwFy80BSm022603_i0ej-1uw0ary07m86O0eBx11fbQ3W02fO07V80J901Gi0ez80IE0FS0e6U04Ja05JU8o"},
                    {"__req", "0"},
                    {"__hs", "19233.HYP:comet_loggedout_pkg.2.0.0.0.0"},
                    {"dpr", "1"},
                    {"__ccg", "EXCELLENT"},
                    {"__rev", "1006108443."},
                    {"__s", "w1hf84:y27zwt:jnlr9w"},
                    {"__hsi", "7137353732104139120"},
                    {"__comet_req", "1"},
                    {"fb_dtsg", "NAcPaAanSEdGTc2eT0Yw-Rra_NjQL96xnv9BMRbA-RGKKJXqEoo7R5g:26:1661794662"},
                    {"jazoest", "25321"},
                    {"lsd", "OwxuNk2DMZsWFGSUMd4iTK"},
                    {"__spin_r", "1006108443"},
                    {"__spin_b", "trunk"},
                    {"__spin_t", "1661794663"},
                    {"fb_api_caller_class", "RelayModern"},
                    {"fb_api_req_friendly_name", "useFXIMUsernameValidatorBaseQuery"},
                    {"variables", variables},
                    {"server_timestamps", "true"},
                    {"doc_id", "5147530792026571"},
                };
                //urlEncoding the dictionary...
                var encodedData = new FormUrlEncodedContent(data);

                //Defining webclient for proxy to prevent API limitation
                var client1 = new WebClient();

                client1.Proxy = new WebProxy("PROXY ADDRESS HERE");
                client1.Proxy.Credentials =
                  new NetworkCredential("USERNAME HERE", "PASSWORD HERE");

                //Creates HTTP handler to use the proxy address...
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = client1.Proxy
                };

                //Defining the HTTPClient using the httpClientHandler
                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);

                //Adding headers to emulate webbrowser user
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                client.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
                client.DefaultRequestHeaders.Add("Authroity", "https://quest.meta.com");
                client.DefaultRequestHeaders.Add("Referer", "https://quest.meta.com/profile_setup/nux/?redirect_uri=https%3A%2F%2Fauth.oculus.com%2Flogin%2F%3Fredirect_uri%3Dhttps%253A%252F%252Fsecure.oculus.com%252Fmy%252Fprofile%252F%253Ffrlaccountid%253D106559215532196");
                client.DefaultRequestHeaders.Add("X-FB-Friendly-Name", "useFXIMUsernameValidatorBaseQuery");
                client.DefaultRequestHeaders.Add("X-FB-LSD", "SyDCkJcmmdMcFj_napXbT3");
                client.DefaultRequestHeaders.Add("Cookie", "locale=en_US; datr=GcsHYyvU2NP2Q-26oCJ2zL7T; fs=FpzJsYnIujAWPhgOaWJ4Yk9aLVlXLXA4a1EWqMjKsQwA");

                //client.DefaultRequestHeaders.Add();

                //Recording POST HTTPs request passing the URL and encodedData expecting a 200 response
                var response = await client.PostAsync(url, encodedData);

                //Saves response to varContents...
                var contents = response.Content.ReadAsByteArrayAsync().Result;
                
                //Receiving encrypted string, attempting to decompress using Gzip
                using (var compressedStream = new MemoryStream(contents))
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                using (var resultStream = new MemoryStream())
                {
                    zipStream.CopyTo(resultStream);
                    var results1 = resultStream.ToArray();
                    string utfString = Encoding.UTF8.GetString(results1, 0, results1.Length);
                }
                using (HttpContent content = response.Content)
                {
                    //Expected result from request (Never got a 200)
                    string result = await content.ReadAsStringAsync();
                    Log.Information(result);
                    try
                    {
                        string[] shambles = result.Split('"');
                        if (shambles[19] == "Username Already Taken")
                        {
                            taken = true;

                        }
                        if (shambles[19] == "Username Is Unavailable")
                        {
                            baduser = true;
                        }
                        if (shambles[19] == "Username Is Too Short")
                        {
                            shortuser = true;
                        }
                        if (shambles[19] == "Username Is Too Long")
                        {
                            longuser = true;
                        }
                        if (shambles[19] == "Username Has Invalid Characters")
                        {
                            invalid = true;
                        }
                        if (shambles[19] == "Unavailable Username")
                        {
                            method = true;
                        }
                        if (shambles[19] == "Username Can't Start or End With a Special Character")
                        {
                            special = true;
                        }
                    }
                    catch (Exception)
                    {
                        available = true;
                    }

                    Log.Information(content.ToString());
                }
            }
            else
            {
                longuser = true;
            }
        }
    }
}