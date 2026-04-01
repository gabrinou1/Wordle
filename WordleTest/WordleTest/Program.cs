using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Media;
using System.IO;


namespace WordleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameIsRunning = true;
            //// Choisir un mot parmis le tableau de mots, et le decomposer en single character table.
            ///
            while (gameIsRunning)
            {
                string selectedWord = GetRandomWord();
                Console.WriteLine("");
                Console.WriteLine("");


                // Debut du jeu
                bool success = false;
                Console.WriteLine("Bienvenue à mon Wordle!");
                string userInput = "";
                string[] essais = new string[5];

                //Obliger le joueur a ecrire un mot de 5 lettres
                do
                {
                    Console.WriteLine("Veuillez écrire un mot de 5 lettres");
                    userInput = Console.ReadLine();
                } while (userInput.Length != 5);


                // Maximum d essai a suivre
                int essai = 0;
                while (essai < 6 && userInput != selectedWord)
                {
                    if (userInput != selectedWord)
                    {
                        essais[essai] = userInput;
                        CompareTwoWords(selectedWord, userInput);
                        do
                        {
                            userInput = Console.ReadLine();
                        } while (WordIsntValid(userInput));

                        for (int i = 0; i <= essai; i++)
                        {
                            WriteGuesses(selectedWord, essais[i]);
                        }

                        essai++;
                    }

                    success = true;
                }

                string victorySoundPath = "Resources/Victory.wav";
                SoundPlayer victorySound = new SoundPlayer(victorySoundPath);

                if (success)
                {
                    CompareTwoWords(selectedWord, userInput);
                    Console.WriteLine("Félicitation!");
                    Console.WriteLine($"Le mot était {selectedWord}");
                    victorySound.Play();
                }
                else
                {
                    Console.WriteLine("Vous avez échouée!");
                }
                Console.WriteLine("Voulez vous jouer une autre partie [O] Oui / [N] Non?");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.O)
                {
                    Console.WriteLine("Nouvelle Partie!");

                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    Console.WriteLine("ByeBye!");
                    gameIsRunning = false;
                }
                else
                {
                    keyInfo = Console.ReadKey();
                }
            }
           

           

            Console.ReadLine();
        }

        private static Random random = new Random();

        public static bool WordIsntValid(string userInput)
        {
            if (userInput.Length != 5)
            {
                Console.WriteLine("Veuillez ecrire un mot de 5 lettres");
                return true;
            }
            else
            {
                return false;
            }

        }



        public static void CompareTwoWords(string answer, string userWord)
        {

            string greenSoundPath = "Resources/GreenSound.wav";
            string whiteSoundPath = "Resources/WhiteSound.wav";
            string yellowSoundPath = "Resources/YellowSound.wav";

            SoundPlayer greenSound = new SoundPlayer(greenSoundPath);
            SoundPlayer whiteSound = new SoundPlayer(whiteSoundPath);
            SoundPlayer yellowSound = new SoundPlayer(yellowSoundPath);

            string wordToGuess = answer;

            IDictionary<char, int> dictionary = new Dictionary<char, int>();

            foreach (char i in answer)
            {
                if (dictionary.ContainsKey(i))
                {
                    dictionary[i]++;
                }
                else
                {
                    dictionary.Add(i, 1);
                }
            }

            // Prints the Dictionary Value
            //foreach (var kvp in dictionary)
            //{
            //    Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            //}

            for (int letter = 0; letter <= userWord.Length - 1; letter++)
            {
                char guessChar = userWord[letter];
                if (guessChar == wordToGuess[letter])
                {
                    dictionary[guessChar]--;
                }
            }


            for (int letter = 0; letter <= userWord.Length - 1; letter++)
            {
                char guessChar = userWord[letter];
                if (guessChar == wordToGuess[letter])
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    greenSound.Play();
                    Console.Write(guessChar);
                    dictionary[guessChar]--;

                }
                else if (wordToGuess.Contains(guessChar) && (dictionary[guessChar] > 0))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    yellowSound.Play();
                    Console.Write(guessChar);
                    dictionary[guessChar]--;
                }
                else
                {
                    whiteSound.Play();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(guessChar);
                }
                Thread.Sleep(500);

            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteGuesses(string answer, string userWord)
        {

            string wordToGuess = answer;

            IDictionary<char, int> dictionary = new Dictionary<char, int>();

            foreach (char i in answer)
            {
                if (dictionary.ContainsKey(i))
                {
                    dictionary[i]++;
                }
                else
                {
                    dictionary.Add(i, 1);
                }
            }

            for (int letter = 0; letter <= userWord.Length - 1; letter++)
            {
                char guessChar = userWord[letter];
                if (guessChar == wordToGuess[letter])
                {
                    dictionary[guessChar]--;
                }
            }

            for (int letter = 0; letter <= userWord.Length - 1; letter++)
            {
                char guessChar = userWord[letter];
                if (guessChar == wordToGuess[letter])
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(guessChar);
                    dictionary[guessChar]--;

                }
                else if (wordToGuess.Contains(guessChar) && (dictionary[guessChar] > 0))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(guessChar);
                    dictionary[guessChar]--;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(guessChar);
                }

            }

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static string GetRandomWord()
        {

            string[] possibleWords = {
"aimer","aider","aigle","avion","amour","angle","arbre","asile","atlas","audio","autre","avant","bague","balai","banal","banjo","barbe","basse",
"bazar","beige","belle","berge","biais","bijou","blanc","blond","boire","boite","borne","bruit","brume","cable","cache","cadre","calme",
"canal","canne","carte","cause","celle","champ","chant","chaud","chien","choix","chose","chute","cible","clair","clefs","clown","coeur","colle",
"comme","conte","corps","coton","coude","coupe","cours","court","coute","craie","creme","crime","croix","cuire","cycle","danse","debut","debit",
"diner","dinde","doute","droit","duree","eclat","ecran","ecrit","effet","egale","eleve","elite","email","enfer","enjeu","envie","epais","epice",
"exact","exces","extra","fable","faire","faims","femme","ferme","fibre","fiche","fiere","filme","final","fixer","flair","flanc",
"flore","flute","foire","force","forme","forum","foule","franc","frais","froid","fruit","fuite","gagne","garde","gazon","geler","genre","geste","glace","gloire",
"gomme","gorge","grade","grain","grand","grave","guide","habit","haine","halte","heure","huile","hiver","honte","hotel","image",
"imite","index","issue","jalon","jambe","jeter","jeune","jouet","jouir","joute","juive","kilos","lache","lampe","lance","large","laser","laver",
"lecon","legal","leger","levre","ligne","limbe","limon","livre","local","loger","logis","lourd","louer","loupe","lueur","lundi","lutte","magie","maire",
"mains","majus","malin","maman","marin","masse","match","mater","matin","media","melee","mener","merci","merle","metre","meute","micro","mieux",
"mince","miner","minet","moine","moins","monde","monts","moral","motel","moule","moyen","murir","musee","nager","naive","natte","naval","neige","nerfs",
"neuve","noble","noeud","noire","norme","notes","nuage","objet","odeur","offre","ombre","oncle","opere","ordre","oreil","orgue","outil",
"paire","panne","paris","parle","parmi","passe","patte","payer","peine","perle","perte","petit","phase","photo","piece","pieux",
"piler","pince","place","plage","plane","plein","pleur","pluie","poche","poeme","poids","poing","point","poire","pomme","porte","poser","poste",
"poule","pouls","prier","prime","prise","prive","proie","prose","prune","puits","quais","quart","queue","radio","range",
"raser","ratio","rayon","reagi","rebus","recit","regle","reine","relax","repli","repos","reste","rival",
"robot","roche","roman","ronde","route","royal","ruche","rugir",
"sable","sacre","saine","salle","salon","sauce","saute",
"scene","score","seche","seine","selon","semer","serum","seuil",
"seule","sirop","sobre","socle","soeur","solde",
"sorte","souci","soupe","sport","stade","style","sucre","suite","super",
"table","tache","taire","talon","taper","tarif","tarte","texte","theme",
"tiers","tigre","timer","tissu","titre","toile","tombe","tonne","torse","total",
"trace","train","trait","trame","trier","tronc","troue",
"union","usage","utile","vache","vague","valet","valle","valve","varie","vaste",
"venir","vente","verbe","verre","verso","vertu","veste","vieux","vigne","ville",
"virer","virus","viser","visse","vital","vivre","vocal","voici","voile","voire",
"voler","votre","voulu","wagon","zebre","zeste"
};

            int randomIndex = random.Next(0, possibleWords.Length);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            // Devoile la reponse
           // Console.WriteLine(possibleWords[randomIndex]);
            Console.ResetColor();
            return possibleWords[randomIndex];
        }

    }
}

