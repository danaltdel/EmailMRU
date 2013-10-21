using System;
using System.Collections.Generic;
using System.Collections;
//using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
namespace UIPrototype
{
    public class EmailList 
    {
        private int MAX_SIZE = 15;
        public LinkedList<Email> emails = new LinkedList<Email>();
        public void Add(Email mi)
        {
            Email newTop = mi;
            if (emails.Contains(newTop))
            {
                emails.Remove(newTop);
            }

            emails.AddFirst(newTop);

            if (emails.Count > MAX_SIZE)
            {
                while (emails.Count > MAX_SIZE)
                {
                    var old = emails.Last;
                    emails.RemoveLast();
                    old.Value.Dispose();
                }
            }

            Add2(mi);
        }

        private void Add2(Email mi)
        {
            Email newTop = mi;
            if (emails2.Contains(newTop))
            {
                emails2.Move(emails2.IndexOf(newTop), 0);
            }
            else
            {
                emails2.Insert(0, newTop);
            }

            if (emails.Count > MAX_SIZE)
            {
                while (emails.Count > MAX_SIZE)
                {
                    int indexOfLast = emails2.Count - 1;
                    var old = emails2[indexOfLast];
                    emails2.RemoveAt(indexOfLast);
                    old.Dispose();
                }
            }
        }

        private ObservableCollection<Email> emails2 = new ObservableCollection<Email>();

        public ObservableCollection<Email> Emails
        {
            get { return emails2; }
        }
    }

    public class Email : IEquatable<Email>, IDisposable
    {
        public string ID { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public DateTime dateSent { get; set; }
        public bool hasAttachment { get; set; }

        public Email()
        {            
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", from, subject);
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
        }
    }
}