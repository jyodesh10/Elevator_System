using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;


namespace ElevatorControl
{
    public partial class Form1 : Form
    {
        //variablesInstances

        private int y_up = 54;
        private int y_down = 371;
        private int x_door_left_close = 74;
        private int x_door_left_open = 12;
        private int x_door_right_close = 139;
        private int x_door_right_open = 200;

        private bool go_up = false;
        private bool go_down = false;

        private bool arrived_G = false;
        private bool arrived_1 = false;



        public Form1()
        {
            InitializeComponent();
        }


        /// TIMERS

  
        /// Private methods belonging to this form only, void does not return value 
        

        /// LIft Move down control
        private void timer_lift_down_Tick(object sender, EventArgs e)
        {

            if (picture_lift.Top <= y_down)
            {
                picture_lift.Top += 1;
            }
            else
            {
                timer_lift_down.Enabled = false;
                btn_up.Enabled = true;
                btn_1.Enabled = true;
                btn_close.Enabled = true;
                btn_open.Enabled = true;
                btn_down.BackColor = Color.White;
                btn_G.BackColor = Color.White;


                door_open_down();
                arrived_G = true;

                picture_lift.Image = global::ElevatorControl.Properties.Resources.interior;

                display_panel.Image = global::ElevatorControl.Properties.Resources.G;
                display_top.Image = global::ElevatorControl.Properties.Resources.G;
                display_bottom.Image = global::ElevatorControl.Properties.Resources.G;
            }
        }

        ///move lift up controls
        private void timer_lift_up_Tick(object sender, EventArgs e)
        {
            if (picture_lift.Top >= y_up)
            {
                picture_lift.Top -= 1;
            }
            else
            {

                timer_lift_up.Enabled = false;
                btn_down.Enabled = true;
                btn_G.Enabled = true;
                btn_close.Enabled = true;
                btn_open.Enabled = true;
                btn_up.BackColor = Color.White;
                btn_1.BackColor = Color.White;

                door_open_up();
                arrived_1 = true;


                picture_lift.Image = global::ElevatorControl.Properties.Resources.interior;

                display_panel.Image = global::ElevatorControl.Properties.Resources._1;
                display_top.Image = global::ElevatorControl.Properties.Resources._1;
                display_bottom.Image = global::ElevatorControl.Properties.Resources._1;
            }
        }

        ///Open door down controls
        private void door_open_down_Tick(object sender, EventArgs e)
        {
            if (door_left_down.Left >= x_door_left_open && door_right_down.Left <= x_door_right_open)
            {
                door_left_down.Left -= 1;
                door_right_down.Left += 1;
            }
            else
            {

                timer_door_open_down.Enabled = false;

            }
        }

        ///close door down controls
        private void door_close_down_Tick(object sender, EventArgs e)
        {
            if (door_left_down.Left <= x_door_left_close && door_right_down.Left >= x_door_right_close)
            {
                door_left_down.Left += 1;
                door_right_down.Left -= 1;
            }
            else
            {
                timer_door_close_down.Enabled = false;
                

                if (go_up == true)
                {
                    picture_lift.Image = global::ElevatorControl.Properties.Resources.interior;

                    display_panel.Image = global::ElevatorControl.Properties.Resources.up;
                    display_top.Image = global::ElevatorControl.Properties.Resources.up;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.up;


                    timer_lift_up.Enabled = true;
                    go_up = false;
                }
            }
        }

        ///Open door up controls
        private void timer_door_open_up_Tick(object sender, EventArgs e)
        {
            if (door_left_up.Left >= x_door_left_open && door_right_up.Left <= x_door_right_open)
            {
                door_left_up.Left -= 1;
                door_right_up.Left += 1;
            }
            else
            {
                timer_door_open_up.Enabled = false;

            }
        }

        ///close door up controls
        private void timer_door_close_up_Tick(object sender, EventArgs e)
        {
            if (door_left_up.Left <= x_door_left_close && door_right_up.Left >= x_door_right_close)
            {
                door_left_up.Left += 1;
                door_right_up.Left -= 1;
            }
            else
            {
                timer_door_close_up.Enabled = false;

                
                if (go_down == true)
                {
                    picture_lift.Image = global::ElevatorControl.Properties.Resources.interior;

                    display_panel.Image = global::ElevatorControl.Properties.Resources.down;
                    display_top.Image = global::ElevatorControl.Properties.Resources.down;
                    display_bottom.Image = global::ElevatorControl.Properties.Resources.down;

                    

                    timer_lift_down.Enabled = true;
                    go_down = false;
                }
            }
        }
        //Methods
        //Door Close ande open actions
        private void door_close_down()
        {
            insertdata("Opening doors at Ground Floor");
            timer_door_close_down.Enabled = true;
            timer_door_open_down.Enabled = false;
        }

