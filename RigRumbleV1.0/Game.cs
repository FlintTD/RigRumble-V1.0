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

        //TODO: depricate
        private void renderResourceUI(Rig rig)
        {
            int width = 60;
            int height = 20;
            int wRemaining;
            for (int i = 0; i < height; i++)
            {
                // Top and bottom of the render
                if (i == 0 || i == (height - 1))
                {
                    for (int j = 0; j < (width - 1); j++)
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
                        wRemaining--;
                    }

                    Console.Write("/ ");
                    Console.Write(playerRig.rigName);
                    Console.Write(gap);
                    Console.Write(t);
                    Console.WriteLine("/");
                }
                // Other lines of the render
                else
                {
                    for (int j = 0; j < (width - 1); j++)
                    {
                        if (j == 0 || j == (width - 1))
                        {
                            Console.Write("/");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine("/");
                }
            }
        }

        // Renders a cluster of zero or more windows in a blank terminal space
        private void renderWindows(List<CmdWindow> windows, Rig rig)
        {
            int terminalWidth = 120;
            int terminalHeight = 30;
            int windowCount = windows.Count;
            List<string> masterRaster = new List<string>();
            
            // loop over all rows
            for (int i = 0; i < terminalHeight; i++)
            {
                // loop over all columbs
                for (int j = 0; j < terminalWidth; j++)
                {
                    string rasterBuffer = " ";
                    // loop over all windows
                    for (int w = 0; w < windowCount; w++)
                    {
                        if (windows[w].hasAdjustedValue(i, j))
                        {
                            // write value to master array
                            rasterBuffer = windows[w].getAdjustedValue(i, j);
                        }
                    }
                    masterRaster[i] = masterRaster[i] + rasterBuffer;
                }
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

        // VVVV - Constructors - VVVV --------

        public GameInstance()
        {
            this.saveName = "";
            this.playerRig = new Rig();
        }

        public GameInstance(String name)
        {
            this.saveName = name;
            this.playerRig = new Rig();
        }

        public GameInstance(List<string> loadList)
        {
            this.saveName = loadList[0];
            this.playerRig = new Rig();
        }
    }

    // Windows are drawn starting at the top-left corner,
    // a more positive coordinate is always closer to the bottom-right corner
    public class CmdWindow
    {
        protected int width;
        protected int height;
        protected int x;
        protected int y;
        protected string title;

        public Boolean hasAdjustedValue(int x, int y)
        {
            if ((x - this.x >= 0 && y - this.y >= 0) && (x <= this.x + width && y <= this.y + height))
            {
                return true;
            }
            else return false;
        }

        public string getAdjustedValue(int x, int y)
        {
            if ((x - this.x >= 0 && y - this.y >= 0) && (x <= this.x + width && y <= this.y + height))
            {
                // Boarder
                if (x == this.x || x == this.x + width || y == this.y || y == this.y + height)
                {
                    int halfTitle = (title.Length + 1) / 2;
                    int halfWidth = (width + 1) / 2;
                    int titleEdgeDistance = halfWidth - halfTitle;
                    if (y == this.y && x - this.x > titleEdgeDistance && x - this.x - width < titleEdgeDistance)
                    {
                        return title[x - this.x - titleEdgeDistance].ToString();
                    }
                    else
                    {
                        return "/";
                    }
                }
                // Interior
                else return "/";
            }
            else return "";
        }

        public CmdWindow()
        {

        }

        public CmdWindow(int w, int h, int x, int y, int l, string t)
        {
            this.width = w;
            this.height = h;
            this.x = x;
            this.y = y;
            this.title = t;
        }
    }

    // A list of quantized objects
    public class ManifestCmdWindow : CmdWindow
    {
        protected List<int> values;
        protected int rows;
        protected List<string> labels;

        public string getAdjustedValue(int x, int y)
        {
            if ((x - this.x >= 0 && y - this.y >= 0) && (x <= this.x + width && y <= this.y + height))
            {
                int halfWidth = (width + 1) / 2;
                // Boarder
                if (x == this.x || x == this.x + width || y == this.y || y == this.y + height)
                {
                    int halfTitle = (title.Length + 1) / 2;
                    int titleEdgeDistance = halfWidth - halfTitle;
                    if (y == this.y && x - this.x > titleEdgeDistance && x - this.x - width < titleEdgeDistance)
                    {
                        return title[x - this.x - titleEdgeDistance].ToString();
                    }
                    else
                    {
                        return "/";
                    }
                }
                // Interior
                else
                {
                    // Written line
                    if (y % 2 == 0)
                    {
                        //TODO
                        return " ";
                        //TODO
                    }
                    // Blank line
                    else
                    {
                        return " ";
                    }
                }
            }
            else return "";
        }

        public ManifestCmdWindow()
        {

        }

        public ManifestCmdWindow(int w, int h, string t, List<int> v, List<string> l)
        {
            this.width = w;
            this.height = h;
            this.title = t;
            this.values = v;
            this.labels = l;
            rows = labels.Count;
        }
    }

    // A list of objects with multiple values
    public class TableCmdWindow : CmdWindow
    {
        protected List<List<int>> table;
        protected List<string> labels;
        protected int columns;
        protected int rows;

        public string getAdjustedValue(int x, int y)
        {
            //TODO
        }

        public TableCmdWindow()
        {

        }

        public TableCmdWindow(int w, int h, string t, List<List<int>> b, List<string> l)
        {
            this.width = w;
            this.height = h;
            this.title = t;
            this.table = b;
            this.labels = l;
            columns = b.Count;
            rows = labels.Count;
        }
    }
}
