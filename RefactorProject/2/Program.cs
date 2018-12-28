using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProject
{
    class Program
    {
        static string Path = @"C:\Users\jkoch\Documents\Development_Practice\";
        static string FileName = "Practice";
        static string FileExtension = ".txt";
        static string FullFilePath = $"{Path}{FileName}{FileExtension}";
        static string NameOfPerson = "Joe Koch";

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
        public enum PromptType { Create, Read, Write, Delete, Close }
        public void ConsolePrompt(PromptType promptType)
        {
            if (promptType == PromptType.Create)
                Console.WriteLine("Press any key to create file");

            if (promptType == PromptType.Read)
                Console.WriteLine("Press any key to read file");

            if (promptType == PromptType.Write)
                Console.WriteLine("Press any key to write text");

            if (promptType == PromptType.Delete)
                Console.WriteLine("Press any key to delete file");

            if (promptType == PromptType.Close)
                Console.WriteLine("Press any key to close program");

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
