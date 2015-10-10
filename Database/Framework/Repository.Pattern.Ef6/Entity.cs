using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.Pattern.Ef6
{
    public interface IAuditableEntity
    {
        DateTime DateCreate { get; set; }

        string UserCreate { get; set; }

        DateTime DateUpdate { get; set; }

        string UserUpdate { get; set; }
    }

    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        [Display(Name = "Создал")]
        public string UserCreate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата обновления")]
        public DateTime DateUpdate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        [Display(Name = "Обновил")]
        public string UserUpdate { get; set; }
    }

    public abstract class Entity<T> : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T IdRecord { get; set; }
    }
}