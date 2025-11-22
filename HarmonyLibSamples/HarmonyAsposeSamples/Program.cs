using Aspose.Cells;
using Aspose.Slides;
using Aspose.Words;
using HarmonyAsposeSamples;
using HarmonyLib;
using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        // Aspose.Slides反编译后，代码混淆了，完全不知道代码逻辑，
        // 所以不知道如何用补丁绕过许可证验证
        //Harmony harmony = new Harmony("com.example.asposepatch");
        //harmony.PatchAll();

        CreatePdf();
        CreateWord();
        CreatePpt();
        CreateExcel();

        Console.Read();
    }

    private static void CreatePdf()
    {
        AsposeActivationHelper.SetPdfLicense();
        Console.WriteLine("PDF许可证有效！");
        // For complete examples and data files, please go to

        // Create PDF document
        using (var document = new Aspose.Pdf.Document())
        {
            // Add page
            var page = document.Pages.Add();

            // Create a text fragment
            var textFragment = new Aspose.Pdf.Text.TextFragment("Hello, Aspose.PDF for .NET!");
            textFragment.Position = new Aspose.Pdf.Text.Position(100, 600);

            // Add text to the page
            page.Paragraphs.Add(textFragment);

            // Save PDF document
            document.Save("sample.pdf");
        }
    }

    private static void CreateWord()
    {
        AsposeActivationHelper.SetWordsLicense();
        Console.WriteLine("Word许可证有效！");
        // For complete examples and data files, please go to https://github.com/aspose-words/Aspose.Words-for-.NET.git.
        Document docA = new Document();
        DocumentBuilder builder = new DocumentBuilder(docA);

        // Insert text to the document start.
        builder.MoveToDocumentStart();
        builder.Write("First Hello World paragraph");

        docA.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.docx"));
    }

    private static void CreatePpt()
    {
        if (AsposeActivationHelper.SetSlidesLicense().IsLicensed())
        {
            Console.WriteLine("Slides许可证有效！");
            // instantiate a Presentation object that represents a presentation file
            using (Presentation presentation = new Presentation())
            {
                // get the first slide
                ISlide slide = presentation.Slides[0];

                // add an autoshape of type line
                slide.Shapes.AddAutoShape(ShapeType.Line, 50, 150, 300, 0);
                presentation.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pptx"), Aspose.Slides.Export.SaveFormat.Pptx);
            }
        }
    }

    private static void CreateExcel()
    {
        AsposeActivationHelper.SetCellsLicense();
        Console.WriteLine("Excel许可证有效！");

        // Instantiate a Workbook object that represents Excel file.
        Workbook wb = new Workbook();

        // When you create a new workbook, a default "Sheet1" is added to the workbook.
        Worksheet sheet = wb.Worksheets[0];

        // Access the "A1" cell in the sheet.
        Aspose.Cells.Cell cell = sheet.Cells["A1"];

        // Input the "Hello World!" text into the "A1" cell
        cell.PutValue("Hello World!");

        // Save the Excel file.
        wb.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyBook_out.xlsx"));
    }
}