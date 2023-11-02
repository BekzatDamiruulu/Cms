namespace Cms.Shared.Shared.Models;


public class ListConfig
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string OrderFieldName { get; set; } 
    public string OrderFieldDirection { get; set; } 
    public string Filter { get; set; }

    public ListConfig(int? pageIndex, int? pageSize, string? orderFieldName, string? orderFieldDirection , string? filter)
    {
        PageIndex = pageIndex ?? 1;
        PageSize = pageSize ?? 20;
        OrderFieldName = orderFieldName ?? "Id";
        OrderFieldDirection = orderFieldDirection ?? "ASC";
        Filter = filter ?? "";
    }
}