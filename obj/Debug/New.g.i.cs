﻿#pragma checksum "D:\kuliah\window phone\task_ex\task\task\New.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "654A48875C67A86A64621A2532771A50"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace task {
    
    
    public partial class New : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox tittle;
        
        internal System.Windows.Controls.TextBox isi;
        
        internal Microsoft.Phone.Controls.DatePicker datePicker;
        
        internal Microsoft.Phone.Controls.TimePicker timePicker;
        
        internal System.Windows.Controls.RadioButton TopButton;
        
        internal System.Windows.Controls.RadioButton MiddleButton;
        
        internal System.Windows.Controls.RadioButton LowerButton;
        
        internal System.Windows.Controls.TextBlock choiceTextBlock;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/task;component/New.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tittle = ((System.Windows.Controls.TextBox)(this.FindName("tittle")));
            this.isi = ((System.Windows.Controls.TextBox)(this.FindName("isi")));
            this.datePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("datePicker")));
            this.timePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("timePicker")));
            this.TopButton = ((System.Windows.Controls.RadioButton)(this.FindName("TopButton")));
            this.MiddleButton = ((System.Windows.Controls.RadioButton)(this.FindName("MiddleButton")));
            this.LowerButton = ((System.Windows.Controls.RadioButton)(this.FindName("LowerButton")));
            this.choiceTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("choiceTextBlock")));
        }
    }
}

