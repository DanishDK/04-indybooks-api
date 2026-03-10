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
        //DONE: Get the Writer at the given id from the db context
        Writer writer = _db.Writers.Single(w => w.Id == id);
        _db.Writers.Remove(writer);
        _db.SaveChanges();
        //     Remove the Writer at that id, be sure to SaveChanges()
        
        return writer; //DONE: return the deleted Writer info
    }
    public long PostWriter(Writer writer)
    {
        //DONE : Add a new Writer to the db context, return the writer id

        _db.Writers.Add(writer);
        _db.SaveChanges();
        return writer.Id;
    }
    public Writer PutWriter(Writer writer, long id)
    {
        //DONE: Update the Writer at the given id, return the Writer

       Writer dbwriter = _db.Writers.Single(w => w.Id == id);
       dbwriter = writer;
       _db.Writers.Update(writer);
        _db.SaveChanges();
        return  dbwriter;
    }
    }