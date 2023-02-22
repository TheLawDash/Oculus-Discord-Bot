using System;
using System.Collections.Generic;
using System.Linq;
using AE.Net.Mail;
using static discordbot1.SingleGen;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics.Metrics;
using Serilog;

namespace discordbot1
{
    internal class CheckInbox
    { 
        //Defines three private variables
        private static bool found;
        private static string almost;
        private static int num = 0;
        public static void checkinbox(int amount, callCommand c)
        {
            //Sets local integer val for later...
            int val;

            //Sets found to false until the email has been found
            found = false;
            
            //Connects to email via ImapClient
            ImapClient IC = new ImapClient("imap.gmail.com", "EMAIL ADDRESS HERE", "PASSWORD HERE", ImapClient.AuthMethods.Login, 993, true);

            //Gathers current ammount of emails
            int current = IC.GetMessageCount();

            //Defines 3 local string arrays with the count of amount...
            string[] confnu = new string[amount];
            string[] conflin = new string[amount];
            string[] email = new string[amount];

            //While loop to search for confirmation emails
            while (!found)
            {
                //Selects inbox mailbox to pull emails from...
                IC.SelectMailbox("INBOX");

                //Sets val to Inbox Message Count
                val = IC.GetMessageCount();

                //Checks to see if the correct amount of emails have been retrieved
                if (val >= amount - current)
                {
                    //Sends Success Message
                    Log.Information("Gotem All!");

                    //Itterates through the amount of messages
                    for(int i = val - 1; i>-0; i--)
                    {
                        //Selects each message
                        var message = IC.GetMessage(i, false);

                        //Gets the message to address
                        var to = message.To;

                        //Saves subject to strin subject
                        string subject = message.Subject;

                        //Checks subject for confirmation subject
                        if(subject == "Please confirm your email address")
                        {
                            //Makes a MailAddress list from the to variable
                            List<System.Net.Mail.MailAddress> bleh = to.ToList();

                            //String variable addie is set
                            string addie = bleh[0].ToString();

                            //Itterates through each email in the call Command structure
                            foreach (string s in c.email)
                            {
                                //Changes email to lowercase(address is case sensitive)
                                string lowermail = s.ToLower();

                                //Checks to make sure the email is for the generated email...
                                if (addie == lowermail)
                                {
                                    //Set's index of the email
                                    int index = c.email.IndexOf(s);

                                    //Set's string to body message
                                    string body = message.Body;

                                    //Creates character array to 
                                    string[] splitit = body.Split('"');

                                    //Sets the string of confirmation link
                                    conflin[num] = splitit[193];

                                    //Splits conflin variable by '='
                                    string[] thelink = conflin[num].Split('=');

                                    //Splits thelink variable by '&'
                                    string[] conf = thelink[5].Split('&');

                                    //Defines what to split by
                                    Char[] at = { '4', '0' };

                                    //Splits thelink variable by '%'
                                    string[] address = thelink[4].Split('%');

                                    //Splits address variable by at
                                    string[] addressbetter = address[1].Split(at);
                                    
                                    //Splits addressbetter variable by '&'
                                    string[] strings = addressbetter[2].Split('&');
                                    try
                                    {
                                        almost = address[0] + "@" + strings[0];
                                    }
                                    catch
                                    {
                                        almost = address[0] + "@" + strings[0];
                                    }
                                    //Takes away the ambersand
                                    string[] noamp = almost.Split('&');
                                    email[num] = noamp[0];

                                    //Sets the confirmation code
                                    confnu[index] = conf[0];
                                    Log.Information("The email is " + s + " and the confirmation code is " + confnu[index]);
                                    IC.DeleteMessage(message);
                                }
                            }
                        }
                    }
                    //Once all emails are found it sets the found bool to true
                    found = true;

                    //Creates local callCommand struct
                    callCommand d = new callCommand();

                    //Sets each variable in callCommand
                    d.cofnum = confnu.ToList();
                    d.memeber = c.memeber;
                    d.DiscordUsername = c.DiscordUsername;
                    d.usr = c.usr;
                    d.email = c.email;
                    d.password = c.password;
                    d.firstname = c.firstname;
                    d.lastname = c.lastname;
                    d.amount = c.amount;
                    //Removes old itteration of the callcommand from the list
                    commandList.Remove(c);
                    
                    //Adds the callCommand to the list...
                    commandList.Add(d);
                }
                else
                {
                    //Sends message saying how many emails are still required...
                    Thread.Sleep(2000);
                    int currentAmount = val - current;
                    Console.WriteLine("Not here yet...");
                    Console.WriteLine("Have " + currentAmount + "... Need " + (amount - currentAmount) + " more emails...");
                }
                
            }
        }   
    }
}
