using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using storev2.Models;

using System.Linq.Dynamic;
namespace storev2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        StoreDbContext _db = new StoreDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.AreaList = new SelectList(_db.AreaMasters.ToList(), "AreaId", "AreaName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerMaster oCustomerMaster)
        {
            ViewBag.AreaList = new SelectList(_db.AreaMasters.ToList(), "AreaId", "AreaName");
            CustomerMaster _CustomerMaster = new CustomerMaster()
            {
                AreaId = oCustomerMaster.AreaId,
                CustomerAddress = oCustomerMaster.CustomerAddress,
                CustomerAltMobile = oCustomerMaster.CustomerAltMobile,
                CustomerAnnDate = oCustomerMaster.CustomerAnnDate,
                CustomerDob = oCustomerMaster.CustomerDob,
                CustomerMobile = oCustomerMaster.CustomerMobile,
                CustomerName = oCustomerMaster.CustomerName,
                CustomerNo = oCustomerMaster.CustomerNo,
                CustomerOccupation = oCustomerMaster.CustomerOccupation,
                GurantorContatNo = oCustomerMaster.GurantorContatNo,
                GurantorName = oCustomerMaster.GurantorName,
                GurantorNo = oCustomerMaster.GurantorNo,
                OfficeNo = oCustomerMaster.OfficeNo,
                Pin = oCustomerMaster.Pin,
                Refrence = oCustomerMaster.Refrence
              
            };
            _db.CustomerMaster.Add(_CustomerMaster);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            ViewBag.AreaList = new SelectList(_db.AreaMasters.ToList(), "AreaId", "AreaName");
            var editList = _db.CustomerMaster.Single(m => m.CustomerId == id);
            return View(editList);
        }
        [HttpPost]
        public ActionResult Edit(int id, CustomerMaster oCustomerMaster)
        {
            ViewBag.AreaList = new SelectList(_db.AreaMasters.ToList(), "AreaId", "AreaName");
            var editList = _db.CustomerMaster.Single(m => m.CustomerId == id);

            editList.CustomerAddress = oCustomerMaster.CustomerAddress;
            editList.CustomerAltMobile = oCustomerMaster.CustomerAltMobile;
            editList.CustomerAnnDate = oCustomerMaster.CustomerAnnDate;
            editList.CustomerDob = oCustomerMaster.CustomerDob;
            editList.CustomerMobile = oCustomerMaster.CustomerMobile;
            editList.CustomerName = oCustomerMaster.CustomerName;
            editList.CustomerNo = oCustomerMaster.CustomerNo;
            editList.CustomerOccupation = oCustomerMaster.CustomerOccupation;
            editList.GurantorContatNo = oCustomerMaster.GurantorContatNo;
            editList.GurantorName = oCustomerMaster.GurantorName;
            editList.GurantorNo = oCustomerMaster.GurantorNo;
            editList.OfficeNo = oCustomerMaster.OfficeNo;
            editList.Pin = oCustomerMaster.Pin;
            editList.Refrence = oCustomerMaster.Refrence;
            editList.AreaId = oCustomerMaster.AreaId;
           
            _db.SaveChanges();
            return View();
        }
        public JsonResult Delete(int id)
        {
            var deleteList = _db.CustomerMaster.Single(m => m.CustomerId == id);
            _db.CustomerMaster.Remove(deleteList);
            _db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerList(string sEcho, int iDisplayStart, int iDisplayLength, string customername, string areaname,string gurantorname)
        {

            string sSearchValue = string.Empty;
            IEnumerable<CustomerMaster> _Type = _db.CustomerMaster; //add model fro which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            CustomerEnum oMemberOrder = (CustomerEnum)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.CustomerMaster
                                  where (sSearchValue == "" || member.CustomerName.StartsWith(sSearchValue))

                                  //&& (customername == "" || member.CustomerName.StartsWith(customername))
                                       && (areaname == "" || member.AreaMaster.AreaName.StartsWith(areaname))
                                        && (gurantorname == "" || member.GurantorName.StartsWith(gurantorname))

                                  select member).OrderBy(c => c.CustomerName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      Id = member.CustomerId,
                                      CustomerName = member.CustomerName,
                                      AreaId = member.AreaMaster.AreaName,
                                      GurantorName = member.GurantorName
                                  }).ToList();

            if (sSearchValue != null && sSearchValue != "")
            {
                totalRecords = _MemberListing.Count();
            }
            else
            {
                totalRecords = _MemberListing.Count();
            }
            var _MemberQuery = _MemberListing.AsEnumerable().OrderBy(sSortColumn + " " + sortdirection).Select(member => new[]
                 {
                     member.Index.ToString(),
                    member.CustomerName,
                    member.AreaId,
                    member.GurantorName,
                    "<a data-toggle='modal' data-target='#EditCustomerModal'  href='#' onclick='EditCustomerList("+ member.Id.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteCustomerList("+member.Id.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
            }).Skip(iDisplayStart).Take(iDisplayLength);

            var _Json_Memberquery = new
            {
                sEcho = sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = _MemberQuery
            };
            return Json(_Json_Memberquery, JsonRequestBehavior.AllowGet);
        }

    }
}