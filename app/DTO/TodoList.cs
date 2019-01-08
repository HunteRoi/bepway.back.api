using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class TodoList
    {
        [StringLength(1000)]
        [DefaultValue(null)]
        public String Todo { get; set; }
    }
}