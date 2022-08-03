using DbMigrator.Models;
using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;
using DbPopulator.CsvDataProcessing.CsvModels;
using DbPopulator.Enums;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DbPopulator.Migrations
{
    [Migration(202207051250)]
    public class Migration202207051250InsertProperties : AutoReversingMigration
    {
        private const string PropertyTable = "Property";
        private const string AmenitiesPackageTable = "AmenitiesPackage";
        private const string PlaceTypeTable = "PlaceType";
        private const string PropertyTypeTable = "PropertyType";
        private const string RoomAndBedTable = "RoomAndBed";

        public override void Up()
        {
            var properties = ProcessCsvData<PropertyCsvModel>
                .ReadRecordsFromCsv(CsvLocation.CitiesPropertiesCsvLocation)
                .OrderBy(p => p.City)
                .GroupBy(c => c.City);

            MigratePlaceTypesInDatabase();
            MigratePropertyTypesInDatabase();

            foreach (var city in properties)
            {
                var existingCityInDb = CommonUsedTablesData.Cities.FirstOrDefault(x => x.Name == city.Key);

                if (existingCityInDb == default) continue;

                foreach (var property in city)
                {
                    var amenitiesPackage = new AmenitiesPackage()
                    {
                        Id = Guid.NewGuid(),
                        AirConditioning = property.AirConditioning,
                        Dryer = property.Dryer,
                        Heating = property.Heating,
                        Iron = property.Iron,
                        Kitchen = property.Kitchen,
                        Tv = property.Tv,
                        Washer = property.Washer,
                        WiFi = property.WiFi
                    };

                    Insert.IntoTable(AmenitiesPackageTable).Row(amenitiesPackage);

                    var placeTypeId = GetIdOfPlaceType(property.PlaceType);
                    var propertyTypeId = GetIdOfPropertyType(property.PropertyType);

                    var roomAndBed = new RoomAndBed()
                    {
                        Id = Guid.NewGuid(),
                        Bathroom = property.Bathroom,
                        Bed = property.Bed,
                        Bedroom = property.Bedroom
                    };

                    Insert.IntoTable(RoomAndBedTable).Row(roomAndBed);

                    var propertyForDb = new Property()
                    {
                        Id = Guid.NewGuid(),
                        AmenitiesPackageId = amenitiesPackage.Id,
                        CityId = existingCityInDb.Id,
                        Name = property.Name,
                        Pet = property.Pet,
                        PlaceTypeId = placeTypeId,
                        PricePerDay = property.PricePerDay,
                        PropertyTypeId = propertyTypeId,
                        RoomAndBedId = roomAndBed.Id
                    };

                    Insert.IntoTable(PropertyTable).Row(propertyForDb);
                }
            }
        }

        private void MigratePlaceTypesInDatabase()
        {
            for (short id = 1; id < (short) PlaceTypeId.Default; id++)
            {
                Insert.IntoTable(PlaceTypeTable)
                    .WithIdentityInsert()
                    .Row(new
                    {
                        Type = Enum.GetName(typeof(PlaceTypeId), id)!
                    });
            }
        }

        private void MigratePropertyTypesInDatabase()
        {
            for (short id = 1; id < (short) PropertyTypeId.Default; id++)
            {
                Insert.IntoTable(PropertyTypeTable)
                    .WithIdentityInsert()
                    .Row(new
                    {
                        Type = Enum.GetName(typeof(PropertyTypeId), id)!
                    });
            }
        }

        private short GetIdOfPlaceType(string placeType)
        {
            return placeType switch
            {
                "EntirePlace" => (short) PlaceTypeId.EntirePlace,
                "PrivateRoom" => (short) PlaceTypeId.PrivateRoom,
                "SharedRoom" => (short) PlaceTypeId.SharedRoom,
                _ => (short) PlaceTypeId.Default
            };
        }

        private short GetIdOfPropertyType(string propertyType)
        {
            return propertyType switch
            {
                "Apartment" => (short) PropertyTypeId.Apartment,
                "GuestHouse" => (short) PropertyTypeId.GuestHouse,
                "House" => (short) PropertyTypeId.House,
                "Hotel" => (short) PropertyTypeId.Hotel,
                _ => (short) PropertyTypeId.Default
            };
        }
    }
}