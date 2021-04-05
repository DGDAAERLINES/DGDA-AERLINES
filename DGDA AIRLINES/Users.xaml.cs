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

namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para User.xaml
    /// </summary>
    public partial class Users : Window
    {
        // Variables miembro
        private User usuario = new User();
        private List<User> usuarios;
        public Users()
        {
            InitializeComponent();

            // Llenar el combobox con los estados del usuario
            cmbStatus.ItemsSource = Enum.GetValues(typeof(EstadosUsuario));

            // Llenar el listbox de usuarios
            ObtenerUsuarios();
        }

        // Muestra los caracteres de la contraseña si se marca el checkbox de mostrar
        private void MostrarContraseña()
        {
            // 
            if (chkShow.IsChecked == true)
            {
                txtPasswordVisible.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Hidden;
                txtPasswordVisible.Text = txtPassword.Password;
                txtPasswordVisible.Focus();
            }
            else
            {
                txtPasswordVisible.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Visible;
                txtPassword.Password = txtPasswordVisible.Text;
                txtPassword.Focus();
            }
        }

        // Actualiza la passwordbox con los cambios que se realizaron en la caja de texto donde se muestra
        // visible la contraseña
        private void ActualizarContraseñaVisible()
        {
            txtPassword.Password = txtPasswordVisible.Text;
        }

        // Actualiza la caja de texto con los cambios que se realizaron en la paswordbox
        private void ActualizarContraseña()
        {
            txtPasswordVisible.Text = txtPassword.Password;
        }

        // Borra el contenido que se encuentra en los textbox
        private void LimpiarTextbox()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtUser.Text = string.Empty;
            txtPassword.Clear();
            txtPasswordVisible.Text = string.Empty;
            chkShow.IsChecked = false;
            cmbStatus.SelectedValue = null;
        }

        // Oculta los botones que tienen la funcionalidad del CRUD
        private void OcultarBotones(Visibility ocultar)
        {
            btnInsert.Visibility = ocultar;
            btnUpdate.Visibility = ocultar;
            btnDelete.Visibility = ocultar;
            btnBack.Visibility = ocultar;
        }

        // Valida que se ingresen datos en todas las cajas de texto
        private bool ValidarCampos()
        {
            if (txtName.Text == string.Empty || txtUser.Text == string.Empty ||
                txtPassword.Password.ToString() == string.Empty)
            {
                MessageBox.Show("Please fill all the textboxes!");
                return false;
            }
            // Valida que se seleccione el estado del usuario
            else if (cmbStatus.SelectedValue == null)
            {
                MessageBox.Show("Please select the user status!");
                return false;
            }

            return true;
        }

        // Obtiene de la base de datos una lista de los usuarios registrados
        private void ObtenerUsuarios()
        {
            usuarios = usuario.MostrarUsuarios();
            lbUsers.ItemsSource = usuarios;
        }

        // Asigna los valores de los textbox para que se conviertan en las propiedades del usuario
        private void ObtenerValores()
        {
            usuario.Name = txtName.Text;
            usuario.Username = txtUser.Text;
            usuario.Password = txtPassword.Password;
            usuario.Status = (EstadosUsuario)cmbStatus.SelectedValue;
        }

        // Llena las cajas de texto con los valores del DataGrid
        private void ValoresFormularioDesdeObjeto()
        {
            // Crea un objeto de tipo user que captura los valores del DataGrid
            User objuser = new User();
            objuser = (User)lbUsers.SelectedItem;
            txtID.Text = objuser.ID.ToString();
            txtName.Text = objuser.Name;
            txtUser.Text = objuser.Username;
            txtPassword.Password = objuser.Password;
            cmbStatus.SelectedValue = objuser.Status;
        }

        // Agrega un usuario a la base de datos
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron todos los datos 
            if (ValidarCampos())
            {
                try
                {
                    // Obtiene los valores del usuario
                    ObtenerValores();

                    // Insertar los datos del usuario
                    usuario.InsertarUsuario(usuario);

                    MessageBox.Show("Sucessfully User Insert!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Insert Error :X");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarTextbox();
                    ObtenerUsuarios();
                }
            }
        }

        // Actualiza los datos del usuario en la base de datos
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem == null)
                MessageBox.Show("Please select a user in the list");
            else
            {
                // Crea un objeto de tipo User para obtener el ID del usuario
                User objUser = new User();
                objUser = (User)lbUsers.SelectedItem;
                int ID = objUser.ID;

                try
                {
                    // Obtiene los datos del usuario
                    usuario = usuario.BuscarUsuarioID(ID);

                    // Llenar los valores del formulario
                    ValoresFormularioDesdeObjeto();

                    // Ocultar los botones del CRUD
                    OcultarBotones(Visibility.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Update Error :X...");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    ObtenerUsuarios();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar los botones del CRUD
            OcultarBotones(Visibility.Visible);

            // Limpia los campos de texto
            LimpiarTextbox();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    // Obtener los valores del usuario
                    ObtenerValores();

                    // Actualizar los valores en la base de datos
                    usuario.ModificarUsuario(usuario, Convert.ToInt32(txtID.Text));

                    MessageBox.Show("Sucessfully User Update!");

                    // Mostrar los botones del CRUD
                    OcultarBotones(Visibility.Visible);

                    // Limpiar las cajas de texto
                    LimpiarTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("User Update Error :X");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Actualizar el listbox de los usuarios
                    ObtenerUsuarios();
                }
            }
        }

        // Elimina un usuario de la base de datos
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbUsers.SelectedItem == null)
                    MessageBox.Show("Please select a user in the list");
                else
                {
                    // Crea un objeto de tipo User para obtener el ID del usuario
                    User objUser = new User();
                    objUser = (User)lbUsers.SelectedItem;
                    int ID = objUser.ID;

                    // Despliega una confirmación
                    MessageBoxResult result = MessageBox.Show("Are you sure to delete this user?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Elimina el usuario de la base de datos
                        usuario.EliminarUsuario(ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Delete Error :X");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Actualizar el listbox de los usuarios
                ObtenerUsuarios();
            }
        }

        // Regresar al menú
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Cierra el formulario
            this.Close();
        }

        // Si el checkbox 'chkShow' está marcado muestra la contraseña
        private void chkShow_Checked(object sender, RoutedEventArgs e)
        {
            MostrarContraseña();
        }

        // Si el checkbox 'chkShow' está desmarcado oculta la contraseña
        private void chkShow_Uncheked(object sender, RoutedEventArgs e)
        {
            MostrarContraseña();
        }

        // Actualiza la passwordbox si los datos en la caja de texto cambian
        private void txtPasswordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualizarContraseñaVisible();
        }

        // Actualiza la textbox si los datos en la passwordbox cambian
        private void txtPassword_TextInput(object sender, TextCompositionEventArgs e)
        {
            ActualizarContraseña();
        }
    }
}