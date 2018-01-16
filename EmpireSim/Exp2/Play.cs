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
    public partial class Play : Form
    {
        static int xlen = 1400;
        static int ylen = 900;
        Random rand = new Random();
        string[,] map = new string[xlen, ylen];
        char singlechar;
        byte enb = 1;

        int year;
        int month;
        int day;

        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
        string[,] provinces = new string[10000, 16];

        //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
        string[,] kingdoms = new string[10000, 11];
        string[,] kingdomowner = new string[10000, 10000];
        string[] kingidname = new string[10000];

        //$Name%Red%Blue%Green%~
        string[,] Religions = new string[11, 4];
        string[] ReligionId = new string[11];

        //$WARID%WARTYPE%WARNAME%AGGRESSORID%AGRESSORSCORE%DEFENDERID%DEFENDERSCORE%~
        string[,] war = new string[10000, 7];

        //Country id, enemy id, war id
        string[,] Warsgroup = new string[10000, 10000];

        //$COUNTRYID%TRUCELENGTH%~
        string[] truce = new string[10000];


        string path;
        Bitmap Map = new Bitmap(Exp2.Properties.Resources.testimage, xlen, ylen);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();
        Font myFont;
        Font myFontB;
        Font myFontsmall;
        Font myFontTitle;
        Font myFontLarge;
        Font myFontDetail;
        Events ev = new Events();


        Random Rand = new Random();
        int mouseposX;
        int mouseposY;

        public Play()
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
            myFontLarge = new Font(fonts.Families[0], 16.8F);
            myFontDetail = new Font(fonts.Families[0], 12.0F);

            this.Size = new System.Drawing.Size(xlen, ylen);
            //ImagePic.Width = xlen;
            //ImagePic.HeighAt = ylen;
            this.Icon = Exp2.Properties.Resources.WorldGen;
            path = this.Text;
            pullmapdata();
            Back.Image = Map;
            Dobits();
            defaultbuttons();
            this.Text = "Simulating";
            madpadj();
            //Console.WriteLine("M");
        }

        string[] NamesList = new string[501];
        string[] SurnameList = new string[501];

        string[,] adjs = new string[10000,4]; //UP LEFT DOWN RIGHT

        private void madpadj()
        {
            for (int x = 1; x <= xlen - 1; x++)
            {
                for (int y = 1; y <= ylen - 1; y++)
                {
                    if (Convert.ToInt16(map[x, y]) != 0)
                    {
                        if (map[x + 1, y] != map[x, y])
                        {
                            if (Convert.ToInt16(map[x + 1, y]) != 0)
                            {
                                adjs[Convert.ToInt16(provinces[Convert.ToInt16(map[x, y]) - 2, 0]), 3] = map[x + 1, y];
                            }
                        }
                        else if (map[x - 1, y] != map[x, y])
                        {
                            if (Convert.ToInt16(map[x - 1, y]) != 0)
                            {
                                adjs[Convert.ToInt16(provinces[Convert.ToInt16(map[x, y]) - 2, 0]), 1] = map[x - 1, y];
                            }
                        }
                        else if (map[x, y + 1] != map[x, y])
                        {
                            if (Convert.ToInt16(map[x, y + 1]) != 0)
                            {
                                adjs[Convert.ToInt16(provinces[Convert.ToInt16(map[x, y]) - 2, 0]), 0] = map[x, y + 1];
                            }
                        }
                        else if (map[x, y - 1] != map[x, y])
                        {
                            if (Convert.ToInt16(map[x, y - 1]) != 0)
                            {
                                adjs[Convert.ToInt16(provinces[Convert.ToInt16(map[x, y]) - 2, 0]), 2] = map[x, y - 1];
                            }
                        }
                    }
                }
            }
        }

        private void pullmapdata()
        {
            System.IO.StreamReader Readen = new System.IO.StreamReader(path + "//Info//Names.dat");
            for (int p = 0; p <= 500; p++)
            {
                NamesList[p] = Readen.ReadLine();
            }
            Readen.Close();

            System.IO.StreamReader Readeno = new System.IO.StreamReader(path + "//Info//Dynasty.dat");
            for (int p = 0; p <= 500; p++)
            {
                SurnameList[p] = Readeno.ReadLine();
            }
            Readeno.Close();

            System.IO.StreamReader Reada = new System.IO.StreamReader(path + "//world.dat");

            int y = 0;
            int cntvl = 0;
            bool wloop = false;
            string wtmp = null;

            for (int x = 0; x <= xlen - 1; x++)
            {
                for (int my = 0; my <= ylen - 1; my++)
                {
                    map[x, my] = "0";
                }
            }

            for (int x = 0; x <= xlen - 1; x++)
            {
                singlechar = (char)Reada.Read();

                if (wloop == true)
                {
                    while (true)
                    {
                        if (singlechar.ToString() == "m")
                        {
                            cntvl = Convert.ToInt32(wtmp);
                            wloop = false;

                            for (int n = 0; n <= cntvl - 1; n++) //maybe wrong
                            {
                                if (x + n >= xlen - 1)
                                {
                                    map[x + n, y] = "0";
                                    break;
                                }
                                else
                                {
                                    map[x + n, y] = "0";
                                    //singlechar = (char)Reada.Read(); why is this here
                                }
                            }

                            x += cntvl - 1;
                            wtmp = null;
                            cntvl = 0;
                            break;
                        }
                        else if (singlechar.ToString() == "f")
                        {
                            //for (int l = x; l <= xlen - 1; l++)
                            //{
                            //    map[l, y] = "0";
                            //}

                            Reada.ReadLine();
                            wloop = false;
                            y += 1;
                            x = -1;
                            break;
                        }
                        else
                        {
                            wtmp += singlechar.ToString();
                        }
                        singlechar = (char)Reada.Read();
                    }
                }
                else if (singlechar.ToString() == "w")
                {
                    wloop = true;
                    //map[x, y] = "0";
                }
                else
                {
                    while (true)
                    {

                        if (singlechar.ToString() == "@")
                        {
                            singlechar = (char)Reada.Read();
                        }
                        else if (singlechar == 65535 || singlechar.ToString() == "~" || singlechar.ToString() == "\r" || singlechar.ToString() == "\n" || singlechar.ToString() == "." || singlechar.ToString() == ",")
                        {
                            if (singlechar.ToString() == "\r" || singlechar.ToString() == "\n" || singlechar.ToString() == "~" && x != 0)
                            {
                                if (singlechar.ToString() == "\n")
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
                else if (singlechar.ToString() == "~")
                {
                    aft = true;
                }
                else if (aft == true)
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

            System.IO.StreamReader Readc = new System.IO.StreamReader(path + "//Kingdoms.dat");
            int indcountb = -1;

            //for(int i = -1; i <= 10000; i++)
            //{
            //    indcount = -1;

            int ib = -1;
            bool aftb = false;
            bool ownedmode = false;

            int indcountc = -1;

            while (true)
            {
                singlechar = (char)Readc.Read();

                if (ownedmode == false)
                {
                    indcountc = -1;

                    if (singlechar.ToString() == "$")
                    {
                        aftb = false;
                        ib += 1;
                        indcountb = 0;
                    }
                    else if (singlechar.ToString() == "%")
                    {
                        indcountb += 1;
                    }
                    else if (singlechar.ToString() == "~")
                    {
                        aftb = true;
                    }
                    else if (singlechar.ToString() == "(" && indcountb == 4)
                    {
                        ownedmode = true;
                        indcountb -= 1;
                    }
                    else if (aftb == true)
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
                        kingdoms[ib, indcountb] += singlechar.ToString();

                        //if (indcountb == 1)
                        //{
                        //    kingidname[ib] += singlechar.ToString();
                        //}
                    }
                }
                else
                {
                    if (singlechar.ToString() == "%")
                    {
                        indcountc += 1;
                    }
                    else if (singlechar.ToString() == ")")
                    {
                        ownedmode = false;
                    }
                    else if (singlechar.ToString() == "," || singlechar.ToString() == " " || singlechar.ToString() == null)
                    {

                    }
                    else
                    {
                        kingdomowner[ib, indcountc] += singlechar.ToString();
                    }

                }
            }

            //}

            Readc.Close();

            System.IO.StreamReader Readd = new System.IO.StreamReader(path + "//GameInfo.sav");
            Readd.ReadLine();
            year = Convert.ToInt32(Readd.ReadLine());
            month = Convert.ToInt16(Readd.ReadLine());
            day = Convert.ToInt16(Readd.ReadLine());
            existingreligions = Convert.ToInt16(Readd.ReadLine());
            Readd.Close();

            //$WARID%WARTYPE%WARNAME%AGGRESSORID%AGRESSORSCORE%DEFENDERID%DEFENDERSCORE%STARTDATE%~
            //string[,] war = new string[10000, 7];

            //Country id, enemy id, war id
            //string[,] Warsgroup = new string[10000, 10000];

            System.IO.StreamReader Reade = new System.IO.StreamReader(path + "//History.dat");

            ib = -1;
            aftb = false;
            ownedmode = false;
            indcountb = -1;
            indcountc = -1;

            while (true)
            {
                singlechar = (char)Reade.Read();

                if (ownedmode == false)
                {
                    indcountc = -1;

                    if (singlechar.ToString() == "$")
                    {
                        aftb = false;
                        ib += 1;
                        indcountb = 0;
                    }
                    else if (singlechar.ToString() == "%")
                    {
                        indcountb += 1;
                    }
                    else if (singlechar.ToString() == "~")
                    {
                        if (war[ib, 0] == null)
                        {

                        }
                        else
                        {
                            Warsgroup[Convert.ToInt32(war[ib, 3]), Convert.ToInt32(war[ib, 5])] = war[ib, 0].ToString();
                            Warsgroup[Convert.ToInt32(war[ib, 5]), Convert.ToInt32(war[ib, 3])] = war[ib, 0].ToString();
                        }
                        aftb = true;
                    }
                    else if (singlechar.ToString() == "(" && indcountb == 4)
                    {
                        ownedmode = true;
                        indcountb -= 1;
                    }
                    else if (aftb == true)
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
                        war[ib, indcountb] += singlechar.ToString();

                        //if (indcountb == 1)
                        //{
                        //    kingidname[ib] += singlechar.ToString();
                        //}
                    }
                }
                else
                {
                    if (singlechar.ToString() == "%")
                    {
                        indcountc += 1;
                    }
                    else if (singlechar.ToString() == ")")
                    {
                        ownedmode = false;
                    }
                    else if (singlechar.ToString() == "," || singlechar.ToString() == " " || singlechar.ToString() == null)
                    {

                    }
                    else
                    {
                        war[ib, indcountc] += singlechar.ToString();
                    }

                }
            }

            Reade.Close();

            System.IO.StreamReader Readf = new System.IO.StreamReader(path + "//Peace.dat");

            //$COUNTRYID%TRUCELENGTH%~ all truces end on new years?

            ib = -1;
            aftb = false;
            ownedmode = false;
            indcountb = -1;
            indcountc = -1;

            while (true)
            {
                singlechar = (char)Readf.Read();

                if (ownedmode == false)
                {
                    indcountc = -1;

                    if (singlechar.ToString() == "$")
                    {
                        aftb = false;
                        ib += 1;
                        indcountb = 0;
                    }
                    else if (singlechar.ToString() == "%")
                    {
                        indcountb += 1;
                    }
                    else if (singlechar.ToString() == "~")
                    {
                        aftb = true;
                    }
                    else if (singlechar.ToString() == "(" && indcountb == 4)
                    {
                        ownedmode = true;
                        indcountb -= 1;
                    }
                    else if (aftb == true)
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
                        truce[ib] += singlechar.ToString();

                        //if (indcountb == 1)
                        //{
                        //    kingidname[ib] += singlechar.ToString();
                        //}
                    }
                }
                else
                {
                    if (singlechar.ToString() == "%")
                    {
                        indcountc += 1;
                    }
                    else if (singlechar.ToString() == ")")
                    {
                        ownedmode = false;
                    }
                    else if (singlechar.ToString() == "," || singlechar.ToString() == " " || singlechar.ToString() == null)
                    {

                    }
                    else
                    {
                        truce[ib] += singlechar.ToString();
                    }

                }
            }

            Readf.Close();

            System.IO.StreamReader Readg = new System.IO.StreamReader(path + "//Info//ReligionNames.dat");

            ib = -1;
            aftb = false;
            ownedmode = false;
            indcountb = -1;
            indcountc = -1;

            while (true)
            {
                singlechar = (char)Readg.Read();

                if (ownedmode == false)
                {
                    indcountc = -1;

                    if (singlechar.ToString() == "$")
                    {
                        aftb = false;
                        ib += 1;
                        indcountb = 0;
                    }
                    else if (singlechar.ToString() == "%")
                    {
                        indcountb += 1;
                    }
                    else if (singlechar.ToString() == "~")
                    {
                        aftb = true;
                    }
                    else if (singlechar.ToString() == "(" && indcountb == 4)
                    {
                        ownedmode = true;
                        indcountb -= 1;
                    }
                    else if (aftb == true)
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
                        Religions[ib, indcountb] += singlechar.ToString();
                        ReligionId[ib] = Religions[ib, 0];
                        //truce[ib] += singlechar.ToString();

                        //if (indcountb == 1)
                        //{
                        //    kingidname[ib] += singlechar.ToString();
                        //}
                    }
                }
                else
                {
                    if (singlechar.ToString() == "%")
                    {
                        indcountc += 1;
                    }
                    else if (singlechar.ToString() == ")")
                    {
                        ownedmode = false;
                    }
                    else if (singlechar.ToString() == "," || singlechar.ToString() == " " || singlechar.ToString() == null)
                    {

                    }
                    else
                    {
                        Religions[ib, indcountb] += singlechar.ToString();
                        ReligionId[ib] = Religions[ib, 0];
                    }

                }
            }

            Readg.Close();

            //Country id, enemy id, war id
            //string[,] Warsgroup = new string[10000, 10000];

            //$COUNTRYID%TRUCELENGTH%~
            //string[] truce = new string[10000];

            for (int n = 2; n <= 9999; n++)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000,16];
                //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~

                if (provinces[n - 2, 3] != provinces[n - 2, 1])
                {
                    //kingidname[n -2] = provinces[n -2, 3];
                    kingidname[n - 2] = null;
                }
                else
                {
                    kingidname[n - 2] = provinces[n - 2, 1];
                }
            }

        }

        private void Save()
        {

            tock.Stop();
            speed = 0;
            enb = 1;
            eventnews("Save", null, null);
            Dobits();
            defaultbuttons();


            System.IO.File.WriteAllBytes(path + "//Provs.dat", new byte[0]);
            //System.IO.StreamWriter Writea = new System.IO.StreamWriter(path + "//world.dat");


            //System.IO.File.Create(pathing + "//World.dat"); //Stores World tiles
            System.IO.StreamWriter Writerb = new System.IO.StreamWriter(path + "//Provs.dat");


            int i = 0;
            while (provinces[i, 0] != null)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000, 16];
                Writerb.WriteLine("$" + provinces[i, 0] + "%" + provinces[i, 1] + "%" + provinces[i, 2] + "%" + provinces[i, 3] + "%" + provinces[i, 4] + "%" + provinces[i, 5] + "%" + provinces[i, 6] + "%" + provinces[i, 7] + "%" + provinces[i, 8] + "%" + provinces[i, 9] + "%" + provinces[i, 10] + "%" + provinces[i, 11] + "%" + provinces[i, 12] + "%" + provinces[i, 13] + "%" + provinces[i, 14] + "%" + provinces[i, 15] + "%~");
                i += 1;
            }

            Writerb.Close();

            System.IO.File.WriteAllBytes(path + "//Kingdoms.dat", new byte[0]);
            //System.IO.StreamWriter Writea = new System.IO.StreamWriter(path + "//world.dat");


            //System.IO.File.Create(pathing + "//World.dat"); //Stores World tiles
            System.IO.StreamWriter Writerc = new System.IO.StreamWriter(path + "//Kingdoms.dat");


            i = 0;
            while (kingdoms[i, 0] != null)
            {
                //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
                //string[,] kingdoms = new string[10000, 11];
                //kingdomowner[ib, indcountc] += singlechar.ToString();
                string concatown = "";

                int m = 0;

                while (true)
                {

                    if (kingdomowner[i, m] == null)
                    {
                        break;
                    }

                    concatown += "%" + kingdomowner[i, m] + "%,";

                    m += 1;
                }

                Writerc.WriteLine("$" + kingdoms[i, 0] + "%" + kingdoms[i, 1] + "%" + kingdoms[i, 2] + "%" + kingdoms[i, 3] + " %( " + concatown + " )% " + kingdoms[i, 4] + "%" + kingdoms[i, 5] + "%" + kingdoms[i, 6] + "%" + kingdoms[i, 7] + "%" + kingdoms[i, 8] + "%" + kingdoms[i, 9] + "%" + kingdoms[i, 10] + "%~");
                i += 1;
            }

            Writerc.Close();

            System.IO.File.WriteAllBytes(path + "//Gameinfo.sav", new byte[0]);
            //System.IO.StreamWriter Writea = new System.IO.StreamWriter(path + "//world.dat");


            //System.IO.File.Create(pathing + "//World.dat"); //Stores World tiles
            System.IO.StreamWriter Writerd = new System.IO.StreamWriter(path + "//Gameinfo.sav");

            //System.IO.StreamReader Readd = new System.IO.StreamReader(path + "//GameInfo.sav");

            Writerd.WriteLine(path);
            Writerd.WriteLine(year);
            Writerd.WriteLine(month);
            Writerd.WriteLine(day);
            Writerd.WriteLine(existingreligions);
            Writerd.Close();

            System.IO.StreamWriter Writere = new System.IO.StreamWriter(path + "//History.dat");
            i = 0;
            while (war[i, 0] != null || i == 0)
            {
                if (i == 0 && war[i, 0] == null)
                {
                    Writere.WriteLine("$~");
                    break;
                }
                Writere.WriteLine("$" + war[i, 0] + "%" + war[i, 1] + "%" + war[i, 2] + "%" + war[i, 3] + "%" + war[i, 4] + "%" + war[i, 5] + "%" + war[i, 6] + "%~");
                i += 1;
            }

            Writere.Close();

            System.IO.StreamWriter Writerf = new System.IO.StreamWriter(path + "//Peace.dat");
            //$COUNTRYID%TRUCELENGTH%~
            i = 0;

            while (truce[i] != null || i == 0)
            {
                if (i == 0 && truce[i] == null)
                {
                    Writerf.WriteLine("$~");
                    break;
                }
                Writerf.WriteLine("$" + truce[i] + "%~");
                i += 1;
            }

            Writerf.Close();
        }


        string[] Newsreel = new string[100];
        int lastnews = 0;

        private void updatenews()
        {
            using (var g = Graphics.FromImage(Back.Image))
            {
                Brush newbrushD = new SolidBrush(Color.DarkSlateGray);

                for (int i = 0; i < 16; i++)
                {
                    if (Newsreel[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        Point newpoint = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * (3 + i)));
                        g.DrawString(Newsreel[i], myFontDetail, newbrushD, newpoint);
                    }
                }

                if (lastnews > 15)
                {
                    Array.Clear(Newsreel, 0, 15);
                    lastnews = Math.Max(lastnews - 15, 0);
                }
            }
        }

        private void eventnews(string newevent, string arg1, string arg2)
        {
            if (arg1 == null)
            {
                Newsreel[lastnews] = ev.GetValue(newevent);
                lastnews++;
            }
            else
            {
                Newsreel[lastnews] = String.Format(ev.GetValue(newevent), arg1, arg2);
                lastnews++;
            }
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

            //if (enb == 1)
            //{
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
                        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                        //string[,] provinces = new string[10000,16];
                        ////$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
                        string tm = map[x, y];
                        string tmp1bb = provinces[Convert.ToInt32(map[x, y]) - 2, 3];
                        string tmp2bb = provinces[Convert.ToInt32(map[x, y]) - 2, 1];

                        if (provinces[Convert.ToInt32(map[x, y]) - 2, 3] != provinces[Convert.ToInt32(map[x, y]) - 2, 1] && enb != 3 && enb != 4 && enb != 5)
                        {
                            int tmp3n = Convert.ToInt32(Array.IndexOf(kingidname, provinces[Convert.ToInt32(map[x, y]) - 2, 3]));

                            if (kingdoms[tmp3n, 2] != "TRIBAL")
                            {
                                if (enb == 2)
                                {
                                    if (kingdoms[tmp3n, 2] == "CHIEFTAINSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "KINGDOM")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 255; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "SULTANATE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 204; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "EMPIRE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 204; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "DEMOCRACY")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "COMMUNIST")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 153; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[tmp3n, 2] == "DICTATORSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 51; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 51; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                }
                                else if (enb == 1 || enb == 6 || enb == 7 || enb == 8 || enb == 9)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 13]); //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 14]); //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 15]); //blue
                                }

                            }
                            else
                            {
                                if (enb == 2)
                                {
                                    if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "TRIBAL")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "CHIEFTAINSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "KINGDOM")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 255; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "SULTANATE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 204; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "EMPIRE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 204; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "DEMOCRACY")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "COMMUNIST")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 153; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "DICTATORSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 51; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 51; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                }
                                else if (enb == 1 || enb == 6 || enb == 7 || enb == 8 || enb == 9)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 13]); //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 14]); //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 15]); //blue
                                }
                            }

                        }
                        else if (enb == 3)
                        {
                            if (provinces[Convert.ToInt32(map[x, y]) - 2, 2] != "PAGAN")
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                string tmp = provinces[Convert.ToInt32(map[x, y]) - 2, 2].ToLower();
                                char singlechar = tmp[0];
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(Religions[Array.IndexOf(ReligionId, provinces[Convert.ToInt32(map[x, y]) - 2, 2]), 1]); //red
                                singlechar = tmp[1];
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(Religions[Array.IndexOf(ReligionId, provinces[Convert.ToInt32(map[x, y]) - 2, 2]), 2]); //red
                                singlechar = tmp[2];
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(Religions[Array.IndexOf(ReligionId, provinces[Convert.ToInt32(map[x, y]) - 2, 2]), 3]); //blue
                            }
                            else
                            {
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                            }
                        }
                        else if (enb == 4)
                        {
                            int tmp3n = Convert.ToInt32(Array.IndexOf(kingidname, provinces[Convert.ToInt32(map[x, y]) - 2, 3]));
                            ////$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(255 - Convert.ToByte(kingdoms[tmp3n, 6])); //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(kingdoms[tmp3n, 6]); //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                        }
                        else if (enb == 5)
                        {
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 78; //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 176; //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = 134; //blue
                        }
                        else
                        {
                            if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] != "TRIBAL")
                            {
                                if (enb == 2)
                                {
                                    if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "TRIBAL")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "CHIEFTAINSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "KINGDOM")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 255; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 255; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "SULTANATE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 204; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "EMPIRE")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 102; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 204; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "DEMOCRACY")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 128; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "COMMUNIST")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 153; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                    else if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] == "DICTATORSHIP")
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 51; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 51; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 255; //blue
                                    }
                                    else
                                    {
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                        rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                        rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                    }
                                }
                                else if (enb == 1 || enb == 6 || enb == 7 || enb == 8 || enb == 9)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 13]); //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 14]); //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 15]); //blue
                                }
                            }
                            else
                            {
                                if (enb == 1 || enb == 6 || enb == 7 || enb == 8 || enb == 9)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 78; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 176; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 134; //blue
                                }
                                else if (enb == 2)
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = 0; //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = 0; //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = 0; //blue
                                }
                            }
                        }
                    }

                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            updatenews();


            if (lastnews != 0 && lastnews < 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString(lastnews.ToString(), myFontLarge, newbrush, newpointa); //default map mode
                }
            }
            else if (lastnews >= 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString("+", myFontLarge, newbrush, newpointa); //default map mode
                }
            }

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
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2), 13]); //red
                            rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2), 14]); //green
                            rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(inputarg) - 2), 15]); //blue
                        }
                        else
                        {
                            //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                            //string[,] provinces = new string[10000,16];
                            ////$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~

                            //string tmp1bb = provinces[Convert.ToInt32(map[x, y]) - 2, 3];
                            //string tmp2bb = provinces[Convert.ToInt32(map[x, y]) - 2, 1];

                            if (provinces[Convert.ToInt32(map[x, y]) - 2, 3] != provinces[Convert.ToInt32(map[x, y]) - 2, 1])
                            {
                                int tmp3n = Convert.ToInt32(Array.IndexOf(kingidname, provinces[Convert.ToInt32(map[x, y]) - 2, 3]));

                                if (kingdoms[tmp3n, 2] != "TRIBAL")
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 13]); //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 14]); //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(tmp3n)), 15]); //blue
                                }

                            }
                            else
                            {
                                if (kingdoms[Convert.ToInt32(map[x, y]) - 2, 2] != "TRIBAL")
                                {
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 3] = 255; //alpha
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 2] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 13]); //red
                                    rgbValues[(y * bmpData.Stride) + (x * 4) + 1] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 14]); //green
                                    rgbValues[(y * bmpData.Stride) + (x * 4)] = Convert.ToByte(provinces[(Convert.ToInt16(Convert.ToInt32(map[x, y])) - 2), 15]); //blue
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

                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            if (lastnews != 0 && lastnews < 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString(lastnews.ToString(), myFontLarge, newbrush, newpointa); //default map mode
                }
            }
            else if (lastnews >= 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString("+", myFontLarge, newbrush, newpointa); //default map mode
                }
            }


        }

        private void Observe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        byte speed = 0;

        public void defaultbuttons()
        {
            using (var g = Graphics.FromImage(Back.Image))
            {
                Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
                Brush newbrushA = new SolidBrush(Color.White);
                Brush newbrushB = new SolidBrush(Color.Red);
                Brush newbrushBlaq = new SolidBrush(Color.Black);
                Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);


                //invalidate??
                Back.Invalidate();

                Point newpoint0 = new Point(Convert.ToInt16(((xlen / 40) * 35.2)), Convert.ToInt16((ylen / 40) * 30));
                g.DrawString("Save", myFontTitle, newbrushD, newpoint0);

                Point newpointa1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 30));
                g.DrawString("Back", myFontTitle, newbrushD, newpointa1);

                Point newpointa2 = new Point(Convert.ToInt16(((xlen / 40) * 33.2)), Convert.ToInt16((ylen / 40) * 0));
                g.DrawString("D", myFontTitle, newbrushA, newpointa2); //default map mode

                Point newpointa3 = new Point(Convert.ToInt16(((xlen / 40) * 34.2)), Convert.ToInt16((ylen / 40) * 0));
                g.DrawString("K", myFontTitle, newbrushA, newpointa3); //type map mode

                Point newpointa4 = new Point(Convert.ToInt16(((xlen / 40) * 35.2)), Convert.ToInt16((ylen / 40) * 0));
                g.DrawString("R", myFontTitle, newbrushA, newpointa4); //Religion map mode

                Point newpointa5 = new Point(Convert.ToInt16(((xlen / 40) * 36.2)), Convert.ToInt16((ylen / 40) * 0));
                g.DrawString("S", myFontTitle, newbrushA, newpointa5); //Science

                Point newpointa6 = new Point(Convert.ToInt16(((xlen / 40) * 37.2)), Convert.ToInt16((ylen / 40) * 0));
                g.DrawString("T", myFontTitle, newbrushA, newpointa6); //Terrain map mode

                Point newpointa8 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 2));
                g.DrawString("ll", myFontTitle, newbrushBlaq, newpointa8); //Pause

                Point newpointa7 = new Point(Convert.ToInt16(((xlen / 40) * 32.9)), Convert.ToInt16((ylen / 40) * 2));
                g.DrawString("►", myFontTitle, newbrushBlaq, newpointa7); //One day per sec

                Point newpointa9 = new Point(Convert.ToInt16(((xlen / 40) * 33.5)), Convert.ToInt16((ylen / 40) * 2));
                g.DrawString("►►", myFontTitle, newbrushBlaq, newpointa9); //One week per sec

                Point newpointa10 = new Point(Convert.ToInt16(((xlen / 40) * 34.5)), Convert.ToInt16((ylen / 40) * 2));
                g.DrawString("►►►", myFontTitle, newbrushBlaq, newpointa10); //One month per sec

                Point newpointa11 = new Point(Convert.ToInt16(((xlen / 40) * 36)), Convert.ToInt16((ylen / 40) * 2));
                g.DrawString(day + "/" + month + "/" + year, myFontDetail, newbrushBlaq, newpointa11); //One month per sec

                if (enb == 0)
                {

                }
                else if (enb == 1)
                {
                    g.DrawString("D", myFontTitle, newbrushB, newpointa2); //default map mode
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    speed = 0;
                }
                else if (enb == 2)
                {
                    g.DrawString("K", myFontTitle, newbrushB, newpointa3); //type map mode
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    speed = 0;
                }
                else if (enb == 3)
                {
                    g.DrawString("R", myFontTitle, newbrushB, newpointa4); //Religion map mode
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    speed = 0;
                }
                else if (enb == 4)
                {
                    g.DrawString("S", myFontTitle, newbrushB, newpointa5); //Science
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    speed = 0;
                }
                else if (enb == 5)
                {
                    g.DrawString("T", myFontTitle, newbrushB, newpointa6); //Terrain map mode
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    speed = 0;
                }
                else if (enb == 6)
                {
                    g.DrawString("ll", myFontTitle, newbrushB, newpointa8); //Pause
                    g.DrawString("D", myFontTitle, newbrushB, newpointa2); //default map mode
                    speed = 0;
                    enb = 1;
                }
                else if (enb == 7)
                {
                    g.DrawString("►", myFontTitle, newbrushB, newpointa7); //day per sec
                    g.DrawString("D", myFontTitle, newbrushB, newpointa2); //default map mode
                    speed = 1;
                    tock.Start();
                    //enb = 1;
                }
                else if (enb == 8)
                {
                    g.DrawString("►►", myFontTitle, newbrushB, newpointa9);  //week per sec
                    g.DrawString("D", myFontTitle, newbrushB, newpointa2); //default map mode
                    speed = 2;
                    tock.Start();
                    //enb = 1;
                }
                else if (enb == 9)
                {
                    g.DrawString("►►►", myFontTitle, newbrushB, newpointa10);  //month per sec
                    g.DrawString("D", myFontTitle, newbrushB, newpointa2); //default map mode
                    speed = 3;
                    tock.Start();
                    //enb = 1;
                }
            }
        }

        private void ImagePic_Click(object sender, EventArgs e)
        {
            Back.Invalidate();
            Point p = this.PointToClient(Cursor.Position);
            mouseposX = Math.Max(0, p.X);
            mouseposY = Math.Max(0, p.Y);
            Brush newbrushD = new SolidBrush(Color.DarkSlateGray);
            Brush newbrushC = new SolidBrush(Color.DarkSlateBlue);

            if (map[mouseposX, mouseposY] != "0")
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    enb = 1;
                    PersonaliseBits(map[mouseposX, mouseposY]);
                    defaultbuttons();
                    string tm2 = map[mouseposX, mouseposY];
                    string tm = kingdoms[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 2];
                    if (provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 3] != provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 1] || kingdoms[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 2] != "TRIBAL")
                    {
                        ////$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~

                        Point newpointa1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 4));

                        int tmp3n = Convert.ToInt32(Array.IndexOf(kingidname, provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 3]));

                        Point newpointa2 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 3.2));

                        if (kingidname[Convert.ToInt32(map[mouseposX, mouseposY]) - 2] == null)
                        {
                            g.DrawString("Owned land of ", myFontDetail, newbrushD, newpointa2);
                            g.DrawString(kingdoms[tmp3n, 1], myFontTitle, newbrushD, newpointa1);
                        }
                        else
                        {
                            g.DrawString("Capital city of ", myFontDetail, newbrushD, newpointa2);
                            g.DrawString(kingidname[Convert.ToInt32(map[mouseposX, mouseposY]) - 2], myFontTitle, newbrushD, newpointa1);
                            //kingdoms[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 1]
                            //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                        }

                        //Bug - incorrect info displayed
                        Point newpointb2 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 5));
                        g.DrawString("Type : " + kingdoms[Convert.ToInt32(tmp3n), 2], myFontDetail, newbrushD, newpointb2);

                        Point newpointb1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 6));
                        g.DrawString("Religion : " + kingdoms[Convert.ToInt32(tmp3n), 3], myFontDetail, newbrushD, newpointb1);

                        Point newpointc1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 7));
                        g.DrawString("Spirituality : " + kingdoms[Convert.ToInt32(tmp3n), 4], myFontDetail, newbrushD, newpointc1);

                        Point newpointd1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 8));
                        g.DrawString("Ethics : " + kingdoms[Convert.ToInt32(tmp3n), 5], myFontDetail, newbrushD, newpointd1);

                        Point newpointe1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 9));
                        g.DrawString("Science : " + kingdoms[Convert.ToInt32(tmp3n), 6] + "/255", myFontDetail, newbrushD, newpointe1);

                        Point newpointf1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 10));
                        g.DrawString("Ruler :" + kingdoms[Convert.ToInt32(tmp3n), 7], myFontDetail, newbrushD, newpointf1);

                        Point newpointg1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 11));
                        g.DrawString("Dynasty : " + kingdoms[Convert.ToInt32(tmp3n), 8], myFontDetail, newbrushD, newpointg1);

                        Point newpointh1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 12));
                        g.DrawString("Age : " + kingdoms[Convert.ToInt32(tmp3n), 9], myFontDetail, newbrushD, newpointh1);

                        Point newpointi1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 13));
                        g.DrawString("Manpower : " + kingdoms[Convert.ToInt32(tmp3n), 10], myFontDetail, newbrushD, newpointi1);

                        //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~


                        Point newpointa = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 16));
                        g.DrawString(provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 1], myFontTitle, newbrushD, newpointa);

                        Point newpointb = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 17));
                        g.DrawString("Religion : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 2], myFontDetail, newbrushD, newpointb);

                        Point newpointc = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 18));
                        g.DrawString("Bronze : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 4] + "%", myFontDetail, newbrushD, newpointc);

                        Point newpointd = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 19));
                        g.DrawString("Iron : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 5] + "%", myFontDetail, newbrushD, newpointd);

                        Point newpointe = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 20));
                        g.DrawString("Steel : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 6] + "%", myFontDetail, newbrushD, newpointe);

                        Point newpointf = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 21));
                        g.DrawString("Gunpowder : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 7] + "%", myFontDetail, newbrushD, newpointf);

                        Point newpointg = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 22));
                        g.DrawString("Oil : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 8] + "%", myFontDetail, newbrushD, newpointg);

                        Point newpointh = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 23));
                        g.DrawString("Theology : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 9] + "%", myFontDetail, newbrushD, newpointh);

                        Point newpointi = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 24));
                        g.DrawString("Science : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 10] + "%", myFontDetail, newbrushD, newpointi);

                        Point newpointj = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 25));
                        g.DrawString("Happiness : " + provinces[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 11], myFontDetail, newbrushD, newpointj);

                    }
                    else
                    {


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

                        Point newpointh1 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 15));
                        g.DrawString("Manpower : " + kingdoms[Convert.ToInt32(map[mouseposX, mouseposY]) - 2, 10], myFontDetail, newbrushD, newpointh1);
                    }
                }
            }
            else if (mouseposX >= (xlen / 80) * 66)
            {
                if (mouseposY >= Convert.ToInt16((ylen / 40) * 29.7) && mouseposY <= Convert.ToInt16((ylen / 40) * 31))
                {
                    if (mouseposX >= Convert.ToInt16((xlen / 40) * 32.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 33.2))
                    {
                        Application.Restart();
                    }
                    else if (mouseposX >= Convert.ToInt16((xlen / 40) * 34.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 37.2))
                    {
                        Save();
                    }
                }
                else
                {

                    if (mouseposY >= Convert.ToInt16((ylen / 40) * 0) && mouseposY <= Convert.ToInt16((ylen / 40) * 1))
                    {
                        if (mouseposX >= Convert.ToInt16((xlen / 40) * 33.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 34))
                        {
                            enb = 1;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 34.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 35))
                        {
                            enb = 2;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 35.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 36))
                        {
                            enb = 3;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 36.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 37))
                        {
                            enb = 4;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 37.2) && mouseposX <= Convert.ToInt16((xlen / 40) * 38))
                        {
                            enb = 5;
                        }

                    }
                    else if (mouseposY >= Convert.ToInt16((ylen / 40) * 1) && mouseposY <= Convert.ToInt16((ylen / 40) * 3))
                    {
                        if (mouseposX >= Convert.ToInt16((xlen / 40) * 32) && mouseposX <= Convert.ToInt16((xlen / 40) * 32.7))
                        {
                            enb = 6;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 32.8) && mouseposX <= Convert.ToInt16((xlen / 40) * 33.4))
                        {
                            enb = 7;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 33.41) && mouseposX <= Convert.ToInt16((xlen / 40) * 34.4))
                        {
                            enb = 8;
                        }
                        else if (mouseposX >= Convert.ToInt16((xlen / 40) * 34.41) && mouseposX <= Convert.ToInt16((xlen / 40) * 35.41))
                        {
                            enb = 9;
                        }
                    }
                    //Point newpointa8 = new Point(Convert.ToInt16(((xlen / 40) * 32.2)), Convert.ToInt16((ylen / 40) * 2));
                    //g.DrawString("ll", myFontTitle, newbrushBlaq, newpointa8); //Pause

                    //Point newpointa7 = new Point(Convert.ToInt16(((xlen / 40) * 32.9)), Convert.ToInt16((ylen / 40) * 2));
                    //g.DrawString("►", myFontTitle, newbrushBlaq, newpointa7); //One day per sec

                    //Point newpointa9 = new Point(Convert.ToInt16(((xlen / 40) * 33.5)), Convert.ToInt16((ylen / 40) * 2));
                    //g.DrawString("►►", myFontTitle, newbrushBlaq, newpointa9); //One week per sec

                    //Point newpointa10 = new Point(Convert.ToInt16(((xlen / 40) * 34.5)), Convert.ToInt16((ylen / 40) * 2));
                    //g.DrawString("►►►", myFontTitle, newbrushBlaq, newpointa10); //One month per sec

                    Dobits();
                    defaultbuttons();


                }
            }
            else
            {
                Array.Clear(Newsreel, 0, 15);
                lastnews = Math.Max(lastnews - 15, 0);
                Dobits();
                defaultbuttons();


            }
        }

        private void Reinforcements()
        {
            //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
            //string[,] provinces = new string[10000, 16];

            //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
            //string[,] kingdoms = new string[10000, 11];
            //string[,] kingdomowner = new string[10000, 10000];
            //string[] kingidname = new string[10000];
            int i = 0;
            while (true)
            {
                if (kingdoms[i, 0] != null)
                {

                    if (provinces[i, 3] != provinces[i, 1])
                    {
                        int m = Convert.ToInt32(Array.IndexOf(kingidname, provinces[i, 3]));

                        if (Convert.ToInt32(kingdoms[m, 6]) < 50)
                        {
                            kingdoms[m, 10] = Convert.ToString(Convert.ToInt32(kingdoms[m, 10]) + 0.001 * (1000 * Convert.ToInt32(provinces[i, 4])));
                        }
                        else if (Convert.ToInt32(kingdoms[m, 6]) < 100)
                        {
                            kingdoms[m, 10] = Convert.ToString(Convert.ToInt32(kingdoms[m, 10]) + 0.001 * (1000 * Convert.ToInt32(provinces[i, 5])));
                        }
                        else if (Convert.ToInt32(kingdoms[m, 6]) < 150)
                        {
                            kingdoms[m, 10] = Convert.ToString(Convert.ToInt32(kingdoms[m, 10]) + 0.001 * (1000 * Convert.ToInt32(provinces[i, 6])));
                        }
                        else if (Convert.ToInt32(kingdoms[m, 6]) < 200)
                        {
                            kingdoms[m, 10] = Convert.ToString(Convert.ToInt32(kingdoms[m, 10]) + 0.001 * (1000 * Convert.ToInt32(provinces[i, 7])));
                        }
                        else
                        {
                            kingdoms[m, 10] = Convert.ToString(Convert.ToInt32(kingdoms[m, 10]) + 0.001 * (1000 * Convert.ToInt32(provinces[i, 8])));
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(kingdoms[i, 6]) < 50)
                        {
                            kingdoms[i, 10] = Convert.ToString(Convert.ToInt32(kingdoms[i, 10]) + 0.01 * (1000 * Convert.ToInt32(provinces[i, 4])));
                        }
                        else if (Convert.ToInt32(kingdoms[i, 6]) < 100)
                        {
                            kingdoms[i, 10] = Convert.ToString(Convert.ToInt32(kingdoms[i, 10]) + 0.01 * (1000 * Convert.ToInt32(provinces[i, 5])));
                        }
                        else if (Convert.ToInt32(kingdoms[i, 6]) < 150)
                        {
                            kingdoms[i, 10] = Convert.ToString(Convert.ToInt32(kingdoms[i, 10]) + 0.01 * (1000 * Convert.ToInt32(provinces[i, 6])));
                        }
                        else if (Convert.ToInt32(kingdoms[i, 6]) < 200)
                        {
                            kingdoms[i, 10] = Convert.ToString(Convert.ToInt32(kingdoms[i, 10]) + 0.01 * (1000 * Convert.ToInt32(provinces[i, 7])));
                        }
                        else
                        {
                            kingdoms[i, 10] = Convert.ToString(Convert.ToInt32(kingdoms[i, 10]) + 0.01 * (1000 * Convert.ToInt32(provinces[i, 8])));
                        }
                    }
                }
                else
                {
                    break;
                }

                i += 1;
            }
        }

        int existingreligions = 0;

        private void die()
        {
            int i = 0;
            while (true)
            {
                //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
                //string[,] kingdoms = new string[10000, 11];
                //string[,] kingdomowner = new string[10000, 10000];
                //string[] kingidname = new string[10000];

                if (kingdoms[i, 0] == null)
                {
                    break;
                }
                int rng = rand.Next(1, 200);

                if (rng <= Convert.ToInt16(kingdoms[i, 9]) - 16)
                {
                    rng = rand.Next(1, 100);
                    if (rng <= 94)
                    {
                        kingdoms[i, 7] = NamesList[rand.Next(1, 500)];

                        kingdoms[i, 5] = Convert.ToString(Convert.ToInt32(kingdoms[i, 5]) + rand.Next(-1, 2));
                        kingdoms[i, 4] = Convert.ToString(Convert.ToInt32(kingdoms[i, 4]) + rand.Next(-1, 2));

                        kingdoms[i, 9] = rand.Next(16, 50).ToString();

                    }
                    else
                    {
                        kingdoms[i, 5] = Convert.ToString(rand.Next(-5, 5));
                        kingdoms[i, 4] = Convert.ToString(rand.Next(-5, 5));
                        kingdoms[i, 7] = NamesList[rand.Next(1, 500)];
                        kingdoms[i, 8] = SurnameList[rand.Next(1, 500)];
                        kingdoms[i, 9] = rand.Next(16, 50).ToString();
                    }
                    //Writer3.Write(NamesList[Rander.Next(1, 500)] + "%");
                    //Writer3.Write(SurnameList[Rander.Next(1, 500)] + "%");
                    //Writer3.Write(Rander.Next(16, 50) + "%");
                }
                else
                {
                    kingdoms[i, 9] = (Convert.ToInt16(kingdoms[i, 9]) + 1).ToString();
                }
                i += 1;

            }
        }

        int maxsci = 0;

        private void MaxScience()
        {
            int i = 0;
            while (true)
            {
                if (kingdoms[i, 0] == null)
                {
                    break;
                }
                else
                {
                    if (Convert.ToInt16(kingdoms[i, 6]) > maxsci)
                    {
                        maxsci = Convert.ToInt16(kingdoms[i, 6]);
                    }
                }

                i += 1;
            }
        }

        int[] valuesperprov = new int[10000];

        private void ValueProv()
        {
            int i = 0;
            int smallest = 999999;
            int biggest = 0;

            while (true)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000, 16];

                if (provinces[i, 0] == null)
                {
                    break;
                }

                if (maxsci < 50)
                {
                    valuesperprov[i + 2] = (Convert.ToInt16(provinces[i, 4]) + Convert.ToInt16(kingdoms[i, 9]) + Convert.ToInt16(provinces[i, 9]));
                }
                else if (maxsci < 100)
                {
                    valuesperprov[i + 2] = (Convert.ToInt16(provinces[i, 5]) + Convert.ToInt16(kingdoms[i, 9]) + Convert.ToInt16(provinces[i, 9]));
                }
                else if (maxsci < 150)
                {
                    valuesperprov[i + 2] = (Convert.ToInt16(provinces[i, 6]) + Convert.ToInt16(kingdoms[i, 9]) + Convert.ToInt16(provinces[i, 9]));
                }
                else if (maxsci < 200)
                {
                    valuesperprov[i + 2] = (Convert.ToInt16(provinces[i, 7]) + Convert.ToInt16(kingdoms[i, 9]) + Convert.ToInt16(provinces[i, 9]));
                }
                else
                {
                    valuesperprov[i + 2] = (Convert.ToInt16(provinces[i, 8]) + Convert.ToInt16(kingdoms[i, 9]) + Convert.ToInt16(provinces[i, 9]));
                }

                if (valuesperprov[i + 2] > biggest)
                {
                    biggest = valuesperprov[i + 2];
                }
                else if (valuesperprov[i + 2] < smallest)
                {
                    smallest = valuesperprov[i + 2];
                }
                i += 1;
            }

            i = 0;
            while (true)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000, 16];

                if (provinces[i, 0] == null)
                {
                    break;
                }

                valuesperprov[i + 2] = Math.Abs(smallest - valuesperprov[i + 2]);
                i += 1;
            }
        }

        private void WarFunc()
        {
            int i = 2;

            while (true)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000, 16];

                //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~

                if (provinces[i, 0] == null)
                {
                    break;
                }

                string[] tempreturn = new string[10000];
                tempreturn = return_adjacent_king(i);

                int m = 0;
                int off = -10;
                if (rand.Next(1, 1000) == 5)
                {
                    while (true)
                    {
                        if (tempreturn[m] == null)
                        {
                            break;
                        }

                        int tempid = Convert.ToInt16(kingdoms[Convert.ToInt16(provinces[Convert.ToInt16(tempreturn[m]), 0]), 0]);
                        if (decidewar(Convert.ToInt16(valuesperprov[Convert.ToInt16(tempreturn[m])]), i, tempid, off))
                        {
                            //Console.WriteLine("A");
                            eventnews("War_Declare_0", kingdoms[tempid, 1], kingdoms[Convert.ToInt16(provinces[tempid,0]),1]);
                            //$WARID%WARTYPE%WARNAME%AGGRESSORID%AGRESSORSCORE%DEFENDERID%DEFENDERSCORE%~
                            
                            break;
                        }
                        else
                        {
                            //Console.WriteLine("B");
                        }
                        m += 1;
                        off += rand.Next(-2, 2);
                    }
                }

                i += 1;
            }
        }

        private bool decidewar(int value, int userid, int enemyid,int offset)
        {
            double score = value;

            if(Convert.ToInt32(kingdoms[userid,10]) < Convert.ToInt32(kingdoms[enemyid,10]))
            {
                score -= 100;
            }
            else
            {
                double percent = Convert.ToInt32(kingdoms[enemyid, 10]);
                score += Math.Min((((Convert.ToInt32(kingdoms[userid, 10]) / percent))),50);
            }

            score += rand.Next(-50, 10) + offset;

            if(score > 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string[] return_adjacent_king(int user)
        {
            //find all owned provinces by the target
            string[] adjtiles = new string[10000];

            int i = 0;
            int back = 0;

            while (true)
            {
                //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                //string[,] provinces = new string[10000, 16];

                //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~

                if (provinces[i, 0] == null)
                {
                    break;
                }

                //UP LEFT DOWN RIGHT
                if (provinces[i,3] == kingdoms[user,1])
                {
                    for (int m = 0; m < 4; m++)
                    {
                        if (adjs[i, m] == null)
                        {

                        }
                        else
                        {
                            adjtiles[back] = adjs[i, m];
                            back += 1;
                        }
                    }
                }
                i += 1;
            }

            return adjtiles;

                //find all adjacents by the target
                //compile list of adjacents - ignore own provinces and duplicates.
            }

        private void Religion_Form()
        {

            //IMPORTANT

            //EXISTING RELIGIONS DOES NOT SAVE
            int check = rand.Next(1, (10^(existingreligions + 1)));

            if (check == 5 && existingreligions < 11) //check == 50
            {
                int count = 0;
                int largestindex = 0;
                bool nochange = true;

                while (true)
                {
                    //$ID%Name%Religion%OwningEmpire%Bronze%Iron%Steel%Gunpowder%Oil%Theology%Science%Happiness%Capital%R%G%B%~
                    //string[,] provinces = new string[10000, 16];

                    //$ID%NAME%TYPE%OFFICIALRELIGION%(OWNEDPROV)%SPIRIT%ETHICS%SCIENCE%RULERF%RULERS%RULERAGE%MANPOWER%~
                    //string[,] kingdoms = new string[10000, 11];
                    //string[,] kingdomowner = new string[10000, 10000];
                    //string[] kingidname = new string[10000];
                    if (provinces[count,0] == null)
                    {
                        break;
                    }
                    if (Convert.ToInt16(provinces[count,9]) > Convert.ToInt16(provinces[largestindex,9]) && provinces[count,2] == "PAGAN")
                    {
                        if (provinces[count, 3] != provinces[count, 1])
                        {
                            int m = Convert.ToInt32(Array.IndexOf(kingidname, provinces[count, 3]));

                            if(kingdoms[m,3] == "NULL" && kingdoms[m,2] != "TRIBAL")
                            {
                                largestindex = count;
                                nochange = false;
                            }
                        }
                        else
                        {
                            if (kingdoms[count, 2] != "TRIBAL")
                            {
                                largestindex = count;
                                nochange = false;
                            }
                        }
                    }
                    count += 1;
                }

                //$Name%Red%Blue%Green%~
                //string[,] Religions = new string[11, 4];
                //string[] ReligionId = new string[11];

                if (nochange == false || largestindex == 0 && provinces[largestindex, 2] == "PAGAN" && kingdoms[largestindex + 2, 2] != "TRIBAL")
                {
                    provinces[largestindex, 2] = ReligionId[existingreligions];
                    eventnews("Religion_Form_1", provinces[largestindex, 1], ReligionId[existingreligions]);
                    existingreligions += 1;
                }
            }
        }

        private void Tock_Tick(object sender, EventArgs e)
        {
            int maxcount = 0;
            int realcount = 0;

            if(speed == 0)
            {
                tock.Stop();
                return;
            }
            else if(speed == 1) //slowboye
            {
                maxcount = 1;
                tock.Interval = 500;
            }
            else if (speed == 2) //average speedboye
            {
                maxcount = 1;
                tock.Interval = 71;
                maxcount = rand.Next(5, 15);
            }
            else if (speed == 3) //vvvv fastboye
            {
                maxcount = 1;
                tock.Interval = 16;
                maxcount = rand.Next(20,41);
            }

            while (realcount <= maxcount - 1)
            {
                day += 1;

                if (day == 31)
                {
                    month += 1;
                    day = 1;
                    Reinforcements();
                    Religion_Form();
                    eventnews("Reinforce", null, null);
                    MaxScience();
                    ValueProv();
                    WarFunc();
                }
                else if(day == 29)
                {
                    Array.Clear(Newsreel, 0, 15);
                    lastnews = Math.Max(lastnews - 15, 0);
                }

                if (month == 13)
                {
                    year += 1;
                    eventnews("New_Year",year.ToString(),null);
                    die();
                    month = 1;
                }

                if(year % 10 == 0 && day == 1 && month == 1)
                {
                    //Save();
                }

                realcount += 1;
            }

            Dobits();
            defaultbuttons();
            //mapprov();

            Back.Invalidate();
            if (lastnews != 0 && lastnews < 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString(lastnews.ToString(), myFontLarge, newbrush, newpointa); //default map mode
                }
            }
            else if (lastnews >= 10)
            {
                using (var g = Graphics.FromImage(Back.Image))
                {
                    Point newpointa = new Point(Convert.ToInt16(((xlen / 80) * 65)), Convert.ToInt16((ylen / 40) * 0));
                    Brush newbrush = new SolidBrush(Color.Red);
                    g.DrawString("+", myFontLarge, newbrush, newpointa); //default map mode
                }
            }
        }

        private void Play_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (enb != 1)
                {
                    //tock.Stop();
                    //speed = 0;
                    enb = 1;
                }
                else
                {
                    enb = 7;
                }

                Dobits();
                defaultbuttons();
            }
        }
    }
}
