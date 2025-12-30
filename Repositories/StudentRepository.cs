using Microsoft.EntityFrameworkCore;
using Mypcot.Data;
using Mypcot.Models.Domain;

namespace Mypcot.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetAll();
    Task<Student?> GetById(int id);
    Task<bool> Add(Student student);
    Task<bool> Update(Student student);
    Task<bool> Delete(Student student);
}
public class StudentRepository : IStudentRepository
{
    private readonly Context _context;
    public StudentRepository(Context context)
    {
        _context = context;
    }    

    public async Task<List<Student>> GetAll()
    {
        return await _context.Students
            .AsNoTracking()
            .Include(s => s.CreatedByUser)
            .ToListAsync();
    }

    public async Task<Student?> GetById(int id)
    {
        return await _context.Students
            .Where(s => s.Id == id)
            .Include(s => s.CreatedByUser)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Add(Student student)
    {
        await _context.Students.AddAsync(student);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Student student)
    {
        _context.Students.Update(student);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Student student)
    {
        _context.Students.Remove(student);
        return await _context.SaveChangesAsync() > 0;
    }
}
