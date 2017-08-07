using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigRumble
{
    class MainMenu
    {
        public const string versionString = "Rig Rumble Version 1.0";
        public const short hoursToIncrement = 1;

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
                catch (Exception e)
                {
                    Console.Write(e);
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
            List<int> date = game.getGameDate();
            try
            {
                string[] lines = { name,
                                Convert.ToString(date[0]),
                                Convert.ToString(date[1]),
                                Convert.ToString(date[2]),
                                Convert.ToString(date[3])};
                System.IO.File.WriteAllLines(@"Saves\\" + name + ".txt", lines);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e);
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
            catch (Exception e)
            {
                Console.Write(e);
                Console.WriteLine(game.getGameName() + " failed to be saved as " + gameName + "!");
                return false;
            }
        }

        private static String readInText(string gameTextFileName)
        {
            String ret;
            try
            {
                ret = System.IO.File.ReadAllText(@"Text\\" + gameTextFileName + ".txt");
            }
            catch (Exception e)
            {
                Console.Write(e);
                ret = "File " + gameTextFileName + " is missing!";
            }
            return ret;
        }


        // -------- -------- -------- -------- -------- -------- --------

        
        static void Main(string[] args)
        {
            // Set up game environment
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DirectoryInfo d = new DirectoryInfo(@"\Saves");
            GameInstance game = new GameInstance ();
            Boolean gameIsLoaded = false;
            List<String> command;
            Boolean exit = false;

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
                        Console.WriteLine("Type 'quit' to quit the game.");
                        break;

                    case "guide":
                        Boolean closeGuide = false;
                        Console.WriteLine(readInText("1_guideText"));
                        Console.WriteLine(readInText("1_guideChapters"));
                        
                        while (closeGuide == false)
                        {
                            List<String> guideString = parseUserInput();

                            if (guideString[0] == "back")
                            {
                                closeGuide = true;
                            }
                            else if (guideString[0] == "help")
                            {
                                Console.WriteLine("To close the guide, type 'back'.  To go to a chapter, type that chapter's number.  To access game help, close the guide and type 'help' again.");
                            }
                            else if (guideString[0] == "1")
                            {
                                Console.WriteLine(readInText("1_guideTextD2D"));
                                Console.WriteLine(readInText("1_guideChapters"));
                            }
                            else if (guideString[0] == "2")
                            {

                            }
                            else if (guideString[0] == "3")
                            {

                            }
                            else
                            {
                                Console.WriteLine("Unknown command " + guideString);
                            }
                        }
                        break;

                    case "help":
                        Console.WriteLine(readInText("0_menuHelpText"));
                        break;

                    case "load":
                        if (command.Count > 1)
                        {
                            if (Load("Saves\\" + command[1] + ".txt", game))
                            {
                                Console.WriteLine(command[1] + " loaded successfully!");
                                gameIsLoaded = true;
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
                            gameIsLoaded = true;
                        }
                        break;

                    case "new":
                        Boolean repeat = true;
                        while (repeat)
                        {
                            Console.WriteLine("  ------");
                            Console.WriteLine("What would you like your new game to be called?");
                            Console.WriteLine("Remember that a save name cannot contain spaces:");
                            List<String> newString = parseUserInput();
                            if (newString[0] == "back") {
                                repeat = false;
                            }
                            else if (newString[0] == "quit")
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
                                    game = new GameInstance(newString[0]);
                                    repeat = false;
                                    gameIsLoaded = true;
                                }
                            }
                        }
                        break;

                    case "play":
                        if (gameIsLoaded)
                        {
                            game.Play();
                        }
                        break;

                    case "quit":
                        Console.WriteLine("Are you sure you want to quit?");
                        List<String> quitString = parseUserInput();
                        if (quitString[0] == "y" || quitString[0] == "Y" || quitString[0] == "yes" || quitString[0] == "Yes")
                        {
                            exit = true;
                        }
                        break;

                    case "save":
                        if (command.Count >= 3)
                        {
                            if (command[1] == "as")
                            {
                                Console.WriteLine("Would you like to save your game as: " + command[2] + "?");
                                List<String> saveString = parseUserInput();
                                if (saveString[0] == "y" || saveString[0] == "Y" || saveString[0] == "yes" || saveString[0] == "Yes")
                                {
                                    Save(game, command[2]);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Would you like to save your game: " + game.getGameName() + "?");
                            List<String> saveString = parseUserInput();
                            if (saveString[0] == "y" || saveString[0] == "Y" || saveString[0] == "yes" || saveString[0] == "Yes")
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
    }
}
