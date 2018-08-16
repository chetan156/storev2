using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using storev2.Models;

using System.Linq.Dynamic;

namespace storev2.Controllers
{
    public class CatagoryController : Controller
    {
        StoreDbContext _db = new StoreDbContext();
        // GET: Catagory
        public ActionResult Index()
        {   
            var list = _db.CatagoryMasters.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.Companylist = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(CatagoryMaster oCatagoryMaster)
        {
            ViewBag.Companylist = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            _db.CatagoryMasters.Add(oCatagoryMaster);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Companylist = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            var editList = _db.CatagoryMasters.Single(m => m.CatagoryId == id);
            return View(editList);   
        }
        [HttpPost]
        public ActionResult Edit(int id, CatagoryMaster oCatagoryMaster)
        {
            ViewBag.Companylist = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            var editList = _db.CatagoryMasters.Single(m => m.CatagoryId == id);
            editList.CompanyId = oCatagoryMaster.CompanyId;
            editList.CatagoryName = oCatagoryMaster.CatagoryName;
            _db.SaveChanges();
            return View();
        }
        public JsonResult Delete(int id)
        {
            var deleteList = _db.CatagoryMasters.Single(m => m.CatagoryId == id);
            _db.CatagoryMasters.Remove(deleteList);
            _db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult CategoryList()
        {
            var list = _db.CatagoryMasters.ToList();
            return View(list);
        }
        public ActionResult CatagoryList2(string sEcho, int iDisplayStart, int iDisplayLength, string catagoryname, string companyname)
        {

            string sSearchValue = string.Empty;
            IEnumerable<CatagoryMaster> _Type = _db.CatagoryMasters; //add model fro which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            CatagoryEnum oMemberOrder = (CatagoryEnum)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.CatagoryMasters
                                  where (sSearchValue == "" || member.CatagoryName.StartsWith(sSearchValue))

                                  && (catagoryname == "" || member.CatagoryName.StartsWith(catagoryname))
                                       && (companyname == "" || member.CompanyMaster.CompanyName.StartsWith(companyname))

                                  select member).OrderBy(c => c.CatagoryName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      Id = member.CatagoryId,
                                      CatagoryName = member.CatagoryName,
                                      CompanyId = member.CompanyMaster.CompanyName,
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
                    member.CatagoryName,
                    member.CompanyId,
                    "<a data-toggle='modal' data-target='#EditCatagoryModal'  href='#' onclick='EditCatagoryList("+ member.Id.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteCatagoryList("+member.Id.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
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