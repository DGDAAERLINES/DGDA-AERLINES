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
using System.Threading;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para Bank_LoadingScreen.xaml
    /// </summary>
    public partial class Bank_LoadingScreen : Window
    {
        public Bank_LoadingScreen()
        {
            InitializeComponent();

            // Llenar barra de carga
            llenarBarra();
        }

        // Llena la barra de carga
        private void llenarBarra()
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value++;
                Thread.Sleep(100);
            }
        }
    }
}
