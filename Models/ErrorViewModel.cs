﻿
namespace task2.Models
{
  public class HierarchicalItem
  {
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public bool IsExpanded { get; set;  }
    public int ID { get; set; }
    public string? NAME { get; set; }
    public int? PARENTID { get; set; }
    public List<HierarchicalItem> Children { get; set; } = new List<HierarchicalItem>(); // Добавьте это свойство

  }
}
