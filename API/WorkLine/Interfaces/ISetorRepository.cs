using WorkLine.Models;

namespace WorkLine.Interfaces;

public interface ISetorRepository
{
    void Cadastrar(Setor setor);
    void Atualizar(Guid id, Setor setor);
    void Deletar(Guid id);
    Setor BuscarPorId(Guid id);
    List<Setor> Listar();
}
