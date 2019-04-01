using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;

namespace AspNetMvcApplication.Controllers {

    public class CustomerController : BaseController {

        [ValidateInput(false)]
        public ActionResult CustomerListPartial() {
            var model = UnitOfWork.Query<PersistentTypes.Customer>()
                .Select(t => new Models.Customer() {
                    Oid = t.Oid,
                    FirstName = t.FirstName,
                    LastName = t.LastName
                });
            return PartialView("CustomerListPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddCustomer(Models.Customer model) {
            if(ModelState.IsValid) {
                SafeExecute(() => {
                    var customer = new PersistentTypes.Customer(UnitOfWork) {
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    UnitOfWork.CommitChanges();
                });
            } else {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return CustomerListPartial();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateCustomer(Models.Customer model) {
            if(ModelState.IsValid) {
                SafeExecute(() => {
                    var customer = UnitOfWork.GetObjectByKey<PersistentTypes.Customer>(model.Oid);
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    UnitOfWork.CommitChanges();
                });
            } else {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return CustomerListPartial();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteCustomer(int Oid) {
            SafeExecute(() => {
                var customer = UnitOfWork.GetObjectByKey<PersistentTypes.Customer>(Oid);
                customer.Delete();
                UnitOfWork.CommitChanges();
            });
            return CustomerListPartial();
        }
    }
}