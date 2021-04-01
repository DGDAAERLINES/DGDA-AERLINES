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
// Agregar los namespaces necesarios para conectarse a SQL Server
using System.Data;
using System.Data.SqlClient;
using System.Configuration;




namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
       
        // Variables miembro
        private SqlConnection sqlConnection;

        // Constructores
        public Reservation()
        {
            InitializeComponent();

            // Realizar la conexión con el servidor de base de datos (SQL Server Express)
            string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            // Llenar el ListBox de zoológicos
            MostrarPais();
            cmbopcion.Items.Add("Solo ida");
            cmbopcion.Items.Add("Ida y regreso");

        }

        private void MostrarPais()
        {
            try
            {
                // El query de consulta a la tabla de la base de datos
                string query = "SELECT * FROM Aerlines.Pais";

                // SqlDataAdapter es una interfaz entre las tablas de la base de datos
                // y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                // Utilizar el SqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto de C# que refleje la estructura de una tabla
                    DataTable tablaPais = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaPais);

                    // ¿Cuál es la información de la tabla (DataTable) que debería desplegarse al usuario?
                    cmbOrigen.DisplayMemberPath = "nombre";
                    cmbDestino.DisplayMemberPath = "nombre";

                    // ¿Qué valor debe ser entregado cuando un elemento del ListBox es seleccionado?
                    cmbOrigen.SelectedValuePath = "idPais";
                    cmbDestino.DisplayMemberPath = "nombre";

                    // ¿Quién es la referencia para los datos del ListBox? (populate)
                    cmbOrigen.ItemsSource = tablaPais.DefaultView;
                    cmbDestino.ItemsSource = tablaPais.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool VerificarValores()
        {
            if (cmbOrigen.SelectedValue == null)

            {
                MessageBox.Show("Por favor selecciona el Origen");
                return false;
            }
            else if (cmbDestino.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona Destino");
                return false;
            }

            return true;
        }
        private bool VerificarValoresCalendar()
        {
            if (dtReturn.SelectedDate < dtdepart.SelectedDate)
            {
                MessageBox.Show("La fecha de salida no puede ser menor que la fecha de llegada");
                return false;
            }
            else if (dtdepart.SelectedDate == null)
            {
                MessageBox.Show("Por favor selecciona la fecha de salida");
                return false;
            }

            return true;
        }

        private void cmbopcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbopcion.SelectedItem.ToString() == "Solo ida")
            {
                cmbDestino.IsEnabled = true;
            }
            else
            {
                cmbDestino.IsEnabled = false;
            }

        }
      
    }
}
