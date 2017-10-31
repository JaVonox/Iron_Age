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
            MenuImg.Image = Bg;

            for (int x = 0; x <= xlen - 1; x++)
            {
                for (int y = 0; y <= ylen - 1; y++)
                {
                   bitmapc[x, y] = Color.FromArgb(28, 107, 160);
                }
            }

            for (int x = (xlen / 5); x <= ((xlen / 5) * 4) - 1; x++) //title box
            {
                for (int y = (ylen / 10); y <= ((ylen / 10) * 3) - 1; y++)
                {
                    //bitmapc[x, y] = Color.HotPink;
                    int temprand = Rand.Next(2, 10);

                    if (x <= (xlen / 5) + 9 || x >= ((xlen / 5) * 4) - 9)
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
                    else if (y <= (ylen / 10) + 9 || y >= ((ylen / 10) * 3) - 9)
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
                    else
                    {
                        if (temprand == 2)
                        {
                            bitmapc[x, y] = Color.FromArgb(127, 127, 127);
                        }
                        else if (temprand == 3)
                        {
                            bitmapc[x, y] = Color.FromArgb(116, 116, 116);
                        }
                        else if (temprand == 4)
                        {
                            bitmapc[x, y] = Color.FromArgb(115, 115, 115);
                        }
                        else if (temprand == 5)
                        {
                            bitmapc[x, y] = Color.FromArgb(104, 104, 104);
                        }
                        else
                        {
                            bitmapc[x, y] = Color.FromArgb(143, 143, 143);
                        }
                    }
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
                Brush newbrush = new SolidBrush(Color.AntiqueWhite);
                Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 1.93)), Convert.ToInt16((ylen / 10) * 1.69));
                g.DrawString("Iron Age", myFontB, newbrush, newpoint);
            }


        }

        int[] objanimatestage = new int[5];

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mouseposX != 0 && mouseposY != 0)
            {
                if (bitmapc[mouseposX, mouseposY] == Color.FromArgb(28, 107, 160))
                {
                    if(objanimatestage[1] != 1)
                    {
                        using (var g = Graphics.FromImage(MenuImg.Image))
                        {
                            //lags and doesnt work
                            Brush newbrush = new SolidBrush(Color.Red);
                            Point newpoint = new Point(Convert.ToInt16(((xlen / 5) * 2.06)), Convert.ToInt16((ylen / 20) * 7.52));
                            g.DrawString("pGay", myFont, newbrush, newpoint);
                            objanimatestage[1] = 1;
                        }
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
