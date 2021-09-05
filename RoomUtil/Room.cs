// <copyright file="Room.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

namespace FPKHotelreservierung.RoomUtil
{
    /// <summary>
    /// Die Klasse repräsentiert ein Zimmer.
    /// </summary>
    [System.Serializable]

    public class Room
    {
        public Room(string roomNumber, uint roomSize, RoomType roomType, RoomOrientation roomOrientation)
        {
            this.RoomNumber = roomNumber;
            this.RoomSize = roomSize;
            this.RoomType = roomType;
            this.RoomOrientation = roomOrientation;
        }

        public Room()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        // Die Zimmernummer soll auch aus Buchstaben bestehen können.
        public string RoomNumber { get; set; }

        public uint RoomSize { get; set; }

        public RoomOrientation RoomOrientation { get; set; }

        public RoomType RoomType { get; set; }

        // Überschreibung von ToString() zur Ausgabe eines benutzerfreundlichen Strings.
        public override string ToString()
        {
            return "Zimmernummer " + this.RoomNumber
                + ", Größe " + this.RoomSize + "m2, "
                + "Ausrichtung " + RoomOrientationExtension.ToString(this.RoomOrientation)
                + ", Zimmerkategorie " + this.RoomType.RoomTypeName;
        }

        // Überschreibung von Equals(object obj) und GetHashCode() zum Vergleich von zwei Room.
        // Die RoomNumber kann nur einmalig vergeben werden.
        public override bool Equals(object obj)
        {
            return this.RoomNumber.Equals(((Room)obj).RoomNumber);
        }

        public override int GetHashCode()
        {
            return this.RoomNumber.GetHashCode();
        }
    }
}
