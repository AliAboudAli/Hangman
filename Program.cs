using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Galje;

class Program
{
    static int tries = 10;
    private static List<string> words = new List<string> { "baseball", "corruption", "xylofoon", "encapsulation" };
    static string playerName;
    static string guess;
    static string chosen;
    private static bool gameWon;
    private static List<Hangman> letters = new List<Hangman>();

    public struct Hangman
    {
        public char letter;
        public bool isinWord;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to hangman!");
        Console.Clear();                
        Random rand =new Random();
        chosen = words[rand.Next(0, words.Count)];
        Console.WriteLine("voer uw naam in: ");
        playerName = Console.ReadLine();
        Console.Clear();
        Play();
    }

    static void Play()
    {
        //zo lang de woord niet is geraden is of tries is 0 
        while (tries > 0 && gameWon == false)
            //voer een raadactie
        {
            Guess();
        }
    }

    static void Guess()
    {
        Display();
        //letter invoer uitlezen
        Console.WriteLine("Raad een letter of een hele woord");
        guess = Console.ReadLine().ToLower();
        //controleren of input valid is -alleen letter
        foreach (char c in guess)
        {
            if (!char.IsLetter(c))
            {
                Console.WriteLine("Geef een geldige tekens of letter in");
                return;
            }
        }

        //controleren of het een woord of letter is
        if (guess.Length == 1)
        {
            //is een letter
            //check of letter is eerder geraden
            foreach (Hangman h in letters)
            {
                if (h.letter == guess[0])
                {
                    //letter al eerder geraden
                    return;
                }
            }
            //Check of a letter in woord zit
            if (letterInword(guess[0], chosen))
            {
                letters.Add(new Hangman { letter = guess[0], isinWord = true });
            }
            else
            {
                letters.Add(new Hangman { letter = guess[0], isinWord = false });
                tries--;
            }
        }
        else if (guess.Length > 1)
        {
            if (string.Compare(guess, chosen) == 0)
            {
                //is een woord
                gameWon = true;
                return;
            }
        }
        else
        {
            Console.WriteLine("geen corrrecte invoer, doe opnieuw");
            return;
        }

        if (WordComplete())
        {
            gameWon = true;
            return;
        }

        static void Display()
        {
            Console.WriteLine(playerName);
            Console.WriteLine("Attempts :" + tries);
            foreach (char c in chosen)
            {
                char displayLetter = '_';
                foreach (Hangman h in letters)
                {
                    if (h.letter == c)
                    {
                        displayLetter = h.letter;
                    }
                }
                Console.Write(displayLetter);
            }
            Console.WriteLine();
            Console.WriteLine("Guessed letter");
            foreach (Hangman h in letters)
            {
                if (h.isinWord == false)
                {
                    Console.Write(h.letter + " ");
                }
            }
            
        }
        //controleren of woord is geraden
        //Woord letters tonen
        static bool WordComplete()
        {
            int unqiueLetters = chosen.Distinct().Count();
            foreach (Hangman h in letters)
            {
                if (h.isinWord)
                {
                    unqiueLetters--;
                }
            }
            if (unqiueLetters == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static bool letterInword(char letter, string word)
    {
        foreach (char c in word)
        {
            if (c == letter)
            {
                return  true;
            }
        }
        return false;
    }
}