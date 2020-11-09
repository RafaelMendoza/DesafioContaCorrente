using DesafioStone.Domain.Models.Contracts;

namespace DesafioStone.Domain.Models.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
