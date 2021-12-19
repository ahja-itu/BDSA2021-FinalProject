// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="Material.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>Class Material.</summary>
public class Material
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:WebService.Entities.Material" /> class. Only used for
    ///     DBContext.
    /// </summary>
    public Material()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Material" /> class including id.</summary>
    public Material(int id, ICollection<WeightedTag> weightedTags, ICollection<Rating> ratings,
        ICollection<Level> levels, ICollection<ProgrammingLanguage> programmingLanguages, ICollection<Media> medias,
        Language language, string summary, string url, string content, string title, ICollection<Author> authors,
        DateTime timeStamp)
    {
        Id = id;
        WeightedTags = weightedTags;
        Ratings = ratings;
        Levels = levels;
        ProgrammingLanguages = programmingLanguages;
        Medias = medias;
        Language = language;
        Summary = summary;
        URL = url;
        Content = content;
        Title = title;
        Authors = authors;
        TimeStamp = timeStamp;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Material" /> class.</summary>
    public Material(ICollection<WeightedTag> weightedTags, ICollection<Rating> ratings, ICollection<Level> levels,
        ICollection<ProgrammingLanguage> programmingLanguages, ICollection<Media> medias, Language language,
        string summary, string url, string content, string title, ICollection<Author> authors, DateTime timeStamp)
    {
        WeightedTags = weightedTags;
        Ratings = ratings;
        Levels = levels;
        ProgrammingLanguages = programmingLanguages;
        Medias = medias;
        Language = language;
        Summary = summary;
        URL = url;
        Content = content;
        Title = title;
        Authors = authors;
        TimeStamp = timeStamp;
    }

    public int Id { get; set; }
    public ICollection<WeightedTag> WeightedTags { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Level> Levels { get; set; }
    public ICollection<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public ICollection<Media> Medias { get; set; }
    public Language Language { get; set; }

    [StringLength(400)] public string Summary { get; set; }

    public string URL { get; set; }
    public string Content { get; set; }

    [StringLength(50)] public string Title { get; set; }

    public ICollection<Author> Authors { get; set; }
    public DateTime TimeStamp { get; set; }
}