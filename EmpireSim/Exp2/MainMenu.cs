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
    public partial class MainMenu : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        static int xlen = 1400;
        static int ylen = 900;
        Bitmap Bg = new Bitmap(Exp2.Properties.Resources.testimage, xlen, ylen);
        Icon Ic = new Icon(Exp2.Properties.Resources.MainMenu,64,64);
        Color[,] bitmapc = new Color[xlen, ylen];
        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();
        Font myFont;
        Font myFontB;
        Font myFontsmall;
        Font myFontsmallClick;
        Font myFontDetail;
        Random Rand = new Random();
        int mouseposX;
        int mouseposY;
        //pfc.AddFontFile(Path.Combine(Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin")) + "Font.ttf"));
        //private Font Arcade;

        public MainMenu()
        {
            InitializeComponent();
            MenuImg.Image = Bg;
            this.Icon = Ic;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //alot of this is copy and pasted from sources on the internet from free use sources
            byte[] fontData = Exp2.Properties.Resources.ArcadeAlternate;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Exp2.Properties.Resources.ArcadeAlternate.Length);
            AddFontMemResourceEx(fontPtr, (uint)Exp2.Properties.Resources.ArcadeAlternate.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 32.0F);
            myFontB = new Font(fonts.Families[0], 52.0F);
            myFontsmall = new Font(fonts.Families[0], 22.0F);
            myFontsmallClick = new Font(fonts.Families[0], 22.0F,FontStyle.Underline);
            myFontDetail = new Font(fonts.Families[0], 12.0F);
            //pfc.AddFontFile(Path.Combine(Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin")) + "Font.ttf"));
            //Font Arcade = new Font(privateFonts.Families[0], 12, FontStyle.Regular);
            //privateFonts.AddFontFile("ArcadeAlternate.ttf");
            this.Size = new System.Drawing.Size(xlen, ylen);
            //MenuImg.Image = Bg;

            for (int x = 0; x <= xlen - 1; x++)
            {
                for (int y = 0; y <= ylen - 1; y++)
                {
                    if (y >= (ylen / 20) * 16)
                    {
                        if (y <= (ylen / 20) * 17)
                        {
                            int temprand = Rand.Next(2, 10);
                            if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(80, 41, 0);
                            }
                            else if(temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(70, 31, 0);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(60, 21, 0);
                            }
                        }
                        else
                        {
                            if (x <= (xlen / 20) * 2 || x >= (xlen / 20) * 18)
                            {
                                int temprand = Rand.Next(2, 100);
                                if (temprand == 6)
                                {
                                    bitmapc[x, y] = Color.FromArgb(30, 1, 0);
                                }
                                else
                                {
                                    bitmapc[x, y] = Color.FromArgb(50, 11, 0);
                                }
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(0, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        int temprand = Rand.Next(2, 10);
                        if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(150, 111, 51);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(160, 121, 61);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(170, 131, 71);
                        }
                        else if (temprand == 6)
                        {
                            bitmapc[x, y] = Color.FromArgb(140, 101, 41);
                        }
                        else if (temprand == 7)
                        {
                            bitmapc[x, y] = Color.FromArgb(130, 91, 31);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(180, 141, 81);
                        }
                    }
                }
            }

            for (int x = (xlen / 5); x <= ((xlen / 5) * 4) - 1; x++) //title box
            {
                for (int y = (ylen / 10); y <= ((ylen / 10) * 3) - 1; y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 5) + 5 || x >= ((xlen / 5) * 4) - 6)
                    {
                        if (y == (ylen / 10)|| y == (((ylen / 10) * 3) - 1))
                        {
                            bitmapc[x, y] = Color.Gold;
                            bitmapc[x, y + 1] = Color.Gold;
                            bitmapc[x, y + 2] = Color.Gold;
                            bitmapc[x, y - 1] = Color.Gold;
                            bitmapc[x, y - 2] = Color.Gold;
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                            }
                        }
                    }
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                        }
                    }

                }
            }

            for (int x = (xlen / 10) * 1; x <= ((xlen / 10) * 2.7); x++) //play box
            {
                for (int y = (ylen / 20) * 7; y <= ((ylen / 20) * 9); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 10) * 1 + 5 || x >= ((xlen / 10) * 2.7) - 6)
                    {
                        if (y == (ylen / 20) * 7 || y == ((ylen / 20) * 9))
                        {
                            bitmapc[x, y] = Color.Gold;
                            bitmapc[x, y + 1] = Color.Gold;
                            bitmapc[x, y + 2] = Color.Gold;
                            bitmapc[x, y - 1] = Color.Gold;
                            bitmapc[x, y - 2] = Color.Gold;
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                            }
                        }
                    }
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                        }
                    }
                }
            }

            for (int x = (xlen / 10) * 1; x <= ((xlen / 10) * 2.7); x++) // Create World box
            {
                for (int y = (ylen / 20) * 10; y <= ((ylen / 20) * 12); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 10) * 1 + 5 || x >= ((xlen / 10) * 2.7) - 6)
                    {
                        if (y == (ylen / 20) * 10 || y == ((ylen / 20) * 12))
                        {
                            bitmapc[x, y] = Color.Gold;
                            bitmapc[x, y + 1] = Color.Gold;
                            bitmapc[x, y + 2] = Color.Gold;
                            bitmapc[x, y - 1] = Color.Gold;
                            bitmapc[x, y - 2] = Color.Gold;
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                            }
                        }
                    }
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                        }
                    }
                }
            }

            for (int x = (xlen / 10) * 1; x <= ((xlen / 10) * 2.7); x++) // Options Box
            {
                for (int y = (ylen / 20) * 13; y <= ((ylen / 20) * 15); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 10) * 1 + 5 || x >= ((xlen / 10) * 2.7) - 6)
                    {
                        if (y == (ylen / 20) * 13 || y == ((ylen / 20) * 15))
                        {
                            bitmapc[x, y] = Color.Gold;
                            bitmapc[x, y + 1] = Color.Gold;
                            bitmapc[x, y + 2] = Color.Gold;
                            bitmapc[x, y - 1] = Color.Gold;
                            bitmapc[x, y - 2] = Color.Gold;
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                            }
                        }
                    }
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                        }
                    }
                }
            }

            for (int x = (xlen / 10) * 3; x <= ((xlen / 10) * 8) - 1; x++) //bbox
            {
                for (int y = (ylen / 20) * 7; y <= ((ylen / 20) * 15) - 1; y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 10) * 3 + 5 || x >= ((xlen / 10) * 8) - 6)
                    {
                        if (y == (ylen / 20) * 7 || y == (((ylen / 20) * 15) - 1))
                        {
                            bitmapc[x, y] = Color.Gold;
                            bitmapc[x, y + 1] = Color.Gold;
                            bitmapc[x, y + 2] = Color.Gold;
                            bitmapc[x, y - 1] = Color.Gold;
                            bitmapc[x, y - 2] = Color.Gold;
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                            }
                        }
                    }
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                        }
                    }
                }
            }


            using (var g = Graphics.FromImage(MenuImg.Image))
            {
                for (int x = 0; x <= xlen - 1; x++)
                {
                    for (int y = 0; y <= ylen - 1; y++)
                    {

                        Bg.SetPixel(x, y, bitmapc[x,y]);
                    }
                }
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //options
            {
                Brush newbrush = new SolidBrush(Color.DarkSlateGray);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                g.DrawString("Options", myFont, newbrush, newpoint);
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //Create World
            {
                Brush newbrush = new SolidBrush(Color.DarkSlateGray);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                g.DrawString("Generate", myFont, newbrush, newpoint);
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //Play World
            {
                Brush newbrush = new SolidBrush(Color.DarkSlateGray);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                g.DrawString("Start", myFont, newbrush, newpoint);
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //Title
            {
                Brush newbrush = new SolidBrush(Color.Black);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 1.93)), Convert.ToInt16((ylen / 10) * 1.69));
                g.DrawString("Iron Age", myFontB, newbrush, newpoint);
            }

            Timera.Enabled = true;
            Timera.Start();
            //Datainputtmr.Start();
        }

        int[] objanimatestage = new int[5];

        private void Timer_Tick(object sender, EventArgs e)
        {
            MenuImg.Invalidate();
            Point p = this.PointToClient(Cursor.Position);
            mouseposX = Math.Max(0, p.X);
            mouseposY = Math.Max(0, p.Y);

            if(rndcycle != 0)
            {
                using (var g = Graphics.FromImage(MenuImg.Image))
                {
                    Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
                    Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);
                    Brush newbrushA = new SolidBrush(Color.DarkRed);

                    if (rndcycle == 1)
                    {
                        //provinces
                        opengenerate("Prov");
                        writeto("Provinces");
                        Array.Clear(allgen, 0, 10001);
                        rndcycle = 2;
                    }
                    else if (rndcycle == 2)
                    {
                        opengenerate("Faith");
                        writeto("Religions");
                        Array.Clear(allgen, 0, 10001);
                        rndcycle = 3;
                    }
                    else if (rndcycle == 3)
                    {
                        opengenerate("Fname");
                        writeto("Fname");
                        Array.Clear(allgen, 0, 10001);
                        rndcycle = 4;
                    }
                    else if (rndcycle == 4)
                    {
                        opengenerate("Sname");
                        writeto("Sname");
                        Array.Clear(allgen, 0, 10001);
                        rndcycle = 5;
                    }
                    else if(rndcycle == 5)
                    {
                        Point newpointb6 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 13.52));
                        g.DrawString("Generating Map...", myFontsmall, newbrushD, newpointb6);
                        rndcycle = 6;
                    }
                    else if(rndcycle == 6)
                    {
                        Timera.Stop();
                        Form1 genmap = new Form1();
                        genmap.Text = savpath; 
                        genmap.Show();
                        this.Hide();
                    }
                }
            }
            else if (selectedobj == 1)
            {
                using (var g = Graphics.FromImage(MenuImg.Image))
                {
                    Point newpointm = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    animate(1, objanimatestage[1], "Red", Color.DarkSlateGray, newpointm, "Start");

                    Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("Generate", myFont, newbrushb, newpointb);
                    Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                    Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                    g.DrawString("Options", myFont, newbrushc, newpointc);
                    objanimatestage[2] = 0;
                    objanimatestage[3] = 0;
                }
            }
            else if (selectedobj == 2)
            {
                using (var g = Graphics.FromImage(MenuImg.Image))
                {
                    Point newpointm = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                    animate(2, objanimatestage[2], "Red", Color.DarkSlateGray, newpointm, "Generate");

                    Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Start", myFont, newbrusha, newpointa);
                    Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                    Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                    g.DrawString("Options", myFont, newbrushc, newpointc);
                    objanimatestage[1] = 0;
                    objanimatestage[3] = 0;
                }
            }
            else if (selectedobj == 3)
            {
                using (var g = Graphics.FromImage(MenuImg.Image))
                {
                    Point newpointm = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                    animate(3, objanimatestage[3], "Red", Color.DarkSlateGray, newpointm, "Options");


                    Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Start", myFont, newbrusha, newpointa);
                    Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("Generate", myFont, newbrushb, newpointb);
                    objanimatestage[1] = 0;
                    objanimatestage[2] = 0;
                }
            }
            else
            {

                using (var g = Graphics.FromImage(MenuImg.Image))
                {

                    if (mouseposX != 0 && mouseposY != 0)
                    {
                        if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 7) && mouseposY <= ((ylen / 20) * 9)) //Play box
                        {
                            Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                            animate(1, objanimatestage[1], "Red", Color.DarkSlateGray, newpoint, "Start");

                            Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                            Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                            g.DrawString("Generate", myFont, newbrushb, newpointb);
                            Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                            Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                            g.DrawString("Options", myFont, newbrushc, newpointc);
                            objanimatestage[2] = 0;
                            objanimatestage[3] = 0;

                        }
                        else
                        {
                            if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 10) && mouseposY <= ((ylen / 20) * 12))
                            {
                                Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                animate(2, objanimatestage[2], "Red", Color.DarkSlateGray, newpoint, "Generate");

                                Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                g.DrawString("Start", myFont, newbrusha, newpointa);
                                Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                                Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                g.DrawString("Options", myFont, newbrushc, newpointc);
                                objanimatestage[1] = 0;
                                objanimatestage[3] = 0;

                            }
                            else
                            {
                                if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 13) && mouseposY <= ((ylen / 20) * 15))
                                {
                                    Point newpoint = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                    animate(3, objanimatestage[3], "Red", Color.DarkSlateGray, newpoint, "Options");

                                    Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                    g.DrawString("Start", myFont, newbrusha, newpointa);
                                    Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                    g.DrawString("Generate", myFont, newbrushb, newpointb);
                                    objanimatestage[1] = 0;
                                    objanimatestage[2] = 0;

                                }
                                else
                                {
                                    Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                    g.DrawString("Start", myFont, newbrusha, newpointa);
                                    Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                    g.DrawString("Generate", myFont, newbrushb, newpointb);
                                    Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                                    Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                    g.DrawString("Options", myFont, newbrushc, newpointc);
                                    objanimatestage[1] = 0;
                                    objanimatestage[2] = 0;
                                    objanimatestage[3] = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void animate(int objnum, int currentobjnum,string colourshift,Color currcol, Point inputpo,string inputstring)
        {
            MenuImg.Invalidate();

            using (var g = Graphics.FromImage(MenuImg.Image))
            {
                if (colourshift == "Red")
                {
                    if (currentobjnum + currcol.R >= 255 || objanimatestage[objnum] < 0) // && currentobjnum >= currcol.R
                    {
                        if (objanimatestage[objnum] > -1)
                        {
                            objanimatestage[objnum] = -1;
                        }
                        else
                        {

                            if (currentobjnum + 255 <= currcol.R)
                            {
                                objanimatestage[objnum] = 1;
                            }
                            else
                            {
                                Brush newbrusham = new SolidBrush(Color.FromArgb(currentobjnum + 255, currcol.B, currcol.G));
                                g.DrawString(inputstring, myFont, newbrusham, inputpo);
                                objanimatestage[objnum] = currentobjnum - 13;
                            }
                        }
                    }
                    else
                    {
                        Brush newbrusham = new SolidBrush(Color.FromArgb(currentobjnum + currcol.R, currcol.B, currcol.G));
                        g.DrawString(inputstring, myFont, newbrusham, inputpo);
                        objanimatestage[objnum] = currentobjnum + 13;
                    }
                }
            }
        }

        int selectedobj = 0;

        private void Datainputtmr_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);
            mouseposX = Math.Max(0,p.X);
            mouseposY = Math.Max(0,p.Y);
            //label1.Text = x + "," + y;
            //abel2.Text = xstore + "," + ystore; 
        }

        public void updatebigbox(int mode)
        {
            outcancel = true;
            outname = null;
            existing = false;
            random = false;

            using (var g = Graphics.FromImage(MenuImg.Image))
            {
                for (int x = (xlen / 10) * 3; x <= ((xlen / 10) * 8) - 1; x++) //bbox
                {
                    for (int y = (ylen / 20) * 7; y <= ((ylen / 20) * 15) - 1; y++)
                    {
                        //bitmapc[x, y] = Color.HotPink;
                        //bitmapc[x, y] = Color.HotPink;
                        int temprand = Rand.Next(2, 10);

                        if (x <= (xlen / 10) * 3 + 5 || x >= ((xlen / 10) * 8) - 6)
                        {
                            if (y == (ylen / 20) * 7 || y == (((ylen / 20) * 15) - 1))
                            {
                                bitmapc[x, y] = Color.Gold;
                                bitmapc[x, y + 1] = Color.Gold;
                                bitmapc[x, y + 2] = Color.Gold;
                                bitmapc[x, y - 1] = Color.Gold;
                                bitmapc[x, y - 2] = Color.Gold;
                            }
                            else
                            {
                                if (temprand == 2)
                                {
                                    bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                                }
                                else if (temprand == 3)
                                {
                                    bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                                }
                                else if (temprand == 4)
                                {
                                    bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                                }
                                else if (temprand == 5)
                                {
                                    bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                                }
                                else
                                {
                                    bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                                }
                            }
                        }
                        else
                        {
                            if (temprand == 2)
                            {
                                bitmapc[x, y] = Color.FromArgb(246, 221, 155);
                            }
                            else if (temprand == 3)
                            {
                                bitmapc[x, y] = Color.FromArgb(253, 235, 185);
                            }
                            else if (temprand == 4)
                            {
                                bitmapc[x, y] = Color.FromArgb(247, 225, 175);
                            }
                            else if (temprand == 5)
                            {
                                bitmapc[x, y] = Color.FromArgb(255, 231, 173);
                            }
                            else
                            {
                                bitmapc[x, y] = Color.FromArgb(238, 223, 166);
                            }
                        }
                    }
                }

                    for (int x = (xlen / 10) * 3; x <= ((xlen / 10) * 8) - 1; x++)
                    {
                        for (int y = (ylen / 20) * 7; y <= ((ylen / 20) * 15) - 1; y++)
                        {
                            Bg.SetPixel(x, y, bitmapc[x, y]);
                        }
                    }

                Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
                Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);
                MenuImg.Invalidate();
                if (mode == 1)
                {

                    random = false;
                    existing = false;
                    obs = false;
                    sim = false;
                    browse = false;

                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Selected Save:", myFontsmall, newbrushD, newpointa);

                    Point newpointa1 = new Point(Convert.ToInt16(((xlen / 10) * 4.56)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("_________", myFontsmall, newbrushD, newpointa1);

                    Point newpointa2 = new Point(Convert.ToInt16(((xlen / 10) * 6.56)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Browse", myFontsmallClick, newbrushC, newpointa2);

                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("World Name:", myFontsmall, newbrushD, newpointb);

                    Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 9.52));
                    g.DrawString("Year:", myFontsmall, newbrushD, newpointc);

                    Point newpointd = new Point(Convert.ToInt16(((xlen / 10) * 7.16)), Convert.ToInt16((ylen / 20) * 13.62));
                    g.DrawString("Start >", myFontsmallClick, newbrushC, newpointd);

                    Point newpointe = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("Mode:", myFontsmall, newbrushD, newpointe);

                    Point newpointe1 = new Point(Convert.ToInt16(((xlen / 10) * 3.76)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("Observe", myFontsmall, newbrushC, newpointe1);

                    Point newpointe2 = new Point(Convert.ToInt16(((xlen / 10) * 4.66)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("/", myFontsmall, newbrushD, newpointe2);

                    Point newpointe3 = new Point(Convert.ToInt16(((xlen / 10) * 4.86)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("Simulate", myFontsmall, newbrushC, newpointe3);
                }
                else if(mode == 2)
                {

                    random = false;
                    existing = false;
                    obs = false;
                    sim = false;
                    browse = false;

                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Save Location:", myFontsmall, newbrushD, newpointa);

                    Point newpointa1 = new Point(Convert.ToInt16(((xlen / 10) * 4.56)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("_________", myFontsmall, newbrushD, newpointa1);

                    Point newpointa2 = new Point(Convert.ToInt16(((xlen / 10) * 6.56)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("Browse", myFontsmallClick, newbrushC, newpointa2);

                    Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("Province Names From:", myFontsmall, newbrushD, newpointb);

                    Point newpointb1 = new Point(Convert.ToInt16(((xlen / 10) * 5.56)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("Random", myFontsmall, newbrushC, newpointb1);

                    Point newpointb2 = new Point(Convert.ToInt16(((xlen / 10) * 6.56)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("/", myFontsmall, newbrushD, newpointb2);

                    Point newpointb3 = new Point(Convert.ToInt16(((xlen / 10) * 6.90)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("Existing", myFontsmall, newbrushC, newpointb3);

                    Point newpointd = new Point(Convert.ToInt16(((xlen / 10) * 6.76)), Convert.ToInt16((ylen / 20) * 13.62));
                    g.DrawString("Generate >", myFontsmallClick, newbrushC, newpointd);
                }
                else if (mode == 3)
                {

                    random = false;
                    existing = false;
                    obs = false;
                    sim = false;
                    browse = false;

                    Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 7.52));
                    g.DrawString("About", myFontsmall, newbrushD, newpointa);

                    Point newpointa2 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 8.52));
                    g.DrawString("Iron Age is a program developed by James Benjamin Brimelow Gorman", myFontDetail, newbrushD, newpointa2);

                    Point newpointa3 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 9.52));
                    g.DrawString("While the program is not officially copyrighted under UK laws", myFontDetail, newbrushD, newpointa3);

                    Point newpointa4 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 10.02));
                    g.DrawString("i would respectfully request you refrain from redistributing or selling this software", myFontDetail, newbrushD, newpointa4);

                    Point newpointa5 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 10.52));
                    g.DrawString("the project should always be available for download from my github page:", myFontDetail, newbrushD, newpointa5);

                    Point newpointa6 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 11.02));
                    g.DrawString("https://github.com/JaVonox/Iron_Age", myFontDetail, newbrushD, newpointa6);

                    Point newpointa7 = new Point(Convert.ToInt16(((xlen / 10) * 6.56)), Convert.ToInt16((ylen / 20) * 11.52));
                    g.DrawString(" - Jamie <3", myFontDetail, newbrushD, newpointa7);
                }

                else
                {

                }
            }
        }

        string outname = null;
        bool outcancel = true;
        bool FileSpecified = false;
        string savpath = null;

        public void filebrowse(string mode)
        {
            if (mode == "WriteNew")
            {
                browse = true;
                SaveFileDialog savedialog = new SaveFileDialog();

                savedialog.Title = "Select A Place To Save The World";
                savedialog.Filter = "*Save Folder | Save Location";

                if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string newFileName = savedialog.FileName;
                    savpath = newFileName;
                    System.IO.Directory.CreateDirectory(newFileName); //main directory for common files
                    outname = System.IO.Path.GetFileNameWithoutExtension(newFileName);
                    //System.IO.File.Create(newFileName + "//World.dat"); //Stores World tiles
                    //System.IO.File.Create(newFileName + "//Kingdoms.dat"); //Stores existing kingdoms and their provinces
                    System.IO.Directory.CreateDirectory(newFileName + "//Data"); //Data for key data such as world year/name etc.
                    System.IO.File.Create(newFileName + "//Data//Key.dat"); //stores key stuff like names/year
                    System.IO.File.Create(newFileName + "//Data//Provinces.dat"); //Stores Names/Info for provinces
                    System.IO.Directory.CreateDirectory(newFileName + "//Info"); //Data for minor data such as name generation data
                    System.IO.File.Create(newFileName + "//Info//History.dat"); //Stores Historical Data
                    outcancel = false;
                    FileSpecified = true;
                }
                else
                {
                    outcancel = true;
                }
            }
            else if(mode == "Open")
            {
                browse = true;
                OpenFileDialog opendialog = new OpenFileDialog();

                opendialog.Title = "Select the GameInfo file";
                opendialog.Filter = "Save file | *.sav";

                if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string newFileName = opendialog.FileName;
                    savpath = newFileName;
                    outname = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(newFileName));

                    outcancel = false;
                    FileSpecified = true;
                }
                else
                {
                    outcancel = true;
                }
            }
        }

        public void writeto(string type)
        {
            Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
            if(FileSpecified = true)
            {
                if(type == "Provinces")
                {
                    try
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            System.IO.File.WriteAllLines(savpath + "//Info//ProvinceNames.dat", allgen);
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 9.52));
                            g.DrawString("Complete", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                    catch(Exception ex)
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 9.52));
                            g.DrawString("Failed", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                }
                else if (type == "Religions")
                {
                    try
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            System.IO.File.WriteAllLines(savpath + "//Info//ReligionNames.dat", allgen);
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 10.52));
                            g.DrawString("Complete", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                    catch (Exception ex)
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 10.52));
                            g.DrawString("Failed", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                }
                else if (type == "Fname")
                {
                    try
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            System.IO.File.WriteAllLines(savpath + "//Info//Names.dat", allgen);
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 11.52));
                            g.DrawString("Complete", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                    catch (Exception ex)
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 11.52));
                            g.DrawString("Failed", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                }
                else if (type == "Sname")
                {
                    try
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            System.IO.File.WriteAllLines(savpath + "//Info//Dynasty.dat", allgen);
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 12.52));
                            g.DrawString("Complete", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                    catch (Exception ex)
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 5.90)), Convert.ToInt16((ylen / 20) * 12.52));
                            g.DrawString("Failed", myFontsmall, newbrushD, newpointb4);
                        }
                    }
                }
            }
            else
            {

            }
        }

        public void opengenerate(string type)
        {
            if (type == "Prov")
            {
                if (random == true)
                {
                    for (int i = 0; i <= 10000; i++)
                    {
                        string word = funct.Functions.Generator("Prov");

                        if (i <= 1)
                        {
                            allgen[i] = word;
                        }
                        else
                        {
                            if (allgen[i - 1] == word)
                            {
                                i -= 1;
                            }
                            else
                            {
                                allgen[i] = word;
                            }
                        }
                    }
                }
                else if (existing == true)
                {
                    for (int i = 0; i <= 10000; i++)
                    {
                        string word = funct.Functions.Generator("Real");

                        if (i <= 1)
                        {
                            allgen[i] = word;
                        }
                        else
                        {
                            if (allgen[i - 1] == word)
                            {
                                i -= 1;
                            }
                            else
                            {
                                allgen[i] = word;
                            }
                        }
                    }
                }
            }
            else if(type == "Faith")
            {
                    for (int i = 0; i <= 100; i++)
                    {
                        string word = funct.Functions.Generator("Faith");

                        if (i < 1)
                        {
                            allgen[i] = word;
                        }
                        else
                        {
                            if (allgen[i - 1] == word)
                            {
                                i -= 1;
                            }
                            else
                            {
                                allgen[i] = word;
                            }
                        }
                    }
            }
            else if (type == "Fname")
            {
                for (int i = 0; i <= 500; i++)
                {
                    string word = funct.Functions.Generator("Fname");

                    if (i < 1)
                    {
                        allgen[i] = word;
                    }
                    else
                    {
                        if (allgen[i - 1] == word)
                        {
                            i -= 1;
                        }
                        else
                        {
                            allgen[i] = word;
                        }
                    }
                }
            }
            else if (type == "Sname")
            {
                for (int i = 0; i <= 500; i++)
                {
                    string word = funct.Functions.Generator("Sname");

                    if (i < 1)
                    {
                        allgen[i] = word;
                    }
                    else
                    {
                        if (allgen[i - 1] == word)
                        {
                            i -= 1;
                        }
                        else
                        {
                            allgen[i] = word;
                        }
                    }
                }
            }
            else
            {

            }
        }

        bool existing = false;
        bool random = false;
        bool browse = false;
        bool sim = false;
        bool obs = false;
        string[] allgen = new string[10001];
        int point = 0;
        int rndcycle = 0;

        private void MenuImg_Click(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(MenuImg.Image))
            {
                MenuImg.Invalidate();
                Point p = this.PointToClient(Cursor.Position);
                mouseposX = Math.Max(0, p.X);
                mouseposY = Math.Max(0, p.Y);

                if(rndcycle > 0)
                {
                    
                }
                else if (mouseposX != 0 && mouseposY != 0)
                {
                    if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 7) && mouseposY <= ((ylen / 20) * 9)) //play box
                    {
                        selectedobj = 1;

                        Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                        Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                        g.DrawString("Generate", myFont, newbrushb, newpointb);
                        Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                        Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                        g.DrawString("Options", myFont, newbrushc, newpointc);
                        objanimatestage[2] = 0;
                        objanimatestage[3] = 0;
                        updatebigbox(selectedobj);

                    }
                    else
                    {
                        if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 10) && mouseposY <= ((ylen / 20) * 12))
                        {
                            selectedobj = 2;

                            Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                            Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                            g.DrawString("Start", myFont, newbrusha, newpointa);
                            Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                            Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                            g.DrawString("Options", myFont, newbrushc, newpointc);
                            objanimatestage[1] = 0;
                            objanimatestage[3] = 0;
                            updatebigbox(selectedobj);

                        }
                        else
                        {
                            if (mouseposX >= (xlen / 10) * 1 && mouseposX <= ((xlen / 10) * 2.7) && mouseposY >= ((ylen / 20) * 13) && mouseposY <= ((ylen / 20) * 15))
                            {
                                selectedobj = 3;

                                Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                g.DrawString("Start", myFont, newbrusha, newpointa);
                                Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                g.DrawString("Generate", myFont, newbrushb, newpointb);
                                objanimatestage[1] = 0;
                                objanimatestage[2] = 0;
                                updatebigbox(selectedobj);

                            }
                            else
                            {
                                
                                if(selectedobj == 1) //play screen
                                {
                                    Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
                                    Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);
                                    Brush newbrushA = new SolidBrush(Color.DarkRed);

                                    //Point newpointe1 = new Point(Convert.ToInt16(((xlen / 10) * 3.76)), Convert.ToInt16((ylen / 20) * 10.52));
                                    //g.DrawString("Observe", myFontsmall, newbrushC, newpointe1);

                                    //Point newpointe2 = new Point(Convert.ToInt16(((xlen / 10) * 4.66)), Convert.ToInt16((ylen / 20) * 10.52));
                                    //g.DrawString("/", myFontsmall, newbrushD, newpointe2);

                                    //Point newpointe3 = new Point(Convert.ToInt16(((xlen / 10) * 4.86)), Convert.ToInt16((ylen / 20) * 10.52));
                                    //g.DrawString("Simulate", myFontsmall, newbrushC, newpointe3);
                                    if (mouseposX >= (xlen / 10) * 6.36 && mouseposX <= ((xlen / 10) * 7.36) && mouseposY >= ((ylen / 20) * 7.52) && mouseposY <= ((ylen / 20) * 8.32) && browse == false)
                                    {
                                        filebrowse("Open");

                                        if (outcancel == false)
                                        {
                                            MenuImg.Invalidate();
                                            Point newpointa1 = new Point(Convert.ToInt16(((xlen / 10) * 4.56)), Convert.ToInt16((ylen / 20) * 7.32));
                                            g.DrawString(outname, myFontsmall, newbrushD, newpointa1);
                                            outcancel = true;

                                            System.IO.StreamReader Read = new System.IO.StreamReader(savpath);

                                            Read.ReadLine();
                                            string year = Read.ReadLine();

                                            Point newpointa2 = new Point(Convert.ToInt16(((xlen / 10) * 4.26)), Convert.ToInt16((ylen / 20) * 8.52));
                                            g.DrawString(outname, myFontsmall, newbrushD, newpointa2);

                                            Point newpointa3 = new Point(Convert.ToInt16(((xlen / 10) * 3.66)), Convert.ToInt16((ylen / 20) * 9.52));
                                            g.DrawString(year, myFontsmall, newbrushD, newpointa3);
                                            //filenames will stack over each other upon reentry
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else if (mouseposX >= (xlen / 10) * 3.66 && mouseposX <= ((xlen / 10) * 4.56) && mouseposY >= ((ylen / 20) * 10.52) && mouseposY <= ((ylen / 20) * 11.00))
                                    {
                                        //observe

                                        obs = true;
                                        sim = false;

                                        Point newpointe1 = new Point(Convert.ToInt16(((xlen / 10) * 3.76)), Convert.ToInt16((ylen / 20) * 10.52));
                                        g.DrawString("Observe", myFontsmall, newbrushA, newpointe1);

                                        Point newpointe3 = new Point(Convert.ToInt16(((xlen / 10) * 4.86)), Convert.ToInt16((ylen / 20) * 10.52));
                                        g.DrawString("Simulate", myFontsmall, newbrushC, newpointe3);
                                    }
                                    else if (mouseposX >= (xlen / 10) * 4.76 && mouseposX <= ((xlen / 10) * 5.56) && mouseposY >= ((ylen / 20) * 10.52) && mouseposY <= ((ylen / 20) * 11.00))
                                    {
                                        //Simulate

                                        obs = false;
                                        sim = true;

                                        Point newpointe1 = new Point(Convert.ToInt16(((xlen / 10) * 3.76)), Convert.ToInt16((ylen / 20) * 10.52));
                                        g.DrawString("Observe", myFontsmall, newbrushC, newpointe1);

                                        Point newpointe3 = new Point(Convert.ToInt16(((xlen / 10) * 4.86)), Convert.ToInt16((ylen / 20) * 10.52));
                                        g.DrawString("Simulate", myFontsmall, newbrushA, newpointe3);
                                    }
                                    else if (mouseposX >= (xlen / 10) * 6.70 && mouseposX <= ((xlen / 10) * 7.76) && mouseposY >= ((ylen / 20) * 13.52) && mouseposY <= ((ylen / 20) * 14.52))
                                    {
                                        if (obs == true || sim == true && browse == true)
                                        {
                                            Timera.Stop();
                                            Observe observer = new Observe();
                                            observer.Text = savpath;
                                            observer.Show();
                                            this.Hide();
                                        }
                                    }
                                }
                                else if(selectedobj == 2) //generate screen
                                {
                                    Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
                                    Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);
                                    Brush newbrushA = new SolidBrush(Color.DarkRed);

                                    //browse for file directory
                                    if (mouseposX >= (xlen / 10) * 6.36 && mouseposX <= ((xlen / 10) * 7.36) && mouseposY >= ((ylen / 20) * 7.52) && mouseposY <= ((ylen / 20) * 8.32) && browse == false)
                                    {
                                        filebrowse("WriteNew");
                                        if (outcancel == false)
                                        {
                                            MenuImg.Invalidate();
                                            Point newpointa1 = new Point(Convert.ToInt16(((xlen / 10) * 4.56)), Convert.ToInt16((ylen / 20) * 7.32));
                                            g.DrawString(outname, myFontsmall, newbrushD, newpointa1);
                                            outcancel = true;

                                            //filenames will stack over each other upon reentry
                                        }
                                        else
                                        {
                                            
                                        }
                                    }
                                    else if(mouseposX >= (xlen / 10) * 5.56 && mouseposX <= ((xlen / 10) * 6.56) && mouseposY >= ((ylen / 20) * 8.52) && mouseposY <= ((ylen / 20) * 9.52))
                                    {
                                        existing = false;
                                        random = true;

                                        Point newpointb1 = new Point(Convert.ToInt16(((xlen / 10) * 5.56)), Convert.ToInt16((ylen / 20) * 8.52));
                                        g.DrawString("Random", myFontsmall, newbrushA, newpointb1);

                                        Point newpointb3 = new Point(Convert.ToInt16(((xlen / 10) * 6.90)), Convert.ToInt16((ylen / 20) * 8.52));
                                        g.DrawString("Existing", myFontsmall, newbrushC, newpointb3);
                                    }
                                    else if (mouseposX >= (xlen / 10) * 6.90 && mouseposX <= ((xlen / 10) * 7.80) && mouseposY >= ((ylen / 20) * 8.52) && mouseposY <= ((ylen / 20) * 9.52))
                                    {
                                        existing = true;
                                        random = false;

                                        Point newpointb1 = new Point(Convert.ToInt16(((xlen / 10) * 5.56)), Convert.ToInt16((ylen / 20) * 8.52));
                                        g.DrawString("Random", myFontsmall, newbrushC, newpointb1);

                                        Point newpointb3 = new Point(Convert.ToInt16(((xlen / 10) * 6.90)), Convert.ToInt16((ylen / 20) * 8.52));
                                        g.DrawString("Existing", myFontsmall, newbrushA, newpointb3);
                                    }
                                    else if (mouseposX >= (xlen / 10) * 6.70 && mouseposX <= ((xlen / 10) * 7.76) && mouseposY >= ((ylen / 20) * 13.52) && mouseposY <= ((ylen / 20) * 14.52))
                                    {
                                        //provinces
                                        Point newpointb3 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 9.52));
                                        if (random == true || existing == true && browse == true)
                                        {
                                            rndcycle = 1;
                                            if (random == false)
                                            {
                                                g.DrawString("Pulling Province Names:", myFontsmall, newbrushD, newpointb3);
                                            }
                                            else
                                            {
                                                g.DrawString("Generating Province Names:", myFontsmall, newbrushD, newpointb3);
                                            }
                                            Point newpointb4 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                            g.DrawString("Generating Religion Names:", myFontsmall, newbrushD, newpointb4);
                                            Point newpointb5 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 11.52));
                                            g.DrawString("Pulling First Names:", myFontsmall, newbrushD, newpointb5);
                                            Point newpointb6 = new Point(Convert.ToInt16(((xlen / 10) * 3.06)), Convert.ToInt16((ylen / 20) * 12.52));
                                            g.DrawString("Pulling Dynasty Names:", myFontsmall, newbrushD, newpointb6);
                                            Array.Clear(allgen, 0, 501);
                                        }
                                    }
                                    else
                                    {

                                    }
                                        
                                }
                                else if (selectedobj == 3) //options screen
                                {

                                }
                                else //default screen
                                {

                                selectedobj = 0;
                                Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                Point newpointa = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                g.DrawString("Start", myFont, newbrusha, newpointa);
                                Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                Point newpointb = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                g.DrawString("Generate", myFont, newbrushb, newpointb);
                                Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                                Point newpointc = new Point(Convert.ToInt16(((xlen / 10) * 1.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                g.DrawString("Options", myFont, newbrushc, newpointc);
                                objanimatestage[1] = 0;
                                objanimatestage[2] = 0;
                                objanimatestage[3] = 0;
                                updatebigbox(selectedobj);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
