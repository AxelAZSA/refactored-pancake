using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PruebaIngreso.Models;
using Quote;
using Quote.Contracts;
using Quote.Models;

namespace PruebaIngreso.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteEngine quote;

        public HomeController(IQuoteEngine quote)
        {
            this.quote = quote;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                TourCode = "E-U10-PRVPARKTRF",
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);
            var tour = result.Tours.FirstOrDefault();
            ViewBag.Message = "Test 1 Correcto";
            return View(tour);
        }

        public ActionResult Test2()
        {
            ViewBag.Message = "Test 2 Correcto";
            return View();
        }

        public ActionResult Test3(decimal? margin)
         {
            //Confirmación de la respuesta
            if(margin!=null)
            ViewBag.Margin = margin.ToString();

            return View();
        }

        //Accion que declara el servicio y recibe la respuesta de la API
        public ActionResult postCode(string code)
        {
            MarginService service = new MarginService();
            decimal margin = 0;
            MarginResponse response = service.GetMargin(code);
            if (response != null)
                margin = response.margin;

            return RedirectToAction($"Test3", new {margin = margin});
        }

        public ActionResult Test4()
        {
            var request = new TourQuoteRequest
            {
                adults = 1,
                ArrivalDate = DateTime.Now.AddDays(1),
                DepartingDate = DateTime.Now.AddDays(2),
                getAllRates = true,
                GetQuotes = true,
                RetrieveOptions = new TourQuoteRequestOptions
                {
                    GetContracts = true,
                    GetCalculatedQuote = true,
                },
                Language = Language.Spanish
            };

            var result = this.quote.Quote(request);

            //Declaración de los servicios que heredan de la interfaz IMarginProvider
            IMarginProvider marginProvider = new MarginProvider();
            IMarginProvider defaultMarginProvider = new DefaultMarginProvider();

            //Obtención del margen
            decimal margin = marginProvider.GetMargin(request.TourCode);

            //Evaluación del margen
            if (margin == -1)
            {
                //En caso de que el margen no se obtenga se obtiene el margen default
                margin = defaultMarginProvider.GetMargin(request.TourCode);
            }

            ViewBag.Margin = margin;

            return View(result.TourQuotes);
        }
    }
}