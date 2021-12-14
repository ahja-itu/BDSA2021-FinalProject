// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MaterialDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Globalization;

namespace WebService.Core.Shared
{
    /// <summary>
    ///     Class MaterialDTO.
    ///     Implements the <see cref="T:WebService.Core.Shared.CreateMaterialDTO" />
    ///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateMaterialDTO}" />
    ///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.MaterialDTO}" />
    /// </summary>
    public record MaterialDTO : CreateMaterialDTO
    {
        public MaterialDTO(int id, ICollection<CreateWeightedTagDTO> tags, ICollection<CreateRatingDTO> ratings,
            ICollection<CreateLevelDTO> levels, ICollection<CreateProgrammingLanguageDTO> programmingLanguages,
            ICollection<CreateMediaDTO> medias, CreateLanguageDTO language, string summary, string url, string content,
            string title, ICollection<CreateAuthorDTO> authors, DateTime timeStamp) : base(tags, ratings, levels,
            programmingLanguages, medias, language, summary, url, content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    }

    /// <summary>
    ///     Class CreateMaterialDTO.
    ///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateMaterialDTO}" />
    /// </summary>
    public record CreateMaterialDTO
    {
        /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateMaterialDTO" /> class.</summary>
        /// <param name="tags">The tags.</param>
        /// <param name="ratings">The ratings.</param>
        /// <param name="levels">The levels.</param>
        /// <param name="programmingLanguages">The programming languages.</param>
        /// <param name="medias">The medias.</param>
        /// <param name="language">The language.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <param name="title">The title.</param>
        /// <param name="authors">The authors.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public CreateMaterialDTO(ICollection<CreateWeightedTagDTO> tags, ICollection<CreateRatingDTO> ratings,
            ICollection<CreateLevelDTO> levels, ICollection<CreateProgrammingLanguageDTO> programmingLanguages,
            ICollection<CreateMediaDTO> medias, CreateLanguageDTO language, string summary, string url, string content,
            string title, ICollection<CreateAuthorDTO> authors, DateTime timeStamp)
        {
            Tags = tags;
            Ratings = ratings;
            Levels = levels;
            ProgrammingLanguages = programmingLanguages;
            Medias = medias;
            Language = language;
            Summary = summary;
            Url = url;
            Content = content;
            Title = title;
            Authors = authors;
            TimeStamp = timeStamp;
        }

        public ICollection<CreateWeightedTagDTO> Tags { get; }
        public ICollection<CreateRatingDTO> Ratings { get; }
        public ICollection<CreateLevelDTO> Levels { get; }
        public ICollection<CreateProgrammingLanguageDTO> ProgrammingLanguages { get; }
        public ICollection<CreateMediaDTO> Medias { get; }
        public CreateLanguageDTO Language { get; }

        [StringLength(400)] public string Summary { get; }

        public string Url { get; }
        public string Content { get; }

        [StringLength(50)] public string Title { get; }

        public ICollection<CreateAuthorDTO> Authors { get; }
        public DateTime TimeStamp { get; }
    }
}

namespace WebService.Core.Shared
{
    /// <summary>Class MaterialExtensions.</summary>
    public static class MaterialExtensions
    {
        /// <summary>Average rating calculator.</summary>
        /// <param name="material">The material.</param>
        /// <returns>System.Single.</returns>
        public static float AverageRating(this CreateMaterialDTO material)
        {
            float sum = material.Ratings.Sum(e => e.Value);
            return sum / material.Ratings.Count;
        }

        /// <summary>Average rating to string.</summary>
        /// <param name="material">The material.</param>
        /// <returns>System.String.</returns>
        public static string AverageRatingToString(this CreateMaterialDTO material)
        {
            return AverageRating(material).ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>Levels to string.</summary>
        /// <param name="material">The material.</param>
        /// <returns>System.String.</returns>
        public static string LevelsToString(this CreateMaterialDTO material)
        {
            var levels = material.Levels.Aggregate("", (current, level) => current + level.Name + " ");
            return levels.Remove(levels.Length - 1);
        }

        /// <summary>Authors to string.</summary>
        /// <param name="material">The material.</param>
        /// <returns>System.String.</returns>
        public static string AuthorsToString(this CreateMaterialDTO material)
        {
            var authors = material.Authors.Aggregate("Authors: ",
                (current, author) => current + author.FirstName + " " + author.SurName + ", ");
            return authors.Remove(authors.Length - 2);
        }

        /// <summary>Converts material to materialDto.</summary>
        /// <param name="material">The material.</param>
        /// <returns>MaterialDTO.</returns>
        public static MaterialDTO ConvertToMaterialDTO(this Material material)
        {
            return new MaterialDTO(
                material.Id,
                material.WeightedTags.Select(e => new WeightedTagDTO(e.Id, e.Name, e.Weight)).Cast<CreateWeightedTagDTO>().ToList(),
                material.Ratings.Select(e => new RatingDTO(e.Id, e.Value,e.Reviewer)).Cast<CreateRatingDTO>().ToList(),
                material.Levels.Select(e => new LevelDTO(e.Id,e.Name)).Cast<CreateLevelDTO>().ToList(),
                material.ProgrammingLanguages.Select(e => new ProgrammingLanguageDTO(e.Id,e.Name)).Cast<CreateProgrammingLanguageDTO>().ToList(),
                material.Medias.Select(e => new MediaDTO(e.Id,e.Name)).Cast<CreateMediaDTO>().ToList(),
                new CreateLanguageDTO(material.Language.Name), //ANNE, changes from LanguageDTO
                material.Summary,
                material.URL,
                material.Content,
                material.Title,
                material.Authors.Select(e => new AuthorDTO(e.Id, e.FirstName, e.SurName)).Cast<CreateAuthorDTO>()
                    .ToList(),
                material.TimeStamp
            );
        }
    }
}