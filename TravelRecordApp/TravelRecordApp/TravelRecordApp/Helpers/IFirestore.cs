using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Helpers
{
    public interface IFirestore<T> where T : class
    {
        Task<bool> Write(T data);
        Task<bool> Update(T data);
        Task<bool> Delete(T data);
        Task<List<T>> ReadAll();
    }
}
