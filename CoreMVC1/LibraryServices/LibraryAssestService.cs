using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class LibraryAssestService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssestService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset=>asset.Status)
                .Include(asset => asset.Location);
        }

       
        public LibraryAsset GetByid(int id)
        {
            return GetAll()
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetByid(id).Location;
        }

        public string GetDewyIndex(int id)
        {
            //descrminator
            if (_context.Books.Any(book=>book.Id==id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == id)
                    .DeweyIndex;
            }
            return "";
        }

        public string GetIsbn(int id)
        {
            throw new NotImplementedException();
        }

        public string GetTitle(int id)
        {
            return GetAll()
                .FirstOrDefault(asset => asset.Id == id)
                .Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(asset => asset.Id == id).Any();
            return book ? "Book" : "Video";
        }
        public string GetAuthorOrDirecor(int id)
        {
            throw new NotImplementedException();
        }

    }
}
