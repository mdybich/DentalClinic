using DentalClinic.Services.Exceptions;
using DentalClinic.Services.Helpers;
using DentalClinic.Services.Interfaces;
using DentalClinic.Services.Services;
using DentalClinic.UI.Enums;
using DentalClinic.UI.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DentalClinic.UI.ViewModels
{
    public class GenerateRaportWindowViewModel : ViewModelBase
    {
        #region Services Reference
        private readonly IEntryService _entryService;
        #endregion

        #region Properties For Binding
        private RaportType _raportType;
        private DateTime? _startDate;
        private DateTime? _endDate;

        public RaportType RaportType
        {
            get { return this._raportType; }
            set { Set(() => RaportType, ref this._raportType, value); }
        }

        public DateTime? StartDate
        {
            get { return this._startDate; }
            set { Set(() => StartDate, ref this._startDate, value); }
        }

        public DateTime? EndDate
        {
            get { return this._endDate; }
            set { Set(() => EndDate, ref this._endDate, value); }
        }
        #endregion

        #region Commands
        public RelayCommand GenerateRaportCommand { get; set; }
        public RelayCommand<IClosable> CloseWindowCommand { get; set; }
        #endregion

        #region Constructors
        public GenerateRaportWindowViewModel() : this(new EntryService())
        {
            ButtonAsignment();
            RaportType = RaportType.SelectedTimeRange;
        }

        public GenerateRaportWindowViewModel(IEntryService entryService)
        {
            _entryService = entryService;
        }
        #endregion

        #region Private methods
        private void ButtonAsignment()
        {
            this.GenerateRaportCommand = new RelayCommand(GenerateRaportExecute, CanGenerateRaportExecute);
            this.CloseWindowCommand = new RelayCommand<IClosable>(CloseWindowExecute);
        }

        private void GenerateRaportExecute()
        {
            try
            {
                var document = new Document(PageSize.A4, 15, 15, 50, 50);

                var table = CreateRaportTable(document);

                var fs = new FileStream(string.Format("Raporty/Raport z dnia {0}.pdf", DateTime.Now.ToString("dd.MM.yyyy HH-mm")),
                                     FileMode.Create, FileAccess.Write, FileShare.None);

                var writer = PdfWriter.GetInstance(document, fs);
                
                var title = CreateTitle();

                document.Open();
                document.Add(title);
                document.Add(new Chunk("\n"));
                document.Add(table);
                document.Close();

                CustomMessageBox.CustomMessageBox.Show("Poprawnie wygenerowano raport.");
            }
            catch(EntryException exc)
            {
                CustomMessageBox.CustomMessageBox.Show(exc.Message, "Błąd");
            }
        }

        private bool CanGenerateRaportExecute()
        {
            return
                (StartDate != null && EndDate != null && RaportType == RaportType.SelectedTimeRange) ||
                RaportType == RaportType.CurrentYear;
        }

        private IEnumerable<EntryToRaport> GetEntriesForRaport()
        {

            switch (RaportType)
            {
                case RaportType.CurrentYear:
                    return _entryService.GetEntriesToRaportForCurrentYear();
                case RaportType.SelectedTimeRange:
                    return _entryService.GetEntriesToRaportForTimeRange((DateTime)StartDate, (DateTime)EndDate);
                default:
                    return null;
            }
        }

        PdfPTable CreateRaportTable(Document doc)
        {
            var entries = GetEntriesForRaport();

            var table = new PdfPTable(6);
            table.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;

            var tableHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, BaseFont.CP1250, 8);
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, BaseFont.CP1250, 8);

            table.AddCell(new PdfPCell(new Phrase("Pracownik", tableHeaderFont)));
            table.AddCell(new PdfPCell(new Phrase("Typ", tableHeaderFont)));
            table.AddCell(new PdfPCell(new Phrase("Data rozpoczęcia", tableHeaderFont)));
            table.AddCell(new PdfPCell(new Phrase("Data zakończenia", tableHeaderFont)));
            table.AddCell(new PdfPCell(new Phrase("Zastępca", tableHeaderFont)));
            table.AddCell(new PdfPCell(new Phrase("Zatwierdzono", tableHeaderFont)));

            foreach (var entry in entries)
            {
                table.AddCell(new PdfPCell(new Phrase(entry.UserDisplayName, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(entry.EntryType, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(entry.StartDate, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(entry.EndDate, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(entry.SubstituteUserDisplayName, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(entry.IsApproved, cellFont)));
            }

            return table;
        }

        Paragraph CreateTitle()
        {
            string titleContent;

            switch (RaportType)
            {
                case RaportType.CurrentYear:
                    titleContent = string.Format("Raport urlopów i zwolnień w roku {0}", DateTime.Now.Year);
                    break;
                case RaportType.SelectedTimeRange:
                    var startDate = (DateTime)StartDate;
                    var endDate = (DateTime)EndDate;
                    titleContent = string.Format("Raport urlopów i zwolnień od {0} do {1}",
                                                startDate.ToString("dd.MM.yyyy"), endDate.ToString("dd.MM.yyyy"));
                    break;
                default:
                    titleContent = string.Empty;
                    break;
            }
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA, BaseFont.CP1250, 14);
            var title = new Paragraph(titleContent, titleFont);
            title.Alignment = Element.ALIGN_CENTER;

            return title;
        }

        private void CloseWindowExecute(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        #endregion
    }
}
