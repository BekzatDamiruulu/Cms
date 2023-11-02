namespace Cms.Shared.Shared.Entities;

public abstract class Entity
{
    public long Id { get; set; }
    public string ClassName =>  GetType().Name;
    public DateTime LastUpdate { get; set; }
    public long? CreateUserId { get; set; }
    public long? UpdateUserId { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now.ToUniversalTime();
    public string? ObjectName { get; set; }
    public int ClassId { get=>GetClassId();set{} }
    public int StateId { get; set; } = StateNames.Created;
    protected abstract int GetClassId();
}