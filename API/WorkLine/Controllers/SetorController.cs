using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkLine.DTO;
using WorkLine.Interfaces;
using WorkLine.Models;

namespace WorkLine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetorController : ControllerBase
{
    private readonly ISetorRepository _setorRepository;
    public SetorController(ISetorRepository setorRepository)
    {
        _setorRepository = setorRepository;
    }
    /// <summary>
    /// Endpoint para listar todos os setores cadastrados no banco de dados.
    /// </summary>
    /// <returns>Status code 200 com a lista de setores</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_setorRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint para cadastrar um novo setor no banco de dados
    /// </summary>
    /// <param name="setor">Dados do setor cadastrado</param>
    /// <returns>Status code 200 com os dados do setor cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar([FromForm] SetorDTO setor)
    {
        try
        {
            var novoSetor = new Setor
            {
                Nome = setor.Nome!,
                Descricao = setor.Descricao!
            };
            _setorRepository.Cadastrar(novoSetor);
            return Ok(novoSetor);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint para atualizar os dados de um setor já cadastrado no banco de dados.
    /// </summary>
    /// <param name="id">Id do setor a ser atualizado</param>
    /// <param name="setor">Dados do setor atualiazado</param>
    /// <returns>Status code 200 com os dados do setor atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, [FromForm] SetorDTO setor)
    {
        try
        {
            var setorAtualizado = new Setor
            {
                Nome = setor.Nome!,
                Descricao = setor.Descricao!
            };
            _setorRepository.Atualizar(id, setorAtualizado);
            return Ok(setorAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Endpoint para deletar um setor do banco de dados com base no seu ID.
    /// </summary>
    /// <param name="id">Id do setor a ser deletado</param> 
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar([FromForm] Guid id)
    {
        try
        {
            _setorRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API para buscar um setor específico no banco de dados usando seu ID.
    /// </summary>
    /// <param name="id">Id do setor a ser buscado</param>
    /// <returns>Status code 200 com os dados do setor buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId([FromForm] Guid id)
    {
        try
        {            
            return Ok(_setorRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
