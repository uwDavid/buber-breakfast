using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    // We use a dictionary store for now
    private static readonly Dictionary<Guid, Breakfast> _breakDict = new();

    // This is the actual implementation of the service
    public ErrorOr<Created> CreateBreakfast(Breakfast req)
    {
        _breakDict.Add(req.Id, req);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakDict.Remove(id);
        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_breakDict.TryGetValue(id, out var breakfast))
        {
            return breakfast;   //converted to ErrorOr obj via ErrorOr internal converter
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<Updated> Upsert(Breakfast b)
    {
        _breakDict[b.Id] = b;
        return Result.Updated;
    }
}