// <copyright file="Guest.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;

namespace FPKHotelreservierung.GuestUtil
{
    /// <summary>
    /// Die Klasse repräsentiert einen Hotelgast.
    /// </summary>
    [Serializable]

    public class Guest
    {
        public Guest(string forename, string surname, Address address, DateTime birthDate, CreditCard creditCard)
            : this(forename, surname, address, birthDate)
        {
            this.CreditCard = creditCard;
        }

        public Guest(string forename, string surname, Address address, DateTime birthDate)
        {
            this.Forename = forename;
            this.Surname = surname;
            this.Address = address;
            this.BirthDate = birthDate;
        }

        public Guest()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        public string Forename { get; set; }

        public string Surname { get; set; }

        public Address Address { get; set; }

        public DateTime BirthDate { get; set; }

        public CreditCard CreditCard { get; set; }

        // Überschreibung von ToString() zur Ausgabe eines benutzerfreundlichen Strings.
        public override string ToString()
        {
            if (this.CreditCard == null)
            {
                return this.GetFullName()
                    + ", geb. " + this.BirthDate.ToString("dd.MM.yyyy")
                    + ", wohnhaft " + this.Address.ToString()
                    + ", keine Kreditkarte";
            }

            return this.GetFullName()
                + ", geb. " + this.BirthDate.ToString("dd.MM.yyyy")
                + ", wohnhaft " + this.Address.ToString()
                + ", Kreditkarte " + this.CreditCard.ToString();
        }

        public string GetFullName()
        {
            return this.Forename + " " + this.Surname;
        }
    }
}
