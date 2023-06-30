using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using STGenetics.Models;

namespace STGenetics.Controllers
{
    [Authorize]
    public class OrderDetailsController : ApiController
    {
        private STGeneticsEntities db = new STGeneticsEntities();     


        // POST: api/OrderDetails
        [ResponseType(typeof(ReqOrderDetail))]
        public async Task<IHttpActionResult> PostOrderDetail(ReqOrderDetail reqOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            OrderPurchase ord = new OrderPurchase();
            //if it is the first cart order is 0 else it is the number of the order table
            if (reqOrderDetail.OrderPurchaseId == 0)
            {                
                ord.Date = DateTime.Now;
                ord.Discount_ = 0;
                ord.Totalprice = 0;
                ord.Totalquantity = 0;
                db.OrderPurchase.Add(ord);
                db.SaveChanges();
            }
            else {
                ord = db.OrderPurchase.Find(reqOrderDetail.OrderPurchaseId);            
            }


            if (isDuplicate(ord.OrderPurchaseId, reqOrderDetail.Animalid)) {                
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Error. Duplicate Entry ID = {0}", reqOrderDetail.Animalid)),
                    ReasonPhrase = "Error. Duplicate Entry"
                };
                throw new HttpResponseException(resp);
            }

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.Animalid = reqOrderDetail.Animalid;
            orderDetail.Orderpurchaseid = ord.OrderPurchaseId;
            orderDetail.Quantity = reqOrderDetail.Quantity;
            orderDetail.Discount = 0;    
            //	If the customer adds an animal with a quantity greater than 50 in the cart, we must apply a 5% discount on the value of this animal. 
            if (reqOrderDetail.Quantity > 50) {
                orderDetail.Discount = 5;
            }
            db.OrderDetail.Add(orderDetail);
            await db.SaveChangesAsync();

            //update order
            ord.Totalquantity = int.Parse(totalOrderQuantity(ord.OrderPurchaseId).ToString());
            ord.Totalprice = Double.Parse(totalOrderPrice(ord.OrderPurchaseId).ToString());

            //If the customer buys more than 300 animals in the order, the freight value must be free, otherwise it will charge $1,000.00 for freight
            if (ord.Totalquantity > 300)
            {
                ord.Freightcharge = 0;
            }
            else {
                //▪	If the customer buys more than 200 animals in the order, an additional 3% discount will be added to the total purchase price
                if (ord.Totalquantity > 200) {
                    ord.Discount_ = 3;
                    ord.Totalprice = ord.Totalprice - (ord.Totalprice * 0.03);
                }

                //otherwise it will charge $1,000.00 for freight
                ord.Freightcharge = 1000;
            }
            await db.SaveChangesAsync();
            //return Ok(ord);

            RespOrder respOrder = new RespOrder();
            respOrder.OrderPurchaseId = ord.OrderPurchaseId;
            respOrder.Totalquantity = int.Parse(ord.Totalquantity.ToString());
            respOrder.Totalprice = double.Parse(ord.Totalprice.ToString());
            respOrder.Discount = int.Parse(ord.Discount_.ToString());
            respOrder.Freightcharge = double.Parse(ord.Freightcharge.ToString());

            return CreatedAtRoute("DefaultApi", new { id = ord.OrderPurchaseId }, respOrder);
        }

        public bool isDuplicate(int orderId, int idAnimal)
        {
            List<OrderDetail> listDetails = db.OrderDetail.Where(x => x.Orderpurchaseid == orderId  && x.Animalid == idAnimal).ToList();
            if (listDetails == null || listDetails.Count() == 0){
                return false;
            }
            else { 
                return true; 
            }
        }

        public double totalOrderPrice(int orderId) { 
            var listDetails = db.OrderDetail.Where(x => x.Orderpurchaseid == orderId);
            double total = 0;
            foreach (OrderDetail anOrder in listDetails) {
                double subtotal = 0;
                Animal animal = db.Animal.Find(anOrder.Animalid);
                if(anOrder.Discount != null)
                    subtotal = animal.Price + ((animal.Price * int.Parse(anOrder.Discount.ToString())) / 100);            
                else
                    subtotal = animal.Price;

                total += subtotal * anOrder.Quantity;
            }
            return total;        
        }

        public int totalOrderQuantity(int orderId)
        {
            List<OrderDetail> listDetails = db.OrderDetail.Where(x => x.Orderpurchaseid == orderId).ToList();

            int total = 0;
            foreach (OrderDetail anOrder in listDetails)
            {
                total += anOrder.Quantity;
            }
            return total;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetail.Count(e => e.OrderDetailId == id) > 0;
        }
    }
}