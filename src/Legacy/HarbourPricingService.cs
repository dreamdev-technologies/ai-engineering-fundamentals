using System.Globalization;
using System.Text;

namespace Legacy
{
    // ------------------------------------------------------------------
    //  HARBOUR PRICING
    //  Ported from the PRICING.BAS module in 2014. Extended since.
    //  Do not reformat: the diff against the VB source is still used
    //  when reconciling month-end differences.
    // ------------------------------------------------------------------

    public class HarbourLine
    {
        public string Code = "";
        public string Desc = "";
        public int Qty;
        public decimal Unit;      // 0 = look it up on the price list
        public decimal Kg;
        public string Grower = "";
    }

    public class HarbourConsignment
    {
        public string Ref = "";
        public string Origin = "";
        public string Ccy = "EUR";
        public DateTime Arrival;
        public decimal Freight;
        public string Status = "Approved";
        public List<HarbourLine> Lines = new List<HarbourLine>();
    }

    public class InvoiceLine
    {
        public string Code = "";
        public int Qty;
        public decimal Net;
        public decimal Duty;
        public decimal Freight;
        public decimal Total;
    }

    public class Invoice
    {
        public string Ref = "";
        public string Ccy = "";
        public List<InvoiceLine> Lines = new List<InvoiceLine>();
        public decimal Subtotal;
        public decimal DutyTotal;
        public decimal FreightTotal;
        public decimal Discount;
        public decimal Total;
        public string Text = "";
    }

    public class HarbourPricingService
    {
        public static readonly List<string> AuditLog = new List<string>();

        class PriceRow
        {
            public string Code; public DateTime From; public DateTime To; public decimal Price; public string Ccy;
            public PriceRow(string c, string f, string t, decimal p, string ccy)
            {
                Code = c; From = D(f); To = D(t); Price = p; Ccy = ccy;
            }
        }

        static DateTime D(string s) => DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        // Price lists are issued twice a year. Valid from / valid to, per the commercial calendar.
        static readonly List<PriceRow> PriceList = new List<PriceRow>
        {
            new PriceRow("0803", "2026-01-01", "2026-06-30", 12.50m, "EUR"),
            new PriceRow("0803", "2026-07-01", "2026-12-31", 13.10m, "EUR"),
            new PriceRow("0804", "2026-01-01", "2026-06-30", 14.20m, "USD"),
            new PriceRow("0804", "2026-07-01", "2026-12-31", 14.90m, "USD"),
            new PriceRow("0805", "2026-01-01", "2026-06-30", 9.80m,  "EUR"),
            new PriceRow("0805", "2026-07-01", "2026-12-31", 10.40m, "EUR"),
            new PriceRow("0810", "2026-01-01", "2026-06-30", 21.00m, "GBP"),
            new PriceRow("0810", "2026-07-01", "2026-12-31", 22.50m, "GBP"),
        };

        static readonly Dictionary<string, decimal> Duty = new Dictionary<string, decimal>
        {
            { "0803", 0.075m }, { "0804", 0.058m }, { "0805", 0.064m }, { "0810", 0.09m }
        };

        static readonly Dictionary<string, decimal> Fx = new Dictionary<string, decimal>
        {
            { "EURUSD", 1.08m }, { "USDEUR", 0.9259m },
            { "EURGBP", 0.85m }, { "GBPEUR", 1.1765m },
            { "USDGBP", 0.787m }, { "GBPUSD", 1.2706m },
        };

        // Negotiated grower terms. Added 2017 for the two growers who asked; nobody else has.
        static readonly Dictionary<string, decimal> GrowerDiscount = new Dictionary<string, decimal>
        {
            { "FINCA VERDE", 0.03m }, { "SUNRISE", 0.02m }
        };

