// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MediaRepositoryTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

/// <summary>
///     Class MediaRepositoryTests.
///     Contains tests grouped into regions based on the type  of method tested.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class MediaRepositoryTests
{
        private readonly TestVariables _v;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaRepositoryTests" /> class, creating new test variables.
    /// </summary>
    public MediaRepositoryTests()
    {
        _v = new TestVariables();
    }

    #region Create

    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_new_media_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_new_media_with_id()
    {
        var media = new CreateMediaDTO("Article");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.Created, new MediaDTO(4, "Article"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_conflict_and_existing_media.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_conflict_and_existing_media()
    {
        var media = new CreateMediaDTO("Video");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.Conflict, new MediaDTO(3, "Video"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_count_one_more.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_count_one_more()
    {
        var media = new CreateMediaDTO("Article");

        await _v.MediaRepository.CreateAsync(media);

        var actual = _v.MediaRepository.ReadAsync().Result.Count;

        const int expected = 4;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_bad_request_on_name_tooLong()
    {
        var media = new CreateMediaDTO("asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.BadRequest, new MediaDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_bad_request_on_name_empty()
    {
        var media = new CreateMediaDTO("");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.BadRequest, new MediaDTO(-1, ""));

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method CreateAsync_media_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_returns_bad_request_on_name_whitespace()
    {
        var media = new CreateMediaDTO(" ");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.BadRequest, new MediaDTO(-1, " "));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method CreateAsync_media_with_max_length_returns_new_language_with_id.
    /// </summary>
    [Fact]
    public async Task CreateAsync_media_with_max_length_returns_new_language_with_id()
    {
        var media = new CreateMediaDTO("asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.MediaRepository.CreateAsync(media);

        var expected = (Status.Created, new MediaDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar"));

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    /// <summary>
    ///     Defines the test method ReadAsync_media_by_id_returns_media_and_status_found.
    /// </summary>
    [Fact]
    public async Task ReadAsync_media_by_id_returns_media_and_status_found()
    {
        var actual = await _v.MediaRepository.ReadAsync(1);

        var expected = (Status.Found, new MediaDTO(1, "Book"));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAsync_media_by_id_returns_empty_media_and_status_notFound.
    /// </summary>
    [Fact]
    public async Task ReadAsync_media_by_id_returns_empty_media_and_status_notFound()
    {
        var actual = await _v.MediaRepository.ReadAsync(4);

        var expected = (Status.NotFound, new MediaDTO(-1, ""));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method ReadAllAsync_returns_all_medias.
    /// </summary>
    [Fact]
    public async Task ReadAllAsync_returns_all_medias()
    {
        var actual = await _v.MediaRepository.ReadAsync();

        var expected1 = new MediaDTO(1, "Book");
        var expected2 = new MediaDTO(2, "Report");
        var expected3 = new MediaDTO(3, "Video");

        Assert.Collection(actual,
            mediaDTO => Assert.Equal(expected1, mediaDTO),
            mediaDTO => Assert.Equal(expected2, mediaDTO),
            mediaDTO => Assert.Equal(expected3, mediaDTO));
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method DeleteAsync_media_by_id_returns_status_deleted.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_media_by_id_returns_status_deleted()
    {
        var actual = await _v.MediaRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_media_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_media_by_id_returns_status_notFound()
    {
        var actual = await _v.MediaRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method DeleteAsync_media_by_id_returns_count_one_less.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_media_by_id_returns_count_one_less()
    {
        await _v.MediaRepository.DeleteAsync(3);

        var actual = _v.MediaRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    /// <summary>
    ///     Defines the test method UpdateAsync_media_by_id_returns_status_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_by_id_returns_status_updated()
    {
        var updateMediaDTO = new MediaDTO(3, "Article");

        var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_by_id_read_updated_returns_status_found_and_updated_media.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_by_id_read_updated_returns_status_found_and_updated_media()
    {
        var updateMediaDTO = new MediaDTO(3, "Article");

        await _v.MediaRepository.UpdateAsync(updateMediaDTO);

        var actual = await _v.MediaRepository.ReadAsync(3);

        var expected = (Status.Found, updateMediaDTO);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_by_id_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_by_id_returns_status_notFound()
    {
        var updateMediaDTO = new MediaDTO(4, "Article");

        var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_by_id_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_by_id_returns_status_conflict()
    {
        var updateMediaDTO = new MediaDTO(3, "Book");

        var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_returns_bad_request_on_name_tooLong.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_returns_bad_request_on_name_tooLong()
    {
        var media = new MediaDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.MediaRepository.UpdateAsync(media);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_returns_bad_request_on_name_empty.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_returns_bad_request_on_name_empty()
    {
        var media = new MediaDTO(1, "");

        var actual = await _v.MediaRepository.UpdateAsync(media);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }


    /// <summary>
    ///     Defines the test method UpdateAsync_media_returns_bad_request_on_name_whitespace.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_returns_bad_request_on_name_whitespace()
    {
        var media = new MediaDTO(1, " ");

        var actual = await _v.MediaRepository.UpdateAsync(media);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method UpdateAsync_media_with_max_length_returns_updated.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_media_with_max_length_returns_updated()
    {
        var media = new MediaDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.MediaRepository.UpdateAsync(media);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    #endregion
}