using ET.FakeText;



/// <summary>
/// Generate titles and bodies of text. 
/// Title generation is done without dependencies, all from within this class.
/// Text body generation leverages the FakeTextGenerator by etcoding. You can find the remote repository for this libarry here: https://github.com/etcoding/FakeTextGenerator
/// </summary>
public class ContentGenerator
{


    public enum Language
    {
        RUSSIAN,
        ENGLISH,
        ITALIAN,
        DANISH,
        UNKNOWN
    }

    private static Random _rand = new Random();

    private Dictionary<Language, TextGenerator> _textGenerators;

    private IWebHostEnvironment _environment;

    // Special thanks to https://www.nichelaboratory.com/Home/BlogTitleGenerator for generated clickbait titles
    private static string[] _templateTitles = new string[]
    {
        "Creating a webapp with $1",
        "Creating a high performance backend with $1",
        "Understanding $1",
        "$1 is being deprecated in 2022",
        "$1 is so bad, Oracle is going to sue you for using it",
        "Demystifying $1",
        "Earn $100,000 a year with $1",
        "Become a milionaire with $1 in 2022",
        "Announcing $1",
        "Introduction to $1",
        "$1: Your first project",
        "$1: Best practices",
        "$1: Core concepts",
        "Azure for $1: getting started",
        "Azure for $1: advanced course",
        "$1 in the Cloud: Getting Started",
        "$1: Top ten best practices in 2021",
        "When $1 Businesses Grow Too Quickly",
        "A Guide To $1 At Any Age",
        "The Biggest Myth About $1 Exposed",
        "Life After $1",
        "10 Ways You Can Grow Your Creativity Using $1",
        "Don't Just Sit There! Start $1",
        "Successful Stories You Didn’T Know About $1",
        "Want A Thriving Business? Focus On $1!",
        "$1 Creates Experts",
        "Unanswered Questions Into $1 Revealed",
        "14 Ways $1 Can Make You Invincible",
        "Must Have List Of $1 Networks",
        "$1 - Not For Everyone",
        "How To Save Money With $1?",
        "The Unadvertised Details Into $1 That Most People Don't Know About",
        "Kids Love $1",
        "Believe In Your $1 Skills But Never Stop Improving",
        "$1 For Beginners And Everyone Else",
        "7 Ways To Reinvent Your $1",
        "Where Will $1 Be 12 Months From Now?",
        "12 Tips To Start Building A $1 You Always Wanted",
        "Take Advantage Of $1 - Read These 9 Tips",
    };


    public ContentGenerator(IWebHostEnvironment? environment = null)
    {
        _environment = environment;
        _textGenerators = new Dictionary<Language, TextGenerator>();

        var languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
        foreach (var language in languages)
        {
            var (ok, lang) = LanguageToString(language);
            if (!ok)
            {
                continue;
            }

            _textGenerators.Add(language, GetTextGenerator(lang));
        }
    }
    private TextGenerator GetTextGenerator(string filename)
    {
        var fileLocation = GetDataFileLocation($"{filename}.corpus.txt");
        var corpusContent = File.ReadAllText(fileLocation);
        var corpus = Corpus.CreateFromText(corpusContent);
        return new TextGenerator(corpus);
    }

    /// <summary>
    /// Converts a language to its string representation.
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public (bool, string) LanguageToString(Language lang) => lang switch
    {
        Language.DANISH => (true, "danish"),
        Language.ENGLISH => (true, "english"),
        Language.ITALIAN => (true,"italian"),
        Language.RUSSIAN => (true, "russian"),
        _ => (false, "")
    };

    /// <summary>
    /// Will convert a string to a language enum. If it wasnt understood or "russian", the language will be returned as russian (lol)
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public (bool, Language) StringToLanguage(string lang) => lang switch
    {
        "danish" => (true, Language.DANISH),
        "english" => (true, Language.ENGLISH),
        "italian" => (true, Language.ITALIAN),
        "russian" => (true, Language.RUSSIAN),
        _ => (false, Language.UNKNOWN)
    };

    /// <summary>
    /// Generate a body of text with a default length of 20 words. The length can be overridden to your liking.
    /// </summary>
    /// <param name="length">The number of words in the generated body of text.</param>
    /// <returns>(bool, string): bool: was the operation successful. The string with content if the text generation was succesfull.</returns>
    public (bool, string) GenerateText(Language lang, int length = 20)
    {

        var generator = _textGenerators.GetValueOrDefault(lang);
        if (generator != null)
        {
            var text = _textGenerators.GetValueOrDefault(lang).GenerateText(length > 0 ? length : 20);
            return (text != null, text ?? "");
        }

        return (false, "");
    }


    /// <summary>
    /// Generates a title from any given number of tags.
    /// </summary>
    /// <param name="tags">A list of tags containing non-empty names. Has to contain either one or many tags.</param>
    /// <returns>(bool, string): the boolean indicates if the operation was successful. The string may contain the title if the operation was successfull. Otherwise it will be empty.</returns>
    public (bool, string) GenerateTitle(IList<CreateWeightedTagDTO> tags)
    {
        if (tags == null || !tags.Any())
        {
            return (false, "");
        }

        if (tags.Count > 1)
        {
            return (true, CreateTitleWithManyTags(tags));
        }

        return (true, CreateTitleWithOneTag(tags[0]));
    }

    private string CreateTitleWithManyTags(IList<CreateWeightedTagDTO> tags)
    {
        var accumulatedTagNames = "";
        var separator = "";

        for (int i = 0; i < tags.Count; i++)
        {
            if (i == tags.Count - 1)
            {
                accumulatedTagNames += " and " + tags[i].Name;
                continue;
            }

            accumulatedTagNames += separator + tags[i].Name;
            separator = ", ";
        }

        return CreateTitleWithOneTag(new CreateWeightedTagDTO(accumulatedTagNames, 0));
    }

    private string CreateTitleWithOneTag(CreateWeightedTagDTO tag)
         => GetRandomTitle().Replace("$1", tag.Name);

    private string GetRandomTitle()
        => _templateTitles[_rand.Next(_templateTitles.Length)];

    private string GetDataFileLocation(string filename)
        => _environment == null
            ? $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\data\\{filename}" // Directory for when testing
            : $"{Directory.GetCurrentDirectory()}\\..\\..\\data\\{filename}"; // Directory when app is running


}
