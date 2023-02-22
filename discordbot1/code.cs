using AE.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace discordbot1
{
    internal class code
    {
        public static string codey;
        static bool done;
        public static bool stupid;
        public static async Task OculusCode(string email)
        {
            done = false;
            while (!done)
            {
                //Connects to email via ImapClient
                ImapClient IC = new ImapClient("imap.gmail.com", "EMAIL", "PASSWORD", ImapClient.AuthMethods.Login, 993, true);

                //Gathers current ammount of emails
                var count = IC.GetMessageCount();

                //Selects inbox mailbox to pull emails from...
                IC.SelectMailbox("INBOX");

                //Itterates through the amount of messages
                for (var i = count - 1; i >= 0; i--)
                {
                    //Selects each message
                    var msg = IC.GetMessage(i, false);

                    //Saves subject to strin subject
                    string subject = msg.Subject;

                    //Gets the message to address
                    var to = msg.To;

                    //Changes email to lowercase(address is case sensitive)
                    string lowerEmail = email.ToLower();

                    //Checks subject for confirmation subject
                    if (subject == "Meta account login confirmation")
                    {
                        //Makes a MailAddress list from the to variable
                        List<System.Net.Mail.MailAddress> bleh = to.ToList();

                        //String variable addie is set
                        string addie = bleh[0].Address;

                        //Checks to make sure the email is for the generated email...
                        if (addie == lowerEmail)
                        {
                            //Set's string to body message
                            string body = msg.Body;
                            
                            //Splits by >
                            string[] looking = body.Split('>');

                            //Almos tthe code
                            string almost = looking[61];

                            //Splits by <
                            string[] further = looking[61].Split('<');

                            //Gathers code
                            codey = further[0];

                            //Deletes message
                            IC.DeleteMessage(msg);
                            done = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
