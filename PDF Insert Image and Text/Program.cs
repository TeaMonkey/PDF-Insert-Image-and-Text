using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Signatures;
using System;
using System.Collections.Generic;

namespace PDF_Insert_Image_and_Text
{
    class Program
    {
        static void Main(string[] args)
        {
            var pdfSource = @"F:\Source Code\PDF Insert Image and Text\PDF Insert Image and Text\Data\cremation-form-1-app-for-cremation-of-body.pdf";
            var pdfDestination = @"F:\Source Code\PDF Insert Image and Text\PDF Insert Image and Text\Data\Output\result.pdf";
            var imgSource = @"F:\Source Code\PDF Insert Image and Text\PDF Insert Image and Text\Data\NapoleonSignature.jpg";

            PdfDocument pdfDocument = FillInForm(pdfSource, pdfDestination);


            //IMPORTANT - This is NOT electronically signing the document, we have just stuck an image onto the PDF and called it a day
            //Figure out how electronic signing works and do it properly!

            SignDoc(pdfDocument, imgSource);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void SignDoc(PdfDocument pdfDocument, string imgSource)
        {
            //PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfSource), new PdfWriter(pdfDestination));
            Document document = new Document(pdfDocument);

            ImageData imageData = ImageDataFactory.Create(imgSource);

            Image image = new Image(imageData).ScaleAbsolute(382, 48).SetFixedPosition(6, 98, 97);
            document.Add(image);
            document.Close();
        }

        private static PdfDocument FillInForm(string pdfSource, string pdfDestination)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfSource), new PdfWriter(pdfDestination));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField toSet;
            if (!fields.TryGetValue("P8_Name", out toSet))
            {
                Console.WriteLine("Can not find field");
            }
            toSet.SetValue("Napoleon Bonaparte");

            //if (!fields.TryGetValue("Signature", out toSet))
            //{
            //    Console.WriteLine("Can not find field");
            //}

            //PdfSigner signer = new PdfSigner()



            // document.Close();
            //pdfDocument.Close();

            return pdfDocument;
        }
    }
}
