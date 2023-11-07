// **************************************************
// WEB524 Project Template V3 == 6557277e-3a73-4362-a4d4-7ca822512818
// Do not change this header.
// **************************************************

using AutoMapper;
using F2021A6MO.EntityModels;
using F2021A6MO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace F2021A6MO.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistAddFormViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>(); //opposite
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistAddFormViewModel>();

                cfg.CreateMap<Artist, ArtistWithMediaInfoViewModel>();


                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackAddFormViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackEditFormViewModel>();
                cfg.CreateMap<Track, TrackAudioViewModel>(); //get audio

                cfg.CreateMap<MediaItem, MediaItemBaseViewModel>();
                cfg.CreateMap<MediaItem, MediaItemContentViewModel>();
                cfg.CreateMap<MediaItemAddViewModel, MediaItem>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();


                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        //***************************************************************************************
        /*Genre*/
        //GET ALL
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genre = ds.Genres.OrderBy(Genre => Genre.Name);

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(genre);
        }

        //***************************************************************************************
        /*Artist*/
        //GET ALL
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var sorting = ds.Artists.OrderBy(Artist => Artist.Name);
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(sorting);
        }

        //GET ONE
        public ArtistBaseViewModel ArtistGetById(int id)
        {
            var artist = ds.Artists.Include("Albums").SingleOrDefault(o => o.Id == id);
            return artist == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(artist);
        }

        //ANOTHER GET ONE
        //fetch/include the media items and return an object of the new type
        public ArtistWithDetailViewModel ArtistGetByIdWithMediaInfo(int id)
        {
            var artist = ds.Artists.Include("MediaItems").Include("Albums").SingleOrDefault(a => a.Id == id);
            return mapper.Map<Artist, ArtistWithMediaInfoViewModel>(artist);
        }

        //ADD NEW
        public ArtistWithDetailViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            newArtist.Executive = HttpContext.Current.User.Identity.Name;
            var artist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            ds.SaveChanges();
            // If successful, return the added item (mapped to a view model class).
            return artist == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(artist);
        }

        //***************************************************************************************
        /*Album*/
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var sorting = ds.Albums.OrderBy(Album => Album.ReleaseDate);
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(sorting);
        }
        //GET ONE
        public AlbumWithDetailViewModel AlbumGetById(int id)
        {
            var album = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(o => o.Id == id);
            return album == null ? null : mapper.Map<Album, AlbumWithDetailViewModel>(album);
        }

        //ADD NEW
        public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newItem)
        {
            newItem.Coordinator = HttpContext.Current.User.Identity.Name;

            var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
            ds.SaveChanges();
            return (addedAlbum != null) ? mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum) : null;

        }


        /*        //ADD NEW THIS IS FOR ASSIGNMENT5
                public AlbumWithDetailViewModel AlbumAdd(AlbumAddViewModel newItem)
                {
                    newItem.Coordinator = HttpContext.Current.User.Identity.Name;

                    List<Artist> artristList = new List<Artist>();
                    List<Track> trackList = new List<Track>();
                    if (newItem.ArtistIds.Count() > 0)
                    {
                        foreach (var artistID in newItem.ArtistIds)
                        {
                            Artist artist = ds.Artists.Find(artistID);
                            if (artist != null)
                            {
                                artristList.Add(artist);
                            }
                        }
                    }
                    if (newItem.TrackIds.Count() > 0)
                    {
                        foreach (var trackID in newItem.ArtistIds)
                        {
                            Track track = ds.Tracks.Find(trackID);
                            if (track != null)
                            {
                                trackList.Add(track);
                            }
                        }
                    }

                    if (trackList.Count() > 0 || artristList.Count() > 0)
                    {
                        Album addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                        addedAlbum.Artists = artristList;
                        addedAlbum.Tracks = trackList;
                        addedAlbum.Coordinator = HttpContext.Current.User.Identity.Name;
                        ds.SaveChanges();

                        return (addedAlbum != null) ? mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum) : null;
                    }
                    return null;

                }*/
        //************************************************************************************
        /*Track*/
        //GET ALL
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = ds.Tracks.Include("Albums")
                                    .OrderBy(Track => Track.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track);
        }
        //GET ONE
        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var track = ds.Tracks.Include("Albums.Artists").SingleOrDefault(a => a.Id == id);
            if (track == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Track, TrackWithDetailViewModel>(track);
                result.AlbumNames = track.Albums.Select(a => a.Name);
                return result;
            }
        }


        //ADD NEW
        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            // Check user authentication
            newTrack.Clerk = HttpContext.Current.User.Identity.Name;

            // Attempt to add the new item
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            // First, extract the bytes from the HttpPostedFile object
            byte[] audioBytes = new byte[newTrack.AudioUpload.ContentLength];
            newTrack.AudioUpload.InputStream.Read(audioBytes, 0, newTrack.AudioUpload.ContentLength);

            // Then, configure the new object's properties
            addedTrack.Audio = audioBytes;
            addedTrack.AudioContentType = newTrack.AudioUpload.ContentType;

            ds.SaveChanges();
            return (addedTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);

            /*var album = ds.Albums.Find(newTrack.AlbumId);
            if (album == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));
                addedItem.Albums = new List<Album> { album };
                addedItem.Clerk = HttpContext.Current.User.Identity.Name;

                ds.SaveChanges();
                return addedItem == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }*/
        }

        //"ADD NEW" ALBUM TASK
        //It will be called when you create/configure the multi-select list in the AlbumsController.
        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var o = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);
            if (o == null) return null;

            var c = new List<Track>();

            foreach (var album in o.Albums) { c.AddRange(album.Tracks); }

            c = c.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(c.OrderBy(a => a.Name));
        }

        //GET AUDIO
        //TrackAudio
        public TrackAudioViewModel TrackAudio(int id)
        {
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : mapper.Map<Track, TrackAudioViewModel>(o);
        }

        //EDIT TRACK
        public TrackWithDetailViewModel TrackEdit(TrackEditViewModel editedTrack)
        {
            var track = ds.Tracks.Find(editedTrack.Id);

            if (track == null)
            {
                return null;
            }
            else
            {
                byte[] audioBytes = new byte[editedTrack.AudioUpload.ContentLength];
                editedTrack.AudioUpload.InputStream.Read(audioBytes, 0, editedTrack.AudioUpload.ContentLength);
                track.Audio = audioBytes;
                track.AudioContentType = editedTrack.AudioUpload.ContentType;

                ds.SaveChanges();

                return mapper.Map<Track, TrackWithDetailViewModel>(track);
            }
        }
        //DELETE TRACK
        public bool TrackDelete(int? id)
        {
            var track = ds.Tracks.Find(id);

            if (track == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(track);
                ds.SaveChanges();
                return true;
            }
        }

        //***************************************************************************************
        /*MediaItem*/

        public MediaItemContentViewModel MediaGetById(string stringId)
        {
            var media = ds.MediaItems.SingleOrDefault(mItem => mItem.StringId == stringId);

            return (media == null) ? null : mapper.Map<MediaItem, MediaItemContentViewModel>(media);
        }

        //add media item for artist
        //adding a new object for a known/existing object.
        public ArtistWithMediaInfoViewModel AddArtistMedia(MediaItemAddViewModel newMedia)
        {
            var artist = ds.Artists.Find(newMedia.ArtistId);
            if (artist == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedMedia = new MediaItem();
                ds.MediaItems.Add(addedMedia);

                addedMedia.Caption = newMedia.Caption;
                addedMedia.Artist = artist;

                // First, extract the bytes from the HttpPostedFile object
                byte[] mediaBytes = new byte[newMedia.MediaUpload.ContentLength];
                newMedia.MediaUpload.InputStream.Read(mediaBytes, 0, newMedia.MediaUpload.ContentLength);
                
                // Then, configure the new object's properties
                addedMedia.Content = mediaBytes;
                addedMedia.ContentType = newMedia.MediaUpload.ContentType;

                ds.SaveChanges();

                return (addedMedia == null) ? null : mapper.Map<Artist, ArtistWithMediaInfoViewModel>(artist);
            }
        }

        //***************************************************************************************

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method



        public bool LoadData()
        {

            /*foreach (var e in ds.Tracks) { ds.Entry(e).State = System.Data.Entity.EntityState.Deleted; }
            ds.SaveChanges();
            foreach (var e in ds.Genres) { ds.Entry(e).State = System.Data.Entity.EntityState.Deleted; }
            ds.SaveChanges();
            foreach (var e in ds.Albums) { ds.Entry(e).State = System.Data.Entity.EntityState.Deleted; }
            ds.SaveChanges();
            foreach (var e in ds.Artists) { ds.Entry(e).State = System.Data.Entity.EntityState.Deleted; }
            ds.SaveChanges();*/

            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }
}