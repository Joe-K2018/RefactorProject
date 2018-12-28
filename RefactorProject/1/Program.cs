using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProject
{
    class ProgramOld1
    {
        static string Path = @"C:\Users\jkoch\Documents\Development_Practice\";

        static void MainOld1(string[] args)
        {
            //crud operations
            //create, read, update, delete
            //wait for keypress between each step

            string fileName = "Practice";
            string fileExtension = ".txt";
            string fullFilePath = $"{Path}{fileName}{fileExtension}";
            string nameOfPerson = "Joe Koch";

            Console.WriteLine("Press any key to create file and write name");
            Console.ReadKey();

            //create text file: Name
            Console.WriteLine($"Creating file and writing \"{nameOfPerson}\"");

            using (var writer = new StreamWriter(fullFilePath, true))
            {
                writer.WriteLine(nameOfPerson);
            }

            //read: file, print content
            Console.WriteLine("Press any key to read file");
            Console.ReadKey();

            string readText = File.ReadAllText(fullFilePath);
            Console.WriteLine(readText);

            //update: append date time on new line
            Console.WriteLine("Press any key to write date to new line in file");
            Console.ReadKey();

            var currentDateTime = DateTime.Now;
            Console.WriteLine($"Writing \"{currentDateTime}\"");
            using (var writer = new StreamWriter(fullFilePath, true))
            {
                writer.WriteLine(currentDateTime);
            }

            //read: file, print content
            Console.WriteLine("Press any key to read file");
            Console.ReadKey();

            readText = File.ReadAllText(fullFilePath);
            Console.WriteLine(readText);

            //Delete file
            Console.WriteLine("Press any key to delete file");
            Console.ReadKey();

            Console.WriteLine("Deleting file");
            File.Delete(fullFilePath);

            //Close Program
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
