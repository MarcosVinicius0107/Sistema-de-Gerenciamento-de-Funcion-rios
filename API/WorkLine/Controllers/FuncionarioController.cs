using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkLine.DTO;
using WorkLine.Interfaces;
using WorkLine.Models;

namespace WorkLine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    public FuncionarioController(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }
    /// <summary>
    /// Endpoint da API que busca um funcionário específico no banco de dados usando seu ID
    /// </summary>
    /// <param name="id">Id do funcionário a ser buscado.</param>
    /// <returns>Status code 200 e os dados do funcionário buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_funcionarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que cadastra um novo funcionário no banco de dados
    /// </summary>
    /// <param name="funcionario">Dados do novo funcionario cadastrado</param>
    /// <returns>Status code 200 e os dados do funcionário cadastrado</returns> 
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromForm] FuncionarioDTO funcionario)
    {
        if (String.IsNullOrWhiteSpace(funcionario.Nome))
            return BadRequest("O nome do funcionário é obrigatório.");
        Funcionario novoFuncionario = new Funcionario();
        if (funcionario.Foto != null && funcionario.Foto.Length > 0)
        {
            var extensao = Path.GetExtension(funcionario.Foto.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await funcionario.Foto.CopyToAsync(stream);
            }
            novoFuncionario.Foto = nomeArquivo;
        }
        novoFuncionario.IdSetor = funcionario.IdSetor;
        novoFuncionario.IdCargo = funcionario.IdCargo;
        novoFuncionario.Nome = funcionario.Nome;
        novoFuncionario.Email = funcionario.Email;
        novoFuncionario.Telefone = funcionario.Telefone;
        novoFuncionario.DataAdmissao = funcionario.DataAdmissao ?? DateTime.Now;

        try
        {
            _funcionarioRepository.Cadastrar(novoFuncionario);
            return Ok(novoFuncionario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que lista todos os funcionários cadastrados no banco de dados
    /// </summary>
    /// <returns>Status code 200 e uma lista com todos os funcionários cadastrados</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_funcionarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que atualiza os dados de um funcionário específico no banco de dados usando seu ID
    /// </summary>
    /// <param name="id">Id do funcionário a ser atualizado</param>
    /// <param name="funcionarioAtualizado">Dados do funcionário atualizado</param>
    /// <returns>Status code 200 e os dados do funcionário atualizado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromForm] FuncionarioDTO funcionarioAtualizado)
    {
        var funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);
        if (funcionarioBuscado == null)
            return NotFound("Funcionário não encontrado.");
        if (funcionarioAtualizado.Foto != null && funcionarioAtualizado.Foto.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!String.IsNullOrWhiteSpace(funcionarioBuscado.Foto))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, funcionarioBuscado.Foto);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }
            var extensao = Path.GetExtension(funcionarioAtualizado.Foto.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await funcionarioAtualizado.Foto.CopyToAsync(stream);
            }
            funcionarioBuscado.Foto = nomeArquivo;
        }

        funcionarioBuscado.IdSetor = funcionarioAtualizado.IdSetor;
        funcionarioBuscado.IdCargo = funcionarioAtualizado.IdCargo;
        funcionarioBuscado.Nome = funcionarioAtualizado.Nome!;
        funcionarioBuscado.Email = funcionarioAtualizado.Email;
        funcionarioBuscado.Telefone = funcionarioAtualizado.Telefone;
        funcionarioBuscado.DataAdmissao = funcionarioAtualizado.DataAdmissao ?? DateTime.Now;
        try
        {
            _funcionarioRepository.Atualizar(id, funcionarioBuscado);
            return Ok(funcionarioAtualizado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que deleta um funcionário específico do banco de dados usando seu ID
    /// </summary>
    /// <param name="id">Id do funcionário a ser deletado</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        var funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);
        if (funcionarioBuscado == null)
            return NotFound("Funcionário não encontrado.");
        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        if (!String.IsNullOrWhiteSpace(funcionarioBuscado.Foto))
        {
            var caminhoAntigo = Path.Combine(caminhoPasta, funcionarioBuscado.Foto);
            if (System.IO.File.Exists(caminhoAntigo))
                System.IO.File.Delete(caminhoAntigo);
        }
        try
        {
            _funcionarioRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
