using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeDemoApp.Models;
using TreeDataLibrary.BusinessLogic;

namespace TreeDemoApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult AddBranch(int parentId = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(TreeElementModel model, int parentId = 0)
        {
            if (ModelState.IsValid)
            {
                TreeElementLogic.CreateTreeElement(model.Name, parentId);
                return RedirectToAction("TreeList", new { id = model.ParentId });
            }

            return View();
        }

         public ActionResult EditBranch(int id, int parentId)
        {
            return View();
        }

        public ActionResult DeleteBranch(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("TreeList");
            }
            else
            {
                TreeElementLogic.DeleteTreeElement(id);
            }
            return RedirectToAction("TreeList");
        }

        public ActionResult MoveTo(int id, int parentID, int pos)
        {
            if (id == parentID && parentID != 0)
            {
                return RedirectToAction("TreeList", new { id = 0, move = 0 });
            }
            TreeElementLogic.ChangeParent(id, parentID);
            if (pos == -1)
            {
                TreeElementLogic.SetLastPosition(id);
            }
            else
            {
                TreeElementLogic.ChangePosition(id, pos);
            }
            return RedirectToAction("TreeList", new { id = parentID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranch(TreeElementModel model, int id, int parentId)
        {
            if (ModelState.IsValid)
            {
                TreeElementLogic.ChangeName(model.Name, id);
                return RedirectToAction("TreeList", new { id = parentId });
            }

            return View();
        }

        public ActionResult TreeList(int id = 0, int move = 0, int moveId = 0)
        {
            ViewBag.rootId = id;
            ViewBag.move = move;
            ViewBag.moveId = moveId;
            ViewBag.countRows = 0;

            List<TreeElementModel> treeParents = new List<TreeElementModel>();
            var parentData = TreeElementLogic.LoadParents(id);
            if (parentData.Count() != 0)
            {
                foreach (var item in parentData)
                {
                    treeParents.Add(new TreeElementModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ParentId = item.ParentId,
                        Pos = item.Pos
                    });
                }
                treeParents.Reverse();
                ViewBag.treeDirectory = treeParents;
            }
            else if (move == 1 && parentData.Count() == 0 && id != 0)
            {
                return RedirectToAction("TreeList", new { id = 0, move = 0});
            }

            List<TreeElementModel> treeElements = new List<TreeElementModel>();
            var data = TreeElementLogic.LoadTree(id);
            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    treeElements.Add(new TreeElementModel 
                    { 
                        Id = item.Id,
                        Name = item.Name,
                        ParentId = item.ParentId,
                        Pos = item.Pos
                    });
                }
                ViewBag.countRows = data.Count();
            }
            return View(treeElements);
        }



    }
}