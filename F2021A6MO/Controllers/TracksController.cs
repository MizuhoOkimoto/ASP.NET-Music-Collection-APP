using F2021A6MO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Controllers
{
    public class TracksController : Controller
    {
            Manager m = new Manager();

            // GET: Track
            [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
            public ActionResult Index()
            {
                return View(m.TrackGetAll());
            }
        //---------------------------------------------------------------------

            // GET: Track/Details/5
            [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
            public ActionResult Details(int? id)
            {
                var obj = m.TrackGetById(id.GetValueOrDefault());
                if (obj == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(obj);
                }
            }

        //---------------------------------------------------------------------

        // GET: Tracks/Edit/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id)
        {
            var track = m.TrackGetById(id.GetValueOrDefault());

            if (track == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = m.mapper.Map<TrackWithDetailViewModel, TrackEditFormViewModel>(track);
                return View(form);
            }
        }

        //---------------------------------------------------------------------

        // POST: Tracks/Edit/5
        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditViewModel newTrack)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Edit", new { id = newTrack.Id });
                }
                else if (id.GetValueOrDefault() != newTrack.Id)
                {
                    return RedirectToAction("Index");
                }

                var editedTrack = m.TrackEdit(newTrack);

                if (editedTrack == null)
                {
                    return RedirectToAction("Edit", new { id = newTrack.Id });
                }
                else
                {
                    return RedirectToAction("Details", new { id = newTrack.Id });
                }

            }
            catch
            {
                return View();
            }
        }

        //---------------------------------------------------------------------

        // GET: Tracks/Delete/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {

            var trackToDelete = m.TrackGetById(id.GetValueOrDefault());

            if (trackToDelete == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(trackToDelete);
            }

        }

        //---------------------------------------------------------------------

        // POST: Tracks/Delete/5
        [HttpPost]
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                m.TrackDelete(id.GetValueOrDefault());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //---------------------------------------------------------------------

    }
}
