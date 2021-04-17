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
    /// Lógica de interacción para Prices.xaml
    /// </summary>
    public partial class Prices : Window
    {
        // Variables miembro
        private Price precio = new Price();
        public int idVuelo;
        private string Destino;
        private double precioEconomy;
        private double precioFirst;
        private double precioEconomyDollars;
        private double precioFirstDollars;
        public Prices(int ID)
        {
            InitializeComponent();

            // Obtener el valor del vuelo
            this.idVuelo = ID;

            try
            {
                // Valida que se haya seleccionado un vuelo
                if (this.idVuelo == 0)
                {
                    MessageBox.Show("¡Error! Flight not found");
                    this.Close();
                }
                else
                {
                    // Mostrar los lugares de origen y destino 
                    LlenarTextBox();
                    // Calcular el precio del vuelo
                    CalcularPrecios();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Llena los textbox de destino y origen con el nombre de los lugares contenidos en la base de datos
        private void LlenarTextBox()
        {
            txtDestino.Text = precio.ObtenerDestino(idVuelo);
            txtOrigen.Text = precio.ObtenerOrigen(idVuelo);
        }

        // Obtiene el precio del vuelo dependiendo de la clase y el destino de este
        private void CalcularPrecios()
        {
            // Obtener el destino para calcular el precio
            Destino = precio.ObtenerDestino(idVuelo);

            // Calcular los precios
            // Honduras
            if (Destino == "Honduras, TGS:Aeropuerto Internacional Toncontín" ||
                Destino == "Honduras, SPS:Aeropuerto Internacional Ramón Villeda Morales" ||
                Destino == "Honduras,Ceiba:Aeropuerto Internacional Goloson")
            {
                precioFirst = 9000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 5000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Estados Unidos
            else if (Destino == "Estados Unidos, Houston:Aeropuerto Intercontinental George Bush" ||
                     Destino == "Estados Unidos, New Jersey:Aeropuerto Internacional Libertad de Newark")
            {
                precioFirst = 24000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 12000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Colombia
            else if (Destino == "Colombia, Medellin:Aeropuerto Olaya Herrera" ||
                     Destino == "Colombia, Cartajena: Aeropuerto Internacional Rafael Núñez")
            {
                precioFirst = 21500;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 16500;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // México
            else if (Destino == "Mexico,Veracruz:Aeropuerto Internacional de Veracruz" ||
                     Destino == "Mexico,Tijuana:Aeropuerto Internacional de Tijuana")
            {
                precioFirst = 32000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 21500;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Panamá
            else if (Destino == "Panama, Tocumen:Aeropuerto Internacional de Tocumen" ||
                     Destino == "Panama, Arraijan:Aeropuerto Internacional Panamá Pacífico")
            {
                precioFirst = 25000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 16000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Corea del Sur
            else if (Destino == "Corea del sur,Incheon:Aeropuerto Internacional de Incheon" ||
                     Destino == "Corea del sur,Seul:Aeropuerto Internacional de Gimpo")
            {
                precioFirst = 88000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 43000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Bolivia
            else if (Destino == "Bolivia,Santa cruz:Aeropuerto Internacional Viru Viru" ||
                     Destino == "Bolivia,Cochabamba:Aeropuerto Internacional Jorge Wilsterman")
            {
                precioFirst = 45000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 27500;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // El Salvador
            else if (Destino == "El salvador,San miguel:Aeropuerto Regional de San Miguel" ||
                     Destino == "El salvador:El Salvador Aeropuerto internacional")
            {
                precioFirst = 15000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 9500;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Canadá
            else if (Destino == "Canada,Toronto:Aeropuerto Toronto City Centre" ||
                     Destino == "Canada,Montreal:Aeropuerto Internacional Pierre Elliott Trudeau")
            {
                precioFirst = 39000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 28000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // Argentina
            else if (Destino == "Argentina, Buenos aires:Aeropuerto Internacional Ezeiza" ||
                     Destino == "Argentina,Mendoza:Aeropuerto Internacional El Plumerillo")
            {
                precioFirst = 45000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 29000;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
            // España
            else if (Destino == "Espana,Madrid:Aeropuerto de Madrid-Barajas Adolfo Suárez" ||
                     Destino == "Espana,Barcelona:Aeropuerto Josep Tarradellas Barcelona-El Prat")
            {
                precioFirst = 40000;
                precioFirstDollars = Math.Round(precioFirst / 24.02);
                precioEconomy = 20500;
                precioEconomyDollars = Math.Round(precioEconomy / 24.02);

                // Mostrar los valores en el formulario
                txtPrice.Text = precioEconomy.ToString();
                txtPriceDollars.Text = precioEconomyDollars.ToString();
                txtPriceF.Text = precioFirst.ToString();
                txtPriceDollarsF.Text = precioFirstDollars.ToString();
            }
        }

        // Almacena en la base de datos la clase de asiento que eligió el usuario
        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtiene los valores del precio
                precio.idClase = 2;
                precio.Precio = Convert.ToDouble(txtPrice.Text);

                // Insertar los datos del precio
                precio.InsertarPrecio(precio);

                MessageBox.Show("Sucessfully price selection!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting this price");
                Console.WriteLine(ex.Message);
            }
        }

        // Almacena en la base de datos la clase de asiento que eligió el usuario
        private void btnPurchaseF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtiene los valores del precio
                precio.idClase = 1;
                precio.Precio = Convert.ToDouble(txtPriceF.Text);

                // Insertar los datos del precio
                precio.InsertarPrecio(precio);

                MessageBox.Show("Sucessfully price selection!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting this price");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            CRUD_Pasajeros Cpasajeros = new CRUD_Pasajeros();
            Cpasajeros.Show();
            Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservaciones = new Reservation();
            reservaciones.Show();
            Close();
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
