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
        string[,] lista;
        readonly int MAX = 100;
        int itens = 0;
        public Form1()
        {
            InitializeComponent();
            lista = new string[MAX, 2];

        }

        

        private void btAdicionar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text) || !txtTel.MaskFull)
            {
                MessageBox.Show("Insira o nome e telefone");
                return;
            }
            lista[itens, 0]= txtNome.Text;
            lista[itens, 1]= txtTel.Text;
            itens++;
            Atualizar();

        }
        void Atualizar()
        {
            dgvLista.Rows.Clear();
            for (int i = 0; i < itens; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvLista);
                for (int j = 0; j < 2; j++)
                {
                    row.Cells[j].Value = lista[i, j];
                }
                dgvLista.Rows.Add(row);
            }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = dgvLista.SelectedCells[0];
            int indice = cell.RowIndex;
        }
    }
}
