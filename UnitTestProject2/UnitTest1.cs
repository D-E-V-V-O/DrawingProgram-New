using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingProgram;
using System.Windows.Forms;

namespace UnitTestProject2 {
    [TestClass]
    public class UnitTest1 {
        Form1 form;
        public UnitTest1() {
            form = new Form1();
        }

        [TestMethod]
        public void TestCircleTooFewParams() { // Test too few parameters being passed to circle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "circle"};
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "-args");
            form.Clear();
        }

        [TestMethod]
        public void TestCircleTooManyParams() { // Test too many parameters being passed to circle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "circle", "50", "20" };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "+args");
            form.Clear();
        }

        [TestMethod]
        public void TestRectTooFewParams() { // Test too few parameters being passed to rectangle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "rect", "30", };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "-args");
            form.Clear();
        }

        [TestMethod]
        public void TestRectTooManyParams() { // Test too many parameters being passed to rectangle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "rect", "30", "10", "20" };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "+args");
            form.Clear();
        }

        [TestMethod]
        public void TestTrigTooFewParams() { // Test too few parameters being passed to triangle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "triangle", "30", "40", };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "-args");
            form.Clear();
        }

        [TestMethod]
        public void TestTrigTooManyParams() { // Test too many parameters being passed to triangle
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "triangle", "30", "40", "50", "50" };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "+args");
            form.Clear();
        }

        [TestMethod]
        public void TestUnknownCommand() { // Test an unknown command being passed
            form.err = null;
            String[] args1 = new String[] { "banoffee", "pie" };
            form.RunCommand(args1);
            Assert.AreEqual(form.err, "unrec");
            form.Clear();
        }

        [TestMethod]
        public void TestNoPen() { // Test handling of no pen having been set
            form.err = null;
            form.myPen = null;
            String[] args2 = new String[] { "circle", "50", };
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "nopen");
            form.Clear();
        }

        [TestMethod]
        public void TestNoFile() { // Test handling of an unrecognised file being passed
            form.err = null;
            String[] args1 = new String[] { "load", "C:\\i-dont-exist" };
            form.RunCommand(args1);
            Assert.AreEqual(form.err, "nofile");
            form.Clear();
        }

        [TestMethod]
        public void TestInvalidTriangle() { // Test an illegal right triangle being passed
            form.err = null;
            String[] args1 = new String[] { "pen", "black" };
            String[] args2 = new String[] { "triangle", "30", "40", "30" };
            form.RunCommand(args1);
            form.RunCommand(args2);
            Assert.AreEqual(form.err, "badtrig");
            form.Clear();
        }

        [TestMethod]
        public void TestVariableDeclaration() {
            form.err = null;
            String[] args1 = new String[] { "var", "counter", "=", "10" };
            form.RunCommand(args1);
            CollectionAssert.Contains(form.vars, "counter");
            int i = Array.IndexOf(form.vars, "counter");
            Assert.AreEqual(form.vars[1, i], 10);
            form.ClearVars();
        }

        [TestMethod]
        public void TestOpenLoop() {
            form.err = null;
            form.richTextBox1.Text += ("loop" + '\n' + "pen black" + '\n' + "circle 20");
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "openloop");
        }

        [TestMethod]
        public void TestTrailingCloseLoop() {
            form.err = null;
            form.richTextBox1.Text += ("pen black" + '\n' + "circle 20" + '\n' + "end");
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "trailingend");
        }

        [TestMethod]
        public void TestOpenIf() {
            form.err = null;
            form.richTextBox1.Text += ("var counter = 10" + '\n' + "if counter == 10" + '\n' + "pen black" + '\n' + "circle 50" );
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "openif");
        }

        [TestMethod]
        public void TestTrailingEndif() {
            form.err = null;
            form.richTextBox1.Text += ("var counter = 10" + '\n' + '\n' + "pen black" + '\n' + "circle 50" + '\n' + "endif");
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "trailingendif");
        }

        [TestMethod]
        public void TestOpenMethod() {
            form.err = null;
            form.richTextBox1.Text += ("method mymethod(pen)" + '\n' + "pen black" + '\n' + "circle 50");
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "openmethod");
        }

        [TestMethod]
        public void TestTrailingEndmethod() {
            form.err = null;
            form.richTextBox1.Text += ("var counter = 10" + '\n' + '\n' + "pen black" + '\n' + "circle 50" + '\n' + "endmethod");
            form.goButton.PerformClick();
            Assert.AreEqual(form.err, "trailingendmethod");
        }

        [TestMethod]
        public void TestTooFewMethodParams() {
            //TODO: Implement once method implementation is decided
        }

        [TestMethod]
        public void TestTooManyMethodParams() {
            //TODO: Implement once method implementation is decided
        }

        [TestMethod]
        public void TestSyntaxChecking() {
            //TODO: Implement once syntax checking is implemented
        }
    }
}
