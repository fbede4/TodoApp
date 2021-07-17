using System;

namespace TodoApp.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Type of the entity, which was not found.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Id of the Entity, which was not found.
        /// </summary>
        public object Id { get; set; }

        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {
        }

        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }
    }
}
