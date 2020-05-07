using Riddle.Models.Comments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riddle.Models
{
    public class RiddlePost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public RiddleType RiddleType{ get; set; }

        [Required]
        public string QuestionDetails { get; set; }

        [Required]
        public string Answer { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

        public List<MainComment> MainComments { get; set; }

        //public string Image { get; set; } = "";
    }
}
