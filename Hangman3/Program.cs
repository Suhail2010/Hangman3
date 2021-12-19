using static System.Console;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Hangman3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                WriteLine(
                    "\n\n1. Spella Hangman      \n" +
                    "\n0. Stäng Programmet  \n"
                    );
                WriteLine("\n\nVälja ett nummer");
                int menue = Menue();
                Clear();
                switch (menue)
                {
                    case 1:
                        Play();
                        break;
                    case 0:
                        Clear();
                        WriteLine("\nVill du stänga programmet tryck y");
                        if (ReadLine() == "y") run = false;
                        break;
                }
                Clear();
            }
            Environment.Exit(0);
        }
        static int Menue()
        {
            try
            {
                int num = int.Parse(ReadLine());
                return num == 0 || num == 1 ? num : throw new ArgumentException();
            }
            catch (ArgumentException error)
            {
                WriteLine("Ange 1 or  0   " + error.Message);
            }
            catch (FormatException error)
            {
                WriteLine("Error fel input   " + error.Message);
            }
            finally
            {
                WriteLine();
            }
            return Menue();
        
        }

        static Random rnd = new Random();
        static void Play()
        {
            string[] text = new string[] { "animals", "oceans", "continents", "atmosfear", "mountains",
                "plants", "viruses", "bacteria", "amphibians", "birds" };
            int rando = rnd.Next(0, 9);
            string word = text[rando];
            char[] gessword = Regex.Replace(word, @"\w", "_").ToCharArray();
            int key = 1;
            StringBuilder sb = new StringBuilder();
            string failed = "";
            int tries = 0;
            Hangman(gessword, failed, tries);
            for (tries = 0; tries < 10; tries++)
            {
                string gess = ReadLine();
                if (gess.Length == 1) 
                { 
                    key = 0;
                    int i = 0;
                    char gessLetter = char.Parse(gess);
                    foreach (char ch in word) 
                    { 
                        if ( gessLetter == gessword[i] || sb.ToString().Contains(gess))
                        {
                            WriteLine("Letter already used! Try another one");
                            key = 1;
                            tries--;
                            ReadLine();
                            break;
                        }
                        else if (gessLetter == ch)
                        {
                            gessword[i] = gessLetter;
                            key = 1;
                        }
                        i++;

                    }
                    if (key == 0)
                    {
                        if (tries != 9) WriteLine("Wrong gess try again");
                        sb.Append(gessLetter + "_");
                        failed = sb.ToString();
                        ReadLine();
                    }
                }
                if (gess == word || string.Equals(word,new string(gessword)))
                {

                    gessword = word.ToCharArray();
                    tries += 20;
                    Hangman(gessword, failed, tries + 1);
                    ReadLine();
                    tries = -1;
                    break;
                }
                else
                if (gess.Length > 1)
                {
                    WriteLine("Too bad it isn't the right word. Better luck next time.");
                    ReadLine();
                    sb.Append(gess + "_");
                    failed = sb.ToString();
                    tries = 10;
                    Hangman(gessword, failed, tries);
                    break;
                }
                else if (gess == "")
                {
                    tries--;
                }

                Hangman(gessword, failed, tries + 1);
            }
            if (tries == -1) { return; }
            WriteLine(" ´H A N G M A N` :(   Didn't succeed give it another try");
            ReadLine();
        }



        static void Hangman(char[] gessword, string failed, int tries)
        {
            Clear();
            string[] hangman = {
            "              ________          ",
            "              |      |          ",
            "              |      O          ",
            "              |     /|\\        ",
            "              |    / | \\       ",
            "              |     / \\        ",
            "              |    /   \\       ",
            "             /|                 ",
            "            / |                 ",
            "          _/__|_H_a_n_g_e_d_    "
            };
            int won = 0;
            if (tries > 14) { tries -= 20; won = 1; }
            for (int round = 1; round < 11; round++)
            {
                if (round <= failed.Length / 2 || tries == 10)
                {
                    WriteLine(hangman[round - 1]);
                }
                else
                {
                    WriteLine();
                }
            }
            Write("\n\nGess word :  ");
            Write(gessword);
            WriteLine($"            Gesses left : {10 - tries}             Failed letters : {failed}");
            if (won == 0)
            {
                WriteLine("\n\nEnter one letter or enter the gess word (only one try):\n");
            } 
            else
            {
                WriteLine("\n\nGreat! You gessed it after {0} attempts", tries);
            }
        }
    }
}

