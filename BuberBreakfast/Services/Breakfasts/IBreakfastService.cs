using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services;

public interface IBreakfastService
{
    // This is for our internal models
    ErrorOr<Created> CreateBreakfast(Breakfast req);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<Updated> Upsert(Breakfast b);
}