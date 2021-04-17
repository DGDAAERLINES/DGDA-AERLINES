using System;
using System.Windows;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Retornar al formulario de inicio de sesión
            MainWindow iniciarSesion = new MainWindow();
            iniciarSesion.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
             Pasaporte passport = new Pasaporte();
             passport.Show();
             Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Manda el usuario al formulario de Menu
            Reservation Reservacion = new Reservation();
            Reservacion.Show();
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Pago pago = new Pago();
            pago.Show();
            Hide();

        }
    }

    //Creacion de clase 
    public class Mydatetimeclass
    {
        //Para poder implementar formatos de hora y fecha
        public DateTime DT { get; set; } = DateTime.Now;
    }
}
