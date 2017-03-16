using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    

    public int Numberofpackages;
    public int Numberofswimlanes;


    protected void Page_Load(object sender, EventArgs e)
    {
        time.Text = DateTime.Now.ToString();
        GridView1.Visible = false;
        HttpCookie myCookie = new HttpCookie("UserSettings");
        myCookie.Expires = DateTime.Now.AddDays(1d);
        Response.Cookies.Add(myCookie);

        redraw();



    }
    protected void button_Clickfailed(object sender, EventArgs e)
    {
        Button button = sender as Button;
        HttpCookie myCookie = new HttpCookie("DBDashboard");

        //Add key-values in the cookie
        string text = button.ID.ToString();
        myCookie.Values.Add("sort", text);

        //set cookie expiry date-time. Made it to last for next 12 hours.
        myCookie.Expires = DateTime.Now.AddHours(12);

        //Most important, write the cookie to client.
        Response.Cookies.Add(myCookie);

        
        Response.Redirect(Request.RawUrl);
      

    }
    protected void button_Clickfinished(object sender, EventArgs e)
    {

        Button button = sender as Button;
        HttpCookie myCookie = new HttpCookie("DBDashboard");

        //Add key-values in the cookie
        string text = button.ID.ToString();
        myCookie.Values.Add("sort", text);

        //set cookie expiry date-time. Made it to last for next 12 hours.
        myCookie.Expires = DateTime.Now.AddHours(12);

        //Most important, write the cookie to client.
        Response.Cookies.Add(myCookie);


        Response.Redirect(Request.RawUrl);

    }
    protected void button_Clickpending(object sender, EventArgs e)
    {


        Button button = sender as Button;
        HttpCookie myCookie = new HttpCookie("DBDashboard");

        //Add key-values in the cookie
        string text = button.ID.ToString();
        myCookie.Values.Add("sort", text);

        //set cookie expiry date-time. Made it to last for next 12 hours.
        myCookie.Expires = DateTime.Now.AddHours(12);

        //Most important, write the cookie to client.
        Response.Cookies.Add(myCookie);


        Response.Redirect(Request.RawUrl);

    }

    private void redraw()
    {
        string Sortby = "";
        HttpCookie myCookie = Request.Cookies["DBDashboard"];
        if (myCookie == null)
        {
            //No cookie found or cookie expired.
            //Handle the situation here, Redirect the user or simply return;
        }
        else
        {
            if (!string.IsNullOrEmpty(myCookie.Values["sort"]))
            {
                Sortby = myCookie.Values["sort"].ToString();
                
            }
        }
        
       
        string[] swimlanes = new string[200];
        string[] packages = new string[200];
        string[,] packprogess = new string[200, 200];
        TableRow[] trows = new TableRow[200];
        trows[0] = new TableRow();
        MyTable.Rows.Add(trows[0]);
        TableCell tCell3 = new TableCell();
        trows[0].Cells.Add(tCell3);
        tCell3.Width = 60;
        trows[1] = new TableRow();
        MyTable.Rows.Add(trows[1]);
        TableCell tCell4 = new TableCell();
        trows[1].Cells.Add(tCell4);
        tCell3.Width = 60;
        Numberofpackages = 0;
        Numberofswimlanes = 0;
        swimlanes[0] = "!";
        swimlanes[1] = "!";
        foreach (GridViewRow row in GridView1.Rows)
        {
            string stremp = row.Cells[0].Text;
            Char delimiter = '_';
            String[] substrings = stremp.Split(delimiter);
            string swimlane = substrings[1];
            string package = substrings[5];
            if (!Array.Exists(packages, element => element == package))
            {
                Numberofpackages++;
                packages[Numberofpackages] = package;
                TableCell tCell = new TableCell();
                HyperLink hyper1 = new HyperLink();
                hyper1.NavigateUrl = "www.google.com";
                hyper1.Text = package;
                tCell.Width = 75;
                tCell.Controls.Add(hyper1);
                trows[0].Cells.Add(tCell);
                TableCell tCell2 = new TableCell();
                tCell2.Width = 75;
                tCell.Controls.Add(hyper1);
                trows[0].Cells.Add(tCell2);
                TableCell tCellt = new TableCell();
                tCell.Width = 75;
                tCell.Controls.Add(hyper1);
                trows[0].Cells.Add(tCellt);

                TableCell tCella = new TableCell();
                Button button = new Button();
               button.ID = "Finished" + Numberofpackages.ToString();
                button.Text = "Finished";
              //  button.
                button.Click += new EventHandler(button_Clickfinished);
                tCella.Controls.Add(button);
                trows[1].Cells.Add(tCella);


                TableCell tCellb = new TableCell();
                Button button2 = new Button();
               button2.ID = "Pending" + Numberofpackages.ToString();
                button2.Text = "Pending";
                tCellb.Controls.Add(button2);
                button2.Click += new EventHandler(button_Clickpending);
                trows[1].Cells.Add(tCellb);



                TableCell tCellc = new TableCell();
                Button button3 = new Button();
               button3.ID = "Failed" + Numberofpackages.ToString();
                button3.Text = "Failed";
                tCellc.Controls.Add(button3);
                button3.Click += new EventHandler(button_Clickfailed);
                trows[1].Cells.Add(tCellc);



            }
            if (!Array.Exists(swimlanes, element => element == swimlane))
            {
                Numberofswimlanes++;
                swimlanes[Numberofswimlanes] = swimlane;

            }


        }
      
        Array.Resize(ref swimlanes, Numberofswimlanes + 1);

        Array.Sort(swimlanes);

        foreach (GridViewRow row in GridView1.Rows)
        {
            string stremp = row.Cells[0].Text;
            Char delimiter = '_';
            String[] substrings = stremp.Split(delimiter);
            string swimlane = substrings[1];
            string package = substrings[5];
            string nFinished = runsql(stremp, "Finished");
            string nPending = runsql(stremp, "PENDING");
            string nFailed = runsql(stremp, "Failed");

            int y = Array.IndexOf(swimlanes, swimlane, 0);
            var x = Array.IndexOf(packages, package, 0);
            try
            {
                packprogess[x, y] = "" + nFinished + " " + nPending + " " + nFailed;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                // throw;
            }

        }

        for (int i = 1; i < Numberofswimlanes + 1; i++)
        {
            TableCell tCell2 = new TableCell();
            HyperLink hyper2 = new HyperLink();
            hyper2.NavigateUrl = "www.google.com";
            hyper2.Text = swimlanes[i];
            tCell2.Controls.Add(hyper2);
            tCell2.Height = 30;
            trows[i] = new TableRow();
            MyTable.Rows.Add(trows[i]);
            trows[i].Cells.Add(tCell2);
        }

        


        for (int y = 1; y < Numberofswimlanes + 1; y++)
        {
            for (int x = 1; x < Numberofpackages + 1; x++)
            {
                Char delimiter = ' ';
                String[] substrings = packprogess[x, y].Split(delimiter);
                TableCell tCell = new TableCell();
                tCell.Text = substrings[0];
                trows[y].Cells.Add(tCell);
                TableCell tCell2 = new TableCell();
                tCell2.Text = substrings[1];
                trows[y].Cells.Add(tCell2);
                TableCell tCell5 = new TableCell();
                tCell5.Text = substrings[2];
                trows[y].Cells.Add(tCell5);
            }
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private string runsql(string Tble,string Status)
    {
        SqlConnection sqlConnection1 = new SqlConnection("Data Source=.;Initial Catalog=csod_nolio;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        Object returnValue;

        cmd.CommandText = "select COUNT(*) from " + Tble+ " WHERE hhv_status='" + Status + "'";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlConnection1;

        sqlConnection1.Open();

        returnValue = cmd.ExecuteScalar();

        sqlConnection1.Close();
        return returnValue.ToString();

    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void csod_nolo_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}