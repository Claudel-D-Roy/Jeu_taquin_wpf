using JeuTaquinWPF.Views;
using JeuxLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuTaquinWPF
{
    /// <summary>
    /// Auteur: Claudel D. Roy & Mathieu Duval
    /// Description: Interaction logique pour MainWindow.xaml
    /// Date: 2022-04-02
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Champs
        private TaquinLogique _jeu = new TaquinLogique();
        private Options _options = new Options();
        private Label[,] _planche = new Label[5, 4];
        private byte[,] _grilleResolue = new byte[5, 4]
        {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9, 10, 11, 12},
            {13, 14, 15, 16},
            {17, 18, 19, 20}
        };
        #endregion

        #region Constantes
        const string _URIImages = "pack://application:,,,/Images/";
        const string _URISons = "pack://siteoforigin:,,,/Sons/";
        #endregion

        #region Méthodes
        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet de préparer la grille de jeu pour accepter les valeurs à y insérer.
        /// Date: 2022-04-02
        /// </summary>
        private void RemplirTableauPlanche()
        {
            //Variables locales
            int iColonne = 0;
            int iLigne = 0;

            for (int iPos = 0; iPos < grdGrilleJeu.Children.Count; iPos++)
            {
                iLigne = Grid.GetRow(grdGrilleJeu.Children[iPos]);
                iColonne = Grid.GetColumn(grdGrilleJeu.Children[iPos]);
                _planche[iLigne, iColonne] = (Label)grdGrilleJeu.Children[iPos];
            }
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet de remplir la grille de jeu avec les valeurs utilisées.
        /// Date: 2022-04-02 
        /// </summary>
        private void RemplirGrillePlanche()
        {
            for (int iLigne = 0; iLigne < _planche.GetLength(0); iLigne++)
            {
                for (int iColonne = 0; iColonne < _planche.GetLength(1); iColonne++)
                {
                    _planche[iLigne, iColonne].Content = _jeu.Grille[iLigne, iColonne];

                    if (_jeu.Grille[iLigne, iColonne] < 20)
                    {
                        // Remplis les cellules avec des sections d'images si le mode de jeu "Images" est activé.
                        if (_options.Images)
                        {
                            _planche[iLigne, iColonne].Content = " ";
                            _planche[iLigne, iColonne].Background = ChangerPourImage(_jeu.Grille[iLigne, iColonne]);
                        }
                        // Assigne une couleur de fond verte si la valeur est bien placée lorsque le mode de jeu "FondCouleur" est activé.
                        else if (_options.FondCouleur)
                        {

                            if ((byte)(_planche[iLigne, iColonne].Content) == _grilleResolue[iLigne, iColonne])
                                _planche[iLigne, iColonne].Background = Brushes.LightGreen;
                            else
                            {
                                _planche[iLigne, iColonne].Content = _jeu.Grille[iLigne, iColonne];
                                _planche[iLigne, iColonne].Background = Brushes.White;
                            }
                        }
                        else
                        {
                            _planche[iLigne, iColonne].Content = _jeu.Grille[iLigne, iColonne];
                            _planche[iLigne, iColonne].Background = Brushes.White;
                        }
                    }
                    // Assigne une cellule de couleur grise à la case "20" qui sert de pion.
                    else if (_jeu.Grille[iLigne, iColonne] == 20)
                    {
                        _planche[iLigne, iColonne].Content = " ";
                        _planche[iLigne, iColonne].Background = Brushes.Gray;
                    }
                }
            }
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet d'afficher un court message et d'émettre une sonnerie lorsque la partie est gagnée.
        /// Date: 2022-04-09 
        /// </summary>
        private void ValiderSiGagnant()
        {
            // Variables locales
            MediaPlayer lecteurMP3 = new MediaPlayer(); ;

            if (_jeu.DeterminerGrilleResolue())
            {
                lblTrouvé.Visibility = Visibility.Visible;
                _jeu.Record = _jeu.Deplacements;
                txtRecord.Text = _jeu.Record.ToString();

                // Déclenche une sonnerie si la partie est gagnée.
                if (_options.Son)
                {
                    lecteurMP3.Open(new Uri(_URISons + "Success.mp3"));
                    lecteurMP3.Play();
                }
            }
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Prends la valeur numérique d'une case et la change pour son équivalent graphique en mode "Image"
        /// Date: 2022-04-09 
        /// </summary>
        /// <param name="iValeur">La valeur de la case</param>
        /// <returns></returns>
        private Brush ChangerPourImage(int iValeur)
        {
            Brush CaseImage = null;

            // Association nombre-image se fait ici.
            switch (iValeur)
            {
                case 1:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj01.jpg")));
                    break;
                case 2:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj02.jpg")));
                    break;
                case 3:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj03.jpg")));
                    break;
                case 4:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj04.jpg")));
                    break;
                case 5:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj05.jpg")));
                    break;
                case 6:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj06.jpg")));
                    break;
                case 7:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj07.jpg")));
                    break;
                case 8:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj08.jpg")));
                    break;
                case 9:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj09.jpg")));
                    break;
                case 10:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj10.jpg")));
                    break;
                case 11:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj11.jpg")));
                    break;
                case 12:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj12.jpg")));
                    break;
                case 13:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj13.jpg")));
                    break;
                case 14:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj14.jpg")));
                    break;
                case 15:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj15.jpg")));
                    break;
                case 16:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj16.jpg")));
                    break;
                case 17:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj17.jpg")));
                    break;
                case 18:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj18.jpg")));
                    break;
                case 19:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj19.jpg")));
                    break;
                case 20:
                    CaseImage = new ImageBrush(new BitmapImage(new Uri(_URIImages + "sj20.jpg")));
                    break;
                default:
                    break;
            }
            return CaseImage;
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Série de méthodes permettant l'initialistion du jeu dans une fenêtre WPF
        /// Date: 2022-04-02 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            grdDroiteOptions.DataContext = _options;

            RemplirTableauPlanche();
            _jeu.InitialiserGrille();
            RemplirGrillePlanche();

            txtEnCours.Text = "0";

            if (txtRecord.Text != "")
                txtRecord.Text = _jeu.Record.ToString();
            else
                txtRecord.Text = "Aucun";
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur le bouton RÉSOUDRE.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRésoudre_Click(object sender, RoutedEventArgs e)
        {
            _jeu.Resoudre();
            ValiderSiGagnant();
            RemplirGrillePlanche();
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur le bouton RECOMMENCER.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btsRecommencer_Click(object sender, RoutedEventArgs e)
        {
            _jeu.InitialiserGrille();
            lblTrouvé.Visibility = Visibility.Hidden;
            RemplirGrillePlanche();
            txtEnCours.Text = "0";
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur le bouton PARAMÈTRES.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParametres_Click(object sender, RoutedEventArgs e)
        {
            // Variables locales                   
            frmParametres frmPara = new frmParametres();

            frmPara.Owner = this;
            frmPara.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            frmPara.grdMain.DataContext = _options;

            bool? bRetour = frmPara.ShowDialog();

            RemplirGrillePlanche();
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur la flèche GAUCHE.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _jeu.DeplacerVers(TaquinLogique.Directions.GAUCHE);
            ValiderSiGagnant();
            RemplirGrillePlanche();
            txtEnCours.Text = _jeu.Deplacements.ToString();
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur la flèche DROITE.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _jeu.DeplacerVers(TaquinLogique.Directions.DROITE);
            ValiderSiGagnant();
            RemplirGrillePlanche();
            txtEnCours.Text = _jeu.Deplacements.ToString();
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur la flèche HAUT.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgUp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _jeu.DeplacerVers(TaquinLogique.Directions.HAUT);
            ValiderSiGagnant();
            RemplirGrillePlanche();
            txtEnCours.Text = _jeu.Deplacements.ToString();
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet la gestion des actions à effectuer lors d'un clique de souris sur la flèche BAS.
        /// Date: 2022-04-02 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgDown_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _jeu.DeplacerVers(TaquinLogique.Directions.BAS);
            ValiderSiGagnant();
            RemplirGrillePlanche();
            txtEnCours.Text = _jeu.Deplacements.ToString();
        }
        #endregion
    }
}
