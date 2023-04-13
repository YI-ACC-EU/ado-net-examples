using System.Data;
using Euris.Examples.Common.Models.Entities;
using static Euris.Examples.Data.MovieQueries;
using static Euris.Examples.Data.MovieFields;

namespace Euris.Examples.Data.Extensions;

public static class DataReaderExtensions
{
    public static T GetValueOrDefault<T>(this IDataReader? dataReader, string columnName, T defaultValue)
    {
        if (columnName == null) throw new ArgumentNullException(nameof(columnName));
        if (dataReader is null) return defaultValue;
        var value = dataReader.GetValue(dataReader.GetOrdinal(columnName));
        if (value is T value1) {
            return value1;
        } 
        try {
            return (T)Convert.ChangeType(value, typeof(T));
        } 
        catch (InvalidCastException) {
            return defaultValue;
        }
        //Solo nel caso in qui siamo consapevoli che nascondiamo il problema!
    }

    public static T? GetValue<T>(this IDataReader? dataReader, string columnName)
    {
        if (columnName == null) throw new ArgumentNullException(nameof(columnName));
        if (dataReader is null) return default(T);
        var value = dataReader.GetValue(dataReader.GetOrdinal(columnName));
        if (value is T value1) {
            return value1;
        } 
        try {
            return (T)Convert.ChangeType(value, typeof(T));
        } 
        catch (InvalidCastException) {
            //Problemi sul tipo di dato!
            throw;
        }
    }

    public static Movie MapToMovie(this IDataReader dataReader)
        => new ()
        {
            Id = dataReader.GetValue<int>(MovieId),
            Name = dataReader.GetValue<string>(Title) 
                   ?? throw new MissingFieldException(
                       nameof(Movie), Title) ,
            Budget = dataReader.GetValue<long>(Budget),
            HomePage = dataReader.GetValueOrDefault(HomePage, string.Empty),
            Overview = dataReader.GetValueOrDefault(Overview, string.Empty),
            Popularity = dataReader.GetValue<decimal>(Popularity),
            ReleaseDate = dataReader.GetValueOrDefault(ReleaseDate, string.Empty),
            Revenue = dataReader.GetValue<int>(Revenue),
            RunTime = dataReader.GetValue<int>(Runtime),
            Tagline = dataReader.GetValue<string>(TagLine),
            VoteAverage = dataReader.GetValue<decimal>(VoteAverage),
            VoteCount = dataReader.GetValue<int>(VoteCount),
            Status = dataReader.GetValue<string>(MovieStatus)
        };

    public static Actor MapToActor(this IDataReader dataReader)
        => new()
        {
            Id = dataReader.GetValue<int>(Id),
            Name = dataReader.GetValueOrDefault(Name, string.Empty),
            CharacterName = dataReader.GetValueOrDefault(CharacterName, string.Empty),
            Gender = dataReader.GetValueOrDefault(Gender, string.Empty)
        };

    public static CrewMember MapToCrewMember(this IDataReader dataReader)
        => new ()
        {
            Id = dataReader.GetValue<int>(Id),
            Name = dataReader.GetValueOrDefault(Name, string.Empty),
            Job = dataReader.GetValueOrDefault(Job, string.Empty)
        };

    public static Company MapToCompany(this IDataReader dataReader)
        => new ()
        {
            Id = dataReader.GetValue<int>(Id),
            Name = dataReader.GetValueOrDefault(Name, string.Empty)
        };

    public static Genre MapToGenre(this IDataReader dataReader)
        => new ()
        {
            Id = dataReader.GetValue<int>(Id),
            Name = dataReader.GetValueOrDefault(Name, string.Empty)
        };

    public static Country MapToCountry(this IDataReader dataReader)
        => new ()
        {
            Id = dataReader.GetValue<int>(Id),
            Name = dataReader.GetValueOrDefault(Name, string.Empty),
            IsoCountryCode = dataReader.GetValueOrDefault(IsoCountryCode, string.Empty)
        };
}