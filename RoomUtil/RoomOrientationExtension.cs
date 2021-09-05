// <copyright file="RoomOrientationExtension.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

namespace FPKHotelreservierung.RoomUtil
{
    /// <summary>
    /// Klasse mit Methoden zur Umwandlung in und von das/dem enum RoomOrientation.
    /// </summary>
    public class RoomOrientationExtension
    {
        // Umwandlung eines RoomOrientation-Mitglieds in string.
        public static string ToString(RoomOrientation roomOrientation)
        {
            return roomOrientation switch
            {
                RoomOrientation.Street => "Straße",
                RoomOrientation.Valley => "Tal",
                RoomOrientation.Sea => "Meer",
                _ => null,
            };
        }

        // Umwandlung eines string in RoomOrientation-Mitglied.
        public static RoomOrientation ToRoomOrientation(string roomOrientationString)
        {
            return roomOrientationString switch
            {
                "Straße" => RoomOrientation.Street,
                "Tal" => RoomOrientation.Valley,
                _ => RoomOrientation.Sea,
            };
        }
    }
}
