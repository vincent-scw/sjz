using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SJZ.Images.Repository
{
    public interface IImageRepository
    {
        Task<List<Uri>> GetImagesByUserAsnyc(string user, int year, int month);
        Task<Uri> UploadImageAsync(string user, int year, int month, string name, System.IO.Stream stream);
    }
}
