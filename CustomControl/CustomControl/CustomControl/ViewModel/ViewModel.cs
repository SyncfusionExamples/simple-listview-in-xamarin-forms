using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CustomControl
{
    public class ContactsViewModel
    {
        public ObservableCollection<ContactInfo> Contacts { get; set; }

        public ContactsViewModel()
        {
            GenerateContactDetails();
            var random = new Random();
            Contacts = new ObservableCollection<ContactInfo>();

            foreach (var cusName in CustomerNames)
            {
                ContactInfo contact = new ContactInfo();
                contact.ContactName = cusName;
                contact.ContactImage = contact.ContactName.Substring(0, 1).ToUpper();
                contact.ContactImageColor = CustomerNameColors[random.Next(0, 4)];

                var time = (DateTime.Now.Hour - Contacts.Count) + ":" + random.Next(10, 59);
                contact.CallTime = (DateTime.Now.Hour - Contacts.Count) < 0 ? "Yesterday" : time;
                contact.ContactNumber = ContactNumbers[contact.ContactName];
                Contacts.Add(contact);
            }
        }

        #region Fields

        string[] CustomerNames = new string[] {
            "Kyle",
            "Gina",
            "Irene",
            "Katie",
            "Michael",
            "Oscar",
            "Ralph",
            "Torrey",
            "William",
            "Bill",
            "Daniel",
            "Frank",
            "Brenda",
            "Danielle",
            "Fiona",
            "Howard",
            "Jack",
            "Larry",
            "Holly",
            "Jennifer",
            "Liz",
            "Pete",
            "Steve",
            "Vince",
            "Zeke",
            "Aiden",
            "Jackson",
            "Mason",
            "Liam",
            "Jacob",
        };
        Color[] CustomerNameColors = new Color[] { Color.Blue, Color.Brown, Color.DarkGray, Color.Green, Color.Indigo };

        Dictionary<string, long> ContactNumbers = new Dictionary<string, long>();

        private void GenerateContactDetails()
        {
            ContactNumbers.Add("Kyle", 23564582);
            ContactNumbers.Add("Gina", 77656453);
            ContactNumbers.Add("Irene", 28363443);
            ContactNumbers.Add("Katie", 36563493);
            ContactNumbers.Add("Michael", 46563443);
            ContactNumbers.Add("Oscar", 34263923);
            ContactNumbers.Add("Ralph", 72560323);
            ContactNumbers.Add("Torrey", 82356835);
            ContactNumbers.Add("William", 82563903);
            ContactNumbers.Add("Bill", 97342091);
            ContactNumbers.Add("Daniel", 37364837);
            ContactNumbers.Add("Frank", 25372086);
            ContactNumbers.Add("Brenda", 20547286);
            ContactNumbers.Add("Danielle", 80635906);
            ContactNumbers.Add("Fiona", 98755453);
            ContactNumbers.Add("Howard", 83762683);
            ContactNumbers.Add("Jack", 93746384);
            ContactNumbers.Add("Larry", 37474934);
            ContactNumbers.Add("Holly", 36474638);
            ContactNumbers.Add("Jennifer", 63684629);
            ContactNumbers.Add("Liz", 27463873);
            ContactNumbers.Add("Pete", 28374649);
            ContactNumbers.Add("Steve", 84749373);
            ContactNumbers.Add("Vince", 28373937);
            ContactNumbers.Add("Zeke", 39387373);
            ContactNumbers.Add("Aiden", 37393728);
            ContactNumbers.Add("Jackson" , 29283733);
            ContactNumbers.Add("Mason", 37362822);
            ContactNumbers.Add("Liam", 28726937);
            ContactNumbers.Add("Jacob", 39283738);
        }

        #endregion
    }
}
