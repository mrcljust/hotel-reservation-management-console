// <copyright file="RoomType.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

namespace FPKHotelreservierung.RoomUtil
{
    /// <summary>
    /// Die Klasse repräsentiert eine Zimmerkategorie.
    /// </summary>
    [System.Serializable]

    public class RoomType
    {
        public RoomType(string roomTypeName, uint maxPersons, string interiorDescription, decimal pricePerNight, decimal pricePerWeek)
        {
            this.RoomTypeName = roomTypeName;
            this.MaxPersons = maxPersons;
            this.InteriorDescription = interiorDescription;
            this.PricePerNight = pricePerNight;
            this.PricePerWeek = pricePerWeek;
        }

        public RoomType()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        public string RoomTypeName { get; set; }

        public uint MaxPersons { get; set; }

        public string InteriorDescription { get; set; }

        public decimal PricePerNight { get; set; }

        public decimal PricePerWeek { get; set; }

        // Überschreibung von ToString() zur Ausgabe eines benutzerfreundlichen Strings.
        public override string ToString()
        {
            return "Kategorie '" + this.RoomTypeName
                + "', max. Personen: " + this.MaxPersons
                + ", Ausstattung: " + this.InteriorDescription
                + ", Preis pro Nacht: " + UserInput.FormatPrice(this.PricePerNight)
                + ", Preis pro Woche: " + UserInput.FormatPrice(this.PricePerWeek);
        }

        // Benutzerfreundlicher String zur Anzeige von Infos beim Anlegen/Bearbeiten einer Reservierung.
        public string ShowInfoForReservation()
        {
            return " (Ausstattung: " + this.InteriorDescription
                + ", Preis pro Nacht: " + UserInput.FormatPrice(this.PricePerNight)
                + ", Preis pro Woche: " + UserInput.FormatPrice(this.PricePerWeek) + ")";
        }

        // Überschreibung von Equals(object obj) und GetHashCode() zum Vergleich von zwei RoomType.
        // Der RoomTypeName kann nur einmalig vergeben werden.
        public override bool Equals(object obj)
        {
            return this.RoomTypeName.Equals(((RoomType)obj).RoomTypeName);
        }

        public override int GetHashCode()
        {
            return this.RoomTypeName.GetHashCode();
        }
    }
}
