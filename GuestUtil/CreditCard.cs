// <copyright file="CreditCard.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using System.Globalization;

namespace FPKHotelreservierung.GuestUtil
{
    /// <summary>
    /// Die Klasse repräsentiert eine Kreditkarte eines Gasts.
    /// </summary>
    [Serializable]

    public class CreditCard
    {
        public CreditCard(CreditCardType cardType, ulong cardNumber, DateTime validUntil, uint checkDigit)
        {
            this.CardType = cardType;
            this.CardNumber = cardNumber;
            this.ValidUntil = validUntil;
            this.CheckDigit = checkDigit;
        }

        public CreditCard()
        {
            // Leerer Konstruktor wird für Serialisierung benötigt.
        }

        // Auto-Properties für XML-Serialisierung.
        public CreditCardType CardType { get; set; }

        public ulong CardNumber { get; set; }

        public DateTime ValidUntil { get; set; }

        public uint CheckDigit { get; set; }

        /// <summary>
        /// Gibt einen String zurück, der das aktuelle CreditCard Objekt repräsentiert.
        /// Ohne CultureInfo würde bei deutschen Anwendern das / durch . ersetzt werden.
        /// Das / ist in diesem Falle für die Gültigkeitsdauer der Kreditkarte jedoch gewünscht.
        /// </summary>
        /// <returns>Ein benutzerfreundliches String des aktuellen Objekts.</returns>
        public override string ToString()
        {
            return this.CardType + " (gültig bis " + this.ValidUntil.ToString("MM/yy", new CultureInfo("en-US")) + ")";
        }
    }
}
