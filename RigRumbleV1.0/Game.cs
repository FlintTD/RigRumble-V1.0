using System;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace RigRumble
{
    public class GameInstance
    {
        // Data variables
            // All data variables below are public due to System.Xml.Serialization.XmlSerializer only reading public varaibles
            // Please treat all variables as private; i.e. make get and set methods for sanity
        public string saveName;
        public Rig playerRig;
        public List<int> inGameTime = new List<int>() { 0000, 1, 1, 3000 };
        WindowsMediaPlayer jukebox = new WindowsMediaPlayer();

        // User Input Functions
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

        // Wrapper for reading music from the 'BGM' folder
        private static Boolean readInMusic(WindowsMediaPlayer p, string gameBGMFileName)
        {
            Boolean ret;
            try
            {
                p.URL = AppDomain.CurrentDomain.BaseDirectory + "BGM\\" + gameBGMFileName + ".wav";
                ret = true;
            }
            catch (Exception m)
            {
                ret = false;
                Console.WriteLine("File " + gameBGMFileName + " is missing!");
                Console.Write(m);
                Console.WriteLine("");
            }
            return ret;
        }


        // -------- -------- -------- --------

        public void Play()
        {
            // Game initialization header
            List<String> command;
            Boolean exit = false;
            if (readInMusic(jukebox, "0_main"))
            {
                jukebox.controls.play();
            }
            // ---- ^^^^ ----

            // Core loop
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
                        //renderResourceUI(playerRig);
                        ManifestCmdWindow rigManifest = new ManifestCmdWindow(
                                        2, 2, 40, "Manifest", playerRig.getManifestValues(), playerRig.getManifestLabels()
                                        );
                        renderWindows(new List<CmdWindow> { rigManifest });
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
        private void renderWindows(List<CmdWindow> windows)
        {
            int terminalWidth = 120;
            int terminalHeight = 50;
            int windowCount = windows.Count;
            List<string> masterRaster = new List<string>();

            // loop over all rows
            for (int i = 0; i < terminalHeight; i++)
            {
                masterRaster.Add("");
                // loop over all columns
                for (int j = 0; j < terminalWidth; j++)
                {
                    string rasterBuffer = " ";
                    // loop over all windows
                    for (int w = 0; w < windowCount; w++)
                    {
                        // inverted positional variables to print row by row, instead of column by column
                        if (windows[w].hasAdjustedValue(j, i))
                        {
                            // write value to master array
                            rasterBuffer = windows[w].getAdjustedValue(j, i);
                        }
                    }
                    // read into the master raster to be printed
                    masterRaster[i] = masterRaster[i] + rasterBuffer;
                }
                Console.WriteLine(masterRaster[i]);
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
            this.saveName = "Default";
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
        protected string[][] mapping;
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

        public string getAdjustedValue(int xVal, int yVal)
        {
            if ((xVal - this.x >= 0 && yVal - this.y >= 0) && (xVal < this.x + width && yVal < this.y + height))
            {
                return mapping[xVal - this.x][yVal - this.y];
            }
            else
            {
                return "";
            }
        }

        public CmdWindow()
        {

        }

        public CmdWindow(int xCoord, int yCoord, int w, int h, string t)
        {
            this.mapping = new string[w][];
            for (int a = 0; a < h; a++)
            {
                mapping[a] = new string[h];
            }
            this.x = xCoord;
            this.y = yCoord;
            this.width = w;
            this.height = h;
            this.title = t;

            int titleEdgeDistance = ((this.width + 1) / 2) - ((this.title.Length + 1) / 2);
            for (int y = 0; y < this.width; y++)
            {
                for (int x = 0; x < this.height; x++)
                {
                    // Exterior
                    if (x == 0 || y == 0 || x == this.width - 1 || y == this.height - 1)
                    {
                        // Title
                        if (y == 0 && x > titleEdgeDistance && x < this.width - titleEdgeDistance)
                        {
                            mapping[x][y] = title[x - titleEdgeDistance].ToString();
                        }
                        // Boarder
                        else
                        {
                            mapping[x][y] = "/";
                        }
                    }
                    //Interior
                    else
                    {
                        mapping[x][y] = " ";
                    }
                }
            }
        }
    }

    // A list of quantized objects
    public class ManifestCmdWindow : CmdWindow
    {
        protected List<int> values;
        protected int rows;
        protected List<string> labels;

        new public string getAdjustedValue(int x, int y)
        {
            if (x >= this.x && y >= this.y && x <= this.x + width && y <= this.y + height)
            {
                return mapping[x - this.x][y - this.y];
            }
            else
            {
                return "";
            }
        }

        public ManifestCmdWindow()
        {

        }

        // positional x, positional y, width, window title, values, labels
        public ManifestCmdWindow(int xCoord, int yCoord, int w, string t, List<int> v, List<string> l)
        {
            this.height = 3 + (2 * labels.Count);
            this.mapping = new string[w][];
            for (int a = 0; a < height; a++)
            {
                mapping[a] = new string[height];
            }
            this.x = xCoord;
            this.y = yCoord;
            this.width = w;
            this.height = 3 + (2 * labels.Count);
            this.title = t;
            this.values = v;
            this.labels = l;
            rows = labels.Count;

            int titleEdgeDistance = ((this.width + 1) / 2) - ((this.title.Length + 1) / 2);
            int currentLine = 0;

            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    currentLine = y / 2;
                    // Exterior
                    if (x == 0 || y == 0 || x == this.width - 1 || y == this.height - 1)
                    {
                        // Title
                        if (y == 0 && x > titleEdgeDistance && x < this.width - titleEdgeDistance)
                        {
                            mapping[x][y] = title[x - titleEdgeDistance].ToString();
                        }
                        // Boarder
                        else
                        {
                            mapping[x][y] = "/";
                        }
                    }
                    //Interior
                    else
                    {
                        // Written line
                        if (y % 2 == 0)
                        {
                            // Spacing for Visibility
                            if (x == 1 || x == this.width - 2)
                            {
                                mapping[x][y] = " ";
                            }
                            // labels and values
                            else if (currentLine < labels.Count)
                            {
                                //labels
                                if (x >= 2 && x - 2 < labels[currentLine].Length)
                                {
                                    mapping[x][y] = labels[currentLine][x - 2].ToString();
                                }
                                // values
                                else if (x < width - 2 && x > width - 2 - values[currentLine].ToString().Count())
                                {
                                    string vString = values[currentLine].ToString();
                                    mapping[x][y] = vString[vString.Count() - (width - x)].ToString();
                                }
                            }
                            // all other space
                            else
                            {
                                mapping[x][y] = " ";
                            }
                        }
                        // Blank line
                        else
                        {
                            mapping[x][y] = " ";
                        }
                    }
                }
            }
        }
    }

    // A list of objects with multiple values
    public class TableCmdWindow : CmdWindow
    {
        protected List<List<int>> table;
        protected List<string> labels;
        protected int columns;
        protected int rows;

        new public string getAdjustedValue(int x, int y)
        {
            //TODO
            return "";
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
