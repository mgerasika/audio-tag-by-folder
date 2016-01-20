using System;
using System.IO;
using TagLib.Mpeg;

namespace MusicFileRename
{
    internal class Program
    {
        private static void Main(string[] args) {
            var root = new DirectoryInfo(Directory.GetCurrentDirectory());

            RenameFilesInFolder(root);
            RenameDirectory(root);

            Console.WriteLine("Press key to exit");
            Console.ReadLine();
        }

        private static void RenameDirectory(DirectoryInfo info) {
            

            foreach (var subDir in info.GetDirectories()) {
                RenameFilesInFolder(subDir);
                RenameDirectory(subDir);
            }
        }

        private static void RenameFilesInFolder(DirectoryInfo dir) {
            foreach (var file in dir.GetFiles()) {
                if (file.Extension.Equals(".mp3") || file.Extension.Equals(".wma"))
                {
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
        }

        public static void RenameOneFile(FileInfo file,string caption) {
            var audioFile = new AudioFile(file.FullName) {
                Tag = {Album = caption, Artists = new[] {caption}, AlbumArtists = new[] {caption}}
            };
            audioFile.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(file.Name );
            Console.ResetColor();
        }
    }
}