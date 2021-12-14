using ET.FakeText;



/// <summary>
/// Generate titles and bodies of text. 
/// Title generation is done without dependencies, all from within this class.
/// Text body generation leverages the FakeTextGenerator by etcoding. You can find the remote repository for this libarry here: https://github.com/etcoding/FakeTextGenerator
/// </summary>
public class ContentGenerator
{

    private static Random _rand = new Random();

    private TextGenerator _textGenerator;

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

    public ContentGenerator()
    {
        var corpusContent = File.ReadAllText(@"..\..\data\english.corpus.txt");
        var corpus = Corpus.CreateFromText(corpusContent);
        _textGenerator = new TextGenerator(corpus);
    }

    /// <summary>
    /// Generate a body of text with a default length of 20 words. The length can be overridden to your liking.
    /// </summary>
    /// <param name="length">The number of words in the generated body of text.</param>
    /// <returns>(bool, string): bool: was the operation successful. The string with content if the text generation was succesfull.</returns>
    public (bool, string) GenerateText(int length = 20)
    {
        var text = _textGenerator.GenerateText(length > 0 ? length : 20);
        return (text != null, text ?? "");
    }


    /// <summary>
    /// Generates a title from any given number of tags.
    /// </summary>
    /// <param name="tags">A list of tags containing non-empty names. Has to contain either one or many tags.</param>
    /// <returns>(bool, string): the boolean indicates if the operation was successful. The string may contain the title if the operation was successfull. Otherwise it will be empty.</returns>
    public static (bool, string) GenerateTitle(IList<CreateWeightedTagDTO> tags)
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

    private static string CreateTitleWithManyTags(IList<CreateWeightedTagDTO> tags)
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

    private static string CreateTitleWithOneTag(CreateWeightedTagDTO tag)
         => GetRandomTitle().Replace("$1", tag.Name);

    private static string GetRandomTitle()
        => _templateTitles[_rand.Next(_templateTitles.Length)];
}
