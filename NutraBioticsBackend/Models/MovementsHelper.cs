using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutraBioticsBackend.Models
{
    public class MovementsHelper : IDisposable
    {
        public static DataContext db = new DataContext();
        public void Dispose()
        {
            db.Dispose();
        }

        public static Response NewOrder(NewOrderView view, int UserId)
        {

            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var orderHeader = new OrderHeader
                    {
                        OrderNum = 0,
                        SalesOrderHeaderInterId = 0,
                        CustomerId = view.CustomerId,
                        ConNum = view.ConNum,
                        ContactId = view.ContactId,
                        CreditHold = view.CreditHold,
                        CustId = view.CustId,
                        Date = view.Date,
                        NeedByDate = view.NeedByDate,
                        Observations = view.Observations,
                        SalesCategory = view.SalesCategory,
                        Platform = "WEB",
                        ShipToId = view.ShipToId,
                        ShipToNum = view.ShipToNum,
                        TaxAmt = 0,
                        SincronizadoEpicor = false,
                        TermsCode = view.TermsCode,
                        Total = view.Total,
                        UserId = UserId,
                        VendorId = view.VendorId,
                        RowMod = "C"
                    };
                    db.OrderHeaders.Add(orderHeader);
                    db.SaveChanges();
                    int i = 0;
                    var orderDetails = db.OrderDetailTmp.Where(o => o.UserId == UserId).ToList();
                    foreach (var detail in orderDetails)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderLine = i++,
                            OrderQty = detail.OrderQty,
                            OrderNum = orderHeader.OrderNum,
                            PartId = detail.PartId,
                            PartNum = detail.PartNum,
                            PriceListPartId = detail.PriceListPartId,
                            SalesOrderHeaderId = orderHeader.SalesOrderHeaderId,
                            TaxAmt = detail.TaxAmt,
                            UnitPrice = detail.UnitPrice,
                            Reference = detail.Reference,
                            Total = detail.Total
                        };
                        db.OrderDetails.Add(orderDetail);
                        db.OrderDetailTmp.Remove(detail);
                    }
                    db.SaveChanges();
                    Transaction.Commit();
                    return new Response { Succes = true, };

                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return new Response { Succes = false,Message=ex.Message };
                }
            }
        }
    }
}