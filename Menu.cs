using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

// можно сделать базовый класс 
// class Param {}
// и от него наследовать 
// class ParamInt : class Param
// { public int intParam {get; set;}}
// вместо object будет Param

namespace StoreLib
{
    public class Param { }
    public class Menu
    {
        public class Command
        {
            public const int SeparatorValue = 99;
            public string Text { get; }
            public int Value { get; }
            public Action<Param> method;
            public Func<Param> getParam;
            public Command(string text, int value, 
                Action<Param> method, Func<Param> getParam) 
                => (Text, Value, this.method, this.getParam) 
                = (text, value, method, getParam);
        }
        const string menuSeparator = "================";
        const string separator = "------";
        const string prompt = "--->: ";
        const string menuPrompt = "Choose command:";
        const string valueError = "No such command!";
        const string numberError = "Not a number!";
        List<Command> commands;
        
        public Menu(Func<Menu.Command[]> initMenu)
        {
            commands = new List<Command>(initMenu?.Invoke());
            AlignText();
        }
        void AlignText() => commands = commands.Select
                (cmd => new Command
                    (cmd.Text.PadRight(commands.Max(cmd => cmd.Text.Length)), 
                    cmd.Value, cmd.method, cmd.getParam)
                ).ToList<Command>();
        public void Run() { while (GetCommand()) {} }
        public bool GetCommand()
        {
            WriteLine(menuSeparator);
            WriteLine(menuPrompt);
            WriteLine(separator);
            foreach (var it in commands)
            {
                if (it.Value == Command.SeparatorValue)
                {
                    WriteLine(separator);
                }
                else
                {
                    WriteLine($"{it.Text}{it.Value,3}");
                }
            }
            int value = GetInt(prompt);
            Command command;
            while((command = FindCommand(value)) is null)
            {
                WriteLine(valueError);
                value = GetInt(prompt);
            }
            if (command.method is null)
            {
                return false;
            }
            Param? param = command.getParam == null ? null 
                : command.getParam();
            command.method?.Invoke(param);
            return true;
        }
        public Command? FindCommand(int value)
            => commands.FirstOrDefault(cmd => cmd.Value == value);
        
        public static int GetInt(string prompt)
        {
            int value;
            Write(prompt);
            bool correct = int.TryParse(Console.ReadLine(), out value);
            while(!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = int.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static double GetDouble(string prompt)
        {
            double value;
            Write(prompt);
            bool correct = double.TryParse(Console.ReadLine(), out value);
            while (!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = double.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static decimal GetDecimal(string prompt)
        {
            decimal value;
            Write(prompt);
            bool correct = decimal.TryParse(Console.ReadLine(), out value);
            while (!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = decimal.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static string GetString(string prompt)
        {
            Write(prompt);
            return Console.ReadLine();
        }
        public static string GetPassword(string prompt)
        {
            Write(prompt);
            string password = string.Empty;
            while(true)
            {
                ConsoleKeyInfo keyInfo = ReadKey(true); 
                switch(keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        WriteLine();
                        return password;
                    case ConsoleKey.Backspace:
                        Write("\b \b");
                        password = password.Substring(0, password.Length - 1);
                        break;
                    default:
                        password += keyInfo.KeyChar;
                        Write("*");
                        break;
                }
            }
        }
    }

    public static class AddExtension
    {
        const string numberError = "Not a number!";
        public static string Read(this string prompt)
        {
            Write(prompt);
            return Console.ReadLine();
        }
        public static int ReadInt(this string prompt)
        {
            int value;
            Write(prompt);
            bool correct = int.TryParse(Console.ReadLine(), out value);
            while (!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = int.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static double ReadDouble(this string prompt)
        {
            double value;
            Write(prompt);
            bool correct = double.TryParse(Console.ReadLine(), out value);
            while (!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = double.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static decimal ReadDecimal(this string prompt)
        {
            decimal value;
            Write(prompt);
            bool correct = decimal.TryParse(Console.ReadLine(), out value);
            while (!correct)
            {
                WriteLine(numberError);
                Write(prompt);
                correct = decimal.TryParse(Console.ReadLine(), out value);
            }
            return value;
        }
        public static string ReadPassword(this string prompt)
        {
            Write(prompt);
            string password = string.Empty;
            while (true)
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        WriteLine();
                        return password;
                    case ConsoleKey.Backspace:
                        Write("\b \b");
                        password = password.Substring(0, password.Length - 1);
                        break;
                    default:
                        password += keyInfo.KeyChar;
                        Write("*");
                        break;
                }
            }
        }
    }
}
