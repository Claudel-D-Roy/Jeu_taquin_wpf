using JeuxLib;
using System.Windows;

namespace JeuTaquinWPF.Views
{
    /// <summary>
    /// Auteur: Claudel D. Roy & Mathieu Duval
    /// Description: Interaction logique pour frmParamètres.
    /// Date: 2022-04-02
    /// </summary>
    public partial class frmParametres : Window
    {
        #region Champs
        private Options _options = new Options();
        #endregion

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Permet l'initialisation de frmParametres.
        /// Date: 2022-04-02 
        /// </summary>
        public frmParametres()
        {
            InitializeComponent();
            grdMain.DataContext = _options;
        }

        /// <summary>
        /// Auteur: Claudel D. Roy & Mathieu Duval
        /// Description: Détermine le(s) évènements lorsque le bouton "OK" se fait cliquer dans la fenêtre des paramètres.
        /// Date: 2022-04-02  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
