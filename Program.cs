using System;

namespace TalkingClock
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Talking Clock - Numeric Time Human Friendly Text
            string enterTime = "Please enter the time format hh:mm";
            Console.WriteLine("Talking Clock Coding Challenge");
            Console.WriteLine("Please press the enter button to get human friendly current time.");
            Console.WriteLine("Please enter time and press enter button to get human friendly time text.");
            Console.WriteLine($"{enterTime}");
            while (true)
            {
                try
                {
                    string? tc1 = Console.ReadLine();
                    Console.WriteLine(TalkingClockCode.Time(tc1));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{enterTime}");
                }
            }
            
        }
    }
}
