﻿using System;
using System.Globalization;

namespace Creativity_Task
{
    public class NameData
    {
        /// <summary>Дата рождения</summary>
        public DateTime BirthDate;

        /// <summary>Имя</summary>
        public string Name;

        public NameData(int year, int month, int day, string name)
            : this(new DateTime(year, month, day), name)
        {
        }

        public NameData(DateTime birthDate, string name)
        {
            BirthDate = birthDate;
            Name = name;
        }

        public static NameData ParseFrom(string textLine)
        {
            var parts = textLine.Split('\t');
            const string format = "dd.MM.yyyy";
            var date = DateTime.ParseExact(parts[0], format, CultureInfo.InvariantCulture);
            return new NameData(date, parts[1]);
        }

        public override string ToString()
        {
            return string.Format("{0}    {1}", BirthDate.ToString("dd.MM.yyyy"), Name);
        }
    }
}