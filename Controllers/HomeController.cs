using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Models;

public class HomeController : Controller
{
    private static List<HierarchicalItem> _data = new List<HierarchicalItem>
    {
        new HierarchicalItem { ID = 1, NAME = "Root 1", PARENTID = null },
        new HierarchicalItem { ID = 2, NAME = "Root 2", PARENTID = null },
        new HierarchicalItem { ID = 3, NAME = "Child 1.1", PARENTID = 1 },
        new HierarchicalItem { ID = 4, NAME = "Child 1.2", PARENTID = 1 },
        new HierarchicalItem { ID = 5, NAME = "Child 2.1", PARENTID = 2 },
    };

    public IActionResult Index()
    {
        var roots = _data.Where(item => item.PARENTID == null).ToList();
        return View(roots);
    }

    [HttpPost]
    public IActionResult AddItem(HierarchicalItem newItem)
    {
        newItem.ID = _data.Count + 1;
        _data.Add(newItem);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditItem(HierarchicalItem editedItem)
    {
        var existingItem = _data.FirstOrDefault(item => item.ID == editedItem.ID);
        if (existingItem != null)
        {
            existingItem.NAME = editedItem.NAME;
            existingItem.PARENTID = editedItem.PARENTID;
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteItem(int itemId)
    {
        var itemToRemove = _data.FirstOrDefault(item => item.ID == itemId);
        if (itemToRemove != null)
        {
            _data.Remove(itemToRemove);
        }
        return RedirectToAction("Index");
    }
}
