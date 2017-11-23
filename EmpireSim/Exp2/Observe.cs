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
    public partial class Observe : Form
    {
        static int xlen = 1400;
        static int ylen = 900;
        Random rand = new Random();
        string[,] map = new string[xlen,ylen];
        char singlechar;

        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
        string[,] provinces = new string[10000,16];

        //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%
        string[,] kingdoms = new string[10000, 10];
        string[,] kingdomowner = new string[10000, 10000];

        string path;
        Bitmap Map = new Bitmap(Exp2.Properties.Resources.testimage,xlen,ylen);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();
        Font myFont;
        Font myFontB;
        Font myFontsmall;
        Font myFontTitle;
        Font myFontDetail;

        Random Rand = new Random();
        int mouseposX;
        int mouseposY;

        public Observe()
        {
            InitializeComponent();
        }

        private void Observe_Load(object sender, EventArgs e)
        {
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
            myFontTitle = new Font(fonts.Families[0], 16.8F, FontStyle.Underline);
            myFontDetail = new Font(fonts.Families[0], 12.0F);

            this.Size = new System.Drawing.Size(xlen,ylen);
            //ImagePic.Width = xlen;
            //ImagePic.Height = ylen;
            this.Icon = Exp2.Properties.Resources.WorldGen;
            path = this.Text;
            pullmapdata();
            ImagePic.Image = Map;
            Dobits();
            this.Text = "Observing ";
        }

        private void pullmapdata()
        {
            System.IO.StreamReader Reada = new System.IO.StreamReader(path + "//world.dat");

                int y = 0;

                for (int x = 0; x <= xlen - 1; x++)
                {
                    singlechar = (char)Reada.Read();
                    if(singlechar.ToString() == "*")
                    {
                        map[x, y] = "0";
                    }
                    else
                    {
                        while(true)
                        {

                            if(singlechar.ToString() == "@")
                            {
                                singlechar = (char)Reada.Read();
                            }
                            else if (singlechar == 65535||singlechar.ToString() == "~" || singlechar.ToString() == "\r" || singlechar.ToString() == "\n" || singlechar.ToString() == "." || singlechar.ToString() == ",")
                            {
                                if (singlechar.ToString() == "\r" || singlechar.ToString() == "\n" || singlechar.ToString() == "~" && x != 0)
                                {
                                    if(singlechar.ToString() == "\n")
                                    {
                                        y += 1;
                                        x = -1;
                                    }
                                    else
                                    {
                                        x -= 1;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                map[x, y] += singlechar.ToString();
                                singlechar = (char)Reada.Read();
                            }

                        }
                    }
            }
            Reada.Close();

            System.IO.StreamReader Readb = new System.IO.StreamReader(path + "//Provs.dat");
            int indcount = -1;

            //for(int i = -1; i <= 10000; i++)
            //{
            //    indcount = -1;

            int i = -1;
            bool aft = false;

                while (true)
                {
                    singlechar = (char)Readb.Read();

                    if (singlechar.ToString() == "$")
                    {
                        aft = false;
                        i += 1;
                        indcount = 0;
                    }
                    else if (singlechar.ToString() == "%")
                    {
                        indcount += 1;
                    }
                    else if(singlechar.ToString() == "~")
                    {
                        aft = true;
                    }
                    else if(aft == true)
                    {
                        if (singlechar.ToString() == "\r" || singlechar.ToString() == "\n")
                        {

                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        provinces[i, indcount] += singlechar.ToString();
                    }
                }

            //}

            Readb.Close();

        }

        private void Dobits()
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

            for (int y = 1; y <= ylen - 41; y++)
            {
                int l = y * Math.Abs(bmpData.Stride);
                int rowStart = y * bmpData.Stride;

                for (int x = 1; x <= xlen - 18; x++)
                {
                        int dx = l + x * 4;
                        
                        if(x >= (xlen / 80) * 65)
                        {
                            if(y >= (ylen / 40) * 38 || y <= (ylen / 40) * 1)
                            {
                                if (x >= (xlen / 80) * 81 || x <= (xlen / 80) * 66)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 215; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                }
                                else
                                {
                                    int temprand = rand.Next(2, 10);
                                    if (temprand == 2)
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 73; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 64; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 32; //blue
                                    }
                                    else if (temprand == 3)
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 192; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 151; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 98; //blue
                                    }
                                    else if (temprand == 4)
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 158; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 133; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 72; //blue
                                    }
                                    else if (temprand == 5)
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 108; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 85; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 58; //blue
                                    }
                                    else
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 115; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 94; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 57; //blue
                                    }
                                }
                            }
                            else if (x >= (xlen / 80) * 66)
                            {
                                int temprand = rand.Next(2, 10);
                                if (temprand == 2)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 246; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 221; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 155; //blue
                                }
                                else if (temprand == 3)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 253; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 235; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 185; //blue
                                }
                                else if (temprand == 4)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 247; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 225; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 175; //blue
                                }
                                else if (temprand == 5)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 231; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 173; //blue
                                }
                                else
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 238; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 223; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 166; //blue
                                }
                            }
                            else
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 28; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 107; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 160; //blue
                            }

                        }
                        else if (map[x, y] == "0" || map[x, y] == null)
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride)+ (x * 4) + 2] = 28; //red
                            rgbValues[(y * bmpData.Stride)+(x * 4) + 1] = 107; //green
                            rgbValues[(y * bmpData.Stride)+(x * 4)] = 160; //blue
                        }
                        else
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 78; //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 176; //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = 134; //blue
                        }
                   
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);


        }

        private void PersonaliseBits(string inputarg)
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

            for (int y = 1; y <= ylen - 41; y++)
            {
                int l = y * Math.Abs(bmpData.Stride);
                int rowStart = y * bmpData.Stride;

                for (int x = 1; x <= xlen - 18; x++)
                {
                    int dx = l + x * 4;

                    if (x >= (xlen / 80) * 65)
                    {
                        if (y >= (ylen / 40) * 38 || y <= (ylen / 40) * 1)
                        {
                            if (x >= (xlen / 80) * 81 || x <= (xlen / 80) * 66)
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 215; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                            }
                            else
                            {
                                int temprand = rand.Next(2, 10);
                                if (temprand == 2)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 73; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 64; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 32; //blue
                                }
                                else if (temprand == 3)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 192; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 151; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 98; //blue
                                }
                                else if (temprand == 4)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 158; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 133; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 72; //blue
                                }
                                else if (temprand == 5)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 108; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 85; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 58; //blue
                                }
                                else
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 115; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 94; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 57; //blue
                                }
                            }
                        }
                        else if (x >= (xlen / 80) * 66)
                        {

                            int temprand = rand.Next(2, 10);
                            if (temprand == 2)
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 246; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 221; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 155; //blue
                            }
                            else if (temprand == 3)
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 253; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 235; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 185; //blue
                            }
                            else if (temprand == 4)
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 247; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 225; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 175; //blue
                            }
                            else if (temprand == 5)
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 231; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 173; //blue
                            }
                            else
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 238; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 223; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 166; //blue
                            }
                        }
                        else
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 28; //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 107; //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = 160; //blue
                        }

                    }
                    else if (map[x, y] == "0" || map[x, y] == null)
                    {
                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 28; //red
                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 107; //green
                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 160; //blue
                    }
                    else
                    {
                        if (map[x, y] == inputarg)
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2),13]); //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2),14]); //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2), 15]); //blue
                        }
                        else
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 78; //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 176; //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = 134; //blue
                        }
                    }

                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);


        }

        private void Observe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ImagePic_Click(object sender, EventArgs e)
        {
            ImagePic.Invalidate();
            Point p = this.PointToClient(Cursor.Position);
            mouseposX = Math.Max(0, p.X);
            mouseposY = Math.Max(0, p.Y);
            Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
            Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);

            if(map[mouseposX,mouseposY] != "0")
            {
                using (var g = Graphics.FromImage(ImagePic.Image))
                {

                    if (provinces[Convert.ToInt32(map[mouseposX, mouseposY]), 3] != provinces[Convert.ToInt32(map[mouseposX, mouseposY]), 1])
                    {
                        //temp area for when a province is owned by another kingdom.
                        PersonaliseBits(map[mouseposX, mouseposY]);
                    }
                    else
                    {
                        PersonaliseBits(map[mouseposX, mouseposY]);

                        Point newpointa = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 4));
                        g.DrawString(provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 1], myFontTitle, newbrushD, newpointa);

                        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~

                        Point newpointb = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 6));
                        g.DrawString("Religion : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 2], myFontDetail, newbrushD, newpointb);

                        Point newpointc = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 7));
                        g.DrawString("Bronze : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 4] + "%", myFontDetail, newbrushD, newpointc);

                        Point newpointd = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 8));
                        g.DrawString("Iron : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 5] + "%", myFontDetail, newbrushD, newpointd);

                        Point newpointe = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 9));
                        g.DrawString("Steel : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 6] + "%", myFontDetail, newbrushD, newpointe);

                        Point newpointf = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 10));
                        g.DrawString("Gunpowder : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 7] + "%", myFontDetail, newbrushD, newpointf);

                        Point newpointg = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 11));
                        g.DrawString("Oil : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 8] + "%", myFontDetail, newbrushD, newpointg);

                        Point newpointh = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 12));
                        g.DrawString("Theology : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 9] + "%", myFontDetail, newbrushD, newpointh);

                        Point newpointi = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 13));
                        g.DrawString("Science : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 10] + "%", myFontDetail, newbrushD, newpointi);

                        Point newpointj = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 14));
                        g.DrawString("Happiness : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 11], myFontDetail, newbrushD, newpointj);
                    }
                }
            }
            else
            {
                Dobits();
            }
        }
    }
}