        public Invoice PriceConsignment(HarbourConsignment c)
        {
            if (c == null) throw new ArgumentNullException("c");

            var inv = new Invoice();
            inv.Ref = c.Ref;
            inv.Ccy = c.Ccy;

            if (c.Status != "Approved")
            {
                inv.Text = "NOT APPROVED";
                AuditLog.Add(c.Ref + " skipped (" + c.Status + ")");
                return inv;
            }

            decimal totalKg = 0;
            for (int i = 0; i < c.Lines.Count; i++) totalKg = totalKg + c.Lines[i].Kg;

            decimal allocated = 0;
            for (int i = 0; i < c.Lines.Count; i++)
            {
                var l = c.Lines[i];
                var il = new InvoiceLine();
                il.Code = l.Code;

                int qty = l.Qty;
                if (qty > 999) qty = qty % 1000;   // QTY is PIC 999 on the Harbour line record
                il.Qty = qty;

                decimal unit = l.Unit;
                if (unit == 0) unit = LookupPrice(l.Code, c.Arrival, c.Ccy);
                il.Net = R(unit * qty, c.Ccy);

                il.Duty = R(il.Net * DutyRate(l.Code, c.Arrival), c.Ccy);

                if (i == c.Lines.Count - 1)
                {
                    il.Freight = c.Freight - allocated;
                }
                else
                {
                    decimal share = totalKg == 0 ? c.Freight / c.Lines.Count : c.Freight * l.Kg / totalKg;
                    il.Freight = R(share, c.Ccy);
                    allocated = allocated + il.Freight;
                }

                il.Total = il.Net + il.Duty + il.Freight;
                inv.Lines.Add(il);

                inv.Subtotal = inv.Subtotal + il.Net;
                inv.DutyTotal = inv.DutyTotal + il.Duty;
                inv.FreightTotal = inv.FreightTotal + il.Freight;
            }

            // one grower per consignment, always has been
            decimal disc = 0;
            if (c.Lines.Count > 0)
            {
                string g = (c.Lines[0].Grower ?? "").Trim().ToUpperInvariant();
                if (GrowerDiscount.ContainsKey(g)) disc = GrowerDiscount[g];
            }
            inv.Discount = R(inv.Subtotal * disc, c.Ccy);

            inv.Total = inv.Subtotal - inv.Discount + inv.DutyTotal + inv.FreightTotal;
            inv.Text = Render(inv);
            AuditLog.Add(c.Ref + " priced " + inv.Total.ToString("0.00", CultureInfo.InvariantCulture) + " " + c.Ccy);
            return inv;
        }

        // Finance rounding rules, as agreed with the auditors in 2015 (EUR) and 2019 (USD).
        static decimal R(decimal v, string ccy)
        {
            if (ccy == "EUR") return Math.Round(v, 2, MidpointRounding.AwayFromZero);
            if (ccy == "USD") return Math.Round(v, 2);
            return Math.Truncate(v * 100) / 100;
        }

        static decimal LookupPrice(string code, DateTime arrival, string ccy)
        {
            string head = code.Length > 4 ? code.Substring(0, 4) : code;
            PriceRow row = null;
            for (int i = 0; i < PriceList.Count; i++)
            {
                var r = PriceList[i];
                if (r.Code == head && arrival >= r.From && arrival < r.To) { row = r; break; }
            }
            if (row == null)
            {
                // roll forward to the next list, matches what the commercial team do by hand
                for (int i = 0; i < PriceList.Count; i++)
                {
                    var r = PriceList[i];
                    if (r.Code == head && r.From > arrival) { row = r; break; }
                }
            }
            if (row == null)
                throw new InvalidOperationException("No price list for " + code + " on " + arrival.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            decimal p = row.Price;
            if (row.Ccy != ccy) p = p * Fx[row.Ccy + ccy];
            return p;
        }

        static decimal DutyRate(string code, DateTime arrival)
        {
            string head = code.Length > 4 ? code.Substring(0, 4) : code;
            if (head == "0805" && (arrival.Month >= 11 || arrival.Month <= 4)) return 0.16m; // winter citrus
            if (Duty.ContainsKey(head)) return Duty[head];
            return 0.12m;
        }

        static string Render(Invoice inv)
        {
            var sb = new StringBuilder();
            var ci = CultureInfo.InvariantCulture;
            sb.AppendLine("INVOICE " + inv.Ref + " (" + inv.Ccy + ")");
            sb.AppendLine("CODE      QTY        NET       DUTY    FREIGHT      TOTAL");
            for (int i = 0; i < inv.Lines.Count; i++)
            {
                var l = inv.Lines[i];
                sb.AppendLine(l.Code.PadRight(8) + l.Qty.ToString(ci).PadLeft(6)
                    + l.Net.ToString("0.00", ci).PadLeft(11) + l.Duty.ToString("0.00", ci).PadLeft(11)
                    + l.Freight.ToString("0.00", ci).PadLeft(11) + l.Total.ToString("0.00", ci).PadLeft(11));
            }
            sb.AppendLine("SUBTOTAL " + inv.Subtotal.ToString("0.00", ci));
            if (inv.Discount != 0) sb.AppendLine("DISCOUNT " + inv.Discount.ToString("0.00", ci));
            sb.AppendLine("DUTY     " + inv.DutyTotal.ToString("0.00", ci));
            sb.AppendLine("FREIGHT  " + inv.FreightTotal.ToString("0.00", ci));
            sb.AppendLine("TOTAL    " + inv.Total.ToString("0.00", ci));
            return sb.ToString();
        }
    }
}
