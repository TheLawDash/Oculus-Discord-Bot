using AE.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace discordbot1
{
    internal class fbcode
    {
        public static string codey;
        static bool done;
        public static bool stupid;
        public static string linky;
        public static async Task fbCode(string email)
        {
            done = false;
            stupid = false;

            //Connects to GMAIL account through an ImapClient
            ImapClient IC = new ImapClient("imap.gmail.com", "EMAIL ADDRESS", "PASSWORD", ImapClient.AuthMethods.Login, 993, true);
            
            //Selects the INBOX to look through
            IC.SelectMailbox("INBOX");

            //Gets the amount of messages from inbox and saves it to an integer
            int val = IC.GetMessageCount();

            //Foreach mail instance it will itterate through each...
            for (int i = val - 1; i > -0; i--)
            {
                //Gets message for each itteration
                MailMessage message = IC.GetMessage(i, false);

                //Gather's mail subject of email
                string subject = message.Subject;

                //Gather's who the message is to
                var to = message.To;

                //It will try parsing the subject and save to variable sub
                string[] sub = subject.Split(' ');

                //If the email is a Facebook email, sub[3] will have Facebook and will move into the if statement
                if (sub[3] == "Facebook")
                {
                    //Makes a MailAddress list from the to variable
                    List<System.Net.Mail.MailAddress> bleh = to.ToList();

                    //String variable addie is set
                    string addie = bleh[0].Address;

                    //Changes email to lowercase(address is case sensitive)
                    string emails = email.ToLower();

                    if (addie == emails)
                    {
                        //Sets string to body message
                        string body = message.Body;

                        //Splits by new line
                        string[] looking = body.Split('\n');

                        //Sets the code
                        linky = looking[2];
                        codey = looking[17];

                        //Deletes the email
                        IC.DeleteMessage(message);
                        done = true;
                        break;
                    }
                }
                Log.Information("Email not found...");
            }
            if (!done)
            {
                stupid = true;
            }
        }
    }
}
