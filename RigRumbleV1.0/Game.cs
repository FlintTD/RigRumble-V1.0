using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigRumble
{
    public class GameInstance
    {
        private string saveName;
        private Rig playerRig;
        private List<int> inGameTime = new List<int>() { 0000, 1, 1, 3000 };



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

        // -------- -------- -------- --------

        public void Play()
        {
            List<String> command;
            Boolean exit = false;
            
            while (!exit)
            {
                Console.WriteLine("  ------");
                // Parse user input
                command = parseUserInput();
                switch (command[0])
                {
                    case "back":
                        Console.WriteLine("Type 'menu' to return to the main menu.");
                        break;

                    case "exit":
                        Console.WriteLine("Type 'menu' to return to the main menu.");
                        break;

                    case "guide":
                        Boolean closeGuide = false;
                        Console.WriteLine(readInText("1_guideText"));
                        Console.WriteLine(readInText("1_guideChapters"));

                        while (closeGuide == false)
                        {
                            List<String> guideString = parseUserInput();

                            if (guideString[0] == "back" || guideString[0] == "exit")
                            {
                                closeGuide = true;
                            }
                            else if (guideString[0] == "help")
                            {
                                Console.WriteLine("To close the guide, type 'back' or 'exit'.  To go to a chapter, type that chapter's number.  To access game help, close the guide and type 'help' again.");
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
                        Console.WriteLine(readInText("0_gameHelpText"));
                        break;

                    case "menu":
                        Console.WriteLine("Are you sure you want to go back to the main menu?");
                        List<String> quitString = parseUserInput();
                        if (quitString[0] == "y" || quitString[0] == "Y" || quitString[0] == "yes" || quitString[0] == "Yes")
                        {
                            exit = true;
                        }
                        break;

                    case "resources":
                        Console.WriteLine("In your cabin, you check the resources monitor.  It reads:");
                        renderResourceUI(playerRig);
                        break;

                    default:
                        Console.WriteLine("Unknown command " + command);
                        break;
                }
            }
        }

        // -------- -------- -------- --------

        private void renderResourceUI(Rig rig)
        {
            int width = 60;
            int height = 20;
            int wRemaining;
            for (int i = 0; i > height; i++)
            {
                // Top and bottom of the render
                if (i == 0 || i == height - 1)
                {
                    for (int j = 0; j > width-1; j++)
                    {
                        Console.Write("/");
                    }
                    Console.WriteLine("/");
                }
                // 2nd line of the render
                else if (i == 1)
                {
                    wRemaining = width - playerRig.rigName.Length - 2;
                    int hour = inGameTime[0] / 100;
                    String t = hour.ToString() + ":00  ";
                    String d = inGameTime[1].ToString();
                    String gap = " ";
                    if (inGameTime[1] == 1)
                    {
                        d += "st";
                    }
                    else if (inGameTime[1] == 2)
                    {
                        d += "nd";
                    }
                    else if (inGameTime[1] == 3)
                    {
                        d += "rd";
                    }
                    else
                    {
                        d += "th";
                    }
                    d += " of " + inGameTime[2].ToString() + ", " + inGameTime[3].ToString() + " ";
                    t += d;
                    wRemaining -= t.Length + 1;
                    while (wRemaining > 1)
                    {
                        gap += " ";
                    }

                    Console.Write("/");
                    Console.Write(playerRig.rigName);
                    Console.Write(gap);
                    Console.Write(t);
                    Console.WriteLine("/");
                }
                // Other lines of the render
            }
        }

        public string getGameName()
        {
            return saveName;
        }

        public void setGameName(string name)
        {
            saveName = name;
        }

        public List<int> getGameDate()
        {
            return inGameTime;
        }

        public void setGameDate(List<int> date)
        {
            inGameTime = date;
        }

        public GameInstance()
        {
            saveName = "";
            playerRig = new Rig();
        }

        public GameInstance(String name)
        {
            saveName = name;
            playerRig = new Rig();
        }

        public GameInstance(List<string> loadList)
        {
            saveName = loadList[0];
            playerRig = new Rig();
        }
    }
}
