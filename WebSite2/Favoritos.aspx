<%@ Page Title="Favoritos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Favoritos.aspx.cs" Inherits="Favoritos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Meus Favoritos </h2>
    <asp:GridView ID="gvFavoritos" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" GridLines="None" 
        OnRowDeleting="gvFavoritos_RowDeleting" OnRowCancelingEdit="gvFavoritos_RowCancelingEdit" DataKeyNames="imdbID">
        <Columns>
            <asp:TemplateField HeaderText="Pôster" >
                <ItemTemplate>
                    <img src='<%# Eval("PosterURL") %>' style="width=100%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="imdbID" HeaderText="ID IMDb"/> 
            <asp:BoundField DataField="Titulo" HeaderText="Título" />
            <asp:BoundField DataField="Ano" HeaderText="Ano" />
            <asp:CommandField ShowDeleteButton="true" ShowSelectButton="true" ButtonType="Button" EditText="Alterar" DeleteText="Remover" HeaderText="Ações"/>
        </Columns>
    </asp:GridView>
</asp:Content>
