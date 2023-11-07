using F2021A6MO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Controllers
{
    public class AlbumsController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        //------------------------------------------------------------------

        // GET: Album
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        //------------------------------------------------------------------

        // GET: Album/Details/5
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var obj = m.AlbumGetById(id.GetValueOrDefault());

            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(obj);
            }
        }

        //------------------------------------------------------------------

        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                //1. Create a new TrackAddForm.
                var TrackaddForm = new TrackAddFormViewModel();

                //2. Configure the album name. Your TrackAddForm should have a string property for that.
                TrackaddForm.AlbumName = album.Name;

                //3. Configure the album identifier. Your TrackAddForm should have an integer property for that.
                TrackaddForm.AlbumId = album.Id;

                //4. Configure the select list object for the genres.
                TrackaddForm.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(TrackaddForm);
            }
        }

        //------------------------------------------------------------------

        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        [HttpPost,ValidateInput(false)]
        public ActionResult AddTrack(TrackAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var addedTrack = m.TrackAdd(newItem);

            if (addedTrack == null)
            {
                return View(addedTrack);
            }
            else
            {
                return RedirectToAction("details", "tracks", new { id = addedTrack.Id });
            }
        }
        //------------------------------------------------------------------
        
        [Route("clip/{id}")]
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult GetAudio(int? id)
        {
            // Attempt to get the matching object
            var o = m.TrackAudio(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (o.AudioContentType == null && o.Audio.Length == 0)
                {
                    return Content("No Audio");
                }
                return File(o.Audio, o.AudioContentType);
            }
        }

    }
}
