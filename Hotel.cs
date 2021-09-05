// <copyright file="Hotel.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using System.Collections.Generic;
using FPKHotelreservierung.GuestUtil;
using FPKHotelreservierung.ReservationUtil;
using FPKHotelreservierung.RoomUtil;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Die Klasse verwaltet die Listen und die Methoden, die bei Wahl von Aktionen in der Konsole aufgerufen werden.
    /// </summary>
    public class Hotel
    {
        // Auto-Properties
        public static List<Guest> GuestList { get; set; } = new List<Guest>();

        public static List<RoomType> RoomTypeList { get; set; } = new List<RoomType>();

        public static List<Room> RoomList { get; set; } = new List<Room>();

        public static List<Reservation> ReservationList { get; set; } = new List<Reservation>();

        /// <summary>
        /// Hauptmenü anzeigen und Benutzerwahl auslesen.
        /// </summary>
        public void ShowMainMenu()
        {
            NumericMenu mainMenu = new NumericMenu(NumericMenuEntry.MainMenu, new NumericMenuEntry[] { NumericMenuEntry.GuestMenu, NumericMenuEntry.RoomMenu, NumericMenuEntry.ReservationMenu, NumericMenuEntry.SafeAndExit });
            NumericMenuEntry userChoice = mainMenu.ShowMenu();

            switch (userChoice)
            {
                case NumericMenuEntry.GuestMenu:
                    this.ShowGuestMenu();
                    break;
                case NumericMenuEntry.RoomMenu:
                    this.ShowRoomMenu();
                    break;
                case NumericMenuEntry.ReservationMenu:
                    this.ShowReservationMenu();
                    break;
                case NumericMenuEntry.SafeAndExit:
                    this.SafeAndExit();
                    break;
            }
        }

        /// <summary>
        /// Gastmenü anzeigen und Benutzerwahl auslesen.
        /// </summary>
        private void ShowGuestMenu()
        {
            NumericMenu guestMenu = new NumericMenu(NumericMenuEntry.GuestMenu, new NumericMenuEntry[] { NumericMenuEntry.ListGuests, NumericMenuEntry.AddGuest, NumericMenuEntry.EditGuest, NumericMenuEntry.RemoveGuest, NumericMenuEntry.MainMenu });
            NumericMenuEntry userChoice = guestMenu.ShowMenu();

            switch (userChoice)
            {
                case NumericMenuEntry.ListGuests:
                    this.ListGuests(true, true, false);
                    break;
                case NumericMenuEntry.AddGuest:
                    this.AddGuest(true);
                    break;
                case NumericMenuEntry.EditGuest:
                    this.EditGuest();
                    break;
                case NumericMenuEntry.RemoveGuest:
                    this.RemoveGuest();
                    break;
                case NumericMenuEntry.MainMenu:
                    this.ShowMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Zimmermenü zeigen und Benutzerauswahl auslesen.
        /// </summary>
        private void ShowRoomMenu()
        {
            NumericMenu roomMenu = new NumericMenu(NumericMenuEntry.RoomMenu, new NumericMenuEntry[] { NumericMenuEntry.ListRoomsAndTypes, NumericMenuEntry.AddRoomType, NumericMenuEntry.EditRoomType, NumericMenuEntry.RemoveRoomType, NumericMenuEntry.AddRoom, NumericMenuEntry.EditRoom, NumericMenuEntry.RemoveRoom, NumericMenuEntry.MainMenu });
            NumericMenuEntry userChoice = roomMenu.ShowMenu();

            switch (userChoice)
            {
                case NumericMenuEntry.ListRoomsAndTypes:
                    this.ListRoomsAndTypes();
                    break;
                case NumericMenuEntry.AddRoomType:
                    this.AddRoomType(true);
                    break;
                case NumericMenuEntry.EditRoomType:
                    this.EditRoomType();
                    break;
                case NumericMenuEntry.RemoveRoomType:
                    this.RemoveRoomType();
                    break;
                case NumericMenuEntry.AddRoom:
                    this.AddRoom(true, false);
                    break;
                case NumericMenuEntry.EditRoom:
                    this.EditRoom();
                    break;
                case NumericMenuEntry.RemoveRoom:
                    this.RemoveRoom();
                    break;
                case NumericMenuEntry.MainMenu:
                    this.ShowMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Reservierungsmenü zeigen und Benutzerauswahl auslesen.
        /// </summary>
        private void ShowReservationMenu()
        {
            NumericMenu reservationMenu = new NumericMenu(NumericMenuEntry.ReservationMenu, new NumericMenuEntry[] { NumericMenuEntry.ListReservations, NumericMenuEntry.AddReservation, NumericMenuEntry.EditReservation, NumericMenuEntry.RemoveReservation, NumericMenuEntry.MainMenu });
            NumericMenuEntry userChoice = reservationMenu.ShowMenu();

            switch (userChoice)
            {
                case NumericMenuEntry.ListReservations:
                    this.ListReservations(true, true);
                    break;
                case NumericMenuEntry.AddReservation:
                    this.AddReservation();
                    break;
                case NumericMenuEntry.EditReservation:
                    this.EditReservation();
                    break;
                case NumericMenuEntry.RemoveReservation:
                    this.RemoveReservation();
                    break;
                case NumericMenuEntry.MainMenu:
                    this.ShowMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Daten per XML Serialisierung speichern.
        /// </summary>
        private void SafeAndExit()
        {
            XMLSerializer<Guest>.SerializeList(Hotel.GuestList, "GuestList.xml");
            XMLSerializer<RoomType>.SerializeList(Hotel.RoomTypeList, "RoomTypeList.xml");
            XMLSerializer<Room>.SerializeList(Hotel.RoomList, "RoomList.xml");
            XMLSerializer<Reservation>.SerializeList(Hotel.ReservationList, "ReservationList.xml");

            Environment.Exit(0);
        }

        /// <summary>
        /// Methode zum Listen aller vorhandenen Gäste.
        /// </summary>
        /// <param name="showHeader">Gibt an, ob der Header und Seperator angezeigt werden soll.</param>
        /// <param name="returnToGuestMenu">Gibt an, ob nach Ausführung wieder das Gastmenü aufgerufen werden soll.</param>
        /// <param name="showAddNewGuest">Gibt an, ob ein Eintrag 'Neuen Gast anlegen' vorhanden sein soll.</param>
        private void ListGuests(bool showHeader, bool returnToGuestMenu, bool showAddNewGuest)
        {
            if (showHeader)
            {
                NumericMenu.ShowMenuHeader(NumericMenuEntry.ListGuests, true);
            }

            if (GuestList == null || GuestList.Count < 1)
            {
                Console.WriteLine("Es existiert noch kein Gast im System.");
            }

            // Jeden Gast mit ID ausgeben
            int id = 1;
            foreach (Guest guest in GuestList)
            {
                Console.WriteLine(id++ + " - " + guest.ToString());
            }

            if (!showHeader && !showAddNewGuest)
            {
                // Wird beim Bearbeiten oder Löschen von Gästen aufgerufen
                // Zurück-Auswahl einfügen
                Console.WriteLine(id++ + " - Keinen Gast/Zurück");
            }

            if (showAddNewGuest)
            {
                Console.WriteLine(id++ + " - Neuen Gast anlegen");
            }

            if (returnToGuestMenu)
            {
                this.ShowGuestMenu();
            }
        }

        /// <summary>
        /// Methode zum Anlegen eines Gasts.
        /// </summary>
        /// <param name="showGuestMenuAfter">Gibt an, ob nach Ausführung wieder das Gastmenü aufgerufen werden soll.</param>
        /// <returns>Den neuangelegten Gast.</returns>
        private Guest AddGuest(bool showGuestMenuAfter)
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.AddGuest, true);

            // Neues Objekt wird mit Usereingaben in der Methode AddOrEditGuest erstellt
            Guest newGuest = this.AddOrEditGuest(null);

            // Zur Gästeliste hinzufügen
            GuestList.Add(newGuest);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Der Gast '" + newGuest.Forename + " " + newGuest.Surname + "' wurde erfolgreich angelegt.");

            if (showGuestMenuAfter)
            {
                this.ShowGuestMenu();
            }

            return newGuest;
        }

        /// <summary>
        /// Methode zum Bearbeiten eines Gasts.
        /// </summary>
        private void EditGuest()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.EditGuest, true);

            // Liste von Gästen ausgeben
            this.ListGuests(false, false, false);
            if (GuestList == null || GuestList.Count < 1)
            {
                this.ShowGuestMenu();
                return;
            }

            // Userwahl eines Gasts abwarten
            UserInput.ShowPleaseSelect("welchen Gast Sie bearbeiten möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > GuestList.Count || userInputId < 1)
            {
                if (userInputId == GuestList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowGuestMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welchen Gast Sie bearbeiten möchten", true);
            }

            int guestIndexId = --userInputId;
            Guest guestToEdit = GuestList[guestIndexId];

            // Bearbeitetes Objekt wird mit Usereingaben in der Methode AddOrEditGuest erstellt
            // Gast in Liste ersetzen
            Guest editedGuest = this.AddOrEditGuest(guestToEdit);
            GuestList[guestIndexId] = editedGuest;

            // Betroffene Reservierungen anpassen
            foreach (Reservation reservation in ReservationList)
            {
                if (reservation.Guest.Equals(guestToEdit))
                {
                    reservation.Guest = editedGuest;
                }
            }

            Console.WriteLine(string.Empty);
            Console.WriteLine("Der Gast '" + editedGuest.Forename + " " + editedGuest.Surname + "' wurde erfolgreich bearbeitet und gespeichert.");

            this.ShowGuestMenu();
        }

        /// <summary>
        /// Methode zum Bearbeiten und Anlegen eines Gasts.
        /// Beim Bearbeiten werden die vorherigen Werte als Standardtext für den User dargestellt.
        /// </summary>
        /// <param name="guestToEdit">Wenn ein Gast bearbeitet wird stellt das Objekt den Gast vor der Bearbeitung dar.</param>
        /// <returns>Den erstellten oder geänderten Gast.</returns>
        private Guest AddOrEditGuest(Guest guestToEdit)
        {
            // Grundinfo
            string forename = UserInput.GetString("Vorname", guestToEdit?.Forename, false);
            string surname = UserInput.GetString("Nachname", guestToEdit?.Surname, false);
            DateTime birthDate = UserInput.GetDate("Geburtsdatum", guestToEdit?.BirthDate);

            // Adresse
            string street = UserInput.GetString("Straße", guestToEdit?.Address.Street, false);
            string number = UserInput.GetString("Hausnummer", guestToEdit?.Address.Number, false);
            uint postalcode = UserInput.GetUint("Postleitzahl", guestToEdit?.Address.PostalCode);
            string city = UserInput.GetString("Stadt", guestToEdit?.Address.City, false);
            string country = UserInput.GetString("Land", guestToEdit?.Address.Country, false);
            Address newGuestAddress = new Address(street, number, postalcode, city, country);

            // Kreditkarte und Objekterstellung
            if (UserInput.GetYesNo("Möchten Sie für diesen Gast eine Kreditkarte hinterlegen?"))
            {
                CreditCardType ccType = UserChoice<CreditCardType>.ChooseFromEnum(typeof(CreditCardType), "welchen Kreditkartentyp Sie für den Gast '" + forename + " " + surname + "' hinterlegen möchten", null);
                ulong ccCardNumber = UserInput.GetUlong("Kreditkartennummer", guestToEdit?.CreditCard?.CardNumber);
                DateTime ccValidUntil = UserInput.GetDateInValidUntilFormat(guestToEdit?.CreditCard?.ValidUntil);
                uint ccCheckDigit = UserInput.GetUint("Prüfziffer der Kreditkarte", guestToEdit?.CreditCard?.CheckDigit);
                CreditCard newGuestCC = new CreditCard(ccType, ccCardNumber, ccValidUntil, ccCheckDigit);
                guestToEdit = new Guest(forename, surname, newGuestAddress, birthDate, newGuestCC);
            }
            else
            {
                guestToEdit = new Guest(forename, surname, newGuestAddress, birthDate);
            }

            return guestToEdit;
        }

        /// <summary>
        /// Methode zum Löschen eines Gasts.
        /// </summary>
        private void RemoveGuest()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.RemoveGuest, true);

            // Liste von Gästen ausgeben
            this.ListGuests(false, false, false);
            if (GuestList == null || GuestList.Count < 1)
            {
                this.ShowGuestMenu();
                return;
            }

            // Userwahl eines Gasts abwarten
            UserInput.ShowPleaseSelect("welchen Gast Sie löschen möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > GuestList.Count || userInputId < 1)
            {
                if (userInputId == GuestList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowGuestMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welchen Gast Sie löschen möchten", true);
            }

            int guestIndexId = --userInputId;
            Guest guestToRemove = GuestList[guestIndexId];

            if (UserInput.GetYesNo("Möchten Sie den Gast '" + guestToRemove.Forename + " " + guestToRemove.Surname + "' wirklich löschen?") == true)
            {
                GuestList.RemoveAt(guestIndexId);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Der Gast '" + guestToRemove.Forename + " " + guestToRemove.Surname + "' wurde erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Das Löschen des Gasts wurde abgebrochen.");
            }

            this.ShowGuestMenu();
        }

        /// <summary>
        /// Auswahl des Gasts dem eine Reservierung zugeordnet wird.
        /// </summary>
        /// <returns>Den selektierten Gast.</returns>
        private Guest ChooseGuestForReservation()
        {
            Console.WriteLine(string.Empty);
            this.ListGuests(false, false, true);
            UserInput.ShowPleaseSelect("welchem Gast Sie die Reservierung zuordnen möchten, oder legen Sie einen neuen Gast an", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > GuestList.Count || userInputId < 1)
            {
                if (userInputId == GuestList.Count + 1)
                {
                    // User hat Neuen Gast gewählt
                    return this.AddGuest(false);
                }

                UserInput.ShowPleaseSelect("welchem Gast Sie die Reservierung zuordnen möchten, oder legen Sie einen neuen Gast an", true);
            }

            int guestIndexId = --userInputId;
            Guest selectedGuest = GuestList[guestIndexId];
            return selectedGuest;
        }

        /// <summary>
        /// Methode zum Listen aller vorhandenen Zimmerkategorien und Zimmer.
        /// </summary>
        private void ListRoomsAndTypes()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.ListRoomsAndTypes, true);

            if (RoomTypeList == null || RoomTypeList.Count < 1)
            {
                // Wenn keine Zimmerkategorien existieren, können auch keine Zimmer existieren.
                Console.WriteLine("Es existiert noch keine Zimmer und keine Zimmerkategorie im System.");
            }

            int roomTypeId = 1;
            int roomId = 1;
            foreach (RoomType roomType in RoomTypeList)
            {
                Console.WriteLine(roomTypeId + " - " + roomType.ToString());
                uint roomsInRoomType = 0;
                foreach (Room room in RoomList)
                {
                    if (room.RoomType.Equals(roomType))
                    {
                        // Räume einrücken
                        Console.WriteLine("    " + roomTypeId + "." + roomId++ + " - " + room.ToString());
                        roomsInRoomType++;
                    }
                }

                if (roomsInRoomType < 1)
                {
                    Console.WriteLine("    Keine Zimmer in dieser Kategorie");
                }

                roomsInRoomType = 0;
                roomId = 1;
                roomTypeId++;
            }

            this.ShowRoomMenu();
        }

        /// <summary>
        /// Methode zum Listen aller vorhandenen Zimmer.
        /// Methode wird nur aufgerufen bei 'Zimmer bearbeiten' und 'Zimmer löschen' zur Auswahl des zu bearbeitenden/zu löschenden Zimmers.
        /// </summary>
        private void ListRooms()
        {
            if (RoomTypeList == null || RoomTypeList.Count < 1 || RoomList == null || RoomList.Count < 1)
            {
                // Wenn keine Zimmerkategorien existieren, können auch keine Zimmer existieren.
                Console.WriteLine("Es existiert noch keine Zimmer im System.");
            }
            else
            {
                int roomId = 1;
                foreach (Room room in RoomList)
                {
                    Console.WriteLine(roomId++ + " - " + room.ToString());
                }

                // Zurück-Auswahl einfügen
                Console.WriteLine(roomId++ + " - Kein Zimmer/Zurück");
            }
        }

        /// <summary>
        /// Zimmerauswahl durch den Nutzer im Rahmen einer Reservierung.
        /// </summary>
        /// <param name="roomList">Liste der zur Verfügung stehenden Zimmer.</param>
        /// <returns>Den selektierten Zimmer.</returns>
        private Room ChooseRoomForReservation(List<Room> roomList)
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Folgende Zimmer stehen für die gewünschte Personenanzahl zur Auswahl:");

            // Listen der zur Verfügung stehenden Zimmer
            int roomId = 1;
            foreach (Room room in roomList)
            {
                Console.WriteLine(roomId++ + " - " + room.ToString() + room.RoomType.ShowInfoForReservation());
            }

            UserInput.ShowPleaseSelect("welches Zimmer gebucht werden soll", false);

            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > roomList.Count || userInputId < 1)
            {
                UserInput.ShowPleaseSelect("welches Zimmer gebucht werden soll", true);
            }

            // Auswahl getroffen
            int roomListId = --userInputId;
            Room selectedRoom = roomList[roomListId];
            return selectedRoom;
        }

        /// <summary>
        /// Methode zum Listen aller vorhandenen Zimmerkategorien.
        /// Methode wird nur aufgerufen bei 'Zimmerkategorie bearbeiten' und 'Zimmerkategorie löschen' zur Auswahl der zu bearbeitenden/zu löschenden Zimmerkategorie.
        /// </summary>
        /// <param name="showReturnItemInList">Wenn true wird ein Zurück-Item in der Liste angezeigt.</param>
        private void ListRoomTypes(bool showReturnItemInList)
        {
            if (RoomTypeList == null || RoomTypeList.Count < 1)
            {
                // Wenn keine Zimmertypen existieren, können auch keine Zimmer existieren.
                Console.WriteLine("Es existiert noch keine Zimmerkategorie im System.");
            }
            else
            {
                int roomTypeId = 1;
                foreach (RoomType roomType in RoomTypeList)
                {
                    Console.WriteLine(roomTypeId++ + " - " + roomType.ToString());
                }

                if (showReturnItemInList)
                {
                    // Zurück-Auswahl einfügen
                    Console.WriteLine(roomTypeId++ + " - Keine Zimmerkategorie/Zurück");
                }
            }
        }

        /// <summary>
        /// Methode zum Anlegen einer Zimmerkategorie.
        /// </summary>
        /// <param name="returnToRoomMenu">Gibt an, ob nach Ausführung wieder das Zimmermenü aufgerufen werden soll.</param>
        private void AddRoomType(bool returnToRoomMenu)
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.AddRoomType, true);

            // Neues Objekt wird mit Usereingaben in der Methode AddOrEditRoomType erstellt
            RoomType newRoomType = this.AddOrEditRoomType(null);

            RoomTypeList.Add(newRoomType);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Die Zimmerkategorie '" + newRoomType.RoomTypeName + "' wurde erfolgreich angelegt.");

            if (returnToRoomMenu)
            {
                this.ShowRoomMenu();
            }
        }

        /// <summary>
        /// Methode zum Bearbeiten einer Zimmerkategorie.
        /// </summary>
        private void EditRoomType()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.EditRoomType, true);
            this.ListRoomTypes(true);

            if (RoomTypeList == null || RoomTypeList.Count < 1)
            {
                this.ShowRoomMenu();
                return;
            }

            Console.WriteLine(string.Empty);
            UserInput.ShowPleaseSelect("welche Zimmerkategorie Sie bearbeiten möchten. ACHTUNG: Beim Bearbeiten von Zimmerkategorien werden alle Reservierungen für diese Zimmerkategorie aus dem System gelöscht. Beachten Sie, dass die Bearbeitung von Zimmerkategorien auch Auswirkungen auf die Zimmer in der Zimmerkategorie hat", false);

            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > RoomTypeList.Count || userInputId < 1)
            {
                if (userInputId == RoomTypeList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowRoomMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welche Zimmerkategorie Sie bearbeiten möchten. ACHTUNG: Beim Bearbeiten von Zimmerkategorien werden alle Reservierungen für diese Zimmerkategorie aus dem System gelöscht. Beachten Sie, dass die Bearbeitung von Zimmerkategorien auch Auswirkungen auf die Zimmer in der Zimmerkategorie hat", true);
            }

            int roomTypeIndexId = --userInputId;
            RoomType roomTypeToEdit = RoomTypeList[roomTypeIndexId];

            // Bearbeitetes Objekt wird mit Usereingaben in der Methode AddOrEditRoomType erstellt
            RoomType editedRoomType = this.AddOrEditRoomType(roomTypeToEdit);
            RoomTypeList[roomTypeIndexId] = editedRoomType;

            // Zimmerkategorie bei betroffenen Räumen ändern
            foreach (Room room in RoomList)
            {
                if (room.RoomType.Equals(roomTypeToEdit))
                {
                    room.RoomType = editedRoomType;
                }
            }

            // Betroffene Reservierungen löschen (Lambda)
            ReservationList.RemoveAll(x => x.RoomType.Equals(roomTypeToEdit));

            Console.WriteLine(string.Empty);
            Console.WriteLine("Die Zimmerkategorie '" + editedRoomType.RoomTypeName + "' und die betroffenen Zimmer wurden erfolgreich bearbeitet und gespeichert. Bestehende Reservierungen wurden ggf. gelöscht.");

            this.ShowRoomMenu();
        }

        /// <summary>
        /// Methode zum Bearbeiten und Anlegen einer Zimmerkategorie.
        /// Name der Zimmerkategorie wird einmalig vergeben.
        /// Beim Bearbeiten werden die vorherigen Werte als Standardtext für den User dargestellt.
        /// </summary>
        /// <param name="previousRoomType">Wenn eine Zimmerkategorie bearbeitet wird stellt das Objekt die Zimmerkategorie vor der Bearbeitung dar.</param>
        /// <returns>Die erstellte oder geänderte Zimmerkategorie.</returns>
        private RoomType AddOrEditRoomType(RoomType previousRoomType)
        {
            string name = UserInput.GetString("Zimmerkategorienbezeichnung", previousRoomType?.RoomTypeName, false);

            // Prüfen, ob Zimmerkategorie-Name bereits existiert
            foreach (RoomType roomTypeInList in RoomTypeList)
            {
                if (name.Equals(roomTypeInList.RoomTypeName, StringComparison.OrdinalIgnoreCase) && roomTypeInList != previousRoomType)
                {
                    // Zimmerkategorie-Name bereits vergeben
                    Console.WriteLine("Es existiert bereits eine Zimmerkategorie mit dem Namen '" + name + "'. Bitte geben Sie einen anderen Namen ein.");
                    return this.AddOrEditRoomType(previousRoomType);
                }
            }

            uint maxPersons = UserInput.GetUint("Maximale Personenanzahl", previousRoomType?.MaxPersons);
            string interiorDescription = UserInput.GetString("Beschreibung der Ausstattung", previousRoomType?.InteriorDescription, false);
            decimal pricePerNight = UserInput.GetDecimal("Preis pro Nacht", previousRoomType?.PricePerNight);
            decimal pricePerWeek = UserInput.GetDecimal("Preis pro Woche", previousRoomType?.PricePerWeek);

            previousRoomType = new RoomType(name, maxPersons, interiorDescription, pricePerNight, pricePerWeek);

            return previousRoomType;
        }

        /// <summary>
        /// Methode zum Löschen einer Zimmerkategorie.
        /// </summary>
        private void RemoveRoomType()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.RemoveRoomType, true);
            this.ListRoomTypes(true);

            if (RoomTypeList == null || RoomTypeList.Count < 1)
            {
                this.ShowRoomMenu();
                return;
            }

            UserInput.ShowPleaseSelect("welche Zimmerkategorie Sie löschen möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > RoomTypeList.Count || userInputId < 1)
            {
                if (userInputId == RoomTypeList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowRoomMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welche Zimmerkategorie Sie löschen möchten", true);
            }

            int roomTypeIndexId = --userInputId;
            RoomType roomTypeToRemove = RoomTypeList[roomTypeIndexId];

            if (UserInput.GetYesNo("Möchten Sie die Zimmerkategorie '" + roomTypeToRemove.RoomTypeName + "' wirklich löschen? ACHTUNG: Alle Zimmer in dieser Zimmerkategorie und alle Reservierungen für diese Zimmerkategorie werden ebenfalls aus dem System gelöscht!") == true)
            {
                // Betroffene Räume löschen (Lambda)
                RoomList.RemoveAll(x => x.RoomType.Equals(roomTypeToRemove));

                // Betroffene Reservierungen löschen (Lambda)
                ReservationList.RemoveAll(x => x.RoomType.Equals(roomTypeToRemove));

                RoomTypeList.RemoveAt(roomTypeIndexId);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Die Zimmerkategorie '" + roomTypeToRemove.RoomTypeName + "' wurde erfolgreich gelöscht. Bestehende Reservierungen wurden ggf. gelöscht.");
            }
            else
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Das Löschen der Zimmerkategorie wurde abgebrochen.");
            }

            this.ShowRoomMenu();
        }

        /// <summary>
        /// Methode zum Anlegen eines Zimmers.
        /// </summary>
        /// <param name="returnToRoomMenu">Gibt an, ob nach Anlegen des Zimmers zur Zimmerverwaltung zurückgekehrt werden soll.</param>
        /// <param name="returnToReservationMenuOnCancel">Gibt an, ob bei Abbruch (wenn noch keine Zimmerkategorie besteht und der
        /// Benutzer keine Zimmerkategorie anlegen will) zum Reservierungsmenü zurückgekehrt werden soll.
        /// Ist true, wenn die Methode im Zuge der Erstellung einer Reservierung aufgerufen wird, sonst false.</param>
        private void AddRoom(bool returnToRoomMenu, bool returnToReservationMenuOnCancel)
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.AddRoom, true);

            if (RoomTypeList == null || RoomTypeList.Count < 1)
            {
                Console.WriteLine("Bevor Zimmer im System angelegt werden können, muss mindestens eine Zimmerkategorie angelegt werden.");

                if (UserInput.GetYesNo("Möchten Sie jetzt eine Zimmerkategorie anlegen?") == true)
                {
                    this.AddRoomType(false);

                    // Nach dem Anlegen einer Zimmerkategorie wird das Anlegen des Zimmers fortgesetzt
                    NumericMenu.ShowMenuHeader(NumericMenuEntry.AddRoom, true);
                }
                else
                {
                    if (returnToReservationMenuOnCancel)
                    {
                        this.ShowReservationMenu();
                        return;
                    }
                    else
                    {
                        this.ShowRoomMenu();
                        return;
                    }
                }
            }

            // Neues Objekt wird mit Usereingaben in der Methode AddOrEditRoom erstellt
            Room newRoom = this.AddOrEditRoom(null);
            RoomList.Add(newRoom);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Das Zimmer mit der Nummer '" + newRoom.RoomNumber + "' wurde erfolgreich angelegt.");

            if (returnToRoomMenu)
            {
                this.ShowRoomMenu();
            }
        }

        /// <summary>
        /// Methode zum Bearbeiten eines Zimmers.
        /// </summary>
        private void EditRoom()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.EditRoom, true);
            this.ListRooms();

            if (RoomTypeList == null || RoomTypeList.Count < 1 || RoomList == null || RoomList.Count < 1)
            {
                this.ShowRoomMenu();
                return;
            }

            UserInput.ShowPleaseSelect("welches Zimmer Sie bearbeiten möchten. ACHTUNG: Beim Bearbeiten von Zimmern werden alle Reservierungen für dieses Zimmer aus dem System gelöscht", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > RoomList.Count || userInputId < 1)
            {
                if (userInputId == RoomList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowRoomMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welches Zimmer Sie bearbeiten möchten. ACHTUNG: Beim Bearbeiten von Zimmern werden alle Reservierungen für dieses Zimmer aus dem System gelöscht", true);
            }

            int roomIndexId = --userInputId;
            Room roomToEdit = RoomList[roomIndexId];

            // Bearbeitetes Objekt wird mit Usereingaben in der Methode AddOrEditRoom erstellt
            Room editedRoom = this.AddOrEditRoom(roomToEdit);
            RoomList[roomIndexId] = editedRoom;

            // Betroffene Reservierungen löschen (Lambda)
            ReservationList.RemoveAll(x => x.Room.Equals(roomToEdit));

            Console.WriteLine(string.Empty);
            Console.WriteLine("Das Zimmer mit der Nummer '" + editedRoom.RoomNumber + "' wurde erfolgreich bearbeitet und gespeichert. Bestehende Reservierungen wurden ggf. gelöscht.");

            this.ShowRoomMenu();
        }

        /// <summary>
        /// Methode zum Bearbeiten und Anlegen eines Zimmers.
        /// Nummer des Zimmers wird einmalig vergeben.
        /// Beim Bearbeiten werden die vorherigen Werte als Standardtext für den User dargestellt.
        /// </summary>
        /// <param name="previousRoom">Wenn ein Zimmer bearbeitet wird stellt das Objekt das Zimmer vor der Bearbeitung dar.</param>
        /// <returns>Das erstellte oder geänderte Zimmer.</returns>
        private Room AddOrEditRoom(Room previousRoom)
        {
            string number = UserInput.GetString("Raumnummer", previousRoom?.RoomNumber, false);

            // Prüfen, ob Zimmer-Nummer bereits existiert
            foreach (Room roomInList in RoomList)
            {
                if (number.Equals(roomInList.RoomNumber, StringComparison.OrdinalIgnoreCase) && roomInList != previousRoom)
                {
                    // Zimmer-Nummer bereits vergeben
                    Console.WriteLine("Es existiert bereits ein Zimmer mit der Nummer '" + number + "'. Bitte geben Sie eine andere Nummer ein.");
                    return this.AddOrEditRoom(previousRoom);
                }
            }

            uint size = UserInput.GetUint("Raumgröße in vollen m2", previousRoom?.RoomSize);
            RoomType roomType = this.ChooseRoomType(number);
            RoomOrientation roomOrientation = UserChoice<RoomOrientation>.ChooseFromEnum(typeof(RoomOrientation), "welche Ausrichtung das Zimmer mit der Nummer '" + number + "' hat", null);

            previousRoom = new Room(number, size, roomType, roomOrientation);

            return previousRoom;
        }

        /// <summary>
        /// Methode zum Löschen eines Zimmers.
        /// </summary>
        private void RemoveRoom()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.RemoveRoom, true);
            this.ListRooms();

            if (RoomTypeList == null || RoomTypeList.Count < 1 || RoomList == null || RoomList.Count < 1)
            {
                this.ShowRoomMenu();
                return;
            }

            UserInput.ShowPleaseSelect("welches Zimmer Sie löschen möchten", false);

            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > RoomList.Count || userInputId < 1)
            {
                if (userInputId == RoomList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowRoomMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welches Zimmer Sie löschen möchten", true);
            }

            int roomIndexId = --userInputId;
            Room roomToRemove = RoomList[roomIndexId];

            if (UserInput.GetYesNo("Möchten Sie das Zimmer mit der Nummer '" + roomToRemove.RoomNumber + "' wirklich löschen? ACHTUNG: Alle Reservierungen für dieses Zimmer werden ebenfalls aus dem System gelöscht!") == true)
            {
                // Betroffene Reservierungen löschen (Lambda)
                ReservationList.RemoveAll(x => x.Room.Equals(roomToRemove));

                RoomList.RemoveAt(roomIndexId);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Das Zimmer mit der Nummer '" + roomToRemove.RoomNumber + "' wurde erfolgreich gelöscht."
                    + "Bestehende Reservierungen wurden ggf. gelöscht.");
            }
            else
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Das Löschen des Zimmers wurde abgebrochen.");
            }

            this.ShowRoomMenu();
        }

        /// <summary>
        /// Zimmerkategorie durch User wählen lassen.
        /// </summary>
        /// <param name="roomNumber">Die zugehörige Zimmernummer.</param>
        /// <returns>Die gewählte Zimmerkategorie.</returns>
        private RoomType ChooseRoomType(string roomNumber)
        {
            Console.WriteLine(string.Empty);
            this.ListRoomTypes(false);

            UserInput.ShowPleaseSelect("welcher Zimmerkategorie Sie den Raum mit der Nummer '" + roomNumber + "' zuweisen möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > RoomTypeList.Count || userInputId < 1)
            {
                UserInput.ShowPleaseSelect("welcher Zimmerkategorie Sie den Raum mit der Nummer '" + roomNumber + "' zuweisen möchten", true);
            }

            int roomTypeIndexId = --userInputId;
            RoomType selectedRoomType = RoomTypeList[roomTypeIndexId];
            return selectedRoomType;
        }

        /// <summary>
        /// Methode zum Listen aller vorhandenen Reservierungen.
        /// </summary>
        /// <param name="showHeader">Soll der Header und Seperator angezeigt werden.</param>
        /// <param name="returnToReservationMenu">Wenn true wird am Ende der Methode zum Reservierungsmenü zurückgekehrt.</param>
        private void ListReservations(bool showHeader, bool returnToReservationMenu)
        {
            if (showHeader)
            {
                NumericMenu.ShowMenuHeader(NumericMenuEntry.ListReservations, true);
            }

            if (ReservationList == null || ReservationList.Count < 1)
            {
                Console.WriteLine("Es existiert noch keine Reservierung im System.");
            }
            else
            {
                int id = 1;
                foreach (Reservation reservation in ReservationList)
                {
                    Console.WriteLine(id++ + " - " + reservation.ToString());
                }

                // Wird beim Bearbeiten oder Löschen von Reservierungen aufgerufen
                // Zurück-Auswahl einfügen
                Console.WriteLine(id++ + " - Keine Reservierung/Zurück");
            }

            if (returnToReservationMenu)
            {
                this.ShowReservationMenu();
            }
        }

        /// <summary>
        /// Methode zum Anlegen einer neuen Reservierung.
        /// Es wird geprüft, ob mindestens ein Zimmer und eine Zimmerkategorie existieren.
        /// </summary>
        private void AddReservation()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.AddReservation, true);

            if (RoomTypeList == null || RoomTypeList.Count < 1 || RoomList == null || RoomList.Count < 1)
            {
                Console.WriteLine("Bevor Reservierungen im System angelegt werden können, muss mindestens ein Zimmer angelegt werden.");

                if (UserInput.GetYesNo("Möchten Sie jetzt ein Zimmer anlegen?") == true)
                {
                    // In AddRoom wird geprüft, ob Zimmerkategorien bestehen, ansonsten muss vorher noch eine Zimmerkategorie erstellt werden
                    this.AddRoom(false, true);

                    // Nach dem Anlegen eines Zimmers (und ggf. einer Zimmerkategorie) wird das Anlegen der Reservierung fortgesetzt
                    NumericMenu.ShowMenuHeader(NumericMenuEntry.AddReservation, true);
                }
                else
                {
                    this.ShowReservationMenu();
                    return;
                }
            }

            // Neues Objekt wird mit Usereingaben in der Methode AddOrEditReservation erstellt.
            Reservation newReservation = this.AddOrEditReservation(null);

            // Wenn newReservation null ist, wurde der Vorgang abgebrochen.
            if (newReservation != null)
            {
                ReservationList.Add(newReservation);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Die Reservierung wurde erfolgreich angelegt."
                    + " Gesamtkosten im Zeitraum inkl. Verpflegung: " + UserInput.FormatPrice(newReservation.Price));
                if (newReservation.Guest.CreditCard == null)
                {
                    Console.WriteLine("Hinweis: Der Gast hat noch keine Kreditkarte hinterlegt.");
                }
            }

            this.ShowReservationMenu();
        }

        /// <summary>
        /// Methode zum Bearbeiten einer bestehenden Reservierung.
        /// </summary>
        private void EditReservation()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.EditReservation, true);
            this.ListReservations(false, false);

            if (ReservationList == null || ReservationList.Count < 1)
            {
                this.ShowReservationMenu();
                return;
            }

            UserInput.ShowPleaseSelect("welche Reservierung Sie bearbeiten möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > ReservationList.Count || userInputId < 1)
            {
                if (userInputId == ReservationList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowReservationMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welche Reservierung Sie bearbeiten möchten", true);
            }

            int reservationIndexId = --userInputId;
            Reservation reservationToEdit = ReservationList[reservationIndexId];

            // Bearbeitetes Objekt wird mit Usereingaben in der Methode AddOrEditReservation erstellt.
            Reservation editedReservation = this.AddOrEditReservation(reservationToEdit);
            if (editedReservation != null)
            {
                ReservationList[reservationIndexId] = editedReservation;

                Console.WriteLine(string.Empty);
                Console.WriteLine("Die Reservierung wurde erfolgreich bearbeitet und gespeichert."
                    + " Gesamtkosten: " + UserInput.FormatPrice(editedReservation.Price));
                if (editedReservation.Guest.CreditCard == null)
                {
                    Console.WriteLine("Hinweis: Der Gast hat noch keine Kreditkarte hinterlegt.");
                }
            }

            this.ShowReservationMenu();
        }

        /// <summary>
        /// Methode zum Bearbeiten und Anlegen einer Reservierung.
        /// Beim Bearbeiten werden die vorherigen Werte als Standardtext für den User dargestellt.
        /// </summary>
        /// <param name="previousReservation">Wenn eine Reservierung bearbeitet wird stellt das Objekt die Reservierung vor der Bearbeitung dar.</param>
        /// <returns>Die erstellte oder geänderte Reservierung.</returns>
        private Reservation AddOrEditReservation(Reservation previousReservation)
        {
            // Personenanzahl abfragen
            uint personCount = UserInput.GetUint("Personenanzahl", previousReservation?.PersonCount);

            if (personCount == 0)
            {
                // 0 ist unzulässig
                if (UserInput.GetYesNo("Sie haben eine unzulässige Personenanzahl angegeben. Möchten Sie nochmal beginnen? (Drücken Sie N, um zurück zum Reservierungsmenü zu navigieren.)"))
                {
                    Console.WriteLine(string.Empty);
                    return this.AddOrEditReservation(previousReservation);
                }
                else
                {
                    Console.WriteLine("Das Anlegen/Bearbeiten der Reservierung wurde abgebrochen.");
                    this.ShowReservationMenu();
                    return null;
                }
            }

            // Zimmer filtern, die in einer Zimmerkategorie mit genug Platz für die Personenanzahl sind
            List<Room> suitableRooms = new List<Room>();
            foreach (Room room in RoomList)
            {
                if (room.RoomType.MaxPersons >= personCount)
                {
                    suitableRooms.Add(room);
                }
            }

            if (suitableRooms.Count < 1)
            {
                // Kein Zimmer mit ausreichend Platz gefunden
                if (UserInput.GetYesNo("Es existiert kein Zimmer bzw. keine Zimmerkategorie im System mit genug Platz für die gewünschte Personenanzahl. Möchten Sie nochmal beginnen? (Drücken Sie N, um zurück zum Reservierungsmenü zu navigieren.)"))
                {
                    Console.WriteLine(string.Empty);
                    return this.AddOrEditReservation(previousReservation);
                }
                else
                {
                    Console.WriteLine("Das Anlegen/Bearbeiten der Reservierung wurde abgebrochen.");
                    this.ShowReservationMenu();
                    return null;
                }
            }

            // An- und Abreisedatum abfragen und validieren
            DateTime arrivalDate = UserInput.GetDate("Anreisedatum", previousReservation?.ArrivalDate);
            DateTime departureDate = UserInput.GetDate("Abreisedatum", previousReservation?.DepartureDate);
            uint reservationPeriodInDays = (uint)(departureDate - arrivalDate).TotalDays;

            if (arrivalDate.Date < DateTime.Now.Date || departureDate.Date < DateTime.Now.Date || (reservationPeriodInDays < 1) || departureDate.Date <= arrivalDate)
            {
                // Unzulässige Daten
                if (UserInput.GetYesNo("Bitte geben Sie ein Anreise- und Abreisedatum in der Zukunft an. Die Mindestaufenthaltsdauer beträgt 1 Tag/Nacht und das Anreisedatum muss vor dem Abreisedatum liegen. Möchten Sie nochmal beginnen? (Drücken Sie N, um zurück zum Reservierungsmenü zu navigieren.)"))
                {
                    Console.WriteLine(string.Empty);
                    return this.AddOrEditReservation(previousReservation);
                }
                else
                {
                    Console.WriteLine("Das Anlegen/Bearbeiten der Reservierung wurde abgebrochen.");
                    this.ShowReservationMenu();
                    return null;
                }
            }

            // Prüfen, ob es freie Zimmer im gewählten Zeitraum in der suitableRooms-Liste gibt.
            // Beim Editieren einer Reservierung soll auch das Zimmer und der Zeitraum von previousReservation zur Auswahl verfügbar sein.
            foreach (Reservation reservation in ReservationList)
            {
                // previousReservation ignorieren
                if (reservation != previousReservation)
                {
                    if (suitableRooms.Contains(reservation.Room))
                    {
                        // Quelle: https://stackoverflow.com/questions/40922409/c-sharp-check-if-a-timespan-range-is-between-timespan-range-and-how-many-hours
                        // answered Dec 2 '16 at 0:23 byuser2818430
                        // edited Dec 2 '16 at 10:26 by Bojan B
                        // Herausfinden ob Datenbereiche überlappen
                        double overlapDays = 0;
                        if (reservation.ArrivalDate.Date >= arrivalDate.Date && reservation.DepartureDate.Date <= departureDate.Date)
                        {
                            overlapDays = (reservation.DepartureDate.Date - reservation.ArrivalDate.Date).TotalHours;
                        }

                        if ((reservation.ArrivalDate.Date >= arrivalDate.Date && reservation.ArrivalDate.Date < departureDate.Date) && reservation.DepartureDate.Date > departureDate.Date)
                        {
                            overlapDays = (departureDate.Date - reservation.ArrivalDate.Date).TotalHours;
                        }

                        if (reservation.ArrivalDate.Date < arrivalDate.Date && (reservation.DepartureDate.Date > arrivalDate.Date && reservation.DepartureDate.Date <= departureDate.Date))
                        {
                            overlapDays = (reservation.DepartureDate.Date - arrivalDate.Date).TotalHours;
                        }

                        if (reservation.ArrivalDate.Date < arrivalDate.Date && reservation.DepartureDate.Date > departureDate.Date)
                        {
                            overlapDays = (departureDate.Date - arrivalDate.Date).TotalHours;
                        }

                        if (overlapDays > 0 || arrivalDate.Date == reservation.ArrivalDate.Date || arrivalDate.Date == reservation.DepartureDate.Date || departureDate.Date == reservation.ArrivalDate.Date || departureDate.Date == reservation.DepartureDate.Date)
                        {
                            // Aktuelle Reservierung (in Schleife) überschneidet sich mit den Daten der neuen Reservierung
                            suitableRooms.Remove(reservation.Room);
                        }
                    }
                }
            }

            if (suitableRooms.Count < 1)
            {
                // Kein Zimmer gefunden, dass im ausgewählten Zeitraum genügend Platz hat
                if (UserInput.GetYesNo("Es ist kein freies Zimmer für die gewünschte Personenanzahl im gewählten Zeitraum verfügbar. Möchten Sie eine neue Reservierung beginnen? (Drücken Sie Nein, um zurück zum Reservierungsmenü zu navigieren.)"))
                {
                    Console.WriteLine(string.Empty);
                    return this.AddOrEditReservation(previousReservation);
                }
                else
                {
                    Console.WriteLine("Das Anlegen/Bearbeiten der Reservierung wurde abgebrochen.");
                    this.ShowReservationMenu();
                    return null;
                }
            }

            // Raum wählen
            Room selectedRoom = this.ChooseRoomForReservation(suitableRooms);

            // Verpflegung wählen
            CateringType selectedCateringType = UserChoice<CateringType>.ChooseFromEnum(typeof(CateringType), "welche Verpflegung gebucht werden soll", reservationPeriodInDays);

            // Gast wählen oder anlegen
            Guest selectedGuest = this.ChooseGuestForReservation();

            previousReservation = new Reservation(selectedGuest, selectedRoom.RoomType, selectedRoom, personCount, arrivalDate, departureDate, selectedCateringType);

            return previousReservation;
        }

        /// <summary>
        /// Methode zum Löschen einer bestehenden Reservierung.
        /// </summary>
        private void RemoveReservation()
        {
            NumericMenu.ShowMenuHeader(NumericMenuEntry.RemoveReservation, true);
            this.ListReservations(false, false);

            if (ReservationList == null || ReservationList.Count < 1)
            {
                this.ShowReservationMenu();
                return;
            }

            UserInput.ShowPleaseSelect("welche Reservierung Sie löschen möchten", false);
            int userInputId;
            while (!int.TryParse(Console.ReadLine(), out userInputId) || userInputId > ReservationList.Count || userInputId < 1)
            {
                if (userInputId == ReservationList.Count + 1)
                {
                    // User hat Zurück gewählt
                    this.ShowReservationMenu();
                    return;
                }

                UserInput.ShowPleaseSelect("welche Reservierung Sie löschen möchten", true);
            }

            int reservationIndexId = --userInputId;

            if (UserInput.GetYesNo("Möchten Sie die Reservierung wirklich löschen?") == true)
            {
                ReservationList.RemoveAt(reservationIndexId);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Die Reservierung wurde erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Das Löschen der Reservierung wurde abgebrochen.");
            }

            this.ShowReservationMenu();
        }
    }
}