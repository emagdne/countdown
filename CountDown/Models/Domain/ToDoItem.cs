﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountDown.Models.Domain
{
    [Table("todo_items")]
    public class ToDoItem : IValidatableObject
    {
        private DateTime? _start;
        private DateTime? _due;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide a title.")]
        [StringLength(50, ErrorMessage = "The title must be from 1 to 50 characters in length.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "The description must be from 0 to 500 characters in length.")]
        public string Description { get; set; }

        [Column("start_date")]
        public DateTime Start
        {
            get
            {
                return _start ?? StartDate.Date + StartTime.TimeOfDay;
            }
            set { _start = value; }
        }

        [Required(ErrorMessage = "You must provide a start date.")]
        [NotMapped]
        public DateTime StartDate { get; set; } // Used for model binding purposes only.

        [Required(ErrorMessage = "You must provide a start time.")]
        [NotMapped]
        public DateTime StartTime { get; set; } // Used for model binding purposes only.

        [Column("due_date")]
        public DateTime Due 
        {
            get
            {
                return _due ?? DueDate.Date + DueTime.TimeOfDay;                    
            }
            set { _due = value; }
        }

        [Required(ErrorMessage = "You must provide a due date.")]
        [NotMapped]
        public DateTime DueDate { get; set; } // Used for model binding purposes only.

        [Required(ErrorMessage = "You must provide a due time.")]
        [NotMapped]
        public DateTime DueTime { get; set; } // Used for model binding purposes only.

        [NotMapped]
        public TimeSpan TimeLeft
        {
            get { return Due - DateTime.Now; }
        }

        [Column("owner")]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        [Required(ErrorMessage = "You must assign the to-do item to someone.")]
        [Column("assigned_to")]
        [ForeignKey("Assignee")]
        public int AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public bool Completed { get; set; }

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