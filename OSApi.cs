using System;
using Sys = Cosmos.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace workplease
{
    public static class OSApi
    {
        public static void throwEx()
        {
            BSOD("CRITICAL_PROCESS_DIED", "0x005");
        }

        public static void showFitleg()
        {
            Console.WriteLine(@"  /$$$$$$  /$$   /$$    /$$$$$  /$$$$$$   /$$$$$$ ");
            Console.WriteLine(@" /$$__  $$| $$  | $$   |__  $$ /$$__  $$ /$$__  $$");
            Console.WriteLine(@"| $$  \__/| $$  | $$      | $$| $$  \ $$| $$  \__");
            Console.WriteLine(@"| $$ /$$$$| $$$$$$$$      | $$| $$  | $$|  $$$$$$");
            Console.WriteLine(@"| $$|_  $$| $$__  $$ /$$  | $$| $$  | $$ \____  $$");
            Console.WriteLine(@"| $$  \ $$| $$  | $$| $$  | $$| $$  | $$ /$$  \ $$");
            Console.WriteLine(@"|  $$$$$$/| $$  | $$|  $$$$$$/|  $$$$$$/|  $$$$$$/");
            Console.WriteLine(@" \______/ |__/  |__/ \______/  \______/  \______/ ");
        }
        public static void changeTextColor(ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
        }
        public static void changeBgColor(ConsoleColor cc)
        {
            Console.BackgroundColor = cc;
        }


        public static void CopyFiles(string folderFrom, string folderTo)
        {
            DirectoryInfo dirFrom = new DirectoryInfo(folderFrom);
            DirectoryInfo dirTo = new DirectoryInfo(folderTo);
            foreach (FileInfo file in dirFrom.GetFiles())
            {
                string destPath = Path.Combine(dirTo.FullName, file.Name);
                file.CopyTo(destPath, true);
                AIAPI.General.writeAsAI("Copyed the " + file.Name + " from " + file.FullName + " to " + destPath);
            }
        }

        public static void BSOD(string reason, string exid)
        {
            const string title = "Wystapil nieoczekiwany blad systemu GHJOS!";
            const string waitMessage = "Prosze czekac...";
            const string restartPrompt = "Czy chcesz zrestartowac komputer? (Y/N): ";
            const string manualShutdownMessage = "Komputer nalezy wylaczyc recznie";

            Console.Clear();
            changeBgColor(ConsoleColor.DarkBlue);
            changeTextColor(ConsoleColor.White);
            showFitleg();
            Console.WriteLine(title);
            Console.WriteLine();
            Console.WriteLine($"Powod bledu: {reason}");
            Console.WriteLine($"ID bledu: {exid}");
            Console.WriteLine();
            Console.WriteLine(waitMessage);
            Console.WriteLine();
            Console.Beep(500, 2000);
            Console.Write(restartPrompt);

            var input = Console.ReadLine()?.ToLowerInvariant();

            if (input == "y" || input == "yes" || input == "tak")
            {
                Sys.Power.Reboot();
            }
            else if (input == "n" || input == "no" || input == "nie")
            {
                Sys.Power.Shutdown();
                Console.WriteLine(manualShutdownMessage);
            }
            else
            {
                BSOD(reason, exid);
            }
        }

    }
}
