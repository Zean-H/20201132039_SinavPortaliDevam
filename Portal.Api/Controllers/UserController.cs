using _20201132039_SinavPortali.Dtos;
using _20201132039_SinavPortali.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _20201132039_SinavPortali.Controllers
{
    [Authorize]
    [Route("api/User/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly AppDbContext _context;
        Response _resultDto = new Response();
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public UserController( AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public UserDto GetUserById(string id)
        {
            var user = _userManager.Users.Where(s => s.Id == id).SingleOrDefault();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        public List<AppUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("ChangeRole/{id}")]
        public async Task<Response> ChangeUserRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Kullanıcı Bulunamadı!";
                return _resultDto;
            }
            if (!await _roleManager.RoleExistsAsync(role))
            {
                _resultDto.Status = false;
                _resultDto.Message = "Rol Bulunamadı!";
                return _resultDto;
            }
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, role);
            _resultDto.Status = true;
            _resultDto.Message = "Rol Değiştirildi!";
            return _resultDto;
        }

        [HttpPut]
        public async Task<Response> Update(UserDto dto) 
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if(user == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Kullanıcı Bulunamadı!";
                return _resultDto;
            }
            user.UserName = dto.UserName;
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            await _userManager.UpdateAsync(user);
            _resultDto.Status = true;
            _resultDto.Message = "Kullanıcı bilgileri değiştirildi veya eklendi!";
            return _resultDto;
        }

    }
}
