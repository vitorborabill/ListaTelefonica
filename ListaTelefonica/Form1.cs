using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListaTelefonica.Models;   

namespace ListaTelefonica
{
    public partial class Form1 : Form
    {
        List<Contato> lista;
        string id = "";
        public Form1()
        {
            InitializeComponent();
            lista = new List<Contato>();

        }
        void Atualizar()
        {
            dgvLista.Rows.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvLista);
                row.Cells[0].Value = lista[i].Id; 
                row.Cells[1].Value = lista[i].Nome; 
                row.Cells[2].Value = lista[i].Telefone;            
                dgvLista.Rows.Add(row);
            }
        }
        private void btAdicionar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text) || !txtTel.MaskFull)
            {
               MessageBox.Show("Preencha os campos corretamente");
                return;
            }
            int id = 1;
            if (lista.Count > 0)
                id = lista.Max(c => c.Id) + 1;
            Contato novo = new Contato
            {
                Id = id,
                Nome = txtNome.Text.Trim(),
                Telefone = txtTel.Text.Trim()
            };

            lista.Add(novo);
            Atualizar();
            txtNome.Text = null;
            txtTel.Text = null;
            
        }
        

        private void btRemove_Click(object sender, EventArgs e)
        {
            if(dgvLista.SelectedCells.Count == 0)
            {
                MessageBox.Show("Selecione uma lista pra remove");
                return;
            }
            DataGridViewCell cell = dgvLista.SelectedCells[0];
            int linha = cell.RowIndex;
            string id = dgvLista.Rows[linha].Cells[0].Value.ToString();
            
            int indice = lista.FindIndex(c => c.Id + "" == id);
            DialogResult r = MessageBox.Show($"Deseja MESMO retirar o contato de {lista[indice].Nome}?","",MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                lista.RemoveAt(indice);
                Atualizar();
            }


        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgvLista.SelectedCells[0];
            int linha = cell.RowIndex;
            id = dgvLista.Rows[linha].Cells[0].Value.ToString();
        }
    }
}
