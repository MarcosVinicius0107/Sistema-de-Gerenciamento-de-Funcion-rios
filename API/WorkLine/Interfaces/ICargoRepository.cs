using WorkLine.Models;

namespace WorkLine.Interfaces;

public interface ICargoRepository
{
    void Cadastrar(Cargo cargo);
    void Atualizar(Guid id, Cargo cargo);
    void Deletar(Guid id);
    Cargo BuscarPorId(Guid id);
    List<Cargo> Listar();
}
