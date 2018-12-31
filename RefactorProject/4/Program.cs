using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProject
{
    public static class Extensions
    {
        public static string Action(this PromptType promptType)
        {
            if (promptType == PromptType.Create)
                return "create file";

            if (promptType == PromptType.Read)
                return "read file";

            if (promptType == PromptType.Write)
                return "write text";

            if (promptType == PromptType.Delete)
                return "delete file";

            if (promptType == PromptType.Close)
                return "close program";

            return "";
        }
    }

    public enum PromptType { Create, Read, Write, Delete, Close };

    class Program
    {
        static string Path = @"C:\Users\jkoch\Documents\Development_Practice\";
        static string FileName = "Practice";
        static string FileExtension = ".txt";
        static string FullFilePath = $"{Path}{FileName}{FileExtension}";
        static string NameOfPerson = "Joe Koch";
        static string PromptText = $"Press any key to ";

        static void Main(string[] args)
        {
            var program = new Program();

            // Create file
            program.ConsolePrompt(PromptType.Create);
            program.Create(FullFilePath, true);

            //Write name to file
            program.ConsolePrompt(PromptType.Write);
            program.Write(FullFilePath, NameOfPerson, true);

            //read: file, print content to console
            program.ConsolePrompt(PromptType.Read);
            string readText = program.Read(FullFilePath, true);

            //update: append date time on new line
            program.ConsolePrompt(PromptType.Write);
            program.WriteDateTime(FullFilePath, true);

            //read: file, print content
            program.ConsolePrompt(PromptType.Read);
            readText = program.Read(FullFilePath, true);

            //Delete file
            program.ConsolePrompt(PromptType.Delete);
            program.Delete(FullFilePath, true);

            //Close Program
            program.ConsolePrompt(PromptType.Close);
        }

        public void ConsolePrompt(PromptType promptType)
        {
            Console.WriteLine($"{PromptText}{promptType.Action()}");
            Console.ReadKey();
        }

        public void Create(string fullFilePath, bool writeToConsole = false)
        {
            if (writeToConsole)
                Console.WriteLine($"Creating file: {fullFilePath}");

            var newFile = File.Create(fullFilePath);
            newFile.Close();
        }
        public void Write(string fullFilePath, string text, bool writeToConsole = false)
        {
            if (writeToConsole)
                Console.WriteLine($"Writing to file: \"{text}\"");

            using (var writer = new StreamWriter(fullFilePath, true))
            {
                writer.WriteLine(text);
            }
        }

        public string Read(string fullFilePath, bool writeToConsole = false)
        {
            string readText = File.ReadAllText(fullFilePath);
            if (writeToConsole)
                Console.WriteLine(readText);

            return readText;
        }

        public void Delete(string fullFilePath, bool writeToConsole = false)
        {
            if (writeToConsole)
                Console.WriteLine($"Deleting {fullFilePath}");

            File.Delete(FullFilePath);
        }

        public void WriteDateTime(string fullFilePath, bool writeToConsole = false)
        {
            if (writeToConsole)
                Console.WriteLine("Writing DateTime");

            using (var writer = new StreamWriter(fullFilePath, true))
            {
                var currentDateTime = DateTime.Now;
                writer.WriteLine(currentDateTime);
            }
        }
    }
}
