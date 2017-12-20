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
    public partial class Historico_Revision : Form
    {
        public Historico_Revision()
        {
            InitializeComponent();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";

            
        }
        SqlConnection cn = new SqlConnection("Data Source=SRV_SALUD_VINA;Initial Catalog=BDIST;User ID=sysmk;Password=qT1OVkF7;Integrated Security=false;");
        //SqlConnection cn = new SqlConnection("Data Source=NORTE_0_73;Initial Catalog=BDIST;User ID=sa;Password=jordan;Integrated Security=false;");
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



            DateTime s = dateTimePicker1.Value;// = "yyyy-MM-dd";
            var fechaCorta = s.ToString("yyyy-MM-dd");
            DateTime x = dateTimePicker2.Value;// = "yyyy-MM-dd";
            var fechaCorta2 = x.ToString("yyyy-MM-dd");

            SqlCommand cmd = new SqlCommand("");
            try
            {
                cn.Open();
                string perro = "" + comboBox1.SelectedItem;
                if (perro != "Revisado")
                {
                    perro = "NULL";
                    
                    cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo left join INGRESOLEY.dbo.MED_ALTAS a with(nolock) on i.atencion_NET COLLATE DATABASE_DEFAULT=a.FOL_ATE COLLATE DATABASE_DEFAULT where c.FECHA_IN between '" + fechaCorta + "' and '" + fechaCorta2 + "' and c.soc='4021' and c.FECHA_EG is not null and i.TIPO='U'and i.TIPOFILIACION='4' and a.IND_CTP='S' and k.estado IS NULL;", cn);
                   
                   
                }
                else
                {
                    perro = "'Revisado'";
                   // cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',c.nue_diasctp AS 'Dias Reposo',m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo where c.FECHA_IN between '" + fechaCorta + "' and '" + fechaCorta2 + "' and c.soc='4021' and c.FECHA_EG is not null and i.TIPO='U'and i.TIPOFILIACION='4' and c.nue_diasctp>=0 and k.estado =" + perro + ";", cn);
                    cmd = new SqlCommand("select c.FECHA_IN AS 'Fecha de Atencion',i.atencion_NET AS 'Folio de Atencion', i.CARGO as 'Cargo',case when c.nue_diasctp=0 and c.nue_diascontrol=0  then '99' when c.nue_diasctp=0 then c.nue_diascontrol else c.nue_diasctp end AS 'Dias Reposo', m.nomdiagnostico AS 'Diagnostico',c.RUT AS 'Rut Paciente',CASE WHEN k.Estado IS NULL THEN 'No Revisado' else k.estado end as 'Estado' from INGRESO_FICHA c with(nolock) left join INGRESOCOMERCIALUDCA i with(nolock) on c.CARGO=i.CARGO left join MAEDIAGNOSTICOS m with(nolock) on c.COD_DIAGNOS1=m.CODIAGNOSTICO left join MTE_BLK$ k with(nolock) on i.cargo=k.cargo  where c.FECHA_IN between '" + fechaCorta + "' and '" + fechaCorta2 + "' and k.estado =" + perro + ";", cn);
                    
                  
                }
                

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

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    btn_exportar_Click(dataGridView1);
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {

                worksheet = workbook.ActiveSheet;

                worksheet.Name = "ExportedFromDatGrid";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Proceso exitoso");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }



    }
}
