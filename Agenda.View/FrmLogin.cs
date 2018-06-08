using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agenda.View
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            try
            {
                TxtLogin.Text = string.Empty;
                TxtSenha.Text = string.Empty;
            }
            catch (Exception ex)
            {
                new Exception("Erro - " + ex.Message.ToString() + " - Contate o Suporte.");
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtLogin.Text) && !string.IsNullOrEmpty(TxtSenha.Text))
                {
                    MessageBox.Show("Informe o Login", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!string.IsNullOrEmpty(TxtSenha.Text) && string.IsNullOrEmpty(TxtSenha.Text))
                {
                    MessageBox.Show("Informe a Senha", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(TxtLogin.Text) && string.IsNullOrEmpty(TxtSenha.Text))
                {
                    MessageBox.Show("Informe Login e Senha", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {

                    var retorno = new Agenda.Core.Service.Service().LoginUsuario(TxtLogin.Text, TxtSenha.Text);

                    if (retorno != null)
                    {
                        if (retorno.idusuario > 0)
                        {
                            var frm = new FrmPrincipal();
                            frm.Iniciar(retorno);
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login ou Senha Incorreto.", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                new Exception("Erro - " + ex.Message.ToString() + " - Contate o Suporte.");
            }
        }
    }
}
