using WorkLine.BdContextWorkLine;
using WorkLine.Interfaces;
using WorkLine.Models;

namespace WorkLine.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly WorkLineContext _context;
    public FuncionarioRepository(WorkLineContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Método para atualizar um funcionário existente.
    /// </summary>
    /// <param name="id">Id do funcionário a ser atualizado.</param> 
    /// <param name="funcionario">Dados do funcionário a ser atualizado.</param> 

    public void Atualizar(Guid id, Funcionario funcionario)
    {
       var funcionarioBuscado = _context.Funcionarios.Find(id);
        if (funcionarioBuscado != null)
        {
            funcionarioBuscado.Nome = String.IsNullOrWhiteSpace(funcionario.Nome) ? funcionarioBuscado.Nome : funcionario.Nome;
            funcionarioBuscado.Email = String.IsNullOrWhiteSpace(funcionario.Email) ? funcionarioBuscado.Email : funcionario.Email;
            funcionarioBuscado.Telefone = String.IsNullOrWhiteSpace(funcionario.Telefone) ? funcionarioBuscado.Telefone : funcionario.Telefone;
            funcionarioBuscado.DataAdmissao = funcionario.DataAdmissao;
            funcionarioBuscado.Foto = funcionario.Foto;
            funcionarioBuscado.IdSetor = funcionario.IdSetor;
            funcionarioBuscado.IdCargo = funcionario.IdCargo;
            _context.Funcionarios.Update(funcionarioBuscado);
            _context.SaveChanges();
        }

    }
    /// <summary>
    /// Método para buscar um funcionário por seu Id.
    /// </summary>
    /// <param name="id">Id do funcionário a ser buscado.</param>
    /// <returns>Retorna o funcionário encontrado.</returns>
    public Funcionario BuscarPorId(Guid id)
    {

        return _context.Funcionarios.Find(id)!;
    }
    /// <summary>
    /// Método para cadastrar um novo funcionário.
    /// </summary>
    /// <param name="funcionario">Dados do funcionário cadastrado.</param>
    public void Cadastrar(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        _context.SaveChanges();
    }
    /// <summary>
    /// Método para deletar um funcionário existente.
    /// </summary>
    /// <param name="id">Id do funcionário a ser deletado.</param>
    public void Deletar(Guid id)
    {
        var funcionarioBuscado = _context.Funcionarios.Find(id);
        if (funcionarioBuscado != null)
        {
            _context.Funcionarios.Remove(funcionarioBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Método para listar todos os funcionários cadastrados.
    /// </summary>
    /// <returns>Uma lista com todos os funcionários cadastrados</returns>
    public List<Funcionario> Listar()
    {
        return _context.Funcionarios.ToList();
    }
}
 