using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agenda.Entity;
using System.IO;

namespace Agenda.View
{
    public partial class frmAdicionarUsuario : Form
    {
        // variavel 
        private Entity.Usuario _Usuario;
        private string _Base64Foto;
        private byte[] _ByteFoto;

        public frmAdicionarUsuario()
        {
            InitializeComponent();
        }
        // função que recebe o objeto do usuario do form principal
        internal void Usuario(Entity.Usuario objUsuario)
        {
            // instância o obejto Usuario
            _Usuario = new Usuario();
            // alimenta a variavel com o parametro objUsuario
            _Usuario = objUsuario;

        }

        private void frmAdicionarUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                if (_Usuario.idusuario > 0)
                {

                }
                PopularCombos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Limpar()
        {
            try
            {
                txtNome.Text = string.Empty;
                txtSobrenome.Text = string.Empty;
                txtLogin.Text = string.Empty;
                txtSenha.Text = string.Empty;
                txtEmail.Text = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool VerificaCampo()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNome.Text))
                {
                    MessageBox.Show("Informe o nome do usuário!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtSobrenome.Text))
                {
                    MessageBox.Show("Informe o sobrenome!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtLogin.Text))
                {
                    MessageBox.Show("Informe o Login!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtSenha.Text))
                {
                    MessageBox.Show("Informe a Senha!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Informe o E-mail!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (cmbSexo.Text == string.Empty)
                {
                    MessageBox.Show("Selecione o Sexo!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if ((int)((System.Collections.Generic.KeyValuePair<int, string>)(cmbPerfil.SelectedValue)).Key == 0)
                {
                    MessageBox.Show("Selecione o Perfil!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if ((int)((System.Collections.Generic.KeyValuePair<int, string>)(cmbStatus.SelectedValue)).Key == 0)
                {
                    MessageBox.Show("Selecione o Status!", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }

        private void PopularCombos()
        {
            try
            {
                cmbSexo.Items.Add("M");
                cmbSexo.Items.Add("F");

                Dictionary<int, string> dicionario;
                dicionario = new Dictionary<int, string>();

                dicionario.Add(1, "Administrador");
                dicionario.Add(2, "Operador");
                dicionario.Add(3, "Suporte");

                cmbPerfil.DataSource = dicionario.ToList();

                Dictionary<int, string> dicionariostatus;
                dicionariostatus = new Dictionary<int, string>();

                dicionariostatus.Add(0, "Inativo");
                dicionariostatus.Add(1, "Ativo");

                cmbStatus.DataSource = dicionariostatus.ToList();

                //cmbPerfil.DisplayMember = "Operador";
                //cmbPerfil.ValueMember = "1";


                //html
                //<Select>
                //<options value = 1>Inativo</option>


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void BtnProcurarImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    //sr.ReadToEnd();
                    //sr.Close();

                    // ler os byte da imagem                  
                    _ByteFoto = File.ReadAllBytes(openFileDialog1.FileName);
                    // convert para base 64 modo 1
                    _Base64Foto = Convert.ToBase64String(_ByteFoto);
                    // convert para base 64 modo 2
                    _Base64Foto = BitConverter.ToString(_ByteFoto);
                    // faz o picbox ler ocaminho da imagem e exibir
                    PicBoxFoto.Load(openFileDialog1.FileName);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);  //throw;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Salvar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Salvar()
        {
            try
            {
                var checarUsuario = new Agenda.Core.Service.Service();

                if (checarUsuario.ChecarUsuario(txtLogin.Text))
                {
                    MessageBox.Show("Login já cadastrado. Utilize outro nome de login.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (VerificaCampo())
                {
                    var _objUsuario = new Entity.Usuario()
                    {
                        nome = txtNome.Text.ToUpper(),
                        sobrenome = txtSobrenome.Text.ToUpper(),
                        login = txtLogin.Text.ToUpper(),
                        senha = txtSenha.Text,
                        email = txtEmail.Text,
                        foto = _ByteFoto,
                        nivelAcesso = (int)((System.Collections.Generic.KeyValuePair<int, string>)(cmbPerfil.SelectedValue)).Key,
                        status = (int)((System.Collections.Generic.KeyValuePair<int, string>)(cmbStatus.SelectedValue)).Key,
                        sexo = cmbSexo.Text,
                        idUsuarioLogado = _Usuario.idUsuarioLogado,
                        loginLogado = _Usuario.login
                    };

                    var retornoCadastro = new Agenda.Core.Service.Service().CadastrarUsuario(_objUsuario);

                    if (retornoCadastro)
                    {
                        MessageBox.Show("Cadatrado com sucesso.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Dados não cadatrado, favor verifique.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }
}
