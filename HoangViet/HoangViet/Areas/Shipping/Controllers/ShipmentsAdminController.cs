using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoangViet.Models.Shipping;
using HoangViet.Models;
using HoangViet.Areas.Admin.Controllers;
using PagedList;
using HoangViet.Areas.Shipping.ViewModels;
using System.Globalization;

namespace HoangViet.Areas.Shipping.Controllers
{
    public class ShipmentsAdminController : BaseAdminController
    {
        private HoangVietDbContext db = new HoangVietDbContext();

        // GET: /Shipping/ShipmentsAdmin/
        public async Task<ActionResult> Index(int? page)
        {
            var shipments = await db.Shipments.Include(s => s.Shipper).OrderByDescending(s => s.Id).ToListAsync();
            var pageNumber = page ?? 1;
            var onePageShipments = shipments.ToPagedList(pageNumber, 10);
            return View(onePageShipments);
        }

        // GET: /Shipping/ShipmentsAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: /Shipping/ShipmentsAdmin/Create
        public async Task<ActionResult> Create(int orderId)
        {
            var order = await db.Orders.FindAsync(orderId);
            if (order == null)           
                return HttpNotFound();
            var model = new ShipmentAdminViewModel
            {                
                OrderId = order.Id,
            };
            foreach (var orderDetail in order.OrderDetails) {
                var viewModel = new ShipmentDetailViewModel
                {
                    ProductName = orderDetail.Product.ProductName,
                    OrderDetailId = orderDetail.Id,
                    OrderDetailQuantity = orderDetail.Quantity,
                    ShippedQuantity = orderDetail.ShipmentDetails.Sum(sd => sd.Quantity),
                    DefaultShipmentDetailQuantity = orderDetail.Quantity - orderDetail.ShipmentDetails.Sum(sd => sd.Quantity),
                    ShipmentDetailQuantity = orderDetail.Quantity -  orderDetail.ShipmentDetails.Sum(sd => sd.Quantity)
                };
                model.ShipmentDetailViewModels.Add(viewModel);
            }
            ViewBag.ShipperId = new SelectList(db.ShippingMethods, "Id", "ShippingMethodName");
            return View(model);
        }

