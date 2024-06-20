using MySqlConnector;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace ProjetoDePratica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bCadastrarM_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);
            try
            {
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "insert into morador (cpfmorador, nomemorador, unidademorador, blocomorador, emailmorador, telefonemorador) values(@cpf,@nome,@unidade,@bloco,@email,@telefone);";
                comando.Parameters.AddWithValue("@cpf", tbCPF.Text.Trim());
                comando.Parameters.AddWithValue("@nome", tbNome.Text.Trim());
                comando.Parameters.AddWithValue("@unidade", tbUnidade.Text.Trim());
                comando.Parameters.AddWithValue("@bloco", tbBloco.Text.Trim());
                comando.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                comando.Parameters.AddWithValue("@telefone", tbTelefone.Text.Trim());
                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("Erro ao inserir");
                else
                    MessageBox.Show("Cadastro realizado");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao banco de dados" + msqle.Message, "Erro");
            }//fim catch
            finally { conexao.Close(); }//fim finally
        }//fim botão cadastrar morador

        private void bConsultarM_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection con = new MySqlConnection(conn);
            con.Open();
            string comando = "Select * from morador where cpfmorador = @cpf";
            MySqlCommand com = new MySqlCommand(comando, con);
            com.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = tbCPF.Text;

            try
            {
                if (tbCPF.Text == "")
                {
                    throw new System.Exception("Você esqueceu de digitar o CPF");
                }//fim if

                MySqlDataReader cs = com.ExecuteReader();
                if (cs.HasRows == false)
                    throw new System.Exception("CPF não encontrado");
                else
                {
                    cs.Read();
                    tbCPF.Text = System.Convert.ToString(cs["cpfmorador"]);
                    tbNome.Text = System.Convert.ToString(cs["nomemorador"]);
                    tbUnidade.Text = System.Convert.ToString(cs["unidademorador"]);
                    tbBloco.Text = System.Convert.ToString(cs["blocomorador"]);
                    tbEmail.Text = System.Convert.ToString(cs["emailmorador"]);
                    tbTelefone.Text = System.Convert.ToString(cs["telefonemorador"]);
                }//fim else
            }//fim try
            catch (System.Exception E)
            {
                MessageBox.Show(E.Message);
            }//fim catch
        }//Fim Botão Consultar Morador

        private void bExcluirM_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);

            try
            {
                // int i;
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();

                comando.CommandText = "delete from morador where cpfmorador = @cpf;";
                comando.Parameters.AddWithValue("cpf", tbCPF.Text.Trim());
                MySqlDataReader reader = comando.ExecuteReader();
                MessageBox.Show("Cadastro deletado");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao MySQL" + msqle.Message, "Erro");
            }//fim catch
            finally
            {
                conexao.Close();
            }//fim finally
        }//fim botão excluir morador

        private void bAlterarM_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);

            try
            {
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "update morador set nomemorador=@nome, cpfmorador=@cpf, unidademorador=@unidade, blocomorador=@bloco, emailmorador=@email, telefonemorador=@telefone where cpfmorador=@cpf;";
                comando.Parameters.AddWithValue("@cpf", tbCPF.Text.Trim());
                comando.Parameters.AddWithValue("@nome", tbNome.Text.Trim());
                comando.Parameters.AddWithValue("@unidade", tbUnidade.Text.Trim());
                comando.Parameters.AddWithValue("@bloco", tbBloco.Text.Trim());
                comando.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                comando.Parameters.AddWithValue("@telefone", tbTelefone.Text.Trim());
                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("Erro ao atualizar");
                else
                    MessageBox.Show("Atualização realizada");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao MySQL " + msqle.Message, "Erro");
            }//fim catch
            finally
            {
                conexao.Close();
            }//fim finally
        }//fim botao alterar morador

        private void bCadastrarE_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);
            try
            {
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "insert into evento (tipoevento, pessoasevento, blocoevento, unidadeevento, dataevento) values(@tipo,@pessoas,@bloco,@unidade,@data);";
                comando.Parameters.AddWithValue("@tipo", comboBox1.Text.Trim());
                comando.Parameters.AddWithValue("@pessoas", numericUpDown1.Text.Trim());
                comando.Parameters.AddWithValue("@bloco", tbBlocoE.Text.Trim());
                comando.Parameters.AddWithValue("@unidade", tbUnidadeE.Text.Trim());
                //Converte a data selecionada no MonthCalendar para BR
                string dataFormatada = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
                comando.Parameters.AddWithValue("@data", dataFormatada);
                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("Erro ao inserir");
                else
                    MessageBox.Show("Cadastro realizado");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao banco de dados" + msqle.Message, "Erro");
            }//fim catch
            finally { conexao.Close(); }//fim finally
        }//fim botão cadastro evento

        private void bConsultarE_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection con = new MySqlConnection(conn);
            con.Open();
            string comando = "Select * from evento where dataevento = @data";
            MySqlCommand com = new MySqlCommand(comando, con);
            string dataFormatada = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
            com.Parameters.Add("@data", MySqlDbType.VarChar).Value = dataFormatada;

            try
            {
                MySqlDataReader cs = com.ExecuteReader();
                if (cs.HasRows == false)
                    throw new System.Exception("Não há evento na data selecionada");
                else
                {
                    cs.Read();
                    tbUnidadeE.Text = System.Convert.ToString(cs["unidadeevento"]);
                    tbBlocoE.Text = System.Convert.ToString(cs["blocoevento"]);
                    numericUpDown1.Text = System.Convert.ToString(cs["pessoasevento"]);
                    comboBox1.Text = System.Convert.ToString(cs["tipoevento"]);
                }//fim else
            }//fim try
            catch (System.Exception E)
            {
                MessageBox.Show(E.Message);
            }//fim catch
        }//botão consultar evento

        private void bExcluirE_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);

            try
            {
                // int i;
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();

                comando.CommandText = "delete from evento where dataevento = @data;";
                //Converte a data selecionada no MonthCalendar para BR
                string dataFormatada = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
                comando.Parameters.AddWithValue("@data", dataFormatada);
                MySqlDataReader reader = comando.ExecuteReader();
                MessageBox.Show("Cadastro deletado");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao MySQL" + msqle.Message, "Erro");
            }//fim catch
            finally
            {
                conexao.Close();
            }//fim finally
        }//fim botão excluir evento

        private void bAlterarE_Click(object sender, System.EventArgs e)
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);

            try
            {
                conexao.Open();
                MessageBox.Show("Conexão foi criada");
                MySqlCommand comando = new MySqlCommand();
                comando = conexao.CreateCommand();
                comando.CommandText = "update evento set dataevento=@novaData, unidadeevento=@unidade, blocoevento=@bloco, tipoevento=@tipo, pessoasevento=@pessoas where dataevento=@dataOriginal;";
                comando.Parameters.AddWithValue("@unidade", tbUnidadeE.Text.Trim());
                comando.Parameters.AddWithValue("@bloco", tbBlocoE.Text.Trim());
                comando.Parameters.AddWithValue("@tipo", comboBox1.Text.Trim());
                comando.Parameters.AddWithValue("@pessoas", numericUpDown1.Text.Trim());
                //Converte a data selecionada no MonthCalendar para BR
                string dataFormatada = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
                comando.Parameters.AddWithValue("@novaData", dataFormatada);
                string dataOriginal = monthCalendar1.SelectionEnd.ToString("dd/MM/yyyy");
                comando.Parameters.AddWithValue("@dataOriginal", dataOriginal);
                int valorRetorno = comando.ExecuteNonQuery();
                if (valorRetorno < 1)
                    MessageBox.Show("Erro ao atualizar");
                else
                    MessageBox.Show("Atualização realizada");
            }//fim try
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao MySQL " + msqle.Message, "Erro");
            }//fim catch
            finally
            {
                conexao.Close();
            }//fim finally
        }//fim botão alterar evento

        private MySqlDataAdapter da, da2;
        BindingSource bsource = new BindingSource();
        BindingSource bsource2 = new BindingSource();

        DataSet ds, ds2 = null;

        private void bCarregarE_Click(object sender, System.EventArgs e)
        {
            CarregarDadosEvento();
        }//fim botão carregar evento

        string sql, sql2;

        private void bCarregarM_Click(object sender, System.EventArgs e)
        {
            CarregarDadosMorador();
        }//fim botão carregar morador

        private void CarregarDadosEvento()
        {
            string conn = "Server=localhost; Database=bd;Uid=root;Pwd=81638221;Connect Timeout=30;";
            MySqlConnection conexao = new MySqlConnection(conn);
            sql = "Select * from evento";
            da = new MySqlDataAdapter(sql, conexao);
            conexao.Open();
            ds = new DataSet();
            new MySqlCommandBuilder(da);
            da.Fill(ds, "evento");
            bsource.DataSource = ds.Tables["evento"];
            dataGridView1.DataSource = bsource;

        }//fim CarregarDadosEvento

        private void CarregarDadosMorador()
        {
            string conn = "Server=localhost; Database=bd;Uid=root;Pwd=81638221;Connect Timeout=30;";
            MySqlConnection conexao = new MySqlConnection(conn);
            sql2 = "Select * from morador";
            da = new MySqlDataAdapter(sql2, conexao);
            conexao.Open();
            ds2 = new DataSet();
            new MySqlCommandBuilder(da);
            da.Fill(ds2, "morador");
            bsource2.DataSource = ds2.Tables["morador"];
            dataGridView1.DataSource = bsource2;
        }//fim CarregarDadosMorador

    }//fim class form1
}//fim projetodepratica
