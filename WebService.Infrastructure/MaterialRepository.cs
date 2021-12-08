using System.Collections.ObjectModel;
using System.Linq;

namespace WebService.Infrastructure
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IContext _context;

        public MaterialRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO createMaterialDTO)
        {
            var materialDTO = ConvertCreateMaterialDTOToMaterialDTO(createMaterialDTO, -1);

            if (!ValidTags(createMaterialDTO.Tags).Result || InvalidInput(createMaterialDTO)) return (Status.BadRequest, materialDTO);

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
                                        createMaterialDTO.URL,
                                        createMaterialDTO.Content,
                                        createMaterialDTO.Title,
                                        createMaterialDTO.Authors,
                                        createMaterialDTO.TimeStamp
                                        );
        }

        private static MaterialDTO ConvertMaterialToMaterialDTO(Material entity)
        {
            return new MaterialDTO(
                                entity.Id,
                                entity.WeightedTags.Select(e => new CreateWeightedTagDTO(e.Name, e.Weight)).ToList(),
                                entity.Ratings.Select(e => new CreateRatingDTO(e.Value, e.Reviewer)).ToList(),
                                entity.Levels.Select(e => new CreateLevelDTO(e.Name)).ToList(),
                                entity.ProgrammingLanguages.Select(e => new CreateProgrammingLanguageDTO(e.Name)).ToList(),
                                entity.Medias.Select(e => new CreateMediaDTO(e.Name)).ToList(),
                                new CreateLanguageDTO(entity.Language.Name),
                                entity.Summary,
                                entity.URL,
                                entity.Content,
                                entity.Title,
                                entity.Authors.Select(e => new CreateAuthorDTO(e.FirstName, e.SurName)).ToList(),
                                entity.TimeStamp
                                );
        }

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
                createMaterialDTO.URL,
                createMaterialDTO.Content,
                createMaterialDTO.Title,
                createMaterialDTO.Authors.Select(e => new Author(e.FirstName, e.SurName)).ToList(),
                createMaterialDTO.TimeStamp
            );
        }

        public async Task<Status> DeleteAsync(int materialId)
        {
            var material = await _context.Materials.FindAsync(materialId);

            if (material == null) return Status.NotFound;

            _context.Materials.Remove(material);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, MaterialDTO)> ReadAsync(int materialId)
        {
            var query = from m in _context.Materials
                        where m.Id == materialId
                        select m;

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound,CreateEmptyMaterialDTO());

            return (Status.Found, ConvertMaterialToMaterialDTO(category));
        }

        public async Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
        {
            return await _context.Materials.Select(m => ConvertMaterialToMaterialDTO(m)).ToListAsync();
        }

        public async Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput)
        {
            var materials = _context.Materials.AsEnumerable()
                                              .Where(material => material.hasMinimumAverageRating(searchInput.Rating))
                                              .Where(MayContainProgrammingLanguage(searchInput))
                                              .Where(MayContainLanguage(searchInput))
                                              .Where(MayContainMedia(searchInput))
                                              .Where(MayContainTag(searchInput))
                                              .Where(MayContainLevel(searchInput))
                                              .Select(ConvertMaterialToMaterialDTO)
                                              .ToList();

            if (materials.Count() == 0)
            {
                return (Status.NotFound, new ReadOnlyCollection<MaterialDTO>(new List<MaterialDTO>()));
            }

            return (Status.Found, new ReadOnlyCollection<MaterialDTO>(materials));
        }

        public static Func<Material, bool> MayContainLevel(SearchForm searchInput)
            => material => searchInput.Levels.Any()
                ? material.Levels.Any(level => searchInput.Levels.Any(searchInputLevels => searchInputLevels.Name == level.Name))
                : true;
        

        public static Func<Material, bool> MayContainTag(SearchForm searchInput)
            =>  material => searchInput.Tags.Any()
                  ? material.WeightedTags.Any(wt => searchInput.Tags.Any(st => st.Name == wt.Name)) 
                  : true;
        

        public static Func<Material, bool> MayContainMedia(SearchForm searchInput)
            => material => searchInput.Medias.Any()
                ? material.Medias.Any(media => searchInput.Medias.Any(mediadto => mediadto.Name == media.Name))
                : true;
        

        public static Func<Material, bool> MayContainLanguage(SearchForm searchInput)
            => material => searchInput.Languages.Any()
                ? searchInput.Languages.Any(l => l.Name == material.Language.Name) 
                : true;

        public static Func<Material, bool> MayContainProgrammingLanguage(SearchForm searchInput)
            => material => searchInput.ProgrammingLanguages.Any()
                ? material.ProgrammingLanguages.Select(pl => pl.Name)
                                                .Any(pl => searchInput.ProgrammingLanguages.Select(sipl => sipl.Name)
                                                                                            .Any(sipl => pl == sipl)) 
                : true;

        public async Task<Status> UpdateAsync(MaterialDTO materialDTO)
        {
            if (!ValidTags(materialDTO.Tags).Result || InvalidInput(materialDTO)) return Status.BadRequest;

            var existing = await(from m in _context.Materials
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

        private static MaterialDTO CreateEmptyMaterialDTO()
        {
            var tags = new List<CreateWeightedTagDTO> {};
            var ratings = new List<CreateRatingDTO> {};
            var levels = new List<CreateLevelDTO> {};
            var programmingLanguages = new List<CreateProgrammingLanguageDTO> {};
            var medias = new List<CreateMediaDTO> {};
            var language = new CreateLanguageDTO("");
            var summary = "";
            var url = "";
            var content = "";
            var title = "";
            var authors = new List<CreateAuthorDTO>() {};
            var datetime = DateTime.UtcNow;
            
            var material = new MaterialDTO(-1,tags,ratings,levels,programmingLanguages,medias,language,summary,url,content,title,authors,datetime);
            return material;
        }

        private async Task<ICollection<Media>> ReadMedias(ICollection<CreateMediaDTO> mediaDTOs)
        {
            var mediaDTONames = mediaDTOs.Select(e => e.Name).ToHashSet();
            var medias = await _context.Medias.Where(e => mediaDTONames.Contains(e.Name)).ToListAsync();
            if (medias.Count != mediaDTOs.Count) throw new Exception("Bad request");
            else return medias;
        }

        private async Task<ICollection<Level>> ReadLevels(ICollection<CreateLevelDTO> levelDTOs)
        {
            var levelDTONames = levelDTOs.Select(e => e.Name).ToHashSet();
            var levels = await _context.Levels.Where(e => levelDTONames.Contains(e.Name)).ToListAsync();
            if (levels.Count != levelDTOs.Count) throw new Exception("Bad request");
            else return levels;
        }

        private async Task<ICollection<ProgrammingLanguage>> ReadProgrammingLanguages(ICollection<CreateProgrammingLanguageDTO> programmingLanguageDTOs)
        {
            var ProgrammingLanguageDTONames = programmingLanguageDTOs.Select(e => e.Name).ToHashSet();
            var programmingLanguages = await _context.ProgrammingLanguages.Where(e => ProgrammingLanguageDTONames.Contains(e.Name)).ToListAsync();
            if (programmingLanguages.Count != programmingLanguageDTOs.Count) throw new Exception("Bad request");
            else return programmingLanguages;
        }

        private async Task<bool> ValidTags(ICollection<CreateWeightedTagDTO> tags)
        {

            var existingTags = await (from t in _context.Tags
                                      select new TagDTO(t.Id, t.Name)).ToListAsync();

            bool allTagsExists = false;
            foreach (var tag in tags)
            {
                allTagsExists = existingTags.Select(e => e.Name).Contains(tag.Name);
                if (!allTagsExists) break;
            }

            return allTagsExists;
        }
        private bool InvalidInput(CreateMaterialDTO material)
        {
            var stringList = new List<string>();
            stringList.Append(material.Title);
            stringList.Append(material.Language.Name);
            stringList.AddRange(material.Medias.Select(e => e.Name).ToList());
            stringList.AddRange(material.Authors.Select(e => e.FirstName).ToList());
            stringList.AddRange(material.Authors.Select(e => e.SurName).ToList());
            stringList.AddRange(material.Levels.Select(e => e.Name).ToList());
            stringList.AddRange(material.ProgrammingLanguages.Select(e => e.Name).ToList());
            stringList.AddRange(material.Ratings.Select(e => e.Reviewer).ToList());
            stringList.AddRange(material.Tags.Select(e => e.Name).ToList());

            foreach (var name in stringList)
            {
                if (name.Length > 50 || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    return true;
                }
            }

            if (material.Summary.Length > 250 || string.IsNullOrEmpty(material.Summary) || string.IsNullOrWhiteSpace(material.Summary)) return true;

            foreach (var rating in material.Ratings)
            {
                if (rating.Value < 1 || rating.Value > 10) return true;
            }

            foreach (var tag in material.Tags)
            {
                if (tag.Weight < 1 || tag.Weight > 100) return true;
            }


            return false;

        }
    }
}

