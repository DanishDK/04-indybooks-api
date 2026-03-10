#nullable enable
using IndyBooks.Models;
namespace IndyBooks.Services;
public interface IWriterService
{
    public List<Writer> GetWriterList();
    public Writer? GetWriterById(long id);

   //* TODO: Remove this comment and complete the methods below

    public Writer? DeleteWriterById(long id);
    
    public long PostWriter(Writer writer);
    public Writer? PutWriter(Writer writer, long id);
}