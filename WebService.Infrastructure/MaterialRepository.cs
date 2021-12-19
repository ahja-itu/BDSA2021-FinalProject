// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MaterialRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure;

/// <summary>
///     Class MaterialRepository.
///     Implements the <see cref="WebService.Core.Shared.IMaterialRepository" /> interface
/// </summary>
public class MaterialRepository : IMaterialRepository
{
    private readonly IContext _context;

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Infrastructure.MaterialRepository" /> class.</summary>
    public MaterialRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>Create a material asynchronously from a given material creation DTO.</summary>
    public async Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO createMaterialDTO)
    {
        var materialDTO = ConvertCreateMaterialDTOToMaterialDTO(createMaterialDTO, -1);

        if (!ValidTags(createMaterialDTO.Tags).Result || InvalidInput(createMaterialDTO))
            return (Status.BadRequest, materialDTO);

        var existing = await (from m in _context.Materials
                where m.Title == createMaterialDTO.Title
                select m)
            .FirstOrDefaultAsync();
        if (existing != null) return (Status.Conflict, ConvertMaterialToMaterialDTO(existing));
        try
        {
            var entity = await ConvertCreateMaterialDTOToMaterial(createMaterialDTO);

            await _context.Materials.AddAsync(entity);

            await _context.SaveChangesAsync();

            var created = ConvertMaterialToMaterialDTO(entity);

            return (Status.Created, created);
        }
        catch (Exception)
        {
            return (Status.BadRequest, materialDTO);
        }
    }

    /// <summary>Delete a material based on id asynchronously.</summary>
    public async Task<Status> DeleteAsync(int materialId)
    {
        var material = await _context.Materials.FindAsync(materialId);

        if (material == null) return Status.NotFound;

        _context.Materials.Remove(material);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>Reads a materials based on id asynchronously and returns them with an http status.</summary>
    public async Task<(Status, MaterialDTO)> ReadAsync(int materialId)
    {
        var query = from m in _context.Materials
            where m.Id == materialId
            select m;

        var category = await query.FirstOrDefaultAsync();

        return category == null
            ? (Status.NotFound, CreateEmptyMaterialDTO())
            : (Status.Found, ConvertMaterialToMaterialDTO(category));
    }

    /// <summary>Reads all materials asynchronously.</summary>
    public async Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
    {
        var materials = await ReadAllMaterials();
        return materials.Select(ConvertMaterialToMaterialDTO).ToList();
    }

    /// <summary>Reads all materials based on given search form asynchronously and returns them 
    /// with an http status.</summary>
    public async Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput)
    {
        var allMaterials = await ReadAllMaterials();
        // We can't have the server translate our query where we do linq statements on searchInput
        var materialsWhereRatingHolds = allMaterials
            .Where(material => GetAverage(material) >= searchInput.Rating)
            .ToList();

        // We're doing the following computations on the client, instead of the server
        var rawMaterials = materialsWhereRatingHolds
            .Where(material => MayContainProgrammingLanguage(searchInput).Invoke(material)
                               || MayContainLanguage(searchInput).Invoke(material)
                               || MayContainMedia(searchInput).Invoke(material)
                               || MayContainTag(searchInput).Invoke(material)
                               || MayContainLevel(searchInput).Invoke(material))
            .ToList();


        var materials = rawMaterials
            .Select(ConvertMaterialToMaterialDTO)
            .ToList();

        return materials.Count == 0
            ? (Status.NotFound, new ReadOnlyCollection<MaterialDTO>(new List<MaterialDTO>()))
            : (Status.Found, new ReadOnlyCollection<MaterialDTO>(materials));
    }

    /// <summary>Update a given material asynchronously.</summary>
    public async Task<Status> UpdateAsync(MaterialDTO materialDTO)
    {
        if (!ValidTags(materialDTO.Tags).Result || InvalidInput(materialDTO)) return Status.BadRequest;

        var existing = await (from m in _context.Materials
            where m.Id != materialDTO.Id
            where m.Title == materialDTO.Title
            select m).AnyAsync();


        if (existing) return Status.Conflict;

        var entity = await _context.Materials.FindAsync(materialDTO.Id);

        if (entity == null) return Status.NotFound;

        try
        {
            var newEntity = await ConvertCreateMaterialDTOToMaterial(materialDTO);

            entity.Ratings = newEntity.Ratings;
            entity.Title = newEntity.Title;
            entity.ProgrammingLanguages = newEntity.ProgrammingLanguages;
            entity.Levels = newEntity.Levels;
            entity.Content = newEntity.Content;
            entity.WeightedTags = newEntity.WeightedTags;
            entity.Ratings = newEntity.Ratings;
            entity.Medias = newEntity.Medias;
            entity.TimeStamp = newEntity.TimeStamp;
            entity.Authors = newEntity.Authors;
            entity.Language = newEntity.Language;
            entity.URL = newEntity.URL;
            entity.Summary = newEntity.Summary;

            await _context.SaveChangesAsync();

            return Status.Updated;
        }
        catch
        {
            return Status.BadRequest;
        }
    }

    /// <summary>Converts the given createMaterialDTO to a materialDTO with given id.</summary>
        private static MaterialDTO ConvertCreateMaterialDTOToMaterialDTO(CreateMaterialDTO createMaterialDTO, int id)
    {
        return new MaterialDTO
        (
            id,
            createMaterialDTO.Tags,
            createMaterialDTO.Ratings,
            createMaterialDTO.Levels,
            createMaterialDTO.ProgrammingLanguages,
            createMaterialDTO.Medias,
            createMaterialDTO.Language,
            createMaterialDTO.Summary,
            createMaterialDTO.Url,
            createMaterialDTO.Content,
            createMaterialDTO.Title,
            createMaterialDTO.Authors,
            createMaterialDTO.TimeStamp
        );
    }

    /// <summary>Converts the material entity to materialDTO.</summary>
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    private static MaterialDTO ConvertMaterialToMaterialDTO(Material entity)
    {
        var id = entity.Id;
        var tags = entity.WeightedTags.Select(e =>
            e == null ? new CreateWeightedTagDTO("", 0) : new CreateWeightedTagDTO(e.Name, e.Weight)).ToList();
        var ratings = entity.Ratings
            .Select(e => e == null ? new CreateRatingDTO(0, "") : new CreateRatingDTO(e.Value, e.Reviewer)).ToList();
        var levels = entity.Levels.Select(e => e == null ? new CreateLevelDTO("") : new CreateLevelDTO(e.Name))
            .ToList();
        var pls = entity.ProgrammingLanguages.Select(e =>
            e == null ? new CreateProgrammingLanguageDTO("") : new CreateProgrammingLanguageDTO(e.Name)).ToList();
        var medias = entity.Medias.Select(e => e == null ? new CreateMediaDTO("") : new CreateMediaDTO(e.Name))
            .ToList();
        var lang = new CreateLanguageDTO(entity.Language.Name);
        var summary = entity.Summary;
        var url = entity.URL;
        var content = entity.Content;
        var title = entity.Title;
        var authors = entity.Authors
            .Select(e => e == null ? new CreateAuthorDTO("", "") : new CreateAuthorDTO(e.FirstName, e.SurName))
            .ToList();
        var timestamp = entity.TimeStamp;

        return new MaterialDTO(
            id,
            tags,
            ratings,
            levels,
            pls,
            medias,
            lang,
            summary,
            url,
            content,
            title,
            authors,
            timestamp
        );
    }

    /// <summary>Converts the given createMaterialDTO to a material.</summary>
    private async Task<Material> ConvertCreateMaterialDTOToMaterial(CreateMaterialDTO createMaterialDTO)
    {
        return new Material(
            createMaterialDTO.Tags.Select(e => new WeightedTag(e.Name, e.Weight)).ToList(),
            createMaterialDTO.Ratings.Select(e => new Rating(e.Value, e.Reviewer)).ToList(),
            await ReadLevels(createMaterialDTO.Levels),
            await ReadProgrammingLanguages(createMaterialDTO.ProgrammingLanguages),
            await ReadMedias(createMaterialDTO.Medias),
            await _context.Languages.Where(e => e.Name == createMaterialDTO.Language.Name).SingleAsync(),
            createMaterialDTO.Summary,
            createMaterialDTO.Url,
            createMaterialDTO.Content,
            createMaterialDTO.Title,
            createMaterialDTO.Authors.Select(e => new Author(e.FirstName, e.SurName)).ToList(),
            createMaterialDTO.TimeStamp
        );
    }

    /// <summary>Reads all materials.</summary>
    private async Task<IList<Material>> ReadAllMaterials()
    {
        return await _context.Materials
            .Include(m => m.Language)
            .Include(m => m.Levels)
            .Include(m => m.Medias)
            .Include(m => m.ProgrammingLanguages)
            .ToListAsync();
    }

    /// <summary>
    ///         Get the average rating of a material
    /// </summary>

    private static double GetAverage(Material material)
    {
        return material.Ratings.Count != 0 ? material.Ratings.Average(rating => rating.Value) : 10;
    }

    /// <summary>Checks wheter a material might contain any of the levels given in the search form.</summary>
        public static Func<Material, bool> MayContainLevel(SearchForm searchInput)
    {
        return material => !searchInput.Levels.Any() || material.Levels.Any(level =>
            searchInput.Levels.Any(searchInputLevels => searchInputLevels.Name == level.Name));
    }


    /// <summary>Checks wheter a material might contain any of the tags given in the search form..</summary>
    public static Func<Material, bool> MayContainTag(SearchForm searchInput)
    {
        return material => !searchInput.Tags.Any() ||
                           material.WeightedTags.Any(wt => searchInput.Tags.Any(st => st.Name == wt.Name));
    }


    /// <summary>
    ///     Checks wheter a material might contain any of the media types given in the search form.
    /// </summary>
    public static Func<Material, bool> MayContainMedia(SearchForm searchInput)
    {
        return material => !searchInput.Medias.Any() ||
                           material.Medias.Any(media =>
                               searchInput.Medias.Any(mediaDTO => mediaDTO.Name == media.Name));
    }


    /// Checks wheter a material might contain any of the languages given in the search form.</summary>
    public static Func<Material, bool> MayContainLanguage(SearchForm searchInput)
    {
        return material =>
            !searchInput.Languages.Any() || searchInput.Languages.Any(l => l.Name == material.Language.Name);
    }

    /// <summaryChecks wheter a material might contain any of the programming languages given in the search form.</summary>
    public static Func<Material, bool> MayContainProgrammingLanguage(SearchForm searchInput)
    {
        return material => !searchInput.ProgrammingLanguages.Any() || material.ProgrammingLanguages
            .Select(pl => pl.Name)
            .Any(pl => searchInput.ProgrammingLanguages
                .Select(searchInputProgrammingLanguageDTO => searchInputProgrammingLanguageDTO.Name)
                .Any(searchInputProgrammingLanguageDTO => pl == searchInputProgrammingLanguageDTO));
    }

    /// <summary>Creates an empty material dto.</summary>
    private static MaterialDTO CreateEmptyMaterialDTO()
    {
        var tags = new List<CreateWeightedTagDTO>();
        var ratings = new List<CreateRatingDTO>();
        var levels = new List<CreateLevelDTO>();
        var programmingLanguages = new List<CreateProgrammingLanguageDTO>();
        var medias = new List<CreateMediaDTO>();
        var language = new CreateLanguageDTO("");
        const string summary = "";
        const string url = "";
        const string content = "";
        const string title = "";
        var authors = new List<CreateAuthorDTO>();
        var datetime = DateTime.UtcNow;

        var material = new MaterialDTO(-1, tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, datetime);
        return material;
    }

    /// <summary>Reads medias from list of media DTOs.</summary>
    private async Task<ICollection<Media>> ReadMedias(ICollection<CreateMediaDTO> mediaDTOs)
    {
        var mediaDTONames = mediaDTOs.Select(e => e.Name).ToHashSet();
        var medias = await _context.Medias.Where(e => mediaDTONames.Contains(e.Name)).ToListAsync();
        if (medias.Count != mediaDTOs.Count) throw new Exception("Bad request");
        return medias;
    }

    /// <summary>Reads levels from list of level DTOs.</summary>
    private async Task<ICollection<Level>> ReadLevels(ICollection<CreateLevelDTO> levelDTOs)
    {
        var levelDTONames = levelDTOs.Select(e => e.Name).ToHashSet();
        var levels = await _context.Levels.Where(e => levelDTONames.Contains(e.Name)).ToListAsync();
        if (levels.Count != levelDTOs.Count) throw new Exception("Bad request");
        return levels;
    }

    /// <summary>Reads programming languages from list of programming language DTOs.</summary>
    private async Task<ICollection<ProgrammingLanguage>> ReadProgrammingLanguages(
        ICollection<CreateProgrammingLanguageDTO> programmingLanguageDTOs)
    {
        var programmingLanguageDTONames = programmingLanguageDTOs.Select(e => e.Name).ToHashSet();
        var programmingLanguages = await _context.ProgrammingLanguages
            .Where(e => programmingLanguageDTONames.Contains(e.Name)).ToListAsync();
        if (programmingLanguages.Count != programmingLanguageDTOs.Count) throw new Exception("Bad request");
        return programmingLanguages;
    }

    /// <summary>Validates that all tags on the list tags contain valid names.</summary>
    private async Task<bool> ValidTags(IEnumerable<CreateWeightedTagDTO> tags)
    {
        var existingTags = await (from t in _context.Tags
            select new TagDTO(t.Id, t.Name)).ToListAsync();

        var allTagsExists = false;
        foreach (var tag in tags)
        {
            allTagsExists = existingTags.Select(e => e.Name).Contains(tag.Name);
            if (!allTagsExists) break;
        }

        return allTagsExists;
    }

    /// <summary>Validates the input material with regards to string lengths and number ranges.</summary>
    private static bool InvalidInput(CreateMaterialDTO material)
    {
        var stringList = new List<string>
        {
            material.Title,
            material.Language.Name
        };
        stringList.AddRange(material.Medias.Select(e => e.Name).ToList());
        stringList.AddRange(material.Authors.Select(e => e.FirstName).ToList());
        stringList.AddRange(material.Authors.Select(e => e.SurName).ToList());
        stringList.AddRange(material.Levels.Select(e => e.Name).ToList());
        stringList.AddRange(material.ProgrammingLanguages.Select(e => e.Name).ToList());
        stringList.AddRange(material.Ratings.Select(e => e.Reviewer).ToList());
        stringList.AddRange(material.Tags.Select(e => e.Name).ToList());

        if (stringList.Any(name => name.Length > 50 || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)))
            return true;

        if (material.Summary.Length > 250 || string.IsNullOrEmpty(material.Summary) ||
            string.IsNullOrWhiteSpace(material.Summary))
            return true;

        return material.Ratings.Any(rating => rating.Value is < 1 or > 10) ||
               material.Tags.Any(tag => tag.Weight is < 1 or > 10);
    }
}