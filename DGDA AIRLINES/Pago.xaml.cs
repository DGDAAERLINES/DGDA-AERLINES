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
using System.IO;

//para los siguientes namespaces, se consulto la siguiente pagina:
//https://docs.microsoft.com/en-us/dotnet/api/system.net.networkcredential?view=net-5.0
using System.Net.Mail;
using System.Net;

//En la siguiente página se consultó acerca de Aspose.pdf
//https://blog.aspose.com/2020/12/02/create-pdf-files-using-csharp/
using Aspose.Pdf;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para Pago.xaml
    /// </summary>
    public partial class Pago : Window
    {
        public Pago()
        {
            InitializeComponent();
        }
        public int Opcion { get; set; }

        public Pago(int opcion)
        {
            Opcion = opcion;
        }
        public void ElegirOpcion(int opcion)
        {
            if (opcion == 1)
            {
                lblDistincion.Content = "Pago por emisión pasaporte";
                lblCantidad.Visibility = Visibility.Visible;
                cmbAnios.Visibility = Visibility.Visible;
            }
            else if (opcion == 2)
            {
                lblDistincion.Content = "Pago por boleto de vuelo";
                lblAdultos.Visibility = Visibility.Visible;
                lblNinios.Visibility = Visibility.Visible;
                txtAdultos.Visibility = Visibility.Visible;
                txtNinios.Visibility = Visibility.Visible;
            }
        }

        private bool VerificarValoresPasaporte()
        {
            //se verifica que el usuario haya llenado todas las casillas y elegido el metodo de pago
            if (txtNumerosTarjeta.Text == string.Empty || txtNumerosTarjeta2.Text == string.Empty || txtNumerosTarjeta3.Text == string.Empty || txtNumerosTarjeta4.Text == string.Empty
                || txtNumerosTarjeta5.Text == string.Empty || txtNumerosTarjeta6.Text == string.Empty || txtNumerosTarjeta7.Text == string.Empty || txtNumerosTarjeta8.Text == string.Empty
                || txtNumerosTarjeta9.Text == string.Empty || txtNumerosTarjeta10.Text == string.Empty || txtNumerosTarjeta11.Text == string.Empty
                || txtNumerosTarjeta13.Text == string.Empty || txtNumerosTarjeta14.Text == string.Empty || txtNumerosTarjeta15.Text == string.Empty
                || txtNumerosTarjeta16.Text == string.Empty || cmbMetodo.SelectedIndex == -1 || txtNumerosCVV1.Text == string.Empty
                || txtNumerosCVV2.Text == string.Empty || txtNumerosCVV3.Text == string.Empty || txtNumerosExpiracion1.Text == string.Empty
                || txtNumerosExpiracion2.Text == string.Empty || txtNumerosExpiracion3.Text == string.Empty || txtNumerosExpiracion4.Text == string.Empty
                || txtCorreoEmpresa.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores para poder realizar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (rtMastercard.Visibility == Visibility.Visible && rtVisa.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Por favor, seleccione Visa o Mastercard.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool VerificarValoresBoleto()
        {
            //se verifica que el usuario haya llenado todas las casillas y elegido el metodo de pago
            if (txtNumerosTarjeta.Text == string.Empty || txtNumerosTarjeta2.Text == string.Empty || txtNumerosTarjeta3.Text == string.Empty || txtNumerosTarjeta4.Text == string.Empty
                || txtNumerosTarjeta5.Text == string.Empty || txtNumerosTarjeta6.Text == string.Empty || txtNumerosTarjeta7.Text == string.Empty || txtNumerosTarjeta8.Text == string.Empty
                || txtNumerosTarjeta9.Text == string.Empty || txtNumerosTarjeta10.Text == string.Empty || txtNumerosTarjeta11.Text == string.Empty
                || txtNumerosTarjeta13.Text == string.Empty || txtNumerosTarjeta14.Text == string.Empty || txtNumerosTarjeta15.Text == string.Empty
                || txtNumerosTarjeta16.Text == string.Empty || cmbMetodo.SelectedIndex == -1 || txtNumerosCVV1.Text == string.Empty
                || txtNumerosCVV2.Text == string.Empty || txtNumerosCVV3.Text == string.Empty || txtNumerosExpiracion1.Text == string.Empty
                || txtNumerosExpiracion2.Text == string.Empty || txtNumerosExpiracion3.Text == string.Empty || txtNumerosExpiracion4.Text == string.Empty
                || txtCorreoEmpresa.Text == string.Empty || txtNinios.Text == string.Empty || txtAdultos.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores para poder realizar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (rtMastercard.Visibility == Visibility.Visible && rtVisa.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Por favor, seleccione Visa o Mastercard.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        //Se regresa al menu
        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            //Concede al usuario la opcion de cancelar la transaccion
            MessageBoxResult resultado = MessageBox.Show("¿Seguro que desea cancelar la transacción?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultado == MessageBoxResult.Yes)
            {
                (App.Current.MainWindow as MainWindow).Show();
                Close();
            }
        }

        private void LimpiarFormulario()
        {
            //Despeje del formulario 
            txtNumerosTarjeta.Text = string.Empty;
            txtNumerosTarjeta2.Text = string.Empty;
            txtNumerosTarjeta3.Text = string.Empty;
            txtNumerosTarjeta4.Text = string.Empty;
            txtNumerosTarjeta5.Text = string.Empty;
            txtNumerosTarjeta6.Text = string.Empty;
            txtNumerosTarjeta7.Text = string.Empty;
            txtNumerosTarjeta8.Text = string.Empty;
            txtNumerosTarjeta9.Text = string.Empty;
            txtNumerosTarjeta10.Text = string.Empty;
            txtNumerosTarjeta11.Text = string.Empty;
            txtNumerosTarjeta12.Text = string.Empty;
            txtNumerosTarjeta13.Text = string.Empty;
            txtNumerosTarjeta14.Text = string.Empty;
            txtNumerosTarjeta15.Text = string.Empty;
            txtNumerosTarjeta16.Text = string.Empty;
            txtNumerosCVV1.Text = string.Empty;
            txtNumerosCVV2.Text = string.Empty;
            txtNumerosCVV3.Text = string.Empty;
            txtNumerosExpiracion1.Text = string.Empty;
            txtNumerosExpiracion2.Text = string.Empty;
            txtNumerosExpiracion3.Text = string.Empty;
            txtNumerosExpiracion4.Text = string.Empty;
            cmbMetodo.SelectedIndex = -1;
            txtCorreo.Text = string.Empty;
            cmbAnios.SelectedIndex = -1;
            txtAdultos.Text = string.Empty;
            txtNinios.Text = string.Empty;
        }

        private void btnVisa_Click(object sender, RoutedEventArgs e)
        {
            //Selección de tarjeta Visa
            //Se muestra un rectangulo semi-transparente para indicar que el boton fue seleccionado
            //Cuando un boton es seleccionado, su rectangulo aparece y el del otro desaparece
            rtVisa.Visibility = Visibility.Visible;
            rtMastercard.Visibility = Visibility.Hidden;
        }

        private void btnRealizarTransaccion_Click(object sender, RoutedEventArgs e)
        {
            CrearPDF();
            //Se realiza el pago y se envia el correo electronico con el recibo
            SendEmail();
        }

        private void SendEmail()
        {
            if (VerificarValoresPasaporte())
            {
                try
                {
                    //Se consulto el siguiente video para la obtencion de este metodo para enviar correos electrónicos
                    //https://www.youtube.com/watch?v=edmFkfMe1K0
                    MailMessage mail = new MailMessage();
                    SmtpClient Smtp = new SmtpClient("smtp.gmail.com");
                    txtCorreoEmpresa.Text = "dgdaairlines@gmail.com";
                    mail.From = new MailAddress(txtCorreoEmpresa.Text);
                    mail.To.Add(txtCorreo.Text);
                    mail.Subject = "Recibo DGDA-AIRLINES";
                    mail.Body = "Estimado usuario, por este medio le hacemos envio de su recibo. \n" +
                                "Cuando se trata de aerolineas, sabemos que cuenta con más opciones. \n" +
                                "Muchas gracias por preferir a DGDA-AIRLINES.\n" +
                                "Tenga un muy buen día.";
                    Attachment archivo = new Attachment("RECIBO.pdf");
                    mail.Attachments.Add(archivo);
                    Smtp.Port = 587;
                    Smtp.Credentials = new NetworkCredential(txtCorreoEmpresa.Text, "DGDA2021");
                    //Al inicio no lograba que enviase los correos. hasta que aprendi en 
                    //https://support.google.com/mail/answer/7126229?visit_id=1-636683482170517029-2536242402&hl=es&rd=1
                    // que EnableSsl era necesario para que pudieran enviarse. 
                    Smtp.EnableSsl = true;
                    Smtp.Send(mail);
                    MessageBox.Show("Transacción realizada", "Recibo enviado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (SmtpException e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    LimpiarFormulario();
                }
            }

        }

        private void CrearPDF()
        {
            DateTime ahora = DateTime.Now;
            string tiempo = ahora.ToString();
            string precioPasaporte, nombreTarjeta;
            try
            {
                if (cmbAnios.SelectedIndex == 0)
                {
                    precioPasaporte = "35";
                }
                else if (cmbAnios.SelectedIndex == 1)
                {
                    precioPasaporte = "50";
                }
                else
                {
                    precioPasaporte = "0";
                }
                if (rtVisa.IsVisible == true)
                {
                    nombreTarjeta = "VISA";
                }
                else if (rtMastercard.IsVisible == true)
                {
                    nombreTarjeta = "Mastercard";
                }
                else
                {
                    nombreTarjeta = "Otro";
                }

                Document documento = new Document();
                //Se presentó un problema debido a una referencia ambigua pero se encontró la solucion en la siguiente página:
                //https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0104?f1url=%3FappId%3Droslyn%26k%3Dk(CS0104)
                Aspose.Pdf.Page paginas = documento.Pages.Add();
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("****************************************************************************"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("                                           DGDA-Airlines"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("                                 Oficina de emisión de pasaporte"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("                                           Tel: 7777-7777"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("                                        " + tiempo));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("****************************************************************************"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("****************************************************************************"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Nombre(s): Javier Danilo"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Apellido(s): Zavala Meza"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Numero de identidad: 0318199802248"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Pasaporte valido por: " + cmbAnios.Text + " años."));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("-----------------------------------------------------------------------------------------"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Producto                                                                                  Precio"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("-----------------------------------------------------------------------------------------"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Pasaporte por: " + cmbAnios.Text + " años                                                               $" + precioPasaporte));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("-----------------------------------------------------------------------------------------"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("-----------------------------------------------------------------------------------------"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Tarjeta de " + cmbMetodo.Text + " remitida"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("           (Nombre tarjeta: " + nombreTarjeta + ")"));
                paginas.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("           (Numero tarjeta: XXXX-XXXX-XXXX-" + txtNumerosTarjeta13.Text + txtNumerosTarjeta14.Text + txtNumerosTarjeta15.Text + txtNumerosTarjeta16.Text + ")"));

                documento.Save("RECIBO.pdf");
                MessageBox.Show("Recibo generado.");

            }
            catch (PdfException x)
            {
                Console.WriteLine(x);
                MessageBox.Show("Error");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Seleccion del tarjeta Mastercard
            //Se muestra un rectangulo semi-transparente para indicar que el boton fue seleccionado
            //Cuando un boton es seleccionado, su rectangulo aparece y el del otro desaparece
            rtVisa.Visibility = Visibility.Hidden;
            rtMastercard.Visibility = Visibility.Visible;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Close();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Bank_LoadingScreen screen = new Bank_LoadingScreen();
            screen.Show();
            Close();
        }
    }
}
