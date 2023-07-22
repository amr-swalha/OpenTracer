using LinqToDB.Mapping;

namespace WebApp.Models;

public class Traces
{
    [Identity,PrimaryKey]
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }
    public string Details { get; set; }
}