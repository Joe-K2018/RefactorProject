using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
        static string Path = @"C:\Users\jkoch\Documents\Development_Practic\";
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

            try
            {
                var newFile = fullFilePath.Create();
                newFile.Close();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.WriteLine("Create file failed");
                Console.WriteLine(ex.Message);
            }
        }

        public void Write(FileInfo fullFilePath, string text, bool writeToConsole = true)
        {
            if (!fullFilePath.Exists)
            {
                Console.WriteLine("File does not exist");
                return;
            }

            if (writeToConsole)
                Console.WriteLine($"Writing to file: \"{text}\"");

            try
            {
                using (var writer = new StreamWriter(fullFilePath.ToString(), true))
                {
                    writer.WriteLine(text);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Write to file failed");
                Console.WriteLine(ex);
            }            
        }

        public string Read(FileInfo fullFilePath, bool writeToConsole = true)
        {
            try
            {
                using (var stream = fullFilePath.OpenRead())
                {
                    var length = (int)stream.Length;
                    var buffer = new byte[length];
                    int count;
                    var sum = 0;
                    while ((count = stream.Read(buffer, sum, length - sum)) > 0) 
                    {
                        sum += count;
                    }

                    var encode = new UTF8Encoding(true);
                    var readText = encode.GetString(buffer);

                    if (writeToConsole)
                        Console.WriteLine(readText);

                    return readText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("File read failed");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Delete(FileInfo fullFilePath, bool writeToConsole = true)
        {
            if (writeToConsole)
                Console.WriteLine($"Deleting {fullFilePath}");

            try
            {
                fullFilePath.Delete();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.WriteLine("Delete file failed");
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteDateTime(FileInfo fullFilePath, bool writeToConsole = true)
        {
            if (!fullFilePath.Exists)
            {
                Console.WriteLine("File does not exist");
                return;
            }

            if (writeToConsole)
                Console.WriteLine("Writing DateTime");

            using (StreamWriter writer = fullFilePath.AppendText())
            {
                var currentDateTime = DateTime.Now;
                writer.WriteLine(currentDateTime);
            }
        }
    }
}
