namespace DynamicQueryWebAPI.Entities
{
    public class QueryBuilderDto
    {
        public List<QueryProperty>? QueryProperties { get; set; }
        public string? LogicalOperator { get; set; }
    }
}
