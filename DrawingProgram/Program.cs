using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingProgram {
    public class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Application.Run(form);
        }

        

    }
}

