using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.tests.Data.Excel
{

    public class File
    {
        public static Maybe<File> CreateFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Maybe<File>.None();

            return new File();
        }
    }

    public class ExcelFile
    {
        public static Maybe<ExcelFile> CreateFromFile(File file)
        {
            return new ExcelFile();
        }
    }

    public class ExcelSheet
    {
        public static Maybe<ExcelSheet> Find(ExcelFile file, string sheetName)
        {
            if (string.IsNullOrWhiteSpace(sheetName))
                return Maybe<ExcelSheet>.None();

            return new ExcelSheet();
        }
    }

    public class ExcelCell
    {
        public static Maybe<ExcelCell> FindInSheet(ExcelSheet file, string cellReference)
        {
            if (string.IsNullOrWhiteSpace(cellReference))
                return Maybe<ExcelCell>.None();

            return new ExcelCell();
        }
    }
}
