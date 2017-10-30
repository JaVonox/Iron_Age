using System;
using System.IO;

namespace funct
{
    public class Functions
    {
        public static void MarkovsGen(string type)
        {
            Random ran = new Random();
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\ProvincesReal.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));


            StreamReader read = new StreamReader(FileName);
            String[] citi = new String[23018];

            for (int i = 0; i <= 23017;i++ )
            {
                citi[i] = read.ReadLine();
            }

            read.Close();

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
                    int rando = ran.Next(2, 6);
                    int randpro = ran.Next(1, 23018);


                }
        }
    }
}