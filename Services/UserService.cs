using Microsoft.AspNetCore.Identity;
using csapi.ErrorExceptions;
using Microsoft.AspNetCore.Http.HttpResults;

public class UserService: IUserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
  {
    User user;

    user = _context.Users.FirstOrDefault(u => u.Username == userDTO.Username);

    if (user != null)
    {
      throw new AlreadyExistsError($"user {userDTO.Username} already exists");
    }

    user.Username = userDTO.Username;
    user.Password = userDTO.Password;

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return new UserDTO()
    {
      Id = user.Id,
      Username = user.Username
    };
  }
}
