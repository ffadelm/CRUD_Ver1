using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace gramed
{

    class Program
    {
        static void Main(string[] args)
        {
            //koneksi ke database
            SqlConnection con = new SqlConnection("Data Source = MSI; database = gramedStore; integrated security = True; MultipleActiveResultSets=true");
            //open koneksi
            con.Open();

            Console.WriteLine("\n===========================================");
            Console.WriteLine("EXERCISE 1 - PENGEMBANGAN APLIKASI BASISDATA");
            Console.WriteLine("============================================");

            Console.WriteLine("\nisi awal:");
            Console.WriteLine("\n1 - teh pucuk ha");
            Console.WriteLine("2 - bite & brigh");
            Console.WriteLine("3 - sari roti kr");
            Console.WriteLine("4 - kantong plas ");
           

            //perulangan manu pilihan
            char ch;
            do
            {
                
                a:
                Console.WriteLine();
                Console.WriteLine("Menu Pilihan");
                Console.WriteLine("1. Insert Barang");
                Console.WriteLine("2. Menampilkan Semua Barang");
                Console.WriteLine("3. Update Barang");
                Console.WriteLine("4. Delete Barang");
                Console.WriteLine("5. Search Barang");
                Console.WriteLine("6. Exit");
                Console.Write("Pilihan : ");
                //menyimpan pilihan
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    //1. Insert Barang
                    case "1":
                        Console.WriteLine("\n~INSERT BARANG~");
                        Console.Write("Kode Barang : ");
                        string Kd_barang = Console.ReadLine();
                        Console.Write("Nama Barang : ");
                        string Nama_barang = Console.ReadLine();
                        Console.Write("Harga : ");
                        string Harga = Console.ReadLine();

                        //membuat variable untuk di insert kedalam table barang 
                        string strQueryInsert = String.Format("INSERT INTO barang(kd_barang, nama_barang, harga) " +
                            "VALUES('{0}', '{1}', '{2}')", Kd_barang, Nama_barang, Harga);
                        //membuat command cmdIN untuk di execute
                        SqlCommand cmdIN = new SqlCommand(strQueryInsert, con);
                        //membuat command cm membuat kondisi 
                        SqlCommand cm = new SqlCommand("SELECT kd_barang FROM barang WHERE kd_barang = @kd_barang", con);
                        //cm dibuat parameter dengan kd_barang
                        cm.Parameters.AddWithValue("@kd_barang", Kd_barang);

                        //membuat variable isCheck untuk di cek apakah kd_barang sudah ada atau belum
                        //jika ischeck tidak null maka 
                        var isCheck = Convert.ToString(cm.ExecuteScalar()) != "";
                        if (isCheck)
                        {
                            Console.WriteLine("\nkode barang telah tersedia...");
                        }
                        //jika isCheck null maka akan di execute
                        else
                        {
                            cmdIN.ExecuteNonQuery();
                            Console.WriteLine("\nBarang berhasil ditambah....");
                        }
                            break;

                    //2. Menampilkan semua barang
                    case "2":
                        Console.WriteLine("\n~MENAMPILKAN SEMUA BARANG~ :");

                        //membuat variable untuk mendapatkan data dari table barang
                        string strQuerySelect = "SELECT * FROM barang";
                        //membuat command cmdSEL untuk mengeksekusi sql query
                        SqlCommand cmdSEL = new SqlCommand(strQuerySelect, con);
                        //membuat DR data reader untuk mengeksekusi hasil apa pun yang ditetapkan dengan beberapa baris / kolom 
                        SqlDataReader DR = cmdSEL.ExecuteReader();
                        Console.WriteLine();
                        
                         if (DR.Read())
                            {
                                Console.Write("\n Kode Barang : {0}\n Nama Barang : {1}\n Harga:{2}\n",
                                DR["kd_barang"], DR["nama_barang"], DR["harga"]);
                            }
                            else
                            {
                                Console.WriteLine("Database Kosong");
                            }
                        break;

                    //3. Update Barang
                    case "3":
                        Console.WriteLine("\n~UPDATE BARANG~");
                        //untuk memilih kode barang yng ingin di update
                        Console.Write("Masukkan kode barang yang akan di update :");
                        string up = Console.ReadLine();
                        //untuk mengisi data baru barang 
                        Console.Write("\nMasukkan kode barang yang yang baru : ");
                        string kd_brg = Console.ReadLine();
                        Console.Write("Masukkan nama barang yang yang baru: ");
                        string nm_brg = Console.ReadLine();
                        Console.Write("Masukkan harga yang akan yang baru: ");
                        string hrg = Console.ReadLine();

                        //membuat variable untuk mengupdate dari table barang
                        string strQueryUpdate = "UPDATE barang SET kd_barang='" + kd_brg + "', nama_barang='" + nm_brg + "', harga='" + hrg + "' " +
                            "WHERE kd_barang =" + up + "";
                        //membuat command mengeksekusi sql query dari variable strQueryUpdate
                        SqlCommand cmdUP = new SqlCommand(strQueryUpdate, con);
                        //membuat command cmd untuk mengeksekusi sql dengan kodisi kd_barang
                        SqlCommand cmd = new SqlCommand("SELECT kd_barang FROM barang WHERE kd_barang = @kd_barang", con);
                        //membuat parameter cmd dengan kd_barang
                        cmd.Parameters.AddWithValue("@kd_barang", kd_brg);

                        //membuat variable check untuk mengcek kd_barang adlah tidak null 
                        //executescalar digunakan karena akan mengembalikan sebuah nilai 
                        var Check = Convert.ToString(cmd.ExecuteScalar()) != "";
                        //jikacheck adalah tidak null maka
                        if (Check )
                        {
                            Console.WriteLine("\nkode barang telah tersedia...");
                        }
                        else
                        {
                            Console.Write("yakin ingin di update (y/n)?");
                            string Y = Console.ReadLine();
                            if (Y == "y" || Y == "Y")
                            {
                                //jika check adalah null maka
                                cmdUP.ExecuteNonQuery();
                                Console.WriteLine("\nData berhasil di update...");
                            }
                            else
                            {
                                Console.WriteLine("Batal Mengpdate...");
                                Console.WriteLine("Press any key goto menu..");
                                Console.ReadKey();
                                Console.Clear();
                                goto a;
                            }
                        }
                        break;

                    //4. Menghapus Barang
                    case "4":
                        Console.WriteLine("\n~HAPUS BARANG~");
                        Console.Write("Masukkan kode barang yang akan di hapus : ");
                        string kd_del = Console.ReadLine();

                        //membuat variable untuk menghapus data sesuai kd_barang
                        string strQueryDelete = "DELETE barang WHERE kd_barang=" + kd_del + "";
                        SqlCommand cmdDEL = new SqlCommand(strQueryDelete, con);
                        
                        //kondisi meyakinkan user
                        Console.Write("yakin hapus (y/n)?");
                        string y = Console.ReadLine();
                        if (y == "y" || y == "Y")
                        {
                            //jika setuju maka data akan di hapus
                            cmdDEL.ExecuteNonQuery();
                            Console.WriteLine("\nData berhasil di hapus...");
                        }
                        else
                        {
                            //jika tidak setuju maka akan kembali ke menu
                            Console.WriteLine("Batal Menghapus...");
                            Console.WriteLine("Press any key goto menu..");
                            Console.ReadKey();
                            Console.Clear();
                            goto a;
                        }
                        
                        break;

                    //5. Mencari Barang
                    case "5":
                        Console.WriteLine("\n~MENCARI BARANG~");
                        Console.Write("Masukkan Kode Barang yang ingin di cari :");
                        string kd = Console.ReadLine();

                        //membuat variable untuk mencari data sesuai kd_barang
                        using (SqlCommand cmdSER = new SqlCommand("SELECT * FROM barang WHERE kd_barang = @kd_barang", con))
                        {
                            //membuat permisalan bahwa kd_barang == variable kd (saat diinput)
                            cmdSER.Parameters.Add("kd_barang", System.Data.SqlDbType.VarChar).Value = kd;
                            //untuk membaca  hasil apa pun yang ditetapkan dengan beberapa baris / kolom. dari comand cmdSER
                            using (SqlDataReader dr = cmdSER.ExecuteReader())
                            {
                                //jika memenuhi
                                if (dr.Read())
                                {
                                    Console.WriteLine("\nbarang telah di temukan :");
                                    Console.WriteLine();
                                    Console.Write(" Kode Barang : {0}\n Nama Barang : {1}\n Harga:{2}\n",
                                        dr["kd_barang"], dr["nama_barang"], dr["harga"]);
                                }
                                //jika tidak memenuhi
                                else
                                {
                                    Console.WriteLine("\nBarang dengan kode '" + kd + "' tidak di temukan");
                                }
                            }
                        }
                        break;

                    //6. EXIT PROGRAM
                    case "6":
                        return;

                    //kodisi jika pilihan menu salah
                    default:
                        Console.WriteLine("\nPilihan Salah !!!!");
                        Console.WriteLine("Masukkan pilihan 1-5");
                        goto a;

                }
                //kondisi untuk kembali kemenu 
                Console.Write("\nKembali ke Menu ? (y/n) : ");
                ch = Char.Parse(Console.ReadLine());
                Console.Clear();
            } while ((ch == 'y') || (ch == 'Y'));

            //menutup koneksi database
            con.Close();
        }
    }
}
