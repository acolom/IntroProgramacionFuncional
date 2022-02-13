using lib.tests.Data.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace lib.tests.Step01
{
    public class Step01
    {
        [Fact]

        public void Step01_If_Access_Value_On_Fail_Expect_Exception()
        {
            var failed = Maybe<int>.None();
            Assert.Throws<InvalidOperationException>(() => failed.Value);
        }

        [Fact]

        public void Step01_CheckIdentity()
        {
            Maybe<int> maybeInt = 1;

            Assert.True(maybeInt.Success);
            Assert.True(maybeInt.Value == 1);
        }

        [Fact]

        public void Step01_CheckIdentity_Ok()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);

            Assert.True(maybeInt.Success);
            Assert.True(maybeInt.Value == 1);
        }


        public void Step01_Sample_01()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue
            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReference = string.Empty;

            var file = File.CreateFromPath(path);
            if (file.Success)
            {
                var excelFile = ExcelFile.CreateFromFile(file.Value);
                if (excelFile.Success)
                {
                    var sheet = ExcelSheet.Find(excelFile.Value, sheetName);
                    if (sheet.Success)
                    {
                        var cell = ExcelCell.FindInSheet(sheet.Value, cellReference);
                        if (cell.Success)
                        {
                            // hacer algo con cell value
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

        public void Step01_Sample_02()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue

            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReference = string.Empty;

            var file = File.CreateFromPath(path);
            if (!file.Success)
                return;

            var excelFile = ExcelFile.CreateFromFile(file.Value);
            if (!excelFile.Success)
                return;

            var sheet = ExcelSheet.Find(excelFile.Value, sheetName);
            if (!sheet.Success)
                return;

            var cell = ExcelCell.FindInSheet(sheet.Value, cellReference);
            if (!cell.Success)
                return;

            // hacer algo con cell value
        }
    }
}
