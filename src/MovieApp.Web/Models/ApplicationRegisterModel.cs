using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Areas.Account.Models;
using MovieApp.Web.Areas.BackOffice.Models;
using MovieApp.Web.Models;

namespace MovieApp.Web.Models
{
    public class ApplicationRegisterModel:DbContext
    {
        public ApplicationRegisterModel(DbContextOptions<ApplicationRegisterModel> options) : base(options)
        {

        }
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<LoginModel> LoginUser { get; set; }
        public DbSet<Film> Filmss { get; set; }
        public DbSet<DivertismentTypes> DivertismentTypes { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<MovieApp.Web.Areas.BackOffice.Models.Serials> Serialss { get; set; }
        public DbSet<ComingSoon> ComingSoon { get; set; }
        public DbSet<MovieApp.Web.Areas.BackOffice.Models.Watchlist> WatchLists { get; set; }
        public DbSet<MovieApp.Web.Areas.BackOffice.Models.FavFilm> FavFilms { get; set; }
        public DbSet<MovieApp.Web.Areas.BackOffice.Models.FavSerial> FavSerials { get; set; }
        
        
        
    }
}
