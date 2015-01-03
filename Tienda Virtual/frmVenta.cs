using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //directiva

namespace Tienda_Virtual
{
    public partial class frmVenta : Form
    {
        public frmVenta(int id_Usuario_Recivido)
        {
            InitializeComponent();
            id_Usuario = id_Usuario_Recivido;
        }

        #region Variables importante
        Declaraciones decla = new Declaraciones();  //Objeto del tipo de la clase Declaraciones
        double subtotal, iva, total;
        int id, id_Usuario;
        #endregion

        private void Form5_Load(object sender, EventArgs e)
        {
            alternativeColorFilasDataGridView(dtgVentas);
            btnComprar.Select();
            llenarDataGrid();
            sacarPrecios();
            try
            {
                decla.Instruccion.Parameters.AddWithValue("id", id_Usuario);
                decla.Instruccion.CommandText = "select Nombre, CURP, Direccion, Telefono, Correo from usuario WHERE id_Usuario = @id";  //Se pasa como una instruccion
                decla.Conexion.Open();  //Se habre la conexion
                decla.Lector = decla.Instruccion.ExecuteReader(); //Se pasa la instruccion para leer
                if (decla.Lector.HasRows) //Si tiene filas
                {
                    while (decla.Lector.Read())  //Mientras existan filas
                    {
                        txtNombre.Text = decla.Lector[0].ToString();
                        txtCURP.Text = decla.Lector[1].ToString();
                        txtDireccion.Text = decla.Lector[2].ToString();
                        txtTelefono.Text = decla.Lector[3].ToString();
                        txtCorreo.Text = decla.Lector[4].ToString();
                    }
                }
                decla.Lector.Close();  //Se cierra Lector
                decla.Conexion.Close();  //Se cierra conexion
            }
            catch (Exception ex)  //Si hay error
            {
                decla.Conexion.Close();  //Se cierra conexion
                MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
            }
        }

        public void alternativeColorFilasDataGridView(DataGridView dtg)
        {
            dtg.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dtg.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }

        public void sacarPrecios()
        {
            try
            {
                subtotal = 0;
                decla.Instruccion.CommandText = "select Precio, TotalProducto from carrito";  //Se pasa como una instruccion
                decla.Conexion.Open();  //Se habre la conexion
                decla.Lector = decla.Instruccion.ExecuteReader(); //Se pasa la instruccion para leer
                if (decla.Lector.HasRows) //Si tiene filas
                    while (decla.Lector.Read())  //Mientras existan filas
                       subtotal = subtotal + Convert.ToDouble(decla.Lector[0])* Convert.ToInt32(decla.Lector[1]);
                decla.Lector.Close();  //Se cierra Lector
                decla.Conexion.Close();  //Se cierra conexion
            }
            catch (Exception ex)  //Si hay error
            {
                decla.Conexion.Close();  //Se cierra conexion
                MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
            }
            txtSubtotal.Text = "$" + subtotal.ToString();
            iva = subtotal * 0.15;
            total = subtotal + iva;
            txtIVA.Text = "$" + iva.ToString();
            txtTotal.Text = "$" + total.ToString();
        }

        private void llenarDataGrid()
        {
            try
            {
                MySqlDataAdapter DataAdapter = new MySqlDataAdapter("SELECT id_Carrito, Nombre, Descripcion, Precio, TotalProducto as Cantidad FROM carrito where id_Cliente = '" + id_Usuario + "'", decla.Conexion.ConnectionString);
                //decla.Instruccion.Parameters.AddWithValue("id_DataGrid", id_Usuario);
                DataSet DSRetiro = new DataSet();
                DataAdapter.Fill(DSRetiro);
                dtgVentas.DataSource = DSRetiro.Tables[0];
                dtgVentas.Columns[0].Visible = false; //ID
                dtgVentas.Columns[1].Visible = true; //NOMBRE
                dtgVentas.Columns[2].Visible = true; //DESCRIPCION
                dtgVentas.Columns[3].Visible = true; //PRECIO
                dtgVentas.Columns[4].Visible = true; //TOTAL_PRODUCTO

                dtgVentas.Columns[1].Width = 140;
                dtgVentas.Columns[2].Width = 399;
                dtgVentas.Columns[3].Width = 57;
                dtgVentas.Columns[4].Width = 60;

                dtgVentas.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgVentas.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                decla.Conexion.Close();
            }
            catch (MySqlException ex)
            {
                decla.Conexion.Close();  //Se cierra conexion
                MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
            }
        }

        private void dtgVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)  //obtner datos de tabla
        {
            id = Convert.ToInt32(dtgVentas.Rows[dtgVentas.CurrentRow.Index].Cells[0].Value);
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            frmFormularioCompra frm = new frmFormularioCompra(dtgVentas, txtCorreo.Text, txtSubtotal.Text, txtIVA.Text, txtTotal.Text, txtNombre.Text, id_Usuario);
            this.Hide();
            frm.ShowDialog();
            if (frm.compraRealizada)
                this.Close();
            else
                this.Show();
        }

        private void btnSeguirComrando_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                decla.Instruccion.Parameters.AddWithValue("idBorrar", id);
                if (decla.dosomething("Delete from carrito where id_Carrito = @idBorrar"))  //Llama al metodo que ejecuta acciones en la base de datos
                {
                    MessageBox.Show("Se ha borrado el producto de tu carrito", "Producto descartado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);  //Se muestra mensaje de que se realizo todo
                    llenarDataGrid();
                    sacarPrecios();
                }
                else  //Si existe algun problema
                    MessageBox.Show("Existe algun tipo de problema, cierre y abra otra ves el programa", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Stop);  //Se muestra mensaje de error
            }
            catch (Exception ex)  //Si hay error
            {
                decla.Conexion.Close();  //Se cierra conexion
                MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssl1.Text = DateTime.Now.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dtgVentas.Width, this.dtgVentas.Height);
            dtgVentas.DrawToBitmap(bm, new Rectangle(0, 0, this.dtgVentas.Width, this.dtgVentas.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
    }
}
