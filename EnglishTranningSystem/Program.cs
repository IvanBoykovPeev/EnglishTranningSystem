using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishTranningSystem
{
    public class Program
    {
        private static WordListContext db = new WordListContext();

        public static WordListContext Db
        {
            get
            {
                return db;
            }

            set
            {
                db = value;
            }
        }

        public static void Main(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("bg-BG");
            
            
            int wordIndex = 0;
            string con;
            var listEnglishWord = Db.WordList.Select(x => x.EnglishWord).ToList();
            var listBulgarianhWord = Db.WordList.Select(x => x.BulgarianWord).ToList();
            var word = "";
            var wordGess = "";
            Console.WriteLine("For exit press е.\nFor new word press н.\nFor add word press а\n--------------------------------------------------------------------------");
            do
            {
                word = listEnglishWord[wordIndex];
                wordGess = listBulgarianhWord[wordIndex];
                GuessWord(word, wordGess);
                string showWord = wordGess;
                
                con = Console.ReadLine();
                if (con == "н")
                {                
                    if (wordIndex < listEnglishWord.Count - 1)
                    {
                        wordIndex++;
                    }
                    else
                    {
                        wordIndex = 0;
                    }
                }

                if (con == "а")
                {
                    AddWord(Db);
                    listEnglishWord = Db.WordList.Select(x => x.EnglishWord).ToList();
                    listBulgarianhWord = Db.WordList.Select(x => x.BulgarianWord).ToList();
                }
                if (con == "п")
                {
                    Console.WriteLine("The word is: " + wordGess + "\n---------------------------------");
                }
            } while (con != "е");
            
                Console.WriteLine("Press key any for Exit");
                Console.ReadKey();            
        }
        public static void GuessWord(string word, string wordGess)
        {
            Console.WriteLine(word);
            Console.WriteLine("Guess the word:");
            var anserWord = Console.ReadLine();
            if (anserWord == wordGess)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Wrong!");
            }            
        }

        public static void AddWord(WordListContext db)
        {
            Console.WriteLine("Enter English word");
            string en = Console.ReadLine();
            Console.WriteLine("Enter Bulgarian word");
            string bg = Console.ReadLine();
            var wordAdd = new WordList { EnglishWord = en, BulgarianWord = bg };
            db.WordList.Add(wordAdd);
            db.SaveChanges();
            Console.WriteLine("word added");
        }
    }
}
