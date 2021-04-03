using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

       
    }

    //Creacion de clase 
    public class Mydatetimeclass
    {
        //Para poder implementar formatos de hora y fecha
        public DateTime DT { get; set; } = DateTime.Now;
    }
}
