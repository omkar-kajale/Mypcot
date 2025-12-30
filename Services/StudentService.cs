
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Mypcot.Models.Domain;
using Mypcot.Models.Dto;
using Mypcot.Repositories;
using System.Numerics;
using System.Security.Claims;

namespace Mypcot.Services;

public interface IStudentService
{
    Task<(bool, object)> GetAll();
    Task<(bool, object?)> GetById(int id);
    Task<(bool, string)> Add(AddStudentDto request);
    Task<(bool, string)> Update(UpdateStudentDto request);
    Task<(bool, string)> Delete(int id);
}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StudentService(IStudentRepository studentRepository, IHttpContextAccessor httpContextAccessor)
    {
        _studentRepository = studentRepository;
        _httpContextAccessor = httpContextAccessor;
    }    

    public async Task<(bool, object)> GetAll()
    {
        var students = await _studentRepository.GetAll();

        if (students.Count == 0)
            return (false, "No students found");

        var studentResponse = new List<StudentResponse>();

        foreach (var student in students)
        {
            var response = MapStudentToStudentResponse(student);
            studentResponse.Add(response);
        }        

        return (true, studentResponse);
    }

    public async Task<(bool, object?)> GetById(int id)
    {
        var student = await _studentRepository.GetById(id);
        if (student == null)
            return (false, $"No student with Id: {id} found");

        var response = MapStudentToStudentResponse(student);

        return (true, response);
    }

    public async Task<(bool, string)> Add(AddStudentDto request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            CreatedBy = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
        };

        var result = await _studentRepository.Add(student);
        if (!result)
            return (false, "Failed to add student");

        return (true, "Student added successfully");
    }

    public async Task<(bool, string)> Update(UpdateStudentDto request)
    {
        var student = await _studentRepository.GetById(request.Id);
        if (student == null)
            return (false, $"No student with Id: {request.Id} found");

        student.Name = request.Name;
        student.Email = request.Email;
        student.Phone = request.Phone;
        student.ModifiedDate = DateTime.Now;

        var result = await _studentRepository.Update(student);
        if (!result)
            return (false, "Failed to update student");

        return (true, "Student updated successfully");
    }

    public async Task<(bool, string)> Delete(int id)
    {
        var student = await _studentRepository.GetById(id);
        if (student == null)
            return (false, $"No student with Id: {id} found");

        var result = await _studentRepository.Delete(student);
        if (!result)
            return (false, "Failed to delete student");

        return (true, "Student deleted successfully");
    }

    private StudentResponse MapStudentToStudentResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Phone = student.Phone,
            CreatedBy = student.CreatedByUser.Name,
            CreatedDate = student.CreatedDate,
            ModifiedDate = student.ModifiedDate
        };
    }
}
