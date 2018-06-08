using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agenda.Entity;

namespace Agenda.View
{
    public partial class FrmPrincipal : Form
    {
        private Entity.Usuario _Usuario;
        private string PerilAcesso = string.Empty;


        public FrmPrincipal()
        {
            InitializeComponent();
        }

        internal void Iniciar(Entity.Usuario objUsuario)
        {
            _Usuario = new Usuario();
            _Usuario = objUsuario;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                Limpar();
                PopularCampo();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        private void Limpar()
        {
            LblUsuario.Text = string.Empty;
            LblPerfil.Text = string.Empty;        
        }

        private void PopularCampo()
        {
            try
            {
                LblUsuario.Text = _Usuario.login;

                switch (_Usuario.nivelAcesso)
                {
                    case 1:
                        LblPerfil.Text = Entity.Perfil.Administrador.ToString();
                        break;
                    case 2:
                        LblPerfil.Text = Entity.Perfil.Operador.ToString();
                        break;
                    case 3:
                        LblPerfil.Text = Entity.Perfil.Suporte.ToString();
                        break;
                }

                MessageBox.Show("Bem vindo.", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {                
                throw;
            }
        
        }

        private void adicionarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                // Criar a variavel frm e cria um instância do frmAdicionarUsuario
                var frm = new frmAdicionarUsuario();
                frm.Usuario(_Usuario);
                frm.ShowDialog();
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
