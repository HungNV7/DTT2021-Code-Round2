using DTT2021_Round2.sample.dtos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Spire.Xls;

namespace DTT2021_Round2
{
     class ReadExcel
    {
        private static ReadExcel instance = new ReadExcel();
        private ReadExcel()
        { 
        }

        public static ReadExcel Instance { get { return instance; } }

        public string FileName { get; set; }

        public string GetPath()
        {
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            path = System.IO.Path.GetDirectoryName(path);
            path = System.IO.Path.GetDirectoryName(path);
            path += @"\Resources\";

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = path;
            openDialog.Filter = "Excel(*.xlsv,*.xls,*.csv,*.xlsx)|*.xlsv;*.xls;*.csv;*.xlsx";

            string fileName = String.Empty;
            if(openDialog.ShowDialog() == true)
            {
                fileName = openDialog.FileName;
            }
            return fileName;
        }

        public List<Question> GetQuestion(int isBackup)
        {
            List<Question> list = new List<Question>();
            Workbook workBook = new Workbook();
            Worksheet sheet = null;
            workBook.LoadFromFile(FileName);

            sheet = workBook.Worksheets[isBackup];

            for (int i = 2; i <= 7; i++)
            {
                int id = Convert.ToInt32(sheet[i, 1].NumberText.ToString());
                string detail = sheet[i, 2].Text;
                string answer = sheet[i, 4].Text;
                int numberOfDigit = Convert.ToInt32(sheet[i, 3].NumberText.ToString());

                list.Add(new Question(id, detail, answer, numberOfDigit));
            }
            return list;
        }
    }
}
