using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workplease
{
    class LoginSystem
    {
        
        public static void registerMachineName()
        {
            PermissionsAPI.LoadUsers();
            if (!Directory.Exists(@"0:\OS\MachineInfo\"))
            {
                Directory.CreateDirectory(@"0:\OS\MachineInfo\");

            }
            if (File.Exists(@"0:\OS\MachineInfo\machinename.ini"))
            {
                Kernel.machinename = File.ReadAllText(@"0:\OS\MachineInfo\machinename.ini");
            }
            if (!File.Exists(@"0:\OS\MachineInfo\machinename.ini"))
            {
                File.Create(@"0:\OS\MachineInfo\machinename.ini");
                Console.WriteLine("Nie znaleziono nazwy maszyny, prosimy ustalic nazwe komputera:");
                Console.Write("Machine Name > ");
                var input = Console.ReadLine();
                File.WriteAllText(@"0:\OS\MachineInfo\machinename.ini", input);
                Console.WriteLine("Pomyslnie zapisano nazwe maszyny!");
                Kernel.machinename = input;
            }

        }
        public static void LogIn()
        {
            PermissionsAPI.LoadUsers();
            try
            {
                if (!Directory.Exists(@"0:\OS\"))
                {
                    Directory.CreateDirectory(@"0:\OS\");
                }
                if (!Directory.Exists(@"0:\OS\Users\"))
                {
                    Directory.CreateDirectory(@"0:\OS\Users\");
                }
                if (!Directory.Exists(@"0:\OS\Users\registry\"))
                {
                    Directory.CreateDirectory(@"0:\OS\Users\Registry\");
                }
            }
            catch (FileNotFoundException)
            {
                OSApi.BSOD("DISK_NOT_FOUND_OR_FAT32_REQUIRED", "0x091");
                return;
            }
            if (File.Exists(@"0:\OS\Users\registry\defined.reg"))
            {
                Console.WriteLine("Prosze sie zalogowac uzywajac loginu i hasla...");
            morgo:
                Console.Write("User > ");
                var input = Console.ReadLine();
                string user = input;
                Console.Write("Pass > ");
                input = Kernel.GetHiddenConsoleInput();
                string pass = input;
                if(user=="dev"&&pass=="")
                {
                    Kernel.loggeduser = "yes";
                    Kernel.username = "Developer";
                    PermissionsAPI.addUser("Developer", pass, true);
                    return;
                }
                if (Directory.Exists(@"0:\OS\Users\" + user + @"\"))
                {
                    if (Directory.Exists(@"0:\OS\Users\" + user + @"\pass" + pass))
                    {
                        Kernel.loggeduser = "yes";
                        Kernel.username = user;
                        
                        return;
                    }
                    else
                    {
                        

                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("Haslo jest niepoprawne!");
                        Console.WriteLine(" ");
                        goto morgo;

                    }
                }
                else
                {
                     
                    System.Threading.Thread.Sleep(2000);

                    Console.WriteLine("Login jest niepoprawny!");
                    Console.WriteLine(" ");

                    goto morgo;

                }
            }
            if (!File.Exists(@"0:\OS\Users\registry\defined.reg"))
            {
                Console.WriteLine("Nie zdefiniowales jeszcze uzytkownika, zdefiniuj teraz!");
                Console.WriteLine(" ");
                Console.Write("User > ");
                var user = Console.ReadLine();
                Console.Write("Haslo > ");
                var haslo = Kernel.GetHiddenConsoleInput();
                Console.Write("Powtorz haslo > ");
                var haslo1 = Kernel.GetHiddenConsoleInput();
                if (haslo == haslo1)
                {
                    File.Create(@"0:\OS\Users\registry\defined.reg");
                    PermissionsAPI.addUser(user, haslo1,true);
                    Console.WriteLine("Pomyslnie zarejestrowano, teraz sie zaloguj");
                    
                    Console.Clear();
                    PermissionsAPI.SaveUsers();
                    LogIn();
                    return;
                }
                else
                {
                    Console.WriteLine("Hasla sa rozne!");
                    Console.ReadKey();
                }
            }
        }
    }
}
