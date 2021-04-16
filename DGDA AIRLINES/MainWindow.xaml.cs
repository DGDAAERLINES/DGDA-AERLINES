﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        // Objeto de tipo usuario para implementar su funcionalidad
        private Usuario usuario = new Usuario();
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Implementar la búsqueda del usuario desde la clase Usuario
                Usuario elUsuario = usuario.BuscarUsuario(txtUsername.Text);

                // Verificar si el usuario existe
                if (elUsuario.Username == null)
                    MessageBox.Show("El usuario/contraseña incorrectos");
                else
                {
                    // Verificar que la contraseña ingresada es igual a la contraseña
                    // almacenada en la base de datos
                    if (elUsuario.Password == txtPassword.Password && elUsuario.Estado)
                    {
                        // Mostrar el formulario de menú principal
                        Menu MenuPrincipal = new Menu();
                        MenuPrincipal.Show();
                        Close();
                    }
                    else if (!elUsuario.Estado)
                        MessageBox.Show("usuario innactivo");
                    else
                        MessageBox.Show("El usuario/contraseña incorrecta. Favor verificar.");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ha ocurrido un error al momento de realizar la consulta...");
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
    }
}
