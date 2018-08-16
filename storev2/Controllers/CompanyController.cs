using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using storev2.Models;
using System.Linq.Dynamic;

namespace storev2.Controllers
{
    public class CompanyController : Controller
    {
        StoreDbContext _db = new StoreDbContext();
        // GET: Company
        public ActionResult Index()
        {
            var companylist = _db.CompanyMasters.ToList();
            return View(companylist);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CompanyMaster oCompanyMaster)
        {
            CompanyMaster _CompanyMaster = new CompanyMaster()
            {
                CompanyName= oCompanyMaster.CompanyName
            };
            _db.CompanyMasters.Add(_CompanyMaster);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var editrecord = _db.CompanyMasters.Single(m => m.CompanyId == id);
            return View(editrecord);
        }
        [HttpPost]
        public ActionResult Edit(int id,CompanyMaster oCompanyMaster)
        {
            var editrecord = _db.CompanyMasters.Single(m => m.CompanyId == id);
            editrecord.CompanyName = oCompanyMaster.CompanyName;       
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Delete(int id)
        {
            var deleterecord = _db.CompanyMasters.Single(m => m.CompanyId == id);
            _db.CompanyMasters.Remove(deleterecord);
            _db.SaveChanges();
            return Json("Success",JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanyList(string sEcho, int iDisplayStart, int iDisplayLength)
        {           
            string sSearchValue = string.Empty;
            IEnumerable<CompanyMaster> _Type = _db.CompanyMasters; //add model from which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Company oMemberOrder = (Company)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.CompanyMasters
                                  where (sSearchValue == "" || member.CompanyName.StartsWith(sSearchValue))                                   
                                  select member).OrderBy(c => c.CompanyName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      CompanyId = member.CompanyId,
                                      CompanyName = member.CompanyName,
                                    
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
                    member.CompanyName,                  

                    "<a data-toggle='modal' data-target='#EditCompanyModal'  href='#' onclick='EditCompany("+ member.CompanyId.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteCompany("+member.CompanyId.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
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