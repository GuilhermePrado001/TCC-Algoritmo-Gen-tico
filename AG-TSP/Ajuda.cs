using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG_TSP
{
    public partial class Ajuda : Form
    {
        public Ajuda()
        {
            InitializeComponent();
        }

        private void BtnBR_Click(object sender, EventArgs e)
        {
            CultureInfo cultura = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = cultura;

            richTextBox1.Text = Resources.Resource.ajuda_dicas;
            como_usar.Text = Resources.Resource.como_usar;

        }

        private void BtnEN_Click(object sender, EventArgs e)
        {
            CultureInfo cultura = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = cultura;

            richTextBox1.Text = Resources.Resource.ajuda_dicas;
            como_usar.Text = Resources.Resource.como_usar;
        }
    }
}
