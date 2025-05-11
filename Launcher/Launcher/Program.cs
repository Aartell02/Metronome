using System.Diagnostics;

namespace Launcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz aplikację do uruchomienia:");
            Console.WriteLine("1 - Metronom Konsolowy");
            Console.WriteLine("2 - Metronom Graficzny");

            string input = Console.ReadLine();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string exePath = "";
            string solutionRoot = Path.GetFullPath(Path.Combine(basePath, "..", "..", "..",".."));

            switch (input)
            {
                case "1":
                    exePath = Path.Combine(solutionRoot, "Metronome", "bin", "Debug", "net8.0", "Metronome.exe");
                    break;
                case "2":
                    exePath = Path.Combine(solutionRoot, "MetronomeGraphic", "bin", "Debug", "MetronomeGraphic.exe");
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    return;
            }

            if (File.Exists(exePath))
            {
                Process.Start(exePath);
            }
            else
            {
                Console.WriteLine("Nie znaleziono pliku: " + exePath);
            }
        }
    }
}
