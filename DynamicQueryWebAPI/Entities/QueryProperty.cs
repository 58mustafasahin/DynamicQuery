namespace DynamicQueryWebAPI.Entities
{
    public class QueryProperty
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public string? FieldName { get; set; }

        /// <summary>
        /// Variable type
        /// </summary>
        public string? FieldType { get; set; }

        /// <summary>
        /// Variable value
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Comparison operator
        /// </summary>
        public string? ComparisonOperator { get; set; }
    }
}
