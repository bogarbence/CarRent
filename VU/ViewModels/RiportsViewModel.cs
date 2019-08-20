using Caliburn.Micro;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VU.Objects;


namespace VU.ViewModels
{
    class RiportsViewModel : Screen
    {
        #region
        private DateTime _EndDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
                NotifyOfPropertyChange(() => EndDate);
            }
        }
        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
                NotifyOfPropertyChange(() => StartDate);
            }
        }
        private ObservableCollection<Reservation> _ResList = IoC.Get<ReservationListViewModel>().ResList;
        public ObservableCollection<Reservation> ResList
        {
            get
            {
                return _ResList;
            }
            set
            {
                _ResList = value;
                NotifyOfPropertyChange(() => ResList);
            }
        }
        private ObservableCollection<Car> _CarList = IoC.Get<CarListViewModel>().CarList;
        public ObservableCollection<Car> CarList
        {
            get
            {
                return _CarList;
            }
            set
            {
                _CarList = value;
                NotifyOfPropertyChange(() => CarList);
            }
        }
        private ObservableCollection<Car> _BrokenCarList = IoC.Get<CarListViewModel>().CarList;
        public ObservableCollection<Car> BrokenCarList
        {
            get
            {
                return _BrokenCarList;
            }
            set
            {
                _BrokenCarList = value;
                NotifyOfPropertyChange(() => BrokenCarList);
            }
        }
        #endregion
        public void GenerateBrokenCarList()
        {
            //Kreál egy listát az összes törött autóval
            string pdfcontent = "";
            CarList = IoC.Get<CarListViewModel>().refCarList();
            int counter = 1;
            for (int i = 0; i < CarList.Count; i++)
            {
                if (CarList[i].status == "Broken")
                {
                    pdfcontent += counter.ToString() + ", " + CarList[i].modell + " | " + CarList[i].licenseplate + " | " + CarList[i].status + '\n';
                    counter++;
                }
            }
            GeneratePDF("Broken Car List", pdfcontent);
        }
        public void GenerateWeeklyReservations()
        {
            //Lekéri az ezen a héten végződő foglalásokat
            string pdfcontent = "";
            DateTime startOfWeek = DateTime.Today.AddDays((-1 * (Int32)DateTime.Now.DayOfWeek) + 1);
            DateTime endOfWeek = DateTime.Today.AddDays(7 - (Int32)DateTime.Now.DayOfWeek);
            endOfWeek = endOfWeek.AddHours(23);
            endOfWeek = endOfWeek.AddMinutes(59);
            endOfWeek = endOfWeek.AddSeconds(59);
            ObservableCollection<Car> reservedCars = new ObservableCollection<Car>();
            for (int i = 0; i < ResList.Count; i++)
            {
                bool isContain = false;
                for (int j = 0; j < reservedCars.Count; j++)
                {
                    if (reservedCars[j].licenseplate == ResList[i].choosenCar.licenseplate)
                    {
                        isContain = true;
                    }
                }
                if (!isContain)
                {
                    reservedCars.Add(ResList[i].choosenCar);
                }
            }
            for (int i = 0; i < reservedCars.Count; i++)
            {
                bool wroteCar = false;
                for (int j = 0; j < ResList.Count; j++)
                {
                    if (ResList[j].choosenCar.licenseplate == reservedCars[i].licenseplate)
                    {
                        if (ResList[j].endDate.Ticks > startOfWeek.Ticks && ResList[j].endDate.Ticks < endOfWeek.Ticks)
                        {
                            if (!wroteCar)
                            {
                                pdfcontent += reservedCars[i].modell + " | " + reservedCars[i].licenseplate + '\n';
                                wroteCar = true;
                            }
                            pdfcontent += "   * " + ResList[j].startDate + " | " + ResList[j].endDate + " | " + ResList[j].comment + '\n';
                        }
                    }
                }
            }
            GeneratePDF("Weekly Reservations", pdfcontent);
        }
        public void GenerateMostProfitableCar()
        {
            //Lekéri az adott időintervallumban a legnagyobb profitot termelő autót
            int biggestpricecarID = 0;
            int biggestPrice = 0;
            for (int i = 0; i < CarList.Count; i++)
            {
                int cartotalhours = 0;
                for (int j = 0; j < ResList.Count; j++)
                {
                    if(ResList[j].choosenCar.licenseplate == CarList[i].licenseplate)
                    {
                        Double diff = 0;
                        if(ResList[j].startDate.Ticks > StartDate.Ticks && ResList[j].endDate.Ticks < EndDate.Ticks)
                        {
                            diff = (ResList[j].endDate - ResList[j].startDate).TotalHours;
                            
                        }
                        if (ResList[j].startDate.Ticks > StartDate.Ticks && ResList[j].endDate.Ticks > EndDate.Ticks)
                        {
                            diff = (EndDate - ResList[j].startDate).TotalHours;

                        }
                        if (ResList[j].startDate.Ticks < StartDate.Ticks && ResList[j].endDate.Ticks < EndDate.Ticks)
                        {
                            diff = (ResList[j].endDate - StartDate).TotalHours;

                        }
                        if (ResList[j].startDate.Ticks < StartDate.Ticks && ResList[j].endDate.Ticks > EndDate.Ticks)
                        {
                            diff = (EndDate - StartDate).TotalHours;
                        }
                        diff = Math.Ceiling(diff);
                        cartotalhours += (int)diff;
                    }

                }
                if (biggestPrice < cartotalhours * CarList[i].price)
                {
                    biggestPrice = cartotalhours * CarList[i].price;
                    biggestpricecarID = i;
                }
            }
            string pdfcontent = CarList[biggestpricecarID].modell + " | " + CarList[biggestpricecarID].licenseplate + " | Profit: " + biggestPrice;
            GeneratePDF("Most Profitable Car", pdfcontent);
        }
        public void GeneratePDF(string title, string content)
        {
            //PDF-et generál
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = title;
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 13, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(graph);
            tf.DrawString(content, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            string pdfFilename = "firstpage.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }
    }
}
