using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Exp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int xlen = 1400;
        static int ylen = 900;
        int j = 1;
        string era = "Prehistoric";
        string AD = "BH";
        string age = "Age of Formation";
        int year = 0;
        int month = 1;
        int day = 1;
        int randxb = 0;
        int randyb = 0;
        int mouseposX;
        int mouseposY;

        int tempgreencount = 0;
        Point media = new Point(0, 0);

        int[,,] Mapnum = new int[xlen,ylen, 3]; //arraysnum (tileval),Dredline (red),islandnum
        int[, ,] Tempcolor = new int[xlen, ylen, 3]; //r //g //b
        int[,] Newcolor = new int[xlen * ylen, 3];
        Color[] colours = new Color[99999];
        Bitmap Map = new Bitmap(Exp2.Properties.Resources.testimage,xlen,ylen);

        string pathing;
        string[] ProvList = new string[10001];
        string[] FaithList = new string[101];
        string[] NamesList = new string[501];
        string[] SurnameList = new string[501];
        //int[,] landgen = new int[xlen,ylen];

        Random Rander = new Random();


        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(800, 800);
            this.Size = new System.Drawing.Size(xlen, ylen);
            //this.Height = ylen;
            //this.Width = xlen;

            pictureBox1.ClientSize = new Size(this.Width, this.Height);
            pictureBox1.Image = Map;
            drawfile();
            circles_are_dumb();

            refreshmap();
            timer1.Start();
        }
        int countp = 0;

        public void drawfile()
        {
            pathing = this.Text;
            this.Text = "Generating Map...";
            this.Icon = Exp2.Properties.Resources.WorldGen;

            System.IO.StreamReader Read = new System.IO.StreamReader(pathing + "//Info//ProvinceNames.dat");
            for(int i = 0; i <= 10000;i++)
            {
                ProvList[i] = Read.ReadLine();
            }
            Read.Close();

            System.IO.StreamReader Reada = new System.IO.StreamReader(pathing + "//Info//ReligionNames.dat");
            for (int i = 0; i <= 100; i++)
            {
                FaithList[i] = Reada.ReadLine();
            }
            Reada.Close();

            System.IO.StreamReader Readb = new System.IO.StreamReader(pathing + "//Info//Names.dat");
            for (int i = 0; i <= 500; i++)
            {
                NamesList[i] = Readb.ReadLine();
            }
            Readb.Close();

            System.IO.StreamReader Readc = new System.IO.StreamReader(pathing + "//Info//Dynasty.dat");
            for (int i = 0; i <= 500; i++)
            {
                SurnameList[i] = Readc.ReadLine();
            }
            Readc.Close();

        }

        private void circles_are_dumb()
        {
            int ff = 0;
            for (int j = 0; j <= 716; j++)
            {
                for (double i = 0.0; i < 360.0; i += 0.1)
                {
                    int radius = (716 - j) / 2;
                    double angle = i * System.Math.PI / 180;
                    int x = xlen / 2 - (int)(radius * System.Math.Cos(angle));
                    int y = Math.Max(ylen / 2 - (int)(radius * System.Math.Sin(angle)),1);
                    if (j > 0)
                    {
                        if (y == ff)
                        {

                        }
                        else
                        {
                            tempgreencount += 1;
                        }
                        Mapnum[x, y, 0] = 2;
                        ff = y;
                    }
                    else
                    {
                        countp += 1;
                        Mapnum[x, y, 0] = 1;
                        Mapnum[x, y, 1] = 1;
                    }
                }
            }
        }

        bool impossmove = false;
        int countl = 0;
        int[,] Colorv = new int[9999, 3];
        int tempy = 0;
        int islandnuma = 1;
        int[,] islnum = new int[xlen, ylen];
        int tempi = 0;
        int tempib = 0;

        string[,] prov = new string[xlen, ylen];
        string[] listprovn = new string[10001];

        bool rtn = false;
        Color[] tmpcoler = new Color[100000];
        int ccount;

        int[] convprov = new int[5000];
        string[,] newprov = new string[xlen, ylen];

        private void timer1_Tick(object sender, EventArgs e)
        {
            //temp
            label1.Text = day + ","  + month + "," +  year + AD + ", " + era ;
            label2.Text = age;

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {

                //g.DrawRectangle(Pens.Orange, 0, 0, xlen - 17, ylen - 40);

                if (age == "Provincial Age")
                {
                    //seperate world into different provinces

                    int[] valueof = new int[10000];

                    for(int x = 1; x <= xlen - 30; x+= xlen / 100)
                    {
                        for (int y = 1; y <= ylen - 50; y+= ylen / 100)
                        {

                            int a1 = Rander.Next(1, 255);
                            int a2 = Rander.Next(1, 255);
                            int a3 = Rander.Next(1, 255);
                            Color Randerpaint = new Color();
                            Randerpaint = Color.FromArgb(a1, a2, a3);


                            for (int mx = x; mx <= x + xlen / 100 && mx <= xlen - 1; mx++)
                            {
                                for (int my = y; my <= y + ylen / 100 && my <= ylen - 1; my++)
                                {

                                    if(Map.GetPixel(mx,my) != Color.FromArgb(28,107,160))
                                    {
                                        tmpcoler[ccount] = Randerpaint;
                                        Map.SetPixel(mx, my, Randerpaint);
                                        prov[mx,my] = ccount.ToString();
                                        rtn = true;
                                    }

                                }
                            }
                            if(rtn == true)
                            {
                                ccount++;
                                rtn = false;
                            }
                        }
                    }

                    int convcount = 0;
                    bool[] taken = new bool[10000];
                    int counttken = 2;


                    for (int x = 1; x <= xlen - 1; x++)
                    {
                        for (int y = 1; y <= ylen - 1; y++)
                        {

                            if (y >= ylen || x >= xlen)
                            {
                                
                            }
                            else
                            {
                                if (prov[x, y] != null)
                                {
                                    if (valueof[Convert.ToInt32(prov[x, y])] > 0)
                                    {
                                        newprov[x, y] = valueof[Convert.ToInt32(prov[x, y])].ToString();
                                    }
                                    else
                                    {
                                        taken[counttken] = true;
                                        valueof[Convert.ToInt32(prov[x, y])] = counttken;
                                        newprov[x, y] = counttken.ToString();
                                        counttken += 1;
                                    }
                                }
                            }
                        }
                    }

                    //System.IO.File.Create(pathing + "//World.dat"); //Stores World tiles
                    System.IO.StreamWriter Writer = new System.IO.StreamWriter(pathing + "//World.dat");

                    for (int y = 1; y <= ylen - 1; y++)
                    {
                        for (int x = 1; x <= xlen - 1; x++)
                        {
                            if(newprov[x,y] == null)
                            {
                                Writer.Write("*"); //blank space
                            }
                            else
                            {
                                Writer.Write("@" + newprov[x,y] + ","); //taken land
                            }
                        }
                        Writer.Write("~"); //end char
                        Writer.Write("\r\n"); //new line

                    }

                    Writer.Close();

                    System.IO.StreamWriter Writer2 = new System.IO.StreamWriter(pathing + "//Provs.dat");

                    for (int m = 2; m <= counttken + 1; m++)
                    { 
                        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%
                        Writer2.Write("$" + m + "%");
                        Writer2.Write(ProvList[m] + "%");
                        Writer2.Write("PAGAN%");
                        Writer2.Write(ProvList[m] + "%");
                        Writer2.Write(Rander.Next(100,150) + "%");
                        Writer2.Write(Rander.Next(151, 250) + "%");
                        Writer2.Write(Rander.Next(250, 300) + "%");
                        Writer2.Write(Rander.Next(300, 500) + "%");
                        Writer2.Write(Rander.Next(500, 700) + "%");
                        Writer2.Write(Rander.Next(100, 200) + "%");
                        Writer2.Write(Rander.Next(100, 300) + "%");
                        Writer2.Write("5%");
                        Writer2.Write("Y%");
                        Writer2.Write("~"); //end char
                        Writer2.Write("\r\n"); //new line
                    }

                    Writer2.Close();

                    System.IO.StreamWriter Writer3 = new System.IO.StreamWriter(pathing + "//Kingdoms.dat");

                    for (int m = 2; m <= counttken + 1; m++)
                    {
                        //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%
                        Writer3.Write("$" + m + "%");
                        Writer3.Write(ProvList[m] + "%");
                        Writer3.Write("TRIBAL%");
                        Writer3.Write("NULL%");
                        Writer3.Write("( " + "%" + m + "%" + "," + " ) %");
                        Writer3.Write(Rander.Next(-5, 5) + "%");
                        Writer3.Write(Rander.Next(-5, 5) + "%");
                        Writer3.Write("0%");
                        Writer3.Write(NamesList[Rander.Next(1,500)] + "%");
                        Writer3.Write(SurnameList[Rander.Next(1, 500)] + "%");
                        Writer3.Write(Rander.Next(16, 50)+ "%");
                        Writer3.Write("~"); //end char
                        Writer3.Write("\r\n"); //new line
                    }

                    Writer3.Close();

                    age = "Post-Provincial Age";

                }
                else if(age == "Post-Provincial Age")
                {
                    try
                    {
                        //if mouse is outside the border it crashes
                        Point p = this.PointToClient(Cursor.Position);
                        mouseposX = Math.Max(0, p.X);
                        mouseposY = Math.Max(0, p.Y);

                        if (newprov[mouseposX, mouseposY] != null)
                        {
                            Province.Text = ProvList[Convert.ToInt32(newprov[mouseposX, mouseposY])];
                        }
                        else
                        {
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                else if(age == "Age of Formation")
                {
                    year = (Convert.ToInt32(countp / 3.7) - countl);
                    month = 1;
                    day = 1;
                    AD = "BH";
                    era = "Prehistoric era";

                for (int x = 2; x <= xlen - 2; x++)
                {
                    for (int y = 2; y <= ylen - 2; y++)
                    {

                        if (Mapnum[x - 1, y - 1, 1] == j)
                        {

                            int rndval = Rander.Next(1, 10);

                            Map.SetPixel(x - 1, y - 1, Color.DarkBlue);
                            islnum[x - 1, y - 1] = -1;

                                if (rndval == 1) //back
                            {
                                if (Mapnum[x - 1, y - 2, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x - 2, y - 1, 1] = j + 1;
                                    Map.SetPixel(x - 2, y - 1, Color.Crimson);
                                }
                                else
                                {
                                    impossmove = true;
                                    rndval = 2;
                                }

                            }

                            if (rndval == 2) //back up
                            {
                                if (Mapnum[x - 2, y, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x - 2, y, 1] = j + 1;
                                    Map.SetPixel(x - 2, y, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 3;
                                }

                            }

                            if (rndval == 3) //back down
                            {
                                if (Mapnum[x - 2, y - 2, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x - 2, y - 2, 1] = j + 1;
                                    Map.SetPixel(x - 2, y - 2, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 4;
                                }

                            }

                            if (rndval == 4) //up 
                            {
                                if (Mapnum[x - 1, y - 1, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x - 1, y - 1, 1] = j + 1;
                                    Map.SetPixel(x - 1, y - 1, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 5;
                                }

                            }

                            if (rndval == 5) //down 
                            {
                                if (Mapnum[x, y - 2, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x, y - 2, 1] = j + 1;
                                    Map.SetPixel(x, y - 2, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 6;
                                }

                            }

                            if (rndval == 6) //forward 
                            {
                                if (Mapnum[x, y - 1, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x, y - 1, 1] = j + 1;
                                    Map.SetPixel(x, y - 1, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 7;
                                }

                            }

                            if (rndval == 7) //forward up 
                            {
                                if (Mapnum[x, y, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x, y, 1] = j + 1;
                                    Map.SetPixel(x, y, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 8;
                                }

                            }

                            if (rndval == 8) //forward down
                            {
                                if (Mapnum[x, y - 2, 0] == 2)
                                {
                                    Mapnum[x - 1, y - 1, 0] = 0;
                                    Mapnum[x - 1, y - 1, 1] = 0;

                                    Mapnum[x, y - 2, 1] = j + 1;
                                    Map.SetPixel(x, y - 2, Color.Crimson);
                                }
                                else
                                {
                                    rndval = 9;
                                }

                            }

                            if (rndval == 9) //not
                            {
                                if (impossmove == true)
                                {
                                    countl += 1;
                                    for (int splii = 0; splii <= 999999; splii++)
                                    {
                                        int opx = Rander.Next(0, xlen - 1);
                                        int opy = Rander.Next(0, ylen - 1);

                                        if (Rander.Next(1, 3) == 2)
                                        {
                                            if (Mapnum[opx, opy, 0] == 2)
                                            {
                                                Mapnum[opx, opy, 1] = j + 1;
                                                Map.SetPixel(opx, opy, Color.Crimson);
                                                break;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (countl >= Convert.ToInt32(countp / 3.7))
                                    {
                                            age = "Pangeaic age";
                                    }
                                }
                                else
                                {
                                    Mapnum[x - 1, y - 1, 1] = j + 1;
                                }
                            }

                            }
                            impossmove = false;
                        }
                    }
                }
                else
                {

                
                    }
            }

                pictureBox1.Refresh();
                j += 1;

                if(age == "Pangeaic age")
                {
                    month += 1;
                    if(month > 12)
                    {
                        month = 1;
                        year += 1;
                    }
                    day = 1;
                    AD = "PF";
                    era = "Prehistoric era";

                    //islandnuma = 0;
                    tempy += 1;
                    if (tempy <= ylen - 1)
                    {
                        for (int x = 1; x <= xlen - 1; x++)
                        {
                            if (Mapnum[x, tempy, 0] == 2 && islnum[x, tempy] == 0)
                            {
                                islandnuma += 1;
                                int a1 = Rander.Next(1, 255);
                                int a2 = Rander.Next(1, 255);
                                int a3 = Rander.Next(1, 255);
                                Color Randerpaint = new Color();
                                Randerpaint = Color.FromArgb(a1, a2, a3);
                                Point xy = new Point(x, tempy);
                                FloodFill(Map, xy,Color.Green, Randerpaint);
                            }
                        }
                    }
                    else
                    {
                        age = "Pre-Islandic Age";
                    }

                    
            }

            else if (age == "Islandic Age")
            {
                int count = 0;
                year += Rander.Next(1,10000);


                int xchanget;
                int ychanget;

                for (int x = 1; x <= xlen - 1; x++)
                {
                    for (int y = 1; y <= ylen - 1; y++)
                    {

                        if (islnum[x, y] != 0 && islnum[x, y] != -1)
                        {
                            xchanget = xchange[islnum[x, y]];
                            ychanget = ychange[islnum[x, y]];

                            if(x + xchanget < xlen - 2 && y + ychanget < ylen - 2 && x + xchanget > 2 && y + ychanget > 2)
                            {
                              newMap[x + xchanget, y + ychanget] = 1;
                            }

                            //try
                            //{
                                //newMap[x + xchanget, y + ychanget] = 1;
                            //}
                            //catch
                            //{

                            //}
                        }
                        else
                        {
                            if (islnum[x, y] == -1)
                            {
                                newMap[x, y] = 0;
                            }
                        }
                    }
                }

                //for (int x = 1; x <= xlen - 1; x++)
                //{
                //    for (int y = 1; y <= ylen - 1; y++)
                //    {
                //        count += 1;

                //        if (newMap[x, y] == 1)
                //        {
                //            newMapS[count] = 1;
                //        }
                //        else
                //        {
                //            newMapS[count] = 0;
                //        }
                //    }
                //}

                using (var g = Graphics.FromImage(pictureBox1.Image))
                {
                    Dobits();
                    bg = ((green / (green + blue))) * 100;
                    Console.WriteLine(bg);
                    age = "Provincial Age";
                }
            }

            else if (age == "Pre-Islandic Age")
            {
                year += 1;
                day = 1;
                AD = "PF";
                era = "Prehistoric era";


                for (int x = 1; x <= xlen - 1; x++)
                {
                    for (int y = 1; y <= ylen - 1; y++)
                    {
                        if (islnum[x, y] != 0 && islnum[x, y] != -1)
                        {
                            int tempisln = islnum[x, y];

                            if (newpos[tempisln].X == 0)
                            {
                                newpos[tempisln].X = Rander.Next(1, xlen); //random new position
                                newpos[tempisln].Y = Rander.Next(50, ylen - 50);

                                xchange[tempisln] = newpos[tempisln].X - x; //change in coords
                                ychange[tempisln] = newpos[tempisln].Y - y;
                            }


                        }
                    }
                }

                age = "Islandic Age";

            }
        }
        

        int[] xchange = new int[9999];
        int[] ychange = new int[9999];
        Point[] newpos = new Point[9999];
        byte[,] newMap = new byte[xlen + 1,ylen];
        //int[,] newMapS = new int[xlen * ylen) + 1,2];

        private void Dobits() //PaintEventArgs e
        {

            // Create a new bitmap.
            Bitmap bmp = Map;

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, xlen - 17, ylen - 40);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat); //32 ARGB

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            // Set every third value to 255. A 24bpp bitmap will look red.  

            int tcount = 0;

            for (int y = 1; y <= ylen - 41; y++)
            {
                int l = y * Math.Abs(bmpData.Stride);

                for (int x = 1; x <= xlen - 18; x++)
                {
                    int dx = l + x * 4;

                    if(newMap[x,y] == 1) //land
                    {
                        if (newMap[x, y + 1] == 0 && newMap[x, y - 1] == 0 && newMap[x + 1, y] == 0 && newMap[x - 1, y] == 0) //if its a single island
                        {
                            if (Rander.Next(1, 41) == 15)
                            {
                                green += 1;
                                rgbValues[dx + 3] = 255; //alpha
                                rgbValues[dx + 2] = 32; //red
                                rgbValues[dx + 1] = 176; //green
                                rgbValues[dx] = 124; //blue
                            }
                            else
                            {
                                rgbValues[dx + 3] = 255; //alpha
                                rgbValues[dx + 2] = 28; //red
                                rgbValues[dx + 1] = 107; //green
                                rgbValues[dx] = 160; //blue
                            }
                        }
                        else
                        {
                            if (newMap[x, y + 1] == 1 && newMap[x, y - 1] == 1 && newMap[x + 1, y] == 1 && newMap[x - 1, y] == 1)
                            {
                                green += 1;

                                if (newMap[x + 1, y + 1] == 1 && newMap[x + 1, y - 1] == 1 && newMap[x - 1, y + 1] == 1 && newMap[x - 1, y - 1] == 1) //land
                                {
                                    rgbValues[dx + 3] = 255; //alpha
                                    rgbValues[dx + 2] = 78; //red
                                    rgbValues[dx + 1] = 176; //green
                                    rgbValues[dx] = 134; //blue
                                }
                                else
                                {
                                    rgbValues[dx + 3] = 255; //alpha
                                    rgbValues[dx + 2] = 32; //red
                                    rgbValues[dx + 1] = 176; //green
                                    rgbValues[dx] = 124; //blue
                                }
                            }
                            else
                            {
                                rgbValues[dx + 3] = 255; //alpha
                                rgbValues[dx + 2] = 32; //red
                                rgbValues[dx + 1] = 176; //green
                                rgbValues[dx] = 124; //blue
                            }
                        }
                    }
                    else
                    {
                        //water
                        blue += 1;
                            rgbValues[dx + 3] = 255; //alpha
                            rgbValues[dx + 2] = 28; //red
                            rgbValues[dx + 1] = 107; //green
                            rgbValues[dx] = 160; //blue
                    }
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            //using (var g = Graphics.FromImage(pictureBox1.Image))
            //{
            //    // Draw the modified image.
            //    g.DrawImage(bmp, 0, 0);
            //}

        }

        double green = 0;
        double blue = 0;
        double bg = 0;

        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor) //code from https://simpledevcode.wordpress.com/2015/12/29/flood-fill-algorithm-using-c-net/ by Karim Oumghar
        {
            colours[islandnuma] = replacementColor;
            Stack<Point> pixels = new Stack<Point>();
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < bmp.Width && a.X > 0 &&
                        a.Y < bmp.Height && a.Y > 0)
                {

                    if (bmp.GetPixel(a.X, a.Y) == targetColor)
                    {
                        bmp.SetPixel(a.X, a.Y, replacementColor);
                        islnum[a.X, a.Y] = islandnuma;
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            pictureBox1.Refresh();
            return;
        }

        public void clearmap()
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush newBrush = new SolidBrush(Color.MidnightBlue);
                g.FillRectangle(newBrush,0,0,xlen,ylen);
            }
        }
        public void refreshmap()
        {

            for (int x = 1; x <= xlen; x++)
            {
                using (var g = Graphics.FromImage(pictureBox1.Image))
                {
                    for (int y = 1; y <= ylen; y++)
                    {
                        //Point p = this.PointToClient(Cursor.Position);
                        //Pen penip = new Pen(Color.FromArgb(255, 0, 0, 0));
                        if (Mapnum[x - 1, y - 1, 0] == 0)
                        {
                            Map.SetPixel(x - 1, y - 1, Color.DarkBlue);
                            islnum[x - 1, y - 1] = -1;
                            pictureBox1.Refresh();
                        }

                        if (Mapnum[x - 1, y - 1, 0] == 1)
                        {
                            Map.SetPixel(x - 1, y - 1, Color.DarkRed);
                            islnum[x - 1, y - 1] = 0;
                            //g.DrawRectangle(Pens.White, x - 1, y - 1, 1, 1);
                            //g.DrawLine(Pens.White, x, y, x + 1, y + 1);
                            //g.DrawLine(Pens.OrangeRed, 0, 0, 100, 100);
                            //g.DrawLine(Pens.White, xlen / 2 - x, ylen / 2 - y, xlen / 2 - (x + 1), ylen / 2 - (y - 1));
                            pictureBox1.Refresh();
                        }

                        if (Mapnum[x - 1, y - 1, 0] == 2)
                        {
                            Map.SetPixel(x - 1, y - 1, Color.ForestGreen);
                            islnum[x - 1, y - 1] = 0;
                            pictureBox1.Refresh();
                        }
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }


}


