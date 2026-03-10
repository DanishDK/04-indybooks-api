#nullable enable
using IndyBooks.Models;

namespace IndyBooks.Services;
public class ApiWriterService : IWriterService
{
    private IndyBooksDataContext _db;
    public ApiWriterService(IndyBooksDataContext db) { _db = db; }
   
    public List<Writer> GetWriterList()
    {
        return _db.Writers.ToList();
    }
    public Writer? GetWriterById(long id)
    {
        return _db.Writers.SingleOrDefault(w=>w.Id == id); //Uses lamda function and extension methods here
    }
    public Writer DeleteWriterById(long id)
    {
        //TODO: Get the Writer at the given id from the db context
        //     Remove the Writer at that id, be sure to SaveChanges()
        return new Writer{ Name = "Fix the ApiWriter Service DeleteWriterById method"}; //TODO: return the deleted Writer info
    }
    public long PostWriter(Writer writer)
    {
        //TODO : Add a new Writer to the db context, return the writer id
        return 0;
    }
    public Writer PutWriter(Writer writer, long id)
    {
        //TODO: Update the Writer at the given id, return the Writer
        return new Writer{ Name = "Fix the ApiWriter Service PutWriter method"};;
    }
}
