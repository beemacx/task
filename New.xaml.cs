using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;//tambahan untuk pejadwalan
using System.Threading;//untuk mengitung mundur

namespace task
{
    public partial class New : PhoneApplicationPage
    {
        // mendeklarasikan db dengan tipedata context dari localdatabase
        //remindera adalah class untuk membuat localdatabase
        task.Remindera.ReminderContext db;

        //prioritys sebagai hasil dari yang di pilih di RadioButton
        string prioritys;

        //Reminder, untuk menampilakan Notifikasi/Reminder di layar hp
        Reminder reminder;

        public New()
        {
            InitializeComponent();

            //memanggil localdatabse dan mengecek nya
            Loaded += (s, e) =>
            {

                db = new task.Remindera.ReminderContext("isostore:/Reminder.sdf");
                if (!db.DatabaseExists())
                {
                    db.CreateDatabase();
                }

            };

        }

        //aksi jika RadioButton di pilih
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            if (TopButton.IsChecked == true)

                //contentnya adalah High, sesuai yang di New.xaml nya
                prioritys = TopButton.Content.ToString();

            else if (MiddleButton.IsChecked == true)

                prioritys = MiddleButton.Content.ToString();

            else

                prioritys = LowerButton.Content.ToString();
        }

        //jika button OK di klik
        private void newClick(object sender, RoutedEventArgs e)
        {
            //MyDateTime sebagai DateTime, hari dan jam
            DateTime MyDateTime = new DateTime();

            //menggabungkan hari tanggal dan jam jadi satu yaitu MyDateTime
            MyDateTime = ((DateTime)datePicker.Value).Date.Add(((DateTime)timePicker.Value).TimeOfDay);

            

            //Timespan untuk ghitung mundur, tinggal berapa hari
            TimeSpan tgl = new TimeSpan();

            //datePicker.Value.Value adalah tanggal yang di pilih di datePicker
            tgl  = datePicker.Value.Value - DateTime.Now;

            //untuk penghtungan contdown
            //.Days adalah nama hari nya
            int days = tgl.Days;
            int wholeWeeks = days / 7;
            int remainder = days % 7;
            
            string result = days + (Math.Abs(days) == 1 ? " day" : " days");
            if (Math.Abs(days) > 14)
            {
                result += "\n(" + wholeWeeks + " weeks";
                if (remainder != 0)
                    result += ", " + remainder + (Math.Abs(remainder) == 1 ? " day" : " days"); 
                    result += ")";
            }

            //pengecekan apakah waktu yang dimasukkan sudah lewat belum
            if (MyDateTime <= DateTime.Now)
            {
                MessageBox.Show("Opss!! Waktunya telah lewat bung!! Coba di cek lagi");
            }
            
            //jika belum lewat, maka data akan disimpan
            else
            {
                //if( Remindera.ReminderContext  ){}

                //untuk memasukkan ke localdatabase, sesuai tipe data dan fieldnya
                Remindera _reminder = new Remindera() { 
                    title = tittle.Text, 
                    content = isi.Text, 
                    waktu = MyDateTime, 
                    priority = prioritys, 
                    tgl = result };

                //untuk menginsert ke localdatabase
                db.reminders.InsertOnSubmit(_reminder);
                db.SubmitChanges();//untuk mengupdate

                //untuk menampilkan notifikasi
                //memanggil idReminders di localdatabase, untuk dijadikan pembeda

                
                        
                        reminder = new Reminder(_reminder.idReminder.ToString());


                        //akan tampil jika MyDateTime telah tiba
                        reminder.BeginTime = MyDateTime.AddSeconds(10);
                        //yang akan keluar adl content, prioritas dan title
                        reminder.Content = _reminder.content + " >>>> " + _reminder.priority;
                        reminder.Title = _reminder.title;

                        //menambahkan jadwal reminder yang diatas
                        ScheduledActionService.Add(reminder);
                
                MessageBox.Show("Data Tersimpan");

                //langsung menuju ke halaman MainPage, dimana ada Task dan Countdown
                NavigationService.Navigate(new Uri("/Mainpage.xaml", UriKind.Relative));
            };

        }
    }
}