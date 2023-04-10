using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web;

namespace ATM_Project
{
    public partial class Form1 : Form
    {
        private bool Label_overwrite = true;
        private bool Num_Dot = false; // " . "

        public Form1()
        {
            InitializeComponent();
            CWD2.Visible = false;
            TMF.Visible = false;
            Check3.Visible = false;
            WD3.Visible = false;
            TMT.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            DepositS.Visible = false;
            TransferS.Visible = false;
            WSP.Visible = false;
            DSP1.Visible = false;
            C2L1.Visible = false;
            W2L1.Visible = false;
            D2L1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e) {}

        ////////Homepage////////////////////////////////////////////////////////////////////////////////
        private void WithdrawM_Click(object sender, EventArgs e) { // withdraw m button
            WDtext3.ResetText();
            CWD2.Visible = true;
            W2L1.Visible = true;
            W2b1.Visible = true;
            W2b2.Visible = true;
            DSwithdraw.Visible = true;
            Wenter.Visible = true;
            C2b1.Visible = false;
            C2b2.Visible = false;
            C2L1.Visible = false;
            D2L1.Visible = false;
            D2b1.Visible = false;
            D2b2.Visible = false;
            DepositS.Visible = false;
            DSdeposit.Visible = false;
            Denter.Visible = false;
        }

        private void CheckM_Click(object sender, EventArgs e) {  //check money button
            CWD2.Visible = true;
            C2L1.Visible = true;
            C2b1.Visible = true;
            C2b2.Visible = true;
            W2b1.Visible = false;
            W2b2.Visible = false;
            W2L1.Visible = false;
            D2b1.Visible = false;
            D2b2.Visible = false;
            D2L1.Visible = false;
            DepositS.Visible = false;
        }

        private void DepositM_Click_1(object sender, EventArgs e){ //deposit m button
            WDtext3.ResetText();
            CWD2.Visible = true;
            D2L1.Visible = true;
            D2b1.Visible = true;
            D2b2.Visible = true;
            Denter.Visible = true;
            DSdeposit.Visible = true;
            C2b1.Visible = false;
            C2L1.Visible = false;
            C2b2.Visible = false;
            W2b1.Visible = false;
            W2b2.Visible = false;
            W2L1.Visible = false;
            DepositS.Visible = false;
            DSwithdraw.Visible = false;
            Wenter.Visible = false;
        }
        private void TransferM_Click(object sender, EventArgs e) { //transfer m button
            TMF.Visible = true;
            TransferP.Visible = false;
            CWD2.Visible = false;
            Check3.Visible = false;
            WD3.Visible = false;
            TMT.Visible = false;
            transactionSuccessPanel.Visible = false;
            DepositS.Visible = false;
            TransferS.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            C2L1.Visible = false;
            W2L1.Visible = false;
            D2L1.Visible = false;
        }

