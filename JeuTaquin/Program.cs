using JeuxLib;
using System;

namespace JeuTaquinConsole
{
    /// <summary>
    /// Auteur:         Claudel D. Roy & Mathieu Duval 
    /// Description:    - Algorithme pour jouer au "JEU DU TAQUIN" (qui consiste à remettre en ordre une série de nombres mélangés dans une grille.
    ///                 - Un menu persistant sous la grille, pour afficher les options disponibles ("(R)ecommencer", "Ré(S)oudre" et "(Q)uitter").
    ///                 - Un compteur de déplacements et l'enregistrement du record (pour le minimum de déplacements effectués) sont aussi affichés.
    /// Date:           2022-03-09
    /// </summary>
    class Program
    {
        #region Champs
        static TaquinLogique _jeu = new TaquinLogique();
        #endregion

        #region Methodes
        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description:    Permet d'afficher la grille de jeu tout au long de la partie.
        /// Date:           2022-02-23
        /// </summary>
        static void AfficherGrille()
        {
            Console.Clear();

            // Suite de méthodes pour afficher la grille de jeu (contenant et contenu).
            CreerBordureSuperieure();
            Console.WriteLine(" Déplacements: " + _jeu.Deplacements);

            for (int i = 0; i < _jeu.Grille.GetLength(0); i++)
            {
                for (int j = 0; j < _jeu.Grille.GetLength(1); j++)
                {
                    if (_jeu.Grille[i, j] == _jeu.Grille.GetLength(0) * _jeu.Grille.GetLength(1))
                    {
                        Console.Write("║ ");

                        // Affiche le contenu de la case "vide".
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("██ ");
                        Console.ResetColor();
                    }
                    else if (_jeu.Grille[i, j] < 10)
                        Console.Write("║  " + _jeu.Grille[i, j] + " ");
                    else
                        Console.Write("║ " + _jeu.Grille[i, j] + " ");
                }

                if (i == 0 && _jeu.Record == 0)
                    Console.WriteLine("║ Record......: Aucun");
                else if (i == 0)
                    Console.WriteLine("║ Record......: " + _jeu.Record);
                else
                    Console.WriteLine("║");

                if (i < _jeu.Grille.GetLength(0) - 1)
                    CreerBordureHorizontales();
            }
            CreerBordureInferieure();

            // Méthodes locales utilisées pour afficher la grille de jeu dans le code ci-haut.

            /// <summary>
            /// Auteur:         Claudel D. Roy & Mathieu Duval
            /// Description:    Affiche la bordure SUPÉRIEURE du tableau de jeu.
            /// Date:           2022-02-23
            /// </summary>
            static void CreerBordureSuperieure()
            {
                Console.Write("╔");

                for (int i = 0; i < 4; i++)
                    Console.Write("═");

                for (int i = 0; i < _jeu.Grille.GetLength(1) - 1; i++)
                {
                    Console.Write("╦");

                    for (int j = 0; j < 4; j++)
                        Console.Write("═");
                }
                Console.Write("╗");
            }

            /// <summary>
            /// Auteur:          Claudel D. Roy & Mathieu Duval
            /// Description:     Affiche la bordure INFÉRIEURE du tableau de jeu.
            /// Date:            2022-02-23
            /// </summary>
            static void CreerBordureInferieure()
            {
                Console.Write("╚");

                for (int i = 0; i < 4; i++)
                    Console.Write("═");

                for (int i = 0; i < _jeu.Grille.GetLength(1) - 1; i++)
                {
                    Console.Write("╩");

                    for (int j = 0; j < 4; j++)
                        Console.Write("═");
                }
                Console.WriteLine("╝");
            }

            /// <summary>
            /// Auteur:         Claudel D. Roy & Mathieu Duval
            /// Description:    Affiche les bordures HORIZONTALES entre chaque ligne de cellules de la grille de jeu.
            /// Date:           2022-02-23
            /// </summary>
            static void CreerBordureHorizontales()
            {
                Console.Write("╠");

                for (int i = 0; i < 4; i++)
                    Console.Write("═");

                for (int i = 0; i < _jeu.Grille.GetLength(1) - 1; i++)
                {
                    Console.Write("╬");

                    for (int j = 0; j < 4; j++)
                        Console.Write("═");
                }
                Console.WriteLine("╣");
            }
        }

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description:    Affiche les options de touches disponibles au joueur.
        /// Date:           2022-03-01
        /// </summary>
        /// <param name="sMessage">Message contenant les options de touches disponibles au joueur.</param>
        /// <param name="bPause">Un booléen pour vérifier si le jeu est en mode "pause".</param>
        static void AfficherMessage(string sMessage, bool bPause)
        {
            Console.Write(sMessage);
        }
        #endregion

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description:    Programme qui permet le déroulement d'une partie de Jeu de Taquin sur console. pp
        /// Date:           2022-02-23
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Variables locales.
            ConsoleKeyInfo Touche;
            bool bPause = false;
            string sMessage = "Flèches pour déplacer.\n" +
                              "Q=Quitter\n" +
                              "R=Recommencer\n" +
                              "S=Résoudre\n" +
                              "Direction: ";

            do
            {
                // Appels aux méthodes pour le déroulement du jeu.
                _jeu.InitialiserGrille();
                AfficherGrille();
                AfficherMessage(sMessage, bPause);

                // Déplacements dans la grille.
                do
                {
                    Touche = Console.ReadKey(true);

                    if (Touche.Key == ConsoleKey.UpArrow)
                        _jeu.DeplacerVers(TaquinLogique.Directions.HAUT);
                    else if (Touche.Key == ConsoleKey.DownArrow)
                        _jeu.DeplacerVers(TaquinLogique.Directions.BAS);
                    else if (Touche.Key == ConsoleKey.LeftArrow)
                        _jeu.DeplacerVers(TaquinLogique.Directions.GAUCHE);
                    else if (Touche.Key == ConsoleKey.RightArrow)
                        _jeu.DeplacerVers(TaquinLogique.Directions.DROITE);
                    else if (Touche.Key == ConsoleKey.R)
                    {
                        Console.WriteLine("\nMélanger la grille!");
                        Console.WriteLine("APPUYEZ SUR UNE TOUCHE...");

                        _jeu.InitialiserGrille();

                        Touche = Console.ReadKey(true);
                    }
                    else if (Touche.Key == ConsoleKey.S)
                    {
                        _jeu.Resoudre();
                        AfficherGrille();
                        AfficherMessage(sMessage, bPause);

                        Console.WriteLine("\nRésoudre la grille!");
                        Console.WriteLine("APPUYEZ SUR UNE TOUCHE...");
                        Console.ReadKey();
                    }
                    else if (Touche.Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("\n\nAu revoir!");
                        break;
                    }

                    bPause = _jeu.DeterminerGrilleResolue();

                    AfficherGrille();

                    if (bPause == true)
                    {
                        if (Touche.Key == ConsoleKey.S)
                            break;

                        Console.WriteLine("BRAVO! Vous avez réussi! Q = Quitter, autres touches = Recommencer");
                        Touche = Console.ReadKey(true);

                        if (Touche.Key == ConsoleKey.Q)
                        {
                            Console.WriteLine("\nAu revoir!");
                            break;
                        }
                    }

                    AfficherMessage(sMessage, bPause);

                } while (!bPause == true);

            } while (Touche.Key != ConsoleKey.Q);
        }
    }
}
