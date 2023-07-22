using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using WebApp.Models;

namespace WebApp;

public class AppDataAccess : DataConnection
{
    private static MappingSchema mappingSchema;
    public AppDataAccess(DataOptions<AppDataAccess> options)
        : base(options.Options)
    {
        if (mappingSchema == null)
            mappingSchema = InitContextMappings(this.MappingSchema);
    }
    private static MappingSchema InitContextMappings(MappingSchema ms)
    {
        FluentMappingBuilder mappingBuilder = new FluentMappingBuilder();
        mappingBuilder.Entity<Traces>().HasTableName("Traces")
            .HasPrimaryKey(y => y.Id);
        ms = mappingBuilder.Build().MappingSchema;
            
        return ms;
    }
}