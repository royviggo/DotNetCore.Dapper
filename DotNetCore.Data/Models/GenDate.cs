using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore.Data.Enums;
using DotNetCore.Data.Extensions;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Models
{
    public class GenDate : IEquatable<GenDate>, IComparable<GenDate>
    {
        [NotMapped]
        public IDateStringParser StringParser { get; private set; } = new DateStringParser();

        public GenDateType DateType { get; set; }
        public DatePart DateFrom { get;  set; }
        public DatePart DateTo { get;  set; }
        public string DatePhrase { get;  set; }
        public bool IsValid { get;  set; }
        public int SortDate { get;  set; }
        public string DateString => CreateDateString();

        public GenDate() { }

        public GenDate(DatePart datePart)
        {
            DateFrom = datePart;
            DateTo = datePart;
            DateType = GenDateType.Exact;
            SortDate = GetSortDate();
            IsValid = true;
        }

        public GenDate(GenDateType dateType, DatePart datePart)
        {
            DateFrom = datePart;
            DateTo = datePart;
            DateType = dateType;
            SortDate = GetSortDate();
            IsValid = true;
        }

        public GenDate(GenDateType dateType, DatePart fromDatePart, DatePart toDatePart)
        {
            DateFrom = fromDatePart;
            DateTo = toDatePart;
            DateType = dateType;
            SortDate = GetSortDate();
            IsValid = true;
        }

        public GenDate(GenDateType dateType, DatePart fromDatePart, DatePart toDatePart, bool isValid)
        {
            DateType = dateType;
            DateFrom = fromDatePart;
            DateTo = toDatePart;
            SortDate = GetSortDate();
            IsValid = isValid;
        }

        public GenDate(GenDateType dateType, DatePart fromDatePart, DatePart toDatePart, string datePhrase, bool isValid)
        {
            DateType = dateType;
            DateFrom = fromDatePart;
            DateTo = toDatePart;
            DatePhrase = datePhrase;
            SortDate = GetSortDate();
            IsValid = isValid;
        }

        public GenDate(GenDateType dateType, string datePhrase, bool isValid)
        {
            DateType = dateType;
            DatePhrase = datePhrase;
            SortDate = GetSortDate();
            IsValid = isValid;
        }

        public GenDate(string dateString)
        {
            StringParser.Parse(dateString);
            SortDate = GetSortDate();
        }
        public GenDate(IDateStringParser parser, string dateString)
        {
            StringParser = parser;
            StringParser.Parse(dateString);
            SortDate = GetSortDate();
        }

        public int CompareTo(GenDate other)
        {
            var sortDate = GetSortDate();
            return sortDate.CompareTo(other.SortDate);

        }

        public static bool operator ==(GenDate obj1, GenDate obj2)
        {
            return obj1 != null && (Equals(obj1, obj2) || obj1.Equals(obj2));
        }

        public static bool operator !=(GenDate obj1, GenDate obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(GenDate other)
        {
            return other != null && (DateType == other.DateType && DateFrom == other.DateFrom && DateTo == other.DateTo 
                   && DatePhrase == other.DatePhrase && IsValid == other.IsValid && SortDate == other.SortDate);
        }

        public override bool Equals(object obj)
        {
            return obj != null && (obj.GetType() == GetType() && Equals((GenDate)obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) DateType;
                hashCode = (hashCode * 397) ^ (DateFrom?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DateTo?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DatePhrase?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ IsValid.GetHashCode();
                hashCode = (hashCode * 397) ^ SortDate;
                return hashCode;
            }
        }

        public override string ToString()
        {
            var typeNames = new Dictionary<int, string> { {0, ""}, {1, "Bef. "}, {2, ""}, {3, "Abt. "}, {4, "Cal. "}, {5, "Bet. "}, {6, "From "}, {7, "Aft. "} };
            var rangeJoin = new Dictionary<int, string> { {5, " - "}, {6, " to "} };

            if (!IsValid)
                return DatePhrase;

            var result = typeNames[(int)DateType];
            result += DateFrom.ToString();

            if (DateType == GenDateType.Between || DateType == GenDateType.FromTo)
                result += rangeJoin[(int)DateType] + DateTo;

            return result;
        }

        private int GetSortDate()
        {
            return (DatePart.CompareValue(DateFrom) * 10) + (int)DateType;
        }

        private string CreateDateString()
        {
            return IsValid
                ? string.Join("", new List<string> { "1", DateFrom.ToSortString(), ((int)DateType).ToString(), DateTo.ToSortString() })
                : string.Join("", new List<string> { "2", DatePhrase });
        }
    }
}