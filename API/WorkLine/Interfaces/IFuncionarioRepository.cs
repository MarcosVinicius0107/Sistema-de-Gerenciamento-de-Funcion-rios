using WorkLine.Models;

namespace WorkLine.Interfaces;

public interface IFuncionarioRepository
{
        void Cadastrar(Funcionario funcionario);
        void Atualizar(Guid id, Funcionario funcionario);
        void Deletar(Guid id);
        Funcionario BuscarPorId(Guid id);
        List<Funcionario> Listar();
}
