using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using Xamarin.Forms;
using TravelRecordApp.Models;
using Java.Util;
using Android.Gms.Tasks;
using Java.Interop;
using Firebase.Firestore;

[assembly: Dependency(typeof(TravelRecordApp.Droid.Dependencies.FirestoreAndriod))]
namespace TravelRecordApp.Droid.Dependencies
{
    public class FirestoreAndriod : Java.Lang.Object, IFirestore<Post>, IOnCompleteListener
    {
        private const string postsCollection = "posts";
        private List<Post> posts;
        private bool postRead;

        public FirestoreAndriod()
        {
            posts = new List<Post>();
        }


        public Task<bool> Delete(Post data) 
        {
            try
            {
                var collection =  Firebase.Firestore.FirebaseFirestore.Instance.Collection(postsCollection);
                collection.Document(data.Id).Delete();
                return System.Threading.Tasks.Task.FromResult(true);
            }
            catch (Exception)
            {
                return System.Threading.Tasks.Task.FromResult(false);
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var result = (QuerySnapshot) task.Result;
                foreach (var post in result.Documents)
                {
                    var newPost = new Post()
                    {
                        Id = post.Id,
                        Experience = post.Get("experience").ToString(),
                        VenueId = post.Get("venueId").ToString(),
                        VenueName = post.Get("venueName").ToString(),
                        Address = post.Get("address").ToString(),
                        Latitude = (double)post.Get("latitude"),
                        Longitude = (double)post.Get("longitude"),
                        CategoryId = post.Get("categoryId").ToString(),
                        CategoryName = post.Get("categoryName").ToString(),
                        UserId = post.Get("userId").ToString()
                    };
                    posts.Add(newPost);
                }
                postRead = true;
            }
            else
                postRead = true;
        }

        public async Task<List<Post>> ReadAll()
        {
            try
            {
                posts.Clear();
                postRead = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection(postsCollection);
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);
                while(!postRead)
                {
                    await System.Threading.Tasks.Task.Delay(1000);
                }
                return posts;
            }
            catch (Exception)
            {
                return posts;
            }
        }

        public Task<bool> Update(Post data)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
                {
                    { "experience", data.Experience },
                    { "venueId", data.VenueId },
                    { "venueName", data.VenueName },
                    { "address", data.Address },
                    { "latitude", data.Latitude },
                    { "longitude", data.Longitude },
                    { "categoryId", data.CategoryId },
                    { "categoryName", data.CategoryName},
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
                };

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection(postsCollection);
                collection.Document(data.Id).Update(postDocument);
                return System.Threading.Tasks.Task.FromResult(true);
            }
            catch (Exception)
            {
                return System.Threading.Tasks.Task.FromResult(false);
            }
        }

        public Task<bool> Write(Post data)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
            {
                { "experience", data.Experience },
                { "venueId", data.VenueId },
                { "venueName", data.VenueName },
                { "address", data.Address },
                { "latitude", data.Latitude },
                { "longitude", data.Longitude },
                { "categoryId", data.CategoryId },
                { "categoryName", data.CategoryName},
                { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
            };

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection(postsCollection);
                collection.Add(new HashMap(postDocument));
                return System.Threading.Tasks.Task.FromResult(true);
            }
            catch (Exception)
            {
                return System.Threading.Tasks.Task.FromResult(false);
            }
        }


        #region not implemented methods

        public IntPtr Handle => throw new NotImplementedException();

        public int JniIdentityHashCode => throw new NotImplementedException();

        public JniObjectReference PeerReference => throw new NotImplementedException();

        public JniPeerMembers JniPeerMembers => throw new NotImplementedException();

        public JniManagedPeerStates JniManagedPeerState => throw new NotImplementedException();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Disposed()
        {
            throw new NotImplementedException();
        }

        public void DisposeUnlessReferenced()
        {
            throw new NotImplementedException();
        }

        public void Finalized()
        {
            throw new NotImplementedException();
        }
        public void SetJniIdentityHashCode(int value)
        {
            throw new NotImplementedException();
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
            throw new NotImplementedException();
        }

        public void SetPeerReference(JniObjectReference reference)
        {
            throw new NotImplementedException();
        }

        public void UnregisterFromRuntime()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}