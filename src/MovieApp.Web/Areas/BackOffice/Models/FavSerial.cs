using MovieApp.Web.Areas.Account.Models;
using MovieApp.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Areas.BackOffice.Models
{
    public class FavSerial
    {
        public int Id { get; set; }
        public int DivertismentTypeId { get; set; }
        [Display(Name = "Divertisment type")]
        public virtual DivertismentTypes DivertismentType { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genres Genre { get; set; }
        [Display(Name = "Number of seasons")]
        public int NumberOfSeasons { get; set; }
        [Display(Name = "Number of episodes")]
        public int NumberOfEpisodes { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date released")]
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
