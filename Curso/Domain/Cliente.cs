using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Domain{
    [Table("Clientes")]//mesma coisa do Column, caso no banco seja um nome diferente é passado aqui
    public class Cliente{
        //exemplo de data annotation
        [Key]
        public int Id {get; set;}
        [Required]//obrigatorio
        public string? Nome {get; set;}
        [Column("Fone")] //serve pra informar o nome do campo no banco. ex: se no banco minha coluna for phone passa no data annotation
        public string? Telefone{get; set;}
        public string? CEP {get; set;}
        public string? Estado {get; set;}
        public string? Cidade {get; set;}
    }
}