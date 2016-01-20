using System;
using System.IO;
using TagLib.Mpeg;

namespace AudioTagByFolder
{
    internal class Program {
        private static void Main(string[] args) {
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory());

            RenameRecursive(root);

            Console.WriteLine("Press key to exit");
            Console.ReadLine();
        }

        private static void RenameRecursive(DirectoryInfo dir) {
            foreach (FileInfo file in dir.GetFiles()) {
                if (file.Extension.Equals(".mp3") || file.Extension.Equals(".wma")) {
                    try {
                        RenameOneFile(file, dir.Name);
                    }
                    catch (Exception e) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.ResetColor();
                    }
                }
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories()) {
                RenameRecursive(subDir);
            }
        }


        private static void RenameOneFile(FileInfo file, string caption) {
            try {
                AudioFile audioFile = new AudioFile(file.FullName) {
                    Tag = {Album = caption, Artists = new[] {caption}, AlbumArtists = new[] {caption}}
                };
                audioFile.Save();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Change tag name to [" +caption+"] for [" + file.Name + "] success.");
                Console.ResetColor();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error change tag name for [" + file.Name + "]");
                Console.ResetColor();
            }
        }
    }
}