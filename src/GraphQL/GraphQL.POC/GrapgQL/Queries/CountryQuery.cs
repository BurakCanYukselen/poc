﻿using GraphQL.POC.GrapgQL.Models;
using GraphQL.POC.Repo;
using GraphQL.Types;

namespace GraphQL.POC.GrapgQL.Queries
{
    public class CountryQuery : ObjectGraphType
    {
        public CountryQuery(ICountryHttpClient countryHttpClient)
        {
            Name = this.GetType().Name;
            FieldAsync<ListGraphType<CountryType>>(
                name: "countries",
                resolve: async (context) =>
                {
                    var query = await countryHttpClient.GetAll();
                    return query;
                }
                );
        }
    }

    //public class MyHotelQuery : ObjectGraphType
    //{
    //    public MyHotelQuery(ReservationRepository reservationRepository)
    //    {
    //        Field<ListGraphType<ReservationType>>("reservations",
    //            arguments: new QueryArguments(new List<QueryArgument>
    //            {
    //                new QueryArgument<IdGraphType>
    //                {
    //                    Name = "id"
    //                },
    //                new QueryArgument<DateGraphType>
    //                {
    //                    Name = "checkinDate"
    //                },
    //                new QueryArgument<DateGraphType>
    //                {
    //                    Name = "checkoutDate"
    //                },
    //                new QueryArgument<BooleanGraphType>
    //                {
    //                    Name = "roomAllowedSmoking"
    //                },
    //                new QueryArgument<RoomStatusType>
    //                {
    //                    Name = "roomStatus"
    //                }
    //            }),
    //            resolve: context =>
    //            {
    //                var query = reservationRepository.GetQuery();

    //                var reservationId = context.GetArgument<int?>("id");
    //                if (reservationId.HasValue)
    //                {
    //                    return reservationRepository.GetQuery().Where(r => r.Id == reservationId.Value);
    //                }

    //                var checkinDate = context.GetArgument<DateTime?>("checkinDate");
    //                if (checkinDate.HasValue)
    //                {
    //                    return reservationRepository.GetQuery()
    //                        .Where(r => r.CheckinDate.Date == checkinDate.Value.Date);
    //                }

    //                var checkoutDate = context.GetArgument<DateTime?>("checkoutDate");
    //                if (checkoutDate.HasValue)
    //                {
    //                    return reservationRepository.GetQuery()
    //                        .Where(r => r.CheckoutDate.Date >= checkoutDate.Value.Date);
    //                }

    //                var allowedSmoking = context.GetArgument<bool?>("roomAllowedSmoking");
    //                if (allowedSmoking.HasValue)
    //                {
    //                    return reservationRepository.GetQuery()
    //                        .Where(r => r.Room.AllowedSmoking == allowedSmoking.Value);
    //                }

    //                var roomStatus = context.GetArgument<RoomStatus?>("roomStatus");
    //                if (roomStatus.HasValue)
    //                {
    //                    return reservationRepository.GetQuery().Where(r => r.Room.Status == roomStatus.Value);
    //                }

    //                return query.ToList();
    //            }
    //        );

    //    }
    //}
}
