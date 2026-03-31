namespace WorkLine.DTO;

public class FuncionarioDTO
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public DateTime? DataAdmissao { get; set; }
    public IFormFile? Foto { get; set; }
    public Guid? IdSetor { get; set; }
    public Guid? IdCargo { get; set; }
}