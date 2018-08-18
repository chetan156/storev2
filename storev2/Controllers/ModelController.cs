using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using storev2.Models;
using System.Linq.Dynamic;

namespace storev2.Controllers
{
    public class ModelController : Controller
    {
        StoreDbContext _db = new StoreDbContext();
        // GET: Model
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategory(int id)
        {
            var resultData = _db.CatagoryMasters.Where(m => m.CompanyId == id).Select(c => new { Value = c.CatagoryId, Text = c.CatagoryName }).ToList();
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewBag.CompanyList = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ModelMaster oModelMaster)
        {
            ViewBag.CompanyList = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            ModelMaster _ModelMaster = new ModelMaster()
            {
                ModelName=oModelMaster.ModelName,
                CompanyId=oModelMaster.CompanyId,
                CatagoryId=oModelMaster.CatagoryId
            };
            _db.ModelMasters.Add(_ModelMaster);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CompanyList = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            ViewBag.CategoryList = new SelectList(_db.CatagoryMasters.ToList(), "CatagoryId", "CatagoryName");
            var editrecord = _db.ModelMasters.Single(m => m.ModelId == id);
            return View(editrecord);
        }
        [HttpPost]
        public ActionResult Edit(int id,ModelMaster oModelMaster)
        {
            ViewBag.CompanyList = new SelectList(_db.CompanyMasters.ToList(), "CompanyId", "CompanyName");
            ViewBag.CategoryList = new SelectList(_db.CatagoryMasters.ToList(), "CatagoryId", "CatagoryName");
            var editrecord = _db.ModelMasters.Single(m => m.ModelId == id);
            editrecord.ModelName = oModelMaster.ModelName;
            editrecord.CompanyId = oModelMaster.CompanyId;
            editrecord.CatagoryId = oModelMaster.CatagoryId;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult Delete(int id)
        {
            var deleterecord = _db.ModelMasters.Single(m => m.ModelId == id);
            _db.ModelMasters.Remove(deleterecord);
            _db.SaveChanges();
            return Json("Success",JsonRequestBehavior.AllowGet);
        }
        public ActionResult ModelList(string sEcho, int iDisplayStart, int iDisplayLength,string company,string category)
        {
            string sSearchValue = string.Empty;
            IEnumerable<ModelMaster> _Type = _db.ModelMasters; //add model from which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Model oMemberOrder = (Model)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.ModelMasters
                                  where (sSearchValue == "" || member.ModelName.StartsWith(sSearchValue))
                                    && (company == "" || member.CompanyMaster.CompanyName.StartsWith(company))
                                      && (category == "" || member.CatagoryMaster.CatagoryName.StartsWith(category))
                                  select member).OrderBy(c => c.ModelName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      ModelId = member.ModelId,
                                      ModelName = member.ModelName,
                                      CompanyName = member.CompanyMaster.CompanyName,
                                      CatagoryName = member.CatagoryMaster.CatagoryName,

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
                    member.ModelName,
                    member.CompanyName,
                    member.CatagoryName,
                    "<a data-toggle='modal' data-target='#EditModelModal'  href='#' onclick='EditModel("+ member.ModelId.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteModel("+member.ModelId.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
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