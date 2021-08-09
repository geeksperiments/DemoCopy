using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DemoCopy
{
    class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Option(ShortName ="s", LongName ="source",Description = "Source Folder")]
        [Required]
        [DirectoryExists]
        public string Source { get; } 

        [Option(ShortName = "d", LongName = "destination", Description = "Destination Folder")]
        public string Destination { get; }

        private void OnExecute()
        {
            Console.WriteLine($"Copying contents from: {Path.GetFullPath(Source)}");
            Console.WriteLine($"Copying contents to: {Path.GetFullPath(Destination)}");

            CopyFilesRecursively(Source, Destination);
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
    }
}
