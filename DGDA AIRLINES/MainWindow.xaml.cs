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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
//Librerias para webcam
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AForge.Video.DirectShow;
using AForge.Video;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Camara web//
        VideoCaptureDevice LocalWebCam;
        public FilterInfoCollection LoaclWebCamsCollection;
        private readonly BitmapImage latestFrame;

        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    frameHolder.Source = bi;
                }));
            }
            catch (Exception ex)
            {
            }
        }

        /*void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Bmp);
                    bi.StreamSource = ms;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                }
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate { frameHolder.Source = bi; }));


            }
            catch (Exception ex)
            {
                //catch your error here
            }

        }
        
         BTNGUARDAR
        boton

    private void manualCapture_Click(object sender, RoutedEventArgs e)
    {
        if (captureImage != null)
        {
            captureImage(latestFrame);
        }
        Bitmap bm = BitmapImageToBitmap(latestFrame);
        bm.Save(@"C:\tmp\test.jpg", ImageFormat.Jpeg);
    }*/


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();

        }

        //-----------------------------------------------------------------------------------------------------------
        // Variables miembro
        private Passport Pasaporte = new Passport();
        private List<Passport> Pasaportes;
        public MainWindow()
        {
            InitializeComponent();

            //Inicia la camara
            Loaded += MainWindow_Loaded;


            // Llenar el combobox con los tipos del pasaporte
            cmbtype.ItemsSource = Enum.GetValues(typeof(Type));

            // Llenar el listbox de pasaportes
            ObtenerPasaportes();
        }


        //PROYECTO COMO TAL

        // Obtiene de la base de datos en una lista de los Pasaportes registrados
        private void ObtenerPasaportes()
        {
            Pasaportes = Pasaporte.MostrarPasaportes();
            lbpassport.ItemsSource = Pasaportes;
        }

        //Limpiar cada texbox del Formulario
        private void LimpiarTextbox()
        {
            txtID.Text = string.Empty;
            cmbtype.SelectedValue = null;
            txtpaisemisor.Text = string.Empty;
            txtpassportno.Text = string.Empty;
            txtsurname.Text = string.Empty;
            txtname.Text = string.Empty;
            txtbirth.SelectedDate = null;
            txtnationality.Text = string.Empty;
            txtsexo.Text = string.Empty;
            txtid.Text = string.Empty;
            txtplace.Text = string.Empty;
            txtissue.Text = null;
            txtexpiry.Text = null;
            txtauthority.Text = string.Empty;
        }

        // Valida que se ingresen datos en cada las cajas de texto
        private bool ValidarCampos()
        {
            if (txtpaisemisor.Text == string.Empty || txtpassportno.Text == string.Empty || txtsurname.Text == string.Empty || txtname.Text == string.Empty ||
                /*txtbirth.Text == string.Empty ||*/ txtnationality.Text == string.Empty || txtsexo.Text == string.Empty || txtid.Text == string.Empty ||
                txtplace.Text == string.Empty || txtissue.Text == string.Empty || txtexpiry.Text == string.Empty || txtauthority.Text == string.Empty)
            {
                //Mensaje de condicion debe llenar todos los campos no pueden quedar vacios
                MessageBox.Show("Please fill all the textboxes!");
                return false;
            }
            // Valida que se seleccione el un tipo de pasaporte
            else if (cmbtype.SelectedValue == null)
            {
                //Muestra mensaje de condicion debe seleccionar un tipo de pasaporte
                MessageBox.Show("Please select the user status!");
                return false;
            }
            /*else if (txtsexo.Text= Int)
            {
                //Condicionar si identidad de pasa de 15 digitos
                MessageBox.Show("Please select the user status!");
                return false;
            }*/
            return true;
        }


        private void ObtenerValores()
        {
            // Asigna los valores de los textbox para que se conviertan en las propiedades del pasaporte
            //Pasaporte.Type = (Type)cmbtype.SelectedValue;
            Pasaporte.Type = (Type)cmbtype.SelectedValue;
            Pasaporte.IssuingState = txtpaisemisor.Text;
            Pasaporte.PassportNo = txtpassportno.Text;
            Pasaporte.Surname = txtsurname.Text;
            Pasaporte.GivenName = txtname.Text;
            Pasaporte.DateofBirth = Convert.ToDateTime(txtbirth.DisplayDate);
            Pasaporte.Nationality = txtnationality.Text;
            Pasaporte.Sex = txtsexo.Text;
            Pasaporte.IDNo = txtid.Text;
            Pasaporte.PlaceofBirth = txtplace.Text;
            Pasaporte.DateofIssue = Convert.ToDateTime(txtissue.DisplayDate);
            Pasaporte.DateofExpiry = Convert.ToDateTime(txtexpiry.DisplayDate);
            Pasaporte.AuthorityofIssue = txtauthority.Text;

        }

        // Llena las cajas de texto con los valores del DataGrid
        private void ValoresFormularioDesdeObjeto()
        {
            // Crea un objeto de tipo passport que captura los valores del DataGrid
            Passport objpassport = new Passport();
            objpassport = (Passport)lbpassport.SelectedItem;
            txtID.Text = objpassport.ID.ToString();
            cmbtype.SelectedValue = objpassport.Type;
            txtpaisemisor.Text = objpassport.IssuingState;
            txtpassportno.Text = objpassport.PassportNo;
            txtsurname.Text = objpassport.Surname;
            txtname.Text = objpassport.GivenName;
            txtbirth.SelectedDate = objpassport.DateofBirth;
            txtnationality.Text = objpassport.Nationality;
            txtsexo.Text = objpassport.Sex;
            txtid.Text = objpassport.IDNo;
            txtplace.Text = objpassport.PlaceofBirth;
            txtissue.SelectedDate = objpassport.DateofIssue;
            txtexpiry.SelectedDate = objpassport.DateofExpiry;
            txtauthority.Text = objpassport.AuthorityofIssue;

        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron todos los datos 
            if (ValidarCampos())
            {
                try
                {
                    // Obtiene los valores del pasaporte
                    ObtenerValores();

                    // Insertar los datos del pasaporte
                    Pasaporte.InsertarPasaporte(Pasaporte);
                    //Mensaje de proceso realizado correctamente
                    MessageBox.Show("Sucessfully Passport Insert!");

                }
                catch (Exception ex)
                {
                    //Mensaje de error
                    MessageBox.Show("Data Insert Error :X");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    //Realiza la limpieza 
                    //LimpiarTextbox();
                    ObtenerPasaportes();
                }
            }
        }
        private void OcultarBotones(Visibility ocultar)
        {
            //Ocultar bonotenes del crud
            btninsert.Visibility = ocultar;
            btnupdate.Visibility = ocultar;
            btndelete.Visibility = ocultar;
            btnback.Visibility = ocultar;
        }


        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            //Valida si el combobox esta null es decir, vacio
            if (lbpassport.SelectedItem == null)
                MessageBox.Show("Please select a passport type in the list");
            else
            {
                // Crea un objeto de tipo Passport para obtener el ID del pasaporte
                Passport objPassport = new Passport();
                objPassport = (Passport)lbpassport.SelectedItem;
                int IdPassport = objPassport.ID;

                try
                {
                    // Obtiene los datos del pasaporte
                    Pasaporte = Pasaporte.BuscarPasaporteID(IdPassport);

                    // Llenar los valores del formulario
                    ValoresFormularioDesdeObjeto();

                    // Ocultar los botones del CRUD
                    OcultarBotones(Visibility.Hidden);
                }
                catch (Exception ex)
                {
                    //Muestra mensaje de error
                    MessageBox.Show("Data Update Error");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //Obtiene los pasaportes
                    ObtenerPasaportes();
                    btnOK.Visibility = Visibility.Visible;
                    btncancel.Visibility = Visibility.Visible;
                }
            }
        }

        //private void SendEmail()
        //{
        //    try
        //    {
        //        //Se consulto el siguiente video para la obtencion de este metodo para enviar correos electrónicos
        //        //https://www.youtube.com/watch?v=edmFkfMe1K0
        //        MailMessage mail = new MailMessage();
        //        SmtpClient Smtp = new SmtpClient("smtp.gmail.com");
        //        mail.From = new MailAddress("dgdaairlines@gmail.com");
        //        mail.To.Add("danilozm1997@gmail.com");
        //        mail.Subject = "Reporte Pasajeros";
        //        mail.Body = "Reporte Pasajeros";
        //        Attachment archivo = new Attachment("ReportPasaporte.rdlc");
        //        mail.Attachments.Add(archivo);
        //        Smtp.Port = 587;
        //        Smtp.Credentials = new NetworkCredential("dgdaairlines@gmail.com", "DGDA2021");
        //        //Al inicio no lograba que enviase los correos. hasta que aprendi en 
        //        //https://support.google.com/mail/answer/7126229?visit_id=1-636683482170517029-2536242402&hl=es&rd=1
        //        // que EnableSsl era necesario para que pudieran enviarse. 
        //        Smtp.EnableSsl = true;
        //        Smtp.Send(mail);
        //        MessageBox.Show("Mensaje enviado", "Message sent", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (SmtpException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    finally
        //    {
        //        LimpiarTextbox();
        //    }

        //}

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Valida si los campos estan correctos y no esta ninguno vacio
            if (ValidarCampos())
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("¿Are you sure update passport?", "Confirm!", MessageBoxButton.YesNo, MessageBoxImage.Warning);


                    if (result == MessageBoxResult.Yes)
                    {
                        // Obtener los valores del pasaporte
                        ObtenerValores();

                        // Actualizar los valores en la base de datos
                        Pasaporte.ModificarPasaporte(Pasaporte, Convert.ToInt32(txtID.Text));

                        MessageBox.Show("Sucessfully Passport Update!");

                        // Muestra los botones del CRUD
                        OcultarBotones(Visibility.Visible);

                        // Limpiar cada una de las cajas de texto
                        LimpiarTextbox();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Passport Update Error");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Actualizar el listbox de los pasaportes
                    ObtenerPasaportes();
                    btncancel.Visibility = Visibility.Hidden;
                    btnOK.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Are you sure cancel?", "Confirm!", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            if (result == MessageBoxResult.Yes)
            {
                // Permite mostrar los botones del CRUD
                OcultarBotones(Visibility.Visible);

                // Realiza la limpieza de cada uno los campos de texto
                LimpiarTextbox();
                btncancel.Visibility = Visibility.Hidden;
                btnOK.Visibility = Visibility.Hidden;
            }

        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbpassport.SelectedItem == null)
                    MessageBox.Show("Please, select a passport in the list");
                else
                {
                    // Crea un objeto de tipo Passport para obtener el ID del pasaporte
                    Passport objPassport = new Passport();
                    objPassport = (Passport)lbpassport.SelectedItem;
                    int ID = objPassport.ID;

                    // Despliega una confirmación para que el usuario permita eliminar el pasaporte seleccionado
                    MessageBoxResult result = MessageBox.Show("¿Are you sure to delete this passport?", "Confirm!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Elimina el pasaporte de la base de datos
                        Pasaporte.EliminarPasaporte(ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Delete Error");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el listbox de los Pasaportes
                ObtenerPasaportes();
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            // Retornar al formulario Menu Principal
            /*Menu MenuPrincipal = new Menu();
            MenuPrincipal.Show();
            Close();*/
        }

        private void lbpassport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPrueba_Click(object sender, RoutedEventArgs e)
        {
            LoaclWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[0].MonikerString);
            LocalWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);

            MessageBoxResult mensaje = MessageBox.Show("¿Desea encender la camara?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (mensaje == MessageBoxResult.Yes)
            {
                LocalWebCam.Start();
            }
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mensaje = MessageBox.Show("¿Desea apagar la camara?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (mensaje == MessageBoxResult.Yes)
            {
                LocalWebCam.Stop();
            }
        }
    }
}
