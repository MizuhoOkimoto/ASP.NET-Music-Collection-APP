using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Controllers
{
    public class MediaController : Controller
    {
        Manager m = new Manager();

        //----------------------------------------------------------------
        
        // GET: Media
        public ActionResult Index()
        {
            return RedirectToAction("index", "home");
        }

        //----------------------------------------------------------------

        // GET: Media/Details/5
        //Dedicated media delivery method, uses attribute routing
        [Route("media/{stringId}")]
        public ActionResult Details(string stringId = "")
        {
            // Attempt to get the matching object
            var media = m.MediaGetById(stringId);

            if (media == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (media.ContentType != null)
                {
                    // Return a file content result
                    // Set the Content-Type header, and return the photo bytes
                    return File(media.Content, media.ContentType);
                }

                return Content("No media found!");
            }
        }

        //----------------------------------------------------------------

        
        //----------------------------------------------------------------
        //Dedicated photo DOWNLOAD method, uses attribute routing
        [Route("media/{stringId}/download")]
        public ActionResult DetailsDownload(string stringId = "")
        {
            // Attempt to get the matching object
            var media = m.MediaGetById(stringId);

            if (media == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Get file extension, assumes the web server is Microsoft IIS based
                // Must get the extension from the Registry
                // (which is a key-value storage structure for configuration settings, for the Windows operating system
                // and apps that opt to use the Registry)

                // Working variables
                string extension;
                RegistryKey key;
                object value;

                // Open the Registry, attempt to locate the key
                key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + media.ContentType, false);
                // Attempt to read the value of the key
                value = (key == null) ? null : key.GetValue("Extension", null);
                // Build/create the file extension string
                extension = (value == null) ? string.Empty : value.ToString();

                // Create a new Content-Disposition header
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    // Assemble the file name + extension
                    FileName = $"media-{stringId}{extension}",
                    // Force the media item to be saved (not viewed)
                    Inline = false
                };
                // Add the header to the response
                Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
                return File(media.Content, media.ContentType);
            }
        }

        //------------------------------------------------------------------------------------------------------------

        // GET: Media/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Media/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Media/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Media/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
