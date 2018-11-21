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
    public partial class TeladeCadastro : Form
    {
        public TeladeCadastro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtcor.Clear();
            mskano.Clear();
        }

        private void TeladeCadastro_Load(object sender, EventArgs e)
        {
            carregarComboMarcas();
           
        }

        private void combomarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int.Parse(combomarcas.SelectedValue.ToString());
                carregarComboModelos();
            }
            catch { }
        }

        private void carregarComboMarcas() {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = Program.conexaoDB;
            conexao.Open();

            string sqlcomando = "select * from marca";
            SqlCommand sqlCommand = new SqlCommand(sqlcomando, conexao);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            DataTable dtMarca = new DataTable();
            sda.Fill(dtMarca);

            combomarcas.DataSource = dtMarca;
            combomarcas.DisplayMember = "nmMarca";
            combomarcas.ValueMember = "idMarca";
        }

        private void carregarComboModelos ()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = Program.conexaoDB;
            conexao.Open();

            string sqlcomando = "select * from modelo where idMarca ='" + combomarcas.SelectedValue+ "' ";
            SqlCommand sqlCommand = new SqlCommand(sqlcomando, conexao);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
            DataTable dtModelo = new DataTable();
            sda.Fill(dtModelo);

            combomodelos.DataSource = dtModelo;
            combomodelos.DisplayMember = "nmModelo";
        }

        private void combomodelos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void insert() {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = Program.conexaoDB;
            conexao.Open();

            string sqlcomando = "insert into vendas values ='" + combomarcas.SelectedValue + "'" +combomodelos.SelectedValue+ "'" +txtcor.Text +"'"+mskano.Text+"'";

            SqlCommand sqlCommand = new SqlCommand(sqlcomando, conexao);
            sqlCommand.ExecuteNonQuery();

        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            insert();
            try
            {
                if (Directory.Exists(@"C:\teste") == false)
                {
                    Directory.CreateDirectory(@"C:\teste");
                }


                //System.IO.File.WriteAllText(@"C:\teste\teste.txt", "Arquivo Teste");

                preencherLog(" //Usuário '" + txtusuario.Text + "'cadastrou algo no sistema");


            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("C:\teste\teste.txt", ex.Message.ToString());
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
    }
}
