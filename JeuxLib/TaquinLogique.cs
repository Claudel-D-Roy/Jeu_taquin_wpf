using System;

namespace JeuxLib
{
    /// <summary>
    /// Auteur:        Claudel D. Roy & Mathieu Duval
    /// Decription:    Ensemble de la logique derrière le "Jeu de taquin".
    /// Date:          2022-03-09
    /// </summary>
    public class TaquinLogique
    {
        #region Énumérations
        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description:    Énumération des déplacements possibles pour le joueur (associés au touches "flèches directionnelles").
        /// Date:           2022-03-09
        /// </summary>
        public enum Directions
        {
            BAS,
            HAUT,
            DROITE,
            GAUCHE
        }
        #endregion

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description:    Les champs pour les méthodes dans la classe du "Jeu de taquin". 
        /// Date:           2022-03-09
        /// </summary>
        #region Champs
        private byte[] _abyCoordonneeCaseVide = new byte[2];
        private byte[,] _a2byGrille = null;
        private Random _rdm = null;
        public const byte COLONNES = 4;
        public const byte LIGNES = 5;
        #endregion

        #region Propriétés
        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval 
        /// Description:    Propriété qui permet de calculer le nombre de déplacements effectués lors d'une partie. 
        /// Date:           2022-03-09
        /// </summary>
        public int Deplacements { get; set; }

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval 
        /// Description:    Propriété qui permet de d'obtenir en tout temps, le nombre de déplacements effectués dans la grille de jeu. 
        /// Date:           2022-03-09
        /// </summary>
        public byte[,] Grille { get => _a2byGrille; }

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval 
        /// Description:    Propriété qui permet d'accéder au record du minimum de déplacements en une partie durant la durée de vie du jeu.  
        /// Date:           2022-03-09
        /// </summary>
        public int Record { get; set; }

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval 
        /// Description:    Propriété qui permet d'accéder au objets de type "Options". 
        /// Date:           2022-04-09 
        /// </summary>
        internal Options Options
        {
            get => default;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// auteur :        Claudel D. Roy & Mathieu Duval
        /// Description :   Permet d'effectuer les déplacements de la case "vide" dans la grille de jeu
        ///                 ainsi que l'incrémentation du nombre total de déplacements durant une partie.
        /// Date:           2022-03-07
        /// </summary>
        /// <param name="direction">Une des 4 directions possible pour le joueur.</param>
        public void DeplacerVers(Directions direction)
        {
            // Variables locales.
            int iDirectionX = 0;
            int iDirectionY = 0;

            if (direction == Directions.BAS)
            {
                iDirectionX = _abyCoordonneeCaseVide[0] + 1;
                iDirectionY = _abyCoordonneeCaseVide[1];
                Deplacements++;

                if (iDirectionX > _a2byGrille.GetLength(0) - 1)
                {
                    iDirectionX = _abyCoordonneeCaseVide[0];
                    Deplacements--;
                }
            }
            else if (direction == Directions.HAUT)
            {
                iDirectionX = _abyCoordonneeCaseVide[0] - 1;
                iDirectionY = _abyCoordonneeCaseVide[1];
                Deplacements++;

                if (iDirectionX < 0)
                {
                    iDirectionX = _abyCoordonneeCaseVide[0];
                    Deplacements--;
                }
            }
            else if (direction == Directions.DROITE)
            {
                iDirectionX = _abyCoordonneeCaseVide[0];
                iDirectionY = _abyCoordonneeCaseVide[1] + 1;
                Deplacements++;

                if (iDirectionY > _a2byGrille.GetLength(1) - 1)
                {
                    iDirectionY = _abyCoordonneeCaseVide[1];
                    Deplacements--;
                }
            }
            else if (direction == Directions.GAUCHE)
            {
                iDirectionX = _abyCoordonneeCaseVide[0];
                iDirectionY = _abyCoordonneeCaseVide[1] - 1;
                Deplacements++;

                if (iDirectionY < 0)
                {
                    iDirectionY = _abyCoordonneeCaseVide[1];
                    Deplacements--;
                }
            }

            Permuter(_abyCoordonneeCaseVide[0], _abyCoordonneeCaseVide[1], iDirectionX, iDirectionY);
            _abyCoordonneeCaseVide[0] = (byte)iDirectionX;
            _abyCoordonneeCaseVide[1] = (byte)iDirectionY;
        }

        /// <summary>
        /// Auteur :        Claudel D. Roy & Mathieu Duval
        /// Description :   Permet de vérifier et de confirmer si la grille est résolue ou non.
        /// Date:           2022-03-07
        /// </summary>
        /// <returns>Valeur vrai ou faux pour confirmer (ou non) la résolution de la grille de jeu.</returns>
        public bool DeterminerGrilleResolue()
        {
            // Variables locales.
            byte byFaux = 0;
            byte byIndex = 1;

            // Comparaison des valeurs en cellules de la grille "courante" avec une grille "résolue".
            for (int i = 0; i < _a2byGrille.GetLength(0); i++)
            {
                for (int j = 0; j < _a2byGrille.GetLength(1); j++)
                {
                    if (_a2byGrille[i, j] != byIndex)
                        byFaux++;

                    byIndex++;
                }
            }

            // Confirmation (ou non) d'une grille résolue.
            if (byFaux == 0)
            {
                if (Deplacements < Record || Record == 0)
                    Record = Deplacements;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// auteur :        Claudel D. Roy & Mathieu Duval
        /// Description :   Permet de créer la grille de jeu initiale ainsi que du placement semi-aléatoire
        ///                 des valeurs (par permutations).
        /// Date:           2022-02-28
        /// </summary>
        public void InitialiserGrille()
        {
            // Variables locales.
            _rdm = new Random();
            _a2byGrille = new byte[LIGNES, COLONNES];
            byte byIndex = 1;
            Deplacements = 0;

            // Remplissage initial de la grille de jeu (valeurs numériques consécutives + case vide).
            for (int i = 0; i < _a2byGrille.GetLength(0); i++)
            {
                for (int j = 0; j < _a2byGrille.GetLength(1); j++)
                {
                    _a2byGrille[i, j] = byIndex;
                    byIndex++;

                    if (_a2byGrille[i, j] == _a2byGrille.GetLength(0) * _a2byGrille.GetLength(1))
                    {
                        _abyCoordonneeCaseVide[0] = (byte)i;
                        _abyCoordonneeCaseVide[1] = (byte)j;
                    }
                }
            }

            // Logique pour la permutation des valeurs dans la grille de jeu.
            for (int i = 0; i <= 500; i++)
            {
                Directions direction = (Directions)(_rdm.Next(0, 4));
                DeplacerVers(direction);
                Deplacements = 0;
            }
        }

        /// <summary>
        /// Auteur:         Claudel D. Roy & Mathieu Duval
        /// Description :   Permet d'effectuer la permutation des valeurs entre deux cellules (les "déplacements") de la grille de jeu .
        /// Date:           2022-02-28
        /// </summary>
        /// <param name="iAx">Coordonnée sur l'axe de X (position initiale).</param>
        /// <param name="iAy">Coordonnée sur l'axe de Y (position initiale).</param>
        /// <param name="iBx">Coordonnée sur l'axe de X (position finale).</param>
        /// <param name="iBy">Coordonnée sur l'axe de Y (position finale).</param>
        private void Permuter(int iAx, int iAy, int iBx, int iBy)
        {
            int iValeurTemp = _a2byGrille[iAx, iAy];
            _a2byGrille[iAx, iAy] = _a2byGrille[iBx, iBy];
            _a2byGrille[iBx, iBy] = (byte)iValeurTemp;
        }

        /// <summary>
        /// Auteur :        Claudel D. Roy & Mathieu Duval
        /// Description :   Permet de résoudre automatiquement la grille de jeu. 
        /// Date :          2022-03-05
        /// </summary>
        public void Resoudre()
        {
            // Variables locales.
            byte byIndex = 1;

            for (int i = 0; i < _a2byGrille.GetLength(0); i++)
            {
                for (int j = 0; j < _a2byGrille.GetLength(1); j++)
                {
                    _a2byGrille[i, j] = byIndex;
                    byIndex++;

                    if (_a2byGrille[i, j] == _a2byGrille.GetLength(0) * _a2byGrille.GetLength(1))
                    {
                        _abyCoordonneeCaseVide[0] = (byte)i;
                        _abyCoordonneeCaseVide[1] = (byte)j;
                    }
                }
            }
        }
        #endregion
    }
}