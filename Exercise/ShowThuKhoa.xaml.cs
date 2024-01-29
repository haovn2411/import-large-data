using Exercise2_ShowData;
using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Exercise1
{
    /// <summary>
    /// Interaction logic for ShowThuKhoa.xaml
    /// </summary>
    public partial class ShowThuKhoa : Window
    {
        public ShowThuKhoa()
        {
            InitializeComponent();
            var listYear = new List<string>();
            listYear.Add("2017");
            listYear.Add("2018");
            listYear.Add("2019");
            listYear.Add("2020");
            cbYear.ItemsSource = listYear;

        }

        private async void btnShow_Click(object sender, RoutedEventArgs e)
        {
            var year = cbYear.SelectedValue as string;
            var listReturn = await GetReturnThuKhoaAsync(int.Parse(year));
            dgvShowThuKhoa.ItemsSource = listReturn;
        }

        private async Task<List<ReturnThuKhoaObject>> GetReturnThuKhoaAsync(int year)
        {
            string connectionString = "Server=.;Database=Score;User Id=sa;Password=12345;TrustServerCertificate=true;MultipleActiveResultSets=true;";
            var listTotalObject = new List<ReturnThuKhoaObject>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Score WHERE year = " + year;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        double A = 0, B = 0, C = 0, D1 = 0, A1 = 0;
                        var listThuKhoaA = new List<ReturnThuKhoaObject>();
                        var listThuKhoaB = new List<ReturnThuKhoaObject>();
                        var listThuKhoaC = new List<ReturnThuKhoaObject>();
                        var listThuKhoaD1 = new List<ReturnThuKhoaObject>();
                        var listThuKhoaA1 = new List<ReturnThuKhoaObject>();
                        while (await reader.ReadAsync())
                        {
                            double math, lite, physic, chemis, biolo, histo, geo, civic, english;
                            string SBD, TenMon, KhoiThi;
                            SBD = reader["student_id"].ToString();
                            math = double.Parse(reader["mathematics"].ToString());
                            lite = double.Parse(reader["literature"].ToString());
                            physic = double.Parse(reader["physics"].ToString());
                            chemis = double.Parse(reader["chemistry"].ToString());
                            biolo = double.Parse(reader["biology"].ToString());
                            histo = double.Parse(reader["history"].ToString());
                            geo = double.Parse(reader["geography"].ToString());
                            civic = double.Parse(reader["civic_education"].ToString());
                            english = double.Parse(reader["english"].ToString());
                            double Atemp = (double)math + (double)physic + (double)chemis;
                            double Btemp = (double)math + (double)biolo + (double)chemis;
                            double Ctemp = (double)lite + (double)histo + (double)geo;
                            double D1temp = (double)math + (double)lite + (double)english;
                            double A1temp = (double)math + (double)physic + (double)english;
                            if (Atemp >= A)
                            {
                                A = Atemp;
                                var ThuKhoaA = new ReturnThuKhoaObject()
                                {
                                    SBD = SBD,
                                    KhoiThi = "A",
                                    Mon1 = math.ToString(),
                                    Mon2 = physic.ToString(),
                                    Mon3 = chemis.ToString(),
                                    TenMon = "Toan,Li,Hoa",
                                    TongDiem = A.ToString(),
                                };
                                listThuKhoaA.Add(ThuKhoaA);
                            }
                            if (Btemp >= B)
                            {
                                B = Btemp;
                                var ThuKhoaB = new ReturnThuKhoaObject()
                                {
                                    SBD = SBD,
                                    KhoiThi = "B",
                                    Mon1 = math.ToString(),
                                    Mon2 = chemis.ToString(),
                                    Mon3 = biolo.ToString(),
                                    TenMon = "Toan,Hoa,Sinh",
                                    TongDiem = B.ToString(),
                                };
                                listThuKhoaB.Add(ThuKhoaB);
                            }
                            if (Ctemp >= C)
                            {
                                C = Ctemp;
                                var ThuKhoaC = new ReturnThuKhoaObject()
                                {
                                    SBD = SBD,
                                    KhoiThi = "C",
                                    Mon1 = lite.ToString(),
                                    Mon2 = histo.ToString(),
                                    Mon3 = geo.ToString(),
                                    TenMon = "Van,Su,Dia",
                                    TongDiem = C.ToString(),
                                };
                                listThuKhoaC.Add(ThuKhoaC);
                            }
                            if (D1temp >= D1)
                            {
                                D1 = D1temp;
                                var ThuKhoaD1 = new ReturnThuKhoaObject()
                                {
                                    SBD = SBD,
                                    KhoiThi = "D1",
                                    Mon1 = math.ToString(),
                                    Mon2 = lite.ToString(),
                                    Mon3 = english.ToString(),
                                    TenMon = "Toan,Van,Anh",
                                    TongDiem = D1.ToString(),
                                };
                                listThuKhoaD1.Add(ThuKhoaD1);
                            }
                            if (A1temp >= A1)
                            {
                                A1 = A1temp;
                                var ThuKhoaA1 = new ReturnThuKhoaObject()
                                {
                                    SBD = SBD,
                                    KhoiThi = "A1",
                                    Mon1 = math.ToString(),
                                    Mon2 = physic.ToString(),
                                    Mon3 = english.ToString(),
                                    TenMon = "Toan,Li,Anh",
                                    TongDiem = A1.ToString(),
                                };
                                listThuKhoaA1.Add(ThuKhoaA1);
                            }
                        }
                        listThuKhoaA = listThuKhoaA.OrderByDescending(x => double.Parse(x.TongDiem)).ToList();
                        var temp = double.Parse(listThuKhoaA.First().TongDiem);
                        listThuKhoaA = listThuKhoaA.Where(x => double.Parse(x.TongDiem) == temp).ToList();

                        listThuKhoaB = listThuKhoaB.OrderByDescending(x => double.Parse(x.TongDiem)).ToList();
                        temp = double.Parse(listThuKhoaB.First().TongDiem);
                        listThuKhoaB = listThuKhoaB.Where(x => double.Parse(x.TongDiem) == temp).ToList();

                        listThuKhoaC = listThuKhoaC.OrderByDescending(x => double.Parse(x.TongDiem)).ToList();
                        temp = double.Parse(listThuKhoaC.First().TongDiem);
                        listThuKhoaC = listThuKhoaC.Where(x => double.Parse(x.TongDiem) == temp).ToList();

                        listThuKhoaD1 = listThuKhoaD1.OrderByDescending(x => double.Parse(x.TongDiem)).ToList();
                        temp = double.Parse(listThuKhoaD1.First().TongDiem);
                        listThuKhoaD1 = listThuKhoaD1.Where(x => double.Parse(x.TongDiem) == temp).ToList();

                        listThuKhoaA1 = listThuKhoaA1.OrderByDescending(x => double.Parse(x.TongDiem)).ToList();
                        temp = double.Parse(listThuKhoaA1.First().TongDiem);
                        listThuKhoaA1 = listThuKhoaA1.Where(x => double.Parse(x.TongDiem) == temp).ToList();

                        listTotalObject.AddRange(listThuKhoaA);
                        listTotalObject.AddRange(listThuKhoaB);
                        listTotalObject.AddRange(listThuKhoaC);
                        listTotalObject.AddRange(listThuKhoaD1);
                        listTotalObject.AddRange(listThuKhoaA1);
                    }
                }
            }
            return listTotalObject;
        }

    }
}
