using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TalkingClock
{
    public class TalkingClockCode
    {
        private static string hoursText = String.Empty;
        private static string minutesText = String.Empty;
        private static string talkTimeText = String.Empty;
        
        /// <summary>
        /// Time in Hours
        /// </summary>
        private static readonly string[] hours = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
        
        /// <summary>
        /// Time in Minutes
        /// </summary>
        private static readonly string[] minutes = new string[] { "o'clock", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
        "thirteen", "fourteen", "quarter", "sixteen", "seventeen", "eighteen", "nineteen", "twenty", "twenty one", "twenty two", "twenty three", "twenty four",
        "twenty five", "twenty six", "twenty seven", "twenty eight", "twenty nine","half" };
        
        /// <summary>
        /// Clock Time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string Time(string time)
        {
            DateTime dateTime;
            if (String.IsNullOrEmpty(time)) { time = DateTime.Now.ToString("HH:mm"); }
            if (TimeFormat(time, out dateTime))
            {
                if (HalfPast(dateTime.Minute)) { dateTime = dateTime.AddHours(1); }
                hoursText = GetTalkHours(dateTime.Hour);
                minutesText = GetTalkMinutes(dateTime.Minute);
                talkTimeText = GetTalkTimeText(dateTime);
                return GetFirstLetter(GetTalkTime(dateTime));
            }
            else
            {
                throw new FormatException($"Incorrect time format in {dateTime}.");
            }
        }

        /// <summary>
        /// Clock Time Format
        /// </summary>
        /// <param name="time"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static bool TimeFormat(string time, out DateTime dateTime)
        {
            //Time entered is in the required format
            string formatSingleHourDigit = "H:mm";   
            string formatDoubleHourDigit = "HH:mm";

            return (DateTime.TryParseExact(time, formatSingleHourDigit, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)
                   || DateTime.TryParseExact(time, formatDoubleHourDigit, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime));
        }

        /// <summary>
        /// Clock Talk Time in hours
        /// </summary>
        /// <param name="hourNumber"></param>
        /// <returns></returns>
        private static string GetTalkHours(int hourNumber)
        {
           
            if (hourNumber > 12) { hourNumber -= 12; }   // since e.g. 14:00 is two o'clock and not fourteen o'clock
            else if (hourNumber == 0) { hourNumber = 12; }
            for (int i = 0; i < hours.Length; i++)
            {
                if (hourNumber - 1 == i)
                    return hours[i];
            }

            throw new Exception("Clock Talk hours not found");
        }

        /// <summary>
        /// Clock Talk Time in minutes
        /// </summary>
        /// <param name="minuteNumber"></param>
        /// <returns></returns>
        private static string GetTalkMinutes(int minuteNumber)
        {
            if (minuteNumber > 30) { minuteNumber = 60 - minuteNumber; } 
            for (int i = 0; i < minutes.Length; i++)
            {
                if (minuteNumber == i)
                    return minutes[i];
            }
            throw new Exception("Clock Talk minutes not found");
        }

        /// <summary>
        /// Clock Time - half past
        /// </summary>
        /// <param name="minuteNumber"></param>
        /// <returns></returns>
        private static bool HalfPast(int minuteNumber)
        {
            if (minuteNumber > 30) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Clock talk Time Text
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string GetTalkTimeText(DateTime dateTime)
        {
            string talkTimePart = String.Empty;
            if (dateTime.Minute == 00) { talkTimePart = "o'clock"; }
            else if (dateTime.Minute > 00 && dateTime.Minute <= 30) { talkTimePart = "past"; }
            else if (dateTime.Minute > 30) { talkTimePart = "to"; }
            return talkTimePart;
        }

        /// <summary>
        /// Clock Talk Time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string GetTalkTime(DateTime dateTime)
        {
            string talkTime = String.Empty;

            if (talkTimeText == "o'clock") { talkTime = $"{hoursText} {talkTimeText}"; }
            else if (talkTimeText == "past") { talkTime = $"{minutesText} {talkTimeText} {hoursText}"; }
            else if (talkTimeText == "to") { talkTime = $"{minutesText} {talkTimeText} {hoursText}"; }
            return talkTime;
        }

        /// <summary>
        /// Get First Letter Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string GetFirstLetter(string text)
        {
            if (String.IsNullOrEmpty(text)) throw new ArgumentException();
            return text.First().ToString().ToUpper() + text.Substring(1);
        }
    }
}
