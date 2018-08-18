using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using storev2.Models;
using System.Linq.Dynamic;

namespace storev2.Controllers
{
    public class AreaController : Controller
    {
        StoreDbContext _db = new StoreDbContext();
        // GET: Area
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AreaMaster oAreaMaster)
        {
            AreaMaster _AreaMaster = new AreaMaster()
            {
                AreaName=oAreaMaster.AreaName
            };
            _db.AreaMasters.Add(_AreaMaster);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var editrecord = _db.AreaMasters.Single(m => m.AreaId == id);
            return View(editrecord);
        }
        [HttpPost]
        public ActionResult Edit(int id,AreaMaster oAreaMaster)
        {
            var editrecord = _db.AreaMasters.Single(m => m.AreaId == id);
            editrecord.AreaName = oAreaMaster.AreaName;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Delete(int id)
        {
            var deleterecord = _db.AreaMasters.Single(m => m.AreaId == id);
            _db.AreaMasters.Remove(deleterecord);
            _db.SaveChanges();
            return Json("Success",JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaList(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            string sSearchValue = string.Empty;
            IEnumerable<AreaMaster> _Type = _db.AreaMasters; //add model from which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Area oMemberOrder = (Area)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.AreaMasters
                                  where (sSearchValue == "" || member.AreaName.StartsWith(sSearchValue))
                                  select member).OrderBy(c => c.AreaName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      AreaId = member.AreaId,
                                      AreaName = member.AreaName,

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
                    member.AreaName,

                    "<a data-toggle='modal' data-target='#EditAreaModal'  href='#' onclick='EditArea("+ member.AreaId.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteArea("+member.AreaId.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
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