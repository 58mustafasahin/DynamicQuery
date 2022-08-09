namespace DynamicQueryWebAPI.Entities
{
    public class Person
    {
        /// <summary>
        ///     Person id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Person name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Person surname
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        ///     Person child count
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        ///     Person salary
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        ///     Person birthdate
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
