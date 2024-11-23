using System;
using System.Collections.Generic;

namespace DrivingSimulator.Functionality
{
    internal class TextFunctionality
    {
        // Dependencies
        private readonly Random rnd;
        private readonly List<string> invalidInputResponses;

        // Constructor
        public TextFunctionality()
        {
            rnd = new Random();
            invalidInputResponses = new()
            {
                "Oops. Wrong Response.",
                "Wrong Key Pressed.",
                "Invalid answer",
                "Incorrect Key"
            };
        }

        // Methods
        public void Write(string text, bool awaitResponse = true, bool clearAfter = false)
        {
            PrivateWrite(false, text, awaitResponse, clearAfter);
        }
        public void WriteLine(string text, bool awaitResponse = true, bool clearAfter = false)
        {
            PrivateWrite(true, text, awaitResponse, clearAfter);
        }
        private void PrivateWrite(bool writeLine, string text, bool awaitResponse, bool clearAfter)
        {
            if (writeLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }

            if (awaitResponse)
            {
                Console.ReadKey(true);
            }

            if (clearAfter)
            {
                Console.Clear();
            }
        }
        public string FormatString(string message, int spacing = 100)
        {
            for (int i = 0; i < message.Length; i++)
            {
                char character = message[i];
                if (char.IsWhiteSpace(character) && i >= spacing)
                {
                    message = message.Insert(i + 1, "\n");
                    spacing += spacing;
                }
            }

            return message;
        }
        public int Options(string headerMessage, params string[] options)
        {
            Dictionary<int, string> acceptableOptions = [];
            AddOptions();
            int requestNum = Ask();

            return ConvertToIndex(requestNum);


            void AddOptions()
            {
                int number = 1;
                foreach (string option in options)
                {
                    if (option != "")
                    {
                        acceptableOptions.Add(number, option);
                    }
                    number++;
                }
            }

            int Ask()
            {
                int? foundKey = null;
                while (foundKey == null)
                {
                    Display();

                    string answer = Console.ReadLine();
                    bool parseWorked = int.TryParse(answer, out int number);

                    if (parseWorked)
                    {
                        if (acceptableOptions.ContainsKey(number))
                        {
                            foundKey = number;
                        }
                        else
                        {
                            SayWrongResponse();
                        }
                    }
                    else
                    {
                        SayWrongResponse();
                    }

                    Console.Clear();
                }

                return (int)foundKey;

                void Display()
                {
                    Console.Clear();

                    Console.WriteLine(FormatString(headerMessage) + "\n\n\n");

                    foreach (KeyValuePair<int, string> acceptableOp in acceptableOptions)
                    {
                        Console.WriteLine("[" + acceptableOp.Key + "] " + acceptableOp.Value + "\n");
                    }

                }
            }

            int ConvertToIndex(int requestedNumber)
            {
                return (requestedNumber - 1);
            }
        }
        public string RandomFromList(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            return list[rnd.Next(0, list.Count)];
        }
        public void SayWrongResponse()
        {
            Write(RandomFromList(invalidInputResponses));
        }
        public void ResetCursor()
        {
            Console.SetCursorPosition(0, 0);
        }
        public ConsoleKey? GetInput()
        {
            ConsoleKey? consoleKey = null;

            while (Console.KeyAvailable)
            {
                consoleKey = Console.ReadKey(true).Key;
            }

            return consoleKey;
        }
        public void CancelRemainingInput()
        {
            while (Console.KeyAvailable)
            {
               Console.ReadKey(true);
            }
        }
    }
}
