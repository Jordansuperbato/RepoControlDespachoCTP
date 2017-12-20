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
    public partial class Revision : Form
    {
        public Revision()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            button3.Visible = false;
            button5.Visible = false;
           
        }
        SqlConnection cn = new SqlConnection("Data Source=SRV_SALUD_VINA;Initial Catalog=BDIST;User ID=sysmk;Password=qT1OVkF7;Integrated Security=false;");
        // SqlConnection cn = new SqlConnection("Data Source=NORTE_0_73;Initial Catalog=BDIST;User ID=sa;Password=jordan;Integrated Security=false;");
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menucs s = new Menucs();
            s.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var var1 = "";
            DateTime s = dateTimePicker1.Value;
            var fechaCorta = s.ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand("");
            try
            {
                cn.Open();
                string perro = ""+comboBox1.SelectedItem;
                if (perro!="Revisado")
                {
                    perro = "NULL";
                    cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo left join INGRESOLEY.dbo.MED_ALTAS a with(nolock) on i.atencion_NET COLLATE DATABASE_DEFAULT=a.FOL_ATE COLLATE DATABASE_DEFAULT where c.FECHA_IN='"+fechaCorta+"' and c.soc='4021' and c.FECHA_EG is not null and i.TIPO='U'and i.TIPOFILIACION='4' and a.IND_CTP='S' and k.estado IS NULL;", cn);
                    button3.Visible = true;
                    button5.Visible = false;
                }
                else
                {
                    perro = "'Revisado'";
                   cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo  where c.FECHA_IN = '" + fechaCorta + "' and k.estado =" + perro + ";", cn);
                    button3.Visible = false;
                    button5.Visible = true;
                }
              
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            dataGridView1.DataSource = dt;

               
                //if (dataGridView1.Columns[3].Equals('0'))
                //{
                   
                //}
            dataGridView1.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd";
            cn.Close();


                for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                {


                    if (dataGridView1.Rows[fila].Cells[3].Value.ToString() == "99")
                    {
                        dataGridView1.Rows[fila].Cells[3].Value = 0;

                        dataGridView1.Rows[fila].DefaultCellStyle.BackColor = Color.LightGreen;
                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("error"+ex);
                cn.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string folio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            string cargo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            try
            {
                cn.Open();
                SqlCommand cm = new SqlCommand("INSERT INTO MTE_BLK$ (Cargo,Folio,Tipo,CodSer,Estado,Usu_ing_reg,Usu_ult_modif,Fec_ing_reg)values('" + cargo+"','"+folio+"','U','Rev','Revisado','"+Globales.gbUsuario+"',NULL,'"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')",cn);
                SqlDataReader dr = cm.ExecuteReader();
                MessageBox.Show("Marcado como Revisado");

               
                cn.Close();



                DateTime s = dateTimePicker1.Value;// = "yyyy-MM-dd";
                var fechaCorta = s.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand("");
                try
                {
                    cn.Open();
                    string perro = "" + comboBox1.SelectedItem;
                    if (perro != "Revisado")
                    {
                        perro = "NULL";
                        cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion', i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico', c.RUT AS 'Rut Paciente', CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO = i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1 = m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo = k.cargo left join INGRESOLEY.dbo.MED_ALTAS a with(nolock) on i.atencion_NET COLLATE DATABASE_DEFAULT = a.FOL_ATE COLLATE DATABASE_DEFAULT where c.FECHA_IN = '" + fechaCorta+"' and c.soc = '4021' and c.FECHA_EG is not null and i.TIPO = 'U'and i.TIPOFILIACION = '4' and a.IND_CTP = 'S' and k.estado IS NULL; ", cn);
                        button3.Visible = true;
                        button5.Visible = false;
                    }
                    else
                    {
                        perro = "'Revisado'";
                        cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo  where c.FECHA_IN = '" + fechaCorta + "' and k.estado =" + perro + ";", cn);
                        button3.Visible = false;
                        button5.Visible = true;
                    }

                    //textBox1.Text="select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',c.nue_diasctp AS 'Dias Reposo',c.causal_ws AS 'Causal de Atencion',m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO where c.FECHA_IN='" + fechaCorta + "' and c.soc='4021' and c.FECHA_EG is not null and i.TIPO='U'and i.TIPOFILIACION='4' and c.nue_diasctp>=0;";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd";
                    cn.Close();
                    for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                    {


                        if (dataGridView1.Rows[fila].Cells[3].Value.ToString() == "99")
                        {
                            dataGridView1.Rows[fila].Cells[3].Value = 0;

                            dataGridView1.Rows[fila].DefaultCellStyle.BackColor = Color.LightGreen;
                        }


                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("error" + ex);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex);
                cn.Close();
            }


}

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime s = dateTimePicker1.Value;// = "yyyy-MM-dd";
            var fechaCorta = s.ToString("yyyy-MM-dd");
            string folio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            string cargo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            try
            {
                cn.Open();
                SqlCommand cm = new SqlCommand("DELETE FROM MTE_BLK$ WHERE CARGO='"+ cargo + "' AND TIPO='U'", cn);
               // textBox1.Text = "DELETE FROM MTE_BLK$ WHERE CARGO='" + cargo + "' AND TIPO='U'";
                SqlDataReader dr = cm.ExecuteReader();
                MessageBox.Show("Marcado como No Revisado");
               

                cn.Close();
                SqlCommand cmd = new SqlCommand("");
                try
                {
                    cn.Open();
                    string perro = "" + comboBox1.SelectedItem;
                    if (perro != "Revisado")
                    {
                        perro = "NULL";
                        cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion', i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico', c.RUT AS 'Rut Paciente', CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO = i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1 = m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo = k.cargo left join INGRESOLEY.dbo.MED_ALTAS a with(nolock) on i.atencion_NET COLLATE DATABASE_DEFAULT = a.FOL_ATE COLLATE DATABASE_DEFAULT where c.FECHA_IN = '" + fechaCorta + "' and c.soc = '4021' and c.FECHA_EG is not null and i.TIPO = 'U'and i.TIPOFILIACION = '4' and a.IND_CTP = 'S' and k.estado IS NULL; ", cn);
                        button3.Visible = true;
                        button5.Visible = false;
                    }
                    else
                    {
                        perro = "'Revisado'";
                        cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo  where c.FECHA_IN = '" + fechaCorta + "' and k.estado =" + perro + ";", cn);
                        button3.Visible = false;
                        button5.Visible = true;
                    }

                    //textBox1.Text="select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',c.nue_diasctp AS 'Dias Reposo',c.causal_ws AS 'Causal de Atencion',m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO where c.FECHA_IN='" + fechaCorta + "' and c.soc='4021' and c.FECHA_EG is not null and i.TIPO='U'and i.TIPOFILIACION='4' and c.nue_diasctp>=0;";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd";
                    cn.Close();
                    for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                    {


                        if (dataGridView1.Rows[fila].Cells[3].Value.ToString() == "99")
                        {
                            dataGridView1.Rows[fila].Cells[3].Value = 0;

                            dataGridView1.Rows[fila].DefaultCellStyle.BackColor = Color.LightGreen;
                        }


                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("error" + ex);
                    cn.Close();
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show("ERROR "+EX);
                cn.Close();
            }

        }
    }
}
