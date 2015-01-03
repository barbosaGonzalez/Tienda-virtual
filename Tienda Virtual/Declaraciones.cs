using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //directiva

namespace Tienda_Virtual
{
    class Declaraciones
    {
        public Declaraciones()
        {
            _Conexion.ConnectionString = "Server=localhost;Database=tiendavirtualCSharp;User id=root;Password=;";
            //_Conexion.ConnectionString = "Server=192.168.56.3;Database=tiendaonline;User id=torby;Password=e.-94B:_41;";
            _Instruccion.Connection = _Conexion;
        }
        MySqlConnection _Conexion = new MySqlConnection(); //Variable para hacer la conexion
        public MySqlConnection Conexion  //Metodo que establece la variable
        {
            get { return _Conexion; }
            set { _Conexion = value; }
        }
        

        MySqlDataReader _Lector; //Variable para leer de la base de datos
        public MySqlDataReader Lector  //Metodo que establece la variable
        {
            get { return _Lector; }
            set { _Lector = value; }
        }

        /// <summary>
        /// Metodo para hacer Updates, Inserts, Deletes, etc.
        /// </summary>
        /// <param name="instrucciones"></param>
        /// <returns></returns>
        public bool dosomething(string instrucciones)
        {
            try
            {
                Conexion.Open(); //Se conecta con el servidor
                Instruccion.CommandText = instrucciones; //Recibe la instruccion
                Instruccion.Connection = Conexion; //Permite realizar operaciones en la base de datos
                Instruccion.ExecuteNonQuery(); //Ejecuta la instruccion
                Conexion.Close(); //Cierra la conexion con el servidor
                return true;
            }
            catch (Exception)  //En caso de error
            {
                Conexion.Close();  //Se cierra la conexion
                return false;  //Se regresa false

            }
        }

        MySqlCommand _Instruccion = new MySqlCommand(); //Variable para hacer comandos
        public MySqlCommand Instruccion  //Metodo que establece la variable
        {
            get { return _Instruccion; }
            set { _Instruccion = value; }
        }

        MySqlDataAdapter _DARetiro = new MySqlDataAdapter();

        public MySqlDataAdapter DARetiro
        {
            get { return _DARetiro; }
            set { _DARetiro = value; }
        }
    }
}
