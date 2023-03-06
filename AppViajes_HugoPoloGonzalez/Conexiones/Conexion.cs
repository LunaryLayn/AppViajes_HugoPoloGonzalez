using AppViajes_HugoPoloGonzalez.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppViajes_HugoPoloGonzalez.Conexiones
{
    public class Conexion
    {
        //public Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


        string path;
        //SQLite.Net.SQLiteConnection conn;

        SQLite.SQLiteConnection conn;

        public Conexion()
        {

        }



        public async Task<bool> compruebaSiExisteBD(string nombreBD)
        {
            bool bdExiste = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(nombreBD);
            }
            catch (Exception)
            {
                bdExiste = false;
            }

            return bdExiste;
        }




        //private async Task CreateDatabaseAsync()
        //{

        //    SQLiteAsyncConnection conn = new SQLiteAsyncConnection(() =>
        //    {
        //        return new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(
        //         Path.Combine(ApplicationData.Current.LocalFolder.Path, "BDAgenda.db"), false));
        //    });

        //    await conn.CreateTableAsync<Viaje>();
        //}

        public void CreateDatabase()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

            //conn = new SQLite.Net.SQLiteConnection(new
            //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            conn = new SQLite.SQLiteConnection(path);


            conn.CreateTable<Viaje>();
            conn.Close();


        }

        public void addPrimerosViajes()
        {
            //Creamos una lista de usuarios
            var listaViajes = new List<Viaje>()
            {
                new Viaje()
                {
                    Ciudad = "Paris",
                    Detalle = "Bonita ciudad en Francia",
                    Foto = "https://viajes.nationalgeographic.com.es/medio/2022/07/13/paris_37bc088a_1280x720.jpg",
                    Realizado = "No",
                }
            };

            //Añadimos los usuarios a la tabla

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

            //conn = new SQLite.Net.SQLiteConnection(new
            //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            conn = new SQLite.SQLiteConnection(path);

            conn.InsertAll(listaViajes);

            conn.Close();
        }

        public List<Viaje> LeerViajes()
        {
            List<Viaje> ListaDeViajes =
               new List<Viaje>();

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

            //conn = new SQLite.Net.SQLiteConnection(new
            //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            conn = new SQLite.SQLiteConnection(path);

            var query = conn.Table<Viaje>();
            // Viajes = await query.ToListAsync();

            ListaDeViajes = query.ToList<Viaje>();

            return ListaDeViajes;

        }

        public void addViaje(Viaje NViaje)
        {


            //Añadimos  Viaje a la tabla

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

            //conn = new SQLite.Net.SQLiteConnection(new
            //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            conn = new SQLite.SQLiteConnection(path);

            //Viaje Viaje = new Viaje();
            //Viaje.Nombre = nombre;
            //Viaje.Telefono = telefono;
            //Viaje.Direccion = direccion;

            conn.Insert(NViaje);

            conn.Close();
        }




         public void borrarViaje(int Identificador)
         {
             path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

             //conn = new SQLite.Net.SQLiteConnection(new
             //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

             conn = new SQLite.SQLiteConnection(path);

             var Viaje = conn.Table<Viaje>().Where(x => x.Id == Identificador).FirstOrDefault();
             if (Viaje != null)
             {
                 conn.Delete(Viaje);
             }

         }

         /*public void modificarViaje(int Identificador, string nombre, string direccion, string telefono)
         {
             path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BDViajes.db");

             //conn = new SQLite.Net.SQLiteConnection(new
             //SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

             conn = new SQLite.SQLiteConnection(path);

             var Viaje = conn.Table<Viaje>().Where(x => x.Id == Identificador).FirstOrDefault();
             if (Viaje != null)
             {
                 Viaje.Nombre = nombre;
                 Viaje.Direccion = direccion;
                 Viaje.Telefono = telefono;

                 conn.Update(Viaje);
             }

         }*/

    }
}
