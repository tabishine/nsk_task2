using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Models;

public class HomeController : Controller
{
    private static List<HierarchicalItem> _data = new List<HierarchicalItem>
    {
        new HierarchicalItem { ID = 1, NAME = "Navigator", PARENTID = null },
        new HierarchicalItem { ID = 2, NAME = "TopBar", PARENTID = null },
        new HierarchicalItem { ID = 3, NAME = "SiteSetting", PARENTID = null },
        new HierarchicalItem { ID = 4, NAME = "Footer", PARENTID = null },
        new HierarchicalItem { ID = 5, NAME = "Child 1.1", PARENTID = 1 },
        new HierarchicalItem { ID = 6, NAME = "Child 1.2", PARENTID = 1 },
        new HierarchicalItem { ID = 7, NAME = "Child 2.1", PARENTID = 2 },
    };

    public IActionResult Index()
    {
        var roots = _data.Where(item => item.PARENTID == null).ToList();
        return View(roots);
    }

    [HttpPost]
    public IActionResult AddItem(HierarchicalItem newItem)
    {
        if (newItem.PARENTID == null)
        {
            newItem.ID = _data.Count + 1;
            _data.Add(newItem);
        }
        else
        {
            var parentItem = _data.FirstOrDefault(item => item.ID == newItem.PARENTID);
            if (parentItem != null)
            {
                newItem.ID = _data.Count + 1;
                parentItem.Children.Add(newItem);
            }
        }

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult ToggleChildItems(int itemId)
    {
        var item = _data.FirstOrDefault(i => i.ID == itemId);
        if (item != null)
        {
            // Переключаем состояние открытия/закрытия дочерних элементов
            item.IsExpanded = !item.IsExpanded;
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }


    [HttpPost]
    public IActionResult GetItemForEdit(int itemId)
    {
        var item = _data.FirstOrDefault(i => i.ID == itemId);
        if (item != null)
        {
            return Json(item);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult EditItem(HierarchicalItem editedItem)
    {
        var existingItem = _data.FirstOrDefault(item => item.ID == editedItem.ID);
        if (existingItem != null)
        {
            // Обновляем значения элемента
            existingItem.NAME = editedItem.NAME;
            existingItem.PARENTID = editedItem.PARENTID;

            return Json(new { success = true }); // Возвращаем JSON-ответ об успешном выполнении операции
        }

        return Json(new { success = false }); // Возвращаем JSON-ответ об ошибке, если элемент не найден
    }





    [HttpPost]
    public IActionResult DeleteChildItem(int parentId, int childId)
    {
        var parentItem = _data.FirstOrDefault(item => item.ID == parentId);
        if (parentItem != null)
        {
            var childToRemove = parentItem.Children.FirstOrDefault(child => child.ID == childId);
            if (childToRemove != null)
            {
                parentItem.Children.Remove(childToRemove);
            }
        }
        return RedirectToAction("Index");
    }
}
