using MovieApp.Web.Areas.Account.Models;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;


namespace MovieApp.Web.Areas.BackOffice.Models
{
    public class Film
    {
        public int Id { get; set; }
        public int DivertismentTypeId { get; set; }
        [Display(Name ="Divertisment type")]
        public virtual DivertismentTypes DivertismentType { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public  Genres Genre { get; set; }
        [DataType(DataType.Time)]
        public DateTime Duration { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Date released")]
        public DateTime DateReleased { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public RegisterModel User { get; set; }
        public string Trailer { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
    }
}
