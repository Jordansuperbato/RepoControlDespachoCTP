using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDespachoCTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("Data Source=SRV_SALUD_VINA;Initial Catalog=BDIST;User ID=sysmk;Password=qT1OVkF7;Integrated Security=false;");
        private void button1_Click(object sender, EventArgs e)
        {

            txtUsuario.Text.ToUpper();

            if (txtUsuario.Text == "" && txtContra.Text == "")
            {

                MessageBox.Show("Debe ingresar Usuario y Contraseña");
                txtUsuario.Focus();
            }
            else
            {

                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM MAEPASSWORD WITH (NOLOCK) WHERE LOGIN='" + txtUsuario.Text + "' AND CLAVE = '" + txtContra.Text + "';", cn);
                    // SqlCommand cmd = new SqlCommand("select * from med_atencion  where fol_ate ='" + txtCargo.Text + "' and num_suc= '" + txtRut.Text + "';", cn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {


                        Globales.gbUsuario = Convert.ToString(dr["LOGIN"]);
                        MessageBox.Show("Bienvenido(a) " + Convert.ToString(dr["NOMBRE"]));

                      
                            Menucs men = new Menucs();
                            men.Show();
                            this.Hide();


                        


                    }
                    else
                    {
                        MessageBox.Show("Usuario MK no encontrado");

                    }

                    cn.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("error" + ex);
                  
                    cn.Close();
                }
            }





        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
