using Exercise2_ShowData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Exercise1
{
    /// <summary>
    /// Interaction logic for ShowData.xaml
    /// </summary>
    public partial class ShowData : Window
    {
        private DispatcherTimer timer;
        private DateTime startTime;

        public ShowData()
        {
            InitializeComponent();
            // Initialize timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            // Start the timer when the window loads
            Loaded += btnShow_Click;
        }

        private async void btnShow_Click(object sender, RoutedEventArgs e)
        {
            // Save the start time
            startTime = DateTime.Now;

            // Start the timer
            timer.Start();
            var returnList = await GetReturnTotalsAsync();
            dtgDataShow.ItemsSource = returnList;

            timer.Stop();
        }

        private async Task<List<ReturnTotalModel>> GetReturnTotalsAsync()
        {
            string connectionString = "Server=.;Database=Score;User Id=sa;Password=12345;TrustServerCertificate=true;MultipleActiveResultSets=true;";
            var listTotalObject = new List<ReturnTotalModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                for (int i = 2017; i <= 2020; i++)
                {
                    string query = "SELECT * FROM Score WHERE year = " + i;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            int total = 0, math = 0, lite = 0, physic = 0, chemis = 0
                                , biolo = 0, histo = 0, geo = 0, civic = 0, english = 0;
                            while (await reader.ReadAsync())
                            {
                                if (double.Parse(reader["mathematics"].ToString()) != -1)
                                {
                                    math += 1;
                                }
                                if (double.Parse(reader["literature"].ToString()) != -1)
                                {
                                    lite += 1;
                                }
                                if (double.Parse(reader["physics"].ToString()) != -1)
                                {
                                    physic += 1;
                                }
                                if (double.Parse(reader["chemistry"].ToString()) != -1)
                                {
                                    chemis += 1;
                                }
                                if (double.Parse(reader["biology"].ToString()) != -1)
                                {
                                    biolo += 1;
                                }
                                if (double.Parse(reader["history"].ToString()) != -1)
                                {
                                    histo += 1;
                                }
                                if (double.Parse(reader["geography"].ToString()) != -1)
                                {
                                    geo += 1;
                                }
                                if (double.Parse(reader["civic_education"].ToString()) != -1)
                                {
                                    civic += 1;
                                }
                                if (double.Parse(reader["english"].ToString()) != -1)
                                {
                                    english += 1;
                                }
                                total += 1;
                                // Create a new Person object for each record and add it to the list
                            }
                            listTotalObject.Add(new ReturnTotalModel
                            {
                                Year = i,
                                mathematics = math,
                                literature = lite,
                                physics = physic,
                                chemistry = chemis,
                                biology = biolo,
                                history = histo,
                                geography = geo,
                                civic_education = civic,
                                english = english,
                                Total = total,
                            });
                            dtgDataShow.ItemsSource = listTotalObject;
                        }
                    }
                }
            }
            return listTotalObject;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Calculate elapsed time
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Display the elapsed time in the TextBox
            txtTime.Text = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }
    }
}
