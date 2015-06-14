//membuat class Remindara unutk membuat localdatabase
using System; //namespace yang dibutuhkan, default tersedia jika membuat projek baru
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.Linq.Mapping; // tambahan untuk membuat localdatabse
using System.Data.Linq; // tambahan untuk membuat localdatabse

namespace task
{
    [Table] //untuk membuat table localdatabase
    public class Remindera //dibuat public agar dapat di akses semua class dalam aplikasi
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity")]
        public int idReminder { set; get; }

        [Column] public string title { set; get; }  // kolom table nya
        
        [Column] public string content { set; get; } //dideklarasikan apa yang akan dimasukkan
        
        [Column] public DateTime waktu { set; get; } //set; get; agar dapat diatur dan diabmil dataya

        [Column] public string priority { set; get; }

        [Column] public string tgl { set; get; }

    public class ReminderContext : DataContext //membuat isinya yang berupa string
    {
        public ReminderContext(string connectionString) : base(connectionString) { 
        }
        public Table<Remindera> reminders  // mengambil table
        {
            get { return this.GetTable<Remindera>(); }
        }}}
}
