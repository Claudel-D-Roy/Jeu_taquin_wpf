namespace JeuxLib
{
    /// <summary>
    /// Auteur: Claudel D. Roy & Mathieu Duval
    /// Description: Classe contenant la gestion et les informations associées aux options de jeu dans les paramètres (bouton).
    /// Date: 2022-04-02 
    /// </summary>
    public class Options
    {
        #region Propriétés
        /// <summary>
        /// Auteur: Claudel D. Roy et Mathieu Duval
        /// Description: Permet de changer de mode de jeu en la version CHIFFRES
        /// Fate: 2022-04-02
        /// </summary>
        public bool Chiffres { get; set; } = true;

        /// <summary>
        /// Auteur: Claudel D. Roy et Mathieu Duval
        /// Description: Permet de changer le mode de jeu (chiffres seulement) pour changer la couleur d'une case lorsque bien placée..
        /// Fate: 2022-04-02
        /// </summary>
        public bool FondCouleur { get; set; }

        /// <summary>
        /// Auteur: Claudel D. Roy et Mathieu Duval
        /// Description: Permet de changer de mode de jeu en la version IMAGES (Steve Jobs)
        /// Fate: 2022-04-02
        /// </summary>
        public bool Images { get; set; }

        /// <summary>
        /// Auteur: Claudel D. Roy et Mathieu Duval
        /// Description: Permet d'implémenter une sonnerie lorsque la grille est résolue.
        /// Fate: 2022-04-02
        /// </summary>
        public bool Son { get; set; }
        #endregion
    }
}
