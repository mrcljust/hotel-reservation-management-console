// <copyright file="UserChoice.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using FPKHotelreservierung.GuestUtil;
using FPKHotelreservierung.ReservationUtil;
using FPKHotelreservierung.RoomUtil;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Klasse mit Methoden zum Auslesen der Benutzerauswahl.
    /// </summary>
    /// <typeparam name="T">Der Typ, zu dem eine Auswahl getroffen werden soll.</typeparam>
    public class UserChoice<T>
    {
        /// <summary>
        /// Member aus Enum auswählen lassen.
        /// </summary>
        /// <param name="enumType">Typ des Enums.</param>
        /// <param name="prompt">Eingabe-/Auswahlaufforderung.</param>
        /// <param name="reservationPeriodInDays">Bei Auswahl von CateringType, Reservierungsdauer in Tagen.</param>
        /// <returns>Den ausgewählten Member des Enums.</returns>
        public static T ChooseFromEnum(Type enumType, string prompt, uint? reservationPeriodInDays)
        {
            Console.WriteLine(string.Empty);

            int typeId = 1;
            foreach (T enumValue in Enum.GetValues(enumType))
            {
                if (enumType == typeof(CateringType))
                {
                    Console.WriteLine(typeId++ + " - " + CateringTypeExtension.ShowInfoForReservation((CateringType)Convert.ChangeType(enumValue, typeof(CateringType)), (uint)reservationPeriodInDays));
                }
                else if (enumType == typeof(CreditCardType))
                {
                    Console.WriteLine(typeId++ + " - " + enumValue.ToString());
                }
                else if (enumType == typeof(RoomOrientation))
                {
                    Console.WriteLine(typeId++ + " - " + RoomOrientationExtension.ToString((RoomOrientation)Convert.ChangeType(enumValue, typeof(RoomOrientation))));
                }
            }

            UserInput.ShowPleaseSelect(prompt, false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > Enum.GetNames(enumType).Length || userInputId < 1)
            {
                // Ungültige Eingabe
                UserInput.ShowPleaseSelect(prompt, true);
            }

            int selectedTypeId = --userInputId;
            T selectedCateringType = (T)Enum.ToObject(enumType, selectedTypeId);
            return selectedCateringType;
        }
    }
}
