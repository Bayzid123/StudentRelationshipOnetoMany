using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRelationshipOnetoMany.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentGradeId { get; set; }

        [JsonIgnore]
        public virtual Grade Grade { get; set; }
    }
}
