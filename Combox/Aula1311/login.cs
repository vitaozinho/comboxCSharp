using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Aula1311
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnlogar_Click(object sender, EventArgs e)
        {
             if (txtusuario.Text != string.Empty && txtsenha.Text != string.Empty)
            {
                verificar();
             }
              else {
                MessageBox.Show("Preencha todos os campos");
            }
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }


        public void limpar() {
            txtsenha.Clear();
            txtusuario.Clear();

        }


        private void verificar()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = Program.conexaoDB;
            conexao.Open();
           
           string sqlcomando = "select * from usuario where flogin = '" + txtusuario.Text + "' and fsenha = '" + txtsenha.Text + "' ";
           SqlCommand sqlCommand = new SqlCommand(sqlcomando, conexao);
           SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
           DataTable dtLogin = new DataTable();
           sda.Fill(dtLogin);
           if (dtLogin.Rows.Count == 1)
           {
               try
               {
                   if (Directory.Exists(@"C:\teste") == false)
                   {
                       Directory.CreateDirectory(@"C:\teste");
                   }


                   //System.IO.File.WriteAllText(@"C:\teste\teste.txt", "Arquivo Teste");

                   preencherLog(" //Usuário '" + txtusuario.Text + "'logou no sistema");


                   MessageBox.Show("Bem Vindo '" + txtusuario.Text + "'!");

                   TeladeCadastro tela01 = new TeladeCadastro();
                   this.Hide();
                   
                   tela01.Show();


               }
               catch (Exception ex)
               {
                   System.IO.File.WriteAllText("C:\teste\teste.txt", ex.Message.ToString());
               }

           }
           else
           {
               try
               {
                   if (Directory.Exists(@"C:\teste") == false)
                   {
                       Directory.CreateDirectory(@"C:\teste");
                   }


                   //System.IO.File.WriteAllText(@"C:\teste\teste.txt", "Arquivo Teste");

                   preencherLog(" //Usuário '" + txtusuario.Text + "'tentou logar no sistema");


                   limpar();
                   MessageBox.Show("Usario ou senha invalidos");
        


               }
               catch (Exception ex)
               {
                   System.IO.File.WriteAllText("C:\teste\teste.txt", ex.Message.ToString());
               }
           }
        
        }
        private void preencherLog(string Log)
        {
            using (StreamWriter writer = File.AppendText(@"C:\teste\teste.txt"))
            {
                
                writer.Write(Log + System.DateTime.Now.ToString());
                writer.Flush();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

    }
}
