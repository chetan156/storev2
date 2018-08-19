using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using storev2.Models;

namespace storev2.Controllers
{
    public class ExecutiveController : Controller
    {

        // GET: Executive
        StoreDbContext _db = new StoreDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult Create(ExecutiveInfo oExecutiveInfo)
        {
            ExecutiveInfo _ExecutiveInfo = new ExecutiveInfo()
            {
                ExecutiveName = oExecutiveInfo.ExecutiveName,
                ExecutiveContactNo = oExecutiveInfo.ExecutiveContactNo,
                ExecutiveAddress = oExecutiveInfo.ExecutiveAddress,
                ExecutiveALtNo = oExecutiveInfo.ExecutiveALtNo

            };
            _db.ExecutiveInfos.Add(_ExecutiveInfo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var editlist = _db.ExecutiveInfos.Single(m => m.ExecutiveId == id);
            return View(editlist);
        }
        [HttpPost]
        public ActionResult Edit(int id,ExecutiveInfo oExecutiveInfo)
        {
            var editlist = _db.ExecutiveInfos.Single(m => m.ExecutiveId == id);

            editlist.ExecutiveName = oExecutiveInfo.ExecutiveName;
            editlist.ExecutiveContactNo = oExecutiveInfo.ExecutiveContactNo;
            editlist.ExecutiveAddress = oExecutiveInfo.ExecutiveAddress;
            editlist.ExecutiveALtNo = oExecutiveInfo.ExecutiveALtNo;
            _db.SaveChanges();

            return View();
        }
        public JsonResult Delete(int id)
        {
            var deleteList = _db.ExecutiveInfos.Single(m => m.ExecutiveId == id);
            _db.ExecutiveInfos.Remove(deleteList);
            _db.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }







        public ActionResult ExecutiveList(string sEcho, int iDisplayStart, int iDisplayLength, string executiveaddress)
        {

            string sSearchValue = string.Empty;
            IEnumerable<ExecutiveInfo> _Type = _db.ExecutiveInfos; //add model fro which list is being called 
            sSearchValue = Request["sSearch"];
            var sortdirection = Request["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Executiveorder oMemberOrder = (Executiveorder)sortColumnIndex; // create enum in respective model
            string sSortColumn = oMemberOrder.ToString();
            int totalRecords = 0;
            var _MemberListing = (from member in _db.ExecutiveInfos
                                  where (sSearchValue == "" || member.ExecutiveName.StartsWith(sSearchValue))

                                       && (executiveaddress == "" || member.ExecutiveAddress.StartsWith(executiveaddress))
                                       


                                  select member).OrderBy(c => c.ExecutiveName).AsEnumerable().Select((member, index) => new
                                  {
                                      Index = index + 1,
                                      Id = member.ExecutiveId,
                                      ExecutiveName = member.ExecutiveName,
                                      ExecutiveAddress = member.ExecutiveAddress,
                                      ExecutiveContactNo = member.ExecutiveContactNo,
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
                    member.ExecutiveName,
                    member.ExecutiveAddress,
                    member.ExecutiveContactNo,
                    "<a data-toggle='modal' data-target='#EditExecutiveModal'  href='#' onclick='EditExecutive("+ member.Id.ToString()+ ")'><i class='glyphicon glyphicon-pencil'></i>Edit</a>",
                    "<a href='javascript:DeleteExecutive("+member.Id.ToString()+ ")'><i class='glyphicon glyphicon-trash'></i>Delete</a>"
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