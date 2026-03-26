using WorkLine.BdContextWorkLine;
using WorkLine.Interfaces;
using WorkLine.Models;

namespace WorkLine.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly WorkLineContext _context;
    public CargoRepository(WorkLineContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Método que atualiza um cargo existente.
    /// </summary>
    /// <param name="id">Id do cargo a ser atualizado</param> 
    /// <param name="cargo">Dados do cargo atualizado</param>
    public void Atualizar(Guid id, Cargo cargo)
    {
        var cargoBuscado = _context.Cargos.Find(id);
        if(cargoBuscado != null)
        {
            cargoBuscado.Titulo = String.IsNullOrEmpty(cargo.Titulo) ? cargoBuscado.Titulo : cargo.Titulo;
            cargoBuscado.Descricao = String.IsNullOrEmpty(cargo.Descricao) ? cargoBuscado.Descricao : cargo.Descricao;
            _context.Cargos.Update(cargoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Método que busca um cargo por seu Id.
    /// </summary>
    /// <param name="id">Id do cargo a ser buscado</param> 
    /// <returns>Daados do cargo buscado</returns>
    public Cargo BuscarPorId(Guid id)
    {
        return _context.Cargos.Find(id)!;
    }
    /// <summary>
    /// Método que cadastra um novo cargo no banco de dados.
    /// </summary>
    /// <param name="cargo">Dados do cargo a ser cadastrado</param>
    public void Cadastrar(Cargo cargo)
    {
        _context.Cargos.Add(cargo);
        _context.SaveChanges();
    }
    /// <summary>
    /// Método que deleta um cargo do banco de dados.
    /// </summary>
    /// <param name="id">Id do cargo a ser deletado</param>
    public void Deletar(Guid id)
    {
      var cargoBuscado = _context.Cargos.Find(id);
        if(cargoBuscado != null)
        {
            _context.Cargos.Remove(cargoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Método que lista todos os cargos cadastrados no banco de dados.
    /// </summary>
    /// <returns>Uma lista com todos os cargos cadastrados no banco de dados</returns>
    public List<Cargo> Listar()
    {
        return _context.Cargos.ToList();
    }
}
