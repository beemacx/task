using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using Microsoft.Phone.Scheduler;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Threading;

namespace task
{
    public partial class MainPage : PhoneApplicationPage
    {
        //memmanggil localdatabase
        public const string strConnectionString = @"isostore:/Reminder.sdf";
        
        // mendeklarasikan db dengan tipedata context dari localdatabase
        //remindera adalah class untuk membuat localdatabase
        task.Remindera.ReminderContext db;

        public MainPage()
        {
            InitializeComponent();

            tanggal();//memenggil tanggal
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //membuat instance db sebagai data localdatabse
            //dicek apakah sudah ada belum
            db = new task.Remindera.ReminderContext("isostore:/Reminder.sdf");
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }

            RefreshData();//memanggil class RefreshData

        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        //class tanggal untuk menampilkan tanggal dan jam sekarang
        public void tanggal(){
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(1);
            tmr.Tick += OnTimerTick;
            tmr.Start();

        }
        
        // OnTimerTick yang ada di class tanggal
        void OnTimerTick(object sender, EventArgs args)
         {
             tgl.Text = DateTime.Now.ToString();
        }

        //class RefreshData, memanggil List/isi db
        //MainLisbox, listbox untuk yang di Task
        //MainLis untuk yang di Coundown
        public void RefreshData()
        {
            MainLisbox.ItemsSource = db.reminders.ToList();
            MainLis.ItemsSource = db.reminders.ToList();
        }

        //jika Appbar new di klik akan menuju halaman New untuk membuat baru
        private void New(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/New.xaml", UriKind.Relative));
        }

        //jika di tekan tombol Back
        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        //Untuk menampilkan tanggal di calendar
        private void Cal_MonthChanged(object sender, WPControls.MonthChangedEventArgs e)
        {
            Debug.WriteLine("Cal_MonthChanged fired.  New year is " + e.Year.ToString() + " new month is " + e.Month.ToString());
        }

        private void Cal_MonthChanging(object sender, WPControls.MonthChangedEventArgs e)
        {
            Debug.WriteLine("Cal_MonthChanging fired.  New year is " + e.Year.ToString() + " new month is " + e.Month.ToString());
        }

        private void Cal_SelectionChanged(object sender, WPControls.SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Cal_SelectionChanged fired.  New date is " + e.SelectedDate.ToString());
        }

        private void Cal_DateClicked(object sender, WPControls.SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Cal_DateClicked fired.  New date is " + e.SelectedDate.ToString());
        }

        //jika App menu settings di klik akan menuju halaman Settings untuk membuat baru
        private void set_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        //jika App menu about di klik akan menuju halaman New untuk membuat baru
        private void about_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        //jika menekan hapus
        private void hapus_click(object sender, RoutedEventArgs e)
        {   
            //pemanggilan localdatabase
            using (task.Remindera.ReminderContext ini = new task.Remindera.ReminderContext(strConnectionString))
            {
                int id = ((sender as MenuItem).DataContext as Remindera).idReminder;

                IQueryable<Remindera> EmpQuery = from reminderlist in ini.reminders where reminderlist.idReminder == id select reminderlist;
                
                Remindera EmpRemove = EmpQuery.FirstOrDefault();
                ini.reminders.DeleteOnSubmit(EmpRemove);//data dihapus
                ini.SubmitChanges();//lalu localdatabase di update
                MessageBox.Show("data terhapus");

                ScheduledActionService.Remove(id.ToString());

                RefreshData();//menampilkan data localdatabase
            }
        }

        
        //jika menekan edit
        private void edit_click(object sender, RoutedEventArgs e)
        {
            //id untuk menunjuk idReminders mana yang datanya di klik
            int id = ((sender as MenuItem).DataContext as Remindera).idReminder;

            //menuju halaman DetailPage dan mem-parse id
            //jadi yang tampil adl data dengan idReminders yang di klik
            NavigationService.Navigate(new Uri("/DetailPage.xaml?id="+id.ToString(), UriKind.Relative));
        }
    }
}