using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Model
{
    public enum Status { Incomplete, Complete }
    public class TaskModel
    {
        /// <summary>
        /// Schema for Task table, with model validations
        /// </summary>
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(150, ErrorMessage = "Please limit description to be less than 150 characters")]
        public string Description { get; set; }

        // Constrain to default discrete, int32 implementation values
        [Range(0, 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int Progress { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Update Date")]
        public DateTime DateUpdated { get; set; }
    }
}
