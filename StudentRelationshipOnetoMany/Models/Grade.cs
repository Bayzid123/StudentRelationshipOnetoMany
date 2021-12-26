using System.Text.Json.Serialization;

namespace StudentRelationshipOnetoMany.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        [JsonIgnore]
        public ICollection<Student> Students { get; set; }
    }
}
