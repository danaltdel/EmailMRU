using System;
using System.Collections.Generic;
using System.Collections;
//using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using System.Collections.ObjectModel;
namespace EmailMRU
{
    public class EmailList 
    {
        private int MAX_SIZE = 75;
        private ObservableCollection<Email> emailList = new ObservableCollection<Email>();

        public ObservableCollection<Email> Emails
        {
            get { return emailList; }
        }

        public void Add(MailItem mi)
        {
            Email newTop = new Email(mi);
            if (emailList.Contains(newTop))
            {
                emailList.Move(emailList.IndexOf(newTop), 0);
            }
            else
            {
                emailList.Insert(0, newTop);
            }

            while(emailList.Count > MAX_SIZE)
            {
                emailList.RemoveAt(emailList.Count - 1);
            }
        }
    }

    public class Email : IEquatable<Email>, IDisposable
    {
        public string ID { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public DateTime dateSent { get; set; }
        public bool hasAttachment { get; set; }
        public MailItem mail { get; set; }

        public Email(MailItem mi)
        {
            ID = mi.EntryID;
            from = mi.SenderName;
            subject = mi.Subject;
            mail = mi;
            dateSent=  mi.SentOn;
            //hasAttachment = mi.Attachments.Count != 0;
            hasAttachment = true;
        }

        public bool Equals(Email other)
        {
            return this.ID == other.ID;
        }

        /// <summary>
        /// Release COM reference to prevent holding onto shared Outlook resources
        /// </summary>
        public void Dispose()
        {
            mail = null;
        }
    }
}