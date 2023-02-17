using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ado5;

public partial class Form1 : Form
{
    // connection string add olunub ADO-5.dll.config filesine hansi ki, exe filenin oldugu yerde yerlesir

    DbConnection? conn = null;
    DbProviderFactory? factory = null;
    DbDataAdapter? adapter = null;
    DbCommand? cmd = null;
    DataTable? dataTable = null;

    string? connection = default, pName = default, categoryName = default;
    int authorId = default;

    public Form1()
    {
        ///Configuration String connection add method. If you want to add key and value 
        ///use this method before initialize 
        ///or go to exe file location and config file add by hand 

        //ConnectionStringAdd("P_SQl", "");

        InitializeComponent();

        connection = ConfigurationManager.AppSettings.Get("KeyS");

        cboxProvider.Items.Add("Sql");
        cboxProvider.Items.Add("OleDb");
    }
    private void AdapterConfig(string ProviderName)
    {
        try
        {
            factory = DbProviderFactories.GetFactory(ProviderName);
            adapter = factory!.CreateDataAdapter();
            cmd = factory.CreateCommand();
            conn = factory.CreateConnection();
            conn!.ConnectionString = connection;
            cmd!.Connection = conn;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private DataTable AuthorsFillMethod(string commandText, DataTable dt)
    {
        try
        {
            cmd!.CommandText = commandText;
            adapter!.SelectCommand = cmd;
            adapter.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null!;
        }
        finally
        {
            cmd!.Dispose();
            adapter!.Dispose();
            conn?.Dispose();
        }
    }
    private DataTable CategoriesFillMethod(string commandText, DataTable dt, string parametr)
    {
        try
        {
            AdapterConfig(pName!);

            cmd!.CommandText = commandText;
            cmd.Parameters.Add(new SqlParameter("@id", parametr));

            if (adapter is not null)
            {
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null!;
        }
        finally
        {
            cmd?.Parameters.Clear();
            cmd?.Dispose();
            adapter?.Dispose();
            conn?.Dispose();
        }
    }
    private DataTable BooksFillMethod(DataTable dt, string categoryName, int authorId)
    {
        try
        {
            AdapterConfig(pName!);

            cmd!.CommandText = "usp_BooksPrinter";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@categoryName", categoryName));
            cmd.Parameters.Add(new SqlParameter("@authorsId", authorId));

            adapter!.SelectCommand = cmd;
            adapter.Fill(dt);

            Thread.Sleep(2000); 

            return dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null!;
        }
        finally
        {
            cmd?.Parameters.Clear();
            cmd?.Dispose();
            adapter?.Dispose();
            conn?.Dispose();
        }
    }
    private async void AuthorsReader()
    {
        string cmdText = "SELECT * FROM Authors WAITFOR DELAY '00:00:02'";

        DataTable dt = new();
        cboxAuthors.Items.Clear();

        await Task.Run(() =>
            AuthorsFillMethod(cmdText, dt)
        );

        foreach (DataRow row in dt.Rows)
            cboxAuthors.Items.Add(row[0] + ". " + row[1] + ' ' + row[2]);
    }
    public async void CategoriesReader()
    {
        string cmdText = @"SELECT DISTINCT Categories.Name FROM Books
JOIN Authors
ON Books.Id_Author = Authors.Id
JOIN Categories
ON Categories.Id = Books.Id_Category
WHERE Authors.Id = @id
WAITFOR DELAY '00:00:02'";

        DataTable dt = new();

        StringBuilder? value = new();

        cboxCategories.Items.Clear();

        for (int i = 0; i < cboxAuthors.SelectedItem.ToString()?.Length; i++)
        {
            if (cboxAuthors.SelectedItem.ToString()?[i].ToString() != ".")
                value.Append(cboxAuthors.SelectedItem.ToString()?[i].ToString());
            else
                break;
        }

        await Task.Run(() =>
            CategoriesFillMethod(cmdText, dt, value.ToString())
        );

        foreach (DataRow row in dt.Rows)
            cboxCategories.Items.Add(row[0]);
    }
    public async void BooksReader()
    {
        DataTable dt = new();

        StringBuilder? value = new();

        for (int i = 0; i < cboxAuthors.SelectedItem.ToString()?.Length; i++)
        {
            if (cboxAuthors.SelectedItem.ToString()?[i].ToString() != ".")
                value.Append(cboxAuthors.SelectedItem.ToString()?[i].ToString());
            else
                break;
        }

        authorId = int.Parse(value.ToString());

        await Task.Run(() =>
            BooksFillMethod(dt, categoryName!, authorId)
        );

        dataTable = dt;

        ListViewMapping(dt);
    }
    private void ListViewMapping(DataTable dt)
    {
        listView1.Items.Clear();
        listView1.Columns.Clear();
        listView1.View = View.Details;
        listView1.Columns.Add("");
        listView1.Columns.Add("Number  ");
        listView1.Columns.Add("Books Id  ");
        listView1.Columns.Add("Books Name  ");
        listView1.Columns.Add("FullName of Authors  ");
        listView1.Columns.Add("Category  ");
        listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        dataTable = dt;

        int line = 0;
        foreach (DataRow row in dt!.Rows)
        {
            ListViewItem item = new ListViewItem("X");
            
            item.ForeColor = Color.Red;
            item.SubItems.Add((++line).ToString()).ForeColor = Color.DarkKhaki;
            item.UseItemStyleForSubItems = false;

            for (int i = 0; i < dt.Columns.Count; i++)
                item.SubItems.Add(row[i].ToString()).ForeColor = Color.DarkKhaki;
            
            listView1.Items.Add(item);
        }

        adapter.Dispose();
        cmd.Parameters.Clear();
        cmd.Dispose();
        conn.Dispose();

    }
    private void SearchBooks()
    {
        try
        {
            DataTable dt = new();

            AdapterConfig(pName!);
            cmd!.CommandText = "usp_SearchBooks";
            cmd.CommandType = CommandType.StoredProcedure;

            if (int.TryParse(txtBoxSearch.Text, out int id))
                cmd.Parameters.Add(new SqlParameter("@booksId", id));
            else
                cmd.Parameters.Add(new SqlParameter("@booksName", txtBoxSearch.Text));

            dt.TableName = "MyTable";

            adapter!.SelectCommand = cmd;
            adapter.Fill(dt);

            ListViewMapping(dt);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            cmd?.Parameters.Clear();
            cmd?.Dispose();
            adapter?.Dispose();
            conn?.Dispose();
        }
    }

    private void txtBoxSearch_Click(object sender, EventArgs e)
    {
        txtBoxSearch.Clear();
        button1.Enabled = true;
        if (!listView1.Enabled)
            listView1.Enabled = true;
    }

    private void DeleteItems(int id)
    {
        string text = @"DELETE Books
WHERE Books.Id = @Id";

        AdapterConfig(pName);

        cmd!.CommandText = text;
        cmd.Parameters.Add(new SqlParameter("@Id", id));
        adapter!.DeleteCommand = cmd;

        adapter.Update(dataTable!);
    }

    private void listView1_DoubleClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in listView1.SelectedItems)
        {
            listView1.Items.Remove(item);
            DeleteItems(int.Parse(item.SubItems[2].Text));
        }
    }

    private void button_Click(object sender, EventArgs e)
    {
        var btn = sender as Button;
        SearchBooks();
    }

    private void cbox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboBox? cbox = sender as ComboBox;

        if (cbox == cboxProvider)
        {
            if (cboxProvider.SelectedItem.ToString() == "Sql")
            {
                pName = ConfigurationManager.AppSettings.Get(cboxProvider.SelectedItem.ToString())!;
                if (pName == null)
                    return;
                else
                {
                    DbProviderFactories.RegisterFactory(pName, typeof(SqlClientFactory));

                    AdapterConfig(pName);
                    txtBoxSearch.Enabled = true;
                    cboxAuthors.Enabled = true;

                    AuthorsReader();
                }
            }
            else if (cboxProvider.SelectedItem.ToString() == "OleDb")
            {
                MessageBox.Show("OleDb will be here soon..)");
                cboxProvider.SelectedIndex = 0;
            }
        }
        else if (cbox == cboxAuthors)
        {
            cboxCategories.Enabled = true;
            CategoriesReader();
        }
        else if (cbox == cboxCategories)
        {
            categoryName = cboxCategories.SelectedItem.ToString();
            listView1.Enabled = true;
            BooksReader();
        }
    }


}
