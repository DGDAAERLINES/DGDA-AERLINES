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
            try
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult result = MessageBox.Show("You can issue the passport if you have already made your payment at the Bank", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Mostrar formulario de Emision de pasaporte
                    // iniciarSesion = new MainWindow();
                    //iniciarSesion.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("You should first pay");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("You should first pay");
                Console.WriteLine(ex.Message);
            }
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
    }

    //Creacion de clase 
    public class Mydatetimeclass
    {
        //Para poder implementar formatos de hora y fecha
        public DateTime DT { get; set; } = DateTime.Now;
    }
}
