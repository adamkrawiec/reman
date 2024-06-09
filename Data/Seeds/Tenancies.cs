using Microsoft.EntityFrameworkCore;
using reman.Models;

namespace reman.Data.Seeds;

class Tenancies : BaseSeed<Tenancy>
{
    public Tenancies(RemanContext _context) : base(_context)
    {
    }

    protected override List<Tenancy> GetData()
    {
        return new List<Tenancy>
        {
            newTenancy(13, 1, new DateOnly(2020, 1, 1), new DateOnly(2020, 6, 30)),
            newTenancy(13, 2, new DateOnly(2020, 8, 1), new DateOnly(2021, 12, 31)),
            newTenancy(13, 3, new DateOnly(2022, 1, 1), new DateOnly(2022, 12, 31)),
            newTenancy(13, 4, new DateOnly(2023, 2, 1)),
            newTenancy(14, 5, new DateOnly(2019, 4, 1), new DateOnly(2021, 9, 30)),
            newTenancy(14, 6, new DateOnly(2021, 10, 1), new DateOnly(2024, 1, 1)),
            newTenancy(15, 7, new DateOnly(2018, 5, 1), new DateOnly(2024, 4, 30)),
            newTenancy(16, 8, new DateOnly(2019, 1, 1), new DateOnly(2020, 5, 31)),
            newTenancy(16, 9, new DateOnly(2020, 6, 1), new DateOnly(2021, 12, 31)),
            newTenancy(16, 10, new DateOnly(2021, 1, 1), new DateOnly(2021, 9, 30)),
            newTenancy(16, 11, new DateOnly(2021, 10, 1), new DateOnly(2022, 12, 31)),
            newTenancy(16, 12, new DateOnly(2023, 1, 1)),
        };
    }

    private Tenancy newTenancy(int estateUnitId, int tenantId, DateOnly startDate, DateOnly? endDate = null)
    {
        return new Tenancy {
            EstateUnit = findEstateUnit(estateUnitId),
            Tenant = findTenant(tenantId),
            StartDate = startDate,
            EndDate = endDate
        };
    }

    private EstateUnit findEstateUnit(int estateUnitId)
    {
        return _context.EstateUnits.Find(estateUnitId);
    }

    private Tenant findTenant(int tenantId)
    {
        return _context.Tenants.Find(tenantId);
    }

    protected override bool DataAlreadyAdded()
    {
        return _context.Tenancies.Any();
    }
}

//  Id |   Name   | RealEstateId | Type | FlatNumber | Area
// ----+----------+--------------+------+------------+------
//  17 | EUC0022  |            2 |    0 |          0 |    0
//  18 | EU00023  |            2 |    1 |          0 |    0
//  19 | EU00031  |            3 |    1 |          0 |    0
//  20 | EU00024  |            2 |    1 |          0 |    0
//  14 | EU00024  |            2 |    0 |          2 | 39.5
//  15 | EU00024  |            2 |    0 |          3 | 56.1
//  16 | EU00024  |            2 |    0 |          1 | 34.2
//  13 | EU000443 |            1 |    1 |         10 | 72.3

//  Id |     Name
// ----+---------------
//   1 | John Doe
//   2 | Jane Doe
//   3 | Alex Smith
//   4 | Abby Smith
//   5 | Mark Johnson
//   6 | Tanya Johnson
//   7 | John Brown
//   8 | Jane Brown
//   9 | Duncan Idaho