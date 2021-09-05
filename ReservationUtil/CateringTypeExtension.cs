// <copyright file="CateringTypeExtension.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;

namespace FPKHotelreservierung.ReservationUtil
{
    /// <summary>
    /// Klasse mit Methoden zur Umwandlung in und von das/dem enum CateringType.
    /// </summary>
    public class CateringTypeExtension
    {
        // Umwandlung eines CateringType-Mitglieds in string.
        public static string ToString(CateringType cateringType)
        {
            return cateringType switch
            {
                CateringType.None => "Keine Verpflegung",
                CateringType.Breakfast => "Frühstück",
                CateringType.HalfBoard => "Halbpension",
                _ => null,
            };
        }

        // Umwandlung eines string in CateringType-Mitglied.
        public static CateringType ToCateringType(string cateringTypeString)
        {
            return cateringTypeString switch
            {
                "Keine Verpflegung" => CateringType.None,
                "Frühstück" => CateringType.Breakfast,
                "Halbpension" => CateringType.HalfBoard,
                _ => CateringType.HalfBoard,
            };
        }

        // Preis eines CateringType-Mitglieds berechnen
        public static decimal CalcCateringTypePricePerDay(CateringType cateringType)
        {
            return cateringType switch
            {
                CateringType.None => 0,
                CateringType.Breakfast => 5,
                CateringType.HalfBoard => 10,
                _ => 0,
            };
        }

        // Benutzerfreundlicher String zur Anzeige von Infos beim Anlegen/Bearbeiten einer Reservierung.
        public static string ShowInfoForReservation(CateringType cateringType, uint reservationPeriodInDays)
        {
            // Anzeigen des Preis pro Tages und des Preises im gewählten Zeitraum
            return ToString(cateringType)
                + " (Preis pro Nacht: " + UserInput.FormatPrice(CalcCateringTypePricePerDay(cateringType))
                + ", Preis im gewählten Zeitraum: " + UserInput.FormatPrice(reservationPeriodInDays
                * CalcCateringTypePricePerDay(cateringType)) + ")";
        }
    }
}
