using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tienda_Virtual
{
    public partial class frmFormularioCompra : Form
    {
        public frmFormularioCompra(DataGridView dtg_get, string correo_get, string subtotal_get, string iva_get, string total_get, string nombre_get, int idCliente_get)
        {
            InitializeComponent();
            dtgVentas = dtg_get;
            email.correoTo = correo_get;
            IVA = iva_get;
            Subtotal = subtotal_get;
            TotalVenta = total_get;
            Nombre = nombre_get;
            idCliente = idCliente_get;
        }

        #region Variables importantes
        Declaraciones decla = new Declaraciones();  //Objeto del tipo de la clase Declaraciones
        DataGridView dtgVentas;
        SendEmail email = new SendEmail();
        string TotalVenta, Subtotal, IVA, Nombre, tabla;
        public bool compraRealizada = false;
        int idCliente;
        #endregion

        public bool AccesoInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Estas Seguro de Comprar los Articulos Seleccionados?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    for (int i = 0; i < dtgVentas.Rows.Count - 1; i++)
                    {
                        tabla += "<tr>";
                        tabla += "<td align=center>" + dtgVentas.Rows[i].Cells[1].Value.ToString() + "</td><td>" + dtgVentas.Rows[i].Cells[2].Value.ToString() + "</td><td>$" + dtgVentas.Rows[i].Cells[3].Value.ToString() + "</td><td align=center>" + dtgVentas.Rows[i].Cells[4].Value.ToString() + "</td>";
                        try
                        {
                            decla.dosomething("UPDATE almacen SET Productos = (Productos -'" + Convert.ToInt32(dtgVentas.Rows[i].Cells[4].Value) + "') WHERE Nombre = '" + dtgVentas.Rows[i].Cells[1].Value.ToString() + "'");  //Llama al metodo que ejecuta acciones en la base de datos
                        }
                        catch (Exception ex)  //Si hay error
                        {
                            decla.Conexion.Close();  //Se cierra conexion
                            MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
                        }
                        tabla += "</tr>";
                    }
                    decla.Instruccion.Parameters.AddWithValue("idCliente", idCliente);
                    if (decla.dosomething("Delete from carrito where id_Cliente = @idCliente"))  //Llama al metodo que ejecuta acciones en la base de datos
                    {
                        if (AccesoInternet())
                        {
                            MessageBox.Show("Espere un momento, enviando datos de la compra", "Espere", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            email.subject = "Detalles de su compra con Pegasus Technology";
                            email.body = "<html>" +
                            "<body>"+
                                "<h3>Gracias por tu compra.</h3><br>"+
                                "<h4>" + txtNombre.Text + ".</h4><br>" +
                                "Tu transaccion ha finalizado y te hemos enviado un recibo de tu compra por correo"+
                                "electronico.<br>Un operario de nuestra tienda de contactara contigo<br>"+
                                "Saludos cordiales<br><br>" +
                                "<div id='cuerpo'>" +
                                    "<h1>Detalles de su compra ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " )</h1><br>" +
                                    "<table border=1>" +
                                        "<tr>" +
                                            "<td>Nombre</td>" +
                                            "<td>Descripci&oacute;n</td>" +
                                            "<td>Precio</td>" +
                                            "<td>Cantidad</td>" +
                                        "</tr>" + tabla +
                                    "</table>" +
                                    "<h2>El subtotal de su compra es: " + Subtotal + "<br>" +
                                    "El IVA de su compra es: " + IVA + "<br>" +
                                    "El total de su compra es: " + TotalVenta + "</h2><br>" +
                                    "<center><h3>Atte: Departamento de ventas</h3></center><br>" +
                                    "<center><img src='http://serlimcova.mx/Laterales.png'</center>" +
                                "</div>" +
                                "<style>" +
                                    "h3{font-size:15px;}" +
                                    "#cuerpo{background-color:#E6E6E6;}" +
                                "</style>" +
                            "</body></html>";
                            email.CreateEmail();
                            if (email.enviado)
                                MessageBox.Show("Datos de compra enviados satisfactoriamente", "Hecho", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            //Mandar correo
                        }
                        else
                            MessageBox.Show("No estas conectado a internet, aun asi tu compra se realizara", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        MessageBox.Show("Gracias " + Nombre + " tu compra fue realizada satisfactoriamente", "Compra realizada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);  //Se muestra mensaje de que se realizo todo
                        this.Close();
                        compraRealizada = true;
                    }
                    else  //Si existe algun problema
                        MessageBox.Show("Existe algun tipo de problema, cierre y abra otra ves el programa", "Problema", MessageBoxButtons.OK, MessageBoxIcon.Stop);  //Se muestra mensaje de error
                }
            }
            catch (Exception ex)  //Si hay error
            {
                decla.Conexion.Close();  //Se cierra conexion
                MessageBox.Show(ex.Message.ToString(), "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);  //Se muestra el error
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString();
        }

        #region Validaciones
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombre.Text.Length == 0)
                this.ValidarErrores.SetError(this.txtNombre, "Porfavor, Ingrese el Nombre del Socio.");
            else
                this.ValidarErrores.SetError(this.txtNombre, "");
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text.Length == 0)
                this.ValidarErrores.SetError(this.textBox2, "Porfavor, Ingrese un número de tarjeta valido.");
            else
                this.ValidarErrores.SetError(this.textBox2, "");
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (textBox3.Text.Length == 0)
                this.ValidarErrores.SetError(this.textBox3, "Porfavor, Ingrese un mes.");
            else
                this.ValidarErrores.SetError(this.textBox3, "");
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text.Length == 0)
                this.ValidarErrores.SetError(this.textBox4, "Porfavor, Ingrese un año.");
            else
                this.ValidarErrores.SetError(this.textBox4, "");
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (textBox5.Text.Length == 0)
                this.ValidarErrores.SetError(this.textBox5, "Porfavor, Ingrese un codigo de seguridad.");
            else
                this.ValidarErrores.SetError(this.textBox5, "");
        }
#endregion

        #region Solo numeros
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si es Cualquier otro Digito
            if (Char.IsDigit(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Espacio
            else if (e.KeyChar == 255)  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Cualquier tecla de Control
            else if (Char.IsControl(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Separador
            else if (Char.IsSeparator(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Número
            else  //Entra
                e.Handled = true;    // indica que el evento de KeyPress si se controló.
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si es Cualquier otro Digito
            if (Char.IsDigit(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Espacio
            else if (e.KeyChar == 255)  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Cualquier tecla de Control
            else if (Char.IsControl(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Separador
            else if (Char.IsSeparator(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Número
            else  //Entra
                e.Handled = true;    // indica que el evento de KeyPress si se controló.
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si es Cualquier otro Digito
            if (Char.IsDigit(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Espacio
            else if (e.KeyChar == 255)  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Cualquier tecla de Control
            else if (Char.IsControl(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Separador
            else if (Char.IsSeparator(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Número
            else  //Entra
                e.Handled = true;    // indica que el evento de KeyPress si se controló.
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si es Cualquier otro Digito
            if (Char.IsDigit(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Espacio
            else if (e.KeyChar == 255)  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Cualquier tecla de Control
            else if (Char.IsControl(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Separador
            else if (Char.IsSeparator(e.KeyChar))  //Entra
                e.Handled = false;    // indica que el evento de KeyPress no se controló.
            //Si es Número
            else  //Entra
                e.Handled = true;    // indica que el evento de KeyPress si se controló.
        }
        #endregion
    }
}
