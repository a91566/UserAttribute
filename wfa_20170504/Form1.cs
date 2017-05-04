using System;
using System.Windows.Forms;

namespace wfa_20170504
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var result = Person.GetTableFields<Person>();
			this.textBox1.Text = $"SELECT {result.Fields} FROM {result.TableName}";
			this.textBox1.Text += Environment.NewLine;
			this.textBox1.Text += Environment.NewLine;
			this.textBox1.Text += Environment.NewLine;
			result = Student.GetTableFields<Student>();
			this.textBox1.Text += $"SELECT {result.Fields} FROM {result.TableName}";
		}

		
		
	}
}
