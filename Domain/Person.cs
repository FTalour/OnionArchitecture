namespace Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int Age { get; set; }

        public void UpdateAge()
        {
            Age++;
        }

        public override string ToString()
        {
            string fullNameWithTitle;
            if (!string.IsNullOrWhiteSpace(Title))
            {
                fullNameWithTitle = $"{Title} {FirstName} {LastName}";
            }
            else
            {
                fullNameWithTitle = $"{FirstName} {LastName}";
            }
            return $"{Id} : {fullNameWithTitle} ({ModifiedDate})";
        }
    }
}
