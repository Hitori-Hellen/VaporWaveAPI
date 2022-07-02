using API_PBL.Models.DatabaseModels;
using OfficeOpenXml;

namespace API_PBL.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<List<Game>> GetGameListAsync(IFormFile file)
        {
            var list = new List<Game>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;

                    if (rowcount == 0)
                        return null;

                    for (int row = 2; row <= rowcount; row++)
                    {
                        for (int col = 1; col <= 10; col++)
                        {
                            if (worksheet.Cells[row, col].Value == null)
                                throw new NullReferenceException("Data from excel file has null value");
                        }
                        list.Add(new Game
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            ReleaseDate = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            AgeRating = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                            GameRating = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            Price = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            Description = Convert.ToDecimal(worksheet.Cells[row, 6].Value),
                            Developer = worksheet.Cells[row, 7].Value.ToString().Trim(),
                            Publisher = worksheet.Cells[row, 8].Value.ToString().Trim(),
                            Website = worksheet.Cells[row, 9].Value.ToString().Trim(),
                            Spec = worksheet.Cells[row, 10].Value.ToString().Trim(),
                        });
                    }
                }
            }
            return list;
        }
    }
}
