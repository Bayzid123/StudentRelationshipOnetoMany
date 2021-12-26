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
        [HttpGet("get by id")]
        public async Task<ActionResult<List<Student>>> GetbyID(int id)
        {
            var data = await _context.Students.FindAsync(id);
            if (data == null)
                return BadRequest("id not found");
            return Ok(data);
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

        [HttpPut("update Student")]

        public async Task<ActionResult<List<Student>>> UpdateStudent(StudentUpdateDto student)
        {
            var dbstudent = await _context.Students.FindAsync(student.Id);
            if (dbstudent == null)
                return BadRequest("Student not found");

            dbstudent.Name= student.Name;
            dbstudent.Id= student.Id;
            dbstudent.CurrentGradeId= student.CurrentGradeId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete("delete by id")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var data = await _context.Students.FindAsync(id);
            if (data == null)
                return BadRequest("id not found");
            _context.Students.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
    }
}
