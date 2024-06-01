using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace EşleştirmeOyunu
{
	public partial class Form3 : Form
	{
		string filename = "";
		public Form3()
		{
			InitializeComponent();

		}
		public void Form_Load(object sender, EventArgs e)
		{
		}
		private void button_writeandsave_Click(object sender, EventArgs e, string skor)
		{
			try
			{
				string skor1 = skor;
				List<string> student = new List<string>();
				object Missing = System.Reflection.Missing.Value; ;

				Excel.Application Excel_Student = new Excel.Application();
				Excel.Workbook Excel_Student_WB = Excel_Student.Workbooks.Add(Missing);
				Excel.Worksheet Excel_Student_WS = (Excel.Worksheet)Excel_Student_WB.Worksheets.get_Item(1);
				Excel.Range Excel_Student_Range = Excel_Student_WS.UsedRange;

				ExcelFile.Write(skor1, student, Excel_Student, Excel_Student_WB, Excel_Student_WS);
				if (filename != "")
				{
					ExcelFile.saveandclose(Excel_Student, Excel_Student_WB, Excel_Student_WS, Excel_Student_Range, Missing, filename);
				}
				else
				{
					filename = "student_list";
					ExcelFile.saveasandclose(Excel_Student, Excel_Student_WB, Excel_Student_WS, Excel_Student_Range, Missing, filename);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
