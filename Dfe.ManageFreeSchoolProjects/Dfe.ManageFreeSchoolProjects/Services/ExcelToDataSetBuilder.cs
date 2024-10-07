using ExcelDataReader;
using System.Data;
using System.IO;

namespace Dfe.ManageFreeSchoolProjects.Services
{
    public static class ExcelToDataSetBuilder
    {
        public static DataSet Build(MemoryStream stream, string contentType)
        {
            using var reader = CreateReader(stream, contentType);

            var dataSet = reader.AsDataSet(BuildConfiguration());

            return dataSet;
        }
        private static IExcelDataReader CreateReader(MemoryStream stream, string contentType)
        {
            if (contentType == "text/csv")
            {
                return ExcelReaderFactory.CreateCsvReader(stream);
            }

            return ExcelReaderFactory.CreateReader(stream);
        }

        private static ExcelDataSetConfiguration BuildConfiguration()
        {
            return new ExcelDataSetConfiguration()
            {
                UseColumnDataType = false,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                },
            };
        }

    }
}
