using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FB
{
    public partial class ContactList : System.Web.UI.Page
    {
        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["FB"].ConnectionString; }
        }

        private Table table;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillList();
        }

        protected override void CreateChildControls()
        {
            if (nameListBox.SelectedIndex != -1)
            {
                contactContainer.Controls.Clear();
                BuildContactTable();
            }
            //base.CreateChildControls();
        }

        public override void DataBind()
        {
            CreateChildControls();
            //base.DataBind();
        }

        private void FillList()
        {
            var da = new SqlDataAdapter("SELECT [objID], [name] FROM [Person]", ConnectionString);
            var dt = new DataTable();

            da.Fill(dt);

            nameListBox.DataSource = dt;
            nameListBox.DataValueField = "objID";
            nameListBox.DataTextField = "name";

            nameListBox.DataBind();
        }

        protected void nameListBox_IndexChanged(object sender, EventArgs e)
        {

            this.DataBind();
            //contactContainer.Controls.Add(table);
        }

        private void BuildContactTable()
        {
            //table = new Table() { ID = "contactTable"/*, BorderStyle = BorderStyle.Solid, BorderWidth = 1 */};
            contactContainer.Controls.Clear();

            var query = string.Format(@"SELECT [c].[objID] as id, [c].[value] as value, [ct].[value] as type 
                                        FROM [Contact] [c] LEFT JOIN [ContactType] [ct] ON [c].[typeID] = [ct].[objID] 
                                        WHERE [c].[personID] = {0}", nameListBox.SelectedValue);

            var da = new SqlDataAdapter(query, ConnectionString);
            var dt = new DataTable();
            da.Fill(dt);

            table = new Table()
            {
                ID = "contactTable"
            };

            table.Rows.Add(BuildHeaderRow());

            foreach (DataRow item in dt.Rows)
            {
                var row = new TableRow() { ID = "Row" + item.ItemArray[0].ToString() };
                row.Attributes["cid"] = item.ItemArray[0].ToString();

                var cell = new TableCell() { ID = "cell1" + item.ItemArray[0].ToString() };
                cell.Text = item.ItemArray[2].ToString();
                row.Cells.Add(cell);

                cell = new TableCell() { ID = "cell2" + item.ItemArray[0].ToString() };
                cell.Controls.Add(new TextBox() { ID = "tb" + item.ItemArray[0].ToString(), Text = item.ItemArray[1].ToString() });
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }

            contactContainer.Controls.Add(table);
        }

        private TableRow BuildHeaderRow()
        {
            var row = new TableRow();
            var cell = new TableCell() { Text = "Тип контакта:" };
            row.Cells.Add(cell);
            cell = new TableCell() { Text = "Контакт:" };
            row.Cells.Add(cell);
            return row;
        }

        protected void addPersonButton_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = 1;
        }

        protected void newPersonCancel_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = -1;
        }

        protected void newPersonSave_Click(object sender, EventArgs e)
        {
            var query = string.Format("INSERT INTO [Person] (name) VALUES ('{0}')", newPersonName.Text);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            FillList();
            MView.ActiveViewIndex = -1;
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = 0;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            //var table = contactContainer.Controls[0] as Table;
            if (table == null)
                return;

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var query = string.Format("UPDATE [Contact] SET [value] = '{0}' WHERE objID = {1}", (table.Rows[i].Cells[1].Controls[0] as TextBox).Text, table.Rows[i].Attributes["cid"]);

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            nameListBox_IndexChanged(null, EventArgs.Empty);
        }

        protected void addNewContact_Click(object sender, EventArgs e)
        {
            var query = string.Format("INSERT INTO [Contact] (value, typeID, personID) VALUES ('{0}', {1}, {2})", newContactValue.Text, newContactType.SelectedValue, nameListBox.SelectedValue);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            MView.ActiveViewIndex = -1;
        }

        protected void cancelNewContact_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = -1;
        }
    }
}