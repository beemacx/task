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
using Microsoft.Phone.Scheduler;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Threading;

namespace task
{
    public partial class DetailPage : PhoneApplicationPage
    {
        //secara keseluruhan hampir sama dengan yang New.cs
        //hanya ada tambahan id
        task.Remindera.ReminderContext db;
        Remindera edit;
        string prioritys;

        //id dipakai untuk menuju ke idReminders dan parse dari MainPage.cs
        //menunjukkan bahwa id yang isinya yang di edit adalah data yang dipilih
        int id;

        public DetailPage()
        {
            InitializeComponent();

        }  

        //jika tombol edit di klik
        private void edit_klik(object sender, RoutedEventArgs e)
        {
            //isinya adl nama stackpanel untuk menaruh textbox, textblock dan sebagainya
            //jika edit di klik, isinya akan terlihat dan editb tdk terlihat(collapsed)
            isinya.Visibility = System.Windows.Visibility.Visible;

            //editb adalah nama button edit
            editb.Visibility = System.Windows.Visibility.Collapsed;

            //untuk mengedit berdasarkan data yang dipilih di MainPage.xaml
            //jika id yang di parse tadi tidak null
            //data data yang ada di field2 tadi sesuai yang dipilih
            if (NavigationContext.QueryString["id"] != null)
            {
                //mendapatkan id yang diparse
                int.TryParse(NavigationContext.QueryString["id"].ToString(), out id);

                //mengambil localdatabase
                db = new task.Remindera.ReminderContext("isostore:/Reminder.sdf");

                //select item dari item di loaldatabase dimana idReminder = id
                edit = (from item in db.reminders where item.idReminder == id select item).FirstOrDefault();

                //title.text di DetailPage akan terisi data tittle yang sesuai dengan yang dipilih
                tittle.Text = edit.title;
                //isi.Text juga, akan terisi data content yang sesuai dengan yang dipilih
                isi.Text = edit.content;
                datePicker.Value = edit.waktu.Date;

                //waktu dan prioritas tidak bisa sesuai yang sesuai dengan yang dipilih
                //karena tidak dimasukkan, untuk memasukkan nya tiadk tahu caranya
            }
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {


            if (TopButton.IsChecked == true)

                prioritys = TopButton.Content.ToString();

            else if (MiddleButton.IsChecked == true)

                prioritys = MiddleButton.Content.ToString();

            else

                prioritys = LowerButton.Content.ToString();
        }

        private void ok_editClick(object sender, RoutedEventArgs e)
        {
            DateTime MyDateTime = new DateTime();
            MyDateTime = ((DateTime)datePicker.Value).Date.Add(((DateTime)timePicker.Value).TimeOfDay);

            TimeSpan tgl = new TimeSpan();

            tgl = datePicker.Value.Value - DateTime.Now;

            int days = tgl.Days;
            int wholeWeeks = days / 7;
            int remainder = days % 7;

            string result = days + (Math.Abs(days) == 1 ? "day" : "days");
            if (Math.Abs(days) > 14)
            {
                result += "\n(" + wholeWeeks + "weeks";
                if (remainder != 0)
                    result += ", " + remainder + (Math.Abs(remainder) == 1 ? "day" : " days"); result += ")";
            }

            if (MyDateTime <= DateTime.Now)
            {

                MessageBox.Show("Opss!! Waktunya telah lewat bung!! Coba di cek lagi");
            }

            else
            {
                
                //untuk menyimpan
                db = new Remindera.ReminderContext("isostore:/Reminder.sdf");
                edit = (from item in db.reminders where item.idReminder == id select item).FirstOrDefault();
                edit.title = tittle.Text;
                edit.content = isi.Text;
                edit.waktu = MyDateTime;
                edit.priority = prioritys;
                edit.tgl = result;
                db.SubmitChanges();
                MessageBox.Show("Data Telah Terubah");
                tittle.Text = "";
                isi.Text = "";

                NavigationService.Navigate(new Uri("/Mainpage.xaml", UriKind.Relative));
            }
       }       
    }
}