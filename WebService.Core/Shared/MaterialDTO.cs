using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Infrastructure;

namespace WebService.Core.Shared
{
    public record MaterialDTO : CreateMaterialDTO
    {
        public MaterialDTO(int id, IList<TagDTO> tags, IList<CategoryDTO> categories, IList<RatingDTO> ratings, IList<LevelDTO> levels, IList<ProgrammingLanguageDTO> programmingLanguages, IList<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, IList<string> authors, DateTime timeStamp) : base(tags, categories, ratings, levels, programmingLanguages, medias, language, content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    } 
    public record CreateMaterialDTO
    {
        public CreateMaterialDTO(IList<TagDTO> tags, IList<CategoryDTO> categories, IList<RatingDTO> ratings, IList<LevelDTO> levels, IList<ProgrammingLanguageDTO> programmingLanguages, IList<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, IList<string> authors, DateTime timeStamp)
        {
            Tags = tags;
            Categories = categories;
            Ratings = ratings;
            Levels = levels;
            ProgrammingLanguages = programmingLanguages;
            Medias = medias;
            Language = language;
            Content = content;
            Title = title;
            Authors = authors;
            TimeStamp = timeStamp;
        }

        public IList<TagDTO> Tags { get; init; }
        public IList<CategoryDTO> Categories { get; init; }
        public IList<RatingDTO> Ratings { get; init; }
        public IList<LevelDTO> Levels  { get; init; }
        public IList<ProgrammingLanguageDTO> ProgrammingLanguages { get; init; }
        public IList<MediaDTO> Medias { get; init; }
        public LanguageDTO Language { get; init; }
        public IPresentableMaterial Content { get; init; }
        [StringLength(50)]
        public string Title { get; init; }
        public IList<string> Authors { get; init; }
        public DateTime TimeStamp { get; init; }
    }

    public record UpdateMaterialDTO : CreateMaterialDTO
    {
        public UpdateMaterialDTO(int id, IList<TagDTO> tags, IList<CategoryDTO> categories, IList<RatingDTO> ratings, IList<LevelDTO> levels, IList<ProgrammingLanguageDTO> programmingLanguages, IList<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, IList<string> authors, DateTime timeStamp) : base(tags, categories, ratings, levels, programmingLanguages, medias, language, content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
