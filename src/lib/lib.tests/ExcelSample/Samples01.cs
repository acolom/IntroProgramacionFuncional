﻿using lib.tests.Data.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace lib.tests.ExcelSample
{
    public class Samples01
    {
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

        public void Step01_Sample_03()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue

            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReference = string.Empty;

            var cellResult = File.CreateFromPath(path)
                .Bind((file) => ExcelFile.CreateFromFile(file))
                .Bind((excelFile) => ExcelSheet.Find(excelFile, sheetName))
                .Bind((sheet) => ExcelCell.FindInSheet(sheet, cellReference));

            if (cellResult.Success)
            {
                // hacer algo con cell value
            }
        }

        public void Step01_Sample_04()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue

            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReference = string.Empty;

            File.CreateFromPath(path)
                .Bind((file) => ExcelFile.CreateFromFile(file))
                .Bind((excelFile) => ExcelSheet.Find(excelFile, sheetName))
                .Bind((sheet) => ExcelCell.FindInSheet(sheet, cellReference))
                .Match(cellValue =>
                {
                    // hacer algo con cell value
                });
        }

        public void Step01_Sample_05()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue

            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReference = string.Empty;

            var result = from file in File.CreateFromPath(path)
                         from excelFile in ExcelFile.CreateFromFile(file)
                         from sheet in ExcelSheet.Find(excelFile, sheetName)
                         from cellValue in ExcelCell.FindInSheet(sheet, cellReference)
                         select cellValue;

            result.Match(cellValue =>
            {
                // hacer algo con cell value
            });
        }

        public void Step01_Sample_06()
        {
            // Abrir fichero
            // Recoger excel
            // encontrar el sheet 
            // recogert el cellvalue

            var path = string.Empty;
            var sheetName = string.Empty;
            var cellReferenceA = string.Empty;
            var cellReferenceB = string.Empty;

            var result = from file in File.CreateFromPath(path)
                         from excelFile in ExcelFile.CreateFromFile(file)
                         from sheet in ExcelSheet.Find(excelFile, sheetName)
                         from cellValueA in ExcelCell.FindInSheet(sheet, cellReferenceA)
                         from cellValueB in ExcelCell.FindInSheet(sheet, cellReferenceB)
                         from combined in ExcelCell.CombineValues(cellValueA, cellValueB)
                         select combined;

            result.Match(combinedValues =>
            {
                // hacer algo con cell value
            });
        }
    }
}
