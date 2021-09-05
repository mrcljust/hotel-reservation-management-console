// <copyright file="UserInput.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Klasse mit Methoden zum Auslesen von Benutzereingaben aus der Konsole.
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// Fordert den Nutzer auf, einen String einzugeben, und gibt diesen zurück. Ggf. wird der vorher eingegebene Wert
        /// als Standardwert gesetzt, insofern vom User ein String bearbeitet wird.
        /// </summary>
        /// <param name="prompt">Ein Eingabeaufforderungstext.</param>
        /// <param name="previousValue">Ein zuvor für diese Eingabeaufforderung eingegebener Wert.</param>
        /// <param name="allowEmptyReturnString">Gibt an, ob der rückzugebende String leer sein darf oder nicht.</param>
        /// <returns>Der vom Nutzer eingegeben String.</returns>
        public static string GetString(string prompt, string previousValue, bool allowEmptyReturnString)
        {
            Console.Write(prompt + ": ");
            if (allowEmptyReturnString)
            {
                return ReadLine(previousValue);
            }

            string userInput;
            while (string.IsNullOrEmpty((userInput = ReadLine(previousValue)).Trim()))
            {
                Console.WriteLine("Leere Eingaben sind unzulässig, bitte tätigen Sie eine Eingabe.");
                Console.Write(prompt + ": ");
            }

            return userInput;
        }

        /// <summary>
        /// Stellt dem Nutzer eine Ja/Nein Frage und gibt die Antwort als bool zurück.
        /// </summary>
        /// <param name="question">Eine Frage.</param>
        /// <returns>Die Antwort des Nutzers als bool.</returns>
        public static bool GetYesNo(string question)
        {
            Console.Write(question + " (J/N): ");

            // pressedKey speichert die auf der Tastatur gedrückte Taste.
            ConsoleKey pressedKey;
            while ((pressedKey = Console.ReadKey(false).Key) != ConsoleKey.J && pressedKey != ConsoleKey.N)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Bitte geben Sie J (für ja) oder N (für nein) ein.");
                Console.Write(question + " (J/N): ");
            }

            Console.WriteLine(string.Empty);
            if (pressedKey == ConsoleKey.J)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fordert den Nutzer auf, eine ganze Zahl im uint-Bereich einzugeben, und gibt diese zurück.
        /// Ggf. wird der vorher eingegebene Wert als Standardwert gesetzt, insofern vom User ein Wert bearbeitet wird.
        /// </summary>
        /// <param name="prompt">Der Eingabeaufforderungstext.</param>
        /// <param name="previousValue">Der zuvor für diese Eingabeaufforderung eingegebene Wert.</param>
        /// <returns>Der vom Nutzer eingegebe Wert.</returns>
        public static uint GetUint(string prompt, uint? previousValue)
        {
            Console.Write(prompt + ": ");
            uint userInput;

            while (!uint.TryParse(ReadLine(previousValue.ToString()).Trim(), out userInput))
            {
                Console.WriteLine(prompt + " ungültig (nur ganze Zahlen erlaubt)!");
                Console.Write(prompt + ": ");
            }

            return userInput;
        }

        /// <summary>
        /// Fordert den Nutzer auf, eine (ggf. Dezimal-)Zahl im decimal-Bereich einzugeben, und gibt diese zurück.
        /// Ggf. wird der vorher eingegebene Wert als Standardwert gesetzt, insofern vom User ein Wert bearbeitet wird.
        /// </summary>
        /// <param name="prompt">Der Eingabeaufforderungstext.</param>
        /// <param name="previousValue">Der zuvor für diese Eingabeaufforderung eingegebene Wert.</param>
        /// <returns>Der vom Nutzer eingegebe Wert.</returns>
        public static decimal GetDecimal(string prompt, decimal? previousValue)
        {
            Console.Write(prompt + ": ");
            decimal userInput;

            while (!decimal.TryParse(ReadLine(previousValue.ToString()).Trim().Replace(".", ","), NumberStyles.Any, new CultureInfo("de-DE"), out userInput))
            {
                Console.WriteLine(prompt + " ungültig (nur (Dezimal)Zahlen erlaubt)!");
                Console.Write(prompt + ": ");
            }

            return userInput;
        }

        /// <summary>
        /// Fordert den Nutzer auf, eine Zahl im ulong-Bereich einzugeben, und gibt diese zurück.
        /// Ggf. wird der vorher eingegebene Wert als Standardwert gesetzt, insofern vom User ein Wert bearbeitet wird.
        /// </summary>
        /// <param name="prompt">Der Eingabeaufforderungstext.</param>
        /// <param name="previousValue">Der zuvor für diese Eingabeaufforderung eingegebene Wert.</param>
        /// <returns>Der vom Nutzer eingegebe Wert.</returns>
        public static ulong GetUlong(string prompt, ulong? previousValue)
        {
            Console.Write(prompt + ": ");
            ulong userInput;

            // Die Methode wird u.a. zur Angabe der Kreditkartennummer benutzt. Da die User diese ggf. durch
            // Leertasten getrennt eingibt, werden Leertasten entfernt.
            while (!ulong.TryParse(ReadLine(previousValue.ToString()).Trim().Replace(" ", string.Empty), out userInput))
            {
                Console.WriteLine(prompt + " ungültig (nur ganze Zahlen erlaubt)!");
                Console.Write(prompt + ": ");
            }

            return userInput;
        }

        /// <summary>
        /// Fordert den Nutzer auf, ein Datum im Format d.M.yyyy einzugeben, und gibt dieses zurück.
        /// Ggf. wird der vorher eingegebene Wert als Standardwert gesetzt, insofern vom User ein Wert bearbeitet wird.
        /// </summary>
        /// <param name="prompt">Der Eingabeaufforderungstext.</param>
        /// <param name="previousValue">Der zuvor für diese Eingabeaufforderung eingegebene Wert.</param>
        /// <returns>Der vom Nutzer eingegebe Wert.</returns>
        public static DateTime GetDate(string prompt, DateTime? previousValue)
        {
            string fullPrompt = prompt + " (Format DD.MM.YYYY)";
            Console.Write(fullPrompt + ": ");
            DateTime birthDate;

            string oldValueString;
            if (previousValue == null)
            {
                oldValueString = null;
            }
            else
            {
                DateTime oldValueDT = (DateTime)previousValue;
                oldValueString = oldValueDT.ToString("dd.MM.yyyy");
            }

            while (!DateTime.TryParseExact(ReadLine(oldValueString), "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
            {
                Console.WriteLine("Das eingegebene Datum ist unzulässig, bitte beachten Sie das Format.");
                Console.Write(fullPrompt + ": ");
            }

            return birthDate;
        }

        /// <summary>
        /// Fordert den Nutzer auf, ein Datum im Format MM/YY (Kreditkartengültigkeit) einzugeben, und gibt dieses zurück.
        /// Ggf. wird der vorher eingegebene Wert als Standardwert gesetzt, insofern vom User ein Wert bearbeitet wird.
        /// </summary>
        /// <param name="previousValue">Der zuvor für diese Eingabeaufforderung eingegebene Wert.</param>
        /// <returns>Der vom Nutzer eingegebe Wert.</returns>
        public static DateTime GetDateInValidUntilFormat(DateTime? previousValue)
        {
            string prompt = "Kreditkarte gültig bis (Format MM/YY)";
            Console.Write(prompt + ": ");
            DateTime dateMonth;

            string previousValueString;
            if (previousValue == null)
            {
                previousValueString = null;
            }
            else
            {
                DateTime oldValueDT = (DateTime)previousValue;

                // Ohne CultureInfo würde das / durch . ersetzt werden. Bei der Gültigkeit ist das / allerdings gewünscht.
                previousValueString = oldValueDT.ToString("MM/yy", new CultureInfo("en-US"));
            }

            while (!DateTime.TryParseExact(ReadLine(previousValueString), "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateMonth))
            {
                Console.WriteLine("Das eingegebene Datum ist unzulässig, bitte beachten Sie das Format.");
                Console.Write(prompt + ": ");
            }

            return dateMonth;
        }

        /// <summary>
        /// Zeigt einen Text mit der Bitte um Nutzerauswahl.
        /// </summary>
        /// <param name="whatToSelect">Was soll ausgewählt werden.</param>
        /// <param name="errorBefore">Wurde zuvor eine ungültige Wahl getroffen.</param>
        public static void ShowPleaseSelect(string whatToSelect, bool errorBefore)
        {
            Console.WriteLine(string.Empty);
            if (errorBefore)
            {
                Console.WriteLine("Sie haben eine ungültige Ziffer eingegeben!");
            }

            Console.WriteLine("Bitte wählen Sie aus, " + whatToSelect + ".");
            Console.WriteLine("Geben Sie dazu die jeweilige Ziffer ein und bestätigen Sie Ihre Eingabe mit der Enter-Taste.");
        }

        /// <summary>
        /// Formatiert decimal in einen string mit deutschem Währungszeichen und Format zur Ausgabe in der Konsole.
        /// </summary>
        /// <param name="price">Preis als decimal.</param>
        /// <returns>Formatierter Preis.</returns>
        public static string FormatPrice(decimal price)
        {
            return price.ToString("C", new CultureInfo("de-DE"));
        }

        /// <summary>
        /// Methode zum Einlesen von Benutzereingaben, erweitert für die Bearbeitung von Werten (Setzen von bearbeitbaren Standardwerten),
        /// die z.B. beim Editieren eines Gasts/Raums/... notwendig ist.
        /// </summary>
        /// <param name="defaultValue">Der Standardwert, der angezeigt werden und vom Nutzer bearbeitbar sein soll.</param>
        /// <returns>Den vom Nutzer eingegeben Wert.</returns>
        private static string ReadLine(string defaultValue)
        {
            /* Quelle: https://stackoverflow.com/questions/7565415/edit-text-in-c-sharp-console-application
             * answered Sep 27 '11 at 7:27 by Vasya
             * edited Feb 17 '15 at 22:00 by MisterHux */

            int pos = Console.CursorLeft;
            Console.Write(defaultValue);
            ConsoleKeyInfo info;
            List<char> chars = new List<char>();
            if (string.IsNullOrEmpty(defaultValue) == false)
            {
                chars.AddRange(defaultValue.ToCharArray());
            }

            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Backspace && Console.CursorLeft > pos)
                {
                    chars.RemoveAt(chars.Count - 1);
                    Console.CursorLeft -= 1;
                    Console.Write(' ');
                    Console.CursorLeft -= 1;
                }
                else if (info.Key == ConsoleKey.Enter)
                {
                    Console.Write(Environment.NewLine);
                    break;
                }

                // erweitert, dass auch Punkte akzeptiert werden
                else if (char.IsLetterOrDigit(info.KeyChar) || char.IsPunctuation(info.KeyChar) || char.IsWhiteSpace(info.KeyChar))
                {
                    Console.Write(info.KeyChar);
                    chars.Add(info.KeyChar);
                }
            }

            return new string(chars.ToArray());
        }
    }
}
