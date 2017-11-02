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
        Color[,] bitmapc = new Color[xlen, ylen];
        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();
        Font myFont;
        Font myFontB;
        Random Rand = new Random();
        int mouseposX;
        int mouseposY;
        //pfc.AddFontFile(Path.Combine(Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("bin")) + "Font.ttf"));
        //private Font Arcade;

        public MainMenu()
        {
            InitializeComponent();
            MenuImg.Image = Bg;
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

                    //bitmapc[x, y] = Color.HotPink;
                    //int temprand = Rand.Next(2, 10);

                    //if (x <= (xlen / 5) + 9 || x >= ((xlen / 5) * 4) - 9)
                    //{
                    //    if (temprand == 2)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                    //    }
                    //    else if (temprand == 3)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                    //    }
                    //    else if (temprand == 4)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                    //    }
                    //    else if (temprand == 5)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                    //    }
                    //    else
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                    //    }

                    //}
                    //else if (y <= (ylen / 10) + 9 || y >= ((ylen / 10) * 3) - 9)
                    //{
                    //    if (temprand == 2)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(73, 64, 32);
                    //    }
                    //    else if (temprand == 3)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(192, 151, 98);
                    //    }
                    //    else if (temprand == 4)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(158, 133, 72);
                    //    }
                    //    else if (temprand == 5)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(108, 85, 58);
                    //    }
                    //    else
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(115, 94, 57);
                    //    }
                    //}
                    //else
                    //{
                    //    if (temprand == 2)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(127, 127, 127);
                    //    }
                    //    else if (temprand == 3)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(116, 116, 116);
                    //    }
                    //    else if (temprand == 4)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(115, 115, 115);
                    //    }
                    //    else if (temprand == 5)
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(104, 104, 104);
                    //    }
                    //    else
                    //    {
                    //        bitmapc[x, y] = Color.FromArgb(143, 143, 143);
                    //    }
                    //}
                }
            }

            for (int x = (xlen / 5) * 2; x <= ((xlen / 5) * 3); x++) //play box
            {
                for (int y = (ylen / 20) * 7; y <= ((ylen / 20) * 9); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 5) * 2 + 5 || x >= ((xlen / 5) * 3) - 6)
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

            for (int x = (xlen / 5) * 2; x <= ((xlen / 5) * 3); x++) // Create World box
            {
                for (int y = (ylen / 20) * 10; y <= ((ylen / 20) * 12); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 5) * 2 + 5 || x >= ((xlen / 5) * 3) - 6)
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

            for (int x = (xlen / 5) * 2; x <= ((xlen / 5) * 3); x++) // Options Box
            {
                for (int y = (ylen / 20) * 13; y <= ((ylen / 20) * 15); y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 5) * 2 + 5 || x >= ((xlen / 5) * 3) - 6)
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
                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 13.52));
                g.DrawString("Options", myFont, newbrush, newpoint);
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //Create World
            {
                Brush newbrush = new SolidBrush(Color.DarkSlateGray);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 10.52));
                g.DrawString("Generate", myFont, newbrush, newpoint);
            }

            using (var g = Graphics.FromImage(MenuImg.Image)) //Play World
            {
                Brush newbrush = new SolidBrush(Color.DarkSlateGray);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                g.DrawString("Play", myFont, newbrush, newpoint);
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
            using (var g = Graphics.FromImage(MenuImg.Image))
            {
                MenuImg.Invalidate();
                Point p = this.PointToClient(Cursor.Position);
                mouseposX = Math.Max(0, p.X);
                mouseposY = Math.Max(0, p.Y);

                if (mouseposX != 0 && mouseposY != 0)
                {
                    if (mouseposX >= (xlen / 5) * 2 && mouseposX <= ((xlen / 5) * 3) && mouseposY >= ((ylen / 20) * 7) && mouseposY <= ((ylen / 20) * 9)) //play box
                    {
                                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                animate(1, objanimatestage[1], "Red", Color.DarkSlateGray, newpoint,"Play");

                                Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                Point newpointb = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                g.DrawString("Generate", myFont, newbrushb, newpointb);
                                Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                                Point newpointc = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                g.DrawString("Options", myFont, newbrushc, newpointc);
                                objanimatestage[2] = 0;
                                objanimatestage[3] = 0;

                    }
                    else
                    {
                        if (mouseposX >= (xlen / 5) * 2 && mouseposX <= ((xlen / 5) * 3) && mouseposY >= ((ylen / 20) * 10) && mouseposY <= ((ylen / 20) * 12))
                        {
                            Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 10.52));
                            animate(2, objanimatestage[2], "Red", Color.DarkSlateGray, newpoint,"Generate");

                            Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                            Point newpointa = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                            g.DrawString("Play", myFont, newbrusha, newpointa);
                            Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                            Point newpointc = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 13.52));
                            g.DrawString("Options", myFont, newbrushc, newpointc);
                            objanimatestage[1] = 0;
                            objanimatestage[3] = 0;

                        }
                        else
                        {
                            if (mouseposX >= (xlen / 5) * 2 && mouseposX <= ((xlen / 5) * 3) && mouseposY >= ((ylen / 20) * 13) && mouseposY <= ((ylen / 20) * 15))
                            {
                                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 13.52));
                                animate(3, objanimatestage[3], "Red", Color.DarkSlateGray, newpoint, "Options");

                                Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                Point newpointa = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                g.DrawString("Play", myFont, newbrusha, newpointa);
                                Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                Point newpointb = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                g.DrawString("Generate", myFont, newbrushb, newpointb);
                                objanimatestage[1] = 0;
                                objanimatestage[2] = 0;

                            }
                            else
                            {
                                Brush newbrusha = new SolidBrush(Color.DarkSlateGray);
                                Point newpointa = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                                g.DrawString("Play", myFont, newbrusha, newpointa);
                                Brush newbrushb = new SolidBrush(Color.DarkSlateGray);
                                Point newpointb = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 10.52));
                                g.DrawString("Generate", myFont, newbrushb, newpointb);
                                Brush newbrushc = new SolidBrush(Color.DarkSlateGray);
                                Point newpointc = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 13.52));
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

        private void Datainputtmr_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);
            mouseposX = Math.Max(0,p.X);
            mouseposY = Math.Max(0,p.Y);
            //label1.Text = x + "," + y;
            //abel2.Text = xstore + "," + ystore; 
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {

        }


    }
}
