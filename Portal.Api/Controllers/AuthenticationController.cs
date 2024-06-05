using _20201132039_SinavPortali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using _20201132039_SinavPortali.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace _20201132039_SinavPortali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist!=null) 
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new ResponseMsg { Status = false, Message = "Kullanıcı zaten var." });
            }

            AppUser user = new()
            {
                FullName = registerUser.FullName,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName,
            };
            if (await _roleManager.RoleExistsAsync("Student"))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseMsg { Status = false, Message = "Kullanıcı eklenemedi." });
                }

                await _userManager.AddToRoleAsync(user, "Student");
                return StatusCode(StatusCodes.Status201Created,
                new ResponseMsg { Status = true, Message = "Kullanıcı başarıyla eklendi." });
            }
            else 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseMsg { Status = false, Message = "Belirtilen rol bulunamadı." });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            var user = await _userManager.FindByNameAsync(loginUser.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }


                var jwtToken = GetToken(authClaims);

                /* return Ok(new
                 {
                     token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                     expiration = jwtToken.ValidTo
                 });*/
                return StatusCode(StatusCodes.Status201Created,
                    new ResponseMsg { Status = true, Message = new JwtSecurityTokenHandler().WriteToken(jwtToken)});
                //returning the token...

            }
            return StatusCode(StatusCodes.Status201Created,
                    new ResponseMsg { Status = false, Message = "Kullanıcı adı veya şifre hatalı."});
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
