using System;
using System.IO;

namespace funct
{
    public class Functions
    {
        public static string Generator(string type)
        {
            Random ran = new Random();
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            char[] newname = new char[20];
            int size = 0;

            //okay future jamie, plan time.
            //get a random number between 2 and 5
            //and a random number between 1 and the list size
            //use the second random number to choose 2-5 characters from a name
            //then redo the randoms and find the next best value that has the same letter in the same place as the last character
            //continue this until done
            //eg
            //conventry = 2 = co
            //wodonga = 3 = codon
            //Devonport = 2 = codonpo
            //etc until impossible


            if (type == "Prov")
                {

                    string FileName = string.Format("{0}Resources\\ProvincesReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\"))); //real file


                    StreamReader read = new StreamReader(FileName);
                    String[] citi = new String[23018]; //array of city names

                    for (int i = 0; i <= 23017; i++)
                    {
                        citi[i] = read.ReadLine();
                    }

                read.Close();

                int rando = ran.Next(2, 6); //how many characters to take
                int randpro = ran.Next(1, 23018); //which line to search
                
                char[] pull = citi[randpro].ToCharArray();
                int tempoff = 0;

                for (int i = 0; i <= 20; i++)
                {

                    int tempoff2 = tempoff;

                    for(int ioff = tempoff; ioff <= (tempoff2 + rando); ioff++) //the change in rand
                    {
                        if (ioff >= 20 || pull.Length <= ioff)
                        {
                            break;
                        }
                        else
                        {
                            newname[ioff] = pull[ioff];
                            tempoff += 1;
                        }
                    }

                    if(tempoff >= 6)
                    {
                        if(ran.Next(1, 3) == 2)
                        {
                            size = tempoff;
                            break;
                        }
                    }
                    while (true)
                    {
                        randpro = ran.Next(1, 23018);
                        rando = ran.Next(1, 6);
                        pull = citi[randpro].ToCharArray();
                        int tmp = (int)pull[pull.Length - 1];
                        if(tmp >= 255)
                        {
                            if(ran.Next(1,100) == 30)
                            {
                                break;
                            }
                        }
                        else
                        {
                        if(pull.Length <= tempoff)
                        {

                        }
                        else
                        {
                            if (pull[tempoff - 1] == newname[tempoff - 1])
                            {
                                break;
                            }
                        }
                        }
                        
                    }
                }
             }
            else if(type == "Real")
            {
                string FileName = string.Format("{0}Resources\\ProvincesReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\"))); //real file


                StreamReader read = new StreamReader(FileName);
                String[] citi = new String[23018]; //array of city names
                int randpro = ran.Next(1, 23018); //which line to search

                for (int i = 0; i <= 23017; i++)
                {
                    citi[i] = read.ReadLine();
                }

                read.Close();

                randpro = ran.Next(1, 23018);
                char[] pull = citi[randpro].ToCharArray();

                size = Math.Min(pull.Length - 1,19);

                for(int i = 0; i <= pull.Length - 1  && i < 20;i++)
                {
                    newname[i] = pull[i];
                }
            }
            else if(type == "Faith")
            {

                string FileName = string.Format("{0}Resources\\ReligionsReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\"))); //real file


                StreamReader read = new StreamReader(FileName);
                String[] citi = new String[505]; //array of religion names

                for (int i = 0; i <= 504; i++)
                {
                    citi[i] = read.ReadLine();
                }

                read.Close();

                int rando = ran.Next(2, 6); //how many characters to take
                int randpro = ran.Next(1, 505); //which line to search

                char[] pull = citi[randpro].ToCharArray();
                int tempoff = 0;

                for (int i = 0; i <= 20; i++)
                {

                    int tempoff2 = tempoff;

                    for (int ioff = tempoff; ioff <= (tempoff2 + rando); ioff++) //the change in rand
                    {
                        if (ioff >= 20 || pull.Length <= ioff)
                        {
                            break;
                        }
                        else
                        {
                            newname[ioff] = pull[ioff];
                            tempoff += 1;
                        }
                    }

                    if (tempoff >= 6)
                    {
                        if (ran.Next(1, 3) == 2)
                        {
                            size = tempoff;
                            break;
                        }
                    }
                    while (true)
                    {
                        randpro = ran.Next(1, 505);
                        rando = ran.Next(1, 6);
                        pull = citi[randpro].ToCharArray();
                        int tmp = (int)pull[pull.Length - 1];
                        if (tmp >= 255)
                        {
                            if (ran.Next(1, 100) == 30)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (pull.Length <= tempoff)
                            {

                            }
                            else
                            {
                                if (pull[tempoff - 1] == newname[tempoff - 1])
                                {
                                    break;
                                }
                            }
                        }

                    }
                }
            }
            else if(type == "Fname")
            {
                string FileName = string.Format("{0}Resources\\FirstNameReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\"))); //real file


                StreamReader read = new StreamReader(FileName);
                String[] citi = new String[4945]; //array of names
                int randpro = ran.Next(1, 4944); //which line to search

                for (int i = 0; i <= 4944; i++)
                {
                    citi[i] = read.ReadLine();
                }

                read.Close();

                randpro = ran.Next(1, 4944);
                char[] pull = citi[randpro].ToCharArray();

                size = Math.Min(pull.Length - 1, 19);

                for (int i = 0; i <= pull.Length - 1 && i < 20; i++)
                {
                    newname[i] = pull[i];
                }

            }
            else if (type == "Sname")
            {
                string FileName = string.Format("{0}Resources\\SurnamesReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\"))); //real file


                StreamReader read = new StreamReader(FileName);
                String[] citi = new String[129037]; //array of surnames
                int randpro = ran.Next(1, 129036); //which line to search

                for (int i = 0; i <= 129036; i++)
                {
                    citi[i] = read.ReadLine();
                }

                read.Close();

                randpro = ran.Next(1, 129036);
                char[] pull = citi[randpro].ToCharArray();

                size = Math.Min(pull.Length - 1, 19);

                for (int i = 0; i <= pull.Length - 1 && i < 20; i++)
                {
                    newname[i] = pull[i];
                }
            }

            string outpt = null;

            for (int i = 0; i <= size - 1; i++)
            {
                outpt += newname[i];
            }

            return outpt;
        }
    }
}