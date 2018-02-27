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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using voltaire.PageModels;
using Xamarin.Forms;
using Syncfusion.Drawing;
using voltaire.Pages;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;

namespace voltaire.Helpers
{
    public class InvoiceGenerate
    {
        #region Fields & Properties

        private const string header1 = "Rebelle Sellier - Forestier SAS";
        private const string header2 = "Technopole Izarbel";
        private const string header3 = "231 allée Fauste d'Elhuyar";
        private const string header4 = "64210 BIDART";
        private const string header5 = "France";


        private const string footerPage = "Rebelle : 09 72 42 27 19 - Forestier : 09 72 45 66 46 | jeanne@rebellesellier.com - forever@forestier.com | RebelleSellier.com - Forestier.com Siret: 805075702 00011 Compte bancaire: CIC: 10057 19014 00020008603 35";

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

        List<ProductQuotationModel> OrderItems { get; set; }

        Partner Customer { get; set; }

        QuotationsModel model { get; set; }

        byte[] Signature { get; set; }

        bool GenerateFullVersion { get; set; }

        #endregion Fields & Properties


        public byte[] CreatePdfFile(QuotationsModel saleOrder, List<ProductQuotationModel> items, Partner customer,byte[] signature,bool generateFullVersion)
        {
            SaleOrder = saleOrder.SaleOrder;
            OrderItems = items;
            Customer = customer;
            model = saleOrder;
            Signature = signature;
            GenerateFullVersion = generateFullVersion;

            //Create a new document
            _document = new PdfDocument();
            _document.PageSettings.Size = PdfPageSize.A4;
            _document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            _document.PageSettings.SetMargins(10, 10);

            PdfPage page = _document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

           
            if (true)
            {

                var format = new PdfStringFormat();
                //format.NoClip = true;
                format.Alignment = PdfTextAlignment.Center;
                format.WordWrap = PdfWordWrapType.Word;
                format.LineAlignment = PdfVerticalAlignment.Bottom;

                PdfPageTemplateElement footer = new PdfPageTemplateElement(_document.Pages[0].GetClientSize().Width - 100,30);

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica,7);
                PdfBrush brush = new PdfSolidBrush(Color.Black);

                footer.Alignment = PdfAlignmentStyle.BottomCenter;

                footer.Graphics.DrawLine(new PdfPen(Color.FromArgb(255, 0, 0, 0)) { Width = 0.25f }, 0, 0, _document.Pages[0].GetClientSize().Width - 20, 0);
                footer.Graphics.DrawString(footerPage, font, brush, new RectangleF(0, 0, _document.Pages[0].GetClientSize().Width - 100, 30), format);

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

            if(GenerateFullVersion)
            DrawProperties();
        }

