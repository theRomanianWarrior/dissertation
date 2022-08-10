using VacationPackageWebApi.Domain.Attractions;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction.Mapper;

public static class AttractionMapper
{
    public static AttractionBusinessModel ToBusinessModel(this OpenTripMapAttraction attraction)
    {
        return new AttractionBusinessModel()
        {
            Xid = attraction.Xid,
            Country = attraction.Country,
            CountryCode = attraction.CountryCode,
            County = attraction.County,
            Geometry = attraction.Geometry,
            Height = attraction.Height,
            Html = attraction.Html,
            Image = attraction.Image,
            Kinds = attraction.Kinds,
            Lat = attraction.Lat,
            LatMax = attraction.LatMax,
            LatMin = attraction.LatMin,
            Lon = attraction.Lon,
            LonMax = attraction.LonMax,
            LonMin = attraction.LonMin,
            Name = attraction.Name,
            Neighbourhood = attraction.Neighbourhood,
            Osm = attraction.Osm,
            Otm = attraction.Otm,
            Pedestrian = attraction.Pedestrian,
            Postcode = attraction.Postcode,
            Rate = attraction.Rate,
            Source = attraction.Source,
            State = attraction.State,
            Suburb = attraction.Suburb,
            Text = attraction.Text,
            Title = attraction.Title,
            Town = attraction.Town,
            Width = attraction.Width,
            Wikidata = attraction.Wikidata,
            Wikipedia = attraction.Wikipedia,
        };
    }
}