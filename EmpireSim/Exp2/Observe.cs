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

        string[,] map = new string[xlen,ylen];
        char singlechar;
        string[] provinces = new string[10000];
        string path;
        Bitmap Map = new Bitmap(Exp2.Properties.Resources.testimage,xlen,ylen);
        public Observe()
        {
            InitializeComponent();
        }

        private void Observe_Load(object sender, EventArgs e)
        {
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

            //got it working once, tried to fix it and it stopped worked. inf loop.
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
        }

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

            for (int y = 1; y <= ylen - 41; y++)
            {
                int l = y * Math.Abs(bmpData.Stride);
                int rowStart = y * bmpData.Stride;

                for (int x = 1; x <= xlen - 18; x++)
                {
                        int dx = l + x * 4;

                        if (map[x, y] == "0" || map[x, y] == null)
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

        private void Observe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
