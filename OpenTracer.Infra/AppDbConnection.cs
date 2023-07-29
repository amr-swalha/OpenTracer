using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using OpenTracer.Core.Entities;

namespace OpenTracer.Infra
{
    public class AppDbConnection : DataConnection
    {
        public AppDbConnection(DataOptions<AppDbConnection> options)
       : base(options.Options)
        {
            if (mappingSchema == null)
                mappingSchema = InitContextMappings(this.MappingSchema);
        }
        private static MappingSchema mappingSchema;
        private static MappingSchema InitContextMappings(MappingSchema ms)
        {
            FluentMappingBuilder mappingBuilder = new FluentMappingBuilder();
            mappingBuilder.Entity<Traces>().HasTableName("Traces")
                .HasPrimaryKey(y => y.Id);
            ms = mappingBuilder.Build().MappingSchema;

            return ms;
        }
    }
}
