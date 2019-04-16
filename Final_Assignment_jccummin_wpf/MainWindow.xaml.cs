using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Final_Assignment_jccummin_wpf.Models;

namespace Final_Assignment_jccummin_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        /**
         * All clicks up until the next comment update the input box.
         * They add the requested character, and end. Rather simple, but allows easy, familiar construction of the input value.
         * Also, the expense/credit boolean is stored here 
        **/

        //ture = Expense, false = credit
        public Boolean ExpenseOrCredit = true;


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '1');
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '2');
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '3');
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '4');
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '5');
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '6');
        }
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '7');
        }
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '8');
        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '9');
        }
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '0');
        }
        private void Button_decimal_Click(object sender, RoutedEventArgs e)
        {
            input_num_box.Content = (input_num_box.Content.ToString() + '.');
        }

        /**
         * Next set of button clicks handle moving the info from the input box to the set box.
         * */
        private void Button_enter_Click(object sender, RoutedEventArgs e)
        {
            Double carryDouble = Convert.ToDouble(input_num_box.Content);

            //Functions converts to string to ensure functionality with label
            if (ExpenseOrCredit)
            {
                total_expense_box.Content = (Convert.ToDouble(total_expense_box.Content) + carryDouble).ToString();
            }
            else
            {
                credit_total_box.Content = (Convert.ToDouble(credit_total_box.Content) + carryDouble).ToString();
            }

            //Updating Balance Box
            balance_box.Content = (Convert.ToDouble(credit_total_box.Content) - Convert.ToDouble(total_expense_box.Content)).ToString();
            //Resetting input.
            input_num_box.Content = "";
        }


        /**
         * These two functions ensure easy identification of whether one is adding to the expense or the credit.
         * */
        private void Expense_set_Click(object sender, RoutedEventArgs e)
        {
            ExpenseOrCredit = true;
            expense_or_credit.Content = "Expense";
        }
        private void Credit_set_Click(object sender, RoutedEventArgs e)
        {
            ExpenseOrCredit = false;
            expense_or_credit.Content = "Credit";

        }

        /**
         * Final end button to process the transaction into the SQL table and create a transaction object to reflect the latest transaction.
         * */
        private void End_button_Click(object sender, RoutedEventArgs e)
        {

            
       
            SqlConnection connection = new SqlConnection(@"Data Source=jccummin-assignnment3-comp1098.database.windows.net;Initial Catalog=jccumminassignment3Comp1098;User ID=jccummin;Password=********;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //If something fails, the user is notified. They are free to continue inputting info, however.
            try { 
                connection.Open();
                SqlDataReader reader = null;

                //Gethering the total cash left in the register. It started with 1000.00.
                String totalCash = "";
                String readQuery = ("Select TOP 1 TotalCash FROM Reciept ORDER BY RecieptID DESC");

                SqlCommand readCommand = new SqlCommand(readQuery, connection);

                reader = readCommand.ExecuteReader();
                while (reader.Read())
                {
                    totalCash = reader["TotalCash"].ToString();
                }

                reader.Close();

                //Upload command is a simple insert into statement using the data we've already gathered.
                String uploadQuery = ("INSERT INTO Reciept VALUES (" + Convert.ToDecimal(total_expense_box.Content) + ", " + Convert.ToDecimal(credit_total_box.Content) + ", " + Convert.ToDecimal(balance_box.Content) + ", " + (Convert.ToDecimal(totalCash) + Convert.ToDecimal(balance_box.Content) + ", GETDATE());"));
                SqlCommand uploadCommand = new SqlCommand(uploadQuery, connection);
                uploadCommand.ExecuteNonQuery();


                //Creates new Reciept item with current data
                Reciept newReciept = new Reciept();

                newReciept.Expense = Convert.ToDecimal(total_expense_box.Content);
                newReciept.Credit = Convert.ToDecimal(credit_total_box.Content);
                newReciept.Balance = (Convert.ToDecimal(credit_total_box.Content) - Convert.ToDecimal(total_expense_box.Content));
                newReciept.TotalCash = (Convert.ToDecimal(totalCash) + Convert.ToDecimal(balance_box.Content));
                newReciept.DayOfPurchase = DateTime.Now.Date;

                


            }
            catch (Exception error)
            {
                messageLabel.Content = error;
            }
            finally
            {


                //After all that, it records the info on the user's reciept, and notified them of the successful submission.
                expense_reciept.Content = Convert.ToDouble(total_expense_box.Content);
                credit_reciept.Content = Convert.ToDouble(credit_total_box.Content);
                balance_reciept.Content = (Convert.ToDouble(credit_total_box.Content) - Convert.ToDouble(total_expense_box.Content));

                messageLabel.Content = "Subbmission completed.";
                credit_total_box.Content = "0.00";
                total_expense_box.Content = "0.00";
                balance_box.Content = "0.00";
                connection.Close();

            }
                
           
    
        }
    }
}
