using Microsoft.AspNetCore.Http;
using Riddle.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riddle.ViewModels
{
    public class RiddlePostViewModel //when we create for actual post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public RiddleType RiddleType { get; set; }

        [Required]
        public string QuestionDetails { get; set; }

        [Required]
        public string Answer { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

       // public IFormFile Image { get; set; } = null; 
        //IFormFile is an interface for any sort of file
    }
}
