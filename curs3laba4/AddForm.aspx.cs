using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curs3laba4
{
    public partial class AddForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var serv = new RefLibraryLast.ServiceForLibrarySoapClient();
            var dsreaders = serv.GetDataReaders();
            for (int i = 0; i < dsreaders.Tables[0].Rows.Count; i++)
            {
                (DropDownListReaders as ListControl).Items.Add(string.Join("; ", dsreaders.Tables[0].Rows[i].ItemArray));
            }
            var dsbooks = serv.GetDataBooks();
            for (int i = 0; i < dsbooks.Tables[0].Rows.Count; i++)
            {
                (DropDownListBooks as ListControl).Items.Add(string.Join("; ", dsbooks.Tables[0].Rows[i].ItemArray));
            }
            var dsids = serv.GetAllData();
            var a = new List<int>();
            for (int i = 0; i < dsids.Tables[0].Rows.Count; i++)
            {
                var s = dsids.Tables[0].Rows[i].ItemArray[0];
                    a.Add(int.Parse(dsids.Tables[0].Rows[i].ItemArray[0].ToString()));
            }
            var mx = a.Max() + 1;
            TextID.Text = "" + mx;
            //(DropDownListReaders as ListControl).Items.Add
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var serv = new RefLibraryLast.ServiceForLibrarySoapClient();
            var all = serv.GetAllData().Tables[0];
            if (CalendarFrom.SelectedDate.ToShortDateString() != "01.01.0001"
                && CalendarTo.SelectedDate.ToShortDateString() != "01.01.0001")
            {
                var fdt = CalendarFrom.SelectedDate;
                var tdt = CalendarTo.SelectedDate;
                var o_id = int.Parse(TextID.Text);
                var r_id = int.Parse(DropDownListReaders.Text.Split(';')[0]);
                var b_id = int.Parse(DropDownListBooks.Text.Split(';')[0]);
                var ti = 0;
                var lid = new List<int>();
                for (int i = 0; i < all.Rows.Count; i++)
                {
                    ti = i;
                    var s = all.Rows[i].ItemArray[5];
                    if (int.Parse(s.ToString()) == b_id)
                    {
                        lid.Add(ti);
                    }
                }
                foreach (var id in lid)
                {
                    if (fdt > DateTime.Parse((string)all.Rows[id].ItemArray[10]) && fdt < DateTime.Parse((string)all.Rows[id].ItemArray[11]))
                    {
                        Label1.Text = "Книга уже занята в этот временной промежуток. " +
                            "Попробуйте взять другую";
                        return;
                    }
                    if (tdt > DateTime.Parse((string)all.Rows[id].ItemArray[10]) && tdt < DateTime.Parse((string)all.Rows[id].ItemArray[11]))
                    {
                        Label1.Text = "Книга уже занята в этот временной промежуток. " +
                            "Попробуйте взять другую";
                        return;
                    }
                    if (fdt < DateTime.Parse((string)all.Rows[id].ItemArray[10]) && tdt > DateTime.Parse((string)all.Rows[id].ItemArray[11]))
                    {
                        Label1.Text = "Книга уже занята в этот временной промежуток. " +
                            "Попробуйте взять другую";
                        return;
                    }
                    if (fdt >= tdt)
                    {
                        Label1.Text = "Срок взятия указан неверно";
                        return;
                    }
                }
                serv.NewRec(o_id, r_id, b_id, fdt, tdt);
                Label1.Text = "Запись успешно добавлена.";
                Response.Redirect("~/ListForm.aspx");
            }
            else
            {
                Label1.Text = "Ошибка! Дата выбрана неправильно или не выбрана вообще.";
            }
        }
    }
}