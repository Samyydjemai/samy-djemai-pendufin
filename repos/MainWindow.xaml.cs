using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DJEMAI_SAMY_PENDU
{
    public partial class MainWindow : Window
    {
        string motADeviner;
        char[] motMasque;
        int vie = 7; // Début à 7 vies
        List<string> mots;

        public MainWindow()
        {
            InitializeComponent();
            ChargerMotsDepuisFichier();
            ChoisirMot();
            AfficherMotMasque();

            // Afficher l'image pour 7 vies au démarrage
            img_Vie.Source = new BitmapImage(new Uri("Images/image_vie_7.png", UriKind.Relative));
        }

        private void ChargerMotsDepuisFichier()
        {
            try
            {
                mots = new List<string>(File.ReadAllLines(@"C:\Users\SAMY\source\repos\mots.txt"));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Le fichier mots.txt est introuvable");
            }
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string lettre = button.Content.ToString().ToLower();

            if (motADeviner.Contains(lettre))
            {
                for (int i = 0; i < motADeviner.Length; i++)
                {
                    if (motADeviner[i].ToString() == lettre)
                    {
                        motMasque[i] = motADeviner[i];
                    }
                }

                txt_Display.Text = new string(motMasque);

                if (motADeviner == txt_Display.Text)
                {
                    MessageBox.Show("Bravo !");
                }
            }
            else
            {
                vie--;
                if (vie < 0) vie = 0; // Pour éviter de sortir des limites

                // Mettre à jour l'image affichée
                string imagePath = $"Images/image_vie_{vie}.png"; // Chemin de l'image
                img_Vie.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                if (vie == 0)
                {
                    MessageBox.Show("Perdu ! Le mot à deviner était " + motADeviner);
                    ReinitialiserJeu();
                }
            }
        }

        private void ReinitialiserJeu()
        {
            ChoisirMot();
            AfficherMotMasque();
            vie = 7;

            // Réinitialiser l'image à celle de 7 vies
            img_Vie.Source = new BitmapImage(new Uri("Images/image_vie_7.png", UriKind.Relative));
        }

        private void ChoisirMot()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, mots.Count);
            motADeviner = mots[index];
            motMasque = new string('*', motADeviner.Length).ToCharArray();
        }

        private void AfficherMotMasque()
        {
            txt_Display.Text = new string(motMasque);
        }
    }
}
