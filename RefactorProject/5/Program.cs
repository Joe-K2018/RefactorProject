using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProject
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return genericEnumType.ToString();
        }
    }

    public enum PromptType
    {
        [Description("Create file")]
        Create,

        [Description("Read file")]
        Read,

        [Description("Write text")]
        Write,

        [Description("Delete file")]
        Delete,

        [Description("Close program")]
        Close

    }

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
            Console.WriteLine($"Press any key to {promptType.GetDescription()}");
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