        private void door_open_down()
        {
            insertdata("Opening doors at Ground Floor");
            timer_door_close_down.Enabled = false;
            timer_door_open_down.Enabled = true;
        }

        private void door_close_up()
        {
            insertdata("Opening doors at First Floor");
            timer_door_close_up.Enabled = true;
            timer_door_open_up.Enabled = false;
        }

        private void door_open_up()
        {
            insertdata("Opening doors at First Floor");
            timer_door_close_up.Enabled = false;
            timer_door_open_up.Enabled = true;
        }

        //Methods
        //actions when lift goes up
        private void going_up()
        {
            go_up = true;
            door_close_down();
            btn_G.Enabled = false;
            btn_down.Enabled = false;
            btn_close.Enabled = false;
            btn_open.Enabled = false;
            arrived_G = false;
            insertdata("Elevator Going Up");

        }

        //Methods
        //actions when lift goes down
        private void going_down()
        {
            go_down = true;
            door_close_up();

            btn_1.Enabled = false;
            btn_up.Enabled = false;
            btn_close.Enabled = false;
            btn_open.Enabled = false;
            arrived_1 = false;
            insertdata("Elevator Going Down");

            
        }


        private void btn_down_Click(object sender, EventArgs e)
        {
            btn_up.BackColor = Color.Red;
            going_up();

        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            btn_down.BackColor = Color.Red;
            going_down();
        }


        private void btn_1_Click(object sender, EventArgs e)
        {
            btn_1.BackColor = Color.Red;
            going_up();
        }

        private void btn_G_Click(object sender, EventArgs e)
        {
            btn_G.BackColor = Color.OrangeRed;
            going_down();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (arrived_G == true)
            {
                door_close_down();
            }
            else if (arrived_1 == true)
            {
                door_close_up();
            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (arrived_G == true)
            {
                door_open_down();
            }
            else if (arrived_1 == true)
            {
                door_open_up();
            }

        }

        private void btn_alarm_Click(object sender, EventArgs e)
        {
            btn_alarm.BackColor = Color.Green;
            insertdata("Emergency Stop!");
            timer_lift_down.Enabled = false;
            timer_lift_up.Enabled = false;
            timer_door_open_down.Enabled = true;
            timer_door_open_up.Enabled = true;
            display_panel.Image = global::ElevatorControl.Properties.Resources.alarnbtn;
            display_top.Image = global::ElevatorControl.Properties.Resources.alarnbtn;
            display_bottom.Image = global::ElevatorControl.Properties.Resources.alarnbtn;

        }

       

        //Database

        //nstantiations
        private bool filled;
        public DataSet ds = new DataSet();
        


        private void btn_displaylog_Click(object sender, EventArgs e)
        {
            try
            {
                string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ElevatorDatabase.accdb;";// Connection string
                string dbcommand = "Select * from Actions;";// SQL query command string
                OleDbConnection conn = new OleDbConnection(dbconnection);// Initializing connection variable with method parameter the connection string
                OleDbCommand comm = new OleDbCommand(dbcommand, conn);// Initializing command variable with method parameter connection and the SQL command string
                OleDbDataAdapter adapter = new OleDbDataAdapter(comm);// Initializing the adapter variable to work with the connection above

                //cnn.Open();
                conn.Open();
                //MessageBox.Show("Connection Open ! ");
                while (filled == false)
                {
                    adapter.Fill(ds);
                    filled = true;
                }
                //cnn.Close();
                conn.Close();
            }
            catch (Exception)//catch exception
            {
                MessageBox.Show("connection problem ! ");//popup
                string message = "There is an error in connection to datasource";//System-user feedback message
                string caption = "Error!!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);// Dialog result includes the three method parameters upon showing
            }

            XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX.Items.Clear();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX.Items.Add(row["Date"] + "\t\t" + row["Time"] + "\t\t" + row["Action"]);
            }

        }

        private void insertdata(string action)
        {
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source = ElevatorDatabase.accdb;";
            string dbcommand = "insert into [Actions] ([Date],[Time],[Action]) values (@date, @time, @action)";
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            

            XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX.Items.Add(date + "\t\t" + time + "\t\t" + action);



            OleDbConnection conn_db = new OleDbConnection(dbconnection);
            OleDbCommand comm_insert = new OleDbCommand(dbcommand, conn_db);
            OleDbDataAdapter adapter_insert = new OleDbDataAdapter(comm_insert);
            comm_insert.Parameters.AddWithValue("@date", date);
            comm_insert.Parameters.AddWithValue("@time", time);
            comm_insert.Parameters.AddWithValue("@action", action);




            conn_db.Open();

            comm_insert.ExecuteNonQuery();

            conn_db.Close();
        }

        private void btn_clearlog_Click(object sender, EventArgs e)
        {
            XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX.Items.Clear();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void door_right_up_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}