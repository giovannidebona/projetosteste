using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ProvaFramework1
{
    public partial class frmDecompositor : Form
    {
        public frmDecompositor()
        {
            InitializeComponent();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            GetNumeros();
        }

        private async void GetNumeros()
        {
            string controller = "numero";
            string medoto = "GetListDivisores";
            var numerico = int.TryParse(txtNumero.Text, out int numero);
            string URI = txtURLAPI.Text + "/v1/" + controller + "/" + medoto + "/" + numero;

            if (numerico && numero > 0)
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var retornoJSON = await response.Content.ReadAsStringAsync();
                            txtResultado.Text = retornoJSON;
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível obter os dados : " + response.StatusCode);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Informe um número inteiro maior do que 0.");
            }
        }
    }
}