        private void Logout_Click_1(object sender, EventArgs e) { //logout button
            this.Close();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////Transfer Money///////////////////////////////////////////////////////////
        private void TMTb1_Click(object sender, EventArgs e)
        {
            TMF.Visible = false;
            TMT.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            TransferS.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
        }

        private void TPb3_Click(object sender, EventArgs e)
        {
            int accountNum1 = Int32.Parse(TPT1.Text);
            int accountNum = Int32.Parse(TPT3.Text);
            double a = Double.Parse(TPT2.Text);
            double b = Double.Parse(TPtext.Text);
            double c = Double.Parse(TPT4.Text);

            if (a < b)
            {
                MessageBox.Show("The transaction exceeds the total transaction amount. " + '\n' +
                    "Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning // for Warning  
                    );
                TPtext.ResetText();

            }else{
                MessageBox.Show("The transaction succeeds. " + MessageBoxButtons.OK);
                a -= b;
                c += b;
                TransferP.Visible = false;
                TransferS.Visible = true;
                TST5.Text = TPtext.Text;
                TST5.ReadOnly = true;
                TST1.Text = TPT1.Text;
                TST1.ReadOnly = true;
                TST2.Text = a.ToString("");
                TST2.ReadOnly = true;
                TST3.Text = TPT3.Text;
                TST3.ReadOnly = true;
                TST4.Text = c.ToString("");
                TST4.ReadOnly = true;

                //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
                string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connStr);

                try
                {
                    Console.WriteLine("Connecting to MySQL..."); //connect to the database
                    conn.Open(); // open the database
                    con.Open(); 
                    string sql = "UPDATE account SET balance = @a WHERE accountNum = @accountNum1"; //update database 
                    string sql2 = "UPDATE account SET balance = @c WHERE accountNum = @accountNum"; //update database 
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                    MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql2, con);
                    cmd.Parameters.AddWithValue("@a", a);
                    cmd.Parameters.AddWithValue("@accountNum1", accountNum1);
                    cmd2.Parameters.AddWithValue("@c", c);
                    cmd2.Parameters.AddWithValue("@accountNum", accountNum);
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    MySqlDataReader mR = cmd2.ExecuteReader();
                    
                    myReader.Close();//close the reader
                    mR.Close();

                }
                catch (Exception ex){
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                con.Close();
                    Console.WriteLine("Done.");
                }
        } 
            private void TMFb2_Click(object sender, EventArgs e)
        {
            TFTok.Visible = true;
            TMTok2.Visible = false;
            TMF.Visible = false;
            TMT.Visible = true;
            TPb2.Visible = true;
            TPTb3.Visible = false;
            comboBox1.Visible = true;
            comboBox2.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            TransferS.Visible = false;
            comboBox1.SelectedIndex = -1;

            C2L1.Visible = false;
            W2L1.Visible = false;
            D2L1.Visible = false;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("accountNum", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    TMTtext1.Text = myReader[1].ToString();
                    TMTtext1.ReadOnly = true;
                    TMTtext2.Text = myReader[4].ToString();
                    TMTtext2.ReadOnly = true;

                    TPT1.Text = myReader[1].ToString();
                    TPT1.ReadOnly = true;
                    TPT2.Text = myReader[3].ToString();
                    TPT2.ReadOnly = true;

                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void TMFb3_Click(object sender, EventArgs e)
        {
            TFTok.Visible = false;
            TMTok2.Visible = true;
            TMF.Visible = false;
            TMT.Visible = true;
            TPTb3.Visible = true;
            TPb2.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = true;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            TransferS.Visible = false;
            comboBox2.SelectedIndex = -1;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account WHERE accountNum = 2222";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("2222", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    TMTtext1.Text = myReader[1].ToString();
                    TMTtext1.ReadOnly = true;
                    TMTtext2.Text = myReader[4].ToString();
                    TMTtext2.ReadOnly = true;

                    TPT1.Text = myReader[1].ToString();
                    TPT1.ReadOnly = true;
                    TPT2.Text = myReader[3].ToString();
                    TPT2.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void TFTok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select the account you want to transfer money. " + '\n' +
                        "",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning // for Warning  
                        );
            }
            else
            {
                TransferP.Visible = true;
                TMF.Visible = false;
                TMT.Visible = false;
                transactionSuccessPanel.Visible = false;
                TransferS.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
            }
            int i = comboBox1.SelectedIndex;
            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            String sql = "";
            if (i == 0){
                sql = "SELECT * FROM account WHERE accountNum = 2222";
            }
            else if (i == 1) {
                sql = "SELECT * FROM account WHERE accountNum = 3333";
            }
            else if (i == 2){
                sql = "SELECT * FROM account WHERE accountNum = 4444";
            }

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read()) {
                    TPT3.Text = myReader[1].ToString();
                    TPT3.ReadOnly = true;
                    TPT4.Text = myReader[3].ToString();
                    TPT4.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void TPb1_Click(object sender, EventArgs e){
            TransferP.Visible = false;
            TMF.Visible = true;
            TMT.Visible = false;
        }
        private void TPb2_Click(object sender, EventArgs e) {
            TFTok.Visible = true;
            TransferP.Visible = false;
            TMF.Visible = false;
            TMT.Visible = true;
            comboBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            TransferP.Visible = false;
            TMF.Visible = true;
        }

        private void TPTb3_Click(object sender, EventArgs e){
            TMTok2.Visible = true;
            TransferP.Visible = false;
            TMF.Visible = false;
            TMT.Visible = true;
            comboBox2.Visible = true;
        }

        private void TMTok2_Click(object sender, EventArgs e)
        {
            TPtext.ResetText();
            if (string.IsNullOrEmpty(comboBox2.Text)){
                MessageBox.Show("Please select the account you want to transfer money. " + '\n' +
                        "",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning // for Warning  
                        );
            }
            else{
                TransferP.Visible = true;
                TMF.Visible = false;
                TMT.Visible = false;
                transactionSuccessPanel.Visible = false;
                TransferS.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
            }

            int j = comboBox2.SelectedIndex;
            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            String sql = "";
            if (j == 0){
                sql = "SELECT * FROM account WHERE accountNum = 1111";
            }
            else if (j == 1){
                sql = "SELECT * FROM account WHERE accountNum = 3333";
            }
            else if (j == 2){
                sql = "SELECT * FROM account WHERE accountNum = 4444";
            }

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read()){

                    TPT3.Text = myReader[1].ToString();
                    TPT3.ReadOnly = true;
                    TPT4.Text = myReader[3].ToString();
                    TPT4.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

        }
        private void button9_Click(object sender, EventArgs e) {
            TMT.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            DepositS.Visible = false;
        }
        private void ChekingSelect_Click(object sender, EventArgs e){
            TMT.Visible = false;
            TMF.Visible = true;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            transactionSuccessPanel.Visible = false;
        }
        private void TMFb1_Click(object sender, EventArgs e) {
            TMF.Visible = false;
            TMT.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            TransferS.Visible = false;
        }
        private void ReturnToMenu_Click_1(object sender, EventArgs e){
            CWD2.Visible = false;
            Check3.Visible = false; 
            TMT.Visible = false;
            TransferP.Visible = false;
            transactionSuccessPanel.Visible = false;
            DepositS.Visible = false;
            TransferS.Visible = false;
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////Check Money///////////////////////////////////////////////////////////
        private void C2b1_Click(object sender, EventArgs e)
        {
            Check3.Visible = true;
            CWD2.Visible = false;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("accountNum", s);

                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    C3text1.Text = myReader[1].ToString();
                    C3text1.ReadOnly = true;
                    C3text2.Text = myReader[3].ToString();
                    C3text2.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void C2b2_Click(object sender, EventArgs e)
        {
            Check3.Visible = true;
            CWD2.Visible = false;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account WHERE accountNum = 2222";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("2222", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    C3text1.Text = myReader[1].ToString();
                    C3text1.ReadOnly = true;
                    C3text2.Text = myReader[3].ToString();
                    C3text2.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");       
        }

        private void C2b3_Click(object sender, EventArgs e) {
            CWD2.Visible = false;
        }
        private void Check3_Paint(object sender, PaintEventArgs e){
            Returnbuttom.Visible = true;
            C2L1.Visible = false;
        }
        private void C3rm_Click(object sender, EventArgs e) {
            CWD2.Visible = false;
            Check3.Visible = false;
            WD3.Visible = false;
            DepositS.Visible = false;
            C2L1.Visible = false;
            W2L1.Visible = false;
            D2L1.Visible = false;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////Witdraw Money////////////////////////////////////////////////////////////
        private void W2b1_Click(object sender, EventArgs e)
        {
            CWD2.Visible = false;
            WD3.Visible = true;
            WD3W.Visible = true;
            WD3D.Visible = false;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("accountNum", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    WDtext1.Text = myReader[1].ToString();
                    WDtext2.Text = myReader[3].ToString();
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    
        private void W2b2_Click(object sender, EventArgs e)
        {
            CWD2.Visible = false;
            WD3.Visible = true;
            WD3W.Visible = true;
            WD3D.Visible = false;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account WHERE accountNum = 2222";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("2222", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    WDtext1.Text = myReader[1].ToString();
                    WDtext2.Text = myReader[3].ToString();
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void WDreturnA_Click(object sender, EventArgs e) {
            WD3.Visible = false;
            CWD2.Visible = true;
        }

        private void Wenter_Click(object sender, EventArgs e){
            double a, b, c; //a = account balance, b = the amount customer enter, c = account number
            double.TryParse(WDtext2.Text, out a);
            double.TryParse(WDtext3.Text, out b);
            double.TryParse(WDtext1.Text, out c);

            if (a < b && b < 5000) {  //$5000 is tentative ammount for now 
                    MessageBox.Show("The transaction exceeds the account balance. " + '\n' +
                        "Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning // for Warning  
                        );
                    WDtext3.ResetText();
   
                }if (b > 5000)
            {
                MessageBox.Show("The transaction exceeds the total transaction amount. " + '\n' +
                    "Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning // for Warning  
                    );
                WDtext3.ResetText();
            }
            else
            {
                MessageBox.Show("The transaction succeeds. " + MessageBoxButtons.OK); //message
                a -= b;
                WD3.Visible = false;
                WSP.Visible = true;
                DStext1.Text = WDtext3.Text;
                DStext1.ReadOnly = true;
                DStext2.Text = a.ToString("");
                DStext2.ReadOnly = true;

                string s = "Tom";
                //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
                string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                try{
                    Console.WriteLine("Connecting to MySQL..."); //connect to the database
                    conn.Open(); // open the database
                    string sql = "UPDATE account SET balance = @a WHERE accountNum = @c"; //update database 
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@a", a);
                    cmd.Parameters.AddWithValue("@c", c);
                    MySqlDataReader myReader = cmd.ExecuteReader();
                    myReader.Close();//close the reader

                }
                catch (Exception ex){
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                Console.WriteLine("Done.");
            }
        }

        private void WSPb1_Click(object sender, EventArgs e) { //ok button from withdraw successful panel
            WSP.Visible = false;
            DepositS.Visible = true;
            DStext1.Text = WDtext3.Text;
            DStext1.ReadOnly = true;
            DStext2.ReadOnly = true;
        }
        private void WSb1_Click(object sender, EventArgs e) {
            DSP1.Visible = false;
            DepositS.Visible = true;
            DStext1.Text = WDtext3.Text;
            DStext1.ReadOnly = true;
            DStext2.ReadOnly = true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        ////////Deposit Money//////////////////////////////////////////////////////////////////////////////
        private void D2b1_Click(object sender, EventArgs e){ //get account

            CWD2.Visible = false;
            WD3.Visible = true;
            WD3W.Visible = false;
            WD3D.Visible = true;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("accountNum", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    WDtext1.Text = myReader[1].ToString(); 
                    WDtext1.ReadOnly = true;
                    WDtext2.Text = myReader[3].ToString();
                    WDtext2.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void D2b2_Click(object sender, EventArgs e){ //get account
            CWD2.Visible = false;
            WD3.Visible = true;
            WD3W.Visible = false;
            WD3D.Visible = true;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try{
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM account WHERE accountNum = 2222";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("2222", s);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read()){
                    WDtext1.Text = myReader[1].ToString();
                    WDtext1.ReadOnly = true;
                    WDtext2.Text = myReader[3].ToString();
                    WDtext2.ReadOnly = true;
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
        private void Denter_Click(object sender, EventArgs e){ //enter key for deposit
            double a, b, c; // a is for acount balance, b is for the amount to deposit 
            double.TryParse(WDtext2.Text, out a);
            double.TryParse(WDtext3.Text, out b);
            double.TryParse(WDtext1.Text, out c);

            MessageBox.Show("The transaction succeeds. " + MessageBoxButtons.OK); //message to customer
            a += b;
            WD3.Visible = false;
            DSP1.Visible = true;
            DStext1.Text = WDtext3.Text;
            DStext1.ReadOnly = true;
            DStext2.Text = a.ToString("");
            DStext2.ReadOnly = true;

            string s = "Tom";
            //string connStr = @"server=localhost;user=root;database=atm;port=3306;password=;";
            string connStr = "server = csitmariadb.eku.edu; user = student; database = csc340_db; port = 3306;password = Maroon@21?;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try{
                Console.WriteLine("Connecting to MySQL..."); //connect to the database
                conn.Open(); // open the database
                string sql = "UPDATE account SET balance = @a WHERE accountNum = @c";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@a", a);
                cmd.Parameters.AddWithValue("@c", c);
                MySqlDataReader myReader = cmd.ExecuteReader();
                myReader.Close();//close the reader

            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////keyboard////////////////////////////////////////////////////////////////////////////////////////////////////
        private void B1_Click(object sender, EventArgs e) { //key1 for withdraw and deposit
            if (Label_overwrite == true){
                WDtext3.Text = B1.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B1.Text;
            }
        }
        private void B2_Click(object sender, EventArgs e) {  //key2
            if (Label_overwrite == true){
                WDtext3.Text = B2.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B2.Text;
            }
        }
        private void B3_Click(object sender, EventArgs e) {  //key3
            if (Label_overwrite == true){
                WDtext3.Text = B3.Text;
                Label_overwrite = false;
            } else{
                WDtext3.Text += B3.Text;
            }
        }
        private void B4_Click(object sender, EventArgs e) {  //key4
            if (Label_overwrite == true){
                WDtext3.Text = B4.Text;
                Label_overwrite = false;
            }
            else{
                WDtext3.Text += B4.Text;
            }
        }
        private void B5_Click(object sender, EventArgs e) { //key5
            if (Label_overwrite == true){
                WDtext3.Text = B5.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B5.Text;
            }
        }
        private void B6_Click(object sender, EventArgs e) {  //key6
             if (Label_overwrite == true){
                WDtext3.Text = B6.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B6.Text;
            }
        }
        private void B7_Click(object sender, EventArgs e) { //key7
            if (Label_overwrite == true){
                WDtext3.Text = B7.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B7.Text;
            }
        }
        private void B8_Click(object sender, EventArgs e) { //key8
            if (Label_overwrite == true){
                WDtext3.Text = B8.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B8.Text;
            }
        }
        private void B9_Click(object sender, EventArgs e) { //key9
            if (Label_overwrite == true){
                WDtext3.Text = B9.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B9.Text;
            }
        }
        private void B0_Click(object sender, EventArgs e){ //key0
            if (Label_overwrite == true){
                WDtext3.Text = B0.Text;
                Label_overwrite = false;
            }else{
                WDtext3.Text += B0.Text;
            }
        }
        private void bt1_Click(object sender, EventArgs e){ //key 1 for transfer
            if (Label_overwrite == true){
                TPtext.Text = bt1.Text;
                Label_overwrite = false;
            }else {
                TPtext.Text += bt1.Text;
            }
        }
        private void bt2_Click(object sender, EventArgs e) { //key2
            if (Label_overwrite == true) {
                TPtext.Text = bt2.Text;
                Label_overwrite = false;
            } else{
                TPtext.Text += bt2.Text;
            }
        }
        private void bt3_Click(object sender, EventArgs e) {  //key3
            if (Label_overwrite == true){
                TPtext.Text = bt3.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt3.Text;
            }
        }
        private void bt4_Click(object sender, EventArgs e){ //key4
            if (Label_overwrite == true){
                TPtext.Text = bt4.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt4.Text;
            }
        }
        private void bt5_Click(object sender, EventArgs e){ //key5
            if (Label_overwrite == true){
                TPtext.Text = bt5.Text;
                Label_overwrite = false;
            } else{
                TPtext.Text += bt5.Text;
            }
        }
        private void bt6_Click(object sender, EventArgs e) {//key6
            if (Label_overwrite == true){
                TPtext.Text = bt6.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt6.Text;
            }
        }
        private void bt7_Click(object sender, EventArgs e){//key7
            if (Label_overwrite == true) {
                TPtext.Text = bt7.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt7.Text;
            }
        }
        private void bt8_Click(object sender, EventArgs e) {//key8
            if (Label_overwrite == true){
                TPtext.Text = bt8.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt8.Text;
            }
        }
        private void bt9_Click(object sender, EventArgs e) {//key9
            if (Label_overwrite == true){
                TPtext.Text = bt9.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt9.Text;
            }
        }
        private void bt0_Click(object sender, EventArgs e){//key0
            if (Label_overwrite == true) {
                TPtext.Text = bt0.Text;
                Label_overwrite = false;
            }else{
                TPtext.Text += bt0.Text;
            }
        }
        private void WDclear_Click(object sender, EventArgs e) { //clear ket for Withdraw & Deposit
            WDtext3.Text = "0";
            Label_overwrite = true;
            Num_Dot = false;
        }
        private void WDdelete_Click(object sender, EventArgs e) { //delete key for Withdraw & Deposit
            if (WDtext3.Text != string.Empty){
                int txtlength = WDtext3.Text.Length;
                if (txtlength != 1){
                    WDtext3.Text = WDtext3.Text.Remove(txtlength - 1);
                }else{
                    WDtext3.Text = 0.ToString();
                }
            }
        }
        private void Tclear_Click(object sender, EventArgs e){ //clear key for transfer
            TPtext.Text = "0";
            Label_overwrite = true;
            Num_Dot = false;
        }
        private void Tdelete_Click(object sender, EventArgs e){//delete key for transfer
            if (TPtext.Text != string.Empty) {
                int txtlength = TPtext.Text.Length;
                if (txtlength != 1){
                    TPtext.Text = TPtext.Text.Remove(txtlength - 1);
                }else{
                    TPtext.Text = 0.ToString();
                }
            }
        }       
    }
}
