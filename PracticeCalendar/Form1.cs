﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PracticeCalendar
{
    public partial class Form1 : Form
    {
        private Calendar _calendar;

        public Form1()
        {
            InitializeComponent();

            _calendar = new Calendar();
            _calendar.DisplayMode = CalendarMode.Decade;
            this.elementHost1.Child = _calendar;
        }
    }
}
