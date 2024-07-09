using DigitalFacultySystem.Client.Services.Interfaces;
using DigitalFacultySystem.Entities.Dtos.RequestResponse;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using iText.Layout;
using iText.Barcodes;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Layout.Borders;

namespace DigitalFacultySystem.Client.Pages.ExamSession
{
    public partial class Exam
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IGenericService<ExamDto> _examService { get; set; }
        [Inject]
        public IGenericService<StudentsInExamDto> _studentInExamService { get; set; }

        public List<StudentsInExamDto> studentsInExam = new();

        public ExamDto examModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = string.Empty;

        [Inject]
        public NavigationManager _navi { get; set; }

        private AlertCard alertCard;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var Id = Guid.Parse(this.Id);
                var exam = await _examService.GetById(Id, "api/exam");

                if (exam != null)
                    examModel = exam;

                await RefreshStudentsInExam();
            }
        }

        protected async Task RefreshStudentsInExam()
        {
            var Id = Guid.Parse(this.Id);
            studentsInExam = await _studentInExamService.GetAllById(Id, "api/exam/GetStudentsInExam");
        }

        protected void HandleInValidSumbit()
        {
            alertCard.ShowAlert("Ka disa gabime në validim. Ju lutemi provoni përsëri.", "alert-danger");
        }

        protected async Task HandleValidSumbit()
        {
            var result = await _examService.Update(examModel, "api/exam");
            if (result)
            {
                alertCard.ShowAlert("Provimi u përditësua me sukses.", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Pati një gabim gjatë përditësimit të provimit. Ju lutemi provoni përsëri.", "alert-danger");
            }
        }

        protected async Task UpdateStudentsInExam()
        {
            var result = await _studentInExamService.UpdateList(studentsInExam, "api/exam/UpdateStudentsInExam");
            if (result)
            {
                alertCard.ShowAlert("Notat dhe prezenca e studentëve në këtë provim u përditësuan me sukses.", "alert-success");
            }
            else
            {
                alertCard.ShowAlert("Pati një gabim gjatë përditësimit të studentëve në provim. Ju lutemi provoni përsëri.", "alert-danger");
            }
        }

        private async Task GeneratePdf()
        {
            var stream = new MemoryStream();

            // Define the image paths relative to wwwroot
            string logoUrl1 = "fti_logo.jpeg";
            string logoUrl2 = "upt_logo.png";

            // Fetch the images
            HttpClient client = new HttpClient();
            byte[] logoBytes1 = await client.GetByteArrayAsync(_navi.ToAbsoluteUri(logoUrl1));
            byte[] logoBytes2 = await client.GetByteArrayAsync(_navi.ToAbsoluteUri(logoUrl2));

            // Convert images to ImageData
            ImageData logoData1 = ImageDataFactory.Create(logoBytes1);
            ImageData logoData2 = ImageDataFactory.Create(logoBytes2);

            using (var writer = new PdfWriter(stream))
            {
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Generate a QR code with the exam information
                var qrCodeWriter = new BarcodeQRCode(Id);
                var qrCodeImage = qrCodeWriter.CreateFormXObject(pdf);
                var qrCode = new Image(qrCodeImage)
                    .SetWidth(100)  // Set the width of the QR code image
                    .SetHeight(100); // Set the height of the QR code image

                // Create a table for the header with 3 columns: logo1, qr code with text, logo2
                var headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1 })).UseAllAvailableWidth();

                // Add logos and QR code with text to the header table
                headerTable.AddCell(new Cell().Add(new Image(logoData1).ScaleToFit(100, 100)).SetBorder(Border.NO_BORDER));
                var middleCell = new Cell().SetBorder(Border.NO_BORDER);
                headerTable.AddCell(middleCell); // Empty cell for spacing
                var qrCodeCell = new Cell().SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER);
                qrCodeCell.Add(qrCode);
                qrCodeCell.Add(new Paragraph($"ID: {examModel.Id}").SetFontSize(7).SetTextAlignment(TextAlignment.CENTER));
                qrCodeCell.Add(new Paragraph($"Generated at: {DateTime.Now} ").SetFontSize(7).SetTextAlignment(TextAlignment.CENTER));
                headerTable.AddCell(qrCodeCell);

                // Add header table to document
                document.Add(headerTable);

                // Add the title and other elements
                document.Add(new Paragraph("Lista e studentëve për këtë provim").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18).SetMarginBottom(20));
                document.Add(new Paragraph($"Provimi: {examModel.Name}").SetFontSize(10).SetMarginBottom(1));
                document.Add(new Paragraph($"Data e provimit: {examModel.Date}").SetFontSize(10).SetMarginBottom(1));
                document.Add(new Paragraph($"Nr. i Studentöve: {studentsInExam.Count}").SetFontSize(10).SetMarginBottom(1));

                // Add table with students
                var table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 3, 2, 2 })).UseAllAvailableWidth();
                table.AddHeaderCell("Nr.");
                table.AddHeaderCell("Emri");
                table.AddHeaderCell("Mbiemri");
                table.AddHeaderCell("Prezent");
                table.AddHeaderCell("Nota");

                int i = 1;
                foreach (var student in studentsInExam)
                {
                    table.AddCell(i.ToString());
                    table.AddCell(student.StudentFirstName);
                    table.AddCell(student.StudentLastName);
                    table.AddCell(string.Empty);
                    table.AddCell(string.Empty);
                    i++;
                }

                document.Add(table);
                document.Add(new Paragraph("Shënim: Në këtë listë të gjeneruar automatikisht nga sistemi janë shfaqur vetëm studentët të cilët kanë përmbushur frekuentimin si dhe detyrimet e lëndës.").SetFontSize(10));

                document.Close();
            }

            // Convert stream to base64
            var base64 = Convert.ToBase64String(stream.ToArray());

            // Trigger file download in browser
            await JSRuntime.InvokeVoidAsync("saveAsFile", "students_in_exam.pdf", base64);
        }
    }
}
