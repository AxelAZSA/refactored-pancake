using System;
using PruebaIngreso.Models;
using PruebaIngreso;
using Quote.Contracts;

namespace Quote
{
    public class MarginProvider : IMarginProvider
    {
        //Metodo que llama al servicio y confirma la respuesta
        public decimal GetMargin(string code)
        {

            MarginService service = new MarginService();
            decimal margin = -1;
            MarginResponse response = service.GetMargin(code);
            if (response != null)
                margin = response.margin;

            return margin;
        }
    }
}