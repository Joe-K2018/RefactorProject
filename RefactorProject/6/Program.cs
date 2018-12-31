using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

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
        static FileInfo FullFilePath = new FileInfo($"{Path}{FileName}{FileExtension}");
        static string NameOfPerson = "Joe Koch";
        static string PromptText = $"Press any key to ";

        static void Main(string[] args)
        {
            var program = new Program();

            // Create file
            program.ConsolePrompt(PromptType.Create);
            program.Create(FullFilePath);

            //Write name to file
            program.ConsolePrompt(PromptType.Write);
            program.Write(FullFilePath, NameOfPerson);

            //read: file, print content to console
            program.ConsolePrompt(PromptType.Read);
            string readText = program.Read(FullFilePath);

            //update: append date time on new line
            program.ConsolePrompt(PromptType.Write);
            program.WriteDateTime(FullFilePath);

            //read: file, print content
            program.ConsolePrompt(PromptType.Read);
            readText = program.Read(FullFilePath);

            //Delete file
            program.ConsolePrompt(PromptType.Delete);
            program.Delete(FullFilePath);

            //Close Program
            program.ConsolePrompt(PromptType.Close);
        }

        public void ConsolePrompt(PromptType promptType)
        {
            Console.WriteLine($"Press any key to {promptType.GetDescription()}");
            Console.ReadKey();
        }

        public void Create(FileInfo fullFilePath, bool writeToConsole = true)
        {
            if (writeToConsole)
                Console.WriteLine($"Creating file: {fullFilePath}");

            var newFile = File.Create(fullFilePath.ToString());
            newFile.Close();
        }
        public void Write(FileInfo fullFilePath, string text, bool writeToConsole = true)
        {
            if (writeToConsole)
                Console.WriteLine($"Writing to file: \"{text}\"");

            using (var writer = new StreamWriter(fullFilePath.ToString(), true))
            {
                writer.WriteLine(text);
            }
        }

        public string Read(FileInfo fullFilePath, bool writeToConsole = true)
        {
            string readText = File.ReadAllText(fullFilePath.ToString());
            if (writeToConsole)
                Console.WriteLine(readText);

            return readText;
        }

        public void Delete(FileInfo fullFilePath, bool writeToConsole = true)
        {
            if (writeToConsole)
                Console.WriteLine($"Deleting {fullFilePath}");

            File.Delete(FullFilePath.ToString());
        }

        public void WriteDateTime(FileInfo fullFilePath, bool writeToConsole = true)
        {
            if (writeToConsole)
                Console.WriteLine("Writing DateTime");

            using (var writer = new StreamWriter(fullFilePath.ToString(), true))
            {
                var currentDateTime = DateTime.Now;
                writer.WriteLine(currentDateTime);
            }
        }
    }
}
