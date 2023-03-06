using AppViajes_HugoPoloGonzalez.Commands;
using AppViajes_HugoPoloGonzalez.Conexiones;
using AppViajes_HugoPoloGonzalez.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace AppViajes_HugoPoloGonzalez.Viewmodel
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private Conexion conexionDeDatos;

        public ICommand ComandoHabilitar { get; set; }
        public ICommand ComandoActualizar { get; set; }
        public ICommand ComandoEliminar { get; set; }
        public ICommand ComandoGuardar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainPageModel()
        {
            conexionDeDatos = new Conexion();

            CSiDBExiste();

            ListaViajes = conexionDeDatos.LeerViajes();

            ComandoHabilitar = new Command(AccionHabilitar);
            ComandoActualizar = new Command(AccionActualizar);
            ComandoEliminar = new Command(AccionEliminar);

            ComandoGuardar = new Command(AccionGuardar);

            ActivarHabilitar = true;
            ActivarActualizar = false;
            ActivarEliminar = true;

        } 

        private void AccionHabilitar(object obj)
        {
            throw new NotImplementedException();
        }

        private async void CSiDBExiste()
        {
            bool dbExist = await conexionDeDatos.compruebaSiExisteBD("BDViajes.db");

            if (!dbExist)
            {
                //await CreateDatabaseAsync();
                conexionDeDatos.CreateDatabase();

               conexionDeDatos.addPrimerosViajes();

            }

        }

        private async void AccionModificar(object parametro)
        {

           /* if (ContactoSeleccionado != null)
            {
                ActivarControlT = true;

                ActivarControlB1 = false;
                ActivarControlB2 = true;
                ActivarControlB3 = false;


            }
            else
            {


                string msg = "Tienes que seleccionar un contacto de la lista para poder Modificarlo";

                MessageDialog dialog = new MessageDialog(msg);
                await dialog.ShowAsync();
            }*/


        }

        private async void AccionActualizar(object parametro)
        {

          /*  try
            {


                conexionDeDatos.modificarContacto(ContactoSeleccionado.Id, Nombre, Direccion, Telefono);

                string msg = "Contacto Modificado";

                MessageDialog dialog = new MessageDialog(msg);
                await dialog.ShowAsync();

                ListaContactos = conexionDeDatos.LeerContactos();


            }
            catch (Exception ex)
            {

                string msg = ex.Message;

                MessageDialog dialog = new MessageDialog(msg);
                await dialog.ShowAsync();
            }

            finally
            {
                ActivarControlT = false;

                ActivarControlB1 = true;
                ActivarControlB2 = false;
                ActivarControlB3 = true;



            }*/
        }

        private async void AccionEliminar(object parametro)
        {
            if (ViajeSeleccionado != null)
            {

                var messageDialog = new Windows.UI.Popups.MessageDialog("Seguro quieres borrar el contacto?");

                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers

                //  using Windows.UI.Popups; necesario para  UICommand, UICommandInvokedHandler,UICommandInvokedHandler
                messageDialog.Commands.Add(new UICommand(
                    "OK",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand(
                    "Cancel",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 1;

                // Show the message dialog
                await messageDialog.ShowAsync();

            }
            else
            {
                //MessageBox.Show("Tienes que seleccionar un contacto de la lista para poder borrarlo",
                //    "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                string msg = "Tienes que seleccionar un viaje de la lista para poder borrarlo";

                MessageDialog dialog = new MessageDialog(msg);
                await dialog.ShowAsync();
            }

        }

        private async void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "OK")
            {

                try
                {
                    conexionDeDatos.borrarViaje(ViajeSeleccionado.Id);
                    string msg = "Viaje Borrado";

                    MessageDialog dialog = new MessageDialog(msg);
                    await dialog.ShowAsync();

                    ListaViajes = conexionDeDatos.LeerViajes();

                    // ListaContactos.Remove(ContactoSeleccionado);
                    ViajeSeleccionado = null;

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;

                    MessageDialog dialog = new MessageDialog(msg);
                    await dialog.ShowAsync();

                }
            }

            
        }

        private async void AccionGuardar(object parametro)
        {
            Viaje viaje = new Viaje();
            viaje.Ciudad = Ciudad;
            viaje.Detalle = Detalle;
            viaje.Foto = Foto;
            viaje.Realizado = Realizado;


            conexionDeDatos.addViaje(viaje);

            string msg = "Viaje guardado";

            MessageDialog dialog = new MessageDialog(msg);
            await dialog.ShowAsync();

            ListaViajes = conexionDeDatos.LeerViajes();
            //ListaContactos.Add(contacto);

            ViajeSeleccionado = null;

            PivotSeleccionado = 0;


            
        }


        private List<Viaje> listaViajes;

        public List<Viaje> ListaViajes
        {
            get { return listaViajes; }
            set
            {
                listaViajes = value;
                OnPropertyChanged("ListaViajes");
            }
        }

        private int pivotSeleccionado;
        public int PivotSeleccionado
        {
            get { return pivotSeleccionado; }
            set
            {
                pivotSeleccionado = value;
                OnPropertyChanged("PivotSeleccionado");

                if (pivotSeleccionado == 1)

                {
                    ViajeSeleccionado = null;

                }
            }
        }

        private Viaje viajeSeleccionado;
        public Viaje ViajeSeleccionado
        {
            get { return viajeSeleccionado; }
            set
            {
                viajeSeleccionado = value;
                OnPropertyChanged("ContactoSeleccionado");

                if (viajeSeleccionado != null)

                {

                    Ciudad = viajeSeleccionado.Ciudad;
                    Detalle = viajeSeleccionado.Detalle;
                    Foto = viajeSeleccionado.Foto;
                    Realizado = viajeSeleccionado.Realizado;

                }

                else
                {
                    Ciudad = "";
                    Detalle = "";
                    Foto = "";
                    Realizado = "";

                }




            }
        }

        private string ciudad;
        public string Ciudad
        {
            get { return ciudad; }
            set
            {
                ciudad = value;
                OnPropertyChanged("Ciudad");
            }
        }
        private string foto;
        public string Foto
        {
            get { return foto; }
            set
            {
                foto = value;
                OnPropertyChanged("Foto");
            }
        }
        private string detalle;
        public string Detalle
        {
            get { return detalle; }
            set
            {
                detalle = value;
                OnPropertyChanged("Detalle");
            }
        }
        private string realizado;
        public string Realizado
        {
            get { return realizado; }
            set
            {
                realizado = value;
                OnPropertyChanged("Realizado");

                if (realizado == "Si")
                {
                    RealizadoBool = 0;
                }
                else if (realizado == "No")
                {
                    RealizadoBool = 1;
                }
                else
                {
                    RealizadoBool = -1;
                }
            }
        }

        private int realizadoBool;
        public int RealizadoBool
        {
            get { return realizadoBool; }
            set
            {
                realizadoBool = value;
                OnPropertyChanged("RealizadoBool");
            }
        }

        private bool activarHabilitar;
        public bool ActivarHabilitar
        {
            get { return activarHabilitar; }
            set
            {
                activarHabilitar = value;
                OnPropertyChanged("ActivarHabilitar");
            }
        }
        private bool activarActualizar;
        public bool ActivarActualizar
        {
            get { return activarActualizar; }
            set
            {
                activarActualizar = value;
                OnPropertyChanged("ActivarActualizar");
            }
        }
        private bool activarEliminar;
        public bool ActivarEliminar
        {
            get { return activarEliminar; }
            set
            {
                activarEliminar = value;
                OnPropertyChanged("ActivarEliminar");
            }
        }

    }
}
