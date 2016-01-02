/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bam.Net.Data;
using Bam.Net.Data.Model;
using Bam.Net.Data.Model.Windows;
using System.ComponentModel;

namespace laotzu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LaoTzuForm laoTzu = new LaoTzuForm();
            List<MenuStrip> toolStrips = new List<MenuStrip>();
            toolStrips.Add(laoTzu.MenuStripMain);
            FormModelBinder modelBinder = new FormModelBinder(laoTzu.Controls, toolStrips);
            LaoTzuViewModel viewModel = new LaoTzuViewModel(modelBinder, laoTzu);
            modelBinder.ViewModel = viewModel;
            modelBinder.Bind();
            Application.Run(laoTzu);
        }
    }
}
