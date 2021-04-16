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
        private int keyDestino;
        private double precioEconomy;
        private double precioFirst;
        private double precioEconomyDollars;
        private double precioFirstDollars;
        public Prices()
        {
            InitializeComponent();

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
            keyDestino = precio.ObtenerKeyDestino(idVuelo);

            // Calcular los precios
            // Estados Unidos
            if (keyDestino == 4 || keyDestino == 5)
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
            else if (keyDestino == 6 || keyDestino == 7)
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
            else if (keyDestino == 8 || keyDestino == 9)
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
            // Panamá
            else if (keyDestino == 10 || keyDestino == 11)
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
            // Corea del Sur
            else if (keyDestino == 12 || keyDestino == 13)
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
            // Bolivia
            else if (keyDestino == 14 || keyDestino == 15)
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
            // El Salvador
            else if (keyDestino == 16 || keyDestino == 17)
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
            // Canadá
            else if (keyDestino == 18 || keyDestino == 19)
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
            // Argentina
            else if (keyDestino == 20 || keyDestino == 21)
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
            else if (keyDestino == 22 || keyDestino == 23)
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
        }

        // Almacena en la base de datos la clase de asiento que eligió el usuario
        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtiene los valores del precio
                precio.idVuelo = idVuelo;
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
                precio.idVuelo = idVuelo;
                precio.idClase = 1;
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
    }
}
