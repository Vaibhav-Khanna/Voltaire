using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using RectangleF = Syncfusion.Drawing.RectangleF;
using voltaire.Models.DataObjects;
using voltaire.Models;
using System.Collections.Generic;

namespace voltaire.Helpers
{
    public class InvoiceGenerate
    {
        #region Fields & Properties

        private const int ItemHeight = 50;

        private const int HeaderHeight = 250;

        private const int MaxItemsForOnePageCount = 7;

        private const int MaxItemsForMultiPageOnFirstPageCount = 12;

        private const int MaxItemsForMultiPageOnPageCount = 17;

        private int _remainItemsCount;

        private PdfStringFormat _format = new PdfStringFormat();

        //private User _currentUser = new User();

        private PdfDocument _document;

        //private InvoiceListViewModel _invoice;

        private readonly PdfBrush _brushGreyishBrownTwo = new PdfSolidBrush(Color.FromArgb(255, 84, 84, 84));
        private readonly PdfBrush _brushBlack = new PdfSolidBrush(Color.FromArgb(255, 0, 0, 0));
        private readonly PdfBrush _warmGreyTwo = new PdfSolidBrush(Color.FromArgb(255, 138, 138, 138));
        private readonly PdfBrush _white = new PdfSolidBrush(Color.White);
        private readonly PdfBrush _greyishTwo = new PdfSolidBrush(Color.FromArgb(255, 184, 184, 184));

        //Set the font

