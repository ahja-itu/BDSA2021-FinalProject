// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 12-14-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ContentGenerator.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ET.FakeText;

namespace WebService.Core.Server.Model;

/// <summary>
///     Generate titles and bodies of text.
///     Title generation is done without dependencies, all from within this class.
///     Text body generation leverages the FakeTextGenerator by etc coding. You can find the remote repository for this
///     Library here: https://github.com/etcoding/FakeTextGenerator
/// </summary>
public class ContentGenerator
{
    /// <summary>
    ///     Enum Language
    /// </summary>
    public enum Language
    {
        /// <summary>
        ///     The russian language
        /// </summary>
        Russian,

        /// <summary>
        ///     The english language
        /// </summary>
        English,

        /// <summary>
        ///     The italian language
        /// </summary>
        Italian,

        /// <summary>
        ///     The danish language
        /// </summary>
        Danish,

        /// <summary>
        ///     The unknown language
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     The random instance
    /// </summary>
    private static readonly Random Rand = new();

    // Special thanks to https://www.nichelaboratory.com/Home/BlogTitleGenerator for generated clickbait titles
    /// <summary>
    ///     Template titles
    /// </summary>
    private static readonly string[] TemplateTitles =
    {
        "Creating a webapp with $1",
        "Creating a high performance backend with $1",
        "Understanding $1",
        "$1 is being deprecated in 2022",
        "$1 is so bad, Oracle is going to sue you for using it",
        "Demystifying $1",
        "Earn $100,000 a year with $1",
        "Become a millionaire with $1 in 2022",
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
        "Successful Stories You Did not Know About $1",
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
        "Take Advantage Of $1 - Read These 9 Tips"
    };

    /// <summary>
    ///     The environment
    /// </summary>
    private readonly IWebHostEnvironment? _environment;

    /// <summary>
    ///     The text generators
    /// </summary>
    private readonly Dictionary<Language, TextGenerator> _textGenerators;


    /// <summary>
    ///     Initializes a new instance of the <see cref="ContentGenerator" /> class.
    /// </summary>
    /// <param name="environment">The environment.</param>
    public ContentGenerator(IWebHostEnvironment? environment = null)
    {
        _environment = environment;
        _textGenerators = new Dictionary<Language, TextGenerator>();

        var languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
        foreach (var language in languages)
        {
            var (ok, lang) = LanguageToString(language);
            if (!ok) continue;

            _textGenerators.Add(language, GetTextGenerator(lang));
        }
    }

    /// <summary>
    ///     Gets the text generator.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>TextGenerator.</returns>
    private TextGenerator GetTextGenerator(string filename)
    {
        var fileLocation = GetDataFileLocation($"{filename}.corpus.txt");
        var corpusContent = File.ReadAllText(fileLocation);
        var corpus = Corpus.CreateFromText(corpusContent);
        return new TextGenerator(corpus);
    }

    /// <summary>
    ///     Converts a language to its string representation.
    /// </summary>
    /// <param name="lang">The language.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, System.String&gt;.</returns>
    public static (bool, string) LanguageToString(Language lang)
    {
        return lang switch
        {
            Language.Danish => (true, "danish"),
            Language.English => (true, "english"),
            Language.Italian => (true, "italian"),
            Language.Russian => (true, "russian"),
            _ => (false, "")
        };
    }

    /// <summary>
    ///     Will convert a string to a language enum. If it was not understood or "russian", the language will be returned as
    ///     russian (lol)
    /// </summary>
    /// <param name="lang">The language.</param>
    /// <returns>System.ValueTuple&lt;System.Boolean, Language&gt;.</returns>
    public static (bool, Language) StringToLanguage(string lang)
    {
        return lang switch
        {
            "danish" => (true, Language.Danish),
            "english" => (true, Language.English),
            "italian" => (true, Language.Italian),
            "russian" => (true, Language.Russian),
            _ => (false, Language.Unknown)
        };
    }

    /// <summary>
    ///     Generate a body of text with a default length of 20 words. The length can be overridden to your liking.
    /// </summary>
    /// <param name="language">The language.</param>
    /// <param name="length">The number of words in the generated body of text.</param>
    /// <returns>
    ///     (bool, string): bool: was the operation successful. The string with content if the text generation was
    ///     successful.
    /// </returns>
    public (bool, string) GenerateText(Language language, int length = 20)
    {
        var generator = _textGenerators.GetValueOrDefault(language);
        if (generator == null) return (false, "");
        var text = _textGenerators.GetValueOrDefault(language)!.GenerateText(length > 0 ? length : 20);
        return (text != null, text ?? "");
    }


    /// <summary>
    ///     Generates a title from any given number of tags.
    /// </summary>
    /// <param name="tags">A list of tags containing non-empty names. Has to contain either one or many tags.</param>
    /// <returns>
    ///     (bool, string): the boolean indicates if the operation was successful. The string may contain the title if the
    ///     operation was success full. Otherwise it will be empty.
    /// </returns>
    public static (bool, string) GenerateTitle(IList<CreateWeightedTagDTO> tags)
    {
        if (!tags.Any()) return (false, "");

        return tags.Count > 1 ? (true, CreateTitleWithManyTags(tags)) : (true, CreateTitleWithOneTag(tags[0]));
    }

    /// <summary>
    ///     Creates the title with many tags.
    /// </summary>
    /// <param name="tags">The tags.</param>
    /// <returns>System.String.</returns>
    private static string CreateTitleWithManyTags(IList<CreateWeightedTagDTO> tags)
    {
        var accumulatedTagNames = "";
        var separator = "";

        for (var i = 0; i < tags.Count; i++)
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

    /// <summary>
    ///     Creates the title with one tag.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns>System.String.</returns>
    private static string CreateTitleWithOneTag(CreateTagDTO tag)
    {
        return GetRandomTitle().Replace("$1", tag.Name);
    }

    /// <summary>
    ///     Gets the random title.
    /// </summary>
    /// <returns>System.String.</returns>
    private static string GetRandomTitle()
    {
        return TemplateTitles[Rand.Next(TemplateTitles.Length)];
    }

    /// <summary>
    ///     Gets the data file location.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>System.String.</returns>
    private string GetDataFileLocation(string filename)
    {
        return _environment == null
            ? $"{Directory.GetCurrentDirectory()}/../../../../data/{filename}" // Directory for when testing
            : $"{Directory.GetCurrentDirectory()}/../../data/{filename}";
        // Directory when app is running
    }
}