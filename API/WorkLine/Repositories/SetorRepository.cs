using WorkLine.BdContextWorkLine;
using WorkLine.Interfaces;
using WorkLine.Models;

namespace WorkLine.Repositories;

public class SetorRepository : ISetorRepository
{
    private readonly WorkLineContext _context;
    public SetorRepository(WorkLineContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Cadastrar um novo setor no banco de dados.
    /// </summary>
    /// <param name="setor">Nome do setor cadastrado</param>
    public void Cadastrar(Setor setor)
    {
            _context.Setors.Add(setor);
            _context.SaveChanges();
    }
    /// <summary>
    /// Atualizar as informações de um setor existente no banco de dados.
    /// </summary>
    /// <param name="id">Id do setor a ser atualizado</param>
    /// <param name="setor">Dados do setor atualizado</param>
    public void Atualizar(Guid id, Setor setor)
    {
        var setorBuscado = _context.Setors.Find(id);
        if (setorBuscado != null)
        {
            setorBuscado.Nome = String.IsNullOrWhiteSpace(setor.Nome) ? setorBuscado.Nome : setor.Nome;
            setorBuscado.Descricao = String.IsNullOrWhiteSpace(setor.Descricao) ? setorBuscado.Descricao : setor.Descricao;
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Deleta um setor do banco de dados com base no seu ID.
    /// </summary>
    /// <param name="id">Id do setor a ser deletado</param>
    public void Deletar(Guid id)
    {
        var setor = _context.Setors.Find(id);
        if (setor != null)
        {
            _context.Setors.Remove(setor);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca um setor específico no banco de dados usando seu ID.
    /// </summary>
    /// <param name="id">Id do setor a ser buscado</param>
    /// <returns>Setor buscado</returns>
    public Setor BuscarPorId(Guid id)
    {
        return _context.Setors.Find(id)!;
    }
    /// <summary>
    /// Lista de todos os setores cadastrados no banco de dados.
    /// </summary>
    /// <returns>Lista com todos os setores</returns>
    public List<Setor> Listar()
    {
        return _context.Setors.ToList();
    }
}
