using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListaTelefonica
{
    public partial class Form1 : Form
    {
        string[][] lista;
        readonly int MAX = 100;
        public Form1()
        {
            InitializeComponent();
            lista = new string[MAX][];

        }

        int Length(string[] s)
        {
            int itens = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != null)
                    itens++;
            }
            return itens;
        }

        int Length(string[][] s)
        {
            int itens = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != null)
                    itens++;
            }
            return itens;
        }
        void Atualizar()
        {
            dgvLista.Rows.Clear();
            for (int i = 0; i < Length(lista); i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvLista);
                for (int j = 0; j < Length(lista[i]); j++)
                {
                    row.Cells[j].Value = lista[i][j];
                }
                dgvLista.Rows.Add(row);
            }
        }
        private void btAdicionar_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(txtNome.Text) || !txtTel.MaskFull) && dgvLista.SelectedCells.Count != 0)
            {
                DataGridViewCell cell = dgvLista.SelectedCells[0];
                int linha = cell.RowIndex;
                txtNome.Text = dgvLista.CurrentRow.Cells[1].Value.ToString();
                txtTel.Text = dgvLista.CurrentRow.Cells[2].Value.ToString();
                return;
            }else if(dgvLista.SelectedCells.Count != 0 && !(String.IsNullOrWhiteSpace(txtNome.Text) || !txtTel.MaskFull))
            {
                DataGridViewCell cell = dgvLista.SelectedCells[0];
                int linha = cell.RowIndex;
                lista[linha][1] = txtNome.Text;
                lista[linha][2] = txtTel.Text;
                Atualizar();
                txtNome.Text = null;
                txtTel.Text = null;
                return;
            }
            if (Length(lista) >= MAX)
            {
                MessageBox.Show("Lista cheia!!!!!!!!!!!!!!!");
                return;
            }
            int id = 1;
            if(Length(lista) > 0)
            {
                id = int.Parse(lista[Length(lista)-1][0]) + 1;
            }
            lista[Length(lista)] = new string[] { id.ToString(), txtNome.Text, txtTel.Text };
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
            int indice = 0;
            for (indice = 0; indice < Length(lista) && lista[indice][0] != id; indice++);
            DialogResult r = MessageBox.Show($"Deseja MESMO retirar o contato de {lista[indice][1]}?","",MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                for(int i = indice; i<Length(lista)-1; i++) 
                {
                    lista[i] = lista[i + 1];
                }
                lista[Length(lista) - 1] = null;
                Atualizar();
            }


        }
    }
}
