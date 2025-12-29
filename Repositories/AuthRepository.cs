
using Microsoft.EntityFrameworkCore;
using Mypcot.Data;
using Mypcot.Models.Domain;

namespace Mypcot.Repositories;

public interface IAuthRepository
{
    Task<User?> GetLogin(string login);
}

public class AuthRepository : IAuthRepository
{
    private readonly Context _context;

    public AuthRepository(Context context)
    {
        _context = context;
    }
    public async Task<User?> GetLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower());
    }
}
