using CsvHelper;
using CsvHelper.Configuration;
using Exercise1;
using Exercise1.Entity;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string filePathSelected = null;
        public MainWindow()
        {
            InitializeComponent();
            cbSchoolYears.ItemsSource = GetSchoolYears();
        }

        public List<string> GetSchoolYears()
        {
            using (var context = new ScoreContext())
            {
                var data = context.SchoolYears.ToList();
                // Perform other database operations as needed
                return data.Select(x => x.ExamYear).ToList();
            }
        }

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.Title = "Select a File";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                txtFileName.Text = System.IO.Path.GetFileName(selectedFilePath);
                filePathSelected = selectedFilePath;
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //var listStudent = new List<Student>();
            //var listScore = new List<Score>();
            //var context = new ScoreContext();

            //var schoolYear = context.SchoolYears.ToList().Where(x => x.ExamYear == cbSchoolYears.SelectedValue).FirstOrDefault();
            //var listSubject = context.Subjects.ToList();

            var year = int.Parse(cbSchoolYears.SelectedValue as string);
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.HeaderValidated = null; // Ignore header validation
            configuration.MissingFieldFound = null;
            using (var reader = new StreamReader(filePathSelected))
            using (var csv = new CsvReader(reader, configuration))
            {
                var records = csv.GetRecords<ScoreObject>().ToList();

                // Connection string
                string connectionString = "Server=.;Database=Score;User Id=sa;Password=12345;TrustServerCertificate=true;MultipleActiveResultSets=true;";

                // Destination table name
                string destinationTableName = "Score";

                // Create DataTable
                DataTable dataTable = CreateDataTable(records, year);

                // Bulk insert
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = destinationTableName;

                        // Map columns if needed
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("student_id", "student_id");
                        bulkCopy.ColumnMappings.Add("province", "province");
                        bulkCopy.ColumnMappings.Add("mathematics", "mathematics");
                        bulkCopy.ColumnMappings.Add("literature", "literature");
                        bulkCopy.ColumnMappings.Add("physics", "physics");
                        bulkCopy.ColumnMappings.Add("chemistry", "chemistry");
                        bulkCopy.ColumnMappings.Add("biology", "biology");
                        bulkCopy.ColumnMappings.Add("history", "history");
                        bulkCopy.ColumnMappings.Add("geography", "geography");
                        bulkCopy.ColumnMappings.Add("civic_education", "civic_education");
                        bulkCopy.ColumnMappings.Add("english", "english");
                        bulkCopy.ColumnMappings.Add("year", "year");

                        // Set options (optional)
                        bulkCopy.BatchSize = 1000; // Number of rows in each batch
                        bulkCopy.BulkCopyTimeout = 600; // Timeout in seconds

                        // Write the data to the SQL Server
                        bulkCopy.WriteToServer(dataTable);
                    }
                }

                Console.WriteLine("Bulk insert completed successfully.");
            }
            stopwatch.Stop();
            txtTime.Text = stopwatch.Elapsed.ToString();
        }

        private static DataTable CreateDataTable(List<ScoreObject> dataList, int year)
        {
            DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("Id", typeof(string));
            dataTable.Columns.Add("student_id", typeof(string));
            dataTable.Columns.Add("province", typeof(string));
            dataTable.Columns.Add("mathematics", typeof(double));
            dataTable.Columns.Add("literature", typeof(double));
            dataTable.Columns.Add("physics", typeof(double));
            dataTable.Columns.Add("chemistry", typeof(double));
            dataTable.Columns.Add("biology", typeof(double));
            dataTable.Columns.Add("history", typeof(double));
            dataTable.Columns.Add("geography", typeof(double));
            dataTable.Columns.Add("civic_education", typeof(double));
            dataTable.Columns.Add("english", typeof(double));
            dataTable.Columns.Add("year", typeof(int));


            // Add other columns as needed

            // Add data rows to the DataTable
            foreach (ScoreObject dataItem in dataList)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = Guid.NewGuid().ToString("N");
                row["student_id"] = dataItem.student_id;
                row["province"] = dataItem.province;
                row["mathematics"] = returnScore(dataItem.mathematics);
                row["literature"] = returnScore(dataItem.literature);
                row["physics"] = returnScore(dataItem.physics);
                row["chemistry"] = returnScore(dataItem.chemistry);
                row["biology"] = returnScore(dataItem.biology);
                row["history"] = returnScore(dataItem.history);
                row["geography"] = returnScore(dataItem.geography);
                row["civic_education"] = returnScore(dataItem.civic_education);
                row["english"] = returnScore(dataItem.english);
                row["year"] = year;


                // Set other column values
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        public static double returnScore(string stringScore)
        {
            if (string.IsNullOrEmpty(stringScore))
            {
                return -1;
            }
            return double.Parse(stringScore);
        }

    }
}
