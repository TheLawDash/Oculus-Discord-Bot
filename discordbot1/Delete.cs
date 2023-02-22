using AE.Net.Mail;
using Serilog;
using System;

namespace discordbot1
{
    internal class Delete
    {
        static ImapClient IC;

        public static void delmail()
        {
            try
            {
                //Connects to GMAIL account through an ImapClient
                ImapClient IC = new ImapClient("imap.gmail.com", "EMAIL ADDRESS HERE", "APP PASSWORD HERE", ImapClient.AuthMethods.Login, 993, true);
                
                //Gets the amount of messages from inbox and saves it to an integer
                int count = IC.GetMessageCount();
                
                //Selects the INBOX to look through
                IC.SelectMailbox("INBOX");

                //Foreach mail instance it will itterate through each...
                for (var i = count - 1; i >= 0; i--)
                {
                    //Gets message for each itteration
                    var msg = IC.GetMessage(i, false);
                    
                    //Gather's mail subject of email
                    string subject = msg.Subject;
                    
                    //Gather's who the message is to
                    var to = msg.To;
                    try
                    {
                        //It will try parsing the subject and save to variable sub
                        string[] sub = subject.Split(' ');

                        //If the email is a confimation email it skips incase it's needed or is currently in use
                        if (subject == "Oculus - Login confirmation" || subject == "Please confirm your email address" || sub[3] == "Facebook")
                        {
                            Log.Information("Required mail deletion skipped...");
                        }
                        else
                        {
                            //If it isn't a confirmation email it will delete the message...
                            IC.DeleteMessage(msg);
                        }
                    }
                    catch
                    {
                        Log.Information("Subject doesn't have a space in it...");
                        IC.DeleteMessage(msg);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
