using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingProgram {
    public partial class Form1 : Form {
        Pen BlackPen = new Pen(Color.Black, 15);
        Pen WhitePen = new Pen(Color.White, 15);
        Pen RedPen = new Pen(Color.Red, 15);
        Pen GreenPen = new Pen(Color.Green, 15);
        Pen BluePen = new Pen(Color.Blue, 15);
        public Pen myPen;
        public String err;
        int[] penPos = new int[2] { 0, 0 };

        Codex codex;

        String[] commands;

        public String[,] vars = new String[2,64];

        int commandCounter = 0;
        int i = 0;

        bool loop = false;

        String[][] loopcoms = new String[64][];

        public Form1() {
            
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        void goButton_Click(object sender, EventArgs e) {

            String txt = textBox1.Text;
            String[] words = txt.Split(' '); // Tokenise input

            for (int i = 0; i < words.Length; i++) { // Clean the input
                words[i] = words[i].Trim();
                words[i] = words[i].ToLower();
            }

            if (words[0].Equals("run")){ // If given the "run" command, then split the rich text box into an array and iterate over each command
                String raw = richTextBox1.Text;
                List<String> broken = raw.Split('\n').ToList();
                List<String[]> split = new List<String[]>();
                foreach (String s in broken) split.Add(s.Split(' '));
                codex = new Codex(split);
                foreach (String[] command in codex){
                    RunCommand(command);
                }
                richTextBox1.Clear();
            }

            else {
                RunCommand(words);
            }
        }

        void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                goButton_Click(this, new EventArgs());
            }
        }
                
            

        public void RunCommand(String[] args) {
            try {
                using (var g = Graphics.FromImage(pictureBox1.Image)) { // Set pen to the given colour
                    
                    switch (args[0]) {
                        case "pen":
                            switch (args[1]) {

                                case "black":
                                    myPen = BlackPen;
                                    MessageBox.Show("Pen set to " + args[1]);
                                    break;

                                case "white":
                                    myPen = WhitePen;
                                    MessageBox.Show("Pen set to " + args[1]);
                                    break;

                                case "green":
                                    myPen = GreenPen;
                                    MessageBox.Show("Pen set to " + args[1]);
                                    break;

                                case "red":
                                    myPen = RedPen;
                                    MessageBox.Show("Pen set to " + args[1]);
                                    break;

                                case "blue":
                                    myPen = BluePen;
                                    MessageBox.Show("Pen set to " + args[1]);
                                    break;

                                case "size":
                                    myPen.Width = int.Parse(args[2]);
                                    MessageBox.Show("Pen set to " + args[1] + args[2]);
                                    break;
                            }
                            break;

                        case ("rect"):
                            if (args.Length > 3) {
                                MessageBox.Show("Error: too many parameters");
                                err = "+args";
                                break;
                            }
                            else {
                                DrawRect(g, float.Parse(args[1]), float.Parse(args[2]), myPen);
                                break;
                            }
                        case ("circle"):
                            if (args.Length > 2) {
                                MessageBox.Show("Error: too many parameters");
                                err = "+args";
                                break;
                            }
                            else {
                                DrawCircle(g, float.Parse(args[1]), myPen);
                                break;
                            }

                        case ("triangle"):
                            if (args.Length > 4) {
                                MessageBox.Show("Error: too many parameters");
                                err = "+args";
                                break;
                            }
                            else {
                                DrawTriangle(g, float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), myPen);
                                break;
                            }

                        case ("clear"):
                            Clear(g);
                            break;

                        case ("moveto"):
                            if (args.Length > 3) {
                                MessageBox.Show("Error: too many parameters");
                                err = "+args";
                                break;
                            }
                            else {
                                penPos[0] = int.Parse(args[1]);
                                penPos[1] = int.Parse(args[2]);
                                break;
                            }

                        case ("drawto"):
                            if (args.Length > 3) {
                                MessageBox.Show("Error: too many parameters");
                                err = "-args";
                                break;
                            }
                            else {
                                DrawTo(g, float.Parse(args[1]), float.Parse(args[2]));
                                break;
                            }
                            break;

                        

                        case ("reset"):
                            penPos[0] = 0;
                            penPos[1] = 0;
                            break;

                        case ("load"):
                            if (args.Length > 2) {
                                MessageBox.Show("Error: too many parameters");
                                err = "-args";
                                break;
                            }
                            else {
                                richTextBox1.Text = File.ReadAllText(args[1]);
                                break;
                            }

                        case ("save"):
                            using (File.Create(args[1])) ;
                            richTextBox1.SaveFile(args[1], RichTextBoxStreamType.PlainText);
                            break;

                        default:
                            MessageBox.Show("Error: Unrecognised Command" + args.ToString());
                            err = "unrec";
                            break;
                    } 
                }
            }
            catch (ArgumentNullException) {
                err = "nopen";
                MessageBox.Show("Please choose a pen first");
            }
            catch (IndexOutOfRangeException) {
                err = "-args";
                int l = args.Length - 1;
                String msg = "Error: too few arguments (" + l.ToString() + ") for command \"" + args[0] + "\""; 
                MessageBox.Show(msg);
            }
            catch (FileNotFoundException) {
                err = "nofile";
                MessageBox.Show("Error: File Not Found");
            }
            
            }

        void DrawRect(Graphics g, float w, float h, Pen p) {
            g.DrawRectangle(p, penPos[0], penPos[1], penPos[0] + w, penPos[1] + h);
            Refresh();
        }

        void DrawCircle(Graphics g, float d, Pen p) {
            g.DrawEllipse(p, penPos[0], penPos[1], penPos[0] + d, penPos[1] + d);
            Refresh();
        }

        void Clear(Graphics g) {
            g.Clear(Color.White);
            Refresh();
        }

        public void Clear() {
            using (var g = Graphics.FromImage(pictureBox1.Image)) {
                g.Clear(Color.White);
                Refresh();
            }
        }

        void DrawTriangle(Graphics g, float L1, float L2, float L3, Pen p) {
            if (L1*L1 + L2*L2 != L3 * L3) { // Check if the triangle is valid
                err = "badtrig";
                MessageBox.Show("Error: This is not a valid right-angled triangle. Did you mean " + L1.ToString() + L2.ToString() + Math.Sqrt(L1 * L1 + L2 * L2).ToString() + "?");
                return;
                }
            else {
                g.DrawLine(p, penPos[0], penPos[1], penPos[0] + L1, penPos[1]);
                g.DrawLine(p, penPos[0], penPos[1], penPos[0], penPos[1] + L2);
                } 
         }

        void DrawTo(Graphics g, float x, float y) {
        g.DrawLine(myPen, penPos[0], penPos[1], penPos[0] + x, penPos[1] + y);
        }
    }
}

