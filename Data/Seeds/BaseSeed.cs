using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using reman.Models;

namespace reman.Data.Seeds;

public abstract class BaseSeed<T> where T : class
{
    protected readonly RemanContext _context;

    protected BaseSeed(RemanContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        if (this.DataAlreadyAdded())
        {
            return;
        }

        _context.AddRange(GetData());
        _context.SaveChanges();
    }

    protected abstract List<T> GetData();

    protected abstract bool DataAlreadyAdded();
}
