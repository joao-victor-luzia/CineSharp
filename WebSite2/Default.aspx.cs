using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using WebSite1;
using Newtonsoft.Json;
using System.Data.SqlClient;


public partial class _Default : Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected async void btnPesquisar_Click(object sender, EventArgs e)
    {
        string apiKey = ConfigurationManager.AppSettings["ApiKeyOMDb"];
        string termoBusca = txtBusca.Text;
        string url = $"http://www.omdbapi.com/?s={termoBusca}&apikey={apiKey}";
        string jsonResponse = "";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                jsonResponse = await client.GetStringAsync(url);
            }

            ResultadoBuscaFilme resultado = JsonConvert.DeserializeObject<ResultadoBuscaFilme>(jsonResponse);
            if (resultado != null && resultado.Response == "True")
            {
                rptResultados.DataSource = resultado.Filmes;
                rptResultados.DataBind();
                lblMensagem.Text = "";
            }
            else
            {
                rptResultados.DataSource = null;
                rptResultados.DataBind();
                lblMensagem.Text = "Nenhum filme com esse termo foi encontrado.";
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Houve um erro na pesquisa: " + ex.Message; 
        }
    }

    protected void rptResultados_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Salvar")
        {
            HiddenField hdnImdbID = (HiddenField)e.Item.FindControl("hdnImdbID");
            HiddenField hdnTitulo = (HiddenField)e.Item.FindControl("hdnTitulo");
            HiddenField hdnPosterURL = (HiddenField)e.Item.FindControl("hdnPoster");
            HiddenField hdnAno = (HiddenField)e.Item.FindControl("hdnAno");

            string imdbID = hdnImdbID.Value;
            string titulo = hdnTitulo.Value;
            string posterURL = hdnPosterURL.Value;
            string ano = hdnAno.Value;

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["ConexaoDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(conString))
                {
                    string sql = "INSERT INTO Favoritos (imdbID, Titulo, Ano, PosterURL) VALUES (@imdbID, @Titulo, @Ano, @PosterURL)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@imdbID", imdbID);
                        cmd.Parameters.AddWithValue("@Titulo", titulo);
                        cmd.Parameters.AddWithValue("@Ano", ano);
                        cmd.Parameters.AddWithValue("@PosterURL", posterURL);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                lblMensagem.ForeColor = System.Drawing.Color.Green;
                lblMensagem.Text = $"Filme '{titulo}' salvo com sucesso!";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMensagem.Text = "Este filme já está em seus favoritos!";
                }
                else
                {
                    lblMensagem.Text = "Erro ao salvar no banco: " + ex.Message;
                }
            }
        }
    }
}