using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DatosEmpresa
{
    public partial class Form1 : Form
    {
        private string _pathEmpleados = @"C:\Users\Fran\Documents\VISUAL STUDIO\COMUNNITY\Datos-main\ListaDeEmpleados.json";
        private int aux = 0;
        private List<Empleados> _datosEmpleado = new List<Empleados>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            try { _datosEmpleado = JsonConvert.DeserializeObject<List<Empleados>>(File.ReadAllText(_pathEmpleados)); }
            catch { MessageBox.Show("No pude leer el Json"); }
            //_datosEmpleado = Empleados.Lista();

            int i = 0;
            int j = 0;

            if(_datosEmpleado.Count > 0) 
            { 
                if(_datosEmpleado.Count>1) dtgvEmpleados.Rows.Add(_datosEmpleado.Count-1);

                for (i = 0; i < _datosEmpleado.Count; i++)
                {
                    for (j = 0; j < dtgvEmpleados.Columns.Count; j++)
                    {
                        if (j == 0) dtgvEmpleados[j, i].Value = _datosEmpleado[i].Nombre;
                        else if (j == 1) dtgvEmpleados[j, i].Value = _datosEmpleado[i].Apellido;
                        else if (j == 2) dtgvEmpleados[j, i].Value = _datosEmpleado[i].Cedula;
                    }
                }
            }


            /*int aux = dtgvEmpleados.Rows.Add();

            dtgvEmpleados.Rows[aux].Cells[0].Value = "Luis";
            dtgvEmpleados.Rows[aux].Cells[1].Value = "Galindez";
            dtgvEmpleados.Rows[aux].Cells[2].Value = "28.692.623";*/
        }

        private void btnAgregarVen_Click(object sender, EventArgs e)
        {

            int aux = dtgvEmpleados.Rows.Add();

            dtgvEmpleados.Rows[aux].Cells[0].Value = txtNombre.Text;
            dtgvEmpleados.Rows[aux].Cells[1].Value = txtApellido.Text;
            dtgvEmpleados.Rows[aux].Cells[2].Value = txtCI.Text;
            _datosEmpleado.Add(new Empleados(txtNombre.Text, txtApellido.Text, txtCI.Text));
            txtNombre.Text = " ";
            txtApellido.Text = " ";
            txtCI.Text = " ";
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (aux != -1 && dtgvEmpleados.Rows.Count > 0)
                {
                dtgvEmpleados.Rows.RemoveAt(aux);
                }
                else
                {
                    throw new Exception("No se pueden eliminar m�s art�culos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dtgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (dtgvEmpleados.Rows.Count > 0)
            {
                string empleadosJson = JsonConvert.SerializeObject(_datosEmpleado.ToArray(), Formatting.Indented);
                File.WriteAllText(_pathEmpleados, empleadosJson);
                MessageBox.Show("Guardado Exitosamente");
            }
            else MessageBox.Show("no hay nada que guardar");
        }
    }
}