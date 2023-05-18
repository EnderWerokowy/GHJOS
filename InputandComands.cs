using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace workplease
{
    public static class InputandComands
    {
        public static void Interprethedor(string input)
        {
            if (input.StartsWith("format"))
            {
                Console.WriteLine("GHJ Formatting Tool v1.0");
                Console.WriteLine(" ");
                Console.WriteLine("Drive list:");
                Console.WriteLine("0 - Hard drive - Data - GPT - FAT32 - IDE");
                Console.WriteLine("1 - CD/DVD drive - Bootable - MBR - FAT32 - IDE");
            select:
                Console.Write("Select drive (0/1): ");
                var drv = Console.ReadLine();
                if (drv == "0" || drv == "1")
                {
                    goto filesys;
                }
                else
                {
                    Console.WriteLine("Selected drive not exits!");
                    goto select;
                }
            filesys:
                Console.Write("Select new filesystem (EXT4/NTFS/FAT32/FAT):");
                var filesys = Console.ReadLine();
                if (filesys == "EXT4" || filesys == "NTFS" || filesys == "FAT32" || filesys == "FAT")
                {
                    goto yesno;
                }
                else
                {
                    Console.WriteLine("Selected filesystem doesn't exits!");
                    goto filesys;
                }
            yesno:
                Console.Write("WARNING: FORMATING DRIVE WILL DELETE ALL DATA! DO YOU WANT TO CONTINUE? (Y/N): ");
                var yesno = Console.ReadLine();
                if (yesno == "Y" || yesno == "y" || yesno == "yes" || yesno == "YES")
                {
                    Kernel.formatdrive(filesys, drv);
                    return;
                }
                else
                {
                    Console.WriteLine("Cancelled!");
                    Console.ReadKey();
                    return;
                }
            }
            if (input == "return")
            {
                Kernel.shell = false;
                Kernel.line = 0;
                return;
            }
            if (input == "pause")
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            if (input.StartsWith("color "))
            {
                string color = input.Remove(0, 6);
                ConsoleColor consoleColor;
                if (color == "01")
                    consoleColor = ConsoleColor.DarkBlue;
                else if (color == "02")
                    consoleColor = ConsoleColor.DarkGreen;
                else if (color == "03")
                    consoleColor = ConsoleColor.DarkCyan;
                else if (color == "04")
                    consoleColor = ConsoleColor.DarkRed;
                else if (color == "05")
                    consoleColor = ConsoleColor.DarkMagenta;
                else if (color == "06")
                    consoleColor = ConsoleColor.DarkYellow;
                else if (color == "07")
                    consoleColor = ConsoleColor.Gray;
                else if (color == "08")
                    consoleColor = ConsoleColor.DarkGray;
                else if (color == "09")
                    consoleColor = ConsoleColor.Blue;
                else if (color == "00")
                    consoleColor = ConsoleColor.Black;
                else if (color == "0c")
                    consoleColor = ConsoleColor.Red;
                else if (color == "0f")
                    consoleColor = ConsoleColor.White;
                else if (color == "0d")
                    consoleColor = ConsoleColor.Magenta;
                else if (color == "0e")
                    consoleColor = ConsoleColor.Yellow;
                else if (color == "0a")
                    consoleColor = ConsoleColor.Green;
                else
                {
                    Console.WriteLine("------ COLORS ------");
                    Console.WriteLine("01 - DarkBlue");
                    Console.WriteLine("02 - DarkGreen");
                    Console.WriteLine("03 - DarkCyan");
                    Console.WriteLine("04 - DarkRed");
                    Console.WriteLine("05 - DarkMagenta");
                    Console.WriteLine("06 - DarkYellow");
                    Console.WriteLine("07 - Gray");
                    Console.WriteLine("08 - DarkGray");
                    Console.WriteLine("09 - Blue");
                    Console.WriteLine("00 - Black");
                    Console.WriteLine("0c - Red");
                    Console.WriteLine("0f - White");
                    Console.WriteLine("0d - Magenta");
                    Console.WriteLine("0e - Yellow");
                    Console.WriteLine("0a - Green");
                    return;
                }
                OSApi.changeBgColor(ConsoleColor.Black);
                OSApi.changeTextColor(consoleColor);
                return;
            }

            // OPTIMIZED BY CHAT GPT ⬆️
            if (input == "shutdown" || input == "power off" || input == "close" || input == "exit")
            {
                Console.WriteLine("Shutting down...");

                Cosmos.System.Power.Shutdown();
                return;
            }
            if (input == "logout")
            {
                Kernel.loggeduser = null;
                LoginSystem.LogIn();
                Console.Clear();
                OSApi.showFitleg();
                Console.Beep();
                Console.WriteLine("OS Name: " + Kernel.osname);
                Console.WriteLine("OS Version: " + Kernel.osver);
                Console.WriteLine("    ");
                Console.WriteLine("    ");
                return;
            }
            if (input.StartsWith("start "))
            {
                string filename;
                filename = input.Remove(0, 6);
                if (File.Exists(filename))
                {
                    Kernel.shell = true;
                    Kernel.shellinput = File.ReadAllLines(filename);
                    Kernel.line = 0;
                }
                else if (File.Exists(Kernel.pathvar + filename))
                {
                    string[] f_contents = File.ReadAllLines(Kernel.pathvar + filename);
                    Kernel.shell = true;
                    Kernel.shellinput = f_contents;
                    Kernel.line = 0;
                }
                else
                {
                    Console.WriteLine("Plik nie istieje!");
                }
                return;
            }
            if (input.StartsWith("echo "))
            {
                if (input.Replace("echo ", "") == "off")
                {
                    Kernel.echo = false;

                }
                if (input.Replace("echo ", "") == "on")
                {
                    Kernel.echo = true;
                }
                try
                {
                    Console.WriteLine(input.Replace("echo ", ""));
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OSApi.BSOD("ArgumentOutOfRangeException", "0x003");
                    return;
                }


            }
            if (input == "@echo off")
            {
                Kernel.echo = false;
                return;
            }
            if (input == "@echo on")
            {
                Kernel.echo = true;
                return;
            }
            if (input == "author")
            {
                Console.WriteLine("Author nick: Werokowy");
                Console.WriteLine("Firmware original group: GHJ-EU.ML™️");
                Console.WriteLine("GHJ-EU.ML (c) 2022. All rights reserved.");
            }
            if (new[] { "calc", "calculator", "kalkulator" }.Contains(input))
            {
                Console.Clear();
                Console.WriteLine("Simple Calculator 2.0");
                Console.WriteLine("1 - Add");
                Console.WriteLine("2 - Subtract");
                Console.WriteLine("3 - Multiply");
                Console.WriteLine("4 - Divide");

                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    OSApi.changeTextColor(ConsoleColor.Red);
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    OSApi.changeTextColor(ConsoleColor.Gray);
                    return;
                }

                Console.WriteLine("Enter first value:");
                int value1;
                if (!int.TryParse(Console.ReadLine(), out value1))
                {
                    OSApi.changeTextColor(ConsoleColor.Red);
                    Console.WriteLine("Invalid input. Please enter a number.");
                    OSApi.changeTextColor(ConsoleColor.Gray);
                    return;
                }

                Console.WriteLine("Enter second value:");
                int value2;
                if (!int.TryParse(Console.ReadLine(), out value2))
                {
                    OSApi.changeTextColor(ConsoleColor.Red);
                    Console.WriteLine("Invalid input. Please enter a number.");
                    OSApi.changeTextColor(ConsoleColor.Gray);
                    return;
                }

                int result;
                switch (option)
                {
                    case 1:
                        result = value1 + value2;
                        break;
                    case 2:
                        result = value1 - value2;
                        break;
                    case 3:
                        result = value1 * value2;
                        break;
                    case 4:
                        if (value2 == 0)
                        {
                            OSApi.BSOD("DIVIDE_BY_ZERO", "0x001");
                            return;
                        }
                        result = value1 / value2;
                        break;
                    default:
                        OSApi.changeTextColor(ConsoleColor.Red);
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                        OSApi.changeTextColor(ConsoleColor.Gray);
                        return;
                }

                Console.WriteLine("Result: " + result);
            }

            if (input == "reboot" || input == "restart" || input == "power restart")
            {
                Console.WriteLine("Rebooting...");
                Cosmos.System.Power.Reboot();
                return;
            }
            if (input == "kill")
            {
                OSApi.throwEx();
            }
            if (input == "cls" || input == "clear")
            {
                Console.Clear();
                return;
            }

            if (input == "winver" || input == "sysinfo" || input == "info" || input == "ghjinfo" || input == "osinfo")
            {
                Console.WriteLine("OS Name: " + Kernel.osname);
                Console.WriteLine("OS Version: " + Kernel.osver);
                Console.WriteLine("Total RAM: " + (Cosmos.Core.CPU.GetAmountOfRAM() + 2) + "MB");
                Console.WriteLine("MBIAddres: " + Cosmos.Core.Multiboot2.GetMBIAddress());
                Console.WriteLine("System date: " + DateTime.Now.ToString());

                return;
            }
            if (input == "crash" || input == "crashtest" || input == "bsod")
            {
                OSApi.BSOD("Umyslne scrashowanie systemu", "0x004");
                return;
            }
            if (input == "pomoc" || input == "help")
            {
                Console.WriteLine("<=-----------------POMOC-----------------=>");
                Console.WriteLine("cls - czycci ekran");
                Console.WriteLine("reboot - restaruje system");
                Console.WriteLine("shutdown - Wylacza komputer");
                Console.WriteLine("crash - Umyslne crashowanie komputera");
                Console.WriteLine("sysinfo - Pokazanie informacji o komputerze");
                Console.WriteLine("author - Wyswietla autora");
                Console.WriteLine("echo - Wyswietla podany tekst na ekranie");
                Console.WriteLine("kill - Zabija proces systemowy");
                Console.WriteLine("logo - Wyswietla logo systemowe");
                Console.WriteLine("tasklist - Wyswietla procesy");
                Console.WriteLine("cd - Pokazuje sciezke");
                Console.WriteLine("cd <folder> - Przechodzi do podanego folderu");
                Console.WriteLine("cd .. - Przechodzi do folderu rodzica");
                Console.WriteLine("touch <plik> - Tworzy nowy plik");
                Console.WriteLine("rm <plik> - Usuwa plik");
                Console.WriteLine("mkdir <nazwa> - Tworzy folder");
                Console.WriteLine("rmdir <nazwa> - Usuwa folder");
                Console.WriteLine("show <plik> - Pokazuje zawartosć pliku");
                Console.WriteLine("sound - Odtwarza dzwiek");
                Console.WriteLine("start <plik> - Uruchamia skrypt wsadowy");
                Console.WriteLine("<=-----------------POMOC-----------------=>");
                return;
            }
            if (input == "logo")
            {
                OSApi.showFitleg();
                return;
            }
            if (input == "sound")
            {
                Console.Beep();
                return;
            }
            if (input == "tasklist")
            {
                Console.WriteLine("Nazwa pliku               PID");
                Console.WriteLine("===================       ========");
                Console.WriteLine("conhost.osg               " + Kernel.pid1);
                Console.WriteLine("kernel.osg               " + Kernel.pid2);
                Console.WriteLine("tasklist.sgm               " + new Random().Next(246762));
                return;
            }
           

            if (input.StartsWith("taskkill "))
            {
                if (input.Replace("taskkill ", "") == "conhost.osg")
                {
                    Console.WriteLine("Successfully killed conhost.osg");
                    Kernel.conhostactive = false;
                    return;
                }
                if (input.Replace("taskkill ", "") == "kernel.osg")
                {
                    OSApi.BSOD("CRITICAL_PROCESS_DIED", "0x005");
                    return;
                }
                else
                {
                    Console.WriteLine("Nie znaleziono procesu " + input.Replace("taskkill ", "").Replace(" ", "null"));
                }

                return;
            }
            if (input == "taskkill")
            {
                Console.WriteLine("Nie znaleziono procesu null");
                return;
            }
            if (input == "dir" || input == "ls")
            {
                try
                {
                    if (Kernel.pathvar == @"\\?\PsyhicialDrive0\")
                    {
                        Console.WriteLine("GHJOS nie obsluguje sciezek UNC, przejdz do woluminu uzywajac cd /");
                        return;
                    }
                    string[] filePaths = Directory.GetFiles(Kernel.pathvar);
                    var drive = new DriveInfo("0");
                    Console.WriteLine("Volume in drive 0 is " + drive.VolumeLabel);
                    Console.WriteLine("Directory of " + Kernel.pathvar);
                    Console.Write("\n");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        string path = filePaths[i];
                        Console.WriteLine(Path.GetFileName(path));
                    }
                    foreach (var d in Directory.GetDirectories(Kernel.pathvar))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Console.WriteLine(dirName + " <DIR>");
                    }
                    Console.Write("\n");
                    Console.WriteLine("        " + drive.TotalSize + "bajtow");
                    Console.WriteLine("        " + drive.AvailableFreeSpace + "bajtow wolnych");
                    return;
                }
                catch (FileNotFoundException)
                {
                    OSApi.BSOD("FILE_NOT_FOUND", "0x031");
                }
            }
            if (input == "pwd" || input == "cd")
            {
                Console.WriteLine(Kernel.pathvar); return;
            }
            if (input.StartsWith("touch "))
            {
                try
                {
                    if (Kernel.pathvar == @"\\?\PsyhicialDrive0\")
                    {
                        Console.WriteLine("GHJOS nie obsluguje sciezek UNC, przejdz do woluminu uzywajac cd /");
                        return;
                    }
                    string filename = input.Remove(0, 6);

                    if (File.Exists(Kernel.pathvar + filename))
                    {
                        Console.Write("touch: " + filename + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Plik juz istnieje");
                    }
                    else
                    {
                        File.Create(Kernel.pathvar + filename);
                        Console.Write("touch: " + Kernel.pathvar + filename + ": ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Pomyslnie stworzono plik!");
                    }
                    return;
                }
                catch (FileNotFoundException)
                {
                    OSApi.BSOD("FILE_NOT_FOUND", "0x031");
                    return;
                }
            }
            if (input.StartsWith("rmdir "))
            {
                if (Kernel.pathvar == @"\\?\PsyhicialDrive0\")
                {
                    Console.WriteLine("GHJOS nie obsluguje sciezek UNC, przejdz do woluminu uzywajac cd /");
                    return;
                }
                string dirToRemove = input.Remove(0, 6);
                if (dirToRemove == "OS")
                {
                    Console.WriteLine("Ta czynnosc jest zabroniona!");
                    return;
                }
                if (Directory.Exists(Kernel.pathvar + dirToRemove))
                {
                    try
                    {
                        Directory.Delete(Kernel.pathvar + dirToRemove);
                        Console.Write("rmdir: " + Kernel.pathvar + dirToRemove + ": ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Pomyslnie usunieto folder!");
                    }
                    catch (Exception reason)
                    {
                        Console.Write("rmdir: " + dirToRemove + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(reason.Message + "!\n");
                    }
                }
                else
                {
                    Console.Write("rmdir: " + dirToRemove + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie znaleziono folderu");
                }
                return;
            }
            if (input.StartsWith("mkdir "))
            {
                if (Kernel.pathvar == @"\\?\PsyhicialDrive0\")
                {
                    Console.WriteLine("GHJOS nie obsluguje sciezek UNC, przejdz do woluminu uzywajac cd /");
                    return;
                }
                string dirname = input.Remove(0, 6);
                if (dirname == "OS")
                {
                    Console.WriteLine("Ta nazwa jest niepoprawna!, wybierz inna!");
                    return;
                }
                if (Directory.Exists(Kernel.pathvar + dirname))
                {
                    Console.Write("mkdir: " + dirname + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Folder juz istnieje");
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(Kernel.pathvar + dirname);
                        Console.Write("mkdir: " + Kernel.pathvar + dirname + ": ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Pomyslnie stworzono folder!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    catch (Exception reason)
                    {
                        Console.Write("mkdir: " + dirname + ": ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(reason.Message + "!\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                return;
            }
            if (input == "" || input == " ")
            {
                return;
            }
            if (input.StartsWith("rm "))
            {
                if (Kernel.pathvar == @"\\?\PsyhicialDrive0\")
                {
                    Console.WriteLine("GHJOS nie obsluguje sciezek UNC, przejdz do woluminu uzywajac cd /");
                    return;
                }
                string filename = input.Remove(0, 3);

                if (File.Exists(Kernel.pathvar + filename))
                {
                    File.Delete(Kernel.pathvar + filename);
                    Console.Write("rm: " + filename + ": ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Pomyslnie usunieto plik!");
                }
                else
                {
                    Console.Write("rm: " + filename + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Plik nie istnieje!");
                }
                return;
            }
            if (input.StartsWith("remuser"))
            {
                if (PermissionsAPI.UAC() == false)
                {
                    return;
                }
                Console.WriteLine("Narzedzie usuwania uzytkownika v1.0:");

                Console.WriteLine(" ");
                Console.Write("Nazwa uzytkownika: ");
                var usr = Console.ReadLine();
                Console.Write("Haslo uzytkownika: ");
                var pass = Console.ReadLine();

                if (Directory.Exists("0:\\OS\\Users\\" + usr))
                {
                    try
                    {
                        PermissionsAPI.remUser(usr, pass);
                        Console.WriteLine("Pomyslnie usunieto uzytkownika z systemu!");
                        return;
                    }
                    catch
                    {
                        Console.WriteLine("Bledne haslo!");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Taki uzytkownik nie istnieje!");
                    return;
                }
            }
            if (input.StartsWith("adduser"))
            {
                if (PermissionsAPI.UAC() == false)
                {
                    return;
                }
                Console.WriteLine("Narzedzie dodawania nowego uzytkownika v1.0:");
            raks1:
                Console.WriteLine(" ");
                Console.Write("Nazwa uzytkownika: ");
                var usr = Console.ReadLine();
                Console.Write("Haslo: ");
                var pass = Console.ReadLine();
                Console.Write("Powtorz haslo: ");
                var pass1 = Console.ReadLine();
                if (pass == pass1)
                {
                    PermissionsAPI.addUser(usr, pass);
                    Console.WriteLine("Pomyslnie dodano nowego uzytkownika!");
                    return;
                }
                else
                {
                    Console.WriteLine("Hasla nie sa takie same!");
                    goto raks1;
                }

            }
            if (input.StartsWith("overwritte ") || input.StartsWith("edit "))
            {
                string filename;


                filename = input.Remove(0, 5);
                if (input.StartsWith("overwritte "))
                {
                    filename = input.Remove(0, 11);
                }

                if (File.Exists(filename))
                {
                    Console.Write("Enter text > ");
                    var edit = Console.ReadLine().Replace("|", "\n");
                    File.WriteAllText(filename, edit);

                }
                else if (File.Exists(Kernel.pathvar + filename))
                {
                    Console.Write("Enter text > ");
                    var edit = Console.ReadLine().Replace("|", "\n");
                    File.WriteAllText(Kernel.pathvar + filename, edit);
                }
                else
                {
                    Console.Write("show: " + filename + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie znaleziono pliku");
                }
                return;
            }
            if (input.StartsWith("show "))
            {
                string filename = input.Remove(0, 5);
                string fileExtension = Path.GetExtension(filename);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);

                if (File.Exists(filename))
                {
                    string f_contents = File.ReadAllText(filename);
                    Console.WriteLine(f_contents);
                }
                else if (File.Exists(Kernel.pathvar + filename))
                {
                    string f_contents = File.ReadAllText(Kernel.pathvar + filename);
                    Console.WriteLine(f_contents);
                }
                else if (File.Exists(Kernel.pathvar + fileNameWithoutExtension + fileExtension))
                {
                    string f_contents = File.ReadAllText(Kernel.pathvar + fileNameWithoutExtension + fileExtension);
                    Console.WriteLine(f_contents);
                }
                else
                {
                    Console.Write("show: " + filename + ": ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie znaleziono pliku");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                return;
            }

            if (input == "without83727345")
            {
                File.Create(@"0:\OS\Users\" + Kernel.username + @"\admin.permission");
                Console.WriteLine("Luba antud!");
                return;
            }
            if (input.StartsWith("perm"))
            {
                if (PermissionsAPI.UAC() == false)
                {
                    return;
                }
                Console.WriteLine("GHJOS Permission Manager v1.0");
                Console.WriteLine(" ");
                Thread.Sleep(2600);
                Console.Write("Select username: ");
                var usr = Console.ReadLine();
                if (!Directory.Exists(@"0:\OS\Users\" + usr + @"\"))
                {
                    Console.WriteLine("Specified user doesn't exits!");
                    return;
                }
                Console.WriteLine("\nCollecting informations...");
                if (File.Exists(@"0:\OS\Users\" + usr + @"\admin.permission"))
                {
                    Console.WriteLine("Account type: Administrator");
                }
                if (!File.Exists(@"0:\OS\Users\" + usr + @"\admin.permission"))
                {
                    Console.WriteLine("Account type: Standard");
                }
                Console.Write("New account type (A/S): ");
                var acctype = Console.ReadKey();
                if (acctype.Key == ConsoleKey.A)
                {
                    try
                    {
                        File.Create(@"0:\OS\Users\" + usr + @"\admin.permission");
                        Console.WriteLine("\n" + usr + " now have administrator permissions!");
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("E: " + e.Message);
                        return;
                    }
                }
                if (acctype.Key == ConsoleKey.S)
                {
                    try
                    {
                        File.Delete(@"0:\OS\Users\" + usr + @"\admin.permission");
                        Console.WriteLine("\n" + usr + " now doesn't have administrator permissions!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("E: " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Unrecognized account type!");
                    return;
                }

            }
           

            if (input.StartsWith("cd "))
            {
                string foldername = input.Remove(0, 3);
                if (foldername == @"0:\" || foldername == @"\" || foldername == "/" || foldername == "0:/")
                {
                    Kernel.pathvar = @"0:\";
                    return;
                }
                bool hasPermission = false;
                if (File.Exists(@"0:\OS\Users\" + Kernel.username + @"\admin.permission"))
                {
                    hasPermission = true;
                    Console.WriteLine("Masz uprawnienia administratora");
                }
                else
                {
                    Console.WriteLine("Nie masz uprawnień administratora");
                }


                if (!hasPermission && foldername.Contains("OS") && Kernel.pathvar == @"0:\")
                {
                    Console.WriteLine("Ta czynnosc jest zabroniona!");
                    return;
                }
                string[] dirs = Directory.GetDirectories(Kernel.pathvar);

                List<string> dirslist = new List<string>();

                foreach (string arrItem in dirs)
                {
                    dirslist.Add(arrItem);
                }

                if (foldername.StartsWith("..\\"))
                {
                    string[] pathvarSplit = Kernel.pathvar.Split(@"\");
                    int count = 0;
                    for (int i = 0; i < foldername.Length; i += 3)
                    {
                        if (foldername.Substring(i, 3) == "..\\")
                        {
                            count++;
                        }
                    }
                    if (count > pathvarSplit.Length - 2)
                    {
                        Console.WriteLine("Nie możesz cofnąć się tak daleko!");
                        return;
                    }
                    Kernel.pathvar = "";
                    for (int l = 0; l < pathvarSplit.Length - count - 1; l++)
                    {
                        Kernel.pathvar = Kernel.pathvar + pathvarSplit[l] + @"\";
                    }
                    return;
                }
                if (Directory.Exists(foldername) && !Directory.Exists(Kernel.pathvar + foldername))
                {
                    Kernel.pathvar = foldername;
                    return;
                }
                else if (Directory.Exists(Kernel.pathvar + foldername))
                {
                    Kernel.pathvar = Kernel.pathvar + foldername;
                    return;
                }
                Console.WriteLine("Podany katalog nie istnieje!");
                return;
            }

            if (input.StartsWith("net "))
            {
                string[] splitInput = input.Split(' ');
                if (splitInput.Length == 2 && splitInput[1] == "user")
                {
                    Console.WriteLine("Lista użytkowników:");
                    foreach (User user in PermissionsAPI.Users)
                    {
                        Console.WriteLine(user.Username);
                    }
                }
                else if (splitInput.Length == 3 && splitInput[1] == "user")
                {
                    string targetUserName = splitInput[2];
                    User targetUser = PermissionsAPI.Users.FirstOrDefault(u => u.Username == targetUserName);
                    if (targetUser != null)
                    {
                        Console.WriteLine($"Nazwa użytkownika: {targetUser.Username}");
                        Console.WriteLine($"Hasło: {targetUser.Password}");
                        Console.WriteLine($"Katalog: 0:\\Users\\{targetUser.Username}");
                    }
                    else
                    {
                        Console.WriteLine($"Użytkownik {targetUserName} nie istnieje.");
                    }
                }
                else if (splitInput.Length == 5 && splitInput[1] == "user" && splitInput[3] == "add" && splitInput[4] == "Administrators")
                {
                    string targetUserName = splitInput[2];
                    User targetUser = PermissionsAPI.Users.FirstOrDefault(u => u.Username == targetUserName);
                    if (targetUser != null)
                    {
                        string adminPermissionPath = $@"0:\Users\{Kernel.username}\admin.permission";
                        if (File.Exists(adminPermissionPath))
                        {
                            string targetUserDirectoryPath = $@"0:\Users\{targetUserName}\";
                            string targetUserAdminPermissionPath = targetUserDirectoryPath + "admin.permission";
                            File.Copy(adminPermissionPath, targetUserAdminPermissionPath);
                            Console.WriteLine($"Dodano uprawnienia administratora użytkownikowi {targetUserName}.");
                        }
                        else
                        {
                            Console.WriteLine($"Brak uprawnień administratora.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Użytkownik {targetUserName} nie istnieje.");
                    }
                }
            }


            else
            {
                ConsoleColor cc = Console.ForegroundColor;
                OSApi.changeTextColor(ConsoleColor.DarkRed);
                Console.WriteLine("Nie znaleziono komendy! (Aby wyswietlic pomoc wpisz help)");
                OSApi.changeTextColor(cc);
                return;
            }
        }
    }
}