        private void DrawProperties()
        {
            foreach (var item in OrderItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Product.ConfigurationDetail))
                {
                    PdfPage page = _document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    var properties = JsonConvert.DeserializeObject<List<ProductProperty>>(item.Product.ConfigurationDetail);

                    graphics.DrawString(item.Product.ProductKind, _font14Bold, _brushBlack,
                                 new PointF(0, 10));
                    
                    if (properties != null && properties.Any())
                    {
                        int y = 40;

                        for (int i = 0; i < properties.Count; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(properties[i].PropertyValue))
                            {
                                graphics.DrawString(properties[i].PropertyName+" :", _font10Regular, _brushBlack,
                                 new PointF(0, y));
                                y = y + 15;
                                graphics.DrawString(properties[i].PropertyValue, _font10Regular, _brushGreyishBrownTwo,
                                    new PointF(0, y));
                                y = y + 15;
                            }
                        }
                    }                                  
                }
            }
        }


        private void DrawDataTable(PdfPage page, PdfGraphics graphics)
        {
            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();


            //Pagination
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitColumnsToPage;
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
            pdfGrid.Columns.Add(5);

            pdfGrid.Columns[0].Width = 270;
            pdfGrid.Columns[1].Width = 50;
            pdfGrid.Columns[2].Width = 70;
            pdfGrid.Columns[3].Width = 80;
            pdfGrid.Columns[4].Width = 100;           


            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

           
            pdfGridHeader.Cells[0].Value = "Description";
            pdfGridHeader.Cells[1].Value = "Taxes";
            pdfGridHeader.Cells[2].Value = "Quantité";
            pdfGridHeader.Cells[3].Value = "Prix unitaire";
            pdfGridHeader.Cells[4].Value = "Prix";         


            //centre text dans cellule
            for (var i = 0; i < 5; i++)
            {
                pdfGridHeader.Cells[i].StringFormat = centredText;
            }

            //Add Rows
            string logo = model.CurrencyLogo;



            Stream fontstream = typeof(ContactsPage).GetTypeInfo().Assembly.GetManifestResourceStream("voltaire.Resources.Raleway-Regular.ttf");  

            for (int i = 0; i <  OrderItems.Count; i++)
            {
                
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();

                pdfGridRow.Style.Font = new PdfTrueTypeFont(fontstream,12);

                pdfGridRow.Cells[0].Value = string.IsNullOrEmpty(OrderItems[i].Description) ? "" : OrderItems[i].Description;
                pdfGridRow.Cells[0].StringFormat = leftText;

                pdfGridRow.Cells[1].Value = OrderItems[i].TaxPercent.ToString() + "%";
                pdfGridRow.Cells[1].StringFormat = centredText;

                pdfGridRow.Cells[2].Value = OrderItems[i].Quantity.ToString();
                pdfGridRow.Cells[2].StringFormat = centredText;

                pdfGridRow.Cells[3].Value = OrderItems[i].Product.PriceUnit + logo;
                pdfGridRow.Cells[3].StringFormat = centredText;

                pdfGridRow.Cells[4].Value = (OrderItems[i].Product.PriceUnit * OrderItems[i].Product.ProductQty) + logo;
                pdfGridRow.Cells[4].StringFormat = rightText;                            

            }

            //ajout sub total
            PdfGridRow pdfGridRowTotal = pdfGrid.Rows.Add();
            pdfGridRowTotal.Style.Font = new PdfTrueTypeFont(fontstream, 12);
            pdfGridRowTotal.Cells[0].ColumnSpan = 4;
            pdfGridRowTotal.Cells[0].Value = "Total HT";
            pdfGridRowTotal.Cells[0].StringFormat = rightText;

            pdfGridRowTotal.Cells[4].Value = SaleOrder.AmountUntaxed + logo;
            pdfGridRowTotal.Cells[4].StringFormat = rightText;

            //ajout taxes
            if (true)
            {
                PdfGridRow pdfGridRowDeposit = pdfGrid.Rows.Add();
                pdfGridRowDeposit.Style.Font = new PdfTrueTypeFont(fontstream, 12);

                pdfGridRowDeposit.Cells[0].ColumnSpan = 4;
                pdfGridRowDeposit.Cells[0].Value = "Taxes";
                pdfGridRowDeposit.Cells[0].StringFormat = rightText;

                pdfGridRowDeposit.Cells[4].Value = SaleOrder.AmountTax + logo;
                pdfGridRowDeposit.Cells[4].StringFormat = rightText;
            }

            //Total
            PdfGridRow pdfGridRowtotal = pdfGrid.Rows.Add();
            pdfGridRowtotal.Style.Font = new PdfTrueTypeFont(fontstream, 12);

            pdfGridRowtotal.Cells[0].ColumnSpan = 4;
            pdfGridRowtotal.Cells[0].Value = "Total";
            pdfGridRowtotal.Cells[0].StringFormat = rightText;

            pdfGridRowtotal.Cells[4].Value = $"{SaleOrder.AmountTotal}{logo}";
            pdfGridRowtotal.Cells[4].StringFormat = rightText;


            //signature
            if (Signature != null)
            {
                PdfGridRow pdfGridRowsign = pdfGrid.Rows.Add(); 

                pdfGridRowsign.Height = 70;

                PdfBitmap pBmp = new PdfBitmap(new MemoryStream(Signature));
              
                pdfGridRowsign.Cells[0].Style.BackgroundImage = pBmp;
                pdfGridRowsign.Cells[0].Value = "";
            }

            //Style du PdfGrid
            PdfGridBuiltinStyleSettings tableStyleOption = new PdfGridBuiltinStyleSettings();
            tableStyleOption.ApplyStyleForBandedRows = true;
            tableStyleOption.ApplyStyleForHeaderRow = true;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            //ecriture du pdfGrid
            resultDataTable = pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, 300), layoutFormat);



        }

        private void DrawHeaderOfDocument(PdfGraphics graphics)
        {
            graphics.DrawLine(new PdfPen(Color.FromArgb(255, 0, 0, 0)){Width = 0.25f}, 0, 5, _document.PageSettings.Width - 20, 5);

            graphics.DrawString(header1, _font10Regular, _brushGreyishBrownTwo,
                         new PointF(0, 10));
            graphics.DrawString(header2, _font10Regular, _brushGreyishBrownTwo,
                new PointF(0, 25));
            graphics.DrawString(header3, _font10Regular, _brushGreyishBrownTwo,
                new PointF(0, 40));
            graphics.DrawString(header4, _font10Regular, _brushGreyishBrownTwo,
               new PointF(0, 55));
            graphics.DrawString(header5, _font10Regular, _brushGreyishBrownTwo,
               new PointF(0, 70));
          
            graphics.DrawLine(new PdfPen(Color.FromArgb(255, 0, 0, 0)){ Width = 0.25f },0,90,_document.PageSettings.Width-10,90);

            graphics.DrawString("Adresse de facturation et livraison", _font14Bold, _brushGreyishBrownTwo,
                        new PointF(0, 100));

            graphics.DrawString(string.IsNullOrEmpty(Customer.Name) ? "" :Customer.Name, _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 125));
            graphics.DrawString(string.IsNullOrEmpty(Customer.ContactAddress) ? "" : Customer.ContactAddress.Replace("\n", " "), _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 140));
            graphics.DrawString(string.IsNullOrEmpty(Customer.Website) ? "" :Customer.Website, _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 155));
            graphics.DrawString(string.IsNullOrEmpty(Customer.Phone) ? "" : Customer.Phone, _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 170));
            graphics.DrawString(string.IsNullOrEmpty(Customer.Email) ? "" : Customer.Email, _font10Regular, _brushGreyishBrownTwo,
             new PointF(0, 185));


            graphics.DrawString("N° de commande  " + SaleOrder.ClientOrderRef, _font14Bold, _brushGreyishBrownTwo,
                       new PointF(0, 210));

            graphics.DrawString("Date de commande :", _font10Regular, _brushBlack,
              new PointF(0, 230));
            graphics.DrawString(SaleOrder.CreateDate.ToString("G"), _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 245));

            graphics.DrawString("Prescripteur :", _font10Regular, _brushBlack,
             new PointF(150, 230));
            graphics.DrawString(string.IsNullOrEmpty(SaleOrder.TrainerName) ? "" : SaleOrder.TrainerName, _font10Regular, _brushGreyishBrownTwo,
              new PointF(150, 245));


            graphics.DrawString("Payment Method :", _font10Regular, _brushBlack,
             new PointF(0, 260));
            graphics.DrawString(string.IsNullOrEmpty(SaleOrder.PaymentMethod) ? "" : SaleOrder.PaymentMethod, _font10Regular, _brushGreyishBrownTwo,
              new PointF(0, 275));

            graphics.DrawString("Payment Note :", _font10Regular, _brushBlack,
            new PointF(150, 260));
            graphics.DrawString(string.IsNullOrWhiteSpace(SaleOrder.PaymentNote) ? "" : SaleOrder.PaymentNote, _font10Regular, _brushGreyishBrownTwo,
              new PointF(150, 275));

           
        }

       
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
