using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace example_split_pdf_dotnet
{

    // This example shows how to split a PDF document.
    // It references the ceTe.DynamicPDF.CoreSuite.NET NuGet package.
    class Program
    {
        static void Main(string[] args)
        {
            string pdfFilePath = @"C:\Users\armoshar.NORTHAMERICA\OneDrive - Microsoft\Projects\C#\pdfsplitter\pdfsplitter\Resources\test.pdf";
            string outputPath = @"C:\Users\armoshar.NORTHAMERICA\OneDrive - Microsoft\Projects\C#\pdfsplitter\pdfsplitter\Resources";
            int interval = 10;
            int pageNameSuffix = 0;

            // Intialize a new PdfReader instance with the contents of the source Pdf file:  
            PdfReader reader = new PdfReader(pdfFilePath);

            FileInfo file = new FileInfo(pdfFilePath);
            string pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + "-";

            Program obj = new Program();

            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber += interval)
            {
                pageNameSuffix++;
                string newPdfFileName = string.Format(pdfFileName + "{0}", pageNameSuffix);
                obj.SplitAndSaveInterval(pdfFilePath, outputPath, pageNumber, interval, newPdfFileName);
            }
        }


        private void SplitAndSaveInterval(string pdfFilePath, string outputPath, int startPage, int interval, string pdfFileName)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();

                for (int pagenumber = startPage; pagenumber < (startPage + interval); pagenumber++)
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, pagenumber));
                    }
                    else
                    {
                        break;
                    }

                }

                document.Close();
            }
        }
    }
}