// <copyright file="Reservation.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using FPKHotelreservierung.GuestUtil;
using FPKHotelreservierung.RoomUtil;

namespace FPKHotelreservierung.ReservationUtil
{
    /// <summary>
    /// Die Klasse repräsentiert eine Hotelreservierung.
    /// </summary>
    [Serializable]

    public class Reservation
    {
        public Reservation(Guest guest, RoomType roomType, Room room, uint personCount, DateTime arrival, DateTime departure, CateringType cateringType)
        {
            this.Guest = guest;
            this.RoomType = roomType;
            this.Room = room;
            this.PersonCount = personCount;
            this.ArrivalDate = arrival;
            this.DepartureDate = departure;
            this.PeriodInDays = (uint)(departure - arrival).TotalDays;
            this.CateringType = cateringType;
            this.Price = CalculatePrice(roomType, this.PeriodInDays, this.CateringType);
        }

        public Reservation()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        public Guest Guest { get; set; }

        public RoomType RoomType { get; set; }

        public Room Room { get; set; }

        public uint PersonCount { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public uint PeriodInDays { get; set; }

        public CateringType CateringType { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Methode zum Berechnen des Gesamtpreises bei einer gegebenen Aufenthaltsdauer anhand des Room- und CateringTypes.
        /// </summary>
        /// <param name="roomType">Der RoomType des Aufenthalts.</param>
        /// <param name="stayInDays">Die Aufenthaltsdauer in Tagen.</param>
        /// <param name="cateringType">Der CateringType.</param>
        /// <returns>Gesamtpreis des Aufenthalts.</returns>
        public static decimal CalculatePrice(RoomType roomType, uint stayInDays, CateringType cateringType)
        {
            uint weeksInPeriod = stayInDays / 7;
            uint daysInPeriod = stayInDays % 7;
            decimal cateringPrice = stayInDays * CateringTypeExtension.CalcCateringTypePricePerDay(cateringType);

            return (roomType.PricePerWeek * weeksInPeriod) + (roomType.PricePerNight * daysInPeriod) + cateringPrice;
        }

        // Überschreibung von ToString() zur Ausgabe eines benutzerfreundlichen Strings.
        public override string ToString()
        {
            return "Reservierung von "
                   + this.ArrivalDate.ToString("dd.MM.yyyy") + " bis " + this.DepartureDate.ToString("dd.MM.yyyy")
                   + ", Gast " + this.Guest.GetFullName() + " (" + this.PersonCount + " Personen)"
                   + ", Zimmer '" + this.Room.RoomNumber + "' (" + this.RoomType.RoomTypeName + ", " + this.Room.RoomSize + "m2), "
                   + CateringTypeExtension.ToString(this.CateringType)
                   + ", Kosten: " + UserInput.FormatPrice(this.Price);
        }
    }
}
