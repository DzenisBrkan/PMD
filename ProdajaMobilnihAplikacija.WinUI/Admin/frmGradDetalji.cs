//using ProdajaMobilnihAplikacija.Model.Requests;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

using ProdajaMobilnihAplikacija.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdajaMobilnihAplikacija.WinUI.Admin
{
    public partial class frmGradDetalji : Form
    {

        private readonly APIService _service = new APIService("Grad");
        private readonly APIService _serviceDrzava = new APIService("Drzava");

        private int? _id = null;
        public frmGradDetalji(int? gradId = null)
        {
            InitializeComponent();
            _id = gradId;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async Task LoadDrzava()
        {
            var result = await _serviceDrzava.Get<List<Model.Drzava>>(null);
            result.Insert(0, new Model.Drzava());
            cmbDrzava.DisplayMember = "Naziv";
            cmbDrzava.ValueMember = "DrzavaId";
            cmbDrzava.DataSource = result;
        }

        private async void frmGradDetalji_Load(object sender, EventArgs e)
        {
            await LoadDrzava();
            if (_id.HasValue)
            {

                var grad = await _service.GetById<Model.Grad>(_id);
                var drzava = await _serviceDrzava.GetById<Model.Drzava>(grad.DrzavaID);
                txtNaziv.Text = grad.Naziv;
                cmbDrzava.DisplayMember = drzava.Naziv;
            }
        }

        private async void btnSnimi_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren())
                {
                    var drzavaId = 0;
                    if (cmbDrzava.Text == "Bosna i Hercegovina")
                        drzavaId = 100;
                    else
                        drzavaId = 101;

                    var request = new GradUpsertRequest()
                    {
                        Naziv = txtNaziv.Text,
                        DrzavaId = drzavaId
                    };


                    if (_id.HasValue)
                    {
                        await _service.Update<Model.Grad>(_id, request);
                    }
                    else
                    {
                        await _service.Insert<Model.Grad>(request);
                    }

                    MessageBox.Show("Uspjesno ste dodali grad!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doslo je do greske!");
            }
        }

        private void txtNaziv_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaziv.Text) && txtNaziv.Text.Length < 3)
            {
                //errorProvider.SetError(Naziv, "Obavezno polje");
                MessageBox.Show("Obavezno polje");
                e.Cancel = true;
            }
            else
            {
                MessageBox.Show("Obavezno polje");
                //errorProvider.SetError(Naziv, null);
            }
        }
    }
}
