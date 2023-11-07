using F2021A6MO.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A6MO.Controllers
{
    public class ArtistsController : Controller
    {
        Manager m = new Manager();

        // GET: Artist
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Details(int? id)
        {
            var obj = m.ArtistGetById(id.GetValueOrDefault());
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }
        }

        //**********************************************************************************
        //With media item info
        // GET: Artists/DetailsWithMediaInfo/5
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        [Route("artists/DetailsWithMediaItems/{id}")]
        public ActionResult DetailsWithMediaInfo(int? id)
        {
            var artist = m.ArtistGetByIdWithMediaInfo(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(artist);
            }
        }

        /*//This is from code example!
        // GET: Properties/DetailsWithPhotoInfo/5
        public ActionResult DetailsWithPhotoInfo(int? id)
        {
            // Attempt to get the matching object
            var o = m.PropertyGetByIdWithPhotoInfo(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }*/

        //**********************************************************************************
        // GET: Artist/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var artist = new ArtistAddFormViewModel();
            //artist.Executive = User.Identity.Name; //Not sure
            artist.ArtistGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(artist);
        }
        //**********************************************************************************
        // POST: Artist/Create
        [HttpPost, ValidateInput(false)] //This is for avoiding error
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ArtistAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            try
            {
                var addedArtist = m.ArtistAdd(newItem);
                if (addedArtist == null)
                {
                    return View(addedArtist);
                }
                else
                {
                    return RedirectToAction("details", new { id = addedArtist.Id });
                }
            }
            catch
            {
                return View(newItem);
            }
        }
        //**********************************************************************************
        //GET AddAlbum
        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());
            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var addAlbum = new AlbumAddFormViewModel();
                var artistsSelect = new List<int> { artist.Id };
                addAlbum.ArtistName = artist.Name;
                addAlbum.AlbumGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                /*addAlbum.ArtistNameList = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", artistsSelect);
                addAlbum.TrackList = new MultiSelectList(m.TrackGetAll(), "Id", "Name");*/
                return View(addAlbum);
            }
        }

        //**********************************************************************************   
        // POST: Artists/Create
        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addalbum")]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {

            // TODO: Add insert logic here
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedAlbum = m.AlbumAdd(newItem);

            if (addedAlbum == null)
            {
                return View(addedAlbum);
            }
            else
            {
                return RedirectToAction("details", "Albums", new { id = addedAlbum.Id });
            }
        }
        //*************************************************************************************
        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addmedia")]
        public ActionResult AddMedia(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var mediaForm = new MediaItemAddFormViewModel();

                mediaForm.ArtistId = artist.Id;
                mediaForm.ArtistName = artist.Name;

                return View(mediaForm);
            }
        }
        //*************************************************************************************
        [HttpPost]
        [Authorize(Roles = "Coordinator"), Route("artists/{id}/addmedia")]
        public ActionResult AddMedia(int? id, MediaItemAddViewModel newMedia)
        {
            try
            {
                if (!ModelState.IsValid && id.GetValueOrDefault() == newMedia.ArtistId)
                {
                    return View(newMedia);
                }

                var artistWithMedia = m.AddArtistMedia(newMedia);

                if (artistWithMedia == null)
                {
                    return View(newMedia);
                }
                else
                {
                    //Id: ArtistBaseViewModel
                    return RedirectToAction("DetailsWithMediaInfo", "artists", new { id = artistWithMedia.Id }); 
                }
            }
            catch
            {
                return View();
            }
        }

        //*************************************************************************************
        //Dedicated MEDIA DOWNLOAD method, uses attribute routing
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
                //Response.ContentType = $"FileName"; //DIDN'T WORK
                Response.AppendHeader("Content-Disposition", contentDisposition.ToString());

                return File(media.Content, media.ContentType);
            }
        }



    }
}
