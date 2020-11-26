using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace curs3laba4
{
    /// <summary>
    /// Сводное описание для LibService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class LibService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet GetAllData()
        {
            DataSet ds = new DataSet();

            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                var outputs = db.Outputs;
                var readers = db.Readers;
                var books = db.Books;

                var col = new List<string>() { "ID", "ID Читателя", "ФИО Читателя" ,
                    "Год рожд. Чит-ля", "Пасспорт Чит-ля",  "ID Кн.", "Название Кн.", "Автор Кн.",
                "Дата издания", "Дата написания", "Дата выдачи", "Последний срок приема", "Дней до просрочки"};
                DataTable table = new DataTable("Outputs");

                foreach (var c in col)
                    table.Columns.Add(c);

                foreach (var o in outputs)
                {
                    var R = new Readers();
                    foreach (var r in readers)
                        if (r.r_id == o.R_id)
                            R = r;
                    var B = new Books();
                    foreach (var b in books)
                        if (b.b_id == o.B_id)
                            B = b;
                    table.Rows.Add(
                        o.o_id,
                        R.r_id,
                        R.r_fio,
                        R.r_dt_birth,
                        R.r_passport,
                        B.b_id,
                        B.b_name,
                        B.b_author,
                        B.b_publ,
                        B.b_born,
                        o.o_dt_out,
                        o.o_dt_in,
                        o.o_dt_in - o.o_dt_out
                        );
                }

                ds.Tables.Add(table);
            }
            return ds;

            /*   string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
           string query = "SELECT Outputs.o_id, Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport, Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born  As VarChar(11)), Cast(Outputs.o_dt_out As VarChar(11)), Cast(Outputs.o_dt_in As VarChar(11))," +
               "Year(Outputs.o_dt_in)*12*30+Month(Outputs.o_dt_in)*30+Day(Outputs.o_dt_in)-Year(Outputs.o_dt_out)*12*30-Month(Outputs.o_dt_out)*30-Day(Outputs.o_dt_out) FROM Readers, Books, Outputs where Outputs.R_id = Readers.r_id and Outputs.B_id = Books.b_id";
           DataSet ds = new DataSet();
           using (SqlConnection con = new SqlConnection(path))
           {
               SqlDataAdapter da = new SqlDataAdapter(query, con);
               da.Fill(ds, "Readers");
           }
           ds.Tables["Readers"].Columns[0].ColumnName = "ID";
           ds.Tables["Readers"].Columns[1].ColumnName = "ID Читателя";
           ds.Tables["Readers"].Columns[2].ColumnName = "ФИО Читателя";
           ds.Tables["Readers"].Columns[3].ColumnName = "Год рожд. Чит-ля";
           ds.Tables["Readers"].Columns[4].ColumnName = "Пасспорт Чит-ля";
           ds.Tables["Readers"].Columns[5].ColumnName = "ID Кн.";
           ds.Tables["Readers"].Columns[6].ColumnName = "Название Кн.";
           ds.Tables["Readers"].Columns[7].ColumnName = "Автор Кн.";
           ds.Tables["Readers"].Columns[8].ColumnName = "Дата издания";
           ds.Tables["Readers"].Columns[9].ColumnName = "Дата написания";
           ds.Tables["Readers"].Columns[10].ColumnName = "Дата выдачи";
           ds.Tables["Readers"].Columns[11].ColumnName = "Последний срок приема";
           ds.Tables["Readers"].Columns[12].ColumnName = "Дней до просрочки";
           return ds;*/
        }

        [WebMethod]
        public DataSet GetDataToOrFrom(DateTime time, bool from = true)
        {
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Outputs.o_id, Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport, Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born  As VarChar(11)), Cast(Outputs.o_dt_out As VarChar(11)), Cast(Outputs.o_dt_in As VarChar(11)),Year(Outputs.o_dt_in)*12*30+Month(Outputs.o_dt_in)*30+Day(Outputs.o_dt_in)-Year(Outputs.o_dt_out)*12*30-Month(Outputs.o_dt_out)*30-Day(Outputs.o_dt_out) FROM Readers, Books, Outputs where Outputs.R_id = Readers.r_id and Outputs.B_id = Books.b_id ";
            if (from)
            {
                query += " and Outputs.o_dt_out >= " + "\'" + time + "\'";
            }
            else
            {
                query += " and Outputs.o_dt_out <= " + "\'" + time + "\'";
            }
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
            }
            ds.Tables["Readers"].Columns[0].ColumnName = "ID";
            ds.Tables["Readers"].Columns[1].ColumnName = "ID Читателя";
            ds.Tables["Readers"].Columns[2].ColumnName = "ФИО Читателя";
            ds.Tables["Readers"].Columns[3].ColumnName = "Год рожд. Чит-ля";
            ds.Tables["Readers"].Columns[4].ColumnName = "Пасспорт Чит-ля";
            ds.Tables["Readers"].Columns[5].ColumnName = "ID Кн.";
            ds.Tables["Readers"].Columns[6].ColumnName = "Название Кн.";
            ds.Tables["Readers"].Columns[7].ColumnName = "Автор Кн.";
            ds.Tables["Readers"].Columns[8].ColumnName = "Дата издания";
            ds.Tables["Readers"].Columns[9].ColumnName = "Дата написания";
            ds.Tables["Readers"].Columns[10].ColumnName = "Дата выдачи";
            ds.Tables["Readers"].Columns[11].ColumnName = "Последний срок приема";
            ds.Tables["Readers"].Columns[12].ColumnName = "Дней до просрочки";
            return ds;
        }

        [WebMethod]
        public DataSet GetDataToAndFrom(DateTime from, DateTime to)
        {
            DataSet ds = new DataSet();
            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                var outputs = db.Outputs;
                var readers = db.Readers;
                var books = db.Books;

                var col = new List<string>() { "ID", "ID Читателя", "ФИО Читателя" ,
                    "Год рожд. Чит-ля", "Пасспорт Чит-ля",  "ID Кн.", "Название Кн.", "Автор Кн.",
                "Дата издания", "Дата написания", "Дата выдачи", "Последний срок приема", "Дней до просрочки"};
                DataTable table = new DataTable("Outputs");

                foreach (var c in col)
                    table.Columns.Add(c);

                foreach (var o in outputs)
                {
                    if (from <= o.o_dt_out && o.o_dt_out <= to)
                    {
                        var R = new Readers();
                        foreach (var r in readers)
                            if (r.r_id == o.R_id)
                                R = r;
                        var B = new Books();
                        foreach (var b in books)
                            if (b.b_id == o.B_id)
                                B = b;
                        table.Rows.Add(
                            o.o_id,
                            R.r_id,
                            R.r_fio,
                            R.r_dt_birth,
                            R.r_passport,
                            B.b_id,
                            B.b_name,
                            B.b_author,
                            B.b_publ,
                            B.b_born,
                            o.o_dt_out,
                            o.o_dt_in,
                            o.o_dt_in - o.o_dt_out
                            );
                    }
                }

                ds.Tables.Add(table);
            }
            return ds;
            /*string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Outputs.o_id, Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport, Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born  As VarChar(11)), Cast(Outputs.o_dt_out As VarChar(11)), Cast(Outputs.o_dt_in As VarChar(11))," +
                "Year(Outputs.o_dt_in)*12*30+Month(Outputs.o_dt_in)*30+Day(Outputs.o_dt_in)-Year(Outputs.o_dt_out)*12*30-Month(Outputs.o_dt_out)*30-Day(Outputs.o_dt_out) FROM Readers, Books, Outputs where Outputs.R_id = Readers.r_id and Outputs.B_id = Books.b_id  and Outputs.o_dt_out >= " + "\'" + from + "\'" + " and Outputs.o_dt_out <= " + "\'" + to + "\'";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
            }
            ds.Tables["Readers"].Columns[0].ColumnName = "ID";
            ds.Tables["Readers"].Columns[1].ColumnName = "ID Читателя";
            ds.Tables["Readers"].Columns[2].ColumnName = "ФИО Читателя";
            ds.Tables["Readers"].Columns[3].ColumnName = "Год рожд. Чит-ля";
            ds.Tables["Readers"].Columns[4].ColumnName = "Пасспорт Чит-ля";
            ds.Tables["Readers"].Columns[5].ColumnName = "ID Кн.";
            ds.Tables["Readers"].Columns[6].ColumnName = "Название Кн.";
            ds.Tables["Readers"].Columns[7].ColumnName = "Автор Кн.";
            ds.Tables["Readers"].Columns[8].ColumnName = "Дата издания";
            ds.Tables["Readers"].Columns[9].ColumnName = "Дата написания";
            ds.Tables["Readers"].Columns[10].ColumnName = "Дата выдачи";
            ds.Tables["Readers"].Columns[11].ColumnName = "Последний срок приема";
            ds.Tables["Readers"].Columns[12].ColumnName = "Дней до просрочки";
            return ds;*/
        }

        [WebMethod]
        public DataSet GetDataReaders()
        {
            DataSet ds = new DataSet();
            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                var readers = db.Readers;
                var col = new List<string>() { "ID Читателя", "ФИО Читателя" ,
                    "Год рожд. Чит-ля", "Пасспорт Чит-ля"};

                DataTable table = new DataTable("Readers");
                foreach (var c in col)
                    table.Columns.Add(c);
                foreach (var r in readers)
                {
                    table.Rows.Add(
                            r.r_id,
                            r.r_fio,
                            r.r_dt_birth,
                            r.r_passport);
                }
                ds.Tables.Add(table);
            }
            return ds;
            /*string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport FROM Readers";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
                ds.Tables["Readers"].Columns[0].ColumnName = "ID";
                ds.Tables["Readers"].Columns[1].ColumnName = "ФИО";
                ds.Tables["Readers"].Columns[2].ColumnName = "Дата рождения";
                ds.Tables["Readers"].Columns[3].ColumnName = "Паспорт";
            }
            return ds;*/
        }

        [WebMethod]
        public DataSet GetDataBooks()
        {
            DataSet ds = new DataSet();
            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                var books = db.Books;
                var col = new List<string>() { "ID Кн.", "Название Кн.", "Автор Кн.",
                "Дата издания", "Дата написания"};
                DataTable table = new DataTable("Books");
                foreach (var c in col)
                    table.Columns.Add(c);
                foreach (var b in books)
                {
                    table.Rows.Add(
                            b.b_id,
                            b.b_name,
                            b.b_author,
                            b.b_publ,
                            b.b_born);
                }
                ds.Tables.Add(table);
            }
            return ds;
            /*
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born As VarChar(11)) FROM Books";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Books");
                ds.Tables["Books"].Columns[0].ColumnName = "ID";
                ds.Tables["Books"].Columns[1].ColumnName = "Название Кн.";
                ds.Tables["Books"].Columns[2].ColumnName = "Автор Кн.";
                ds.Tables["Books"].Columns[3].ColumnName = "Дата издания";
                ds.Tables["Books"].Columns[4].ColumnName = "Дата написания";
            }
            return ds;*/
        }

        [WebMethod]
        public string NewRec(int id, int r_id, int b_id, DateTime o_dt_out, DateTime o_dt_in)
        {
            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                Outputs o = new Outputs { o_id = id, R_id = r_id, B_id = b_id, o_dt_out = o_dt_out, o_dt_in = o_dt_in };
                db.Outputs.Add(o);
                db.SaveChanges();
            }
            /*
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = string.Format("INSERT into Outputs(o_id, R_id, B_id, o_dt_out, o_dt_in) VALUES(@id, @r_id, @b_id, @o_dt_out, @o_dt_in)");
            var str = "";
            using (var conn = new SqlConnection(path))
            {
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@r_id", r_id);
                    cmd.Parameters.AddWithValue("@b_id", b_id);
                    cmd.Parameters.AddWithValue("@o_dt_out", o_dt_out);
                    cmd.Parameters.AddWithValue("@o_dt_in", o_dt_in);
                    conn.Open();
                    str = "Изменено " + cmd.ExecuteNonQuery() + " строк в таблице Outputs";
                }
            }*/


            return "Изменена 1 строка в таблице Outputs";
        }

        [WebMethod]
        public string DeleteRec(int ID)
        {
            using (epiclibraryEntities db = new epiclibraryEntities())
            {
                Outputs o = db.Outputs.FirstOrDefault(a => a.o_id == ID);
                if (o != null)
                {
                    db.Outputs.Remove(o);
                    db.SaveChanges();
                }
            }
            /*
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = string.Format("DELETE from Outputs where o_id = \'"+ID+"\'");
            var str = "";
            using (var conn = new SqlConnection(path))
            {
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    str = "Изменено " + cmd.ExecuteNonQuery() + " строк в таблице Outputs";
                }
            }
            */
            return "Изменена 1 строка в таблице Outputs";
        }
    }
}
