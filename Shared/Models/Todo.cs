using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Trifoia.Module.Todo.Models
{
    
    /// <summary>
    /// Todo
    /// </summary>
    [Table("Todo")]
    public class Todo : ModelBase
    {

        [Key]
        public int TodoId { get; set;}

        public int ModuleId { get; set;}

        /// <summary>
        /// [dbo].[Todo].[Topic]
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// [dbo].[Todo].[Done]
        /// </summary>
        public bool Done { get; set; }


    }
}