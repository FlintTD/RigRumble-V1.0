using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigRumble
{
    class Game
    {
        public const string versionString = "Rig Rumble Version 1.0";

        static void Main(string[] args)
        {
            // Set up game environment
            DirectoryInfo d = new DirectoryInfo(@"\Saves");
            GameInstance game = new GameInstance();
            List<String> command;
            Boolean exit = false;
            String helpText = "File '0_helpText' is missing!";
            try
            {
                helpText = System.IO.File.ReadAllText(@"Text\0_helpText.txt");
            }
            catch
            {
                Console.WriteLine("File '0_helpText' is missing!");
            }

            // Write the opening to the player
            Console.WriteLine("Welcome to " + versionString + "!");
            Console.WriteLine("Input 'help' at any time to see a list of commands.");

            // Main loop
            while (!exit)
            {
                Console.WriteLine("  ------");
                // Parse user input
                command = parseUserInput();
                switch (command[0])
                {
                    case "delete":
                        Console.WriteLine("TODO: Delete old save.");
                        break;

                    case "exit":
                        exit = true;
                        break;

                    case "help":
                        Console.WriteLine(helpText);
                        break;

                    case "load":
                        if (command.Count > 1)
                        {
                            if (Load("Saves\\" + command[1] + ".txt", game))
                            {
                                Console.WriteLine(command[1] + " loaded successfully!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine(command[1] + " failed to load!");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Which saved game would you like to load?");
                            //Getting Text files
                            FileInfo[] Saves = d.GetFiles("*.txt");
                            foreach (FileInfo file in Saves)
                            {
                                Console.WriteLine(file.Name);
                            }
                            Load("Saves\\" + parseUserInput()[0] + ".txt", game);
                        }
                        break;

                    case "new":
                        Boolean repeat = true;
                        while (repeat)
                        {
                            Console.WriteLine("What would you like your new game to be called?");
                            Console.WriteLine("Remember that a save name cannot contain spaces:");
                            List<String> i = parseUserInput();
                            if (i[0] == "back") { }
                            else if (i[0] == "quit")
                            {
                                exit = true;
                                repeat = false;
                            }
                            else
                            {
                                Console.WriteLine("Are you sure?");
                                List<String> j = parseUserInput();
                                if (j[0] == "y" || j[0] == "Y" || j[0] == "yes" || j[0] == "Yes")
                                {
                                    game = new GameInstance(i[0]);
                                    repeat = false;
                                }
                            }
                        }
                        break;

                    case "quit":
                        exit = true;
                        break;

                    case "save":
                        if (command.Count >= 3 & command[1] == "as")
                        {
                            Console.WriteLine("Would you like to save your game as: " + command[2]);
                            List<String> i = parseUserInput();
                            if (i[0] == "y" || i[0] == "Y" || i[0] == "yes" || i[0] == "Yes")
                            {
                                Save(game, command[2]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Would you like to save your game: " + game.getGameName());
                            List<String> i = parseUserInput();
                            if (i[0] == "y" || i[0] == "Y" || i[0] == "yes" || i[0] == "Yes")
                            {
                                Save(game);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command " + command);
                        break;
                }
            }
        }

        public static List<String> parseUserInput()
        {
            List<String> ret = new List<String> { };
            String buffer = Console.ReadLine();
            while (buffer.Contains(" "))
            {
                var temp = buffer.Split(' ');
                ret.Add(temp[0]);
                buffer = temp[1];
            }
            ret.Add(buffer);
            return ret;
        }

        // Reads a save game file in, produces a GameInstance object
        private static Boolean Load(string file, GameInstance game)
        {
            try
            {
                string[] tempGameInfo = System.IO.File.ReadAllLines(@file);
                try
                {
                    game = new GameInstance(tempGameInfo[0]);
                    return true;
                }
                catch
                {
                    Console.WriteLine(tempGameInfo[0] + " failed to be read!");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("That save file cannot be found.");
                return false;
            }
            
        }

        private static Boolean Save(GameInstance game)
        {
            String name = game.getGameName();
            try
            {
                string[] lines = { name,
                                "Second line",
                                "Third line" };
                System.IO.File.WriteAllLines(@"Saves\\" + name + ".txt", lines);
                return true;
            }
            catch
            {
                Console.WriteLine(name + " failed to be saved!");
                return false;
            }
        }

        private static Boolean Save(GameInstance game, String gameName)
        {
            try
            {
                string[] lines = { "gameName",
                                "Second line",
                                "Third line" };
                System.IO.File.WriteAllLines(@"Saves\\" + gameName + ".txt", lines);
                return true;
            }
            catch
            {
                Console.WriteLine(game.getGameName() + " failed to be saved as " + gameName + "!");
                return false;
            }
        }
    }

    public class GameInstance
    {
        private string saveName;

        public string getGameName()
        {
            return saveName;
        }

        public void setGameName(string name)
        {
            name = saveName;
        }

        public GameInstance() {}

        public GameInstance(string name)
        {
            saveName = name;
        }
    }
}
