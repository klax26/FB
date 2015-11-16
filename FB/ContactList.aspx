<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="FB.ContactList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Список контактов</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>   <%--добавить класс--%>
        Список контактов
    </div>
    <%--<div runat="server" id="tableContent">--%>
        <table>
            <tr>
                <td>
                    Имя
                </td>
                <td>
                    Контакты
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox runat="server" ID="nameListBox" OnSelectedIndexChanged="nameListBox_IndexChanged" AutoPostBack="true">
                    </asp:ListBox>
                    <br />
                    <asp:Button runat="server" ID="addPersonButton" OnClick="addPersonButton_Click" Text="Добавить человека" />
                </td>
                <td>
                    <%--<div runat="server" id="contactContainer" enableviewstate="true">
                    </div>--%>
                    <asp:PlaceHolder runat="server" ID="contactContainer">
                        <%--<asp:Table ID="table" runat="server"></asp:Table>--%>
                    </asp:PlaceHolder>
                    <div>
                        <asp:Button runat="server" ID="addButton" OnClick="addButton_Click" Text="Добавить" />
                        <asp:Button runat="server" ID="saveButton" OnClick="saveButton_Click" Text="Сохранить" />
                        <asp:Button runat="server" ID="cancelButton" OnClick="cancelButton_Click" Text="Отменить изменения" />
                    </div>
                    <div>
                        <asp:MultiView runat="server" ID="MView">
                            <asp:View ID="View1" runat="server">
                                <asp:DropDownList runat="server" ID="newContactType">
                                    <asp:ListItem Value="1" Text="e-mail" />
                                    <asp:ListItem Value="2" Text="Телефон" />
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="newContactValue" />
                                <asp:Button runat="server" ID="addNewContact" Text="Добавить" OnClick="addNewContact_Click" />
                                <asp:Button runat="server" ID="cancelNewContact" Text="Отменить" OnClick="cancelNewContact_Click" />
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:TextBox runat="server" ID="newPersonName" />
                                <asp:Button runat="server" ID="newPersonSave" OnClick="newPersonSave_Click" Text="Сохранить" />
                                <asp:Button runat="server" ID="newPersonCancel" OnClick="newPersonCancel_Click" Text="Отмена" />
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </td>
            </tr>
        </table>
   <%-- </div>--%>
    </form>
</body>
</html>
