using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace curs3laba4
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ButtonGetDate_Click(sender, e);
        }

        protected void ButtonGetDate_Click(object sender, EventArgs e)
        {
            var service = new RefLibraryLast.ServiceForLibrarySoapClient();
            var from = TextBoxFrom.Text;
            var to = TextBoxTo.Text;
            if (from == "" && to == "")
            {
                Label1.Text = "";
                GridView2.DataSourceID = ObjectDataSource1.ID;
            }
            else if (from != "" && to != "")
            {
                var fdt = new DateTime();
                var tdt = new DateTime();
                if (DateTime.TryParse(from, out fdt))
                {
                    if (DateTime.TryParse(to, out tdt))
                    {
                        if (fdt < tdt)
                        {
                            GridView2.DataSourceID = ObjectDataSource2.ID;
                            Label1.Text = "";
                        }
                        else
                        {
                            Label1.Text = "Дата до меньше, чем дата от!";
                            return;
                        }
                    }
                    else
                    {
                        Label1.Text = "Ошибка ввода даты до";
                        return;
                    }
                }
                else
                {
                    Label1.Text = "Ошибка ввода даты от";
                    return;
                }

            }
            else if (from != "" || to != "")
            {
                if (from != "")
                {
                    var fdt = new DateTime();
                    if (DateTime.TryParse(from, out fdt))
                    {
                        GridView2.DataSourceID = ObjectDataSource2.ID;
                        Label1.Text = "";
                    }
                    else
                    {
                        Label1.Text = "Ошибка ввода даты от";
                        return;
                    }
                }
                else
                {
                    var tdt = new DateTime();
                    if (DateTime.TryParse(to, out tdt))
                    {
                    }
                    else
                    {
                        Label1.Text = "Ошибка ввода даты до";
                        return;
                    }
                }
            }
            //GridView2.DataSource = ds.Tables[0];
            //GridView2.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //var ch = sender as CheckBox;
            //ch.Checked = !ch.Checked;
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

            var service = new RefLibraryLast.ServiceForLibrarySoapClient();
            var c = GridView2;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                var ch = (CheckBox)GridView2.Rows[i].FindControl("CheckBox1");
                if (ch.Checked)
                {
                    service.DeleteRec(int.Parse(GridView2.Rows[i].Cells[1].Text));
                }
            }
            Response.Redirect("~/ListForm.aspx");
        }

        protected void TextBoxFrom_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxFrom.Text == "")
            {
                TextBoxTo.ReadOnly = true;
            }
            else
            {
                TextBoxTo.ReadOnly = false;
            }
        }

        protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxFrom.Text = CalendarFrom.SelectedDate.ToShortDateString();
        }

        protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxTo.Text = CalendarTo.SelectedDate.ToShortDateString();
        }

        protected void ButtonGetDate_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/ListForm.aspx");
        }
    }
}