// <copyright file="XMLSerializer.cs" company="Marcel Just">
// Copyright (c) Marcel Just. FPK NT WS 2020/21, UDE.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FPKHotelreservierung
{
    /// <summary>
    /// Klasse mit Methoden zum Speichern und Lesen von Daten via XML Serialisierung.
    /// </summary>
    /// <typeparam name="T">Der zu speichernde Typ.</typeparam>
    public static class XMLSerializer<T>
    {
        private static readonly XmlWriterSettings XmlWriterSettings = new XmlWriterSettings() { Indent = true };

        /// <summary>
        /// Liste per Serialisierung mit XML in eine XML-Datei speichern.
        /// </summary>
        /// <param name="list">Zu speichernde Liste.</param>
        /// <param name="fileNameWithFileType">Dateiname mit Dateiendung.</param>
        public static void SerializeList(List<T> list, string fileNameWithFileType)
        {
            // Die Liste list muss nicht weiter geprüft werden, da leere Listen keine Exception verursachen
            // und diese auch leer serialisiert werden sollen.
            XmlSerializer xmlSerializer = new XmlSerializer(list.GetType());

            // C# 8.0 using-Syntax (Objekt wird mit dem Ende der Methode disposed)
            using XmlWriter xmlWriter = XmlWriter.Create(fileNameWithFileType, XmlWriterSettings);
            xmlSerializer.Serialize(xmlWriter, list);
        }

        /// <summary>
        /// Liste per Deserialisierung mit XML aus einer XML-Datei erstellen.
        /// </summary>
        /// <param name="fileNameWithFileType">Dateiname mit Dateiendung.</param>
        /// <returns>Aus XML-Datei erstellte Liste.</returns>
        public static List<T> DeserializeList(string fileNameWithFileType)
        {
            if (!File.Exists(fileNameWithFileType))
            {
                // Datei existiert nicht
                Console.WriteLine(fileNameWithFileType + " existiert nicht, nicht geladen.");
                return new List<T>();
            }

            // C# 8.0 using-Syntax (Objekt wird mit dem Ende der Methode disposed)
            using FileStream fileStream = new FileStream(fileNameWithFileType, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            try
            {
                List<T> returnValue = (List<T>)xmlSerializer.Deserialize(fileStream);
                Console.WriteLine(fileNameWithFileType + " erfolgreich geladen.");
                return returnValue;
            }
            catch (InvalidOperationException)
            {
                // Kann z.B. bei ungültigem XML-Format nach einem Fehler beim Speichern o.ä. auftreten
                Console.WriteLine("Fehler beim Laden von " + fileNameWithFileType
                    + ", bitte prüfen Sie das Format oder legen Sie die Datei neu an.");
                return new List<T>();
            }
        }
    }
}
