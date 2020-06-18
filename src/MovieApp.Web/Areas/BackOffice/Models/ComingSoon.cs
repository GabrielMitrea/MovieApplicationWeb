using MovieApp.Web.Areas.Account.Models;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Areas.BackOffice.Models
{
    public class ComingSoon
    {
        public int Id { get; set; }
        public int DivertismentTypeId { get; set; }
        [Display(Name = "Divertisment type")]
        public virtual DivertismentTypes DivertismentType { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genres Genre { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public RegisterModel User { get; set; }
        public string Trailer { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
    }
}
