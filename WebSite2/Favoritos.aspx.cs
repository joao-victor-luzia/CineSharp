using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Favoritos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarFavoritos();
        }
    }

    private void CarregarFavoritos()
    {
        try
        {
            string conString = ConfigurationManager.ConnectionStrings["ConexaoDB"].ConnectionString;
            using(SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT Titulo, Ano, imdbID, PosterURL FROM Favoritos ORDER BY Titulo";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvFavoritos.DataSource = dt;
                    gvFavoritos.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvFavoritos_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvFavoritos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string imdbID = gvFavoritos.DataKeys[e.RowIndex].Value.ToString();
        // ou gvFavoritos.Rows[e.RowIndex].Cells[0];

        string conString = ConfigurationManager.ConnectionStrings["ConexaoDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString))
        {
            string sql = "DELETE FROM Favoritos WHERE imdbID = @imdbID";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@imdbID", imdbID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        CarregarFavoritos();
    }

    protected void gvFavoritos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}