        // POST: /Shipping/ShipmentsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( ShipmentAdminViewModel shipmentViewModel)
        {
            //[Bind(Include = "Id,OrderId, ShipperId,TrackingNumber,TotalWeight,ShippedDate,DeliveryDate,ShippingExclTax,ShippingInclTax")]
            var order = await db.Orders.FindAsync(shipmentViewModel.OrderId);
            if (order == null)
                return HttpNotFound();
            foreach (var shipmentDetailViewModel in shipmentViewModel.ShipmentDetailViewModels)
            {
                var orderDetail = order.OrderDetails.FirstOrDefault(od => od.Id == shipmentDetailViewModel.OrderDetailId);
                if (orderDetail == null)
                    return HttpNotFound();
                shipmentDetailViewModel.ProductName = orderDetail.Product.ProductName;
                shipmentDetailViewModel.ShippedQuantity = orderDetail.ShipmentDetails.Sum(sd => sd.Quantity);
                shipmentDetailViewModel.DefaultShipmentDetailQuantity = orderDetail.Quantity - (orderDetail.Quantity - orderDetail.ShipmentDetails.Sum(sd => sd.Quantity));
                shipmentDetailViewModel.OrderDetailQuantity = orderDetail.Quantity;
            }
            if (ModelState.IsValid)
            {
                var newShipment = new Shipment { 
                OrderId = order.Id,
                ShipperId = shipmentViewModel.ShipperId,
                ShippingExclTax = shipmentViewModel.ShippingExclTax,
                ShippingInclTax = shipmentViewModel.ShippingInclTax,
                TotalWeight = shipmentViewModel.TotalWeight,
                TrackingNumber = shipmentViewModel.TrackingNumber,                
                };
                if (shipmentViewModel.ShippedDate != null)
                    newShipment.ShippedDateUtc = DateTime.ParseExact(shipmentViewModel.ShippedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    newShipment.ShippedDateUtc = null;
                if (shipmentViewModel.DeliveryDate != null)
                    newShipment.DeliveryDateUtc = DateTime.ParseExact(shipmentViewModel.DeliveryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    newShipment.DeliveryDateUtc = null;
                foreach (var shipmentDetailViewModel in shipmentViewModel.ShipmentDetailViewModels)
                {
                    var shipmentDetail = new ShipmentDetail
                    {
                        OrderDetailId = shipmentDetailViewModel.OrderDetailId,
                        Quantity = shipmentDetailViewModel.ShipmentDetailQuantity
                    };
                    newShipment.ShipmentDetails.Add(shipmentDetail);
                }

                db.Shipments.Add(newShipment);
                order.UpdateShippingStatus();
                order.UpdateOrderStatus();
                order.CalOrderAmount();
                await db.SaveChangesAsync();
                return RedirectToAction("edit", new { area = "orders", controller = "order", id = shipmentViewModel.OrderId });
            }
            ViewBag.ShipperId = new SelectList(db.ShippingMethods, "Id", "ShippingMethodName", shipmentViewModel.ShipperId);
            return View(shipmentViewModel);
        }

        // GET: /Shipping/ShipmentsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id, bool fromIndex = false)
        {
            ViewBag.FromIndex = fromIndex;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            var model = new ShipmentAdminViewModel {
            Id = shipment.Id,
            OrderId = shipment.OrderId,
            ShipperId = shipment.ShipperId,
            TotalWeight = shipment.TotalWeight,
            TrackingNumber = shipment.TrackingNumber,
            ShippingExclTax = shipment.ShippingExclTax,
            ShippingInclTax = shipment.ShippingInclTax,
            
            };
            if (shipment.ShippedDateUtc.HasValue)
                model.ShippedDate = shipment.ShippedDateUtc.Value.ToString("dd/MM/yyyy");
            if (shipment.DeliveryDateUtc.HasValue)
                model.DeliveryDate = shipment.DeliveryDateUtc.Value.ToString("dd/MM/yyyy");
            foreach (var shipmentDetail in shipment.ShipmentDetails)
            {
                var viewModel = new ShipmentDetailViewModel
                {
                    ShipmentDetailId = shipmentDetail.Id,
                    ProductName = shipmentDetail.OrderDetail.Product.ProductName,
                    OrderDetailId = shipmentDetail.OrderDetailId,
                    OrderDetailQuantity = shipmentDetail.OrderDetail.Quantity,
                    ShippedQuantity = shipmentDetail.OrderDetail.ShipmentDetails.Sum(sd => sd.Quantity)- shipmentDetail.Quantity,
                    ShipmentDetailQuantity = shipmentDetail.Quantity,
                    DefaultShipmentDetailQuantity = shipmentDetail.OrderDetail.Quantity - (shipmentDetail.OrderDetail.ShipmentDetails.Sum(sd => sd.Quantity) - shipmentDetail.Quantity)
                };
                model.ShipmentDetailViewModels.Add(viewModel);
            }
            ViewBag.ShipperId = new SelectList(db.ShippingMethods, "Id", "ShippingMethodName", shipment.ShipperId);
            return View(model);
        }

        // POST: /Shipping/ShipmentsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShipmentAdminViewModel shipmentViewModel, bool fromIndex = false)
        {
            var shipment = await db.Shipments.FindAsync(shipmentViewModel.Id);
            if (shipment == null)
                return HttpNotFound();
            var order = await db.Orders.FindAsync(shipmentViewModel.OrderId);
            if (order == null)
                return HttpNotFound();
            ViewBag.FromIndex = fromIndex;
            foreach (var shipmentDetailViewModel in shipmentViewModel.ShipmentDetailViewModels)
            {
                var orderDetail = order.OrderDetails.FirstOrDefault(od => od.Id == shipmentDetailViewModel.OrderDetailId);
                     if (orderDetail == null)
                    return HttpNotFound();
                     var shipmentDetail = shipment.ShipmentDetails.FirstOrDefault(sd => sd.Id == shipmentDetailViewModel.ShipmentDetailId);
                  if (shipmentDetail == null)
                    return HttpNotFound();
                shipmentDetailViewModel.ProductName = orderDetail.Product.ProductName;
                shipmentDetailViewModel.ShippedQuantity = orderDetail.ShipmentDetails.Sum(sd => sd.Quantity) - shipmentDetail.Quantity;
                shipmentDetailViewModel.DefaultShipmentDetailQuantity = orderDetail.Quantity - (orderDetail.ShipmentDetails.Sum(sd => sd.Quantity) - shipmentDetail.Quantity);
                shipmentDetailViewModel.OrderDetailQuantity = orderDetail.Quantity;
            }
            if (ModelState.IsValid)
            {
                shipment.ShipperId = shipmentViewModel.ShipperId;
                if (shipmentViewModel.ShippedDate != null)
                    shipment.ShippedDateUtc = DateTime.ParseExact(shipmentViewModel.ShippedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    shipment.ShippedDateUtc = null;
                shipment.TotalWeight = shipmentViewModel.TotalWeight;
                shipment.TrackingNumber = shipmentViewModel.TrackingNumber;
                if (shipmentViewModel.DeliveryDate != null)
                shipment.DeliveryDateUtc = DateTime.ParseExact(shipmentViewModel.DeliveryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    shipment.DeliveryDateUtc = null;
                shipment.ShippingExclTax = shipmentViewModel.ShippingExclTax;
                shipment.ShippingInclTax = shipmentViewModel.ShippingInclTax;
                foreach(var shipmentDetail in shipment.ShipmentDetails){
                    var shipmentDetailViewModel = shipmentViewModel.ShipmentDetailViewModels.SingleOrDefault(sdvm => sdvm.ShipmentDetailId == shipmentDetail.Id);
                    shipmentDetail.Quantity = shipmentDetailViewModel.ShipmentDetailQuantity;
                };
                order.UpdateShippingStatus();
                order.UpdateOrderStatus();
                order.CalOrderAmount();
                await db.SaveChangesAsync();
                if (fromIndex)
                     return RedirectToAction("index");
                return RedirectToAction("edit", new { area = "orders", controller = "order", id = shipmentViewModel.OrderId });
                   
            }
            ViewBag.ShipperId = new SelectList(db.ShippingMethods, "Id", "ShippingMethodName", shipmentViewModel.ShipperId);
            return View(shipmentViewModel);
        }

        // GET: /Shipping/ShipmentsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = await db.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: /Shipping/ShipmentsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, bool fromIndex = false)
        {
            Shipment shipment = await db.Shipments.FindAsync(id);
            var order = shipment.Order;
            db.Shipments.Remove(shipment);
            order.UpdateShippingStatus();
            order.UpdateOrderStatus();
            order.CalOrderAmount();
            await db.SaveChangesAsync();
            if(fromIndex)               
            return RedirectToAction("index");
            return RedirectToAction("edit", new { area = "orders", controller = "order", id = order.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
