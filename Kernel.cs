 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;
using System.Threading;
using System.Linq;
using System.Reflection.Emit;
using Cosmos.System.Graphics;
using System.Drawing;
namespace AIAPI
{
    public static class General
    {
        public static void writeAsAI(string line)
        {
            foreach(char c in line)
            {
                Console.Write(c);
                Thread.Sleep(1);
            }
            Console.WriteLine();
        }
    }
}

namespace workplease
{
    
    public class Kernel : Sys.Kernel
    {
        public static string osname = "GHJOS";
        public static string osver = "Origin 3.6 LTS";
        public static string username = "Test";
        public static string pathvar = @"0:\";
        public static string[] shellinput;
        public static string machinename = null;
        public static bool shell = false;
        public static int line = 0;
        public static string loggeduser;
        public static bool conhostactive = true;
       public static bool echo = true;
        public static int pid1;
        public static int pid2;
        public static int pid3;
        private static readonly string removableDiskLetter = "R:";
        private static readonly string removableDiskPath = @"R:\";


        protected override void BeforeRun()
        {
            System.Cosmos.ConsoleUtils.UpdateDate();
            AIAPI.General.writeAsAI("Network = done, Waiting for press any key...");
            Console.ReadKey();
            AIAPI.General.writeAsAI("Key done, clearing screen....");
            Console.Clear();
            AIAPI.General.writeAsAI("Changing foreground color....");
            Console.ForegroundColor= ConsoleColor.Gray;
            AIAPI.General.writeAsAI("Initializing VFS filesystem....");
            //      ConsoleUtils.StartClockTimer();
            System.Cosmos.ConsoleUtils.UpdateDate();
            var fs = new Sys.FileSystem.CosmosVFS();
            try
            {
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                AIAPI.General.writeAsAI("FileSystem registered successfully!");
            }
            catch(Exception ex)
            {
                AIAPI.General.writeAsAI("Excpeted error!");
                OSApi.BSOD(ex.Message,"0x"+new Random().Next(20));
            }
           
            System.Cosmos.ConsoleUtils.UpdateDate();
            
            try
            {
                if (!Directory.Exists(@"0:\OS\"))
                {
                    Directory.CreateDirectory(@"0:\OS\");
                }
            }
            catch
            {
                Console.WriteLine("Siema! Widze ze probujesz uruchomic GHJOS");
                Console.WriteLine("Jednak nie uruchomiles go z odpowiednim dyskiem!");
                Console.WriteLine("Uzyj dysku z https://github.com/Werokowy/GHJOS (sekcja README)\nDysk zamontuj na IDE. Zycze  milej zabawy!");
                Console.ReadKey();
                Sys.Power.Reboot();
            }

            try
            {
                if (!Directory.Exists(@"0:\OS\"))
                {
                }
                Console.WriteLine("Worked");
            }
            catch
            {
                Console.WriteLine("if tez nie dziala");
            }
            System.Cosmos.ConsoleUtils.UpdateDate();
            if (!File.Exists(@"0:\OS\installed.reg"))
            {
                goto intro;
            intro:
                Console.WriteLine("GHJOS Operating System Setup v1.0");
                Console.WriteLine(" ");
                Thread.Sleep(3000);
                goto info;
            info:
                Console.WriteLine("Before we install the system, we need to set some information...");
                goto step1;
            step1:
                Console.WriteLine("If you recordning, you don't need to censore the product key (OS is open on a github)");
                Console.WriteLine("Please enter now product key:");

                Console.Write("Product key: ");
                var key = Console.ReadLine();
                if (key == "C746-FBF1-ECB4-9CB5-1B5E1")
                {
                    goto step2;
                }
                else
                {
                    Console.WriteLine("Invalid product key!");
                    goto step1;
                }
            step2:
                Console.WriteLine("OK, product activated. Now select your country!");
                Console.Write("Country: ");
                var country = Console.ReadLine();
                Console.WriteLine("Summary:");
                Console.WriteLine("Product key - OK");
                Console.WriteLine("Country - " + country);
                goto prepair;
            prepair:
                Console.WriteLine("Now we will install the system on this device, click any key");
                Console.ReadKey();
                int percent = 0;
                while (true)
                {
                    if (percent == 100)
                    {

                        goto install;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    percent++;
                    Console.WriteLine("If computer stop responding please restart it manual");
                    Console.WriteLine("Gdy wartosc stanie i nie bedzie sie zmieniac przez dluga ilosc czasu zrestartuj komputer recznie");
                    Console.WriteLine("Prepairing files to install (" + percent + "%)...");
                    Console.WriteLine("Installing (0%)");
                    Console.WriteLine("Updating (0%)");
                    Console.WriteLine("Completing (0%)");
                    Thread.Sleep(50);
                }
                System.Cosmos.ConsoleUtils.UpdateDate();
            install:
                percent = 0;
                while (true)
                {
                    if (percent == 100)
                    {

                        goto update;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();
                    percent++;
                    Console.WriteLine("Prepairing files to install (100%)...");
                    Console.WriteLine("Installing (" + percent + "%)");
                    Console.WriteLine("Updating (0%)");
                    Console.WriteLine("Completing (0%)");
                    Thread.Sleep(300);
                }
            update:
                percent = 0;
                while (true)
                {
                    if (percent == 100)
                    {

                        goto complete;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    percent++;
                    Console.WriteLine("Prepairing files to install (100%)...");
                    Console.WriteLine("Installing (100%)");
                    Console.WriteLine("Updating (" + percent + "%)");
                    Console.WriteLine("Completing (0%)");
                    Thread.Sleep(200);
                }
            complete:
                percent = 0;
                while (true)
                {
                    if (percent == 100)
                    {

                        goto finish;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    percent++;
                    Console.WriteLine("Prepairing files to install (100%)...");
                    Console.WriteLine("Installing (100%)");
                    Console.WriteLine("Updating (100%)");
                    Console.WriteLine("Completing (" + percent + "%)");
                    Thread.Sleep(100);
                }
            finish:
                Console.WriteLine("Instalation finished!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                File.Create(@"0:\OS\installed.reg");
                Console.WriteLine("System will be rebooted in 5 seconds...");
                Thread.Sleep(5000);
                Sys.Power.Reboot();
            }
            pid1 = new Random().Next(246762);

            pid2 = new Random().Next(246762);
            pid3 = new Random().Next(246762);
            Console.Clear();
            if (File.Exists("0:\\OS\\unbootable.bin"))
            {
                Console.WriteLine("System can't boot!");
                Console.WriteLine("GHJ Disk Partition Recorver v1.0");
                Console.WriteLine(" ");
                Thread.Sleep(3600);
                Console.WriteLine("A critical error has been detected preventing the system from booting! \nGHJOS Bootloader is unable to find system boot files or they are broken.");
                Console.WriteLine("Attempting automatic repair...");
                Thread.Sleep(4000);
                int percent = 0;
                while (true)
                {
                    if (percent == 100)
                    {
                        Console.WriteLine("Drive has been repaired but data may be corrupted! (Press any key to continue...)");
                        Console.ReadKey();
                        File.Delete("0:\\OS\\unbootable.bin");
                        Sys.Power.Reboot();
                        break;
                    }
                    Console.Clear();
                    percent++;
                    Console.WriteLine("System can't boot!");
                    Console.WriteLine("GHJ Disk Partition Recorver v1.0");
                    Console.WriteLine(" ");

                    Console.WriteLine("A critical error has been detected preventing the system from booting! \nGHJOS Bootloader is unable to find system boot files or they are broken.");
                    Console.WriteLine("Attempting automatic repair...");

                    Console.WriteLine("Repairing disk (" + percent + "%)...");
                    Thread.Sleep(750);
                }

            }
            System.Cosmos.ConsoleUtils.UpdateDate();
            if ((Cosmos.Core.CPU.GetAmountOfRAM() + 2) > 3070)
            {
                OSApi.changeBgColor(ConsoleColor.DarkRed);
                OSApi.changeTextColor(ConsoleColor.Gray);
                Console.WriteLine("GHJOS does not support this high amount of RAM");
                Console.WriteLine("Please remove some ram from your computer and try again");
                Console.WriteLine();
                Console.WriteLine("Maximal RAM for this OS: 3065MB");
                Console.WriteLine("Exception ID: 0x002");
                Console.WriteLine();
                Console.WriteLine(new string(' ', 54));
                Console.WriteLine("This information has been reported by OSKernel");
                Console.WriteLine(new string(' ', 54));
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Click any key to show BSOD screen...");
                Console.ReadKey();
                OSApi.BSOD("TOO_HIGH_RAM", "0x002");
            }
            for (int i = 0; i < 3; i++)
            {
                OSApi.showFitleg();
                Console.Beep();
                Console.WriteLine("OS Name: " + osname);
                Console.WriteLine("OS Version: " + osver);
                Console.WriteLine();
                Console.WriteLine();
                if (i == 0)
                {
                    LoginSystem.LogIn();
                }
                else if (i == 1)
                {
                    LoginSystem.registerMachineName();
                }
                Console.Clear();
            }
            Console.Write("Press any key...");
            Console.ReadLine();
            Console.Clear();
            OSApi.showFitleg();
            Console.Beep();
            Console.WriteLine("OS Name: " + osname);
            Console.WriteLine("OS Version: " + osver);
            Console.WriteLine("    ");
            Console.WriteLine("    ");

        }
        protected override void Run()
        {
            System.Cosmos.ConsoleUtils.UpdateDate();
            try
            {
                
                if (loggeduser == null)
                {
                    OSApi.BSOD("INVALID_USER_EXCEPTION", "0x021");
                }
                if (conhostactive == false)
                {
                    for (int i = 0; i < 2500; i++)
                    {
                        Console.WriteLine("Could not initialize console graphics monitor (Attempt #" + i + ")");
                    }
                    OSApi.BSOD("INITIALIZE_GRAPHICS_ERROR", "0x023");
                    return;
                }
                if (echo == true)
                {
                    Console.Write(username + "@");
                    Console.Write(machinename);
                    Console.Write("> ");
                }
                string input = null;
                if (shell == true)
                {
                    while (true)
                    {
                        input = shellinput[line];
                        line++;
                        break;
                    }
                }
                if (shell == false)
                {
                    input = Console.ReadLine();
                }
                InputandComands.Interprethedor(input);
            }
            catch (Exception e)
            {
                Console.WriteLine("E: " + e.Message);
                return;
            }
        }

        public static void formatdrive(string filesys, string drive)
        {
            Console.WriteLine("Formatting... Please wait...");
            Thread.Sleep(1600);
            Console.WriteLine("New filesystem: " + filesys);
            Thread.Sleep(500);
            Console.WriteLine("Selected drive: " + drive);
            Thread.Sleep(2000);
            int percent = 0;
            while (true)
            {
                if (percent == 100)
                {
                    Console.WriteLine("Formatted! (Press any key to continue...)");
                    Console.ReadKey();
                    File.Create("0:\\OS\\unbootable.bin");
                    Console.WriteLine("A reboot is required to success format!\nPress any key to reboot...");
                    Console.ReadKey();
                    Sys.Power.Reboot();
                    break;
                }
                Console.Clear();
                percent++;
                Console.WriteLine("Formatting (" + percent + "%)...");
                Thread.Sleep(50);
            }
        }
        public static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return input.ToString();
        }




    }

} 
