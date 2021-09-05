// <copyright file="Start.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using FPKHotelreservierung.GuestUtil;
using FPKHotelreservierung.ReservationUtil;
using FPKHotelreservierung.RoomUtil;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Beim Programmstart aufgerufene Klasse mit Main-Void.
    /// </summary>
    public class Start
    {
        /// <summary>
        /// Konsolengrundeinstellungen und EventHandler setzen, Listen mit Daten aus .xml-Dateien füllen, Konsolenhauptmenü aufrufen.
        /// </summary>
        public static void Main()
        {
            // EventHandler für ProcessExit hinzufügen
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            // Encoding setzen, um Spezialzeichen wie zum Beispiel Währungszeichen korrekt darzustellen
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "FPK Hotelreservierung";
            Console.WriteLine("Herzlich Willkommen in der FPK Hotelreservierung");

            // Gespeicherte Daten per XML Deserialisierung laden
            Hotel.GuestList = XMLSerializer<Guest>.DeserializeList("GuestList.xml");
            Hotel.RoomTypeList = XMLSerializer<RoomType>.DeserializeList("RoomTypeList.xml");
            Hotel.RoomList = XMLSerializer<Room>.DeserializeList("RoomList.xml");
            Hotel.ReservationList = XMLSerializer<Reservation>.DeserializeList("ReservationList.xml");

            // Hauptmenü aufrufen
            new Hotel().ShowMainMenu();
        }

        /// <summary>
        /// EventHandler der beim Beenden des Prozesses aufgerufen wird. Speichern der Daten per XML-Serialisierung in .xml-Dateien.
        /// </summary>
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            XMLSerializer<Guest>.SerializeList(Hotel.GuestList, "GuestList.xml");
            XMLSerializer<RoomType>.SerializeList(Hotel.RoomTypeList, "RoomTypeList.xml");
            XMLSerializer<Room>.SerializeList(Hotel.RoomList, "RoomList.xml");
            XMLSerializer<Reservation>.SerializeList(Hotel.ReservationList, "ReservationList.xml");
            Console.WriteLine("Daten erfolgreich gespeichert.");
        }
    }
}