        private readonly PdfFont _font14Bold = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
        private readonly PdfFont _font14Regular = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Regular);
        private readonly PdfFont _font10Regular = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
        private readonly PdfFont _font30 = new PdfStandardFont(PdfFontFamily.Helvetica, 30, PdfFontStyle.Bold);
        private readonly PdfFont _font20 = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Regular);
        private readonly PdfFont _font12 = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Regular);

        //private Account _account;

        public string Title { get; set; }
        public string InvoiceCreatedDate { get; set; }

        public byte[] ArrayBytes { get; set; }

        private Stream pdfstream;
        public Stream PdfDocumentStream { get { return pdfstream; } set { pdfstream = value; RaisePropertyChanged(); } }


        public bool? PaymentButtonVisible { get; set; }

        //Note
        private PdfLayoutResult resultDataTable;
        private int pageNumber;

        SaleOrder SaleOrder { get; set; }

        List<SaleOrderLine> OrderItems { get; set; }

        #endregion Fields & Properties


        public byte[] CreatePdfFile(SaleOrder saleOrder, List<SaleOrderLine> items)
        {
            SaleOrder = saleOrder;
            OrderItems = items;

            //Create a new document
            _document = new PdfDocument();
            _document.PageSettings.Size = PdfPageSize.A4;
            _document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            _document.PageSettings.SetMargins(10, 10);

            PdfPage page = _document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            if (_account.InvoiceConditions != null)
            {

                var format = new PdfStringFormat();
                //format.NoClip = true;
                format.Alignment = PdfTextAlignment.Center;
                format.WordWrap = PdfWordWrapType.Word;
                format.LineAlignment = PdfVerticalAlignment.Bottom;

                PdfPageTemplateElement footer = new PdfPageTemplateElement(_document.Pages[0].GetClientSize().Width - 100, 50);

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
                PdfBrush brush = new PdfSolidBrush(Color.Black);

                footer.Alignment = PdfAlignmentStyle.BottomCenter;

                footer.Graphics.DrawString(_account.InvoiceConditions, font, brush, new RectangleF(0, 0, _document.Pages[0].GetClientSize().Width - 100, 50), format);

                _document.Template.Bottom = footer;

            }

            //CONTENT PRINCIPAL PAGE
            DrawPage(page, graphics);


            var stream = new MemoryStream();

            //save the document into stream

            _document.Save(stream);

            PdfDocumentStream = stream;

            ArrayBytes = stream.ToArray();

            //close the document
            _document.Close(true);

            Debug.WriteLine("End of creating file");

            return ArrayBytes;
        }

        private void DrawPage(PdfPage page, PdfGraphics graphics)
        {
            DrawHeaderOfDocument(graphics);

            DrawDataTable(page, graphics);

            DrawCGV();
        }

        private void DrawCGV()
        {
            if (!string.IsNullOrEmpty(_account.Cgv))
            {
                PdfPage page = _document.Pages.Add();
                PdfTextElement textElement = new PdfTextElement(_account.Cgv, new PdfStandardFont(PdfFontFamily.Helvetica, 10));

                PdfLayoutFormat layoutFormat = new PdfLayoutFormat();

                layoutFormat.Layout = PdfLayoutType.Paginate;
                layoutFormat.Break = PdfLayoutBreakType.FitPage;

                textElement.Draw(page, new Syncfusion.Drawing.RectangleF(0, 0, page.GetClientSize().Width, page.GetClientSize().Height), layoutFormat);
            }
        }


        private void DrawDataTable(PdfPage page, PdfGraphics graphics)

        {
            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Pagination
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Cell Padding
            pdfGrid.Style.CellPadding.All = 4;
            //Cell align text
            PdfStringFormat centredText = new PdfStringFormat()
            {
                LineAlignment = PdfVerticalAlignment.Middle,
                Alignment = PdfTextAlignment.Center
            };
            PdfStringFormat leftText = new PdfStringFormat()
            {
                LineAlignment = PdfVerticalAlignment.Middle,
                Alignment = PdfTextAlignment.Left
            };
            PdfStringFormat rightText = new PdfStringFormat()
            {
                LineAlignment = PdfVerticalAlignment.Middle,
                Alignment = PdfTextAlignment.Right
            };

            //Add 7 column
            pdfGrid.Columns.Add(7);

            pdfGrid.Columns[0].Width = 20;
            pdfGrid.Columns[1].Width = 250;
            pdfGrid.Columns[2].Width = 35;
            pdfGrid.Columns[3].Width = 50;
            pdfGrid.Columns[4].Width = 70;
            pdfGrid.Columns[5].Width = 40;
            pdfGrid.Columns[6].Width = 100;

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

            pdfGridHeader.Cells[0].Value = "#";
            pdfGridHeader.Cells[1].Value = "Description";
            pdfGridHeader.Cells[2].Value = "Qté";
            pdfGridHeader.Cells[3].Value = "Unité";
            pdfGridHeader.Cells[4].Value = "Prix";
            pdfGridHeader.Cells[5].Value = "TVA";
            pdfGridHeader.Cells[6].Value = "Total";

            //centre text dans cellule
            for (var i = 0; i < 7; i++)
            {
                pdfGridHeader.Cells[i].StringFormat = centredText;
            }

            //Add Rows

            for (int i = 0; i < _invoice.InvoicePDFModel.InvoiceItems.Count; i++)
            {

                var items = _invoice.InvoicePDFModel.InvoiceItems;
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();

                pdfGridRow.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                pdfGridRow.Cells[0].Value = items[i].InvoiceIndexNumber.ToString();
                pdfGridRow.Cells[0].StringFormat = centredText;

                pdfGridRow.Cells[1].Value = items[i].InvoiceNumber;
                pdfGridRow.Cells[0].StringFormat = leftText;

                pdfGridRow.Cells[2].Value = items[i].Amount.ToString();
                pdfGridRow.Cells[2].StringFormat = centredText;

                pdfGridRow.Cells[3].Value = items[i].Unit;
                pdfGridRow.Cells[3].StringFormat = centredText;

                pdfGridRow.Cells[4].Value = items[i].Price.ToString("C");
                pdfGridRow.Cells[4].StringFormat = centredText;

                pdfGridRow.Cells[5].Value = $"{items[i].Taxes}%";
                pdfGridRow.Cells[5].StringFormat = centredText;

                pdfGridRow.Cells[6].Value = items[i].Total.ToString("C");
                pdfGridRow.Cells[6].StringFormat = centredText;

            }

            //ajout total
            PdfGridRow pdfGridRowTotal = pdfGrid.Rows.Add();
            pdfGridRowTotal.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            pdfGridRowTotal.Cells[0].ColumnSpan = 6;
            pdfGridRowTotal.Cells[0].Value = "Total en Euros";
            pdfGridRowTotal.Cells[0].StringFormat = rightText;

            pdfGridRowTotal.Cells[6].Value = _invoice.InvoicePDFModel.SubTotal;
            pdfGridRowTotal.Cells[6].StringFormat = centredText;

            //ajout deposit
            if (_invoice.Invoice.Deposit > 0)
            {
                PdfGridRow pdfGridRowDeposit = pdfGrid.Rows.Add();
                pdfGridRowTotal.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                pdfGridRowDeposit.Cells[0].ColumnSpan = 6;
                pdfGridRowDeposit.Cells[0].Value = "Acompte";
                pdfGridRowDeposit.Cells[0].StringFormat = rightText;

                pdfGridRowDeposit.Cells[6].Value = $"{_invoice.Invoice.Deposit}€";
                pdfGridRowDeposit.Cells[6].StringFormat = centredText;
            }


            //Style du PdfGrid
            PdfGridBuiltinStyleSettings tableStyleOption = new PdfGridBuiltinStyleSettings();
            tableStyleOption.ApplyStyleForBandedRows = true;
            tableStyleOption.ApplyStyleForHeaderRow = true;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            //ecriture du pdfGrid
            resultDataTable = pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 200), layoutFormat);

            // ajout de Note si Note présente à la suite du tableau de resultat 
            if (_invoice.Invoice.Note != null)
            {
                PdfBrush brush = new PdfSolidBrush(Color.Black);

                if (_account.Cgv != null)
                {
                    pageNumber = _document.PageCount - 1;
                }
                else
                {
                    pageNumber = _document.PageCount;
                }
                var note = "Note : " + _invoice.Invoice.Note;
                PdfTextElement textElement = new PdfTextElement(note, new PdfStandardFont(PdfFontFamily.Helvetica, 10));
                PdfLayoutFormat format = new PdfLayoutFormat();
                layoutFormat.Layout = PdfLayoutType.Paginate;
                layoutFormat.Break = PdfLayoutBreakType.FitElement;

                //formatage des données (alignement centre)
                var stringFormat = new PdfStringFormat();
                stringFormat.Alignment = PdfTextAlignment.Center;
                textElement.StringFormat = stringFormat;

                textElement.Draw(page, new Syncfusion.Drawing.RectangleF(pageNumber, resultDataTable.Bounds.Bottom + 10, graphics.ClientSize.Width - 50, 50), format);

            }

        }

        private void DrawHeaderOfDocument(PdfGraphics graphics)
        {
            graphics.DrawString(_invoice.InvoicePDFModel.AccountLabel.CheckNullString(), _font14Bold, _brushGreyishBrownTwo,
                         new PointF(0, 10));
            graphics.DrawString(_invoice.InvoicePDFModel.Address != null ? _invoice.InvoicePDFModel.Address : "", _font14Regular, _brushGreyishBrownTwo,
                new PointF(0, 25));
            graphics.DrawString("SIRET/RCS:", _font10Regular, _brushGreyishBrownTwo,
                new PointF(0, 42));
            graphics.DrawString(_invoice.InvoicePDFModel.Siret != null ? _invoice.InvoicePDFModel.Siret : "", _font10Regular, _brushGreyishBrownTwo,
                new PointF(60, 42));
            if (_invoice.InvoicePDFModel.Phone != null && _invoice.InvoicePDFModel.Email != null)
            {
                graphics.DrawString(_invoice.InvoicePDFModel.Phone != null ? _invoice.InvoicePDFModel.Phone : "", _font10Regular, _brushGreyishBrownTwo,
                    new PointF(0, 55));
                graphics.DrawString(_invoice.InvoicePDFModel.Email != null ? _invoice.InvoicePDFModel.Email : "", _font10Regular, _brushGreyishBrownTwo,
                    new PointF(0, 68));
            }
            if (_invoice.InvoicePDFModel.Phone != null && _invoice.InvoicePDFModel.Email == null)
            {
                graphics.DrawString(_invoice.InvoicePDFModel.Phone != null ? _invoice.InvoicePDFModel.Phone : "", _font10Regular, _brushGreyishBrownTwo,
                    new PointF(0, 55));
            }
            if (_invoice.InvoicePDFModel.Phone == null && _invoice.InvoicePDFModel.Email != null)
            {
                graphics.DrawString(_invoice.InvoicePDFModel.Email != null ? _invoice.InvoicePDFModel.Email : "", _font10Regular, _brushGreyishBrownTwo,
                    new PointF(0, 55));
            }

            graphics.DrawString(_invoice.InvoicePDFModel.InvoiceLabel, _font30, _brushBlack, new PointF(570, 33), _format);
            graphics.DrawString(string.IsNullOrEmpty(_invoice.Invoice.InvoiceNumber) ? string.Empty : $"# { _invoice.Invoice.InvoiceNumber}", _font20, _brushGreyishBrownTwo,
                new PointF(570, 83), _format);
            graphics.DrawString(_invoice.InvoicePDFModel.InvoiceDateString, _font12, _warmGreyTwo,
                new PointF(570, 145), _format);
            graphics.DrawString("Facturé à:", _font10Regular, _brushGreyishBrownTwo,
                new PointF(0, 112));
            graphics.DrawString(string.IsNullOrEmpty(_invoice.InvoicePDFModel.CustomerCompagny) ? string.Empty : _invoice.InvoicePDFModel.CustomerCompagny, _font12, _warmGreyTwo,
                new PointF(0, 125));
            graphics.DrawString(_invoice.InvoicePDFModel.CustomerFullName, _font12, _warmGreyTwo,
               new PointF(0, 138));
            graphics.DrawString(_invoice.InvoicePDFModel.CustomerAddress.CheckNullString(), _font12, _warmGreyTwo,
                new PointF(0, 151));
        }

       
    }
}
