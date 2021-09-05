// <copyright file="Address.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

namespace FPKHotelreservierung.GuestUtil
{
    /// <summary>
    /// Die Klasse repräsentiert eine Adresse eines Gasts.
    /// </summary>
    [System.Serializable]

    public class Address
    {
        public Address(string street, string number, uint postalCode, string city, string country)
        {
            this.Street = street;
            this.Number = number;
            this.PostalCode = postalCode;
            this.City = city;
            this.Country = country;
        }

        public Address()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        // number ist vom Typ string, da eine Hausnummer auch einen Buchstabenzusatz enthalten kann.
        public string Street { get; set; }

        public string Number { get; set; }

        public uint PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        // Überschreibung von ToString() zur Ausgabe eines benutzerfreundlichen Strings
        public override string ToString()
        {
            return this.Street + " " + this.Number + ", " + this.PostalCode + " " + this.City + ", " + this.Country;
        }
    }
}
