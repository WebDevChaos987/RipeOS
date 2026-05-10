using System;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RipeOS;

class Program
{
    static void Main()
    {
        // --- STARTUP VERIFICATION ---
        // This locks the OS immediately if a code exists
        if (File.Exists("code.txt"))
        {
            string savedCode = File.ReadAllText("code.txt");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine("--- RipeOS SECURE LOGIN ---");
            Console.Write("Enter System Code: ");
            string loginAttempt = Console.ReadLine();

            if (loginAttempt != savedCode)
            {
                Console.WriteLine("ACCESS DENIED. System Locking...");
                Thread.Sleep(2000);
                return; // Hard shutdown if password is wrong
            }
        }

        // --- BOOT SEQUENCE ---
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        string[] commands = { ".help", "esc", ".CBG;Red", ".CBG;Blue", ".CBG;DG", ".CBG;Grey", ".CBG;Black", ".CBG;Cyan", ".CFG;Red", ".CFG;Blue", ".CFG;DG", ".CFG;Grey", ".CFG;Black", ".CFG;Cyan", "cts", "--version", "open;YT", ".browser" };
        bool System_Running = true;
        string version = "1.0";

        string ripe_logo = @"
        /============\
            RipeOS
        \============/
        ";

        Console.WriteLine($"{ripe_logo}");
        Thread.Sleep(3000);

        // --- MAIN OS LOOP ---
        while (System_Running)
        {
            Console.WriteLine("RipeOS");
            Console.WriteLine($"Version: {version} | Assistance: .help | Preferences: .settings");
            Console.WriteLine();
            Console.Write(">_ ");
            string command = Console.ReadLine();

            // Background Colors
            if (command == ".CBG;Red") { Console.BackgroundColor = ConsoleColor.Red; Console.Clear(); }
            if (command == ".CBG;Blue") { Console.BackgroundColor = ConsoleColor.Blue; Console.Clear(); }
            if (command == ".CBG;DG") { Console.BackgroundColor = ConsoleColor.DarkGreen; Console.Clear(); }
            if (command == ".CBG;Grey") { Console.BackgroundColor = ConsoleColor.Gray; Console.Clear(); }
            if (command == ".CBG;Black") { Console.BackgroundColor = ConsoleColor.Black; Console.Clear(); }
            if (command == ".CBG;Cyan") { Console.BackgroundColor = ConsoleColor.Cyan; Console.Clear(); }

            // Foreground Colors
            if (command == ".CFG;Red") { Console.ForegroundColor = ConsoleColor.Red; }
            if (command == ".CFG;Blue") { Console.ForegroundColor = ConsoleColor.Blue; }
            if (command == ".CFG;DG") { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            if (command == ".CFG;Grey") { Console.ForegroundColor = ConsoleColor.Gray; }
            if (command == ".CFG;Black") { Console.ForegroundColor = ConsoleColor.Black; }
            if (command == ".CFG;Cyan") { Console.ForegroundColor = ConsoleColor.Cyan; }

            // General Commands
            if (command == ".help") { Console.WriteLine("Available Commands: " + string.Join(", ", commands)); }

            if (command == "cts")
            {
                Console.Clear();
                Console.WriteLine("RipeOS");
                Console.WriteLine($"Version: {version} | Assistance: .help | Preferences: .settings");
            }

            if (command == "--version") { Console.WriteLine(version); }

            // Web Commands
            if (command == "open;YT")
            {
                Console.WriteLine("Opening YouTube...");
                Process.Start(new ProcessStartInfo("https://www.youtube.com") { UseShellExecute = true });
            }

            if (command == ".browser")
            {
                Console.Write("Ripe-Web Search: ");
                string search = Console.ReadLine();
                string url = "https://www.google.com/search?q=" + search.Replace(" ", "+");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }

            // --- SHUTDOWN AND ACCOUNT MANAGEMENT ---
            if (command == "esc")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                // 1. Check if a code actually exists in 'memory'
                if (File.Exists("code.txt"))
                {
                    string masterCode = File.ReadAllText("code.txt");
                    Console.Write("Enter Current System Code to manage settings: ");
                    string verify = Console.ReadLine();

                    if (verify != masterCode)
                    {
                        Console.WriteLine("ACCESS DENIED. Returning to OS...");
                        Thread.Sleep(2000);
                        continue; // Goes back to the start of the while loop
                    }
                }

                // 2. Verified or New System - Show Management Options
                Console.WriteLine("--- System Management ---");
                Console.WriteLine("Type 'new' to change code, or 'off' to shutdown.");
                Console.Write("> ");
                string managementChoice = Console.ReadLine();

                if (managementChoice == "new")
                {
                    Console.Write("Enter New System Code: ");
                    string newCode = Console.ReadLine();
                    if (newCode == "000" || string.IsNullOrEmpty(newCode))
                    {
                        Console.WriteLine("Invalid Code choice.");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        File.WriteAllText("code.txt", newCode);
                        Console.WriteLine("Code Updated. Shutting down to apply changes...");
                        System_Running = false;
                    }
                }
                else if (managementChoice == "off")
                {
                    System_Running = false;
                }
            }

            Thread.Sleep(100);
        }
    }
}
