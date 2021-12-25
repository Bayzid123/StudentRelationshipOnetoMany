using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRelationshipOnetoMany.DTO;
using StudentRelationshipOnetoMany.Models;

namespace StudentRelationshipOnetoMany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get(int studentId)
        {
            var students = await _context.Students
                .Include(x => x.Grade)
                .Where(c => c.Id == studentId)
                .ToListAsync();
            return Ok(students);
        }
        [HttpPost("Student Table Post")]
        public async Task<ActionResult<List<Student>>> CreateStudent(StudentDto student)
        {
            var newStudent = new Student
            {
                Name = student.Name,
                CurrentGradeId = student.CurrentGradeId,
            };

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return await Get(newStudent.Id);
        }
        [HttpPost("Grade post")]
        public async Task<ActionResult<List<Student>>> CreateGrade(GradeDto grade)
        {
            var newGrade = new Grade
            {
                GradeName = grade.GradeName,
                Section = grade.Section,
            };

            _context.Grades.Add(newGrade);
            await _context.SaveChangesAsync();

            return Ok(newGrade);
        }
    }
}
