// <copyright file="NumericMenu.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Klasse mit Methoden zum Anzeigen von Konsolen-Auswahlmenüs.
    /// </summary>
    public class NumericMenu
    {
        private const string Seperator = "--------------------------------------------------------------------------------------------------";
        private readonly NumericMenuEntry[] menuEntries;
        private readonly NumericMenuEntry menuTitle;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericMenu"/> class.
        /// Initialisiert ein neues NumericMenu.
        /// </summary>
        /// <param name="menuTitle">Titel des Menüs.</param>
        /// <param name="menuEntries">Einträge des Menüs.</param>
        public NumericMenu(NumericMenuEntry menuTitle, NumericMenuEntry[] menuEntries)
        {
            this.menuTitle = menuTitle;
            this.menuEntries = menuEntries;
        }

        /// <summary>
        /// MenuHeader zu einem NumericMenuEntry, ggf. mit einem Seperator, anzeigen.
        /// </summary>
        /// <param name="menuTitle">Titel des Menüs.</param>
        /// <param name="showSeperator">Gibt an, ob ein Seperator angezeigt werden soll.</param>
        public static void ShowMenuHeader(NumericMenuEntry menuTitle, bool showSeperator)
        {
            if (showSeperator)
            {
                Console.WriteLine(Seperator);
            }

            Console.WriteLine(GetHeader(menuTitle));
            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Eingabeaufforderung ausgeben.
        /// </summary>
        public static void ShowPromptInfo()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Bitte wählen Sie aus, welchen Aktion Sie ausführen möchten.");
            Console.WriteLine("Geben Sie dazu die jeweilige Ziffer ein und bestätigen Sie Ihre Eingabe mit der Enter-Taste.");
        }

        /// <summary>
        /// Header/Bezeichnung eines NumericMenuEntry zurückgeben.
        /// </summary>
        /// <param name="menuEntry">Ein NumericMenuEntry, zu dem die Bezeichnung ausgegeben werden soll.</param>
        /// <returns>Die Bezeichnung als string.</returns>
        public static string GetHeader(NumericMenuEntry menuEntry)
        {
            return menuEntry switch
            {
                NumericMenuEntry.MainMenu => "Hauptmenü",
                NumericMenuEntry.GuestMenu => "Gästeverwaltung",
                NumericMenuEntry.ListGuests => "Gästeliste",
                NumericMenuEntry.AddGuest => "Gast anlegen",
                NumericMenuEntry.EditGuest => "Gast bearbeiten",
                NumericMenuEntry.RemoveGuest => "Gast löschen",
                NumericMenuEntry.SafeAndExit => "Datenstand speichern und Programm beenden",
                NumericMenuEntry.RoomMenu => "Zimmerverwaltung",
                NumericMenuEntry.ListRoomsAndTypes => "Zimmer und Zimmerkategorien",
                NumericMenuEntry.AddRoomType => "Zimmerkategorie anlegen",
                NumericMenuEntry.EditRoomType => "Zimmerkategorie bearbeiten",
                NumericMenuEntry.RemoveRoomType => "Zimmerkategorie löschen",
                NumericMenuEntry.AddRoom => "Zimmer anlegen",
                NumericMenuEntry.EditRoom => "Zimmer bearbeiten",
                NumericMenuEntry.RemoveRoom => "Zimmer löschen",
                NumericMenuEntry.ReservationMenu => "Reservierungsverwaltung",
                NumericMenuEntry.ListReservations => "Reservierungen",
                NumericMenuEntry.AddReservation => "Reservierung anlegen",
                NumericMenuEntry.EditReservation => "Reservierung bearbeiten",
                NumericMenuEntry.RemoveReservation => "Reservierung löschen",
                _ => null,
            };
        }

        /// <summary>
        /// Beschreibung eines NumericMenuEntry zurückgeben.
        /// Für manche Menüs wird eine eigene Beschreibung zurückgegeben, für die anderen Fälle entspricht die Beschreibung dem Header.
        /// </summary>
        /// <param name="menuEntry">Ein NumericMenuEntry, zu dem die Beschreibung ausgegeben werden soll.</param>
        /// <returns>Die Beschreibung als string.</returns>
        public static string GetDescription(NumericMenuEntry menuEntry)
        {
            return menuEntry switch
            {
                NumericMenuEntry.MainMenu => "Zurück zum Hauptmenü",
                NumericMenuEntry.GuestMenu => "Gästeverwaltung (Gäste ansehen, anlegen, bearbeiten oder löschen)",
                NumericMenuEntry.ListGuests => "Gästeliste ansehen",
                NumericMenuEntry.RoomMenu => "Zimmerverwaltung (Zimmer und Zimmerkategorien ansehen, anlegen, bearbeiten oder löschen)",
                NumericMenuEntry.ListRoomsAndTypes => "Zimmer und Zimmerkategorien ansehen",
                NumericMenuEntry.ReservationMenu => "Reservierungsverwaltung (Reservierungen ansehen, anlegen, bearbeiten oder löschen)",
                NumericMenuEntry.ListReservations => "Reservierungen ansehen",
                _ => GetHeader(menuEntry),
            };
        }

        /// <summary>
        /// Ein nummeriertes Konsolenmenü mit wählbaren IDs anzeigen und die Auswahl abwarten.
        /// Für manche Menüs wird eine eigene Beschreibung zurückgegeben, für die anderen Fälle entspricht die Beschreibung dem Header.
        /// </summary>
        /// <returns>Die im Menü getroffene Auswahl als NumericMenuEntry.</returns>
        public NumericMenuEntry ShowMenu()
        {
            ShowMenuHeader(this.menuTitle, true);

            int id = 1;
            foreach (NumericMenuEntry menuEntry in this.menuEntries)
            {
                Console.WriteLine(id++ + " - " + GetDescription(menuEntry));
            }

            ShowPromptInfo();
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > this.menuEntries.Length || userInputId < 1)
            {
                Console.WriteLine("Sie haben eine ungültige Ziffer eingegeben!");
                ShowPromptInfo();
            }

            return this.menuEntries[--userInputId];
        }
    }
}
