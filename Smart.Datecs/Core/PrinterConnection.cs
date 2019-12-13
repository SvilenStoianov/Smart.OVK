using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Datecs
{
    public class PrinterConnection
    {
        public PrinterConnection()
        {
        }

        public string HostUrl { get; set; }

        public int HostPort { get; set; }

        /// <summary>
        /// Код търговски обект
        /// </summary>
        public string SellPointCode { get; set; }

        /// <summary>
        /// Име на търговския обект
        /// </summary>
        public string SellPointName { get; set; }

        /// <summary>
        /// Код работно място
        /// </summary>
        public string SellWorkplaceCode { get; set; }
    }
}
