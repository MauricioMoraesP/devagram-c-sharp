using DevagramC_.Dtos;
using DevagramC_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevagramC_.Service;
namespace DevagramC_.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto longinrequisicao)
        {

            try
            {
 
                if (!String.IsNullOrEmpty(longinrequisicao.Senha) && !String.IsNullOrEmpty(longinrequisicao.Email) && !String.IsNullOrWhiteSpace(longinrequisicao.Senha) && !String.IsNullOrWhiteSpace(longinrequisicao.Email))
                {
                    string email = "mm@hotmail.com";
                    string senha = "senha@123";

                    if (longinrequisicao.Email ==email && longinrequisicao.Senha == senha)
                    {

                        Usuario usuario = new Usuario()
                        {
                            Email = longinrequisicao.Email,
                            Senha = longinrequisicao.Senha,
                            Id=12,
                            Nome="Mauricio"
                        };





                        return Ok(new LoginRespostaDto()
                        {
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            Token = TokenService.CriarToken(usuario)
                        });

                    }
                    else
                    {
                        return BadRequest(new ErrorResposta()
                        {
                            Descricao = "Email ou senha inválido!",
                            Status = 400
                        });
                    }
                }
                else
                {
                     return StatusCode(400, new ErrorResposta()
                    {
                        Descricao = "Usuário não preencheu os campos de login corratamente!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro no login: " + ex);
                return StatusCode(500, new ErrorResposta()
                {
                    Descricao = "Ocorreu um erro ao fazer o login",
                    Status = 500
                });
            }

        }
    }
}
