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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Pasajeros;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para CRUD_Pasajeros.xaml
    /// </summary>
    public partial class CRUD_Pasajeros : Window
    {
        private Conexion conexion = new Conexion();
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        public CRUD_Pasajeros()
        {
            InitializeComponent();
            ObtenerValores();
        }

        private void ObtenerValoresFormulario()
        {
            conexion.Nombre = txtNombre.Text;
            conexion.Apellido = txtApellido.Text;
            conexion.IdPais = Convert.ToInt32(txtIdPais.Text);
            conexion.Sexo = txtSexo.Text;
            conexion.Edad = txtEdad.Text;
            conexion.Telefono = txtTelefono.Text;
        }

        private void ObtenerValoresModificacion()
        {
            txtNombre.Text = "Vacio";
            txtApellido.Text = "Vacio";
            txtIdPais.Text = "0";
            txtSexo.Text = "N";
            txtEdad.Text = "ND";
            txtTelefono.Text = "Vacio";
            conexion.Nombre = txtNombre.Text;
            conexion.Apellido = txtApellido.Text;
            conexion.IdPais = Convert.ToInt32(txtIdPais.Text);
            conexion.Sexo = txtSexo.Text;
            conexion.Edad = txtEdad.Text;
            conexion.Telefono = txtTelefono.Text;
        }
        private void LimpiarFormulario()
        {
            txtIdPasajero.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtIdPais.Text = string.Empty;
            txtSexo.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        private bool VerificarValores()
        {
            if (txtIdPasajero.Text == string.Empty || txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtIdPais.Text == string.Empty
                || txtSexo.Text == string.Empty || txtEdad.Text == string.Empty || txtTelefono.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            return true;
        }
        private void ObtenerValores()
        {
            SqlCommand sqlcommand = new SqlCommand("select * from Aerlines.Pasajero", sqlConnection);
            DataTable dt = new DataTable();
            sqlConnection.Open();
            SqlDataReader sdr = sqlcommand.ExecuteReader();
            dt.Load(sdr);
            sqlConnection.Close();
            DGPasajeros.ItemsSource = dt.DefaultView;
        }

        private void btnVerPasajeros_Click(object sender, RoutedEventArgs e)
        {
            ObtenerValores();
        }

        private void btnAgregarPasajeros_Click(object sender, RoutedEventArgs e)
        {

            if (VerificarValores())
            {
                try
                {
                    ObtenerValoresFormulario();

                    // Insertar los datos de la habitación
                    conexion.InsertarPasajero(conexion);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos insertados correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar al pasajero.");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarFormulario();
                }
            }
        }

        private void btnModificarPasajeros_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {

                    // Llenar los valores del formulario
                    ObtenerValoresFormulario();

                    conexion.ModificarPasajero(conexion);

                    MessageBox.Show("¡Datos modificados correctamente!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de modificar al pasajero.");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    LimpiarFormulario();
                }
            }
        }

        private void btnEliminarPasajeros_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txtIdPasajero.Text == null)
                    MessageBox.Show("Por favor escribe el ID un pasajero desde el listado");
                else
                {
                    // Mostrar un mensaje de confirmación
                    MessageBoxResult result = MessageBox.Show("¿Deseas eliminar el pasajero? Ingrese el ID para eliminar.", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Eliminar la habitación
                        ObtenerValoresModificacion();
                        conexion.EliminarPasajero(Convert.ToInt32(txtIdPasajero.Text), txtNombre.Text, txtApellido.Text,
                            Convert.ToInt32(txtIdPais.Text), txtSexo.Text, txtEdad.Text, txtTelefono.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de eliminar al pasajero...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el listbox de Pasajeros
                ObtenerValores();
                LimpiarFormulario();
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Reservation reserva = new Reservation();
            reserva.Show();
            Close();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Pago pagar = new Pago();
            pagar.Show();
            Close();
        }
    }
}
