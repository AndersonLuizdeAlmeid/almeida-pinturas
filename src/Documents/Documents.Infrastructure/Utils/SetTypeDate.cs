using Documents.Infrastructure.Domain.Enums;
using System;

namespace Documents.Infrastructure.Utils;
public static class SetTypeDate
{
    public static TypeEnum SetType(DateTime date)
    {
        if(date < DateTime.UtcNow)
            return TypeEnum.Expirated;
        if(date >= DateTime.UtcNow)
            return TypeEnum.Valid;
        return TypeEnum.Invalid;
    }
}