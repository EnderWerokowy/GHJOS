using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace workplease
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Permissions { get; set; }
        public string DirectoryPath { get; set; }

        public User(string username, string password, string directoryPath)
        {
            Username = username;
            Password = password;
            Permissions = new List<string>();
            DirectoryPath = directoryPath;
        }

        public void AddUser(User user)
        {
            PermissionsAPI.Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            PermissionsAPI.Users.Remove(user);
        }

        public void AddToGroup(Group group)
        {
            group.Users.Add(this);
        }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public List<User> Users { get; set; }
        public List<string> Permissions { get; set; }

        public Group(string groupName)
        {
            GroupName = groupName;
            Users = new List<User>();
            Permissions = new List<string>();
        }

        public void AddGroup(Group group)
        {
            PermissionsAPI.Groups.Add(group);
        }

        public void RemoveGroup(Group group)
        {
            PermissionsAPI.Groups.Remove(group);
        }
        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
    }


    internal class PermissionsAPI
    {

        public static List<User> Users = new List<User>();
        public static List<Group> Groups = new List<Group>();
        public static void LoadUsers()
        {
            string path = @"0:\OS\users.txt";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] data = line.Split(';');
                    string username = data[0];
                    string password = data[1];
                    string directoryPath = data[2];

                    User user = new User(username, password, directoryPath);
                    Users.Add(user);
                }
            }
        }

        public static void SaveUsers()
        {
            string path = @"0:\OS\users.txt";
            StringBuilder sb = new StringBuilder();
            foreach (User user in Users)
            {
                sb.AppendLine($"{user.Username};{user.Password};{user.DirectoryPath}");
            }
            File.WriteAllText(path, sb.ToString());
        }

        public static void addUser(string user, string pass, bool isAdmin = false)
        {
            Directory.CreateDirectory(@"0:\OS\Users\" + user + @"\");
            Directory.CreateDirectory(@"0:\OS\Users\" + user + @"\pass" + pass);

            var newUser = new User(user, pass, @"0:\OS\Users\" + user + @"\");
            Users.Add(newUser);

            var group = Groups.FirstOrDefault(g => g.GroupName == "Users");
            if (group == null)
            {
                group = new Group("Users");
                Groups.Add(group);
            }

            newUser.AddToGroup(group);

            if (isAdmin)
            {
                var adminGroup = Groups.FirstOrDefault(g => g.GroupName == "Administrators");
                if (adminGroup == null)
                {
                    adminGroup = new Group("Administrators");
                    Groups.Add(adminGroup);
                }

                adminGroup.AddUser(newUser);
                File.Create(@"0:\OS\Users\" + user + @"\admin.permission");
            }

            SaveUsers();
        }



        public static void remUser(string user, string pass)
        {
            Directory.Delete(@"0:\OS\Users\" + user + @"\pass" + pass);
            Directory.Delete(@"0:\OS\Users\" + user + @"\");

            var userToRemove = Users.FirstOrDefault(u => u.Username == user);
            if (userToRemove != null)
            {
                foreach (var group in Groups)
                {
                    group.RemoveUser(userToRemove);
                }
                Users.Remove(userToRemove);
            }
            SaveUsers();
        }

        public static bool UAC()
        {
            var username = Kernel.username;
            Console.WriteLine("GHJOS User Account Control v1.0");
            Console.WriteLine(" ");

            if (!File.Exists(@"0:\OS\Users\" + username + @"\admin.permission"))
            {
                Console.WriteLine("Konto na ktorym jestes zalogowany nie posiada uprawnien do tej komendy");

                while (true)
                {
                    Console.WriteLine("Podaj konto administratora:");
                    Console.Write("Username: ");
                    string tempusername = Console.ReadLine();
                    Console.WriteLine("");
                    Console.Write("Pass: ");
                    string temppass = Console.ReadLine();

                    if (Directory.Exists(@"0:\OS\Users\" + tempusername + @"\") && Directory.Exists(@"0:\OS\Users\" + tempusername + @"\pass" + temppass))
                    {
                        return true;
                    }

                    Console.WriteLine("Dane niepoprawne, czy chcesz powtórzyć próbę?");
                    Console.Write("Y/N: ");
                    string yn = Console.ReadLine();

                    if (yn == "N")
                    {
                        return false;
                    }
                }
            }

            Console.Write("Do you want to allow this program? (Y/N): ");
            var input = Console.ReadKey();
            Console.WriteLine();

            if (input.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Please enter your password to verify.");
                Console.Write("Password: ");
                var pass = Kernel.GetHiddenConsoleInput();

                if (Directory.Exists(@"0:\OS\Users\" + username + @"\pass" + pass))
                {
                    Console.WriteLine("Access granted!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong password!");
                    return false;
                }
            }
            else if (input.Key == ConsoleKey.N)
            {
                Console.WriteLine("Access prohibited!");
                return false;
            }
            else
            {
                Console.WriteLine("Unrecognized option!");
                return false;
            }
        }

    }
}
