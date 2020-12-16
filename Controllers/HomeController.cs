using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private Context dbcontext;
        public HomeController(Context context)
        {
            dbcontext = context;
        }
        public IActionResult Index()
        {
            User logged = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(logged==null)
            {
                return RedirectToAction("Login","Login");
            }
            ViewBag.User = logged;
            List<Wedding> AllWeddings = dbcontext.Weddings.Include(w => w.RSVPs).ThenInclude(w => w.GuestList).ToList();
            foreach(Wedding w in AllWeddings)
            {
                if(w.Date < DateTime.Now)
                {
                    dbcontext.Remove(w);
                    dbcontext.SaveChanges();
                    AllWeddings.Remove(w);
                }
            }

            ViewBag.AllWeddings = AllWeddings;
            User u = dbcontext.Users.Include(u => u.RSVP).ThenInclude(r =>r.Wedding).FirstOrDefault(u => u.UserID == logged.UserID);
            List<int> UserWeddings = new List<int>();
            foreach(RSVP r in u.RSVP)
            {
                UserWeddings.Add(r.WeddingID);
            }
            ViewBag.UserWeddings = UserWeddings;
            return View(); 
        }
        [HttpGet("add")]
        public IActionResult Add()
        {
            if(HttpContext.Session.GetObjectFromJson<User>("LoggedUser")==null)
            {
                return RedirectToAction("Login","Login");
            }
            return View(); 
        }
        [HttpGet("weddings/{routeid}")]
        public IActionResult Details(int routeid)
        {
            if(HttpContext.Session.GetObjectFromJson<User>("LoggedUser")==null)
            {
                return RedirectToAction("Login","Login");
            }
            ViewBag.Wedding = dbcontext.Weddings.Include(w => w.RSVPs).ThenInclude(r =>r.GuestList).FirstOrDefault(w => w.WeddingID == routeid);

            return View(); 
        }
        public IActionResult Create(Wedding newWed)
        {
            if(ModelState.IsValid)
            {
                User loggedin = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
                newWed.UserID = loggedin.UserID;
                dbcontext.Weddings.Add(newWed);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Add");
        }
        [HttpGet("rsvp/{routeid}")]
        public IActionResult RSVP(int routeid)
        {
            User logged = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            RSVP newRSVP = new RSVP();
            newRSVP.UserID = logged.UserID;
            newRSVP.WeddingID = routeid;
            dbcontext.Add(newRSVP);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("unrsvp/{routeid}")]
        public IActionResult UNRSVP(int routeID)
        {
            User logged = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            RSVP change = dbcontext.RSVPs.FirstOrDefault(r => r.UserID == logged.UserID && r.WeddingID == routeID);
            dbcontext.Remove(change);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("delete/{routeid}")]
        public IActionResult Delete(int routeID)
        {
            Wedding change = dbcontext.Weddings.FirstOrDefault(w => w.WeddingID == routeID);
            dbcontext.Weddings.Remove(change);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
        
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        string value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }
}
}
