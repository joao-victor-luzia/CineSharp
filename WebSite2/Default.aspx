<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Async="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Buscar Filmes</h3>
    <div class="form-inline">
        <div class="form-group">
            <label for="txtBusca">Titulo do Filme: </label>
            <asp:TextBox ID="txtBusca" runat="server" CssClass="form-control" placeholder="Ex: Batman"></asp:TextBox>
        </div>
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" CssClass="btn btn-primary" />
    </div>
    <hr />

    <h4>Resultados: </h4>
    <asp:Repeater ID="rptResultados" runat="server" OnItemCommand="rptResultados_ItemCommand">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>

        <ItemTemplate>
            <div class="col-md-4" style="margin-bottom: 30px; text-align: center; height: 600px;">
                <asp:HiddenField ID="hdnImdbID" runat="server" Value='<%# Eval("imdbID") %>' />
                <asp:HiddenField ID="hdnTitulo" runat="server" Value='<%# Eval("Titulo") %>' />
                <asp:HiddenField ID="hdnAno" runat="server" Value='<%# Eval("Ano") %>' />
                <asp:HiddenField ID="hdnPoster" runat="server" Value='<%# Eval("PosterURL") %>' />

                <img src='<%# Eval("PosterURL") %>' alt='<%# Eval("Titulo") %>' style="max-width: 100%;" />
                <h5><%# Eval("Titulo") %></h5>
                <p>Ano: <%# Eval("Ano") %></p>

                <asp:Button ID="btnSalvar" runat="server" Text="Favoritar" CommandName="Salvar" CommandArgument='<%# Eval("imdbID") %>' CssClass="btn btn-success btn-sm" />
            </div>
        </ItemTemplate>

        <FooterTemplate>
        </div>
    </FooterTemplate>
    </asp:Repeater>
    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
