using Microsoft.AspNetCore.Mvc;
using SessionDemo.Web.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SessionDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //int? number = HttpContext.Session.GetInt32("number");
            //int count = 1;
            //if (number.HasValue)
            //{
            //    count = number.Value;
            //}

            //HttpContext.Session.SetInt32("number", count + 1);
            //return View(new NumberViewModel
            //{
            //    Number = count
            //});

            NumberViewModel vm = HttpContext.Session.Get<NumberViewModel>("nvm");
            if(vm == null)
            {
                vm = new NumberViewModel { Number = 0 };
            }

            vm.Number++;
            HttpContext.Session.Set("nvm", vm);

            return View(vm);
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}

/*
* Create an application that allows users to post free stuff. 
* 
* On the home page, there should be a list of all the postings, sorted by date 
* descending (most recent first). 
* 
* There should then be a second page, that allows the user to post a new listing. A 
* listing should have: DateCreated, Text, Name (optional) and a Phone Number. 
* 
* Since we're not creating any kind of login system, we need to give users the ability
* to delete their listings. The way this will be achieved is via Session. E.g.
* In Session, we'll be storing a List<int> that will keep track of all the ids posted
* by the current user. Therefore, every time someone posts a listing, we'll add that new id
* to that list.
*
* Then, when the home page is shown, for every listing we're showing, if that listing is in 
* the session List<int>, a delete button should be shown next to the posting,
* which when clicked, should delete the post. (Alternatively, this can be done in a cookie
* where you store the ids as a comma separated string of ids e.g. (1,2,23,4) -- do this for EC)
* 
* 
* Good luck!
*/
