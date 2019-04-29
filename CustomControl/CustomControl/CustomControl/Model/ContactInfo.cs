﻿using Xamarin.Forms;

namespace CustomControl
{
    public class ContactInfo
    {
        public ContactInfo(string Name)
        {
            ContactName = Name;
        }

        public string ContactName { get; set; }

        public string CallTime { get; set; }

        public string ContactImage { get; set; }

        public Color ContactImageColor { get; set; }

        public long ContactNumber { get; set; }
    }
}
