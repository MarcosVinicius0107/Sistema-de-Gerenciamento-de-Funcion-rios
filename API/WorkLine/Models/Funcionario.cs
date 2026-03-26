using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkLine.Models;

[Table("Funcionario")]
[Index("Email", Name = "UQ__Funciona__A9D10534CF31EDD2", IsUnique = true)]
public partial class Funcionario
{
    [Key]
    public Guid IdFuncionario { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? Telefone { get; set; }

    [Column("Data_Admissao", TypeName = "datetime")]
    public DateTime DataAdmissao { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Foto { get; set; }

    public Guid? IdSetor { get; set; }

    public Guid? IdCargo { get; set; }

    [ForeignKey("IdCargo")]
    [InverseProperty("Funcionarios")]
    public virtual Cargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdSetor")]
    [InverseProperty("Funcionarios")]
    public virtual Setor? IdSetorNavigation { get; set; }
}
