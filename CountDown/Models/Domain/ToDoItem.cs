using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountDown.Models.Domain
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    [Table("todo_items")]
    public class ToDoItem : IValidatableObject
    {
        private DateTime? _start;
        private DateTime? _due;

        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required(ErrorMessage = "You must provide a title.")]
        [StringLength(50, ErrorMessage = "The title must be from 1 to 50 characters in length.")]
        public virtual string Title { get; set; }

        [StringLength(200, ErrorMessage = "The description must be from 0 to 200 characters in length.")]
        public virtual string Description { get; set; }

        [Column("start_date")]
        public virtual DateTime? Start
        {
            get
            {
                if (_start != null)
                {
                    return _start;
                }
                if (StartDate.HasValue && StartTime.HasValue)
                {
                    return StartDate.Value.Date + StartTime.Value.TimeOfDay;
                }
                return null;
            }
            set { _start = value; }
        }

        [Required(ErrorMessage = "You must provide a start date.")]
        [NotMapped]
        public virtual DateTime? StartDate { get; set; } // Used for model binding purposes only.

        [Required(ErrorMessage = "You must provide a start time.")]
        [NotMapped]
        public virtual DateTime? StartTime { get; set; } // Used for model binding purposes only.

        [Column("due_date")]
        public virtual DateTime? Due
        {
            get
            {
                if (_due != null)
                {
                    return _due;
                }
                if (DueDate.HasValue && DueTime.HasValue)
                {
                    return DueDate.Value.Date + DueTime.Value.TimeOfDay;
                }
                return null;
            }
            set { _due = value; }
        }

        [Required(ErrorMessage = "You must provide a due date.")]
        [NotMapped]
        public virtual DateTime? DueDate { get; set; } // Used for model binding purposes only.

        [Required(ErrorMessage = "You must provide a due time.")]
        [NotMapped]
        public virtual DateTime? DueTime { get; set; } // Used for model binding purposes only.

        [NotMapped]
        public virtual TimeSpan? TimeLeft
        {
            get
            {
                if (Due.HasValue)
                {
                    return Due - DateTime.Now;                    
                }
                return null;
            }
        }

        [Column("owner")]
        [ForeignKey("Owner")]
        public virtual long? OwnerId { get; set; }

        public virtual User Owner { get; set; }

        [Required(ErrorMessage = "You must assign the to-do item to someone.")]
        [Column("assigned_to")]
        [ForeignKey("Assignee")]
        public virtual long? AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public virtual bool Completed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Start >= Due)
            {
                yield return
                    new ValidationResult("The due date and time must be after the start date and time.",
                        new[] {"DueDate"});
            }
        }
    }
}