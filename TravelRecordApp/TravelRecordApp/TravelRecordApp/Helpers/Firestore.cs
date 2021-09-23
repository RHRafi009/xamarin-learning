using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers
{
    public class Firestore
    {
        private static IFirestore<Post> firestore = DependencyService.Get<IFirestore<Post>>();

        public static async Task<bool> Write(Post post)
        {
            return await firestore.Write(post);
        }
        public static async Task<bool> Update(Post post)
        {
            return await firestore.Update(post);
        }
        public static async Task<bool> Delete(Post post)
        {
            return await firestore.Delete(post);
        }
        public static async Task<List<Post>> ReadAll<T>()
        {
            return await firestore.ReadAll();
        }
    }
}
